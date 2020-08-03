using C.O.S.E.C.Infrastructure.Treasury.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace C.O.S.E.C.Infrastructure.Treasury.Interfaces
{
    public interface IBaseBLL { }
    public interface IBaseBLL<T> : IBaseBLL where T : class, IEntity<T>, new()
    {
        List<T> GetList();
        List<T> GetList(Expression<Func<T, bool>> exp);
        List<T> GetPageList(Pagination pagination, ref int pageCount);
        List<T> GetPageList(Expression<Func<T, bool>> exp, Pagination pagination, ref int pageCount);
        T GetEntity(Guid keyValue);
        Task<T> GetEntityAsync(Guid keyValue);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> exp);
        Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> exp, Pagination pagination, SqlSugar.RefAsync<int> pageCount);
        [Obsolete("Use SaveFormAsync(Guid keyValue, T entity)")]
        bool SaveForm(Guid keyValue, T entity);
        Task<bool> SaveFormAsync(Guid keyValue, T entity);
        [Obsolete("Use DeleteAsync(Guid keyValue)")]
        bool Delete(Guid keyValue);
        Task<bool> DeleteAsync(Guid keyValue);
    }
}
