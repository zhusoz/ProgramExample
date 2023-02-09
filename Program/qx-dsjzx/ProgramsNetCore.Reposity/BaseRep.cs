using ProgramsNetCore.IReposity;
using ProgramsNetCore.Models;
using System;
using System.Linq.Expressions;
using SqlSugar;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProgramsNetCore.Reposity
{
    public class BaseRep<T> : IBaseRep<T> where T : class, new()
    {
        public SqlSugarClient db = BaseDB.GetClient();

        public async Task<int> Add(T entity)
        {
            
            return await db.Insertable(entity).ExecuteCommandAsync();
        }

        public async Task<int> Add(T entity, Expression<Func<T, object>> exo)
        {
            return await db.Insertable(entity).IgnoreColumns(exo).ExecuteCommandAsync();
        }

        public async Task<int> Adds(List<T> entitys)
        {
            return await db.Insertable(entitys).ExecuteCommandAsync();
        }

        public async Task<int> Adds(List<T> entitys, Expression<Func<T, object>> exo)
        {
            return await db.Insertable(entitys).IgnoreColumns(exo).ExecuteCommandAsync();
        }

        public async Task<int> BulkCopy(List<T> entitys, string acrossLibrary = "")
        {
            if (string.IsNullOrEmpty(acrossLibrary))
                return await db.Fastest<T>().BulkCopyAsync(entitys);
            else return await db.Fastest<T>().AS(acrossLibrary).BulkCopyAsync(entitys);
        }

        public async Task<int> BulkCopy(List<T> entitys, int pageSize = 0, string acrossLibrary = "")
        {
            await db.Utilities.PageEachAsync(entitys, pageSize, async item => {
                if (string.IsNullOrEmpty(acrossLibrary))
                    await db.Insertable<T>(entitys).ExecuteCommandAsync();
                else await db.Insertable<T>(entitys).AS(acrossLibrary).ExecuteCommandAsync();
            });
            return 1;
        }

        public async Task<int> BulkUpdate(List<T> entity,string acrossLibrary="")
        {
            if (string.IsNullOrEmpty(acrossLibrary))
                return await db.Fastest<T>().BulkUpdateAsync(entity);
            else return await db.Fastest<T>().AS(acrossLibrary).BulkUpdateAsync(entity);
            
        }

        public async Task<int> BulkUpdate(List<T> entity, int pageSize, string acrossLibrary = "")
        {
           await db.Utilities.PageEachAsync(entity, pageSize, async item => {
                if (string.IsNullOrEmpty(acrossLibrary))
                     await db.Updateable<T>(entity).ExecuteCommandAsync();
                else  await db.Updateable<T>(entity).AS(acrossLibrary).ExecuteCommandAsync();
            });
            return 1;
        }

        public bool CreateEntity(string entityName, string filePath)
        {
            try
            {
                db.DbFirst.IsCreateAttribute().Where(entityName).CreateClassFile(filePath);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + "------" + ex.Message);
                return false;
            }
        }

        public async Task<int> Delete(T entity)
        {
            return await db.Deleteable(entity).ExecuteCommandAsync();
        }

        public async Task<int> Delete(Expression<Func<T, bool>> exb)
        {
            return await db.Deleteable(exb).ExecuteCommandAsync();
        }

        public async Task<int> Deletes(int[] ids)
        {
            return await db.Deleteable<T>().In(ids).ExecuteCommandAsync();
        }

        public async Task<DataTable> GetDateTable(string sql)
        {
            return await db.SqlQueryable<DataTable>(sql).ToDataTableAsync();
        }

        public async Task<DataTable> GetDateTable(string sql, int pageIndex, int pageSize, RefAsync<int> total)
        {
            return await db.SqlQueryable<DataTable>(sql).ToDataTablePageAsync(pageIndex, pageSize, total);
        }

        public async Task<T> GetEntity(Expression<Func<T, bool>> exb)
        {
            return await db.Queryable<T>().Where(exb).FirstAsync();
        }

        public async Task<TEntity> GetEntity<TEntity>(Expression<Func<T, bool>> exb, Expression<Func<T, TEntity>> exo) where TEntity : class, new()
        {
            return await db.Queryable<T>().Where(exb).Select(exo).FirstAsync();
        }

        public async Task<T> GetEntity(Expression<Func<T, bool>> exb, string asdatabase)
        {
            return await db.Queryable<T>().AS(asdatabase).FirstAsync(exb);
        }

        public async Task<List<T>> GetEntitys(Expression<Func<T, bool>> exb)
        {
            return await db.Queryable<T>().Where(exb).ToListAsync();
        }

        public async Task<List<T>> GetEntitys(Expression<Func<T, bool>> exb, int pageIndex, int pageSize, RefAsync<int> total)
        {
            return await db.Queryable<T>().Where(exb).ToPageListAsync(pageIndex, pageSize, total);
        }

        public async Task<List<T>> GetEntitys(Expression<Func<T, bool>> exb, Expression<Func<T, T>> exo)
        {
            return await db.Queryable<T>().Where(exb).Select(exo).ToListAsync();
        }

        public Task<List<T>> GetEntitys(Expression<Func<T, bool>> exb, Expression<Func<T, object>> exo, int pageIndex, int pageSize, RefAsync<int> total)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetEntitys(Expression<Func<T, bool>> exb, Expression<Func<T, object>> exo)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TEntity>> GetEntitys<TEntity>(Expression<Func<T, bool>> exb, Expression<Func<T, TEntity>> exo) where TEntity : class, new()
        {
            return await db.Queryable<T>().Where(exb).Select(exo).ToListAsync();
        }

        public async Task<List<TEntity>> GetEntitys<TEntity>(Expression<Func<T, bool>> exb, int pageIndex, int pageSize, RefAsync<int> total, Expression<Func<T, TEntity>> exo) where TEntity : class, new()
        {
            return await db.Queryable<T>().Where(exb).Select(exo).ToPageListAsync(pageIndex, pageSize, total);
        }

        public async Task<List<T>> GetEntitys(Expression<Func<T, bool>> exb, string asdatabase)
        {
            return await db.Queryable<T>().AS(asdatabase).Where(exb).ToListAsync();
        }

        public async Task<List<T>> GetEntitys(Expression<Func<T, bool>> exb, string asdatabase, int pageIndex, int pageSize, RefAsync<int> total)
        {
            return await db.Queryable<T>().AS(asdatabase).Where(exb).ToPageListAsync(pageIndex, pageSize, total);
        }

        public async Task<int> IdAdd(T entity)
        {
            return await db.Insertable(entity).ExecuteReturnIdentityAsync();
        }

        public async Task<int> Update(T entity)
        {
            return await db.Updateable(entity).ExecuteCommandAsync();
        }

        public async Task<int> Update(T entity, Expression<Func<T, object>> exb)
        {
            return await db.Updateable(entity).IgnoreColumns(exb).ExecuteCommandAsync();
        }

        public async Task<int> UpdateColumn(T entity, Expression<Func<T, object>> exb)
        {
            return await db.Updateable(entity).UpdateColumns(exb).ExecuteCommandAsync();
        }

        public async Task<T> ValueAdd(T entity)
        {
            return await db.Insertable(entity).ExecuteReturnEntityAsync();
        }



        public async Task<T> ValueAdd(T entity, Expression<Func<T, object>> exo)
        {
            return await db.Insertable(entity).IgnoreColumns(exo).ExecuteReturnEntityAsync();
        }
        
    }
}
