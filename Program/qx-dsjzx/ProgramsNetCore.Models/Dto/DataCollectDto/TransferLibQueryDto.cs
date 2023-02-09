using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.DataCollectDto
{
    /// <summary>
    /// 转库申请dto
    /// </summary>
    public class TransferLibQueryDto
    {
        /// <summary>
        /// 数源表Id
        /// </summary>
        public int DataMapId { get; set; }

        /// <summary>
        /// 转库类型[0:长期库/私有库,1:主题库/公有库]
        /// </summary>
        public int LibType { get; set; }

        /// <summary>
        /// 私有库过期时间[如果是长期库,参数应为null]
        /// </summary>
        public DateTime? ExpireTime { get; set; }
    }
}
