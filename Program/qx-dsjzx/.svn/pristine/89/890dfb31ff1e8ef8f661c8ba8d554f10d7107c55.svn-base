using ProgramsNetCore.IReposity;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using ProgramsNetCore.Models;

namespace ProgramsNetCore.Reposity
{
    public class DataTableRep : IDataTableRep
    {
        SqlSugarScope db = BaseDB.Db;

        public async Task<int> BulkCopyDataTable(DataTable dataTable, string tableName)
        {
            return await db.Fastest<DataTable>().AS(tableName).BulkCopyAsync(dataTable);
        }

        public async Task<int> BulkCopyDataTable(DataTable dataTable, string tableName, int PageSize)
        {
            return await db.Fastest<DataTable>().PageSize(PageSize).AS(tableName).BulkCopyAsync(dataTable);
        }

        public async Task<DataTable> GetDataTable(string sql)
        {

            return await db.SqlQueryable<DataTable>(sql).ToDataTableAsync();
        }

        public async Task<DataTable> GetDataTable(string sql, int pageIndex, int pageSize, RefAsync<int> total)
        {
            return await db.SqlQueryable<DataTable>(sql).ToDataTablePageAsync(pageIndex, pageSize, total);
        }

        public async Task<T> GetEntity<T>(string sql) where T : class, new()
        {
            return await db.SqlQueryable<T>(sql).FirstAsync();
        }

        public async Task<List<T>> GetEntityList<T>(string sql) where T : class, new()
        {
            return await db.SqlQueryable<T>(sql).ToListAsync();
        }

        public async Task<List<T>> GetEntityPageList<T>(string sql, int pageIndex, int pageSize, RefAsync<int> total) where T : class, new()
        {
            return await db.SqlQueryable<T>(sql).ToPageListAsync(pageIndex, pageSize, total);
        }

        public async Task<int> NoQueryExcuteSql(string sql)
        {
            return await db.Ado.ExecuteCommandAsync(sql);
        }

        public async Task<object> QueryCount(string sql)
        {
            return await db.Ado.GetScalarAsync(sql);
        }
    }
}
