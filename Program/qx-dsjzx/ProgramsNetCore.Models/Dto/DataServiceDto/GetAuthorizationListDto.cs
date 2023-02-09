using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataServiceDto
{
    /// <summary>
    /// 获取授权列表
    /// </summary>
    public class GetAuthorizationListDto:PageDto
    {
        /// <summary>
        /// 0待授权列表 1授权记录
        /// </summary>
        public int Type { get; set; }
    }
}
