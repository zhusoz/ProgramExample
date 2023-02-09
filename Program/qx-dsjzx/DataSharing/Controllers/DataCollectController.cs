﻿using AutoMapper;
using DataSharing.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Models;
using ProblemDisposal.Common.Basic;
using ProgramsNetCore.Common;
using ProgramsNetCore.Common.Basic;
using ProgramsNetCore.IService;
using ProgramsNetCore.Models;
using ProgramsNetCore.Models.Dto;
using ProgramsNetCore.Models.Dto.DataCollectDto;
using ProgramsNetCore.Models.Dto.DataManageDto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar.Extensions;
using MySqlX.XDevAPI.Common;

namespace DataSharing.Controllers
{
    /// <summary>
    /// 数据归集
    /// </summary>
    [Route("")]
    [ApiController]
    public class DataCollectController : ControllerBase
    {
        private readonly IDataTableService _dataTableService;
        private readonly IDataTrendService _dataTrendService;
        private readonly IDataMapService _dataMapService;
        private readonly IApprovalTaskService _approvalTaskService;
        private readonly IProcessService _processService;
        private readonly IUserService _userService;
        private readonly IAttributionService _attributionService;
        private readonly IAuthoriyService _authoriyService;
        private readonly ILayeredService _layeredService;
        private readonly IMapper _mapper;



        public DataCollectController(IDataTableService dataTableService, IDataTrendService dataTrendService, IDataMapService dataMapService, IApprovalTaskService approvalTaskService, IProcessService processService, IUserService userService, IAttributionService attributionService, IAuthoriyService authoriyService, ILayeredService layeredService, IMapper mapper)
        {
            _dataTableService = dataTableService;
            _dataTrendService = dataTrendService;
            _dataMapService = dataMapService;
            _approvalTaskService = approvalTaskService;
            _processService = processService;
            _userService = userService;
            _attributionService = attributionService;
            _authoriyService = authoriyService;
            _layeredService = layeredService;
            _mapper = mapper;
        }

        /// <summary>
        /// 源表目录菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<SourceTableDirectoryMenuDto>> GetSourceTableDirectory()
        {
            //源表目录菜单查询dto
            SourceTableDirectoryMenuDto dto = new SourceTableDirectoryMenuDto();

            //1.数源单位
            string sydw_sql = "select t1.`id`,t1.`attribution` from data_sharing_platform.`attribution` t1;";
            //2.分类类型
            string fclx_sql = "select t1.`id`,t1.`name` from data_sharing_platform.`layered` t1;";
            //3.更新频率
            string gxpl_sql = "select t1.`id`,t1.`name` from data_sharing_platform.`frequency` t1;";

            dto.Attributions = await _dataTableService.GetEntityList<AttributionDto>(sydw_sql);
            dto.Layereds = await _dataTableService.GetEntityList<LayeredDto>(fclx_sql);
            dto.Frequencies = await _dataTableService.GetEntityList<FrequencyDto>(gxpl_sql);

            //await Logger.AddPlatformLog("获取源表目录菜单", LogType.DataAccess);

            return new AjaxResult<SourceTableDirectoryMenuDto>(dto);
        }

        /// <summary>
        /// 修改源表上下架状态[update:2022年8月19日19:42:01]
        /// </summary>
        /// <param name="id">源表id</param>
        /// <param name="rackStatusId">货架状态id[0:上架 1:未上架 2:下架]</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<bool>> UpdateSourceTableRackStatus(int id, int rackStatusId)
        {
            //源表查询
            //货架状态值判断
            if (rackStatusId != 0 && rackStatusId != 1 && rackStatusId != 2) return new AjaxResult<bool>(false, "Invalid value of RackStatus");
            datamap entity = await _dataMapService.GetEntity(e => e.Id == id);
            if (entity == null) return new AjaxResult<bool>(false, "查无此表");
            if (entity.Status == 0) return new AjaxResult<bool>(false, "该表未归集");

            //修改货架状态
            entity.RackStatus = rackStatusId;
            await _dataMapService.UpdateColumn(entity, e => e.RackStatus);

            //string text = "";
            //if (rackStatusId == 0)
            //{
            //    text = "上架";
            //}
            //else if (rackStatusId == 1)
            //{
            //    text = "未上架";
            //}
            //else
            //{
            //    text = "下架";
            //}
            //await Logger.AddPlatformLog($"修改{entity}上下架状态为" + text, LogType.DataOperation);

            return new AjaxResult<bool>(true);
        }

        /// <summary>
        /// 源表目录
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<DataMapCollectDto>>> GetSourceTableList(GetSourceTableListPageDto dto)
        {

            //分页参数
            if (dto.PageIndex <= 0) dto.PageIndex = 1;
            if (dto.PageSize <= 0) dto.PageSize = 10;

            //源表目录查询
            RefAsync<int> total = 0;
            string sql = "select t1.`id`,t1.`name`,t1.`guid`,attr.`attribution`,t3.`Name` layeredTypeName,t4.`name` dataSafeType,t1.`describe`,t1.`createTime`,t1.`updatetime`,t1.`Traffic`,t1.`applicationNum`,t2.`name` frequencyName,t1.`associativeTable`,t1.IsDataService from `datamap` t1 left join `frequency` t2 on t1.Frequency = t2.ID left join `layered` t3 on t1.`layeredType` = t3.`id` left join `modified` t4 on t1.`modifierType` = t4.`id` left join `attribution` attr on t1.`attribution` = attr.Id left join cloud_user cu on cu.id=t1.UserId where t1.`Status`=1 and t1.`RackStatus`=0 and t1.`IsMapping` is null";
            //源表目录条件筛选
            if (dto.DataSourceUnitId.HasValue) sql += $" and t1.`attribution` = {dto.DataSourceUnitId.Value}";
            if (dto.LayeredCategoryId.HasValue) sql += $" and t1.`layeredType` = {dto.LayeredCategoryId.Value}";
            if (dto.UpdateId.HasValue) sql += $" and t1.`frequency` = {dto.UpdateId.Value}";
            if (!string.IsNullOrEmpty(dto.KeyWord)) sql += $" and t1.`name` like '%{dto.KeyWord}%'";
            if (dto.DepartmentId != null) sql += $" and cu.description={dto.DepartmentId} ";
            sql += " order by t1.`Id` desc";

            List<DataMapCollectDto> list = await _dataTableService.GetEntityPageList<DataMapCollectDto>(sql, dto.PageIndex, dto.PageSize, total);

            //await Logger.AddPlatformLog("获取源表目录", LogType.DataAccess);

            return new PageResult<List<DataMapCollectDto>>(list, total, total % dto.PageSize == 0 ? total / dto.PageSize : total / dto.PageSize + 1, dto.PageSize, dto.PageIndex);
        }

        /// <summary>
        /// 源表管理[sourceTableStatus:  "全部":null "上架":0 "未上架":1 "下架":2]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<DataMapManageDto>>> GetSourceTableManageList(SourceTableManageListPageDto dto)
        {
            //分页
            if (dto.PageIndex == 0) dto.PageIndex = 1;
            if (dto.PageSize == 0) dto.PageSize = 10;

            RefAsync<int> total = 0;
            string sql = "select t1.`Id`,t1.`Name`,t1.`Guid`,t1.`Describe`,t4.`Attribution`,t2.`Name` `LayeredTypeName`,t3.`Name` `ModifiedName`,t5.`Name` FrequencyName,t1.`AssociativeTable`,t1.`RackStatus` from data_sharing_platform.`datamap` t1 left join data_sharing_platform.`layered` t2 on t1.`LayeredType` = t2.`Id` left join data_sharing_platform.`modified` t3 on t1.`ModifierType` = t3.`Id` left join data_sharing_platform.`attribution` t4 on t1.`Attribution` = t4.Id left join data_sharing_platform.`frequency` t5 on t1.`Frequency` = t5.`ID` where t1.`Status`=1 and t1.`IsMapping` is null";

            //源表状态分类
            if (dto.RackStatus.HasValue) sql += $" and t1.`RackStatus` = {dto.RackStatus.Value}";
            sql += " order by t1.`Id` desc";
            List<DataMapManageDto> list = await _dataTableService.GetEntityPageList<DataMapManageDto>(sql, dto.PageIndex, dto.PageSize, total);

            //string text = "";
            //if (dto.RackStatus == null)
            //{
            //    text = "全部";
            //}
            //else if (dto.RackStatus == 0)
            //{
            //    text = "上架";
            //}
            //else if (dto.RackStatus == 1)
            //{
            //    text = "未上架";
            //}
            //else
            //{
            //    text = "下架";
            //}
            //await Logger.AddPlatformLog($"获取{text}源表", LogType.DataAccess);

            return new PageResult<List<DataMapManageDto>>(list, total, total % dto.PageSize == 0 ? total / dto.PageSize : total / dto.PageSize + 1, dto.PageSize, dto.PageIndex);
        }

        /// <summary>
        /// 归集数据统计
        /// </summary>
        /// <returns>[totalCount:源表数据总量 onCount:上架源表数量 offCount:下架源表数量 todoCount:未上架源表数量]</returns>
        [HttpGet]
        public async Task<AjaxResult<ResultCollectDataStatisticsDto>> CollectDataStatistics()
        {
            //已归集源表状态管理
            string sql = "select result1.`totalCount`,result2.`onCount`,result3.`offCount`,result4.`todoCount` from (select count(t.`Id`) `totalCount` from data_sharing_platform.`datamap` t where t.`Status`=1 and t.`IsMapping` is null) result1,(select count(t.`Id`) `onCount` from data_sharing_platform.`datamap` t where t.`Status`=1 and t.`RackStatus` = 0 and t.`IsMapping` is null) result2,(select count(t.`Id`) `offCount` from data_sharing_platform.`datamap` t where t.`Status`=1 and t.`RackStatus` = 1 and t.`IsMapping` is null) result3,(select count(t.`Id`) `todoCount` from data_sharing_platform.`datamap` t where t.`Status`=1 and t.`RackStatus` = 2 and t.`IsMapping` is null) result4";
            var result = await _dataTableService.GetEntity<ResultCollectDataStatisticsDto>(sql);

            //await Logger.AddPlatformLog("获取归集数据统计", LogType.DataAccess);

            return new AjaxResult<ResultCollectDataStatisticsDto>(result);
        }

        /// <summary>
        /// 源表管理编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> SourceTableManageEdit(DataMapManageEditDto dto)
        {
            datamap entity = await _dataMapService.GetEntity(e => e.Id == dto.Id);
            if (entity == null) return new AjaxResult<bool>(false, "查无此表");
            entity.Frequency = dto.Frequency;         //更新频率
            entity.Name = dto.Name;                   //表名称
            entity.LayeredType = dto.Layered;         //分层类型
            entity.ModifierType = dto.ModifierType;   //数据安全类型

            int result = await _dataMapService.UpdateColumn(entity, e => new { e.Frequency, e.Name, e.LayeredType, e.ModifierType });

            //await Logger.AddPlatformLog("源表管理编辑", LogType.DataOperation);

            return new AjaxResult<bool>(result > 0);
        }

        /// <summary>
        /// 获取原始数据目录[update:2022年8月12日11:24:00]
        /// </summary>
        /// <param name="dataMapId">源表id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<ResultDataItemDicDto>>> GetDataMapDataItems(int dataMapId)
        {
            //是否存在源表
            var dataMap = await _dataMapService.GetEntity(e => e.Id == dataMapId);
            if (dataMap is null) return new AjaxResult<List<ResultDataItemDicDto>>(false, "源表不存在");

            //查询源表结构
            string sql = $"select t.`Id`,t.`CnFieldName`,t.`EnFieldName`,t.`DataType`,t.`DataLength`,t.`Description` `FieldDescription`,t.`IsPrimaryKey`,t.`ShareType` from data_sharing_main.`dic_{dataMap.AssociativeTable}` t"; ;
            var result = await _dataTableService.GetEntityList<ResultDataItemDicDto>(sql);

            //await Logger.AddPlatformLog("获取原始数据目录", LogType.DataAccess);

            return new AjaxResult<List<ResultDataItemDicDto>>(result);
        }

        /// <summary>
        /// 获取清洗数据目录[update:2022年8月16日11:39:28]
        /// </summary>
        /// <param name="dataMapId">源表id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<ResultDataItemDicDto>>> GetDataMapDataItemsAfterCleaning(int dataMapId)
        {
            //await Logger.AddPlatformLog("获取清洗数据目录", LogType.DataAccess);
            return new AjaxResult<List<ResultDataItemDicDto>>(null);
        }

        /// <summary>
        /// 源表数据项新增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> AddDataMapDataItems(AddDataMapDataItemDto dto)
        {
            try
            {
                //找到源表
                var dataMapEntity = await _dataMapService.GetEntity(e => e.Id == dto.DataMapId);
                if (dataMapEntity == null) return new AjaxResult<bool>(false, $"找不到指定id为{dto.DataMapId}的表");
                string dicTableName = $"dic_{dataMapEntity.AssociativeTable}";

                //判断新增字段是否已经存在在表中
                object isFieldExistObj = await _dataTableService.QueryCount($"select if('{dto.EnFieldName}' in (select t.`EnFieldName` from data_sharing_main.`{dicTableName}` t),1,0) `isFieldExist`;");

                //如果存在重复字段
                if (Convert.ToInt32(isFieldExistObj) == 1) return new AjaxResult<bool>(false, $"对应英文名为{dto.EnFieldName}的字段已存在");

                //新增数据项
                //在字段字典表新增字段 同时 在源表中新增字段
                string updateTableSql = $"insert into data_sharing_main.`{dicTableName}`(`CnFieldName`,`EnFieldName`,`DataType`,`DataLength`,`Description`,`IsPrimaryKey`,`ShareType`) values('{dto.CnFieldName}','{dto.EnFieldName}','{dto.DataType}',{dto.DataLength},'{dto.FieldDescription}',{dto.IsPrimaryKey},{dto.ShareType});alter table data_sharing_main.`{dataMapEntity.AssociativeTable}` add `{dto.EnFieldName}` {dto.DataType}({dto.DataLength}) comment '{dto.FieldDescription}';";
                await _dataTableService.NoQueryExcuteSql(updateTableSql);

                //删除原有主键字段 重新添加新的主键
                //判断主键是否存在
                object isPrimaryKeyExistFlag = await _dataTableService.QueryCount($"select if(exists (select COLUMN_NAME `ColumnName` from information_schema.COLUMNS where TABLE_SCHEMA = 'data_sharing_main' and TABLE_NAME = '{dataMapEntity.AssociativeTable}' and COLUMN_KEY='PRI'),1,0) isPrimaryKeyExist");
                string updatePrimaryKeySql = null;
                if (Convert.ToInt32(isPrimaryKeyExistFlag) == 1)
                {
                    //说明源表中存在主键
                    //删除主键
                    updatePrimaryKeySql = $"alter table data_sharing_main.`{dataMapEntity.AssociativeTable}` drop primary key;";
                }

                //主键字段数组
                List<PrimaryColumnDto> primaryColumns = await _dataTableService.GetEntityList<PrimaryColumnDto>($"select t.EnFieldName `ColumnName` from data_sharing_main.`{dicTableName}` t where t.IsPrimaryKey");
                //存在主键
                if (primaryColumns.Count > 0)
                {
                    //新增主键
                    updatePrimaryKeySql += $"alter table data_sharing_main.`{dataMapEntity.AssociativeTable}` add primary key({string.Join(',', primaryColumns.Select(item => $"`{item.ColumnName}`"))});";
                }
                await _dataTableService.NoQueryExcuteSql(updatePrimaryKeySql);

                //await Logger.AddPlatformLog($"源表数据项新增", LogType.DataOperation);

                return new AjaxResult<bool>(true);
            }
            catch (Exception e)
            {
                return new AjaxResult<bool>(false, e.Message);
            }

        }

        /// <summary>
        /// 源表数据项删除
        /// </summary>
        /// <param name="dataMapId">源表id</param>
        /// <param name="dataItemId">数据项id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<bool>> DelDataMapDataItems(int dataMapId, int dataItemId)
        {
            //获取源表实体
            var dataMapEntity = await _dataMapService.GetEntity(e => e.Id == dataMapId);
            if (dataMapEntity == null) return new AjaxResult<bool>(false);
            string dicTableName = $"dic_{dataMapEntity.AssociativeTable}";

            //删除源表字段 同时 删除源表字典表中的字段
            DataItemDicDto dataItem = await _dataTableService.GetEntity<DataItemDicDto>($"select t.`Id`,t.`CnFieldName`,t.`EnFieldName`,t.`DataType`,t.`DataLength`,t.`Description` `FieldDescription`,t.`IsPrimaryKey`,t.`ShareType` from data_sharing_main.`{dicTableName}` t where t.`Id`={dataItemId}");
            await _dataTableService.NoQueryExcuteSql($"delete from data_sharing_main.`{dicTableName}` where id={dataItemId};alter table data_sharing_main.`{dataMapEntity.AssociativeTable}` drop `{dataItem.EnFieldName}`;");

            //await Logger.AddPlatformLog("源表数据项删除", LogType.DataOperation);

            return new AjaxResult<bool>(true);
        }



        /// <summary>
        /// 归集申请 [taskType 0:未申请 1:已申请]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<CollectionApplicationDto>>> GetApplyCollect(GetApplyCollectPageDto dto)
        {
            //参数判断
            if (dto.TaskType != 0 && dto.TaskType != 1) return new PageResult<List<CollectionApplicationDto>>();
            if (dto.PageIndex <= 0) dto.PageIndex = 1;
            if (dto.PageSize <= 0) dto.PageSize = 10;


            RefAsync<int> total = 0;
            string sql;
            if (dto.TaskType == 0)
            {
                //未发起申请的归集申请
                sql = $"select t.`Id`,dt.`Name` `DataName`,u.`real_name` `Applicant`,t.`phone` `Phone`,t.`StartTime` `StartTime`,t.AssociativeTable,case t.`Status` when 0 then '等待审批' when 1 then '审批中' when 2 then '审批完成' end `ApprovalStatus`,null `ProcessDetail` from data_sharing_platform.`approvaltask` t left join data_sharing_platform.`datamap` dt on t.AssociativeTable = dt.`Id` join data_sharing_platform.cloud_user u on t.`UserId` = u.`id` where t.`TaskType`=0";
            }
            else
            {
                //已发起申请的归集申请
                sql = "select t.Id,dt.`Name` `dataName`,u.`real_name` `applicant`,t.`phone` `phone`,t.`StartTime` `startTime`,t.associativeTable,p.`ProcessType`,case t.`Status` when 0 then '等待审批' when 1 then if(p.`ProcessType`=-1 and p.`Status`=0,'等待私有库转主题库审批',if(p.`ProcessType`=0 and p.`Status`=0,'等待归集申请审批',if(p.`ProcessType`=0 and p.`Status`=1,'归集申请审批通过',if(p.`ProcessType`=1 and p.`Status`=0,'等待数据导入审批',if(p.`ProcessType`=1 and p.`Status`=1,'数据导入审批通过','审批未通过'))))) when 2 then '审批完成' end `approvalStatus`,case(t.`Status`) when 0 then 0 when 1 then if(p.ProcessType=-1 and p.`Status`=0,50,if(p.`ProcessType`=0 and p.`Status`=0,25,if(p.`ProcessType`=0 and p.`Status`=1,50,if(p.`ProcessType`=1 and p.`Status`=0,75,if(p.`ProcessType`=1 and p.`Status`=1,100,100))))) when 2 then 100 end `processDetail` from `approvaltask` t join `datamap` dt on t.AssociativeTable = dt.`Id` left join cloud_user u on t.`UserId` = u.`id` left join process p on p.`Id`= t.`CurrentProcess` where t.`TaskType`=1";
            }

            //关键字查询
            if (!string.IsNullOrEmpty(dto.KeyWord)) sql += $" and t.`name` like '%{dto.KeyWord}%' or u.`real_name` like '%{dto.KeyWord}%'";
            sql += $" order by t.`StartTime` desc";

            //await Logger.AddPlatformLog($"获取归集申请{(dto.TaskType == 0 ? "未申请" : "已申请")}数据", LogType.DataAccess);

            var list = await _dataTableService.GetEntityPageList<CollectionApplicationDto>(sql, dto.PageIndex, dto.PageSize, total);
            return new PageResult<List<CollectionApplicationDto>>(list, total, total % dto.PageSize == 0 ? total / dto.PageSize : total / dto.PageSize + 1, dto.PageSize, dto.PageIndex);
        }


        /// <summary>
        /// 导入数据-可导入数据的申请列表[update:2022年8月16日16:21:52] 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<GetApprovedTaskResultDto>>> GetApprovedTask(GetApprovedTaskQueryDto dto)
        {
            //分页
            if (dto.PageIndex <= 0) dto.PageIndex = 1;
            if (dto.PageSize <= 0) dto.PageSize = 10;

            //申请流程查询
            RefAsync<int> total = 0;
            string sql = $"select t.`Id` `ApprovalId`,dt.`Id`,dt.`Name`,dt.`EnName`,a.`Attribution`,l.`Name` `Layered`,f.`Name` `Frequency`,dt.`ApplicationSystemName`,dt.`Source` from datamap dt join attribution a on a.`Id`=dt.`attribution` join layered l on l.`Id`=dt.LayeredType join frequency f on f.`Id`=dt.`Frequency` join approvaltask t on dt.`Id`=t.`AssociativeTable` where dt.`IsEnableImportData` and dt.`IsMapping` is null order by t.`StartTime` desc";
            if (dto.UserId.HasValue)
            {
                sql = $"select t.`Id` `ApprovalId`,dt.`Id`,dt.`Name`,dt.`EnName`,a.`Attribution`,l.`Name` `Layered`,f.`Name` `Frequency`,dt.`ApplicationSystemName`,dt.`Source` from datamap dt join attribution a on a.`Id`=dt.`attribution` join layered l on l.`Id`=dt.LayeredType join frequency f on f.`Id`=dt.`Frequency` join approvaltask t on dt.`Id`=t.`AssociativeTable` where dt.`IsEnableImportData` and dt.`IsMapping` is null and dt.`UserId`={dto.UserId} order by t.`StartTime` desc";
            }

            var list = await _dataTableService.GetEntityPageList<GetApprovedTaskResultDto>(sql, dto.PageIndex, dto.PageSize, total);

            //await Logger.AddPlatformLog("获取可导入数据的申请列表", LogType.DataAccess);

            return new PageResult<List<GetApprovedTaskResultDto>>(list, total, total % dto.PageSize == 0 ? total / dto.PageSize : total / dto.PageSize + 1, dto.PageSize, dto.PageIndex);

        }

        /// <summary>
        /// 查看源表详情
        /// </summary>
        /// <param name="dataMapId">源表Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<SourceTableDetailDto>> GetDataMapDetail(int dataMapId)
        {
            //源表详情查询
            string dataMapSelectSql = $"select t1.`Id`,t1.`Name`,t1.`Guid`,t1.`Describe`,t1.`Traffic`,t1.`InfoSummary`,t1.`ApplicationNum`,t1.`ApplicationSystemName`,t1.`UpdateTime`,t1.`LinkPerson`,t1.`LinkPhone`,t1.`Source`,t4.`Attribution`,t2.`Name` `LayeredTypeName`,t3.`Name` `SafeTypeName`,t1.isdataservice ,t5.`Name` FrequencyName,t1.`AssociativeTable`,t1.`RackStatus`,t1.`Status` from `datamap` t1 left join `layered` t2 on t1.`LayeredType` = t2.`Id` left join `modified` t3 on t1.`ModifierType` = t3.`Id` left join `attribution` t4 on t1.`Attribution` = t4.Id left join `frequency` t5 on t1.`Frequency` = t5.`ID` where t1.`Id`={dataMapId}";
            SourceTableDetailDto result = await _dataTableService.GetEntity<SourceTableDetailDto>(dataMapSelectSql);
            var ismapping = await _dataTableService.QueryCount($"select ismapping from datamap where id={dataMapId}");

            string dicTableName = $"dic_{result.AssociativeTable}";

            //判断字段字典表是否存在
            //int isDicTableExistFlag = await IsTableExist(dicTableName);
            string dbname = "";
            if (ismapping.ObjToInt()!=1) dbname = "data_sharing_main";
            else dbname = "data_sharing_affiliated";
            int isDicTableExistFlag = await IsTableExist(dicTableName, dbname);
            if (isDicTableExistFlag == 0) return new AjaxResult<SourceTableDetailDto>(null, "查无此表");

            //关联源表字典查询
            //string dicSelectSql = $"select t.`Id`,t.`CnFieldName`,t.`EnFieldName`,t.`DataType`,t.`DataLength`,t.`Description` `FieldDescription`,t.`IsPrimaryKey`,t.`ShareType` from data_sharing_main.`{dicTableName}` t";
            string dicSelectSql = $"select t.`Id`,t.`CnFieldName`,t.`EnFieldName`,t.`DataType`,t.`DataLength`,t.`Description` `FieldDescription`,t.`IsPrimaryKey`,t.`ShareType` from {dbname}.`{dicTableName}` t";
            result.DataItems = await _dataTableService.GetEntityList<ResultDataItemDicDto>(dicSelectSql);

            //await Logger.AddPlatformLog("查看源表详情", LogType.DataAccess);

            return new AjaxResult<SourceTableDetailDto>(result);
        }


        /// <summary>
        /// 编辑show[update:2022年8月26日17:00:20]
        /// </summary>
        /// <param name="dataMapId">源表Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<EditShowResultDto>> EditShow(int dataMapId)
        {
            var dt = await _dataMapService.GetEntity(e => e.Id == dataMapId);
            if (dt is null)
            {
                return new AjaxResult<EditShowResultDto>(false, "源表不存在");
            }

            //源表详情查询
            string dataMapSelectSql = $"select t1.`Id`,t1.`EnName` dataMapEnName, t1.`Name`,t1.`Guid`,t1.`Describe`,t1.`InfoSummary`,t1.`ApplicationNum`,t1.`Frequency`, t1.`ApplicationSystemName`,t1.`UpdateTime`,t1.`LinkPerson`,t1.`LinkPhone`,t1.`Source`,t1.`Attribution` AttributionId,t1.`LayeredType` LayeredId,t1.`ModifierType` DataTypeId,t1.`Frequency` FrequencyId,t1.`AssociativeTable`,t1.`RackStatus`,t1.`Status` from `datamap` t1 where t1.`Id`={dataMapId}";
            EditShowResultDto result = await _dataTableService.GetEntity<EditShowResultDto>(dataMapSelectSql);
            string dicTableName = $"dic_{result.AssociativeTable}";

            //判断字段字典表是否存在
            int isDicTableExistFlag = await IsTableExist(dicTableName);
            if (isDicTableExistFlag == 0) return new AjaxResult<EditShowResultDto>(null, "查无此表");

            //关联源表字典查询
            string dicSelectSql = $"select t.`Id`,t.`CnFieldName`,t.`EnFieldName`,t.`DataType`,t.`DataLength`,t.`Description` `FieldDescription`,t.`IsPrimaryKey`,t.`ShareType` from data_sharing_main.`{dicTableName}` t";
            result.DataItems = await _dataTableService.GetEntityList<ResultDataItemDicDto>(dicSelectSql);

            //await Logger.AddPlatformLog("编辑show", LogType.DataOperation);

            return new AjaxResult<EditShowResultDto>(result);
        }


        /// <summary>
        /// 新增申请表[update:2022年8月16日16:45:36]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> AddApplyTable(AddApplyTableDto dto)
        {
            //#用户登录状态判断
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(t => t.Type == JwtRegisteredClaimNames.NameId)?.Value);

            //1.数据项字段异常数据判断
            Type t = typeof(DataItemDicDto);
            if (dto.DataItems.Any(item => t.GetProperties().Any(p => p.GetValue(item) == null))) return new AjaxResult<bool>(false, "数据项字段不能为空");
            //if (dto.DataItems.Any(item => item.DataLength <= 0)) return new AjaxResult<bool>(false, "数据类型长度不能为0");
            var cnNameFileds = dto.DataItems.Select(e => e.CnFieldName);
            var enNameFileds = dto.DataItems.Select(e => e.EnFieldName);
            if (cnNameFileds.Count() - cnNameFileds.Distinct().Count() != 0) return new AjaxResult<bool>(false, "存在名称相同的中文字段");
            if (enNameFileds.Count() - enNameFileds.Distinct().Count() != 0) return new AjaxResult<bool>(false, "存在名称相同的英文字段");

            //2.拼接共享字段
            //List<string> sharingFieldList = new List<string>();
            //dto.DataItems.ForEach(item =>
            //{
            //    if (item.ShareType) sharingFieldList.Add(item.EnFieldName);
            //});

            //3.建表
            //生成表名称  表名称格式:表名_Guid
            string dataMapGuid = Guid.NewGuid().ToString("N");
            string createTableName = dataMapGuid;

            StringBuilder createTableSql = new StringBuilder($"use data_sharing_main;create table `{createTableName}`(`Id` bigint(20) primary key auto_increment comment '自增Id',@items);");
            string createItemsSql = string.Join(',', dto.DataItems.Select(item => $"`{item.EnFieldName}` {item.DataType}({item.DataLength}) comment '{item.FieldDescription}'"));
            string finalCreateTableSql = createTableSql.Replace("@items", createItemsSql).ToString();

            //筛选主键字段
            IEnumerable<DataItemDicDto> primaryKeys = dto.DataItems.Where(item => item.IsPrimaryKey);
            //如果存在要设置的主键
            if (primaryKeys.Count() > 0)
            {
                //获取新增主键字段
                string primaryKeyNames = string.Join(',', primaryKeys.Select(item => $"`{item.EnFieldName}`"));
                //删除表创建时的默认自增主键——id字段
                finalCreateTableSql += $"alter table `{createTableName}` modify Id bigint;alter table `{createTableName}` drop primary key;";
                //新增表中新增主键字段
                finalCreateTableSql += $"alter table `{createTableName}` add primary key(`Id`,{primaryKeyNames});alter table `{createTableName}` modify Id bigint not null auto_increment;";
            }

            //创建源表
            await _dataTableService.NoQueryExcuteSql(finalCreateTableSql);

            //创建字段字典映射表
            string createFieldsDicTableName = $"dic_{createTableName}";
            string createFieldsDicTableSql = $"use data_sharing_main;create table {createFieldsDicTableName}(Id int primary key auto_increment,`CnFieldName` varchar(255),`EnFieldName` varchar(255),`DataType` varchar(255),`DataLength` varchar(255),`Description` varchar(255),`IsPrimaryKey` bit,`ShareType` bit);insert into {createFieldsDicTableName}(`CnFieldName`,`EnFieldName`,`DataType`,`DataLength`,`Description`,`IsPrimaryKey`,`ShareType`) values @values;";
            //字典表数据
            string dicDatas = string.Join(',', dto.DataItems.Select(item => $"('{item.CnFieldName}','{item.EnFieldName}','{item.DataType}','{item.DataLength}','{item.FieldDescription}',{item.IsPrimaryKey},{item.ShareType})"));
            string finalCreateFieldsDicTableSql = createFieldsDicTableSql.Replace("@values", dicDatas);

            //创建字段表
            await _dataTableService.NoQueryExcuteSql(finalCreateFieldsDicTableSql);

            //4.新增源表、申请表记录
            var dt = await _dataMapService.ValueAdd(new datamap
            {
                Name = dto.DataMapName,
                EnName = dto.DataMapEnName,
                Attribution = dto.AttributionId,
                LayeredType = dto.LayeredId,
                Frequency = dto.FrequencyId,
                ApplicationSystemName = dto.ApplicationSystemName,
                ModifierType = dto.DataTypeId,
                Describe = dto.Description,
                InfoSummary = dto.InfoSummary,
                LinkPerson = dto.LinkPerson,
                LinkPhone = dto.LinkPhone,
                GUID = dataMapGuid,
                AssociativeTable = createTableName,
                CreateTime = DateTime.Now,
                ApplicationNum = 0,
                Traffic = 0,
                Source = dto.Source, //数据来源
                Status = 0,
                RackStatus = 1,
                UserId = userId,
                IsPrivate = dto.IsPrivate,
                ExpireTime = dto.ExpireTime,
                PatternName="data_sharing_main"
            });

            //如果不添加私有库
            if (!dto.IsPrivate)
            {
                //添加流程号
                int processId = await _processService.IdAdd(new process
                {
                    Describe = "创建申请表",
                    CreateTime = DateTime.Now,
                    Status = 0, //状态：0:未审批；1:审批通过；2:驳回
                    UserId = userId
                });
                //添加申请
                int taskId = await _approvalTaskService.IdAdd(new approvaltask
                {
                    AssociativeTable = dt.Id,
                    CurrentProcess = processId,
                    StartTime = DateTime.Now,
                    Phone = dto.LinkPhone,
                    TaskType = 0,//0:未申请 1:已申请
                    UserId = userId
                });
                await _processService.UpdateColumn(new process { Id = processId, LinkApproval = taskId }, e => e.LinkApproval);
            }
            else//如果新增的表是私有库
            {
                if (dto.ExpireTime is null)
                {
                    dt.ExpireTime = DateTime.Now.AddDays(7);//新增的私有库默认存放7天
                }
                dt.IsEnableImportData = true;
                dt.IsEnableClassify = true;
                await _dataMapService.UpdateColumn(dt, e => new { e.IsEnableImportData, e.IsEnableClassify, e.ExpireTime });
            }

            //await Logger.AddPlatformLog("新增申请表", LogType.DataOperation);

            return new AjaxResult<bool>(true);
        }

        /// <summary>
        /// 编辑申请表[update:2022年8月24日11:32:51]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> EditApplyTable(EditApplyTableDto dto)
        {
            // 1.参数校验
            var dt = await _dataMapService.GetEntity(e => e.Id == dto.Id);
            if (dt is null) return new AjaxResult<bool>(false, "源表不存在");
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(t => t.Type == JwtRegisteredClaimNames.NameId)?.Value);
            //如果数据项发生改变
            if (dto.IsItemsChanged)
            {
                Type t = typeof(DataItemDicDto);
                if (dto.DataItems.Any(item => t.GetProperties().Any(p => p.GetValue(item) == null))) return new AjaxResult<bool>(false, "数据项字段不能为空");
                //if (dto.DataItems.Any(item => item.DataLength <= 0)) return new AjaxResult<bool>(false, "数据类型长度不能为0");
                var cnNameFileds = dto.DataItems.Select(e => e.CnFieldName);
                var enNameFileds = dto.DataItems.Select(e => e.EnFieldName);
                if (cnNameFileds.Count() - cnNameFileds.Distinct().Count() != 0) return new AjaxResult<bool>(false, "存在名称相同的中文字段");
                if (enNameFileds.Count() - enNameFileds.Distinct().Count() != 0) return new AjaxResult<bool>(false, "存在名称相同的英文字段");

                //2.修改源表、源表字典表

                string tableName = dt.AssociativeTable;
                string dicTableName = $"dic_{tableName}";
                StringBuilder createTableSql = new StringBuilder($"use data_sharing_main;drop table if exists `{tableName}`,`{dicTableName}`;create table `{tableName}` (`Id` bigint(20) primary key auto_increment comment '自增Id',@items);");
                string items = string.Join(',', dto.DataItems.Select(item => $"`{item.EnFieldName}` {item.DataType}({item.DataLength}) comment '{item.FieldDescription}'"));
                string finalCreateTableSql = createTableSql.Replace("@items", items).ToString();
                IEnumerable<DataItemDicDto> primaryKeys = dto.DataItems.Where(item => item.IsPrimaryKey);
                //如果存在要设置的主键
                if (primaryKeys.Count() > 0)
                {
                    //获取新增主键字段
                    string primaryKeyNames = string.Join(',', primaryKeys.Select(item => $"`{item.EnFieldName}`"));
                    //删除表创建时的默认自增主键——id字段,新增表中新增主键字段
                    finalCreateTableSql += $"alter table `{tableName}` modify Id bigint;alter table `{tableName}` drop primary key;alter table `{tableName}` add primary key(`Id`,{primaryKeyNames});alter table `{tableName}` modify Id bigint not null auto_increment;";
                }
                await _dataTableService.NoQueryExcuteSql(finalCreateTableSql);

                //创建字典表
                string createDicTableSql = $"use data_sharing_main;create table {dicTableName}(Id int primary key auto_increment,`CnFieldName` varchar(255),`EnFieldName` varchar(255),`DataType` varchar(255),`DataLength` int,`Description` varchar(255),`IsPrimaryKey` bit,`ShareType` bit);insert into {dicTableName}(`CnFieldName`,`EnFieldName`,`DataType`,`DataLength`,`Description`,`IsPrimaryKey`,`ShareType`) values @values;";
                string dicDatas = string.Join(',', dto.DataItems.Select(item => $"('{item.CnFieldName}','{item.EnFieldName}','{item.DataType}',{item.DataLength},'{item.FieldDescription}',{item.IsPrimaryKey},{item.ShareType})"));
                string finalCreateDicTableSql = createDicTableSql.Replace("@values", dicDatas);
                await _dataTableService.NoQueryExcuteSql(finalCreateDicTableSql);
            }

            dt.Name = dto.DataMapName;
            dt.EnName = dto.DataMapEnName;
            dt.Attribution = dto.AttributionId;
            dt.LayeredType = dto.LayeredId;
            dt.Frequency = dto.FrequencyId;
            dt.ApplicationSystemName = dto.ApplicationSystemName;
            dt.ModifierType = dto.DataTypeId;
            dt.LinkPerson = dto.LinkPerson;
            dt.LinkPhone = dto.LinkPhone;
            dt.Describe = dto.Description;
            dt.InfoSummary = dto.InfoSummary;
            await _dataMapService.Update(dt, e => new { e.GUID, e.AssociativeTable, e.IsEnableClassify, e.IsEnableImportData, e.IsPrivate, e.ImportDataTime });

            //await Logger.AddPlatformLog("编辑申请表", LogType.DataOperation);

            return new AjaxResult<bool>(true);
        }

        /// <summary>
        /// 归集申请-流程查看
        /// </summary>
        /// <param name="approvalTaskId">归集申请任务Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<ResultApprovalDetailDto>>> GetApprovalDetail(int approvalTaskId)
        {
            //流程查询
            var task = await _approvalTaskService.GetEntity(e => e.Id == approvalTaskId);
            if (task == null) return new AjaxResult<List<ResultApprovalDetailDto>>(false, "流程号出错");

            //流程详情查询
            string sql = $"select p.`Id`,u.`real_name` `userName`,p.`Describe`,p.`Accessory`,p.`Status`,p.`CreateTime`,if(p.`Status`=3,true,false) `isOver` from `process` p join `cloud_user` u on p.`UserId` = u.`Id` where p.`LinkApproval`={approvalTaskId} order by p.`CreateTime`";
            var list = await _dataTableService.GetEntityList<ResultApprovalDetailDto>(sql);

            //await Logger.AddPlatformLog("查看归集申请流程", LogType.DataAccess);

            return new AjaxResult<List<ResultApprovalDetailDto>>(list);
        }

        /// <summary>
        /// 归集申请-流程查看-申请表信息
        /// </summary>
        /// <param name="approvalTaskId">归集申请任务Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<ApprovalTableInfoDto>> GetApprovalDataMapDetail(int approvalTaskId)
        {
            //申请流程号、源表查询
            var approval = await _approvalTaskService.GetEntity(e => e.Id == approvalTaskId);
            if (approval is null) return new AjaxResult<ApprovalTableInfoDto>(false, "查无此流程");
            var dataMap = await _dataMapService.GetEntity(e => e.Id == approval.AssociativeTable);
            if (dataMap is null) return new AjaxResult<ApprovalTableInfoDto>(false, "查无此表");

            //源表信息查询
            string dataMapSelectSql = $"select t1.`Id`,t1.`Name`,t1.`InfoSummary`,t1.`ApplicationSystemName`,t1.`LinkPerson`,t1.`LinkPhone`,t1.`Source`,t4.`Attribution`,t2.`Name` `LayeredTypeName`,t5.`Name` FrequencyName from `datamap` t1 left join `layered` t2 on t1.`LayeredType` = t2.`Id` left join `modified` t3 on t1.`ModifierType` = t3.`Id` left join `attribution` t4 on t1.`Attribution` = t4.Id left join `frequency` t5 on t1.`Frequency` = t5.`ID` where t1.`Id`={dataMap.Id}";
            ApprovalTableInfoDto result = await _dataTableService.GetEntity<ApprovalTableInfoDto>(dataMapSelectSql);
            string dicTableName = $"dic_{dataMap.AssociativeTable}";

            //判断字段字典表是否存在
            int isDicTableExist = await IsTableExist(dicTableName);
            int isDataTableExist = await IsTableExist(dataMap.AssociativeTable);
            if (isDicTableExist == 0 || isDataTableExist == 0) return new AjaxResult<ApprovalTableInfoDto>(false, "查无此表");

            //关联源表查询
            string dicSelectSql = $"select t.`Id`,t.`CnFieldName`,t.`EnFieldName`,t.`DataType`,t.`DataLength`,t.`Description` `FieldDescription`,t.`IsPrimaryKey`,t.`ShareType` from data_sharing_main.`{dicTableName}` t";
            result.DataItems = await _dataTableService.GetEntityList<ResultDataItemDicDto>(dicSelectSql);

            //await Logger.AddPlatformLog("查看归集申请表信息", LogType.DataAccess);

            return new AjaxResult<ApprovalTableInfoDto>(result);
        }

        /// <summary>
        /// 归集申请删除
        /// </summary>
        /// <param name="approvalId">审批流程id号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<bool>> ApprovalDel(int approvalId)
        {
            //流程号、源表是否存在
            var approvalTask = await _approvalTaskService.GetEntity(e => e.Id == approvalId);
            if (approvalTask == null) return new AjaxResult<bool>(false, "申请流程号不存在");
            var dataMap = await _dataMapService.GetEntity(e => e.Id == approvalTask.AssociativeTable);
            if (dataMap == null) return new AjaxResult<bool>(false, "查无此表");

            //删除源表、流程号
            await _dataTableService.NoQueryExcuteSql($"drop table if exists data_sharing_main.`{dataMap.AssociativeTable}`;drop table if exists data_sharing_main.`dic_{dataMap.AssociativeTable}`;");
            await _approvalTaskService.Delete(e => e.Id == approvalId);
            await _dataMapService.Delete(e => e.Id == approvalTask.AssociativeTable);

            //await Logger.AddPlatformLog("归集申请删除", LogType.DataOperation);

            return new AjaxResult<bool>(true);
        }

        /// <summary>
        /// 审批管理列表 [Status: 0:等待审批 1:审批中 2:审批完成]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<CollectionApplicationDto>>> GetCollectionApplication(GetCollectionApplicationPageDto dto)
        {
            //分页
            if (dto.PageIndex <= 0) dto.PageIndex = 1;
            if (dto.PageSize <= 0) dto.PageSize = 10;

            //审批管理列表
            RefAsync<int> total = 0;
            string sql = $"select t.Id,dt.`Name` `dataName`,u.`real_name` `applicant`,t.`phone` `phone`,t.`StartTime` `startTime`,t.associativeTable,p.`ProcessType`, case t.`Status` when 0 then '等待审批' when 1 then if(p.`ProcessType`=-1 and p.`Status`=0,'等待私有库转库审批',if(p.`ProcessType`=0 and p.`Status`=0,'等待归集申请审批',if(p.`ProcessType`=0 and p.`Status`=1,'归集申请审批通过',if(p.`ProcessType`=1 and p.`Status`=0,'等待数据导入审批',if(p.`ProcessType`=1 and p.`Status`=1,'数据导入审批通过','数据审批未通过'))))) when 2 then '审批完成' end `approvalStatus`,case(t.`Status`) when 0 then 0 when 1 then if(p.`ProcessType`=-1 and p.`Status`=0,50,if(p.`ProcessType`=0 and p.`Status`=0,25,if(p.`ProcessType`=0 and p.`Status`=1,50,if(p.`ProcessType`=1 and p.`Status`=0,75,if(p.`ProcessType`=1 and p.`Status`=1,100,100))))) when 2 then 100 end `processDetail` from `approvaltask` t left join `datamap` dt on t.AssociativeTable = dt.`Id` left join cloud_user u on t.`UserId` = u.`id` left join process p on p.`Id`= t.`CurrentProcess` where t.`TaskType`=1";
            //条件筛选
            if (dto.UserId.HasValue) sql += $" and t.`userId`={dto.UserId}";
            if (dto.Status.HasValue) sql += $" and t.`status`={dto.Status}";
            sql += " order by t.`Id` desc";//按照申请时间降序排序

            var list = await _dataTableService.GetEntityPageList<CollectionApplicationDto>(sql, dto.PageIndex, dto.PageSize, total);

            //string state = "";
            //if (dto.Status == 0)
            //{
            //    state = "等待审批";
            //}
            //else if (dto.Status == 1)
            //{
            //    state = "审批中";
            //}
            //else
            //{
            //    state = "审批完成";
            //}
            //await Logger.AddPlatformLog($"获取审批管理{state}列表", LogType.DataAccess);

            return new PageResult<List<CollectionApplicationDto>>(list, total, total % dto.PageSize == 0 ? total / dto.PageSize : total / dto.PageSize + 1, dto.PageSize, dto.PageIndex);
        }

        /// <summary>
        /// 获取审批管理申请人
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<ResultGetApprovalApplicantDto>>> GetApprovalApplicant()
        {

            string sql = "select distinct t2.Id `userId`,t2.real_name `name` from `approvaltask` t1 join `cloud_user` t2 on t1.userId = t2.Id;";
            var applicantList = await _dataTableService.GetEntityList<ResultGetApprovalApplicantDto>(sql);

            //await Logger.AddPlatformLog("获取审批管理申请人", LogType.DataAccess);

            return new AjaxResult<List<ResultGetApprovalApplicantDto>>(applicantList);
        }

        /// <summary>
        /// 发起申请
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> ApprovalStart(ApprovalStartDto dto)
        {
            //获取当前登录用户
            int userId = Convert.ToInt32(User.FindFirst(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value);

            //处理每个申请
            foreach (int approvalId in dto.approvalIds)
            {
                //获取当前申请流程
                approvaltask task = await _approvalTaskService.GetEntity(e => e.Id == approvalId);
                var dt = await _dataMapService.GetEntity(e => e.Id==task.AssociativeTable);
                if (task == null) continue;
                else if (dt.IsPrivate && task.Status==0)
                {
                    //添加审批流程
                    var nextPid = await _processService.IdAdd(new process
                    {
                        ProcessType = -1,  //-1:转库申请
                        Describe = "发起转库申请",
                        CreateTime = DateTime.Now,
                        Status = 0, //状态：0:未审批；1:审批通过；2:驳回
                        LinkApproval = approvalId,
                        UserId = userId
                    });

                    //更新:当前审批申请的状态、当前的流程
                    task.CurrentProcess = nextPid;
                    task.Status = 1;
                    task.TaskType = 1;
                    await _approvalTaskService.UpdateColumn(task, e => new { e.CurrentProcess, e.Status, e.TaskType });
                }
                if (task.Status is null)//新导入的源表状态值为null
                {
                    //添加审批流程
                    var nextPid = await _processService.IdAdd(new process
                    {
                        ProcessType = 0,
                        Describe = "发起归集申请",
                        CreateTime = DateTime.Now,
                        Status = 0, //状态：0:未审批；1:审批通过；2:驳回
                        LinkApproval = approvalId,
                        UserId = userId
                    });

                    //更新:当前审批申请的状态、当前的流程
                    task.CurrentProcess = nextPid;
                    task.Status = 1;
                    task.TaskType = 1;
                    await _approvalTaskService.UpdateColumn(task, e => new { e.CurrentProcess, e.Status, e.TaskType });
                }
                if (task.Status == 1)//审批中
                {
                    //当前流程号
                    var currentPid = await _processService.GetEntity(e => e.Id == task.CurrentProcess);
                    //如果归集申请通过才允许发起数据导入申请
                    if (currentPid.ProcessType == 0 && currentPid.Status == 1)
                    {
                        var nextPid = await _processService.IdAdd(new process
                        {
                            ProcessType = 1,
                            Describe = "发起数据导入申请",
                            CreateTime = DateTime.Now,
                            Status = 0, //状态：0:未审批；1:审批通过；2:驳回
                            LinkApproval = approvalId,
                            UserId = userId
                        });
                        task.CurrentProcess = nextPid;
                        await _approvalTaskService.UpdateColumn(task, e => new { e.CurrentProcess });
                    }
                    ////如果数据导入申请通过才允许发起分级分类申请
                    //if (currentPid.ProcessType==1 && currentPid.Status==1)
                    //{
                    //    var nextPid = await _processService.IdAdd(new process
                    //    {
                    //        ProcessType=2,
                    //        Describe="发起分级分类申请",
                    //        CreateTime=DateTime.Now,
                    //        Status=0, //状态：0:未审批；1:审批通过；2:驳回
                    //        LinkApproval=approvalId,
                    //        UserId=userId
                    //    });
                    //    task.CurrentProcess=nextPid;
                    //    await _approvalTaskService.UpdateColumn(task, e => new { e.CurrentProcess });
                    //}
                }
                //await Logger.AddPlatformLog($"发起{(task.Status is null ? "归集" : "数据导入")}申请", LogType.DataApply);
            }


            return new AjaxResult<bool>(true);
        }

        /// <summary>
        /// 审批管理
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<CollectionApplicationDto>>> GetApprovalManageList(GetApprovalManageListPageDto dto)
        {
            //分页
            if (dto.PageIndex <= 0) dto.PageIndex = 1;
            if (dto.PageSize <= 0) dto.PageSize = 10;

            //审批管理
            RefAsync<int> total = 0;
            string sql = $"select t.Id,dt.`Name` `dataName`,u.`real_name` `applicant`,t.`phone` `phone`,t.`StartTime` `startTime`,t.associativeTable,p.`ProcessType`, case t.`Status` when 0 then '等待审批' when 1 then '审批中' when 2 then '审批完成' end `approvalStatus`,case(t.`Status`) when 0 then 0 when 1 then if(p.`ProcessType`=0 and p.`Status`=0,25,if(p.`ProcessType`=0 and p.`Status`=1,50,if(p.`ProcessType`=1 and p.`Status`=0,75,if(p.`ProcessType`=1 and p.`Status`=1,100,100)))) when 2 then 100 end `processDetail` from `approvaltask` t left join `datamap` dt on t.AssociativeTable = dt.`Id` left join cloud_user u on t.`UserId` = u.`id` left join process p on p.`Id`= t.`CurrentProcess` where t.`TaskType`=1";
            //筛选条件
            if (dto.UserId.HasValue) sql += $" and t1.`userId`={dto.UserId}";
            if (dto.StatusId.HasValue) sql += $" and t1.`status`={dto.StatusId}";
            sql += " order by t.`StartTime`";//按照申请发起的时间降序

            var result = await _dataTableService.GetEntityPageList<CollectionApplicationDto>(sql, dto.PageIndex, dto.PageSize, total);

            //await Logger.AddPlatformLog("获取审批管理列表", LogType.DataAccess);

            return new PageResult<List<CollectionApplicationDto>>(result, total, total % dto.PageSize == 0 ? total / dto.PageSize : total / dto.PageSize + 1, dto.PageSize, dto.PageIndex);
        }

        /// <summary>
        /// 审批处理[update:2022年9月2日16:48:36]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> ApprovalProcess(ApprovalProcessQueryDto dto)
        {
            //请求参数status传参判断
            if (dto.Status != 1 && dto.Status != 2) return new AjaxResult<bool>(false, "Invalid value of 'Status'.");
            var task = await _approvalTaskService.GetEntity(e => e.Id == dto.TaskId);
            if (task == null) return new AjaxResult<bool>(false, "Invalid value of 'TaskId'.");

            //流程审批状态判断
            //任务状态[0:等待申请 1:审批中 2:审批完成]
            if (task.Status == 2) return new AjaxResult<bool>(false, "该流程已完结");

            //从token中获取UserId
            int userId = Convert.ToInt32(User.FindFirst(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value);

            //如果审批通过
            if (dto.Status == 1)//状态：0:未审批；1:审批通过；2:驳回
            {
                var currentPId = await _processService.GetEntity(e => e.Id == task.CurrentProcess);
                int nextPId;

                //判断该流程是否已经审批
                if (currentPId.Status != 0) return new AjaxResult<bool>(false, "请勿重复审批");
                if (currentPId.ProcessType==-1)//私有库转库审批
                {
                    //新增审批通过、完结流程
                    await _processService.IdAdd(new process
                    {
                        Describe = "审批通过",
                        CreateTime = DateTime.Now,
                        Status = dto.Status,
                        LinkApproval = task.Id,
                        UserId = userId,
                        ProcessType = currentPId.ProcessType
                    });
                    nextPId = await _processService.IdAdd(new process
                    {
                        Describe = "完结",
                        CreateTime = DateTime.Now,
                        Status = 3,
                        LinkApproval = task.Id,
                        UserId = userId,
                        ProcessType = currentPId.ProcessType
                    });

                    //允许源表导入数据
                    //修改审批当前流程号
                    task.CurrentProcess = nextPId;
                    task.Status=2;
                    task.EndTime=DateTime.Now;
                    await _approvalTaskService.UpdateColumn(task, e => new { e.CurrentProcess, e.Status, e.EndTime });
                    await _dataMapService.UpdateColumn(new datamap { Id = Convert.ToInt32(task.AssociativeTable), IsPrivate=false, Status=1, RackStatus=1 }, e => new { e.IsPrivate, e.Status, e.RackStatus });
                }

                if (currentPId.ProcessType == 0)//归集申请审批
                {
                    //新增审批通过流程
                    nextPId = await _processService.IdAdd(new process
                    {
                        Describe = "审批通过",
                        CreateTime = DateTime.Now,
                        Status = dto.Status,
                        LinkApproval = task.Id,
                        UserId = userId,
                        ProcessType = currentPId.ProcessType
                    });

                    //允许源表导入数据
                    //修改审批当前流程号
                    task.CurrentProcess = nextPId;
                    await _approvalTaskService.UpdateColumn(task, e => new { e.CurrentProcess });
                    await _dataMapService.UpdateColumn(new datamap { Id = Convert.ToInt32(task.AssociativeTable), IsEnableImportData = true }, e => new { e.IsEnableImportData });
                }
                else if (currentPId.ProcessType == 1)//数据导入审批
                {
                    //新增审批通过流程
                    nextPId = await _processService.IdAdd(new process
                    {
                        Describe = "审批通过",
                        CreateTime = DateTime.Now,
                        Status = dto.Status,
                        LinkApproval = task.Id,
                        UserId = userId,
                        ProcessType = currentPId.ProcessType
                    });
                    await _processService.Add(new process
                    {
                        Describe = "完结",
                        CreateTime = DateTime.Now,
                        Status = 3,
                        LinkApproval = task.Id,
                        UserId = userId,
                        ProcessType = currentPId.ProcessType
                    });


                    //修改审批当前进程号
                    task.CurrentProcess = nextPId;
                    task.EndTime = DateTime.Now;
                    task.Status = 2;
                    var dt = await _dataMapService.GetEntity(e => e.Id == task.AssociativeTable);
                    dt.Status = 1;
                    dt.IsPrivate = false;
                    await _approvalTaskService.UpdateColumn(task, e => new { e.Status, e.CurrentProcess, e.EndTime });
                    await _dataMapService.UpdateColumn(dt, e => new { e.IsPrivate, e.Status });
                }
                //else if (currentPId.ProcessType == 2)//分类分级审批
                //{
                //    //当前流程号
                //    nextPId = await _processService.IdAdd(new process
                //    {
                //        Describe = "审批通过",
                //        CreateTime = DateTime.Now,
                //        Status = dto.Status,
                //        LinkApproval = task.Id,
                //        UserId = userId,
                //        ProcessType = currentPId.ProcessType
                //    });
                //    await _processService.Add(new process
                //    {
                //        Describe = "完结",
                //        CreateTime = DateTime.Now,
                //        Status = 3,
                //        LinkApproval = task.Id,
                //        UserId = userId,
                //        ProcessType = currentPId.ProcessType
                //    });

                //    //修改源表归集状态
                //    //如果是将私有库则转成公有库;
                //    //修改审批当前进程号
                //    task.EndTime = DateTime.Now;
                //    task.Status = 2;
                //    var dt = await _dataMapService.GetEntity(e => e.Id == task.AssociativeTable);
                //    dt.Status = 1;
                //    dt.IsPrivate = false;
                //    await _dataMapService.UpdateColumn(dt, e => new { e.Status, e.IsPrivate });
                //    await _approvalTaskService.UpdateColumn(task, e => new { e.Status, e.EndTime });
                //}
                else
                {
                    return new AjaxResult<bool>(false, "未处理的异常");
                }
            }
            if (dto.Status == 2)//状态：0:未审批；1:审批通过；2:驳回
            {
                var currentPid = await _processService.GetEntity(e => e.Id == task.CurrentProcess);
                if (currentPid == null) return new AjaxResult<bool>(false, "当前流程号为null");
                if (currentPid.Status != 0) return new AjaxResult<bool>(false, "该流程已审批");

                var pid = await _processService.IdAdd(new process
                {
                    Describe = dto.AttachInfo,
                    CreateTime = DateTime.Now,
                    Status = dto.Status,
                    LinkApproval = task.Id,
                    UserId = userId,
                    ProcessType = currentPid.ProcessType,

                });

                //流程直接完结
                //修改流程当前进程号
                task.EndTime = DateTime.Now;
                task.Status = 2;
                task.CurrentProcess = pid;
                await _approvalTaskService.UpdateColumn(task, e => new { e.Status, e.CurrentProcess, e.EndTime });
            }

            //await Logger.AddPlatformLog($"归集审批{(dto.Status == 0 ? "通过" : "驳回")}", LogType.DataApproval);

            return new AjaxResult<bool>(true);
        }

        /// <summary>
        /// 一键审批[update:2022年9月2日16:48:17]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> ApprovalListProcess(ApprovalListProcessDto dto)
        {
            //限制请求参数 1:审批通过 2:驳回
            if (dto.Status != 1 && dto.Status != 2) return new AjaxResult<bool>(false, "Invalid value of 'Status'.");

            //所有审批任务list
            List<approvaltask> taskList = await _approvalTaskService.GetEntitys(e => dto.TaskIdList.Contains(e.Id));
            int userId = Convert.ToInt32(User.FindFirst(c => c.Type == JwtRegisteredClaimNames.NameId)?.Value);

            foreach (var task in taskList)
            {
                //如果流程已完结,则跳过
                if (task.Status == 2) continue;

                //判断数据准确性
                var currentProcess = await _processService.GetEntity(e => e.Id == task.CurrentProcess);
                if (currentProcess is null) continue;
                var dt = await _dataMapService.GetEntity(e => e.Id == task.AssociativeTable);
                if (dt is null) continue;

                if (currentProcess.ProcessType == 0 && currentProcess.Status == 0)//归集审批
                {
                    //每个任务新增流程审批记录
                    int nextPid = 0;
                    if (dto.Status == 1)//通过
                    {
                        nextPid = await _processService.IdAdd(new process
                        {
                            Describe = "审批通过",
                            CreateTime = DateTime.Now,
                            Status = 1,//状态：0:未审批；1:审批通过；2:驳回
                            LinkApproval = task.Id,
                            UserId = userId,
                            ProcessType = currentProcess.ProcessType
                        });

                        dt.IsEnableImportData = true;
                        await _dataMapService.UpdateColumn(dt, e => e.IsEnableImportData);
                    }
                    if (dto.Status == 2)//驳回
                    {
                        nextPid = await _processService.IdAdd(new process
                        {
                            Describe = dto.AttachInfo,
                            CreateTime = DateTime.Now,
                            Status = 2,//状态：0:未审批；1:审批通过；2:驳回
                            LinkApproval = task.Id,
                            UserId = userId,
                            ProcessType = currentProcess.ProcessType
                        });
                        task.Status = 2;
                    }

                    task.CurrentProcess = nextPid;
                    await _approvalTaskService.UpdateColumn(task, e => new { e.CurrentProcess, e.Status });
                }
                else if (currentProcess.ProcessType == 1 && currentProcess.Status == 0)//数据导入审批
                {
                    //每个任务新增流程审批记录
                    int nextPid = 0;

                    if (dto.Status == 1)
                    {
                        nextPid = await _processService.IdAdd(new process
                        {
                            Describe = "审批通过",
                            CreateTime = DateTime.Now,
                            Status = dto.Status,//状态：0:未审批；1:审批通过；2:驳回
                            LinkApproval = task.Id,
                            UserId = userId,
                            ProcessType = currentProcess.ProcessType
                        });
                        await _processService.Add(new process
                        {
                            Describe = "完结",
                            CreateTime = DateTime.Now,
                            Status = 3,
                            LinkApproval = task.Id,
                            UserId = userId,
                            ProcessType = 2
                        });

                        dt.IsPrivate = false;
                        dt.Status = 1;
                        await _dataMapService.UpdateColumn(dt, e => new { e.IsPrivate, e.Status });
                        task.CurrentProcess = nextPid;

                    }
                    if (dto.Status == 2)
                    {
                        nextPid = await _processService.IdAdd(new process
                        {
                            Describe = dto.AttachInfo,
                            CreateTime = DateTime.Now,
                            Status = dto.Status,//状态：0:未审批；1:审批通过；2:驳回
                            LinkApproval = task.Id,
                            UserId = userId,
                            ProcessType = currentProcess.ProcessType
                        });
                        task.CurrentProcess = nextPid;
                    }
                    task.EndTime = DateTime.Now;
                    task.Status = 2;
                    await _approvalTaskService.UpdateColumn(task, e => new { e.CurrentProcess, e.Status, e.EndTime });
                }
                //else if (currentProcess.ProcessType == 2 && currentProcess.Status == 0)
                //{
                //    //当前流程号
                //    int nextPid = 0;
                //    if (dto.Status == 1)
                //    {
                //        //分类分级审批
                //        //每个任务新增流程审批记录
                //        await _processService.Add(new process
                //        {
                //            Describe = "审批通过",
                //            CreateTime = DateTime.Now,
                //            Status = 1,//状态：0:未审批；1:审批通过；2:驳回
                //            LinkApproval = task.Id,
                //            UserId = userId,
                //            ProcessType = currentProcess.ProcessType
                //        });
                //        nextPid = await _processService.IdAdd(new process
                //        {
                //            Describe = "完结",
                //            CreateTime = DateTime.Now,
                //            Status = 3,
                //            LinkApproval = task.Id,
                //            UserId = userId,
                //            ProcessType = currentProcess.ProcessType
                //        });

                //        //修改源表状态;1.私有->公有 2.归集状态->已归集
                //        var dt = await _dataMapService.GetEntity(e => e.Id == task.AssociativeTable);
                //        dt.Status = 1;
                //        dt.IsPrivate = false;
                //        await _dataMapService.UpdateColumn(dt, e => new { e.Status, e.IsPrivate });
                //    }
                //    if (dto.Status == 2)//驳回申请
                //    {

                //        nextPid = await _processService.IdAdd(new process
                //        {
                //            Describe = dto.AttachInfo,
                //            CreateTime = DateTime.Now,
                //            Status = 2,//状态：0:未审批；1:审批通过；2:驳回
                //            LinkApproval = task.Id,
                //            UserId = userId,
                //            ProcessType = currentProcess.ProcessType
                //        });

                //    }

                //    //修改审批流程的进程号
                //    task.CurrentProcess = nextPid;
                //    task.Status = 2;
                //    await _approvalTaskService.UpdateColumn(task, e => new { e.CurrentProcess, e.Status });
                //}
                else continue;
            }

            //await Logger.AddPlatformLog($"归集一键审批{(dto.Status == 1 ? "通过" : "驳回")}", LogType.DataApproval);

            return new AjaxResult<bool>(true);
        }

        /// <summary>
        /// 分类分级管理
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<GetClassifyManageDto>>> GetClassifyManage(GetClassifyManageQueryDto dto)
        {
            //分页
            if (dto.PageIndex <= 0) dto.PageIndex = 1;
            if (dto.PageSize <= 0) dto.PageSize = 10;

            RefAsync<int> total = 0;
            string sql;
            if (dto.isAlreadyTag)
            {
                //已标记:源表的分层类型和数据类型>0
                sql = "select t.`Id` as `ApprovalId`,dt.`Id` as `Id` , dt.`Name` as `Name` , dt.`EnName` as `EnName` , dt.`associativeTable` as `associativeTable` , `org`.`Title` as `Title` , `attr`.`Attribution` as `Attribution` , `layer`.`Name` as `LayerName` , `m`.`Name` as `ModifyName` from `datamap` dt left join `cloud_user` u on dt.`UserId` = u.`id` join `layered` layer on dt.`LayeredType` = layer.`Id` join `modified` m on dt.`ModifierType` = m.`Id` left join `attribution` attr on dt.`Attribution` = `attr`.`Id` left join `cloud_org` org on u.`description` = `org`.`Id` join `approvaltask` t on dt.`Id`=t.AssociativeTable where dt.`IsEnableClassify` and dt.`LayeredType`>0 and dt.`ModifierType`>0 order by t.`StartTime` desc";
            }
            else
            {
                //未标记:源表的分层类型或数据类型<=0
                sql = "select t.`Id` as `ApprovalId`,dt.`Id` as `Id` , dt.`Name` as `Name` , dt.`EnName` as `EnName` , dt.`associativeTable` as `associativeTable` , `org`.`Title` as `Title` , `attr`.`Attribution` as `Attribution` , `layer`.`Name` as `LayerName` , `m`.`Name` as `ModifyName` from `datamap` dt left join `cloud_user` u on dt.`UserId` = u.`id` join `layered` layer on dt.`LayeredType` = layer.`Id` join `modified` m on dt.`ModifierType` = m.`Id` left join `attribution` attr on dt.`Attribution` = `attr`.`Id` left join `cloud_org` org on u.`description` = `org`.`Id` join `approvaltask` t on dt.`Id`=t.AssociativeTable where dt.`IsEnableClassify` and dt.`LayeredType` <= 0 or dt.`ModifierType`<=0 order by t.`StartTime` desc";
            }

            var result = await _dataTableService.GetEntityPageList<GetClassifyManageDto>(sql, dto.PageIndex, dto.PageSize, total);

            //await Logger.AddPlatformLog("获取分类分级管理", LogType.DataAccess);

            return new PageResult<List<GetClassifyManageDto>>(result, total, total % dto.PageSize == 0 ? total / dto.PageSize : total / dto.PageSize + 1, dto.PageSize, dto.PageIndex);
        }

        /// <summary>
        /// 分类分级管理详情
        /// </summary>
        /// <param name="dataMapId">源表Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<GetClassifyManageDetailDto>> GetClassifyManage(int dataMapId)
        {
            var dt = await _dataMapService.GetEntity(e => e.Id == dataMapId);
            if (dt == null) return new AjaxResult<GetClassifyManageDetailDto>(false, "查无此表");

            string sql = $"select t.`Id`,t.`Name`,t.`EnName`,t.`AssociativeTable`,t.`InfoSummary`,t.`Source`,t.`LinkPerson`,t.`LinkPhone`,t.`ApplicationSystemName`,`org`.`Title`,`attr`.`Attribution`, `layer`.`Name` as `LayerType`,`modify`.`Name` as `ModifyType`,fre.`Name` as `FrequencyType` from `datamap` t left join `cloud_user` `user` on t.`UserId`=`user`.`id` left join `layered` layer on t.`LayeredType` = `layer`.`Id` left join `modified` `modify` on t.`ModifierType` = `modify`.`Id` left join `attribution` attr on t.`Attribution` = `attr`.`Id` left join `cloud_org` org on `user`.`description` = `org`.`Id` left join `frequency` fre on t.`Frequency` = fre.`Id` where  t.`Id` = {dataMapId}";
            GetClassifyManageDetailDto dto = await _dataTableService.GetEntity<GetClassifyManageDetailDto>(sql);

            //await Logger.AddPlatformLog("获取分类分级管理详情", LogType.DataAccess);

            return new AjaxResult<GetClassifyManageDetailDto>(dto);
        }

        /// <summary>
        /// 分类分级编辑
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> EditClassifyManage(ClassifyManageEditDto dto)
        {
            //update
            var result = await _dataMapService.UpdateColumn(new datamap { Id = dto.DataMapId, ModifierType = dto.ModifierType, LayeredType = dto.LayeredType }, e => new { e.ModifierType, e.LayeredType });

            //await Logger.AddPlatformLog("分类分级编辑", LogType.DataOperation);

            return new AjaxResult<bool>(result > 0);
        }

        /// <summary>
        /// 分类分级管理查看/申请表信息
        /// </summary>
        /// <param name="dataMapId">源表Id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<GetClassifyManageDataItemsDto>> GetClassifyManageDetail(int dataMapId)
        {
            //关联表查询
            var dataMap = await _dataMapService.GetEntity(e => e.Id == dataMapId);
            if (dataMap is null) return new AjaxResult<GetClassifyManageDataItemsDto>(null);

            //查询源表基础信息
            GetClassifyManageDataItemsDto dto = new GetClassifyManageDataItemsDto();
            string sql = $"select t.`Id`,t.`Name`,t.`EnName`,t.`AssociativeTable`,t.`InfoSummary`,t.`Source`,t.`LinkPerson`,t.`LinkPhone`,t.`ApplicationSystemName`,`org`.`Title`,`attr`.`Attribution` from `datamap` t left join `cloud_user` `user` on t.`UserId`=`user`.`id` left join `attribution` attr on t.`Attribution` = `attr`.`Id` left join `cloud_org` org on `user`.`description` = `org`.`Id` where  t.`Id` = {dataMapId}";
            dto = await _dataTableService.GetEntity<GetClassifyManageDataItemsDto>(sql);

            //查询源表数据项
            string dicTableName = $"dic_{dataMap.AssociativeTable}";
            if (await IsTableExist(dataMap.AssociativeTable) == 0 || await IsTableExist(dicTableName) == 0) return new AjaxResult<GetClassifyManageDataItemsDto>(null, "查无此表");
            string getAllTableItemsSql = $"select Id,CnFieldName,EnFieldName,DataType,DataLength,Description FieldDescription,IsPrimaryKey,ShareType from data_sharing_main.{dicTableName}";
            dto.DataItems = await _dataTableService.GetEntityList<ResultDataItemDicDto>(getAllTableItemsSql);

            //await Logger.AddPlatformLog("分类分级管理查看/申请表信息", LogType.DataAccess);

            return new AjaxResult<GetClassifyManageDataItemsDto>(dto);
        }

        /// <summary>
        /// 获取所有分级分类标签状态
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<ResultGetClassifyManageDicDto>> GetClassifyManageDic()
        {
            ResultGetClassifyManageDicDto result = new ResultGetClassifyManageDicDto();
            result.ModifyList = await _dataTableService.GetEntityList<IdNameDto>("select t.`Id`,t.`Name` from `modified` t");
            result.LayerList = await _dataTableService.GetEntityList<IdNameDto>("select t.`Id`,t.`Name` FROM `layered` t");

            //await Logger.AddPlatformLog("获取所有分级分类标签状态", LogType.DataAccess);

            return new AjaxResult<ResultGetClassifyManageDicDto>(result);
        }

        /// <summary>
        /// 领域管理首页type:[0:数据来源 1:分层类型][update:2022年8月11日15:39:48]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<IdNameDto>>> GetDomains(GetDomainsQueryDto dto)
        {
            //分页
            if (dto.PageIndex <= 0) dto.PageIndex = 1;
            if (dto.PageSize <= 0) dto.PageSize = 10;

            string sql;
            switch (dto.TypeId)
            {
                case 0:
                    sql = "select Id,Attribution as `Name` from data_sharing_platform.`attribution` t where 1=1";
                    if (!string.IsNullOrEmpty(dto.KeyWord)) sql += $" and t.`Attribution` like '%{dto.KeyWord}%'"; break;
                case 1:
                    sql = "select Id,`Name` from data_sharing_platform.`layered` t where 1=1";
                    if (!string.IsNullOrEmpty(dto.KeyWord)) sql += $" and t.`Name` like '%{dto.KeyWord}%'"; break;
                default: return new PageResult<List<IdNameDto>>();
            }

            RefAsync<int> total = 0;
            var result = await _dataTableService.GetEntityPageList<IdNameDto>(sql, dto.PageIndex, dto.PageSize, total);

            //await Logger.AddPlatformLog($"获取领域管理首页{(dto.TypeId == 0 ? "数据来源" : "分层类型")}", LogType.DataAccess);

            return new PageResult<List<IdNameDto>>(result, total, total % dto.PageSize == 0 ? total / dto.PageSize : total / dto.PageSize + 1, dto.PageSize, dto.PageIndex);
        }

        /// <summary>
        /// 领域管理编辑type:[0:数据来源 1:分层类型][update:2022年8月11日15:39:52]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> AddDomains(AddDomainsDto dto)
        {
            switch (dto.TypeId)
            {
                case 0: await _attributionService.Add(new attribution { Attribution = dto.Name }); break;
                case 1: await _layeredService.Add(new layered { Name = dto.Name }); break;
                default: return new AjaxResult<bool>(false, "参数错误");
            }

            //await Logger.AddPlatformLog($"新增领域管理{(dto.TypeId == 0 ? "数据来源" : "分层类型")}", LogType.DataOperation);

            return new AjaxResult<bool>(true);
        }


        /// <summary>
        /// 领域管理编辑type:[0:数据来源 1:分层类型][update:2022年8月11日15:39:58]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> EditDomains(EditDomainsDto dto)
        {
            switch (dto.TypeId)
            {
                case 0: await _attributionService.UpdateColumn(new attribution { Id = dto.Id, Attribution = dto.Name }, e => e.Attribution); break;
                case 1: await _layeredService.UpdateColumn(new layered { Id = dto.Id, Name = dto.Name }, e => e.Name); break;
                default: return new AjaxResult<bool>(false, "参数错误");
            }

            //await Logger.AddPlatformLog($"修改领域管理{(dto.TypeId == 0 ? "数据来源" : "分层类型")}", LogType.DataOperation);

            return new AjaxResult<bool>(true);
        }

        /// <summary>
        /// 领域管理删除type:[0:数据来源 1:分层类型][update:2022年8月11日15:40:03]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> DelDomains(DelDomainDto dto)
        {
            switch (dto.TypeId)
            {
                case 0: await _attributionService.Delete(e => e.Id == dto.Id); break;
                case 1: await _layeredService.Delete(e => e.Id == dto.Id); break;
                default: return new AjaxResult<bool>(false, "参数错误");
            }

            //await Logger.AddPlatformLog($"删除领域管理{(dto.TypeId == 0 ? "数据来源" : "分层类型")}", LogType.DataOperation);

            return new AjaxResult<bool>(true);
        }


        /// <summary>
        /// 获取私有库列表 [update:2022年10月11日19:22:03]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<GetPrivateLibResultDto>>> GetPrivateLib(GetPrivateLibraryQueryDto dto)
        {
            //分页
            if (dto.PageIndex <= 0) dto.PageIndex = 1;
            if (dto.PageSize <= 0) dto.PageSize = 10;

            //私有库筛选
            RefAsync<int> total = 0;
            StringBuilder sql = new StringBuilder($"select dt.`Id`,dt.`Name`,a.`Attribution`,dt.`Source`,dt.`Guid`,l.`Name` as `LayeredName`,DATEDIFF(dt.ExpireTime,now()) `ExpireTime`,dt.`PatternName` from `datamap` dt join attribution a on dt.Attribution=a.`Id` join layered l on dt.LayeredType=l.`Id` where dt.IsPrivate and dt.`Status`=0 and dt.`Id` not in (select t.AssociativeTable from approvaltask t where t.IsTransferLib) ");// and dt.ExpireTime > now()

            //私有库过期时间筛选
            if (dto.LayerId.HasValue) sql.Append($" and dt.`LayeredType`={dto.LayerId}");
            if (dto.ExpireTime.HasValue)
            {
                switch (dto.ExpireTime)
                {
                    case 8: sql.Append($" and dt.ExpireTime is null"); break;
                    default: sql.Append($" and DATEDIFF(dt.ExpireTime,now())={dto.ExpireTime}"); break;
                }
            }
            else
            {
                //没有传过期时间参数->返回未过期的私有库(长期库)
                sql.Append($" and (DATEDIFF(dt.ExpireTime,now())>=0 or dt.ExpireTime is null)");
            }
            if (!string.IsNullOrEmpty(dto.KeyWord)) sql.Append($" and (dt.`Name` like '%{dto.KeyWord}%' or dt.`EnName` like '%{dto.KeyWord}%')");
            sql.Append(" order by dt.`Id` desc");//按照表的新增时间降序排序

            var result = await _dataTableService.GetEntityPageList<GetPrivateLibResultDto>(sql.ToString(), dto.PageIndex, dto.PageSize, total);

            //await Logger.AddPlatformLog("获取私有库列表", LogType.DataAccess);

            return new PageResult<List<GetPrivateLibResultDto>>(result, total, total % dto.PageSize == 0 ? total / dto.PageSize : total / dto.PageSize + 1, dto.PageSize, dto.PageIndex);

        }

        /// <summary>
        /// 私有库-转库申请[update:2022年10月11日19:23:21]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> TransferLib(TransferLibQueryDto dto)
        {
            //1.转库类型判断
            if (dto.LibType != 0 && dto.LibType != 1) return new AjaxResult<bool>(false, "Invalid value of `LibType`");
            //获取登录用户Id
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(t => t.Type == JwtRegisteredClaimNames.NameId)?.Value);

            //只有导入过数据的数据表才能转库
            var privateDataMap = await _dataMapService.GetEntity(e => e.Id == dto.DataMapId);
            if (privateDataMap is null) return new AjaxResult<bool>(false, "私有表不存在");
            if (!privateDataMap.ImportDataTime.HasValue) return new AjaxResult<bool>(false, "该表未导入数据,无法发起转库申请");
            else if (!privateDataMap.IsPrivate) return new AjaxResult<bool>(false, "无法操作公有表");//判断是否是公有表

            //2.转库
            //转私有库
            if (dto.LibType == 0)
            {
                privateDataMap.ExpireTime = dto.ExpireTime;
                await _dataMapService.UpdateColumn(privateDataMap, e => new { e.ExpireTime });
            }
            //转主题库
            if (dto.LibType == 1)
            {
                //添加流程号
                var processId = await _processService.IdAdd(new process
                {
                    Describe = "发起转库申请",
                    CreateTime = DateTime.Now,
                    Status = 0, //状态：0:未审批；1:审批通过；2:驳回
                    UserId = userId,
                    ProcessType = -1
                });
                //添加申请
                var taskId = await _approvalTaskService.IdAdd(new approvaltask
                {
                    AssociativeTable = privateDataMap.Id,
                    CurrentProcess = processId,
                    StartTime = DateTime.Now,
                    Phone = privateDataMap.LinkPhone,
                    TaskType = 1,//0:未申请 1:已申请
                    UserId = userId,
                    Status = 1,
                    IsTransferLib=true
                });
                await _processService.UpdateColumn(new process { Id = processId, LinkApproval = taskId }, e => e.LinkApproval);
            }

            //await Logger.AddPlatformLog("私有库转库申请", LogType.DataApply);

            return new AjaxResult<bool>(true);
        }

        /// <summary>
        /// 获取转库记录[update:2022年10月11日19:56:04]
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<GetTransferLibRecordsResultDto>>> GetTransferLibRecords(PageDto dto)
        {
            //分页
            if (dto.PageIndex <= 0) dto.PageIndex = 1;
            if (dto.PageSize <= 0) dto.PageSize = 10;

            //私有库筛选
            RefAsync<int> total = 0;
            string sql = "select dt.Id,t.Id ApprovalId,dt.`Name`,attr.Attribution,layer.`Name` LayerName,case t.`Status` when 1 then '等待审批' when 2 then '审批完成' end ApprovalStatus,t.StartTime from approvaltask t join datamap dt on t.AssociativeTable=dt.Id join process p on p.Id=t.CurrentProcess join `layered` layer on dt.`LayeredType` = layer.`Id` join `modified` m on dt.`ModifierType` = m.`Id` left join `attribution` attr on dt.`Attribution` = `attr`.`Id` where t.IsTransferLib";
            var result = await _dataTableService.GetEntityPageList<GetTransferLibRecordsResultDto>(sql, dto.PageIndex, dto.PageSize, total);

            return new PageResult<List<GetTransferLibRecordsResultDto>>(result, total, total % dto.PageSize == 0 ? total / dto.PageSize : total / dto.PageSize + 1, dto.PageSize, dto.PageIndex);
        }


        /// <summary>
        /// 私有库-删除[update:2022年9月7日15:34:10]
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> PrivateLibDel([FromBody] int id)
        {
            //await Logger.AddPlatformLog("私有库删除", LogType.DataApply);
            return new AjaxResult<bool>(await _dataMapService.Delete(e => e.Id == id && e.IsPrivate));
        }



        /// <summary>
        /// 获取表字段信息[update:2022年9月13日15:35:02]
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<List<GetFieldsInfoResultDto>>> GetFieldsInfo(GetFieldsInfoQueryDto dto)
        {
            List<GetFieldsInfoResultDto> result = new List<GetFieldsInfoResultDto>();
            foreach (int id in dto.Ids)
            {
                //获取公有主题数据表
                datamap dt = await _dataMapService.GetEntity(e => e.Id == id && e.Status == 1 && !e.IsPrivate);
                if (dt is null) continue;

                GetFieldsInfoResultDto temp = new GetFieldsInfoResultDto
                {
                    Id = dt.Id,
                    DataName = dt.Name,
                    Guid = dt.GUID,
                    PatternName = dt.PatternName
                };
                var fields = await _dataTableService.GetEntityList<IdNameDto>($"select Id,CnFieldName `Name` from {(dt.IsMapping is null ? "data_sharing_main" : "data_sharing_affiliated")}.dic_{dt.AssociativeTable}");
                temp.Fields = string.Join(',', fields.Select(e => e.Name));
                result.Add(temp);
                //await Logger.AddPlatformLog($"获取数据表:{id}的字段信息", LogType.DataAccess);
            }
            return new AjaxResult<List<GetFieldsInfoResultDto>>(result);
        }

        /// <summary>
        /// 源表数据分析-涉及数据单位总数[update:2022年9月16日10:42:17]
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<DataUnitDto>>> GetDataRelationUnit(PageDto dto)
        {
            if (dto.PageIndex<=0) dto.PageIndex=1;
            if (dto.PageSize<=0) dto.PageSize=10;

            RefAsync<int> total = 0;
            string itemSql = "select attr.Id,attr.Attribution,count(1) `Count` from `datamap` dt join `attribution` attr on dt.Attribution=attr.Id group by dt.Attribution";
            var result = await _dataTableService.GetEntityPageList<DataUnitDto>(itemSql, dto.PageIndex, dto.PageSize, total);
            //await Logger.AddPlatformLog("获取涉及数据单位总数", LogType.DataAccess);
            return new PageResult<List<DataUnitDto>>(result, total, total % dto.PageSize == 0 ? total / dto.PageSize : total / dto.PageSize + 1, dto.PageSize, dto.PageIndex);

        }

        /// <summary>
        /// 源表数据分析-数据表总数[update:2022年9月16日10:42:17]
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<GetDataMapTotalCountResultDto>>> GetDataMapTotalCount(PageDto dto)
        {
            if (dto.PageIndex<=0) dto.PageIndex=1;
            if (dto.PageSize<=0) dto.PageSize=10;

            RefAsync<int> total = 0;
            string itemSql = "select dt.Id,dt.`Name`,dt.CreateTime from `datamap` dt";
            var result = await _dataTableService.GetEntityPageList<GetDataMapTotalCountResultDto>(itemSql, dto.PageIndex, dto.PageSize, total);
            //await Logger.AddPlatformLog("获取数据表总数", LogType.DataAccess);
            return new PageResult<List<GetDataMapTotalCountResultDto>>(result, total, total % dto.PageSize == 0 ? total / dto.PageSize : total / dto.PageSize + 1, dto.PageSize, dto.PageIndex);

        }

        /// <summary>
        /// 源表数据分析-数据使用情况[update:2022年9月16日10:42:17]
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<GetUsedDataInfoResultDto>> GetUsedDataInfo()
        {
            GetUsedDataInfoResultDto result = new GetUsedDataInfoResultDto();
            string inSql = "select * from(select date_sub(curdate(),interval 7 day) as Time union select date_sub( curdate(), interval 6 day) as Time union select date_sub(curdate(), interval 5 day) as Time union select date_sub(curdate(), interval 4 day) as Time union select date_sub(curdate(), interval 3 day) as Time union select date_sub(curdate(), interval 2 day) as Time union select date_sub(curdate(), interval 1 day) as Time from dual) t1 left join (select sum(AffectedRows) DataCount,Type,date(dt.CreateTime) Time from `datatrend` dt where dt.CreateTime between date_add(date(now()),interval -7 day) and current_date and Type=0 group by Time) t2 on t1.Time=t2.Time order by t1.Time";
            result.InItems=await _dataTableService.GetEntityList<UsedDataInfoDto>(inSql);
            string outSql = "select * from(select date_sub(curdate(),interval 7 day) as Time union select date_sub( curdate(), interval 6 day) as Time union select date_sub(curdate(), interval 5 day) as Time union select date_sub(curdate(), interval 4 day) as Time union select date_sub(curdate(), interval 3 day) as Time union select date_sub(curdate(), interval 2 day) as Time union select date_sub(curdate(), interval 1 day) as Time from dual) t1 left join (select sum(AffectedRows) DataCount,Type,date(dt.CreateTime) Time from `datatrend` dt where dt.CreateTime between date_add(date(now()),interval -7 day) and current_date and Type=1 group by Time) t2 on t1.Time=t2.Time order by t1.Time";
            result.OutItems= await _dataTableService.GetEntityList<UsedDataInfoDto>(outSql);
            //await Logger.AddPlatformLog("获取数据使用情况", LogType.DataAccess);
            return new AjaxResult<GetUsedDataInfoResultDto>(result);
        }

        /// <summary>
        /// 判断表是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="databaseName">数据库名称</param>
        /// <returns>1:存在;0:不存在</returns>
        private async Task<int> IsTableExist(string tableName, string databaseName = "data_sharing_main")
        {
            return Convert.ToInt32(await _dataTableService.QueryCount($"select if(exists(select TABLE_NAME from information_schema.TABLES where TABLE_SCHEMA='{databaseName}' and TABLE_NAME = '{tableName}'),1,0) isTableExist;"));
        }
    }
}
