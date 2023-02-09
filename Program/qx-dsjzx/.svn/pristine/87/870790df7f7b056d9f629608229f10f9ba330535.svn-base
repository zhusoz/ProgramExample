using ProgramsNetCore.IReposity;
using ProgramsNetCore.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProgramsNetCore.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        public IBaseRep<TEntity> _dal { get; set; }

        public async Task<bool> Add(TEntity entity)
        {
            return await _dal.Add(entity)>0;
        }

        public async Task<bool> Add(TEntity entity, Expression<Func<TEntity, object>> exo)
        {
            return await _dal.Add(entity) > 0;
        }

        public async Task<int> Adds(List<TEntity> entitys)
        {
            return await _dal.Adds(entitys);
        }

        public async Task<int> Adds(List<TEntity> entitys, Expression<Func<TEntity, object>> exo)
        {
            return await _dal.Adds(entitys, exo);
        }

        public async Task<bool> Delete(TEntity entity)
        {
            return await _dal.Delete(entity) > 0;
        }

        public async Task<bool> Delete(Expression<Func<TEntity, bool>> exb)
        {
           return await _dal.Delete(exb) > 0;
        }

        public async Task<bool> Deletes(int[] ids)
        {
           return await _dal.Deletes(ids) > 0;
        }

        public async Task<DataTable> GetDateTable(string sql)
        {
           return await _dal.GetDateTable(sql);
        }

        public async Task<DataTable> GetDateTable(string sql, int pageIndex, int pageSize, RefAsync<int> total)
        {
            return await _dal.GetDateTable(sql, pageIndex, pageSize, total);
        }

        public async Task<TEntity> GetEntity(Expression<Func<TEntity, bool>> exb)
        {
            return await _dal.GetEntity(exb);
        }

        public async Task<TEntity1> GetEntity<TEntity1>(Expression<Func<TEntity, bool>> exb, Expression<Func<TEntity, TEntity1>> exo) where TEntity1 : class, new()
        {
            return await _dal.GetEntity<TEntity1>(exb, exo);
        }

        public async Task<TEntity> GetEntity(Expression<Func<TEntity, bool>> exb, string asdatabase)
        {
            return await _dal.GetEntity(exb, asdatabase);
        }

        public async Task<List<TEntity>> GetEntitys(Expression<Func<TEntity, bool>> exb)
        {
           return await _dal.GetEntitys(exb);
        }

        public async Task<List<TEntity>> GetEntitys(Expression<Func<TEntity, bool>> exb, int pageIndex, int pageSize, RefAsync<int> total)
        {
            return await _dal.GetEntitys(exb, pageIndex, pageSize, total);
        }

        public async Task<List<TEntity1>> GetEntitys<TEntity1>(Expression<Func<TEntity, bool>> exb, Expression<Func<TEntity, TEntity1>> exo) where TEntity1 : class, new()
        {
            return await _dal.GetEntitys<TEntity1>(exb, exo);
        }

        public async Task<List<TEntity1>> GetEntitys<TEntity1>(Expression<Func<TEntity, bool>> exb, int pageIndex, int pageSize, RefAsync<int> total, Expression<Func<TEntity, TEntity1>> exo) where TEntity1 : class, new()
        {
            return await _dal.GetEntitys<TEntity1>(exb, pageIndex, pageSize, total, exo);


         }

        public async Task<List<TEntity>> GetEntitys(Expression<Func<TEntity, bool>> exb, string asdatabase)
        {
            return await _dal.GetEntitys(exb, asdatabase);
        }

        public async Task<List<TEntity>> GetEntitys(Expression<Func<TEntity, bool>> exb, string asdatabase, int pageIndex, int pageSize, RefAsync<int> total)
        {
            return await _dal.GetEntitys(exb, asdatabase, pageIndex, pageSize, total);
        }

        public async Task<int> IdAdd(TEntity entity)
        {
           return await _dal.IdAdd(entity);
        }

        public async Task<bool> Update(TEntity entity)
        {
           return await _dal.Update(entity)>0;
        }

        public async Task<bool> Update(TEntity entity, Expression<Func<TEntity, object>> exb)
        {
            return await _dal.Update(entity, exb)>0;
        }

        public async Task<int> UpdateColumn(TEntity entity, Expression<Func<TEntity, object>> exb)
        {
            return await _dal.UpdateColumn(entity, exb);
        }

        public async Task<TEntity> ValueAdd(TEntity entity)
        {
            return await _dal.ValueAdd(entity);
        }
    }
}
