using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProgramsNetCore.IReposity
{
    public interface IBaseRep<T>
    {
        #region 增       ↓---↓---↓---↓

        /// <summary>
        /// 增加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> Add(T entity);
        /// <summary>
        /// 增加多个实体
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        Task<int> Adds(List<T> entitys);
        /// <summary>
        /// 批量复制
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        Task<int> BulkCopy(List<T> entitys, string acrossLibrary = "");
        /// <summary>
        /// 分页批量复制
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        Task<int> BulkCopy(List<T> entitys, int pageSize = 0, string acrossLibrary = "");
        /// <summary>
        /// 增加多个实体并选择不添加列
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        Task<int> Adds(List<T> entitys, Expression<Func<T, object>> exo);
        /// <summary>
        /// 增加并返回实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> ValueAdd(T entity);
        /// <summary>
        /// 增加并返回实体,选择不添加列
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="exo"></param>
        /// <returns></returns>
        Task<T> ValueAdd(T entity, Expression<Func<T, object>> exo);
        /// <summary>
        /// 增加实体返回自增列
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> IdAdd(T entity);
        /// <summary>
        /// 增加实体，选择不添加列
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="exo"></param>
        /// <returns></returns>
        Task<int> Add(T entity, Expression<Func<T, object>> exo);

        #endregion       ↑---↑---↑---↑

        #region 删       ↓---↓---↓---↓
        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> Delete(T entity);
        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="exb"></param>
        /// <returns></returns>
        Task<int> Delete(Expression<Func<T, bool>> exb);

        /// <summary>
        /// 根据ID批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<int> Deletes(int[] ids);
        #endregion       ↑---↑---↑---↑

        #region 改       ↓---↓---↓---↓

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> Update(T entity);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
         Task<int> BulkUpdate(List<T> entity, string acrossLibrary = "");
        /// <summary>
        /// 分页批量修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
         Task<int> BulkUpdate(List<T> entity, int pageSize, string acrossLibrary = "");
        /// <summary>
        /// 修改并指定不修改列
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="exb"></param>
        /// <returns></returns>
        Task<int> Update(T entity, Expression<Func<T, object>> exb);
        /// <summary>
        /// 修改并指定修改列
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="exb"></param>
        /// <returns></returns>
        Task<int> UpdateColumn(T entity, Expression<Func<T, object>> exb);

        #endregion       ↑---↑---↑---↑

        #region 查       ↓---↓---↓---↓

        /// <summary>
        /// 查询多行
        /// </summary>
        /// <param name="exb"></param>
        /// <returns></returns>
        Task<List<T>> GetEntitys(Expression<Func<T, bool>> exb);
        /// <summary>
        /// 查询多行
        /// </summary>
        /// <param name="exb"></param>
        /// <returns></returns>
        Task<List<T>> GetEntitys(Expression<Func<T, bool>> exb,string asdatabase);
        /// <summary>
        /// 查询多行,选择查询字段
        /// </summary>
        /// <param name="exb"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetEntitys<TEntity>(Expression<Func<T, bool>> exb,Expression<Func<T, TEntity>> exo) where TEntity:class,new () ;
        /// <summary>
        /// 分页查询多行
        /// </summary>
        /// <param name="exb"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<T>> GetEntitys(Expression<Func<T, bool>> exb, int pageIndex, int pageSize, RefAsync<int> total);
        /// <summary>
        /// 分页查询多行,并指定数据库
        /// </summary>
        /// <param name="exb"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<T>> GetEntitys(Expression<Func<T, bool>> exb,string asdatabase, int pageIndex, int pageSize, RefAsync<int> total);
        /// <summary>
        /// 分页查询多行,选择查询字段
        /// </summary>
        /// <param name="exb"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<TEntity>> GetEntitys<TEntity>(Expression<Func<T, bool>> exb, int pageIndex, int pageSize, RefAsync<int> total, Expression<Func<T, TEntity>> exo) where TEntity : class, new();
        /// <summary>
        /// 查询单行
        /// </summary>
        /// <param name="exb"></param>
        /// <returns></returns>
        Task<T> GetEntity(Expression<Func<T, bool>> exb);
        /// <summary>
        /// 查询单行，并指定库
        /// </summary>
        /// <param name="exb"></param>
        /// <returns></returns>
        Task<T> GetEntity(Expression<Func<T, bool>> exb,string asdatabase);
        /// <summary>
        /// 查询单行,选择查询字段
        /// </summary>
        /// <param name="exb"></param>
        /// <returns></returns>
        Task<TEntity> GetEntity<TEntity>(Expression<Func<T, bool>> exb, Expression<Func<T, TEntity>> exo) where TEntity : class, new();
        /// <summary>
        ///  根据sql语句获取DateTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<DataTable> GetDateTable(string sql);
        /// <summary>
        /// 根据sql语句分页获取DateTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        Task<DataTable> GetDateTable(string sql, int pageIndex, int pageSize, RefAsync<int> total);

        #endregion       ↑---↑---↑---↑

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="entityName">实体名称</param>
        /// <param name="filePath">目标路径</param>
        /// <returns></returns>
        bool CreateEntity(string entityName, string filePath);
    }
}
