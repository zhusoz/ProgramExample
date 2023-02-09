using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    /// <summary>
    /// 
    /// </summary>
    public class GetFieldsInfoResultDto
    {

        /// <summary>
        /// 表Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        public string DataName { get; set; }

        /// <summary>
        /// Guid
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 表字段
        /// </summary>
        public string Fields { get; set; }

        /// <summary>
        /// 模式名
        /// </summary>
        public string PatternName { get; set; }

    }
}
