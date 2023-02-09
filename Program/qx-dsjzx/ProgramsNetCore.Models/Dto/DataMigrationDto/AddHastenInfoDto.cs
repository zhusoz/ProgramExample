using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataMigration
{
    /// <summary>
    /// 增加催办信息
    /// </summary>
    public class AddHastenInfoDto
    {
        /// <summary>
        /// 催办人Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 任务Id
        /// </summary>
        public List<int> TaskId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}
