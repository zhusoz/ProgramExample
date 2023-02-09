namespace ProblemDisposal.Common.Basic
{
    /// <summary>
    /// 通用请求返回类
    /// </summary>
    public class AjaxResult<T>
    {
        public int StatusCode { get; set; } = 200;
        /// <summary>
        /// 用户数据
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 执行状态
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 执行消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 无参构造
        /// </summary>
        public AjaxResult()
        {
            Success = true;
            Message = "执行成功";
        }
        /// <summary>
        /// 执行成功并返回数据
        /// </summary>
        /// <param name="data"></param>
        public AjaxResult(T data)
        {
            Data = data;
            Success = true;
            Message = "执行成功";
        }
        /// <summary>
        /// 根据Success判断是否成功返回信息
        /// </summary>
        /// <param name="success"></param>
        public AjaxResult(bool success)
        {
            Success = success;
            if (success) Message = "执行成功";
            else Message = "执行失败";
        }
        /// <summary>
        /// 执行成功并返回数据和消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        public AjaxResult(T data, string message)
        {

            Data = data;
            Success = true;
            Message = message;
        }
        /// <summary>
        /// 执行后返回是否成功和消息
        /// </summary>
        /// <param name="success"></param>
        /// <param name="message"></param>
        public AjaxResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
