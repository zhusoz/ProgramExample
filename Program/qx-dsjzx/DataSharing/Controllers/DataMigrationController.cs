﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProblemDisposal.Common.Basic;
using System.Threading.Tasks;
using SqlSugar;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using ProgramsNetCore.Models.Dto.DataMigration;
using ProgramsNetCore.Common.EncryptionToDecrypt;
using ProgramsNetCore.IService;
using AutoMapper;
using Models;
using System.Linq;
using ProgramsNetCore.Common.Basic;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using System.Data;
using DbType = SqlSugar.DbType;
using ProgramsNetCore.Common;
using System.IO;
using Microsoft.AspNetCore.Rewrite;
using ProgramsNetCore.Models.Dto.DataSecurity;
using ResultDeptInfoDto = ProgramsNetCore.Models.Dto.DataMigration.ResultDeptInfoDto;
using NPOI.HPSF;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Text;
using System.Security.Claims;
using NPOI.SS.Formula.Functions;
using ProgramsNetCore.Models.Dto.DataMigrationDto;
using Org.BouncyCastle.Crypto.Agreement;
using Quartz.Impl;
using Quartz;
using ProgramsNetCore.Common.Quartz;
using ReturnTransDataChildInfoDto = ProgramsNetCore.Models.Dto.DataMigrationDto.ReturnTransDataChildInfoDto;

namespace DataSharing.Controllers
{

    /// <summary>
    /// 数据迁移
    /// </summary>
    [Route("")]
    [ApiController]
    public class DataMigrationController : ControllerBase
    {
        private readonly IPublicTaskService _ptService;
        private readonly IDataMigrationService _dmService;
        private readonly IMapper _map;
        private readonly IDataMigrationChildValueService _dmcvService;
        private readonly IProcessService _processService;
        private readonly IDataTableService _dtService;
        private readonly IDataExprotTaskInfoService _detiService;
        private readonly IAttachedMenuService _amService;
        private readonly IDataMapService _datamapService;
        private readonly IDataTrendService _datatrendService;
        private readonly ISchedulerFactory _schedulerFactory;

        public DataMigrationController(IPublicTaskService ptService,IDataMigrationService dmService,IMapper map,IDataMigrationChildValueService dmcvService,IProcessService processService,IDataTableService dtService,IDataExprotTaskInfoService detiService,IAttachedMenuService amService,IDataMapService datamapService,IDataTrendService datatrendService, ISchedulerFactory schedulerFactory)
        {
            _ptService = ptService;
            _dmService = dmService;
            _map = map;
            _dmcvService = dmcvService;
            _processService = processService;
            _dtService = dtService;
            _detiService = detiService;
            _amService = amService;
            _datamapService = datamapService;
            _datatrendService = datatrendService;
            _schedulerFactory = schedulerFactory;
        }

        /// <summary>
        /// 测试链接
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> TestConnection(TestConnInfoDto dto)
        {
            var result = await Task.Factory.StartNew(() => {
                try
                {
                    var DecryptKey = "qx" + dto.Key;
                    //dto.DBHost=DEncrypt.AesDecrypt(dto.DBHost,DecryptKey);
                    //dto.UserName = DEncrypt.AesDecrypt(dto.UserName, DecryptKey);
                    Console.WriteLine(DateTime.Now.ToString("hh:mm:ss"));
                    dto.Passwrod = DEncrypt.AesDecryptECB(dto.Passwrod, DecryptKey);
                    Console.WriteLine(DateTime.Now.ToString("hh:mm:ss"));
                    //dto.DataBase = DEncrypt.AesDecrypt(dto.DataBase, DecryptKey);
                    DbType myDbType = new DbType();
                    string strConn = "";
                    switch (dto.DBType)
                    {
                        case 0:myDbType = DbType.MySql; strConn = $" server={dto.DBHost};Database={dto.DataBase};Uid={dto.UserName};Pwd={dto.Passwrod};Port={dto.Port};AllowLoadLocalInfile=true"; break;
                        case 1:myDbType = DbType.SqlServer; strConn = $"server={(dto.DBHost.Contains(',')?dto.DBHost:dto.DBHost+","+dto.Port)};uid={dto.UserName};pwd={dto.Passwrod};database={dto.DataBase}"; break;
                        case 7:myDbType=DbType.Oracle; strConn = $"Data Source={dto.DBHost}/orcl;User ID={dto.UserName};Password={dto.DataBase}"; break;
                        case 5:myDbType = DbType.Dm; strConn = $"Server={dto.DBHost}; User Id={dto.UserName}; PWD={dto.Passwrod};DATABASE={dto.DataBase}"; break;
                        case 6:myDbType = DbType.Kdbndp; strConn = $"Server={dto.DBHost};Port={dto.Port};UID={dto.UserName};PWD={dto.Passwrod};database={dto.DataBase}"; break;
                    }
                    SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
                    {
                        ConnectionString = strConn,//连接符字串
                        DbType = myDbType, //数据库类型
                        IsAutoCloseConnection = true ,//不设成true要手动close
                    
                    });
                   
                db.Open();
                    Logger.AddDBLog("登录数据库", LogLevel.Information, db.CurrentConnectionConfig.ConnectionString).GetAwaiter().GetResult();
                    return new AjaxResult<bool>(true, "连接成功！");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new AjaxResult<bool>(false, "连接失败！");
                }
            });

            return result;
        }

        /// <summary>
        /// 提交互导申请
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<string>> SubmitApplication(DataMigrationInfoDto dto)
        {
            //根据中文匹配数据库中相应用户/部门Id

         var userinfo= HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userinfo)) return new AjaxResult<string>(false, "认证失败，请重新登录");

            string usersql = $" select cu.id,real_name,co.id departmentid ,co.Title from cloud_user cu join cloud_org co on co.id=cu.description where cu.id={userinfo} ";
            var info = await _dtService.GetEntity<GetContextUserInfoDto>(usersql);
            //if (userid == null || dept == null) return new AjaxResult<string>(false, "申请人或者部门不存在！");
            
            ////映射互导申请信息并插入
            var insertDm = _map.Map<datamigrationtaskinfo>(dto);
            insertDm.Applicant = info.Id;
            insertDm.Department = info.DepartmentId.ToInt32();
         var resultId= await _dmService.IdAdd(insertDm);
            //判断导出结构为表还是库 为表记录表
            if ((dto.ExportType == 2 || dto.ExportType == 4) && dto.ExportObj != null && dto.ExportObj.Count > 0)
            {
               await _dmcvService.Adds(dto.ExportObj.Select(i => new datamigrationtaskinfochildvalue { Parent_ID=resultId, ExprotValue=i.ToString() }).ToList());
            }
          
            var resultptId = await _ptService.IdAdd(new public_task { StartTime = DateTime.Now, Status = 0, TaskType =2 });

            //添加导出申请
            var reusltprocessid=  await _processService.IdAdd(new process { TaskId=resultptId, UserId=  info.Id, Accessory=dto.Accessory,Status=1,Describe="等待审批" });
            ////添加申请审批
            //var reusltprocessid = await _processService.IdAdd(new process { TaskId = resultptId, Status = 0, Describe = "审批" });
            await _ptService.UpdateColumn(new public_task { Id = resultptId, CurrentProcess = reusltprocessid, InfoRelation=resultId,Status=1 }, i => new {  i.CurrentProcess,i.InfoRelation,i.Status });
            //await Logger.AddPlatformLog("提交互导申请", LogType.DataApply);
            return new AjaxResult<string>();
        }

        /// <summary>
        /// 获取数据迁移列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<ResultDataMigrationListDto>>> GetPageDataMigrationList(DataMigrationListDto dto)
        {
            string sql = $"";

            if (dto.OperationType == 1)
            {
                sql += $" select pt.id ,dtm.Name TableNameCN,dtm.EnName TableName,dtm.GUID,cu.real_name Applicant ,co.Title Department ,{(dto.Status==2? "pt.EndTime" : "pt.StartTime")}  createtime,modi.`Name` Modifier,la.`Name` Layered,p.`Describe` Process  from public_task pt  join dataexprottaskinfo  dpinfo on dpinfo.id=pt.inforelation join datamap  dtm on dtm.id=dpinfo.AssociativeTable join modified modi on modi.id=dtm.ModifierType  join layered la on la.id=dtm.LayeredType  join process p on p.id=pt.CurrentProcess left join cloud_user cu on cu.id=dpinfo.UserId left join cloud_org co on co.id=dpinfo.Department  where 1=1 ";
            }
            else
            {
                sql += $" select pt.id ,dpinfo.TargetName_cn TableNameCn,'' GUID,cu.real_name Applicant ,co.Title Department ,pt.StartTime createtime,modi.`Name` Modifier,'' Layered,p.`Describe` Process,dpinfo.isimplement  from public_task pt join datamigrationtaskinfo  dpinfo on dpinfo.id=pt.inforelation   join process p on p.id=pt.CurrentProcess left join cloud_user cu on cu.id=dpinfo.Applicant left join cloud_org co on co.id=dpinfo.Department left join modified modi on modi.id=dpinfo.ModifierType where 1=1";
            }
            string sqlwhere = "";
            if(dto.OperationType.HasValue)sqlwhere += $" and TaskType={dto.OperationType}  ";
            if(dto.Status.HasValue)sqlwhere += $" and pt.Status={dto.Status} ";
            sql += sqlwhere;
            RefAsync<int> total = 0;
            var result = await _dtService.GetEntityPageList<ResultDataMigrationListDto>(sql,dto.PageIndex,dto.PageSize,total);
            //await Logger.AddPlatformLog("获取数据迁移列表", LogType.DataAccess);
            return new PageResult<List<ResultDataMigrationListDto>>(result, total);

        }

        /// <summary>
        /// 获取互导申请列表
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]

        public async Task<PageResult<List<ResultTransconductanceDto>>> GetPageTransconductanceList(GetDataMigrationDto dto)
        {

            string sql = "";
            if (dto.Status == 0) {

                sql = "select pt.id ,dpinfo.TargetName_cn TableNameCn,cu.real_name Applicant ,co.Title Department ,pt.StartTime createtime,p.`Describe` Process,dpinfo.isimplement  from public_task pt join datamigrationtaskinfo  dpinfo on dpinfo.id=pt.inforelation   join process p on p.id=pt.CurrentProcess left join cloud_user cu on cu.id=dpinfo.Applicant left join cloud_org co on co.id=dpinfo.Department  where 1=1 and pt.tasktype=2 ";


            }
            else
            {
                sql = $"  select pt.id ,dpinfo.TargetName_cn TableNameCn,cu.real_name Applicant ,co.Title Department ,pt.{(dto.Status == 1 ? "StartTime" : "EndTime")} createtime,{(dto.Status == 1 ? "p.`Describe`" : "t.dict_info")} Process,dpinfo.isimplement  from public_task pt join datamigrationtaskinfo  dpinfo on dpinfo.id=pt.inforelation   join process p on p.id=pt.CurrentProcess left join cloud_user cu on cu.id=dpinfo.Applicant left join cloud_org co on co.id=dpinfo.Department  {(dto.Status == 1 ? "" : " join ( select * from  public_dict  where dict_key='dmti_execute' ) t on t.dict_value=dpinfo.IsImplement")}  where 1=1 and pt.status={dto.Status} and pt.tasktype=2 {(dto.Status == 2 ? $" and pt.status={dto.Status} " : "")}  ";
            }
            
            RefAsync<int> total = 0;
            var result = await _dtService.GetEntityPageList<ResultTransconductanceDto>(sql, dto.PageIndex, dto.PageSize, total);
            //await Logger.AddPlatformLog("获取互导申请列表", LogType.DataAccess);
            return new PageResult<List<ResultTransconductanceDto>>(result, total);

        }
        /// <summary>
        /// 数据审批
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<string>> DataMigrationApproval(ApprovalDto dto)
        {
            if (dto.DataId == null || dto.DataId.Count <= 0) return new AjaxResult<string>(false, "数据错误！");
            var result = await await Task.Factory.StartNew(async () =>
            {
                bool IsExists=false;
                foreach (var item in dto.DataId)
                {
                    var resultData = await _ptService.GetEntity(i => i.Id == item);
                    if (resultData == null)
                    {
                        IsExists = false;
                        break;
                    }
                    else IsExists = true;
                    //var endProcess = await _processService.GetEntity(i => i.Id == resultData.CurrentProcess.Value);
                    var reusltprocessid = await _processService.IdAdd(new process { TaskId = resultData.Id, Status = 2, Describe =dto.ApprovalType==2? "已审批":"已驳回" , UserId=dto.UserID});
                    //await _processService.UpdateColumn(new process { Id = endProcess.Id, Status = dto.State, UserId = dto.UserID, Accessory=dto.Accessory, CreateTime=DateTime.Now }, i => new { i.Status, i.UserId });
                    await _ptService.UpdateColumn(new public_task { Id = item, EndTime = DateTime.Now, Status = dto.State,CurrentProcess=reusltprocessid }, i =>new  { i.EndTime,i.Status,i.CurrentProcess });
                }
                return IsExists;
            });
            //await Logger.AddPlatformLog("迁移|互导数据审批", LogType.DataApproval);
            return new AjaxResult<string>(result);
        }


        /// <summary>
        /// 加入申请车
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<string>> JoinApplyCar(JoinApplyCarDto dto)
        {
            if (dto.DataId == null || dto.DataId.Count <= 0) return new AjaxResult<string>(false, "数据出现错误！");
            var result = await await Task.Factory.StartNew(async () => {
                bool isExists = false;
                foreach (var item in dto.DataId)
                {
                        var resultid = await _detiService.IdAdd(new dataexprottaskinfo { UserId = dto.UserId, Department = dto.Department, AssociativeTable = item });
                        var resulttaskid = await _ptService.IdAdd(new public_task { StartTime = DateTime.Now, InfoRelation = resultid, Status = 0, TaskType = 1 });
                        //添加导出申请
                        var resultprocessid = await _processService.IdAdd(new process { TaskId = resulttaskid, UserId = dto.UserId, Status = 1, Describe = "添加申请车", CreateTime = DateTime.Now });
                        isExists = (await _ptService.UpdateColumn(new public_task { Id = resulttaskid, CurrentProcess = resultprocessid }, i => new { i.CurrentProcess })) > 0;                 
                }
                return isExists;
            });
            return new AjaxResult<string>(result);
        }

        /// <summary>
        /// 提交购物车申请
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<string>> AppliyCarCompleted(SubmitApplyDto dto)
        {
            if (dto.TaskId == null || dto.TaskId.Count <= 0) return new AjaxResult<string>(false, "数据出现错误！");
            var resultdetermine = await await Task.Factory.StartNew(async () =>
            {
                var entity = await _ptService.GetEntitys(i => i.TaskType == 1 && i.Status == 0);
                return entity.Select(i => i.Id).Intersect(dto.TaskId).Count() == dto.TaskId.Count;
            });
            if (!resultdetermine) return new AjaxResult<string>(false, "数据错误");
            var result = await Task.Factory.StartNew(async () => {

                foreach (var item in dto.TaskId)
                {
                    var entitypt = await _ptService.GetEntity(i => i.Id == item);
                    var entitydeti = await _detiService.GetEntity(i => i.Id == entitypt.InfoRelation);
                    var reusltprocessid = await _processService.IdAdd(new process { Status=1, CreateTime=DateTime.Now, TaskId=entitypt.Id, UserId=entitydeti.UserId, Describe= "导出申请" });
                    //添加申请审批
                  //  var reusltprocessid = await _processService.IdAdd(new process { TaskId = entitypt.Id, Status = 0, Describe = "审批" });
                    await _ptService.UpdateColumn(new public_task { Id = entitypt.Id, CurrentProcess = reusltprocessid,Status=1 }, i => new { i.CurrentProcess,i.Status });
                }
            });
            //await Logger.AddPlatformLog("提交购物车申请", LogType.DataApply);
            return new AjaxResult<string>();
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="taskId">任务Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ExportExcelAsync(int taskId)
        {
            var userid = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userid)) return Ok("认证失败，请登录");
            var resultdetermine = await await Task.Factory.StartNew(async () =>
            {
                var entitypt = await _ptService.GetEntitys(i => i.TaskType == 1 && i.Status == 2&&taskId==i.Id);
                return entitypt.Count>0;
            });
            if (!resultdetermine) return Ok("流程错误");

            var result = await await Task.Factory.StartNew(async () =>
            {
                var path = AppContext.BaseDirectory;
                string getdatainfo = $"select dt.id,dt.GUID,dt.AssociativeTable,dt.Name TableName,st.id SecretId,st.SecretStatus,st.SecretEncode,st.SecretType,st.StartTime,st.EndTime,st.SecretMode,dt.isdataservice from  public_task pt join dataexprottaskinfo deti on deti.id=pt.InfoRelation join datamap  dt on dt.id=deti.AssociativeTable left join secret st on st.MapId=dt.Id where pt.id in ({taskId})";
                var resultEntitys = await _dtService.GetEntity<ResultSecretDataInfoDto>(getdatainfo);
                string key = "";
                var dt = await _dtService.GetDataTable($"select * from {(resultEntitys.IsDataService.HasValue? "data_sharing_main" : "data_sharing_affiliated")}.{resultEntitys.AssociativeTable} where 1=1");
                await _datatrendService.Add(new datatrend { CreateTime=DateTime.Now, DataMapId=resultEntitys.Id, AffectedRows=dt.Rows.Count,  Type=1, UserId=userid.ToInt32() });
                key = resultEntitys.SecretMode == 1 ? resultEntitys.SecretEncode : "qx123456";
                if (resultEntitys.StartTime != null&&resultEntitys.EndTime > DateTime.Now )
                {
                    //字段加密
                    if (resultEntitys.SecretType == 1)
                    {
                        var dtkey = await _dtService.GetDataTable($" select Secretkey from secret_child where parent_id={resultEntitys.SecretId} ");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            for (int j = 0; j < dtkey.Rows.Count; j++)
                            {
                                dt.Rows[i][dtkey.Rows[j][0] as string] = DEncrypt.Encrypt(dt.Rows[i][dtkey.Rows[j][0] as string] as string, key);
                            }
                        }
                    }
                    //表加密
                    else if (resultEntitys.SecretType == 2)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            if (dt.Columns[i].DataType.Name == "String")
                            {
                                for (int j = 0; j < dt.Rows.Count; j++)
                                {
                                    dt.Rows[j][i] = DEncrypt.Encrypt(dt.Rows[j][i] as string, key);
                                }
                            }
                        }
                    }
                    //未加密
                    else if (resultEntitys.SecretType == null)
                    {

                    }
                }

                var filepath = path + @"ExprotExcel\";
                if (!System.IO.Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                filepath += resultEntitys.AssociativeTable + ".xls";
                if (System.IO.File.Exists(filepath))
                    System.IO.File.Delete(filepath);
                var exprotexcel = await PublicFunction.DataTableToExcelAsync(dt, resultEntitys.TableName);

                return File(exprotexcel.ToArray(), "application/octet-stream",resultEntitys.TableName+".xls");

            });
            await Logger.AddDBLog("导出Excel", LogLevel.Information);
            return result;
            //FileStream fs = new FileStream(filepath, FileMode.Create, FileAccess.Write);
            //fs.Write(buf, 0, buf.Length);
            //fs.Flush();
            //fs.Close();


        }

        /// <summary>
        /// 获取数源部门信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<ResultDbDeptInfoDto>>> GetDBDept()
        {
            string sql = "select DISTINCT co.id, co.Attribution departmentname from datamap  dt join attribution co on co.Id = dt.Attribution where dt.IsPrivate=0";
            var result = await _dtService.GetEntityList<ResultDbDeptInfoDto>(sql);
            //await Logger.AddPlatformLog("数据迁移-获取数源部门信息", LogType.DataAccess);
            return new AjaxResult<List<ResultDbDeptInfoDto>>(result);
        }
        /// <summary>
        /// 根据部门id获取数据表
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<ResultDeptInfoDto>>> GetDeptTableName(int deptId)
        {
            var sql = $"select dt.id,dt.name TableName  from datamap dt where 1=1 and attribution={deptId} and IsPrivate=0 ";
            var result = await _dtService.GetEntityList<ResultDeptInfoDto>(sql);
            //await Logger.AddPlatformLog("数据迁移-根据部门id获取数据表", LogType.DataAccess);
            return new AjaxResult<List<ResultDeptInfoDto>>(result);
        }
        /// <summary>
        /// 删除购物车项
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<string>> DeleteShoppingCart(DeleteShoppingCartDto dto)
        {
            if (dto.TaskId == null || dto.TaskId.Count < 0) return new AjaxResult<string>(false, "数据错误");
            var result =await _ptService.Deletes(dto.TaskId.ToArray());
            return new AjaxResult<string>(result);
        }


        /// <summary>
        /// 分页获取数据表数据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<ResultDataInfoPageDto>> GetDataInfoPage(GetDataInfoPageDto dto)
        {
            if (dto.UserId != -1)
            {
                var authorizesql = $"select Id from authority where ( RelevanceId=( select `description` from cloud_user where id={dto.UserId}) or RelevanceId={dto.UserId}) and AssociativeTable={dto.DataId} ";
                var datamapentity = new datamap();
                if (dto.IsChild)
                {
                    authorizesql += $" and ischild=1  union select id from authority where ( RelevanceId=( select `description` from cloud_user where id={dto.UserId}) or RelevanceId={dto.UserId}) and AssociativeTable=( select dm.ID from datamap_child dmc join datamap dm on dm.id=dmc.ParentId where dmc.id={dto.DataId} )";
                    string sql = $" select parentId from datamap_child where id={dto.DataId} ";
                    var parentid = await _dtService.QueryCount(sql);
                    datamapentity = await _datamapService.GetEntity(i => i.Id == parentid.ToInt32() && dto.UserId == i.UserId);
                }
                else
                    datamapentity = await _datamapService.GetEntity(i => i.Id == dto.DataId && dto.UserId == i.UserId);

                var authorizeList = await _dtService.GetEntityList<ResultIdCountDto>(authorizesql);



                if (datamapentity == null && (authorizeList == null || authorizeList.Count <= 0)) return new AjaxResult<ResultDataInfoPageDto>(false, "无权限，请先申请权限！");
            }
            string getdatainfo = "";
            if (dto.IsChild) {
                 getdatainfo = $"select dt.id,dt.GUID,dmc.MappingTable AssociativeTable,dt.Name TableName,st.id SecretId,st.SecretStatus,st.SecretEncode,st.SecretType,st.StartTime,st.EndTime,st.SecretMode,dt.isdataservice,dt.ismapping from  datamap_child dmc join   datamap  dt on dt.id=dmc.parentid  left join secret st on st.MapId=dt.Id where dmc.Id = {dto.DataId}";
            } else
            {
                 getdatainfo = $"select dt.id,dt.GUID,dt.AssociativeTable,dt.Name TableName,st.id SecretId,st.SecretStatus,st.SecretEncode,st.SecretType,st.StartTime,st.EndTime,st.SecretMode,dt.isdataservice,dt.ismapping from   datamap  dt  left join secret st on st.MapId=dt.Id where dt.Id = {dto.DataId}";
            }
            var resultEntitys = await _dtService.GetEntity<ResultSecretDataInfoDto>(getdatainfo);
            if (resultEntitys == null || resultEntitys.AssociativeTable.Equals(string.Empty)) return new AjaxResult<ResultDataInfoPageDto>(false,"数据错误！");
            string key = "";
            RefAsync<int> total = 0;
            var dt = await _dtService.GetDataTable($"select * from {((resultEntitys.IsMapping.HasValue&&resultEntitys.IsMapping.Value) || dto.IsChild ? "data_sharing_affiliated" : "data_sharing_main")}.{resultEntitys.AssociativeTable} where 1=1 {(string.IsNullOrEmpty(dto.Conditions) ? "" : $" and {dto.Conditions} ")}",dto.PageIndex,dto.PageSize,total);
            key = resultEntitys.SecretMode == 1 ? resultEntitys.SecretEncode : "qx123456";
            var result = await await Task.Factory.StartNew(async () => {

                if (resultEntitys.StartTime == null || resultEntitys.EndTime < DateTime.Now )
                {
                    //字段加密
                    if (resultEntitys.SecretType == 1)
                    {
                        var dtkey = await _dtService.GetDataTable($" select Secretkey from secret_child where parent_id={resultEntitys.SecretId} ");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            for (int j = 0; j < dtkey.Rows.Count; j++)
                            {
                                if (dt.Columns[dtkey.Rows[j][0].ToString()].DataType.Name == "String")
                                {
                                    var value = DEncrypt.Encrypt(dt.Rows[i][dtkey.Rows[j][0].ToString()].ToString(),
                                        key);
                                    dt.Rows[i][dtkey.Rows[j][0].ToString()] = value;
                                }
                            }

                        }
                        }
                    //表加密
                    else if (resultEntitys.SecretType == 2)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            if (dt.Columns[i].DataType.Name == "String")
                            {
                                for (int j = 0; j < dt.Rows.Count; j++)
                                {
                                    dt.Rows[j][i] = DEncrypt.Encrypt(dt.Rows[j][i].ToString(), key);
                                }
                            }
                        }
                    }
                    //未加密
                    else if (resultEntitys.SecretType == null)
                    {

                    }
                }
                
                return dt;
            });
            var dicResult = await _dtService.GetDataTable($"select CnFieldName,EnFieldName from {((resultEntitys.IsMapping.HasValue && resultEntitys.IsMapping.Value) || dto.IsChild ? "data_sharing_affiliated" : "data_sharing_main")}.dic_{resultEntitys.AssociativeTable} where 1=1 ");
            //await Logger.AddPlatformLog("数据迁移-分页获取数据表数据", LogType.DataAccess);
            return new AjaxResult<ResultDataInfoPageDto>(new ResultDataInfoPageDto { DicTable = dicResult, Table = result, Total=total });

        }

        /// <summary>
        /// 获取迁移申请信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<ResultTransconductanceApplicationDto>>> GetTransconductanceApplicationInfo(GetMigrationApprovalDto dto)
        {
            if (dto.Id == null || dto.Id.Count <= 0) return new PageResult<List<ResultTransconductanceApplicationDto>>(null, 0);
            string ids = "";
            dto.Id.ForEach(i => ids += i + ",");
            ids = ids.TrimEnd(',');
            RefAsync<int> total = 0;
            string sql = $"select dtm.id DataId ,dtm.Name TableNameCN,dtm.EnName TableName,dtm.GUID,cu.real_name Applicant ,co.Title Department ,modi.`Name` Modifier from  datamap  dtm  join modified modi on modi.id=dtm.ModifierType  left join cloud_user cu on cu.id=dtm.UserId left join cloud_org co on co.id=dtm.Attribution where dtm.id in({ids}) ";
            var result = await _dtService.GetEntityPageList<ResultTransconductanceApplicationDto>(sql, dto.PageIndex, dto.PageSize, total);
            //await Logger.AddPlatformLog("获取迁移申请信息", LogType.DataAccess);
            return new PageResult<List<ResultTransconductanceApplicationDto>>(result, total);
        }


        /// <summary>
        /// 催办
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<string>> GetHasten(AddHastenInfoDto dto)
        {
            var result = await _amService.Adds(dto.TaskId.Select(i => new attachedmenu { EndTime = dto.EndTime, StartTime = dto.StartTime, TaskId = i, Type = 1, UserId = dto.UserId }).ToList());
            //await Logger.AddPlatformLog("数据迁移-催办", LogType.DataOperation);
            return new AjaxResult<string>(result>0);
        }

        /// <summary>
        /// 执行互导任务
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<string>> ExecuteTask(int taskid)
        {

            var taskentity = await _ptService.GetEntity(i => i.Id == taskid);
            if (taskentity == null) return new AjaxResult<string>(false, "数据错误");
            var dpinfo = await _dmService.GetEntity(i => i.Id == taskentity.InfoRelation.Value&&i.IsImplement>0);
            if (dpinfo == null) return new AjaxResult<string>(false, "互导任务正在执行");
            var result = await await Task.Factory.StartNew(async () =>
            {
                try
                {

                    var DecryptKey = "qx" + dpinfo.SecretKey;
                    dpinfo.DbPwd = DEncrypt.AesDecryptECB(dpinfo.DbPwd, DecryptKey);
                    DbType myDbType = new DbType();
                    string strConn = "";
                    switch (dpinfo.DbType)
                    {
                        case 0: myDbType = DbType.MySql; strConn = $" server={dpinfo.HostLink};Database={dpinfo.TargetName};Uid={dpinfo.DbName};Pwd={dpinfo.DbPwd};Port={dpinfo.Port};AllowLoadLocalInfile=true"; break;
                        case 1: myDbType = DbType.SqlServer; strConn = $"server={(dpinfo.HostLink.Contains(',') ? dpinfo.HostLink : dpinfo.HostLink + "," + dpinfo.Port)};uid={dpinfo.DbName};pwd={dpinfo.DbPwd};database={dpinfo.TargetName}"; break;
                        case 7: myDbType = DbType.Oracle; strConn = $"Data Source={dpinfo.HostLink}/orcl;User ID={dpinfo.DbName};Password={dpinfo.DbPwd}"; break;
                        case 5: myDbType = DbType.Dm; strConn = $"Server={dpinfo.HostLink}; User Id={dpinfo.DbName}; PWD={dpinfo.DbPwd};DATABASE={dpinfo.TargetName}"; break;
                        case 6: myDbType = DbType.Kdbndp; strConn = $"Server={dpinfo.HostLink};Port={dpinfo.Port};UID={dpinfo.DbName};PWD={dpinfo.DbPwd};database={dpinfo.TargetName}"; break;
                    }
                    SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
                    {
                        ConnectionString = strConn,//连接符字串
                        DbType = myDbType, //数据库类型
                        IsAutoCloseConnection = true,//不设成true要手动close

                    });

                    db.Open();
                    await Logger.AddDBLog("登录数据库", LogLevel.Information, strConn);



                    List<datamap> dm = new List<datamap>();

                    //获取互导数据表集合

                    if (dpinfo.ExportType == 1 || dpinfo.ExportType == 3)
                    {
                        dm = await _datamapService.GetEntitys(i => 1 == 1);
                    }
                    else
                    {
                        string sql = $" select dm.* from ( select ExprotValue id  from public_task pt  join datamigrationtaskinfo dmti on dmti.id=pt.InfoRelation join datamigrationtaskinfochildvalue dmtic on dmtic.Parent_ID=dmti.id  where pt.id={taskid} )t join datamap dm on dm.id=t.ID ";
                        dm = await _dtService.GetEntityList<datamap>(sql);
                    }


                    //根据集合个数添加任务
                    dm.ForEach(async i =>
                    {

                        //先根据字典表创建目标表
                        string sql = $"  DROP table  if exists `{i.EnName}`;    " +
                            $"CREATE TABLE `{i.EnName}`  ( " +
                            $" `Id` bigint(20) NOT NULL AUTO_INCREMENT, ";
                        var dbstr = i.IsDataService.HasValue && !i.IsMapping.HasValue ? "data_sharing_main" : "data_sharing_affiliated";
                        var nowsql = $" select * from {dbstr}.dic_{i.AssociativeTable} where 1=1 ";
                        var filedlist = await _dtService.GetDataTable(nowsql);
                        string createtablesql = "";
                        string primarysql = "";
                        for (int rowi = 0; rowi < filedlist.Rows.Count; rowi++)
                        {
                            createtablesql += $" `{filedlist.Rows[rowi][2]}` {filedlist.Rows[rowi][3]}({filedlist.Rows[rowi][4]}) NOT NULL COMMENT '{filedlist.Rows[rowi][5]}', ";
                            if (filedlist.Rows[rowi][6].ToString().Equals("1")) primarysql += $",`{filedlist.Rows[rowi][2]}`";
                        }
                        sql += createtablesql + $" PRIMARY KEY (`Id`{primarysql}) USING BTREE ) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;  ";
                        await db.Ado.ExecuteCommandAsync(sql);

                        if (!(dpinfo.ExportType == 3 || dpinfo.ExportType == 4))
                        {


                            var scheduler = await _schedulerFactory.GetScheduler();

                            //添加任务，24点执行，8点结束，每天24小时执行一次
                            IJobDetail job = JobBuilder.Create<SteilheitTaskJob>().WithIdentity($"{i.EnName}_{DateTime.Now.ToString("yyMMddhhmmss")}", "datamap").Build();
                            ITrigger trigger = TriggerBuilder.Create()
                    .WithIdentity(i.EnName + "SteilheitTaskJob")
                    .StartAt(DateBuilder.DateOf(22, 0, 0))
                    .EndAt(DateBuilder.DateOf(6, 0, 0))
                    .StartNow()
                    .WithSimpleSchedule(x =>
                    {
                        x.WithIntervalInHours(24).WithRepeatCount(0);
                    }).Build();


                            //待传输数据sql
                            trigger.JobDataMap.Add("datasql", $"select dt.id,dt.GUID,dt.AssociativeTable,dt.Name TableName,st.id SecretId,st.SecretStatus,st.SecretEncode,st.SecretType,st.StartTime,st.EndTime,st.SecretMode,dt.isdataservice from  datamap  dt left join secret st on st.MapId=dt.Id where dt.id={i.Id}");
                            trigger.JobDataMap.Add("connection", db.CurrentConnectionConfig);
                            trigger.JobDataMap.Add("tablename", i.EnName);
                            trigger.JobDataMap.Add("taskid", taskid);
                            await scheduler.ScheduleJob(job, trigger);

                            await scheduler.Start();
                        }
                    });
                    db.Dispose();
                    db.Close();
                    await _dtService.NoQueryExcuteSql($" update datamigrationtaskinfo set IsImplement=1 where id=(select InfoRelation from public_task where id={taskid}) ");
                    await Logger.AddDBLog("登出数据库", LogLevel.Information, strConn);
                    return new AjaxResult<string>(true, "执行成功");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new AjaxResult<string>(false, "连接失败");
                }
            });
            return result;
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<string>> DeleteTask(DeleteTaskDto dto)
        {
            string sql = "";
            if (dto.TaskIds == null || dto.TaskIds.Count <= 0) return new AjaxResult<string>(false, "缺少任务Id");
           
            try
            {
                dto.TaskIds.ForEach(async i =>
                {
                    if (dto.TaskType == 1)
                    {

                    }
                    else if (dto.TaskType == 2)
                    {
                        sql = $" delete from datamigrationtaskinfochildvalue where Parent_ID=(select InfoRelation from public_task pt where pt.id={i} and pt.tasktype={dto.TaskType});delete from datamigrationtaskinfo where id=(select InfoRelation from public_task pt where pt.id={i} and pt.tasktype={dto.TaskType});delete from process where TaskId={i};delete from public_task where id={i}; ";
                    }
                    else
                    {

                    }
                    if (!string.IsNullOrEmpty(sql))
                        await _dtService.NoQueryExcuteSql(sql);

                });
                //await Logger.AddPlatformLog("数据迁移-删除任务", LogType.DataOperation);
                return new AjaxResult<string>();
            }
            catch (Exception ex)
            {
                return new AjaxResult<string>(false,ex.Message);
            }
        }

        /// <summary>
        /// 获取互导申请数据详情
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        [HttpGet]
        public  async Task<AjaxResult<ReturnTransconductanceDataInfoDto>> GetTransconductanceDataInfo(int taskid) {

            string existssql = $" select id from public_task where id={taskid} " ;
            var existscount = await _dtService.QueryCount(existssql);
            if (existscount.ToInt32() <= 0) return new AjaxResult<ReturnTransconductanceDataInfoDto>(false, "数据错误");

            string sql = $"select pt.Id,dmti.TargetName_cn TableNameCN,dmti.HostLink,cu.real_name UserName,co.Title department,dmti.ExportType,t3.dict_info ExportTypeInfo,dmti.IsImplement,t1.dict_info IsImplementInfo,dmti.DbType,t2.dict_info DbTypeInfo ,pt.StartTime ,pt.EndTime ,pt.`Status` from public_task  pt  join process p on p.id=pt.CurrentProcess  join datamigrationtaskinfo dmti on dmti.id=pt.InfoRelation join cloud_user cu on cu.id=dmti.Applicant join cloud_org co on co.id=dmti.Department join ( select * from public_dict where dict_key='dmti_execute') t1 on t1.dict_value=dmti.IsImplement join ( select * from public_dict where dict_key='sugar_dbtype') t2 on t2.dict_value=dmti.DbType join ( select * from public_dict where dict_key='export_type') t3 on t3.dict_value=dmti.ExportType where 1=1 and pt.tasktype=2 and pt.id={taskid}";
            var result = await _dtService.GetEntity<ReturnTransconductanceDataInfoDto>(sql);
            if (result.ExportType == 1 || result.ExportType == 3)
            {
                sql = "select dt.Name,ab.Attribution from datamap dt join attribution ab on ab.id=dt.attribution  where 1=1 and IsPrivate=0 and (IsDataService or IsMapping) ";
            }
            else
            {
                sql = $" select dm.id,dm.Name,ab.Attribution from public_task pt join datamigrationtaskinfo dmti on dmti.id=pt.InfoRelation join datamigrationtaskinfochildvalue dmtic on dmtic.Parent_ID=dmti.id join datamap dm on dm.id=dmtic.ExprotValue join attribution ab on ab.id=dm.attribution where 1=1 and dm.IsPrivate=0 and  pt.id={taskid} ";
            }
            result.ChildList  = await _dtService.GetEntityList<ReturnTransDataChildInfoDto>(sql);
            //await Logger.AddPlatformLog("数据迁移-获取互导申请数据详情", LogType.DataAccess);
            return new AjaxResult<ReturnTransconductanceDataInfoDto>(result);
        }



    }
}
