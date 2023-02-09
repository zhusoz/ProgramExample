namespace ProgramsNetCore.Models.Dto.RepairOrderDto
{
    /// <summary>
    /// 提交提交工单请求类
    /// </summary>
    public class SubmitOrderDto
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        public int Taskid { get; set; }
        /// <summary>
        /// 反馈
        /// </summary>
        public string FeedBack { get; set; }
        /// <summary>
        /// 状态：1：提交；2：评价
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 节点：1：已处理完成；2：经核实无需处理，驳回；3：协商处理；4：申请延期处理；5：已解决；6：未解决；
        /// </summary>
        public int Node { get; set; }
    }
}