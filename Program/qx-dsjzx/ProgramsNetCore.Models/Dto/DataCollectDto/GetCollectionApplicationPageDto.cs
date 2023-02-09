using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    /// <summary>
    /// 审批管理dto
    /// </summary>
    public class GetCollectionApplicationPageDto:PageDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// [Status 0:等待审批 1:审批中 2:审批完成]
        /// </summary>
        public int? Status { get; set; }

    }
}
