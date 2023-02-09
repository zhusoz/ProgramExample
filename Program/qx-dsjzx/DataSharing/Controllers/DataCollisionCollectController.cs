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
using ProgramsNetCore.Models.Dto.DataCollectDto;
using ProgramsNetCore.Models.Dto.DataCollisionDto;
using ProgramsNetCore.Models.Dto.DataManageDto;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DataSharing.Controllers
{
    /// <summary>
    /// 数据碰撞
    /// </summary>
    [Route("")]
    [ApiController]
    public class DataCollisionCollectController : ControllerBase
    {
        private readonly IDataTableService _dataTableService;
        private readonly IDataModelService _dataModelService;
        private readonly IDataModelPublicSheetService _dataModelPublicSheetService;
        private readonly IDataModelPrivateSheetService _dataModelPrivateSheetService;
        private readonly IDataTaskService _dataTaskService;
        private readonly ITableToTableService _tableToTableService;
        private readonly IDataConditionsService _dataConditionsService;
        private readonly IDataMapService _dataMapService;
        private readonly IDataModelExamineLogService _dataModelExamineLogService;
        private readonly ITopologyService _topologyService;
        private readonly IMapper _mapper;



        public DataCollisionCollectController(IDataTableService dataTableService, IDataModelService dataModelService, IDataModelPublicSheetService dataModelPublicSheetService, IDataModelPrivateSheetService dataModelPrivateSheetService, IDataTaskService dataTaskService, ITableToTableService tableToTableService, IDataConditionsService dataConditionsService, IDataMapService dataMapService, IDataModelExamineLogService dataModelExamineLogService, ITopologyService topologyService, IMapper mapper)
        {
            _dataTableService = dataTableService;
            _dataModelService = dataModelService;
            _dataModelPublicSheetService = dataModelPublicSheetService;
            _dataModelPrivateSheetService = dataModelPrivateSheetService;
            _dataTaskService = dataTaskService;
            _tableToTableService = tableToTableService;
            _dataConditionsService = dataConditionsService;
            _dataMapService = dataMapService;
            _dataModelExamineLogService = dataModelExamineLogService;
            _topologyService = topologyService;
            _mapper = mapper;
        }

        #region 模型

        /// <summary>
        /// 获取单个模型
        /// </summary>
        /// <param name="dto">ModelId</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<DataModelDto>> GetModel(DataCollisionQueryDto dto)
        {
            DataModelDto model = _mapper.Map<DataModelDto>(await _dataModelService.GetEntity(c => c.Id==dto.ModelId));
            await Logger.AddPlatformLog("获取模型", LogType.DataAccess);

            return new AjaxResult<DataModelDto>(model);
        }
        /// <summary>
        /// 获取所有模型List
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<List<DataModelDto>>> GetModelList()
        {
            List<DataModelDto> list = _mapper.Map<List<DataModelDto>>(await _dataModelService.GetEntitys(c => c.Deleted == 0));
            foreach (DataModelDto item in list)
            {
                List<datamodel_privatesheet> privateList = await _dataModelPrivateSheetService.GetEntitys(c => c.ModelId == item.Id && c.Type == 1);
                List<string> prisheets = privateList.Select(c => c.SheetId).Distinct().ToList();
                var a = await _dataMapService.GetEntitys(c => 1 == 1);
                item.PrivateTables = _mapper.Map<List<DataMapCollectDto>>(a.Where(c => prisheets.Exists(x => x == c.GUID)).ToList());
                item.PrivateSheets = item.PrivateSheets ?? new List<SheetDto>();

                foreach (string sheetId in prisheets)
                {
                    SheetDto sheet = new SheetDto();
                    sheet.TableName = privateList.FirstOrDefault(c => c.SheetId == sheetId).SheetName;
                    sheet.FieldNames = String.Join(",", privateList.Where(c => c.SheetId == sheetId).Select(c => c.SheetField).ToArray());
                    sheet.Fields = privateList.Where(c => c.SheetId == sheetId).Select(c => c.SheetField).ToList();
                    item.PrivateSheets.Add(sheet);
                }

                List<datamodel_publicsheet> publicList = await _dataModelPublicSheetService.GetEntitys(c => c.ModelId == item.Id && c.Type == 1);
                List<string> pubsheets = publicList.Select(c => c.SheetId).Distinct().ToList();
                item.PublicTables = _mapper.Map<List<DataMapCollectDto>>(a.Where(c => pubsheets.Exists(x => x == c.GUID)));
                item.PublicSheets = item.PublicSheets ?? new List<SheetDto>();

                foreach (string sheetId in pubsheets)
                {
                    SheetDto sheet = new SheetDto();
                    sheet.TableName = publicList.FirstOrDefault(c => c.SheetId == sheetId).SheetName;
                    sheet.FieldNames = String.Join(",", publicList.Where(c => c.SheetId == sheetId).Select(c => c.SheetField).ToArray());
                    sheet.Fields = publicList.Where(c => c.SheetId == sheetId).Select(c => c.SheetField).ToList();
                    item.PublicSheets.Add(sheet);
                }
            }
            await Logger.AddPlatformLog("获取模型目录", LogType.DataAccess);

            return new AjaxResult<List<DataModelDto>>(list);
        }

        /// <summary>
        /// 获取模型目录分页
        /// </summary>
        /// <param name="dto">Tissue,Region,Name,ExtensionStatus,Status,Deleted,Approver</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<DataModelDto>>> GetModelPageList(DataCollisionQueryDto dto)
        {
            //分页参数
            if (dto.PageIndex <= 0)
            {
                dto.PageIndex = 1;
            }
            if (dto.PageSize <= 0)
            {
                dto.PageSize = 10;
            }
            RefAsync<int> total = 0;

            string sql = "select * from datamodel where Deleted=0";
            if (!string.IsNullOrWhiteSpace(dto.Tissue))
            {
                sql += $" and Tissue = '{dto.Tissue}'";
            }
            if (!string.IsNullOrWhiteSpace(dto.Region))
            {
                sql += $" and Region = '{dto.Region}'";
            }
            if (!string.IsNullOrWhiteSpace(dto.Name))
            {
                sql += $" and INSTR(Name,'{dto.Name}')>0 ";
            }
            if (dto.ExtensionStatus >= 0)
            {
                sql += $" and ExtensionStatus = {dto.ExtensionStatus}";
            }
            if (!string.IsNullOrWhiteSpace(dto.Status))
            {
                sql += $" and Status in ({dto.Status})";
            }
            if (dto.Deleted >= 0)
            {
                sql += $" and Deleted = {dto.Deleted}";
            }
            if (!string.IsNullOrWhiteSpace(dto.Approver))
            {
                sql += $" and Approver = '{dto.Approver}'";
            }

            sql += " order by Id desc";

            List<DataModelDto> list = await _dataTableService.GetEntityPageList<DataModelDto>(sql, dto.PageIndex, dto.PageSize, total);
            foreach (DataModelDto item in list)
            {
                List<datamodel_privatesheet> privateList = await _dataModelPrivateSheetService.GetEntitys(c => c.ModelId == item.Id && c.Type == 1);
                List<string> prisheets = privateList.Select(c => c.SheetId).Distinct().ToList();
                var a = await _dataMapService.GetEntitys(c => 1 == 1);
                item.PrivateTables = _mapper.Map<List<DataMapCollectDto>>(a.Where(c => prisheets.Exists(x => x == c.GUID)).ToList());
                item.PrivateSheets = item.PrivateSheets ?? new List<SheetDto>();

                foreach (string sheetId in prisheets)
                {
                    SheetDto sheet = new SheetDto();
                    sheet.TableName = privateList.FirstOrDefault(c => c.SheetId == sheetId).SheetName;
                    sheet.FieldNames = String.Join(",", privateList.Where(c => c.SheetId == sheetId).Select(c => c.SheetField).ToArray());
                    sheet.Fields = privateList.Where(c => c.SheetId == sheetId).Select(c => c.SheetField).ToList();
                    item.PrivateSheets.Add(sheet);
                }

                List<datamodel_publicsheet> publicList = await _dataModelPublicSheetService.GetEntitys(c => c.ModelId == item.Id && c.Type == 1);
                List<string> pubsheets = publicList.Select(c => c.SheetId).Distinct().ToList();
                item.PublicTables = _mapper.Map<List<DataMapCollectDto>>(a.Where(c => pubsheets.Exists(x => x == c.GUID)));
                item.PublicSheets = item.PublicSheets ?? new List<SheetDto>();

                foreach (string sheetId in pubsheets)
                {
                    SheetDto sheet = new SheetDto();
                    sheet.TableName = publicList.FirstOrDefault(c => c.SheetId == sheetId).SheetName;
                    sheet.FieldNames = String.Join(",", publicList.Where(c => c.SheetId == sheetId).Select(c => c.SheetField).ToArray());
                    sheet.Fields = publicList.Where(c => c.SheetId == sheetId).Select(c => c.SheetField).ToList();
                    item.PublicSheets.Add(sheet);
                }
            }

            await Logger.AddPlatformLog("获取模型目录分页", LogType.DataAccess);

            return new PageResult<List<DataModelDto>>(list, total, total % dto.PageSize == 0 ? total / dto.PageSize : total / dto.PageSize + 1, dto.PageSize, dto.PageIndex);
        }

        /// <summary>
        /// 新增.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> AddModel(DataModelDto dto)
        {
            datamodel model = _mapper.Map<datamodel>(dto);

            try
            {
                model = await _dataModelService.ValueAdd(model);
                foreach (var item in dto.DataModelPrivateSheetDtos)
                {
                    datamodel_privatesheet privatesheet = _mapper.Map<datamodel_privatesheet>(item);
                    privatesheet.ModelId = model.Id;
                    privatesheet.CreateTime = DateTime.Now;

                    await _dataModelPrivateSheetService.Add(privatesheet);
                }
                foreach (var item in dto.DataModelPublicSheetDtos)
                {
                    datamodel_publicsheet publicSheet = _mapper.Map<datamodel_publicsheet>(item);
                    publicSheet.ModelId = model.Id;
                    publicSheet.CreateTime = DateTime.Now;

                    await _dataModelPublicSheetService.Add(publicSheet);
                }
                await Logger.AddPlatformLog($"模型目录新增", LogType.DataOperation);
                return new AjaxResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new AjaxResult<bool>(false, ex.Message);
            }
        }

        /// <summary>
        /// 只更新模型，删除、审核可用
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> UpdateModel(DataModelDto dto)
        {
            datamodel model = _mapper.Map<datamodel>(dto);
            try
            {
                await _dataModelService.Update(model);
                await Logger.AddPlatformLog($"模型更新", LogType.DataOperation);

                return new AjaxResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new AjaxResult<bool>(false, ex.Message);
            }
        }

        /// <summary>
        /// 更新.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> UpdateModelWithSheet(DataModelDto dto)
        {
            datamodel model = _mapper.Map<datamodel>(dto);
            try
            {
                await _dataModelService.Update(model);

                // 先清空 再重新加 => 后续代码可以升级为交集 并集 差集进行增删改
                await _dataModelPrivateSheetService.Delete(c => c.ModelId == model.Id && c.Type == 1);
                foreach (var item in dto.DataModelPrivateSheetDtos)
                {
                    datamodel_privatesheet privatesheet = _mapper.Map<datamodel_privatesheet>(item);
                    privatesheet.ModelId = model.Id;
                    await _dataModelPrivateSheetService.Add(privatesheet);
                }

                await _dataModelPublicSheetService.Delete(c => c.ModelId == model.Id && c.Type == 1);
                foreach (var item in dto.DataModelPublicSheetDtos)
                {
                    datamodel_publicsheet publicSheet = _mapper.Map<datamodel_publicsheet>(item);
                    publicSheet.ModelId = model.Id;
                    await _dataModelPublicSheetService.Add(publicSheet);
                }
                await Logger.AddPlatformLog($"模型、库更新", LogType.DataOperation);

                return new AjaxResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new AjaxResult<bool>(false, ex.Message);
            }
        }


        #endregion

        #region 任务
        /// <summary>
        /// 获取单个任务
        /// </summary>
        /// <param name="dto">ModelId</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<DataTaskDto>> GetTask(DataCollisionQueryDto dto)
        {
            DataTaskDto model = _mapper.Map<DataTaskDto>(await _dataTaskService.GetEntity(c => c.Id == dto.ModelId));
            await Logger.AddPlatformLog($"获取单个任务", LogType.DataOperation);

            return new AjaxResult<DataTaskDto>(model);
        }
        /// <summary>
        /// 获取所有任务List
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<List<DataTaskDto>>> GetTaskList()
        {
            List<DataTaskDto> list = _mapper.Map<List<DataTaskDto>>(await _dataTaskService.GetEntitys(c => c.Deleted == 0));
            foreach (DataTaskDto item in list)
            {
                List<datamodel_privatesheet> privateList = await _dataModelPrivateSheetService.GetEntitys(c => c.ModelId == item.Id && c.Type == 2);
                List<string> prisheets = privateList.Select(c => c.SheetId).Distinct().ToList();
                var a = await _dataMapService.GetEntitys(c => 1 == 1);
                item.PrivateTables = _mapper.Map<List<DataMapCollectDto>>(a.Where(c => prisheets.Exists(x => x == c.GUID)).ToList());
                item.PrivateSheets = item.PrivateSheets ?? new List<SheetDto>();

                foreach (string sheetId in prisheets)
                {
                    SheetDto sheet = new SheetDto();
                    sheet.TableName = privateList.FirstOrDefault(c => c.SheetId == sheetId).SheetName;
                    sheet.FieldNames = String.Join(",", privateList.Where(c => c.SheetId == sheetId).Select(c => c.SheetField).ToArray());
                    sheet.Fields = privateList.Where(c => c.SheetId == sheetId).Select(c => c.SheetField).ToList();
                    item.PrivateSheets.Add(sheet);
                }

                List<datamodel_publicsheet> publicList = await _dataModelPublicSheetService.GetEntitys(c => c.ModelId == item.Id && c.Type == 2);
                List<string> pubsheets = publicList.Select(c => c.SheetId).Distinct().ToList();
                item.PublicTables = _mapper.Map<List<DataMapCollectDto>>(a.Where(c => pubsheets.Exists(x => x == c.GUID)));
                item.PublicSheets = item.PublicSheets ?? new List<SheetDto>();

                foreach (string sheetId in pubsheets)
                {
                    SheetDto sheet = new SheetDto();
                    sheet.TableName = publicList.FirstOrDefault(c => c.SheetId == sheetId).SheetName;
                    sheet.FieldNames = String.Join(",", publicList.Where(c => c.SheetId == sheetId).Select(c => c.SheetField).ToArray());
                    sheet.Fields = publicList.Where(c => c.SheetId == sheetId).Select(c => c.SheetField).ToList();
                    item.PublicSheets.Add(sheet);
                }
            }
            await Logger.AddPlatformLog($"获取任务list", LogType.DataOperation);

            return new AjaxResult<List<DataTaskDto>>(list);
        }

        /// <summary>
        /// 获取任务目录分页
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<DataTaskDto>>> GetTaskPageList(DataCollisionQueryDto dto)
        {
            //分页参数
            if (dto.PageIndex <= 0)
            {
                dto.PageIndex = 1;
            }
            if (dto.PageSize <= 0)
            {
                dto.PageSize = 10;
            }
            RefAsync<int> total = 0;

            string sql = "select * from datatask where Deleted=0 order by Id desc";
            //if (!string.IsNullOrWhiteSpace(dto.Tissue))
            //{
            //    sql += $" and Tissue = '{dto.Tissue}'";
            //}
            //if (!string.IsNullOrWhiteSpace(dto.Region))
            //{
            //    sql += $" and Region = '{dto.Region}'";
            //}
            //if (!string.IsNullOrWhiteSpace(dto.Name))
            //{
            //    sql += $" and INSTR(Name,'{dto.Name}')>0 ";
            //}
            //if (dto.ExtensionStatus >= 0)
            //{
            //    sql += $" and ExtensionStatus = {dto.ExtensionStatus}";
            //}


            List<DataTaskDto> list = await _dataTableService.GetEntityPageList<DataTaskDto>(sql, dto.PageIndex, dto.PageSize, total);
            foreach (DataTaskDto item in list)
            {
                List<datamodel_privatesheet> privateList = await _dataModelPrivateSheetService.GetEntitys(c => c.ModelId == item.Id && c.Type == 2);
                List<string> prisheets = privateList.Select(c => c.SheetId).Distinct().ToList();
                var a = await _dataMapService.GetEntitys(c => 1 == 1);
                item.PrivateTables = _mapper.Map<List<DataMapCollectDto>>(a.Where(c => prisheets.Exists(x => x == c.GUID)).ToList());
                item.PrivateSheets = item.PrivateSheets ?? new List<SheetDto>();
                foreach (string sheetId in prisheets)
                {
                    SheetDto sheet = new SheetDto();
                    sheet.TableName = privateList.FirstOrDefault(c => c.SheetId == sheetId).SheetName;
                    sheet.FieldNames = String.Join(",", privateList.Where(c => c.SheetId == sheetId).Select(c => c.SheetField).ToArray());
                    sheet.Fields = privateList.Where(c => c.SheetId == sheetId).Select(c => c.SheetField).ToList();
                    item.PrivateSheets.Add(sheet);
                }

                List<datamodel_publicsheet> publicList = await _dataModelPublicSheetService.GetEntitys(c => c.ModelId == item.Id && c.Type == 2);
                List<string> pubsheets = publicList.Select(c => c.SheetId).Distinct().ToList();
                item.PublicTables = _mapper.Map<List<DataMapCollectDto>>(a.Where(c => pubsheets.Exists(x => x == c.GUID)));
                item.PublicSheets = item.PublicSheets ?? new List<SheetDto>();
                foreach (string sheetId in pubsheets)
                {
                    SheetDto sheet = new SheetDto();
                    sheet.TableName = publicList.FirstOrDefault(c => c.SheetId == sheetId).SheetName;
                    sheet.FieldNames = String.Join(",", publicList.Where(c => c.SheetId == sheetId).Select(c => c.SheetField).ToArray());
                    sheet.Fields = publicList.Where(c => c.SheetId == sheetId).Select(c => c.SheetField).ToList();
                    item.PublicSheets.Add(sheet);
                }
            }

            await Logger.AddPlatformLog("获取任务分页", LogType.DataAccess);

            return new PageResult<List<DataTaskDto>>(list, total, total % dto.PageSize == 0 ? total / dto.PageSize : total / dto.PageSize + 1, dto.PageSize, dto.PageIndex);
        }

        /// <summary>
        /// 新增.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> AddTask(DataTaskDto dto)
        {
            datatask model = _mapper.Map<datatask>(dto);

            try
            {
                model = await _dataTaskService.ValueAdd(model);
                foreach (var item in dto.DataModelPrivateSheetDtos)
                {
                    datamodel_privatesheet privatesheet = _mapper.Map<datamodel_privatesheet>(item);
                    privatesheet.ModelId = model.Id;
                    privatesheet.CreateTime = DateTime.Now;
                    await _dataModelPrivateSheetService.Add(privatesheet);
                }
                foreach (var item in dto.DataModelPublicSheetDtos)
                {
                    datamodel_publicsheet publicSheet = _mapper.Map<datamodel_publicsheet>(item);
                    publicSheet.ModelId = model.Id;
                    publicSheet.CreateTime = DateTime.Now;

                    await _dataModelPublicSheetService.Add(publicSheet);
                }
                await Logger.AddPlatformLog($"任务新增", LogType.DataOperation);
                return new AjaxResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new AjaxResult<bool>(false, ex.Message);
            }
        }

        /// <summary>
        /// 只更新任务，删除、审核可用
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> UpdateTask(DataTaskDto dto)
        {
            datatask model = _mapper.Map<datatask>(dto);
            try
            {
                await _dataTaskService.Update(model);
                await Logger.AddPlatformLog($"任务更新", LogType.DataOperation);

                return new AjaxResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new AjaxResult<bool>(false, ex.Message);
            }
        }

        /// <summary>
        /// 更新.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> UpdateTaskWithSheet(DataTaskDto dto)
        {
            datatask model = _mapper.Map<datatask>(dto);
            try
            {
                await _dataTaskService.Update(model);

                // 先清空 再重新加 => 后续代码可以升级为交集 并集 差集进行增删改
                await _dataModelPrivateSheetService.Delete(c => c.ModelId == model.Id && c.Type == 2);
                foreach (var item in dto.DataModelPrivateSheetDtos)
                {
                    datamodel_privatesheet privatesheet = _mapper.Map<datamodel_privatesheet>(item);
                    privatesheet.ModelId = model.Id;
                    await _dataModelPrivateSheetService.Add(privatesheet);
                }

                await _dataModelPublicSheetService.Delete(c => c.ModelId == model.Id && c.Type == 2);
                foreach (var item in dto.DataModelPublicSheetDtos)
                {
                    datamodel_publicsheet publicSheet = _mapper.Map<datamodel_publicsheet>(item);
                    publicSheet.ModelId = model.Id;
                    await _dataModelPublicSheetService.Add(publicSheet);
                }
                await Logger.AddPlatformLog($"任务、库更新", LogType.DataOperation);

                return new AjaxResult<bool>(true);

            }
            catch (Exception ex)
            {
                return new AjaxResult<bool>(false, ex.Message);
            }
        }

        /// <summary>
        /// 获取数据库的表结构
        /// </summary>
        /// <param name="dto">TableName,PatternName</param>
        /// <returns></returns>
        [HttpPost]
        public AjaxResult<List<TableDto>> GetTableInfo(DataCollisionQueryDto dto)
        {
            List<TableDto> list = GetTableFields(dto.TableName, dto.PatternName).Result;


            return new AjaxResult<List<TableDto>>(list);
        }

        private async Task<List<TableDto>> GetTableFields(string tableName,string patternName,bool isIn=false)
        {
            string sql = $@"SELECT
    TB.TABLE_SCHEMA,    -- 模式
    TB.TABLE_NAME,      -- 表名
    TB.TABLE_NAME as GUID,      -- 表名
    TB.TABLE_COMMENT,   -- 表名注释
    COL.COLUMN_NAME,    -- 字段名
    COL.COLUMN_NAME as Name,    -- 字段名
    COL.COLUMN_TYPE,    -- 字段类型
    COL.COLUMN_COMMENT,  -- 字段注释
    3 as Level,
		COL.DATA_TYPE        -- 字段数据类型
FROM
    INFORMATION_SCHEMA.TABLES TB,
    INFORMATION_SCHEMA.COLUMNS COL
Where TB.TABLE_SCHEMA ='{patternName}' -- 库名
AND TB.TABLE_NAME = COL.TABLE_NAME";
            if (!string.IsNullOrWhiteSpace(tableName))
            {
                if(isIn)
                {
                    sql += $" AND TB.TABLE_NAME in ({tableName})";
                }
                else
                {
                    sql += $" AND TB.TABLE_NAME = '{tableName}'";
                }
            }

            List<TableDto> list = await _dataTableService.GetEntityList<TableDto>(sql);
            await Logger.AddPlatformLog($"获取表结构", LogType.DataOperation);
            return list;
        }

        #endregion

        #region 过滤条件
        /// <summary>
        /// 获取所有过滤条件List
        /// </summary>
        /// <returns>Type,ModelId,TableName</returns>
        [HttpPost]
        public async Task<AjaxResult<List<DataConditionDto>>> GetConditionsList(DataCollisionQueryDto dto)
        {
            List<DataConditionDto> list = _mapper.Map<List<DataConditionDto>>(await _dataConditionsService.GetEntitys(c => c.Type == dto.Type && c.ModelId == dto.ModelId));
            if (!string.IsNullOrWhiteSpace(dto.TableName))
            {
                list = list.Where(c => c.TableName == dto.TableName).ToList();
            }
            await Logger.AddPlatformLog("获取所有过滤条件List", LogType.DataAccess);

            return new AjaxResult<List<DataConditionDto>>(list);
        }

        /// <summary>
        /// 保存筛选条件
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> SavaConditions(List<DataConditionDto> list)
        {
            if (list.Count <= 0)
            {
                return new AjaxResult<bool>(false);
            }
            else
            {
                // 清空原来所有条件
                await _dataConditionsService.Delete(c => c.ModelId == list[0].ModelId && c.Type == list[0].Type && c.TableName == list[0].TableName);
                foreach (var item in list)
                {
                    dataconditions model = new dataconditions();
                    model.ModelId = item.ModelId;
                    model.Type = item.Type;
                    model.Condition = item.Condition;
                    model.FieldName = item.FieldName;
                    model.FieldType = item.FieldType;
                    model.FieldValue = item.FieldValue;
                    model.TableName = item.TableName;
                    await _dataConditionsService.Add(model);
                }
                await Logger.AddPlatformLog("保存过滤条件", LogType.DataAccess);
                return new AjaxResult<bool>(true);
            }
        }
        #endregion

        #region 表与表之间的关联字段
        /// <summary>
        /// 获取某张表的所有关系
        /// </summary>
        /// <param name="dto">TableNameA</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<List<TableToTableDto>>> GetTableRelationships(DataCollisionQueryDto dto)
        {
            var list = new List<tabletotable>();
            list = await _tableToTableService.GetEntitys(c => c.TableNameA == dto.TableNameA || c.TableNameB == dto.TableNameA);

            return new AjaxResult<List<TableToTableDto>>(_mapper.Map<List<TableToTableDto>>(list));
        }


        /// <summary>
        /// 获取两张表之间的关联
        /// </summary>
        /// <param name="dto">TableNameA，TableNameB</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<TableToTableDto>> GetTableRelationship(DataCollisionQueryDto dto)
        {
            if (!string.IsNullOrWhiteSpace(dto.TableNameA) && !string.IsNullOrWhiteSpace(dto.TableNameB))
            {
                TableToTableDto model = _mapper.Map<TableToTableDto>(await _tableToTableService.GetEntity(c => (c.TableNameA == dto.TableNameA && c.TableNameB == dto.TableNameB) || (c.TableNameA == dto.TableNameB && c.TableNameB == dto.TableNameA)));

                await Logger.AddPlatformLog("获取两张表之间的关联", LogType.DataAccess);

                return new AjaxResult<TableToTableDto>(model);
            }
            else
            {
                return new AjaxResult<TableToTableDto>(new TableToTableDto());
            }
        }

        /// <summary>
        /// 保存两张表之间的关系
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> AddTableRelationship(TableToTableDto dto)
        {
            if (await _tableToTableService.Add(_mapper.Map<tabletotable>(dto)))
            {
                await Logger.AddPlatformLog("新增两张保存表之间的关联", LogType.DataAccess);

                return new AjaxResult<bool> { Success = true };
            }
            else
            {
                return new AjaxResult<bool> { Success = false };
            }
        }

        /// <summary>
        /// 更新两张表之间的关联
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> UpdateTableRelationship(TableToTableDto dto)
        {
            if (await _tableToTableService.Update(_mapper.Map<tabletotable>(dto)))
            {
                await Logger.AddPlatformLog("更新两张保存表之间的关联", LogType.DataAccess);

                return new AjaxResult<bool> { Success = true };
            }
            else
            {
                return new AjaxResult<bool> { Success = false };
            }
        }

        /// <summary>
        /// 删除两张表之间的关联
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> DeleteTableRelationship(TableToTableDto dto)
        {
            if (await _tableToTableService.Delete(_mapper.Map<tabletotable>(dto)))
            {
                await Logger.AddPlatformLog("删除两张保存表之间的关联", LogType.DataAccess);

                return new AjaxResult<bool> { Success = true };
            }
            else
            {
                return new AjaxResult<bool> { Success = false };
            }
        }
        #endregion

        #region 库
        /// <summary>
        /// 获取库以及表结构
        /// </summary>
        /// <param name="dto">PublicOrPrivate,Type,ModelId</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<List<DataMapWithLayeredDto>>> GetSheetWithFields(DataCollisionQueryDto dto)
        {
            string tablename = dto.PublicOrPrivate == 1 ? "datamodel_publicsheet" : "datamodel_privatesheet";
            string sql = $"select b.*,c.`Name` as LayeredTypeStr,2 as Level  from {tablename} a LEFT JOIN datamap b on a.SheetId = b.Id left join layered c on b.LayeredType = c.Id where a.type = {dto.Type} and a.ModelId={dto.ModelId}";
            DataTable dt = await _dataTableService.GetDataTable(sql);

            List<datamodel_privatesheet> datamodel_Privatesheets = await _dataModelPrivateSheetService.GetEntitys(c => c.ModelId == dto.ModelId && c.Type == dto.Type);
            List<datamodel_publicsheet> datamodel_Publicsheets = await _dataModelPublicSheetService.GetEntitys(c => c.ModelId == dto.ModelId && c.Type == dto.Type);

            List<DataMapWithFieldsDto> list = DtConversionList<DataMapWithFieldsDto>.ConvertToModel(dt);
            list = list.Distinct(new Compare<DataMapWithFieldsDto>((x, y) => x != null && y != null && x.GUID.Equals(y.GUID))).ToList();
            foreach (var item in list)
            {
                if (dto.PublicOrPrivate == 1)
                {
                    item.List = GetTableFields(item.GUID, item.PatternName).Result.Where(c => datamodel_Publicsheets.Exists(x => x.SheetField == c.Name && x.SheetName == c.GUID)).ToList();
                }
                else
                {
                    item.List = GetTableFields(item.GUID, item.PatternName).Result.Where(c => datamodel_Privatesheets.Exists(x => x.SheetField == c.Name && x.SheetName == c.GUID)).ToList();
                }
            }
            List<string> tableType = list.Select(c => c.LayeredTypeStr).Distinct().ToList();
            List<DataMapWithLayeredDto> result = new List<DataMapWithLayeredDto>();
            foreach (var item in tableType)
            {
                DataMapWithLayeredDto model = new DataMapWithLayeredDto();
                model.Name = item;
                model.List = list.Where(c => c.LayeredTypeStr == item).ToList();
                model.GUID = Guid.NewGuid().ToString();
                model.Level = 1;
                result.Add(model);
            }
            await Logger.AddPlatformLog($"获取库以及库类型以及表结构", LogType.DataOperation);
            return new AjaxResult<List<DataMapWithLayeredDto>>(result);
        }

        /// <summary>
        /// 获取全部库以及表结构
        /// </summary>
        /// <param name="dto">PublicOrPrivate,Type,ModelId</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<List<DataMapWithLayeredDto>>> GetAllSheetWithFields(DataCollisionQueryDto dto)
        {
            string tablename = dto.PublicOrPrivate == 1 ? "datamodel_publicsheet" : "datamodel_privatesheet";
            string sql = $"select b.*,c.`Name` as LayeredTypeStr  from  datamap b  left join layered c on b.LayeredType = c.Id where b.IsPrivate = {(dto.PublicOrPrivate == 2 ? 1 : 0)} ";
            DataTable dt = await _dataTableService.GetDataTable(sql);

            List<DataMapWithFieldsDto> list = DtConversionList<DataMapWithFieldsDto>.ConvertToModel(dt);
            foreach (var item in list)
            {
                if (dto.PublicOrPrivate == 1)
                {
                    item.List = GetTableFields(item.GUID, item.PatternName).Result;
                }
                else
                {
                    item.List = GetTableFields(item.GUID, item.PatternName).Result;
                }
            }
            List<string> tableType = list.Select(c => c.LayeredTypeStr).Distinct().ToList();
            List<DataMapWithLayeredDto> result = new List<DataMapWithLayeredDto>();
            foreach (var item in tableType)
            {
                DataMapWithLayeredDto model = new DataMapWithLayeredDto();
                model.Name = item;
                model.List = list.Where(c => c.LayeredTypeStr == item).ToList();
                model.GUID = Guid.NewGuid().ToString();
                result.Add(model);
            }
            await Logger.AddPlatformLog($"获取全部库以及库类型以及表结构", LogType.DataOperation);
            return new AjaxResult<List<DataMapWithLayeredDto>>(result);
        }
        #endregion

        #region 拓扑图
        /// <summary>
        /// 获取某个模型或者任务的所有拓扑图节点
        /// </summary>
        /// <param name="dto">Type，ModelId</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<List<TopologyDto>>> GetTopologyList(DataCollisionQueryDto dto)
        {
            List<TopologyDto> list = _mapper.Map<List<TopologyDto>>(await _topologyService.GetEntitys(c => c.Type == dto.Type && c.ModelId == dto.ModelId));


            await Logger.AddPlatformLog($"获取某个模型或者任务的所有拓扑图节点", LogType.DataOperation);
            return new AjaxResult<List<TopologyDto>>(list);
        }

        /// <summary>
        /// 保存某个模型或者任务的所有拓扑图节点
        /// </summary>
        /// <param name="dto">Type，ModelId,TopologyDtos</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> SaveTopology(DataCollisionQueryDto dto)
        {
            List<TopologyDto> list = _mapper.Map<List<TopologyDto>>(await _topologyService.GetEntitys(c => c.Type == dto.Type && c.ModelId == dto.ModelId));

            List<TopologyDto> updateList = dto.TopologyDtos.Intersect(list, new Compare<TopologyDto>((x, y) => x != null && y != null && x.Id.Equals(y.Id))).ToList();
            List<TopologyDto> delList = list.Except(dto.TopologyDtos, new Compare<TopologyDto>((x, y) => x != null && y != null && x.Id.Equals(y.Id))).ToList();
            List<TopologyDto> addList = dto.TopologyDtos.Except(list, new Compare<TopologyDto>((x, y) => x != null && y != null && x.Id.Equals(y.Id) && x.Id != 0 && y.Id != 0)).ToList();

            foreach (var item in updateList)
            {
                topology model = _mapper.Map<topology>(dto.TopologyDtos.FirstOrDefault(c => c.Id == item.Id));
                await _topologyService.Update(model);
            }

            foreach (var item in delList)
            {
                await _topologyService.Delete(_mapper.Map<topology>(item));
            }

            foreach (var item in addList)
            {
                topology model = _mapper.Map<topology>(item);
                await _topologyService.Add(model);
            }

            await Logger.AddPlatformLog($"保存某个模型或者任务的所有拓扑图节点", LogType.DataOperation);
            return new AjaxResult<bool>(true);
        }

        /// <summary>
        /// 预览
        /// </summary>
        /// <param name="dto">TableName,DataConditions</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<dynamic>> Preview(DataCollisionQueryDto dto)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrWhiteSpace(dto.TableName))
            {
                string sql = $"SELECT * FROM {dto.PatternName}." + dto.TableName + " WHERE 1=1 ";
                foreach (var item in dto.DataConditions)
                {
                    if (item.FieldType == "varchar")
                    {
                        switch (item.Condition)
                        {
                            case "等于":
                                sql += $" AND ({item.FieldName} = '{item.FieldValue}') ";
                                break;
                            case "包含":
                                sql += $" AND (INSTR({item.FieldName}, '{item.FieldValue}') > 0) ";
                                break;
                            case "不包含":
                                sql += $" AND (INSTR({item.FieldName}, '{item.FieldValue}') <= 0) ";
                                break;
                            default:
                                sql += $" AND ({item.FieldName} = '{item.FieldValue}') ";
                                break;
                        }
                    }
                    else if (item.FieldType == "int" || item.FieldType == "bigint")
                    {
                        switch (item.Condition)
                        {
                            case "等于":
                                sql += $" AND ({item.FieldName} = {item.FieldValue}) ";
                                break;
                            case "不等于":
                                sql += $" AND ({item.FieldName} <> {item.FieldValue}) ";
                                break;
                            case "大于":
                                sql += $" AND ({item.FieldName} > {item.FieldValue}) ";
                                break;
                            case "小于":
                                sql += $" AND ({item.FieldName} < {item.FieldValue}) ";
                                break;
                            case "大于等于":
                                sql += $" AND ({item.FieldName} >= {item.FieldValue}) ";
                                break;
                            case "小于等于":
                                sql += $" AND ({item.FieldName} <= {item.FieldValue}) ";
                                break;
                            default:
                                sql += $" AND ({item.FieldName} = {item.FieldValue}) ";
                                break;
                        }
                    }
                    else if (item.FieldType == "datetime")
                    {
                        switch (item.Condition)
                        {
                            case "等于":
                                sql += $" AND ({item.FieldName} = 'str_to_date('{item.FieldValue}','%Y-%m-%d %T')') ";
                                break;
                            case "不等于":
                                sql += $" AND ({item.FieldName} <> 'str_to_date('{item.FieldValue}','%Y-%m-%d %T')') ";
                                break;
                            case "大于":
                                sql += $" AND ({item.FieldName} > 'str_to_date('{item.FieldValue}','%Y-%m-%d %T')') ";
                                break;
                            case "小于":
                                sql += $" AND ({item.FieldName} < 'str_to_date('{item.FieldValue}','%Y-%m-%d %T')') ";
                                break;
                            case "大于等于":
                                sql += $" AND ({item.FieldName} >= 'str_to_date('{item.FieldValue}','%Y-%m-%d %T')') ";
                                break;
                            case "小于等于":
                                sql += $" AND ({item.FieldName} <= 'str_to_date('{item.FieldValue}','%Y-%m-%d %T')') ";
                                break;
                            default:
                                sql += $" AND ({item.FieldName} = 'str_to_date('{item.FieldValue}','%Y-%m-%d %T')') ";
                                break;
                        }
                    }
                }
                dt = await _dataTableService.GetDataTable(sql);
            }

            return new AjaxResult<dynamic>(dt);

        }
        #endregion

        #region 模型审核日志

        /// <summary>
        /// 获取模型审核日志目录分页
        /// </summary>
        /// <param name="dto">Tissue,Realm,Region,Type,Operation,ModelName,Operator</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<DataModelExamineLogDto>>> GetExamineLogPageList(ExamineLogQueryDto dto)
        {
            //分页参数
            if (dto.PageIndex <= 0)
            {
                dto.PageIndex = 1;
            }
            if (dto.PageSize <= 0)
            {
                dto.PageSize = 10;
            }
            RefAsync<int> total = 0;
            string sql = "select  * from datamodel_examinelog where 1=1";
            if (!string.IsNullOrWhiteSpace(dto.Tissue))
            {
                sql += $" and Tissue = '{dto.Tissue}'";
            }
            if (!string.IsNullOrWhiteSpace(dto.Realm))
            {
                sql += $" and Realm = '{dto.Realm}'";
            }
            if (!string.IsNullOrWhiteSpace(dto.Region))
            {
                sql += $" and Region = '{dto.Region}'";
            }
            if (!string.IsNullOrWhiteSpace(dto.Type))
            {
                sql += $" and Type = '{dto.Type}'";
            }
            if (!string.IsNullOrWhiteSpace(dto.Operation))
            {
                sql += $" and Operation = '{dto.Operation}'";
            }
            if (!string.IsNullOrWhiteSpace(dto.ModelName))
            {
                sql += $" and INSTR(ModelName,'{dto.ModelName}')>0 ";
            }
            if (!string.IsNullOrWhiteSpace(dto.Operator))
            {
                sql += $" and INSTR(Operator,'{dto.Operator}')>0 ";
            }

            List<DataModelExamineLogDto> list = await _dataTableService.GetEntityPageList<DataModelExamineLogDto>(sql, dto.PageIndex, dto.PageSize, total);
            await Logger.AddPlatformLog("获取模型审核日志目录分页", LogType.DataAccess);

            return new PageResult<List<DataModelExamineLogDto>>(list, total, total % dto.PageSize == 0 ? total / dto.PageSize : total / dto.PageSize + 1, dto.PageSize, dto.PageIndex);
        }

        /// <summary>
        /// 保存模型审核日志
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> AddExamineLog(DataModelExamineLogDto dto)
        {
            datamodel_examinelog model = _mapper.Map<datamodel_examinelog>(dto);

            await _dataModelExamineLogService.Add(model);
            await Logger.AddPlatformLog("保存模型审核日志", LogType.DataAccess);
            return new AjaxResult<bool>(true);

        }
        #endregion

        /// <summary>
        /// 生成某个模型或者任务的sql
        /// </summary>
        /// <param name="dto">Type,ModelId</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> SaveSql(DataCollisionQueryDto dto)
        {
            string sql = "select ";
            List<tabletotable> tables = await _tableToTableService.GetEntitys(c => c.ModelId == dto.ModelId && c.Type == dto.Type);
            List<string> names = tables.Select(c => c.TableNameA).ToList();
            names.AddRange(tables.Select(c => c.TableNameB));
            names = names.Distinct().ToList();
            List<datamodel_privatesheet> priSheets = await _dataModelPrivateSheetService.GetEntitys(c => c.Type == dto.Type && c.ModelId == dto.ModelId);
            priSheets = priSheets.Where(c => names.Exists(x => x == c.SheetName)).ToList();
            List<datamodel_publicsheet> pubSheets = await _dataModelPublicSheetService.GetEntitys(c => c.Type == dto.Type && c.ModelId == dto.ModelId);
            pubSheets = pubSheets.Where(c => names.Exists(x => x == c.SheetName)).ToList();

            List<string> arr = priSheets.Select(c=>c.PatternName).ToList();
            arr.AddRange(pubSheets.Select(c => c.PatternName).ToList());
            arr = arr.Distinct().ToList();
            Dictionary<string,List<TableDto>> dic = new Dictionary<string, List<TableDto>>();
            foreach (var item in arr)
            {
                List< string> a = priSheets.Where(c=>c.PatternName==item).Select(c=>c.SheetName).ToList();
                a.AddRange(pubSheets.Where(c => c.PatternName == item).Select(c => c.SheetName).ToList());
                var val = string.Join("','", a);
                val = val.TrimStart('\'').TrimStart(',').TrimStart('\'');
                val = "\'" + val;
                val = val.TrimEnd('\'');
                val = val + "\'";
                List<TableDto> tableDtos = GetTableFields(val,item,true).Result;
                dic.Add(item, tableDtos);
            }




            foreach (var item in priSheets)
            {
                //sql += item.PatternName+"_"+item.SheetName + "." + item.SheetField + $" as {item.PatternName}_{item.SheetName}_{item.SheetField},";
                sql += item.PatternName + "_" + item.SheetName + "." + item.SheetField + $" as {dic[item.PatternName].FirstOrDefault(c=>c.GUID==item.SheetName && c.COLUMN_NAME==item.SheetField).COLUMN_COMMENT},";
            }
            foreach (var item in pubSheets)
            {
                //sql += item.PatternName + "_" + item.SheetName + "." + item.SheetField + $" as {item.PatternName}_{item.SheetName}_{item.SheetField},";
                sql += item.PatternName + "_" + item.SheetName + "." + item.SheetField + $" as {dic[item.PatternName].FirstOrDefault(c => c.GUID == item.SheetName && c.COLUMN_NAME == item.SheetField).COLUMN_COMMENT},";
            }
            sql = sql.TrimEnd(',');
            sql += " from ";

            int count = tables.Count();
            tables = tables.Distinct(new Compare<tabletotable>((x, y) => x != null && y != null && x.TableNameA==y.TableNameA && x.TableNameB==y.TableNameB)).ToList();
            tables = tables.OrderBy(c => c.TableNameA).ToList();

            List<string> subKeyWords = new List<string>();// result[i]所相关的tabletotable对象中使用A表还是B表
            List<string> keyWords = new List<string>();// result[i]所使用的A表还是B表
            List<int> indexs = new List<int>();// result[i]所相关的tabletotable对象在result中的下标
            List<tabletotable> result = new List<tabletotable>();// 最终sql中表名的顺序
            int flag = IsLoopData(tables, ref subKeyWords, ref keyWords, ref indexs, ref result);
            if (flag == 1)
            {
                return new AjaxResult<bool>(false, "循环连接");
            }
            else if (flag == 2)
            {
                return new AjaxResult<bool>(false, "存在数据孤岛");
            }
            else if(flag==0)
            {
                return new AjaxResult<bool>(false, "没有选择表间关系");
            }
            sql += result[0].PatternNameA+"."+result[0].TableNameA + $" {result[0].PatternNameA}_{result[0].TableNameA} left join {result[0].PatternNameB}.{result[0].TableNameB} {result[0].PatternNameB}_{result[0].TableNameB} on {result[0].PatternNameA}_{result[0].TableNameA}.{result[0].TableFieldA} = {result[0].PatternNameB}_{result[0].TableNameB}.{result[0].TableFieldB} ";
            for (int i = 1; i < result.Count; i++)
            {
                if (keyWords[i] == "A")
                {
                    if (subKeyWords[i] == "A")
                    {
                        sql += $" left join {result[i].PatternNameA}.{result[i].TableNameA} {result[i].PatternNameA}_{result[i].TableNameA} on {result[i].PatternNameA}_{result[i].TableNameA}.{result[i].TableFieldA} = {result[indexs[i]].PatternNameA}_{result[indexs[i]].TableNameA}.{result[indexs[i]].TableFieldA} ";

                    }
                    else
                    {
                        sql += $" left join {result[i].PatternNameA}.{result[i].TableNameA} {result[i].PatternNameA}_{result[i].TableNameA} on {result[i].PatternNameA}_{result[i].TableNameA}.{result[i].TableFieldA} = {result[indexs[i]].PatternNameB}_{result[indexs[i]].TableNameB}.{result[indexs[i]].TableFieldB} ";
                        //sql += $" left join {result[i].TableNameA} {result[i].TableNameA} on {result[i].TableFieldA} = {result[indexs[i]].TableFieldB} ";
                    }
                }
                else
                {
                    if (subKeyWords[i] == "A")
                    {
                        sql += $" left join {result[i].PatternNameB}.{result[i].TableNameB} {result[i].PatternNameB}_{result[i].TableNameB} on {result[i].PatternNameB}_{result[i].TableNameB}.{result[i].TableFieldB} = {result[indexs[i]].PatternNameA}_{result[indexs[i]].TableNameA}.{result[indexs[i]].TableFieldA} ";
                        //sql += $" left join {result[i].TableNameB} {result[i].TableNameB} on {result[i].TableFieldB} = {result[indexs[i]].TableFieldA} ";

                    }
                    else
                    {
                        sql += $" left join {result[i].PatternNameB}.{result[i].TableNameB} {result[i].PatternNameB}_{result[i].TableNameB} on {result[i].PatternNameB}_{result[i].TableNameB}.{result[i].TableFieldB} = {result[indexs[i]].PatternNameB}_{result[indexs[i]].TableNameB}.{result[indexs[i]].TableFieldB} ";
                        //sql += $" left join {result[i].TableNameB} {result[i].TableNameB} on {result[i].TableFieldB} = {result[indexs[i]].TableFieldB} ";
                    }
                }
            }
            sql += " where 1=1 ";

            List<dataconditions> conditions = await _dataConditionsService.GetEntitys(c => c.ModelId == dto.ModelId && c.Type == dto.Type);
            conditions = conditions.Where(c => names.Exists(x => x == c.TableName)).ToList();
            foreach (var item in conditions)
            {
                if (item.FieldType == "varchar")
                {
                    switch (item.Condition)
                    {
                        case "等于":
                            sql += $" AND ({item.PatternName}_{item.TableName}_{item.FieldName} = '{item.FieldValue}') ";
                            break;
                        case "包含":
                            sql += $" AND (INSTR({item.PatternName}_{item.TableName}_{item.FieldName}, '{item.FieldValue}') > 0) ";
                            break;
                        case "不包含":
                            sql += $" AND (INSTR({item.PatternName}_{item.TableName}_{item.FieldName}, '{item.FieldValue}') <= 0) ";
                            break;
                        default:
                            break;
                    }
                }
                else if (item.FieldType == "int" || item.FieldType == "bigint")
                {
                    switch (item.Condition)
                    {
                        case "等于":
                            sql += $" AND ({item.PatternName}_{item.TableName}_{item.FieldName} = {item.FieldValue}) ";
                            break;
                        case "不等于":
                            sql += $" AND ({item.PatternName}_{item.TableName}_{item.FieldName} <> {item.FieldValue}) ";
                            break;
                        case "大于":
                            sql += $" AND ({item.PatternName}_{item.TableName}_{item.FieldName} > {item.FieldValue}) ";
                            break;
                        case "小于":
                            sql += $" AND ({item.PatternName}_{item.TableName}_{item.FieldName} < {item.FieldValue}) ";
                            break;
                        case "大于等于":
                            sql += $" AND ({item.PatternName}_{item.TableName}_{item.FieldName} >= {item.FieldValue}) ";
                            break;
                        case "小于等于":
                            sql += $" AND ({item.PatternName}_{item.TableName}_{item.FieldName} <= {item.FieldValue}) ";
                            break;
                        default:
                            break;
                    }
                }
                else if (item.FieldType == "datetime")
                {
                    switch (item.Condition)
                    {
                        case "等于":
                            sql += $" AND ({item.PatternName}_{item.TableName}_{item.FieldName} = 'str_to_date('{item.FieldValue}','%Y-%m-%d %T')') ";
                            break;
                        case "不等于":
                            sql += $" AND ({item.PatternName}_{item.TableName}_{item.FieldName} <> 'str_to_date('{item.FieldValue}','%Y-%m-%d %T')') ";
                            break;
                        case "大于":
                            sql += $" AND ({item.PatternName}_{item.TableName}_{item.FieldName} > 'str_to_date('{item.FieldValue}','%Y-%m-%d %T')') ";
                            break;
                        case "小于":
                            sql += $" AND ({item.PatternName}_{item.TableName}_{item.FieldName} < 'str_to_date('{item.FieldValue}','%Y-%m-%d %T')') ";
                            break;
                        case "大于等于":
                            sql += $" AND ({item.PatternName}_{item.TableName}_{item.FieldName} >= 'str_to_date('{item.FieldValue}','%Y-%m-%d %T')') ";
                            break;
                        case "小于等于":
                            sql += $" AND ({item.PatternName}_{item.TableName}_{item.FieldName} <= 'str_to_date('{item.FieldValue}','%Y-%m-%d %T')') ";
                            break;
                        default:
                            break;
                    }
                }


            }

            if (dto.Type == 1)
            {
                datamodel dm = await _dataModelService.GetEntity(c => c.Id == dto.ModelId);
                dm.ExcuteSql = sql;
                await _dataModelService.UpdateColumn(dm, c => c.ExcuteSql);
            }
            else
            {
                datatask dt = await _dataTaskService.GetEntity(c => c.Id == dto.ModelId);
                dt.ExcuteSql = sql;
                await _dataTaskService.UpdateColumn(dt, c => c.ExcuteSql);
            }
            await Logger.AddPlatformLog("保存sql语句", LogType.DataAccess);

            return new AjaxResult<bool>(true);
        }

        private int IsLoopData(List<tabletotable> list, ref List<string> subKeyWords, ref List<string> keyWords, ref List<int> indexs, ref List<tabletotable> result)
        {
            List<tabletotable> cloneList = CloneHelper.Clone<tabletotable>(list);
            if(list.Count<=0)
            {
                // 没有数据.
                return 0;
            }
            result.Add(list[0]);
            keyWords.Add("A");
            subKeyWords.Add("A");
            indexs.Add(0);
            cloneList.RemoveAt(0);
            int count = -1;
            while (true)
            {
                for (int i = 1; i < list.Count; i++)
                {
                    if (result.Exists(c => c.Id == list[i].Id))
                    {
                        continue;
                    }
                    if (result.Exists(c => c.TableNameA == list[i].TableNameA || c.TableNameB == list[i].TableNameA) && result.Exists(c => c.TableNameA == list[i].TableNameB || c.TableNameB == list[i].TableNameB))
                    {
                        // 循环连接
                        return 1;
                    }
                    else
                    {
                        if (result.Exists(c => c.TableNameA == list[i].TableNameA || c.TableNameB == list[i].TableNameA))
                        {
                            tabletotable dto = result.FirstOrDefault(c => c.TableNameA == list[i].TableNameA || c.TableNameB == list[i].TableNameA);
                            result.Add(list[i]);
                            indexs.Add(result.IndexOf(dto));
                            if (dto.TableNameA == list[i].TableNameA)
                            {
                                subKeyWords.Add("A");
                            }
                            else
                            {
                                subKeyWords.Add("B");
                            }
                            keyWords.Add("A");
                            cloneList.RemoveAll(c => c.Id == list[i].Id);
                        }
                        else if (result.Exists(c => c.TableNameA == list[i].TableNameB || c.TableNameB == list[i].TableNameB))
                        {
                            tabletotable dto = result.FirstOrDefault(c => c.TableNameA == list[i].TableNameB || c.TableNameB == list[i].TableNameB);
                            result.Add(list[i]);
                            indexs.Add(result.IndexOf(dto));
                            if (dto.TableNameA == list[i].TableNameB)
                            {
                                subKeyWords.Add("A");
                            }
                            else
                            {
                                subKeyWords.Add("B");
                            }
                            keyWords.Add("B");
                            cloneList.RemoveAll(c => c.Id == list[i].Id);
                        }
                    }
                }
                if (cloneList.Count == count)
                {
                    break;
                }
                else
                {
                    count = cloneList.Count;
                }
            }
            if (count > 0)
            {
                // 存在数据孤岛
                return 2;
            }
            else
            {
                // 正确
                return 3;
            }
        }

        /// <summary>
        /// 根据角色获取用户信息
        /// </summary>
        /// <param name="roletype">1：系统管理员；2：安全保密员；3：安全审计员；4：操作员；5：超级管理员；6：管理员；</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult<List<ReturnUserInfoDto>>> GetRoleUserInfo(int roletype)
        {
            string sql = $" SELECT cu.id,cu.real_name FROM `cloud_user_role` cur left  join  cloud_user cu on cu.id=cur.UserId where cur.roleid={roletype} and cur.type =1 union  select  cu.id,cu.real_name from cloud_user cu where description in ( SELECT UserId FROM `cloud_user_role` cur   where cur.roleid={roletype} and cur.type=2 ) ";
            var result = await _dataTableService.GetEntityList<ReturnUserInfoDto>(sql);
            return new AjaxResult<List<ReturnUserInfoDto>>(result);
        }

        /// <summary>
        /// 执行界面上的sql
        /// </summary>
        /// <param name="dto">Sql</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<dynamic>> ExcuteSql(DataCollisionQueryDto dto)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrWhiteSpace(dto.Sql))
            {
                dto.Sql = dto.Sql.Trim(' ');
                if (dto.Sql.Substring(0, 6).ToUpper() == "SELECT")
                {
                    dt = await _dataTableService.GetDataTable(dto.Sql);
                }
            }

            return new AjaxResult<dynamic>(dt);

        }

    }
}
