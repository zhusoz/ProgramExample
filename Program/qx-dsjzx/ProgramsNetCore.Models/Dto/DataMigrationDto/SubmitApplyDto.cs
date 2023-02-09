using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataMigration
{
    /// <summary>
    /// 提交申请请求类
    /// </summary>
    public class SubmitApplyDto
    {
        /// <summary>
        /// 申请车任务Id
        /// </summary>
        public List<int> TaskId { get; set; }
    }
}
