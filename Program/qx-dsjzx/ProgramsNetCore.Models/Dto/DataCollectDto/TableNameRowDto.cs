using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    /// <summary>
    /// 表 - 数据量模型
    /// </summary>
    public class TableNameRowDto
    {

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        
        /// <summary>
        /// 数据量
        /// </summary>
        public string RowsCount { get; set; }

    }
}
