using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataManageDto
{
    /// <summary>
    /// 映射字段返回类
    /// </summary>
    public class ReturnMapInfoDto
    {
        /// <summary>
        /// 中文表名
        /// </summary>
        public string CN_SourceName { get; set; }
        /// <summary>
        /// 英文表名
        /// </summary>
        public string EN_SourceName { get; set; }
    }
}
