using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataMigrationDto
{
    /// <summary>
    /// 任务删除请求类
    /// </summary>
    public class DeleteTaskDto
    {
        /// <summary>
        /// 任务类型：1：迁移申请；2：数据互导
        /// </summary>
        public int TaskType { get; set; }
        /// <summary>
        /// 任务Id
        /// </summary>
        public List<int> TaskIds { get; set; }
    }
}
