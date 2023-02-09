using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    /// <summary>
    /// 
    /// </summary>
    public class GetDataMapTotalCountResultDto
    {
        /// <summary>
        /// 源表Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}
