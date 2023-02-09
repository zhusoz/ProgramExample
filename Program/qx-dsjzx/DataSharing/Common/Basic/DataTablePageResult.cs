using System.Data;

namespace ProblemDisposal.Common.Basic
{
    public class DataTablePageResult
    {
        /// <summary>
        /// 用户数据
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage { get; set; }
        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DataTablePageResult()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="total"></param>
        /// <param name="totalPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        public DataTablePageResult(DataTable data, int total, int totalPage, int pageSize, int pageNumber)
        {
            Data = JsonSerialize(data);
            Total = total;
            TotalPage = totalPage;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="total"></param>
        /// <param name="totalPage"></param>
        public DataTablePageResult(DataTable data, int total, int totalPage)
        {
            Data = JsonSerialize(data);
            Total = total;
            TotalPage = totalPage;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="total"></param>
        public DataTablePageResult(DataTable data, int total)
        {
            Data = JsonSerialize(data);
            Total = total;
        }

        string JsonSerialize( DataTable t)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(t, Newtonsoft.Json.Formatting.Indented);
        }
    }
}
