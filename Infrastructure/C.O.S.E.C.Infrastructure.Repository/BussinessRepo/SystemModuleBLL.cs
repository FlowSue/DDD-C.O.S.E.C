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
    sealed class SystemModuleBLL : DbContext<SystemModule>, ISystemModuleBLL
    {
        public SystemModuleBLL(AllConfigModel _allConfigModel, IEntityBaseAutoSetter _setter) : base(_allConfigModel, _setter)
        {
        }

        public bool Delete(Guid keyValue) => CurrentDb.Update(n => new SystemModule { IsDelete = true }.Modify(keyValue, setter), n => n.ID == keyValue);

        public async Task<bool> DeleteAsync(Guid keyValue) => await Db.Updateable<SystemModule>().SetColumns(n => n.IsDelete == true).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();

        public SystemModule GetEntity(Guid keyValue) => CurrentDb.GetById(keyValue);

        public async Task<SystemModule> GetEntityAsync(Guid keyValue)
        {
            try
            {
                return await Db.Queryable<SystemModule>().InSingleAsync(keyValue);
            }
            catch (Exception)
            {
                return await Db.Queryable<SystemModule>().FirstAsync(n => n.ID == keyValue);
            }
        }

        public List<SystemModule> GetList(Expression<Func<SystemModule, bool>> exp) => CurrentDb.GetList(exp);

        public async Task<List<SystemModule>> GetListAsync(Expression<Func<SystemModule, bool>> exp) => await Db.Queryable<SystemModule>().Where(exp).ToListAsync();

        public List<SystemModule> GetPageList(Pagination pagination, ref int pageCount) => Db.Queryable<SystemModule>().OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public List<SystemModule> GetPageList(Expression<Func<SystemModule, bool>> exp, Pagination pagination, ref int pageCount) => Db.Queryable<SystemModule>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public async Task<List<SystemModule>> GetPageListAsync(Expression<Func<SystemModule, bool>> exp, Pagination pagination, RefAsync<int> pageCount) => await Db.Queryable<SystemModule>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageListAsync(pagination.Page, pagination.Rows, pageCount);

        public bool SaveForm(Guid keyValue, SystemModule entity) => keyValue.IsEmpty()
                ? CurrentDb.Insert(entity.Create(setter))
                : CurrentDb.Update(n => entity.Modify(keyValue, setter), n => n.ID == keyValue);

        public async Task<bool> SaveFormAsync(Guid keyValue, SystemModule entity) => keyValue.IsEmpty()
                ? await Db.Insertable(entity.Create(setter)).ExecuteCommandIdentityIntoEntityAsync()
                : await Db.Updateable<SystemModule>().SetColumns(n => entity.Modify(keyValue, setter)).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();
    }
}
