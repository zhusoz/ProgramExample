using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    /// <summary>
    /// 获取转库记录结果dto
    /// </summary>
    public class GetTransferLibRecordsResultDto
    {
        /// <summary>
        /// 源表id
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// 审批号id
        /// </summary>
        public string ApprovalId { get; set; }
        
        /// <summary>
        /// 源表名称
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// 数源单位
        /// </summary>
        public string Attribution { get; set; }
        
        /// <summary>
        /// 分层类型
        /// </summary>
        public string LayerName { get; set; }
        
        /// <summary>
        /// 审批状态
        /// </summary>
        public string ApprovalStatus { get; set; }

        /// <summary>
        /// 发起时间
        /// </summary>
        public DateTime StartTime { get; set; }

    }
}
