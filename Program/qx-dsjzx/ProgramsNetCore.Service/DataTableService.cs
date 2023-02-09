using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.Service
{
    public class DataTableService : IDataTableService
    {
        private readonly IDataTableRep _dal;
        public DataTableService(IDataTableRep dal)
        {
            _dal = dal;
        }

        public async Task<int> BulkCopyDataTable(DataTable dataTable, string tableName)
        {
           return await _dal.BulkCopyDataTable(dataTable, tableName);
        }

        public async Task<int> BulkCopyDataTable(DataTable dataTable, string tableName, int PageSize)
        {
            return await _dal.BulkCopyDataTable(dataTable,tableName,PageSize);
        }

        public async Task<DataTable> GetDataTable(string sql)
        {
          return await _dal.GetDataTable(sql);
        }

        public Task<DataTable> GetDataTable(string sql, int pageIndex, int pageSize, RefAsync<int> total)
        {
           return _dal.GetDataTable(sql, pageIndex, pageSize, total);
        }

        public Task<T> GetEntity<T>(string sql) where T : class, new()
        {
           return _dal.GetEntity<T>(sql);
        }

        public async Task<List<T>> GetEntityList<T>(string sql) where T : class, new()
        {
            return await _dal.GetEntityList<T>(sql);
        }

        public async Task<List<T>> GetEntityPageList<T>(string sql, int pageIndex, int pageSize, RefAsync<int> total) where T : class, new()
        {
           return await _dal.GetEntityPageList<T>(sql,pageIndex, pageSize, total);
        }

        public async Task<int> NoQueryExcuteSql(string sql)
        {
            return await _dal.NoQueryExcuteSql(sql);
        }

        public async Task<object> QueryCount(string sql)
        {
            return await _dal.QueryCount(sql);
        }
    }
}
