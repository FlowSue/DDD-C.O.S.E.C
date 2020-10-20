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
    public class UserFileBLL : DbContext<UserFile>, IUserFileBLL
    {
        public UserFileBLL(AllConfigModel _allConfigModel, IEntityBaseAutoSetter _setter) : base(_allConfigModel, _setter)
        {
        }

        public bool Delete(Guid keyValue) => CurrentDb.Update(n => new UserFile() { IsDelete = true }.Modify(keyValue, setter), n => n.ID == keyValue);

        public async Task<bool> DeleteAsync(Guid keyValue) => await Db.Updateable<UserFile>().SetColumns(n => n.IsDelete == true).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();

        public UserFile GetEntity(Guid keyValue) => CurrentDb.GetById(keyValue);

        public async Task<UserFile> GetEntityAsync(Guid keyValue)
        {
            try
            {
                return await Db.Queryable<UserFile>().InSingleAsync(keyValue);
            }
            catch (Exception)
            {
                return await Db.Queryable<UserFile>().FirstAsync(n => n.ID == keyValue);
            }
        }

        public List<UserFile> GetList(Expression<Func<UserFile, bool>> exp) => CurrentDb.GetList(exp);

        public async Task<List<UserFile>> GetListAsync(Expression<Func<UserFile, bool>> exp) => await Db.Queryable<UserFile>().Where(exp).ToListAsync();

        public List<UserFile> GetPageList(Pagination pagination, ref int pageCount) => Db.Queryable<UserFile>().OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public List<UserFile> GetPageList(Expression<Func<UserFile, bool>> exp, Pagination pagination, ref int pageCount) => Db.Queryable<UserFile>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public async Task<List<UserFile>> GetPageListAsync(Expression<Func<UserFile, bool>> exp, Pagination pagination, RefAsync<int> pageCount) => await Db.Queryable<UserFile>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageListAsync(pagination.Page, pagination.Rows, pageCount);

        public bool SaveForm(Guid keyValue, UserFile entity) => keyValue.IsEmpty() ? CurrentDb.Insert(entity.Create(setter)) : CurrentDb.Update(n => entity.Modify(keyValue, setter), n => n.ID == keyValue);

        public async Task<bool> SaveFormAsync(Guid keyValue, UserFile entity) => keyValue.IsEmpty()
                ? await Db.Insertable(entity.Create(setter)).ExecuteCommandIdentityIntoEntityAsync()
                : await Db.Updateable<UserFile>().SetColumns(n => entity.Modify(keyValue, setter)).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();
    }
}
