using Org.BouncyCastle.Crypto;
using System.Collections.Generic;
using System;

namespace ProgramsNetCore.Common.Basic
{
    /// <summary>
    /// 分页数据
    /// </summary>
    public class PageResult<T>
    {
        /// <summary>
        /// 用户数据
        /// </summary>
        public T Data { get; set; }
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
        /// 信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PageResult()
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
        public PageResult(T data, int total, int totalPage, int pageSize, int pageNumber)
        {
            Data = data;
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
        public PageResult(T data, int total, int totalPage)
        {
            Data = data;
            Total = total;
            TotalPage = totalPage;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="total"></param>
        public PageResult(T data, int total)
        {
            Data = data;
            Total = total;
        }
        public PageResult(string msg)
        {
            Msg = msg;
        }
    }
}
