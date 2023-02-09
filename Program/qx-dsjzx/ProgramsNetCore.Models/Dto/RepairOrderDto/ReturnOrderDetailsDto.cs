using System;
using System.Collections.Generic;
using System.Text;

namespace ProgramsNetCore.Models.Dto.RepairOrderDto
{
    public class ReturnOrderDetailsDto
    {
        /// <summary>
        ///
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public string NO { get; set; }
        /// <summary>
        /// 申请人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 申请人部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 问题字段
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 处理时长
        /// </summary>
        public string HandlingTime { get; set; }
        /// <summary>
        /// 意见
        /// </summary>
        public string Opinion { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }
        /// <summary>
        /// 节点
        /// </summary>
        public int Node { get; set; }
    }
}
