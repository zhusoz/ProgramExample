using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.RepairOrderDto
{
    /// <summary>
    /// 增加工单请求类
    /// </summary>
    public class AddRepairOrderDto
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// 工单内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 意见
        /// </summary>
        public string Opinion { get; set; }
        /// <summary>
        /// Desc:问题字段
        /// </summary>           
        public string Field { get; set; }

        /// <summary>
        /// Desc:问题类型
        /// </summary>           
        public string Type { get; set; }
    }
}
