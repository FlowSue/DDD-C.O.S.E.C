namespace C.O.S.E.C.Domain.InterfaceDrivers
{
    using global::C.O.S.E.C.Domain.Entity;
    using global::C.O.S.E.C.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// 依赖注入所用接口
    /// </summary>
    public interface IBaseBLL
    {
    }

    /// <summary>
    /// 通用业务接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseBLL<T> : IBaseBLL where T : BaseEntityModel, new()
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        List<T> GetList();

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="exp">查询表达式</param>
        /// <returns></returns>
        List<T> GetList(Expression<Func<T, bool>> exp);

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination">分页参数Model</param>
        /// <param name="pageCount">页码</param>
        /// <returns></returns>
        List<T> GetPageList(Pagination pagination, ref int pageCount);

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="exp">查询表达式</param>
        /// <param name="pagination">分页参数Model</param>
        /// <param name="pageCount">页码</param>
        /// <returns></returns>
        List<T> GetPageList(Expression<Func<T, bool>> exp, Pagination pagination, ref int pageCount);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键ID</param>
        /// <returns></returns>
        T GetEntity(Guid keyValue);

        /// <summary>
        /// 获取实体（异步）
        /// </summary>
        /// <param name="keyValue">主键ID</param>
        /// <returns></returns>
        Task<T> GetEntityAsync(Guid keyValue);

        /// <summary>
        /// 获取列表（异步）
        /// </summary>
        /// <param name="exp">查询表达式</param>
        /// <returns></returns>
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> exp);

        /// <summary>
        /// 获取分页列表（异步）
        /// </summary>
        /// <param name="exp">查询表达式</param>
        /// <param name="pagination">分页参数Model</param>
        /// <param name="pageCount">页码</param>
        /// <returns></returns>
        Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> exp, Pagination pagination, SqlSugar.RefAsync<int> pageCount);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="keyValue">主键ID，空为新增，否则为修改</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [Obsolete("Use SaveFormAsync(Guid keyValue, T entity)")]
        bool SaveForm(Guid keyValue, T entity);

        /// <summary>
        /// 保存（异步）
        /// </summary>
        /// <param name="keyValue">主键ID，空为新增，否则为修改</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<bool> SaveFormAsync(Guid keyValue, T entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [Obsolete("Use DeleteAsync(Guid keyValue)")]
        bool Delete(Guid keyValue);

        /// <summary>
        /// 删除（异步）
        /// </summary>
        /// <param name="keyValue">主键ID</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Guid keyValue);
    }
}
