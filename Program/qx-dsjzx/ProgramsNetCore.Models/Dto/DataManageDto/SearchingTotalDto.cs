using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataManageDto
{
    /// <summary>
    /// 检索数据条数请求类
    /// </summary>
    public class SearchingTotalDto
    {
        /// <summary>
        /// 数据表Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 条件
        /// </summary>
        public string Conditions { get; set; }
    }
}
