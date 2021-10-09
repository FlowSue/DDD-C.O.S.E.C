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
    class BusinessPoolBLL : DbContext<BusinessPool>, IBusinessPoolBLL
    {
        public BusinessPoolBLL(AllConfigModel allConfigModel, IEntityBaseAutoSetter setter) : base(allConfigModel, setter)
        {
        }

        public bool ConversionCustomer(Guid keyValue)
        {
            var result = Db.Ado.UseTran(() =>
            {
                var business = CurrentDb.GetById(keyValue);
                Db.Insertable(((Customer)business).Create(setter)).ExecuteCommand();
                Db.Updateable(business.Modify(keyValue, setter)).SetColumns(n => n.IsDelete == true && n.Status == default).ExecuteCommand();
            });
            if (result.IsSuccess)
            {
                return result.Data;
            }
            else
                throw ExceptionEx.ThrowDataAccessException(result.ErrorException, result.ErrorMessage);
        }

        public bool Delete(Guid keyValue) => CurrentDb.Update(n => new BusinessPool { IsDelete = true }.Modify(keyValue, setter), n => n.ID == keyValue);

        public async Task<bool> DeleteAsync(Guid keyValue) => await Db.Updateable<BusinessPool>().SetColumns(n => n.IsDelete == true).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();

        public BusinessPool GetEntity(Guid keyValue) => CurrentDb.GetById(keyValue);

        public async Task<BusinessPool> GetEntityAsync(Guid keyValue)
        {
            try
            {
                return await Db.Queryable<BusinessPool>().InSingleAsync(keyValue);
            }
            catch (Exception)
            {
                return await Db.Queryable<BusinessPool>().FirstAsync(n => n.ID == keyValue);
            }
        }

        public List<BusinessPool> GetList(Expression<Func<BusinessPool, bool>> exp) => CurrentDb.GetList(exp);

        public async Task<List<BusinessPool>> GetListAsync(Expression<Func<BusinessPool, bool>> exp) => await Db.Queryable<BusinessPool>().Where(exp).ToListAsync();

        public List<BusinessPool> GetPageList(Pagination pagination, ref int pageCount) => Db.Queryable<BusinessPool>().OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public List<BusinessPool> GetPageList(Expression<Func<BusinessPool, bool>> exp, Pagination pagination, ref int pageCount) => Db.Queryable<BusinessPool>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public async Task<List<BusinessPool>> GetPageListAsync(Expression<Func<BusinessPool, bool>> exp, Pagination pagination, RefAsync<int> pageCount) => await Db.Queryable<BusinessPool>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageListAsync(pagination.Page, pagination.Rows, pageCount);

        public bool Invalid(Guid id) => CurrentDb.Update(n => new BusinessPool { Status = default }.Modify(id, setter), n => n.ID == id);

        public bool SaveForm(Guid keyValue, BusinessPool entity) => keyValue.IsEmpty()
                ? CurrentDb.Insert(entity.Create(setter))
                : CurrentDb.Update(n => entity.Modify(keyValue, setter), n => n.ID == keyValue);

        public async Task<bool> SaveFormAsync(Guid keyValue, BusinessPool entity) => keyValue.IsEmpty()
                ? await Db.Insertable(entity.Create(setter)).ExecuteCommandIdentityIntoEntityAsync()
                : await Db.Updateable<BusinessPool>().SetColumns(n => entity.Modify(keyValue, setter)).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();
    }
}
