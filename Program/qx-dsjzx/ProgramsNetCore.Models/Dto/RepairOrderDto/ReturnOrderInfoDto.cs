using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.RepairOrderDto
{
    /// <summary>
    /// 工单信息返回类
    /// </summary>
    public class ReturnOrderInfoDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        ///        提交时间
        /// </summary>
        public DateTime SubmitTime { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// 优先级名称
        /// </summary>
        public string PriorityName { get; set; }
        /// <summary>
        /// 工单状态
        /// </summary>
        public string Process { get; set; }
    }
}
