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

namespace C.O.S.E.C.Business
{
    class SystemSettingBLL : DbContext<SystemSetting>, ISystemSettingBLL
    {
        public SystemSettingBLL(AllConfigModel _allConfigModel, IEntityBaseAutoSetter _setter) : base(_allConfigModel, _setter)
        {
        }

        public bool Delete(Guid keyValue) => CurrentDb.Update(n => new SystemSetting { IsDelete = true }.Modify(keyValue, setter), n => n.ID == keyValue);

        public async Task<bool> DeleteAsync(Guid keyValue) => await Db.Updateable<SystemSetting>().SetColumns(n => n.IsDelete == true).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();

        public SystemSetting GetEntity(Guid keyValue) => CurrentDb.GetById(keyValue);

        public async Task<SystemSetting> GetEntityAsync(Guid keyValue)
        {
            try
            {
                return await Db.Queryable<SystemSetting>().InSingleAsync(keyValue);
            }
            catch (Exception)
            {
                return await Db.Queryable<SystemSetting>().FirstAsync(n => n.ID == keyValue);
            }
        }

        public List<SystemSetting> GetList(Expression<Func<SystemSetting, bool>> exp) => CurrentDb.GetList(exp);

        public async Task<List<SystemSetting>> GetListAsync(Expression<Func<SystemSetting, bool>> exp) => await Db.Queryable<SystemSetting>().Where(exp).ToListAsync();

        public List<SystemSetting> GetPageList(Pagination pagination, ref int pageCount) => Db.Queryable<SystemSetting>().OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public List<SystemSetting> GetPageList(Expression<Func<SystemSetting, bool>> exp, Pagination pagination, ref int pageCount) => Db.Queryable<SystemSetting>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public async Task<List<SystemSetting>> GetPageListAsync(Expression<Func<SystemSetting, bool>> exp, Pagination pagination, RefAsync<int> pageCount) => await Db.Queryable<SystemSetting>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageListAsync(pagination.Page, pagination.Rows, pageCount);

        public bool SaveForm(Guid keyValue, SystemSetting entity) => keyValue.IsEmpty()
                ? CurrentDb.Insert(entity.Create(setter))
                : CurrentDb.Update(n => entity.Modify(keyValue, setter), n => n.ID == keyValue);

        public async Task<bool> SaveFormAsync(Guid keyValue, SystemSetting entity) => keyValue.IsEmpty()
                ? await Db.Insertable(entity.Create(setter)).ExecuteCommandIdentityIntoEntityAsync()
                : await Db.Updateable<SystemSetting>().SetColumns(n => entity.Modify(keyValue, setter)).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();
    }
}