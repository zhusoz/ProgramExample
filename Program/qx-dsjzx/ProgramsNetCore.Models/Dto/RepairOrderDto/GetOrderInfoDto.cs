using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.RepairOrderDto
{
    /// <summary>
    /// 工单信息请求类
    /// </summary>
    public class GetOrderInfoDto:PageDto
    {
        /// <summary>
        /// 工单状态：0：全部；1：执行中；2：已完成；
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int? Priority { get; set; }      
    }
}
