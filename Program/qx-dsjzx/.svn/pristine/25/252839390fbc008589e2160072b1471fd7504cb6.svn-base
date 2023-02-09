using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.IService
{
    public interface IDataTableService
    {
        /// <summary>
        /// 通过SQL语句获取DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<DataTable> GetDataTable(string sql);
        /// <summary>
        /// 通过SQL语句分页获取DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        Task<DataTable> GetDataTable(string sql,int pageIndex,int pageSize,RefAsync<int> total);

        Task<int> NoQueryExcuteSql(string sql);
        /// <summary>
        /// 首行首列
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<object> QueryCount(string sql);


        /// <summary>
        /// 根据SQL语句获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<T> GetEntity<T>(string sql) where T : class, new();

        /// <summary>
        /// 根据SQL语句获取提示集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<List<T>> GetEntityList<T>(string sql) where T : class, new();
        /// <summary>
        /// 根据SQL语句分页获取实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        Task<List<T>> GetEntityPageList<T>(string sql, int pageIndex, int pageSize, RefAsync<int> total) where T : class, new();
        /// <summary>
        /// 大数据导入DataTable数据
        /// </summary>
        /// <param name="dataTable">数据源</param>
        /// <param name="tableName">数据表</param>
        /// <returns></returns>
        Task<int> BulkCopyDataTable(DataTable dataTable, string tableName);

        /// <summary>
        /// 大数据分页导入DataTable数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="tableName"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        Task<int> BulkCopyDataTable(DataTable dataTable, string tableName, int PageSize);
    }
}
