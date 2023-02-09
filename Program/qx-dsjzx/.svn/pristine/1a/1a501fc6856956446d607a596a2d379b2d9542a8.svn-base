using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProblemDisposal.Common.Basic;
using ProblemDisposal.Common;
using ProgramsNetCore.Common.Basic;
using ProgramsNetCore.IService;
using ProgramsNetCore.Models.Dto;
using ProgramsNetCore.Models.Dto.DataManageDto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgramsNetCore.Common;
using ProgramsNetCore.Models.Dto.DataCollectDto;
using System.Security.Claims;
using Models;

namespace DataSharing.Controllers
{
    /// <summary>
    /// 数据管理
    /// </summary>
    [Route("")]
    [ApiController]
    public class DataManageController : ControllerBase
    {
        private readonly IDataTableService _dataTableService;
        private readonly IDataMapService _dataMapService;
        private readonly IApprovalTaskService _approvalTaskService;
        private readonly IAuthoriyService _authoriyService;
        private readonly IRuleService _ruleService;
        private readonly IOriRuleService _orService;
        private readonly IConditionsService _condService;
        private readonly IDataMapChildService _dmcService;

        public DataManageController(IDataTableService dataTableService, IDataMapService dataMapService, IApprovalTaskService approvalTaskService, IAuthoriyService authoriyService, IRuleService ruleService, IOriRuleService orService,IConditionsService condService,IDataMapChildService dmcService)
        {
            _dataTableService = dataTableService;
            _dataMapService = dataMapService;
            _approvalTaskService = approvalTaskService;
            _authoriyService = authoriyService;
            _ruleService = ruleService;
            _orService = orService;
            _condService = condService;
            _dmcService = dmcService;
        }


        /// <summary>
        /// 查询数据源表[update:2022年8月25日14:20:40]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<GetDataMapListResultDto>>> GetDataMapList(GetDataMapListQueryDto dto)
        {
            //RefAsync<int> total = 0;
            //string sql = $"select dt.`Id`,dt.`Name` from datamap dt where !dt.IsPrivate and dt.`Status`=1";
            //if (!string.IsNullOrEmpty(dto.KeyWord))
            //{
            //    sql+=$" and dt.`Name` like '%{dto.KeyWord}%'";
            //}
            //var result = await _dataTableService.GetEntityList<GetDataMapListResultDto>(sql);
            //return new PageResult<List<GetDataMapListResultDto>>(result,total);

            if (dto.PageIndex<=0) dto.PageIndex=1;
            if (dto.PageSize<=0) dto.PageSize=10;

            RefAsync<int> total = 0;
            string sql = $"select dt.`Id`,dt.`Name` from datamap dt where !dt.IsPrivate and dt.`Status`=1";
            if (!string.IsNullOrEmpty(dto.KeyWord))
            {
                sql+=$" and dt.`Name` like '%{dto.KeyWord}%'";
            }
            var result = await _dataTableService.GetEntityPageList<GetDataMapListResultDto>(sql, dto.PageIndex, dto.PageSize, total);

            //await Logger.AddPlatformLog("查询数据源表", LogType.DataAccess);

            return new PageResult<List<GetDataMapListResultDto>>(result, total, total%dto.PageSize==0 ? total/dto.PageSize : total/dto.PageSize+1, dto.PageSize, dto.PageIndex);
        }

        /// <summary>
        /// 查询数据源表字段[update:2022年8月25日14:45:13]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<List<GetDataMapFiledsResultDto>>> GetDataMapFileds(GetDataMapFiledsDto dto)
        {
            var dt = await _dataMapService.GetEntity(e => e.Id==dto.Id);
            if (dt is null) return new AjaxResult<List<GetDataMapFiledsResultDto>>(false, "Can't find data");

            string sql = $"select t.`Id`,t.`CnFieldName`,t.`EnFieldName` from data_sharing_main.`dic_{dt.AssociativeTable}` t";
            var result = await _dataTableService.GetEntityList<GetDataMapFiledsResultDto>(sql);

            //await Logger.AddPlatformLog("查询数据源表字段", LogType.DataAccess);

            return new AjaxResult<List<GetDataMapFiledsResultDto>>(result);
        }

        /// <summary>
        /// 数据表整合[update:2022年8月25日15:41:13]
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> DataTableJoinV2(DataTableJoinQueryDto[] items)
        {
            if (items.Length<2) return new AjaxResult<bool>(false, "至少要有2张关联表");
            var majorTable = items[0];
            string majorTableName = _dataMapService.GetEntity(e => e.Id==majorTable.HeadId).Result.AssociativeTable;
            StringBuilder sql = new StringBuilder($"select {string.Join(',', majorTable.ChildList.Select(e => $"t1.`{e.EnFieldName}`"))},@items from {majorTableName} t1");
            List<string> otherFields = new List<string>();
            for (int i = 1; i < items.Length; i++)
            {
                string tableAsFlag = $"t{i+1}";//表别名标志
                var item = items[i];//当前表
                var dt = await _dataMapService.GetEntity(e => e.Id==item.HeadId);
                if (dt is null) return new AjaxResult<bool>(false, "源表不存在");

                var fields = item.ChildList.Select(e => $"{tableAsFlag}.`{e.EnFieldName}`");
                otherFields.Add(string.Join(',', fields));
                sql.Append($" join {dt.AssociativeTable} {tableAsFlag} on t1.{majorTable.EnRadio}={tableAsFlag}.{item.EnRadio}");
            }

            string selectSql = sql.Replace("@items", string.Join(',', otherFields)).ToString();
            string newTableName = $"data_sharing_affiliated.`{Guid.NewGuid().ToString("N")}`";
            string createTableSql = $"use data_sharing_main;create table if not exists {newTableName} {selectSql}";
            var result = await _dataTableService.NoQueryExcuteSql(createTableSql);

            //await Logger.AddPlatformLog("数据表整合V2", LogType.DataOperation);

            return new AjaxResult<bool>(true, "整合成功");
        }


        /// <summary>
        /// 数据表整合{ "表名" : "字段名" }
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<object>> DataTableJoin(Dictionary<string, string> tableFieldDictionary)
        {
            //1.判断连表数量
            if (tableFieldDictionary.Count < 2)
            {
                return new AjaxResult<object>(false, "至少要有2张关联表");
            }

            //2.获取数据表对应字段并去重
            //设置一张主表
            string startTable = tableFieldDictionary.Keys.First();
            //主表所有的字段
            List<dynamic> startTableFields = (await _dataTableService.GetEntityList<dynamic>($"select column_name as columnName FROM information_schema.columns where table_schema= 'data_sharing_main' and table_name = '{startTable}'")).Select(a => a.columnName).ToList();
            Dictionary<string, List<dynamic>> tableFieldsDictionary = new Dictionary<string, List<dynamic>>();
            tableFieldsDictionary.Add(startTable, startTableFields);

            StringBuilder sql = new StringBuilder($"select @* from data_sharing_main.`{startTable}`");
            foreach (var item in tableFieldDictionary)
            {
                //跳过主表
                if (item.Key == startTable)
                {
                    continue;
                }
                var fields = await _dataTableService.GetEntityList<dynamic>($"SELECT column_name columnName FROM information_schema.columns where table_schema= 'data_sharing_main' and table_name = '{item.Key}'");
                tableFieldsDictionary[item.Key] = fields.Where(a => !startTableFields.Contains(a.columnName)).Select(a => a.columnName).ToList();
                string currentTable = $"data_sharing_main.`{item.Key}`";
                sql.Append($" join {currentTable} on data_sharing_main.`{startTable}`.{item.Value} = {currentTable}.{item.Value}");
            }

            //拼接select查询字段
            StringBuilder selectFiledsSql = new StringBuilder();
            IEnumerable<IEnumerable<string>> tableList = tableFieldsDictionary.Select(a => a.Value.Select(e => $"data_sharing_main.`{a.Key}`.`{e}`"));
            List<string> tableFieldsMapper = new List<string>();
            foreach (var currentTable in tableList)
            {
                foreach (var field in currentTable)
                {
                    tableFieldsMapper.Add(field);
                }
            }
            //生成最后要连表查询的字段
            selectFiledsSql.Append(string.Join(',', tableFieldsMapper));

            //3.建表
            //给表一个Guid标记
            var tableGuid = Guid.NewGuid().ToString("N");
            //替换占位符
            string selectSql = sql.ToString().Replace("@*", selectFiledsSql.ToString());
            string createTableName = $"data_sharing_affiliated.`{tableGuid}`";
            string createTableSql = $"create table if not exists {createTableName} {selectSql}";
            var result = await _dataTableService.NoQueryExcuteSql(createTableSql);

            //创建字典表映射
            string createDicTableName = $"data_sharing_affiliated.`dic_{tableGuid}`";
            string createDicTableSql = $"create table if not exists {createDicTableName} (Id int auto_increment primary key,RelationMainTableName varchar(255),RelationColumn varchar(255));Insert into {createDicTableName}(RelationMainTableName,RelationColumn) values {string.Join(',', tableFieldDictionary.Select(item => $"('{item.Key}','{item.Value}')"))}";
            await _dataTableService.NoQueryExcuteSql(createDicTableSql);

            //await Logger.AddPlatformLog("数据表整合", LogType.DataOperation);

            return result > 0 ? new AjaxResult<object>(true) : new AjaxResult<object>(false);

        }


        /// <summary>
        /// 获取规则详情 type 1：原生规则；2：个性化规则
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<GetRuleInfoDto>>> GetRuleInfo(int type)
        {
            string sql = $" select id,name from originalrule where   ruletype={type}  ";

            var rulelist = await _dataTableService.GetEntityList<GetRuleInfoDto>(sql);

            //await Logger.AddPlatformLog($"获取{(type == 1 ? "原生规则" : "个性化规则")}详情", LogType.DataAccess);

            return new AjaxResult<List<GetRuleInfoDto>>(rulelist);


        }


        /// <summary>
        /// 获取规则列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<PageResult<List<ResultRuleInfoPageDto>>> GetRulePage(PageDto dto)
        {
            string sql = " select r.id,r.`name`,r.guid,r.`describe`,(select GROUP_CONCAT(`Name`) from conn_orirule_rule cor join originalrule o on o. id=cor.OId  where cor.RuleId=r.id) MultipleRules   from rule r where 1=1 ";
            RefAsync<int> total = 0;
            var result = await _dataTableService.GetEntityPageList<ResultRuleInfoPageDto>(sql, dto.PageIndex, dto.PageSize, total);

            //await Logger.AddPlatformLog("获取规则列表", LogType.DataAccess);

            return new PageResult<List<ResultRuleInfoPageDto>>(result, total);
        }

        /// <summary>
        /// 增加规则
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<string>> AddRule(AddRuleDto dto)
        {
            string sql = " select max(guid) from rule ";
            var maxguid = await _dataTableService.QueryCount(sql);
            var ruleid = await _ruleService.IdAdd(new Models.rule { Describe = dto.RuleDesc, GUID = await GetNextNO(maxguid as string), Name = dto.RuleName });
            var result = await _orService.Adds(dto.OrgRuleId.Select(i => new Models.conn_orirule_rule { OId=i, RuleId=ruleid }).ToList());

            //await Logger.AddPlatformLog("增加规则", LogType.DataOperation);

            return new AjaxResult<string>(result > 0);
        }

        /// <summary>
        /// 关联个性化规则
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<string>> JoinRule(JoinRuleDto dto)
        {
            var rulelist = await _ruleService.GetEntitys(i => dto.RuleId.Contains(i.Id), i => i);
            var result = rulelist.Count == dto.RuleId.Count;
            if (!result) return new AjaxResult<string>(result, "数据错误");
            var ids = "";
            dto.RuleId.ForEach(i => { ids += (i + ","); });
            ids.TrimEnd(',');
            string sql = $" update rule set PersonaRule={dto.OriRuleId} where id in ({ids}) ";
            result =(await _dataTableService.NoQueryExcuteSql(sql))>0;

            //await Logger.AddPlatformLog("关联个性化规则", LogType.DataOperation);

            return new AjaxResult<string>(result);
        }

        /// <summary>
        /// 生成下一个No
        /// </summary>
        /// <param name="last"></param>
        /// <returns></returns>
        async Task<string> GetNextNO(string last)
        {
            var result = await Task.Factory.StartNew(() =>
            {

                var startstr = last.Substring(0, 1);
                var laststr = last.Substring(1, last.Length - 1);
                laststr = (laststr.ToInt32() + 1).ToString().PadLeft(last.Length - 1, '0');

                return startstr+laststr;
            });

            return result;
        }
        /// <summary>
        /// 分割表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<string>> SplitTable(SplitInfoDto dto)
        {
            string existssql = $" select * from  datamap where 1=1 and id={dto.SourceId} ";
            var existsdata=await _dataTableService.GetEntity<datamap>(existssql);
            if(existsdata==null) return new AjaxResult<string>(false,"数据错误");
            if (dto.SplitTables.Count > 0)
            {
                var result = Task.Factory.StartNew(() => {
                    dto.SplitTables.ForEach(async i => {

                        //3.建表
                        //生成表名称  表名称格式:表名_Guid
                        string dataMapGuid = Guid.NewGuid().ToString("N");
                        string createTableName = dataMapGuid;

                        await _dmcService.Add(new datamap_child { CreateTime = DateTime.Now, MappingTable = createTableName, Name = i.TableName, ParentId = existsdata.Id, UserId=dto.UserId, CN_Conditions=i.CN_Conditions, Conditions=i.Conditions });

                        string createsql = $" use data_sharing_affiliated;create table `{createTableName}` select * from data_sharing_main.`{existsdata.AssociativeTable}` where 1=1 and {i.Conditions}  ;";

                        await _dataTableService.NoQueryExcuteSql(createsql);
                        createsql = $" use data_sharing_affiliated; create table `dic_{createTableName}` select * from data_sharing_main.dic_{existsdata.AssociativeTable} where 1=1;";
                        await _dataTableService.NoQueryExcuteSql(createsql);

                    });
                });
            
            }

            //await Logger.AddPlatformLog("分割表", LogType.DataOperation);

            return new AjaxResult<string>();
        }

        /// <summary>
        /// 分页获取拆分数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<ReturnSplitTableInfoDto>>> GetSplitTablePage(PageDto dto)
        {
            string sql = $"select dt.id,dt.`Name` cn_name,dt.EnName en_name,u.real_name `user`, dt.AssociativeTable,`org`.`Title` as `Title` , `attr`.`Attribution` as `Attribution` , `layer`.`Name` as `LayerName` , `m`.`Name` as `ModifyName` from   (  select ParentId from (select * from  datamap_child order by CreateTime desc) t  GROUP BY ParentId ) dmc join datamap dt on dt.Id=dmc.ParentId left join `cloud_user` u on u.`id` =dt.UserId join `layered` layer on dt.`LayeredType` = layer.`Id` join `modified` m on dt.`ModifierType` = m.`Id` left join `attribution` attr on dt. `Attribution` = `attr`.`Id` left join `cloud_org` org on u.`description` = `org`.`Id`  ";
            RefAsync<int> total = 0;
            var result = await _dataTableService.GetEntityPageList<ReturnSplitTableInfoDto>(sql, dto.PageIndex, dto.PageSize, total);

            //await Logger.AddPlatformLog("分页获取拆分数据", LogType.DataAccess);

            return new PageResult<List<ReturnSplitTableInfoDto>>(result, total);
        }

        /// <summary>
        /// 获取分表信息
        /// </summary>
        /// <param name="dataid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<ReturnChildInfoDto>>> GetChildInfo(int dataid)
        {


            var result =await  await Task.Factory.StartNew(async () =>
            {

                List<ReturnChildInfoDto> list = new List<ReturnChildInfoDto>();
                var result = await _dmcService.GetEntitys(i => i.ParentId == dataid);
                if (result == null || result.Count <= 0) return null;
                result.ForEach(async i =>
                {
                    
                    var entity = new ReturnChildInfoDto { Id = i.Id, Cn_Conditions = i.CN_Conditions, Name = i.Name };
                    if (!string.IsNullOrEmpty(i.MappingTable))
                    {
                        string filedsql = $" select CnFieldName,EnFieldName from data_sharing_affiliated.dic_{i.MappingTable} where 1=1  ";
                        var filedlist = await _dataTableService.GetEntityList<ReturnChildFieldsDto>(filedsql);
                        entity.Fields = filedlist;
                    }
                        list.Add(entity);
                    

                });
                return list;

            });


            if (result == null) return new AjaxResult<List<ReturnChildInfoDto>>(false, "数据错误");

            //await Logger.AddPlatformLog("获取分表信息", LogType.DataAccess);

            return new AjaxResult<List<ReturnChildInfoDto>>(result);

        }

        /// <summary>
        /// 获取表名称
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<ReturnSourceInfoDto>>> GetSourceTableName()
        {
            // var currentUser = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var sql = " select dm.id, dm.EnName EN_SourceName,CONCAT(dm.name,CONCAT('(',co.Title,')')) CN_SourceName  from datamap dm  left join cloud_user cu on cu.id=dm.UserId left join cloud_org co on co.id=cu.description where 1=1 and Status=1 and RackStatus=0 and ismapping is null ";
            var list = await _dataTableService.GetEntityList<ReturnSourceInfoDto>(sql);

            //await Logger.AddPlatformLog("获取表名称", LogType.DataAccess);
            
            return new AjaxResult<List<ReturnSourceInfoDto>>(list);
        }


        /// <summary>
        /// 获取表字段
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<List<ReturnFieldInfoDto>>> GetFieldInfo(GetFieldInfoDto dto)
        {
            string sql = "";
            var associative = await _dataMapService.GetEntity(i=>i.Id==dto.Id);
            if (associative == null) return new AjaxResult<List<ReturnFieldInfoDto>>(false, "数据错误");
            string cnname = "";
            if (dto.Fields!=null&&dto.Fields.Count > 0)
            {
                dto.Fields.ForEach(i => { cnname += "'"+i + "',";  });
                cnname=cnname.TrimEnd(',');
            }
            sql = $" select * from information_schema.tables where table_name ='{associative.AssociativeTable}'";
            var existstable=await _dataTableService.QueryCount(sql);
            if (existstable == null) {
                return new AjaxResult<List<ReturnFieldInfoDto>>(false, "数据表不存在");
            }
            sql = $" select id,CnFieldName CN_SourceName,EnFieldName EN_SourceName ,datatype FieldType from {(associative.IsMapping==null? "data_sharing_main" : "data_sharing_affiliated")}.dic_{associative.AssociativeTable} where 1=1 {(dto.Fields!=null&&dto.Fields.Count>0? $" and EnFieldName not in ( {cnname} ) " : "")}   ";
            var result= await _dataTableService.GetEntityList<ReturnFieldInfoDto>(sql);

            //await Logger.AddPlatformLog("获取表字段", LogType.DataAccess);
            
            return new AjaxResult<List<ReturnFieldInfoDto>>(result);
        }

        /// <summary>
        /// 获取条件信息
        /// </summary>
        /// <param name="type">类型1：基础条件；2：且或；3：括号；</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<ReturnConditionInfoDto>>> GetConditionsInfo(int type)
        {
            var result = await _condService.GetEntitys<ReturnConditionInfoDto>(i => i.Type == type, i => new ReturnConditionInfoDto { ConditionName=i.ConditionsName, Id=i.Id, InputCount=i.InputCount, Describe=i.Describe, ConditionsValue=i.ConditionsValue, Replace=i.Replace });

            string text = "";
            if (type == 1)
            {
                text = "基础条件";
            }
            else if (type == 2)
            {
                text = "且或";
            }
            else
            {
                text = "括号";
            }

            //await Logger.AddPlatformLog($"获取{text}信息", LogType.DataAccess);

            return new AjaxResult<List<ReturnConditionInfoDto>>(result);
        }

        /// <summary>
        /// 检索
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public  async Task<AjaxResult<string>> SearchingTotal(SearchingTotalDto dto)
        {
            string sql = $" select AssociativeTable from datamap where id={dto.Id} ";
            var associative = await _dataTableService.QueryCount(sql);
            sql = $" select * from information_schema.tables where table_name ='{associative}'";
            var existstable = await _dataTableService.QueryCount(sql);
            if (existstable == null) return new AjaxResult<string>(false, "数据表不存在");
            sql = $" select count(id) from data_sharing_main.{associative} where 1=1 {(string.IsNullOrEmpty(dto.Conditions)?"":$" and {dto.Conditions} ")}  ";
            var result = await _dataTableService.QueryCount(sql);

            //await Logger.AddPlatformLog("检索", LogType.DataAccess);
            
            return new AjaxResult<string>(result.ToString());
        }


        /// <summary>
        /// 获取拆分表字段
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<ReturnFieldInfoDto>>> GetSplitTableFiles(int id)
        {
            var dmcentity = await _dmcService.GetEntity(i => i.Id == id);
            if (dmcentity == null) return new AjaxResult<List<ReturnFieldInfoDto>>(false, "数据错误");
            string sql = $" select CnFieldName CN_SourceName,EnFieldName EN_SourceName  from data_sharing_affiliated.dic_{dmcentity.MappingTable} where 1=1 ";
            var result = await _dataTableService.GetEntityList<ReturnFieldInfoDto>(sql);

            //await Logger.AddPlatformLog("获取拆分表字段", LogType.DataAccess);
            
            return new AjaxResult<List<ReturnFieldInfoDto>>(result);
        }

        /// <summary>
        /// 数据映射
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<string>> DataMapping(DataMappingInfoDto dto)
        {
            var sourceentity = await _dataMapService.GetEntity(i => i.Id == dto.DataId&&i.IsMapping==null);
            if (sourceentity == null) return new AjaxResult<string>(false, "数据错误");
            if (dto.Field.GroupBy(i => i.EN_SourceName).Where(i => i.Count() > 1).Count() > 0) return new AjaxResult<string>(false, "字段重复");
            var existsmap = await _dataMapService.GetEntity(i => i.MapChild == dto.DataId&&i.IsMapping.Value);
            //判断是否已经映射
            if (existsmap != null)
            {
                string delsql = $" delete from  datamap where id={existsmap.Id}; use data_sharing_affiliated; drop table if exists {existsmap.AssociativeTable};drop table if exists dic_{existsmap.AssociativeTable}; ";
                await _dataTableService.NoQueryExcuteSql(delsql);
            }

            //3.建表
            //生成表名称  表名称格式:表名_Guid
            string dataMapGuid = Guid.NewGuid().ToString("N");
            string createTableName = dataMapGuid;
            string cnname = "";
            string enname = "";
            string updatesql = "";
            if (dto.Field != null && dto.Field.Count > 0)
            {
                dto.Field.ForEach(i => { cnname += "'"+i.EN_SourceName + "',"; });
                dto.Field.ForEach(i => {
                    enname += i.EN_SourceName + ",";
                    if (i.IsUpdate)
                    {
                        updatesql += $" update dic_{createTableName} set CnFieldName='{i.CN_SourceName}' where EnFieldName='{i.EN_SourceName}' ; ";
                    }
                });
                cnname = cnname.TrimEnd(',');
                enname = enname.TrimEnd(',');
               
            }
            string createsql = $" use data_sharing_affiliated;create table `{createTableName}` select {enname} from {(sourceentity.IsMapping == null ? "data_sharing_main" : "data_sharing_affiliated")}.`{sourceentity.AssociativeTable}` where 1=1 ;"; 
            await _dataTableService.NoQueryExcuteSql(createsql);
            createsql = $" use data_sharing_affiliated; create table `dic_{createTableName}` select * from {(sourceentity.IsMapping==null? "data_sharing_main" : "data_sharing_affiliated")}.dic_{sourceentity.AssociativeTable} where 1=1 and  EnFieldName in ({cnname}) ;   {updatesql} ";
            await _dataTableService.NoQueryExcuteSql(createsql);
            sourceentity.Name = dto.TableName;
            sourceentity.UserId = dto.UserId;
            sourceentity.Describe = dto.Describe;
            sourceentity.AssociativeTable = createTableName;
            sourceentity.IsMapping = true;
            sourceentity.MapChild = sourceentity.Id;
            sourceentity.GUID = createTableName;
            sourceentity.IsDataService = null;
            sourceentity.PatternName = "data_sharing_affiliated";
            var result=await _dataMapService.IdAdd(sourceentity);

            //await Logger.AddPlatformLog("数据映射", LogType.DataOperation);
            
            return new AjaxResult<string>(result>0);
        }

        /// <summary>
        /// 分页获取映射列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<returnMappingList>>> MappingListPage(GetMappingPage dto)
        {
            string sql = "select t2.id dataid,t1.`Name` mappingname ,t2.`Name` sourcename,t1.`Describe` from datamap t1 left join datamap t2 on t1.MapChild=t2.id  left join cloud_user cu on cu.id=t1.UserId where t1.IsMapping ";

            if (dto.DepartmentId.HasValue)
            {
                sql += $" and  cu.description={dto.DepartmentId.Value} ";
            }
            RefAsync<int> total = 0;
            var result = await _dataTableService.GetEntityPageList<returnMappingList>(sql, dto.PageIndex, dto.PageSize, total);

            return new PageResult<List<returnMappingList>>(result,total);
        }

        /// <summary>
        /// 根据数据Id获取映射信息
        /// </summary>
        /// <param name="dataid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<ReturnMapInfoDto>>> GetMappingInfo(int dataid)
        {

            var mapentity = await _dataMapService.GetEntity(i => i.MapChild == dataid);
            if (mapentity == null) return new AjaxResult<List<ReturnMapInfoDto>>(false, "数据错误");
            string sql = $" select * from information_schema.tables where table_name ='{mapentity.AssociativeTable}'";
            var existstable = await _dataTableService.QueryCount(sql);
            if (existstable == null) return new AjaxResult<List<ReturnMapInfoDto>>(false, "数据表不存在");
             sql = $" select  CnFieldName CN_SourceName ,EnFieldName EN_SourceName from data_sharing_affiliated.dic_{mapentity.AssociativeTable} ";
            var result = await _dataTableService.GetEntityList<ReturnMapInfoDto>(sql);
            return new AjaxResult<List<ReturnMapInfoDto>>(result);
        }

        /// <summary>
        /// 修改映射
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<string>> UpdateMapping(UpdateMappingDto dto)
        {
            var sourceentity = await _dataMapService.GetEntity(i => i.Id == dto.DataId && i.IsMapping == null);
            if (sourceentity == null) return new AjaxResult<string>(false, "数据错误");
            if (dto.Field.GroupBy(i => i.EN_SourceName).Where(i => i.Count() > 1).Count() > 0) return new AjaxResult<string>(false, "字段重复");
            var existsmap = await _dataMapService.GetEntity(i => i.MapChild == dto.DataId && i.IsMapping.Value);
            var up = await _dataMapService.GetEntity(i => i.MapChild == dto.DataId);
            foreach (var item in dto.Field)
            {
                string sql = $" use data_sharing_affiliated; update dic_{up.AssociativeTable} set CnFieldName='{item.CN_SourceName}' where EnFieldName='{item.EN_SourceName}' ";
                await _dataTableService.NoQueryExcuteSql(sql);
            }


            return new AjaxResult<string>() ;
        }
    }
}
