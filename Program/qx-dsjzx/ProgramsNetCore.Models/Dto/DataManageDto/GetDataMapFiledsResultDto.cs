using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataManageDto
{
    /// <summary>
    /// 
    /// </summary>
    public class GetDataMapFiledsResultDto
    {
        /// <summary>
        /// 数据项Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string CnFieldName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnFieldName { get; set; }

    }
}
