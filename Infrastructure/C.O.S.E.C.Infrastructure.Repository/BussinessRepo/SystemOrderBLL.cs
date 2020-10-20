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
    sealed class SystemOrderBLL : DbContext<SystemOrder>, ISystemOrderBLL
    {
        public SystemOrderBLL(AllConfigModel _allConfigModel, IEntityBaseAutoSetter _setter) : base(_allConfigModel, _setter)
        {
        }

        public bool Delete(Guid keyValue) => CurrentDb.Update(n => new SystemOrder { IsDelete = true }.Modify(keyValue, setter), n => n.ID == keyValue);

        public async Task<bool> DeleteAsync(Guid keyValue) => await Db.Updateable<SystemOrder>().SetColumns(n => n.IsDelete == true).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();

        public SystemOrder GetEntity(Guid keyValue) => CurrentDb.GetById(keyValue);

        public async Task<SystemOrder> GetEntityAsync(Guid keyValue)
        {
            try
            {
                return await Db.Queryable<SystemOrder>().InSingleAsync(keyValue);
            }
            catch (Exception)
            {
                return await Db.Queryable<SystemOrder>().FirstAsync(n => n.ID == keyValue);
            }
        }

        public List<SystemOrder> GetList(Expression<Func<SystemOrder, bool>> exp) => CurrentDb.GetList(exp);

        public async Task<List<SystemOrder>> GetListAsync(Expression<Func<SystemOrder, bool>> exp) => await Db.Queryable<SystemOrder>().Where(exp).ToListAsync();

        public List<SystemOrder> GetPageList(Pagination pagination, ref int pageCount) => Db.Queryable<SystemOrder>().ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public List<SystemOrder> GetPageList(Expression<Func<SystemOrder, bool>> exp, Pagination pagination, ref int pageCount) => Db.Queryable<SystemOrder>().Where(exp).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public async Task<List<SystemOrder>> GetPageListAsync(Expression<Func<SystemOrder, bool>> exp, Pagination pagination, RefAsync<int> pageCount) => await Db.Queryable<SystemOrder>().Where(exp).ToPageListAsync(pagination.Page, pagination.Rows, pageCount);

        public bool SaveForm(Guid keyValue, SystemOrder entity) => keyValue.IsEmpty()
                ? CurrentDb.Insert(entity.Create(setter))
                : CurrentDb.Update(n => entity.Modify(keyValue, setter), n => n.ID == keyValue);

        public async Task<bool> SaveFormAsync(Guid keyValue, SystemOrder entity) => keyValue.IsEmpty()
                ? await Db.Insertable(entity.Create(setter)).ExecuteCommandIdentityIntoEntityAsync()
                : await Db.Updateable<SystemOrder>().SetColumns(n => entity.Modify(keyValue, setter)).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();
    }
}
