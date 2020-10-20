using C.O.S.E.C.Domain.Entity;
using C.O.S.E.C.Domain.Factory;
using C.O.S.E.C.Domain.InterfaceDrivers.Business;
using C.O.S.E.C.Domain.InterfaceDrivers.Services;
using C.O.S.E.C.Domain.Models;
using C.O.S.E.C.Infrastructure.Config;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace C.O.S.E.C.Infrastructure.Repository.BussinessRepo
{
    public class _SystemActionLogBLL : DbContext<_SystemActionLog>, _ISystemActionLogBLL
    {
        public _SystemActionLogBLL(AllConfigModel _allConfigModel, IEntityBaseAutoSetter _setter) : base(_allConfigModel, _setter)
        {
        }

        public bool Delete(Guid keyValue) => CurrentDb.Update(n => new _SystemActionLog { IsDelete = true }.Modify(keyValue, setter), n => n.ID == keyValue);

        public async Task<bool> DeleteAsync(Guid keyValue) => await Db.Updateable<_SystemActionLog>().SetColumns(n => n.IsDelete == true).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();

        public _SystemActionLog GetEntity(Guid keyValue) => CurrentDb.GetById(keyValue);

        public async Task<_SystemActionLog> GetEntityAsync(Guid keyValue)
        {
            try
            {
                return await Db.Queryable<_SystemActionLog>().InSingleAsync(keyValue);
            }
            catch (Exception)
            {
                return await Db.Queryable<_SystemActionLog>().FirstAsync(n => n.ID == keyValue);
            }
        }

        public List<_SystemActionLog> GetList(Expression<Func<_SystemActionLog, bool>> exp) => CurrentDb.GetList(exp);

        public async Task<List<_SystemActionLog>> GetListAsync(Expression<Func<_SystemActionLog, bool>> exp) => await Db.Queryable<_SystemActionLog>().Where(exp).ToListAsync();

        public List<_SystemActionLog> GetPageList(Pagination pagination, ref int pageCount) => Db.Queryable<_SystemActionLog>().OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public List<_SystemActionLog> GetPageList(Expression<Func<_SystemActionLog, bool>> exp, Pagination pagination, ref int pageCount) => Db.Queryable<_SystemActionLog>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public async Task<List<_SystemActionLog>> GetPageListAsync(Expression<Func<_SystemActionLog, bool>> exp, Pagination pagination, RefAsync<int> pageCount) => await Db.Queryable<_SystemActionLog>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageListAsync(pagination.Page, pagination.Rows, pageCount);

        public bool SaveForm(Guid keyValue, _SystemActionLog entity) => keyValue.IsEmpty()
                ? CurrentDb.Insert(entity.Create(setter))
                : CurrentDb.Update(n => entity.Modify(keyValue, setter), n => n.ID == keyValue);

        public async Task<bool> SaveFormAsync(Guid keyValue, _SystemActionLog entity) => keyValue.IsEmpty()
? await Db.Insertable(entity.Create(setter)).ExecuteCommandIdentityIntoEntityAsync()
: await Db.Updateable<_SystemActionLog>().SetColumns(n => entity.Modify(keyValue, setter)).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();
    }
}
