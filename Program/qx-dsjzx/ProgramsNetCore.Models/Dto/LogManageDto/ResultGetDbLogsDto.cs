using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.LogManageDto
{
    /// <summary>
    /// 数据库日志查询结果dto
    /// </summary>
    public class ResultGetDbLogsDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 日志等级[严重性],范围为0到6:[Trace:0,Debug:1,Information:2,Warning:3,Error:4,Critical=5,None:6]
        /// </summary>
        public int? LogLevel { get; set; }

        /// <summary>
        /// 日志等级描述
        /// </summary>
        public string LogLevelInfo { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 目标地址
        /// </summary>
        public string TargetDbAddress { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
