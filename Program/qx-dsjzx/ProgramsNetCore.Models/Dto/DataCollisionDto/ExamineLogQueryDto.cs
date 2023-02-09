using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollisionDto
{
    /// <summary>
    /// 模型审核查询类
    /// </summary>
    public class ExamineLogQueryDto: PageDto
    {
        /// <summary>
        /// 模型名称
        /// </summary>
        public string ModelName { get; set; }
        /// <summary>
        /// 建模单位
        /// </summary>
        public string Tissue { get; set; }
        /// <summary>
        /// 所属领域
        /// </summary>
        public string Realm { get; set; }
        /// <summary>
        /// 所属区域
        /// </summary>
        public string Region { get; set; }
        /// <summary>
        /// 模型类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 操作
        /// </summary>
        public string Operation { get; set; }


    }
}
