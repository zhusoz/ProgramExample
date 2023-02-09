using System;

namespace ProgramsNetCore.Models.Dto.RepairOrderDto
{
    /// <summary>
    /// 流程信息返回类
    /// </summary>
    public class ReturnProcessInfoDto
    {
        /// <summary>
        /// 申请人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 节点
        /// </summary>
        public int Node { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>

        public string NodeName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 流程名称
        /// </summary>
        public string Process { get; set; }

        /// <summary>
        /// 意见
        /// </summary>
        public string Opinion { get; set; }
    }
}