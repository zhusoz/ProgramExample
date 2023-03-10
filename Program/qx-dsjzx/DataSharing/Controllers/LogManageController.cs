using Microsoft.AspNetCore.Mvc;
using ProgramsNetCore.Common;
using ProgramsNetCore.Common.Basic;
using ProgramsNetCore.IService;
using ProgramsNetCore.Models.Dto.LogManageDto;
using SqlSugar;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DataSharing.Controllers
{
    /// <summary>
    /// 日志管理
    /// </summary>
    [Route("")]
    [ApiController]
    public class LogManageController : ControllerBase
    {
        private readonly IDbLogService _dbLogService;
        private readonly IErrorLogService _errorLogService;
        private readonly IPlatformLogService _platformLogService;
        private readonly IDataTableService _dataTableService;

        public LogManageController(IDbLogService dbLogService, IErrorLogService errorLogService, IPlatformLogService platformLogService, IDataTableService dataTableService)
        {
            _dbLogService = dbLogService;
            _errorLogService = errorLogService;
            _platformLogService = platformLogService;
            _dataTableService = dataTableService;
        }

        /// <summary>
        /// 平台用户操作日志
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<ResultGetPlatformLogsDto>>> GetPlatformLogs(GetPlatformLogsQueryDto dto)
        {
            if (dto.PageIndex <= 0) dto.PageIndex = 1;
            if (dto.PageSize <= 0) dto.PageSize = 10;

            string sql = $"select l.`Id`,u.`real_name` `Name`,r.`Name` `Role`,l.`Describe`,l.`CreateTime` from data_sharing_platform.`log_platform` l join data_sharing_platform.`cloud_user` u on u.`Id`=l.`UserId` join data_sharing_platform.`cloud_user_role` ur on l.`UserId`=ur.`UserId` join data_sharing_platform.`cloud_role` r on r.`Id`=ur.`RoleId` where 1=1";

            //日志类型筛选
            if (dto.LogType.HasValue)
            {
                sql += $" and l.`LogType`={dto.LogType}";
            }
            //关键词筛选
            if (!string.IsNullOrEmpty(dto.KeyWord))
            {
                sql += $" and l.`Describe` like '%{dto.KeyWord}%'";
            }
            sql += " order by l.CreateTime desc";

            RefAsync<int> total = 0;
            var list = await _dataTableService.GetEntityPageList<ResultGetPlatformLogsDto>(sql, dto.PageIndex, dto.PageSize, total);

            string text = "";
            switch (dto.LogType)
            {
                case 0:
                    text = "登录";
                    break;
                case 1:
                    text = "登出";
                    break;
                case 2:
                    text = "数据审批";
                    break;
                case 3:
                    text = "数据申请";
                    break;
                case 4:
                    text = "数据获取";
                    break;
                case 5:
                    text = "数据操作";
                    break;
                default:
                    text = "全部";
                    break;
            }
            //await Logger.AddPlatformLog($"获取{text}日志", LogType.DataAccess);

            return new PageResult<List<ResultGetPlatformLogsDto>>(list, total, total % dto.PageSize == 0 ? total / dto.PageSize : total / dto.PageSize + 1, dto.PageSize, dto.PageIndex);
        }

        /// <summary>
        /// 数据库操作日志
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<List<ResultGetDbLogsDto>>> GetDbLogs(GetDbLogsQueryDto dto)
        {

            if (dto.PageIndex <= 0) dto.PageIndex = 1;
            if (dto.PageSize <= 0) dto.PageSize = 10;

            string sql = $"select l.`Id`,l.`Source`,l.`LogLevel`,case l.`LogLevel` when 0 then 'Trace' when 1 then 'Debug' when 2 then 'Information' when 3 then 'Warning' when 4 then 'Error' when 5 then 'Critical' when 6 then 'None' else null end as `LogLevelInfo`,l.`Describe`,l.`TargetDbAddress`,l.`CreateTime` from data_sharing_platform.`log_db` l where 1=1";

            //日志类型筛选
            if (dto.LogType.HasValue)
            {
                sql += $" and l.`LogLevel`={dto.LogType}";
            }
            //关键词筛选
            if (!string.IsNullOrEmpty(dto.KeyWord))
            {
                sql += $" and l.`Describe` like '%{dto.KeyWord}%'";
            }
            sql += " order by l.CreateTime desc";

            RefAsync<int> total = 0;
            var list = await _dataTableService.GetEntityPageList<ResultGetDbLogsDto>(sql, dto.PageIndex, dto.PageSize, total);

            //await Logger.AddPlatformLog("获取数据库操作日志", LogType.DataAccess);

            return new PageResult<List<ResultGetDbLogsDto>>(list, total, total % dto.PageSize == 0 ? total / dto.PageSize : total / dto.PageSize + 1, dto.PageSize, dto.PageIndex);

        }


    }
}
