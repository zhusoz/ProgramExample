using DataSharing.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProblemDisposal.Common.Basic;
using ProgramsNetCore.IService;
using ProgramsNetCore.Models.Dto.DataCollectDto;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Models;
using System;
using ProgramsNetCore.Common;
using System.Reflection.Emit;
using System.IdentityModel.Tokens.Jwt;

namespace DataSharing.Controllers
{
    /// <summary>
    /// 数据导入导出
    /// </summary>
    [Route("")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    //[Authorize(Policy = "Permission")]//策略授权
    public class ExcelController : ControllerBase
    {
        private readonly IDataTableService _dataTableService;
        private readonly IDataTrendService _dataTrendService;
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IRolePermissionService _rolePermissionService;
        private readonly IRoleService _roleService;
        private readonly IDataMapService _dataMapService;
        private readonly IProcessService _processService;

        public ExcelController(IDataTableService dataTableService,IDataTrendService dataTrendService, IUserService userService, IUserRoleService userRoleService, IRolePermissionService rolePermissionService, IRoleService roleService, IDataMapService dataMapService, IProcessService processService)
        {
            _dataTableService=dataTableService;
            _dataTrendService=dataTrendService;
            _userService=userService;
            _userRoleService=userRoleService;
            _rolePermissionService=rolePermissionService;
            _roleService=roleService;
            _dataMapService=dataMapService;
            _processService=processService;
        }

        /// <summary>
        /// 下载模板文件[UpdateTime:2022年8月24日10:02:25]
        /// </summary>
        /// <param name="dataMapId">源表id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DownloadTemplatefile(int dataMapId)
        {
            //1.源表是否存在
            var dataMap = await _dataMapService.GetEntity(e => e.Id==dataMapId);
            if (dataMap is null) return BadRequest();

            //2.表的访问量+1
            dataMap.Traffic+=1;
            await _dataMapService.UpdateColumn(dataMap, e => e.Traffic);

            //3.将数据源表转成datable对象
            string dataDicTableName = $"dic_{dataMap.AssociativeTable}";
            List<DataItemDicDto> dataItemsList = await _dataTableService.GetEntityList<DataItemDicDto>($"select CnFieldName,EnFieldName from data_sharing_main.`{dataDicTableName}`");
            Dictionary<string, string> dic = dataItemsList.ToDictionary(item => item.EnFieldName, item => item.CnFieldName);
            string sql = $"select {string.Join(',', dataItemsList.Select(item => $"`{item.EnFieldName}`"))} from data_sharing_main.`{dataMap.AssociativeTable}` limit 0 offset 0";
            DataTable data = await _dataTableService.GetDataTable(sql);

            //4.导出字节数组返回
            byte[] bytes = DataTableConverter.DataTableToBytes(data, dic);  
            //await Logger.AddPlatformLog("下载模板文件", LogType.DataAccess);
            
            return File(bytes, "application/octet-stream", $"{dataMap.Name}.xls");
            
        }

        /// <summary>
        /// 下载数据源文件[UpdateTime:2022年8月10日10:42:41]
        /// </summary>
        /// <param name="dataMapId">源表id</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> DownloadDatafile(int dataMapId)
        {
            //源表是否存在
            var dataMap = await _dataMapService.GetEntity(e => e.Id==dataMapId);
            if (dataMap is null) return BadRequest();
            //获取登录用户
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(t => t.Type==JwtRegisteredClaimNames.NameId)?.Value);

            //源表结构查询
            string dataDicTableName = $"dic_{dataMap.AssociativeTable}";
            List<DataItemDicDto> dataItemsList = await _dataTableService.GetEntityList<DataItemDicDto>($"select CnFieldName,EnFieldName from data_sharing_main.`{dataDicTableName}`");
            Dictionary<string, string> dic = dataItemsList.ToDictionary(item => item.EnFieldName, item => item.CnFieldName);
            DataTable data = await _dataTableService.GetDataTable($"select {string.Join(',', dataItemsList.Select(item => $"`{item.EnFieldName}`"))} from data_sharing_main.`{dataMap.AssociativeTable}`");

            #region Apose.cell导出
            //var stream = DataTableConverter.DataTableToExcel(data, dic);
            //stream.Position=0;
            //return File(stream, "application/octet-stream", $"{dataMap.Name}.xls");
            #endregion

            //NPOI导出
            byte[] bytes = DataTableConverter.DataTableToBytes(data, dic);

            //添加数据导出日志
            await _dataTrendService.Add(new datatrend
            {
                AffectedRows=data.Rows.Count,
                CreateTime=DateTime.Now,
                DataMapId=dataMapId,
                Type=1,
                UserId=userId
            });
            //await Logger.AddPlatformLog("下载数据源文件", LogType.DataAccess);
            
            return File(bytes, "application/octet-stream", $"{dataMap.Name}.xls");
        }


        /// <summary>
        /// 数据导入 [只支持Excel]
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult<bool>> DatasInsertIntoTable(int dataMapId, IFormFile file)
        {
            //获取文件扩展名
            //只处理csv、excel文件
            string fileExtension = Path.GetExtension(file.FileName);
            if (fileExtension!=".csv" && fileExtension!=".xlsx" && fileExtension!=".xls")
            {
                return new AjaxResult<bool>(false, "不支持的文件格式类型");
            }
            //获取登录用户
            int userId = Convert.ToInt32(User.Claims.FirstOrDefault(t => t.Type==JwtRegisteredClaimNames.NameId)?.Value);


            //获取导入数据的数据项字典表
            var dataMapEntity = await _dataMapService.GetEntity(e => e.Id==dataMapId);
            if (dataMapEntity==null || !dataMapEntity.IsEnableImportData) return new AjaxResult<bool>(false);
            List<DataItemDicDto> dataItemsReflect = await _dataTableService.GetEntityList<DataItemDicDto>($"select t.`Id`,t.`CnFieldName`,t.`EnFieldName`,t.`DataType`,t.`DataLength`,t.`Description` `FieldDescription`,t.`IsPrimaryKey`,t.`ShareType` from data_sharing_main.`dic_{dataMapEntity.AssociativeTable}` t");
            Dictionary<string, string> coloumTypeRelection = dataItemsReflect.ToDictionary((source) => source.CnFieldName, element => element.EnFieldName);

            //将文件转成DataTable
            DataTable dt = null;
            if (fileExtension==".csv") dt = DataTableConverter.CSV2DataTable(file.OpenReadStream(), coloumTypeRelection);
            else if (fileExtension==".xlsx" || fileExtension==".xls")
            {
                dt = DataTableConverter.ExcelToDataTable(file.FileName, file.OpenReadStream(), coloumTypeRelection);
            }

            //判断DataTable是否为null
            if (dt==null || dt.Rows.Count<=0) return new AjaxResult<bool>(false,"不是合法的数据");
            DateTime now=DateTime.Now;
            dataMapEntity.ImportDataTime=now;
            dataMapEntity.Source="Excel表格导入";
            await _dataMapService.UpdateColumn(dataMapEntity, e => new { e.ImportDataTime, e.Source });

            //将DataTable导入到数据库
            //返回受影响行数
            int affectedRows = await _dataTableService.BulkCopyDataTable(dt, $"data_sharing_main.{dataMapEntity.AssociativeTable}");

            //添加数据导出日志
            await _dataTrendService.Add(new datatrend
            {
                AffectedRows=affectedRows,
                CreateTime=now,
                DataMapId=dataMapId,
                Type=0,
                UserId=userId
            });
            //await Logger.AddPlatformLog("数据导入", LogType.DataOperation);

            return new AjaxResult<bool>(affectedRows>0);
        }

    }

}
