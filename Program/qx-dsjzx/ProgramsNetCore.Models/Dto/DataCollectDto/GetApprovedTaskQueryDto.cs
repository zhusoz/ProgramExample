using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    /// <summary>
    /// 归集申请导入数据分页dto
    /// </summary>
    public class GetApprovedTaskQueryDto : PageDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int? UserId { get; set; }

    }
}
