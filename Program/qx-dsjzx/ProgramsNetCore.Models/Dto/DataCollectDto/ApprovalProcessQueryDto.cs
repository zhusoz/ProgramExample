using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    /// <summary>
    /// 审批处理Query Dto
    /// </summary>
    public class ApprovalProcessQueryDto
    {
        /// <summary>
        /// 申请流程Id
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 任务状态[0:等待审批 1:审批通过 2:驳回/审批未通过]
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 驳回原因
        /// </summary>
        public string AttachInfo { get; set; }

    }
}
