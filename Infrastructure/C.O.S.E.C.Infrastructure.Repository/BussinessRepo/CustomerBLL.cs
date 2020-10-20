using C.O.S.E.C.Domain.Entity;
using C.O.S.E.C.Domain.Enums;
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
    class CustomerBLL : DbContext<Customer>, ICustomerBLL
    {
        public CustomerBLL(AllConfigModel _allConfigModel, IEntityBaseAutoSetter _setter) : base(_allConfigModel, _setter)
        {
        }

        public bool Delete(Guid keyValue) => CurrentDb.Update(n => new Customer { IsDelete = true }.Modify(keyValue, setter), n => n.ID == keyValue);

        public async Task<bool> DeleteAsync(Guid keyValue) => await Db.Updateable<Customer>().SetColumns(n => n.IsDelete == true).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();

        public Customer GetEntity(Guid keyValue) => CurrentDb.GetById(keyValue);

        public async Task<Customer> GetEntityAsync(Guid keyValue)
        {
            try
            {
                return await Db.Queryable<Customer>().InSingleAsync(keyValue);
            }
            catch (Exception)
            {
                return await Db.Queryable<Customer>().FirstAsync(n => n.ID == keyValue);
            }
        }

        public List<Customer> GetList(Expression<Func<Customer, bool>> exp) => CurrentDb.GetList(exp);

        public async Task<List<Customer>> GetListAsync(Expression<Func<Customer, bool>> exp) => await Db.Queryable<Customer>().Where(exp).ToListAsync();

        public List<Customer> GetPageList(Pagination pagination, ref int pageCount) => Db.Queryable<Customer>().OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public List<Customer> GetPageList(Expression<Func<Customer, bool>> exp, Pagination pagination, ref int pageCount) => Db.Queryable<Customer>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public async Task<List<Customer>> GetPageListAsync(Expression<Func<Customer, bool>> exp, Pagination pagination, RefAsync<int> pageCount) => await Db.Queryable<Customer>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageListAsync(pagination.Page, pagination.Rows, pageCount);

        public bool Invalid(Guid id) => CurrentDb.Update(n => new Customer { Status = StatusState.Invalid }.Modify(id, setter), n => n.ID == id);

        public bool RangeDelete(List<Customer> customers)
        {
            customers.Each(n => n.Modify(n.ID, setter).IsDelete = true);
            return CurrentDb.UpdateRange(customers);
        }

        public bool SaveForm(Guid keyValue, Customer entity) => keyValue.IsEmpty()
                ? CurrentDb.Insert(entity.Create(setter))
                : CurrentDb.Update(n => entity.Modify(keyValue, setter), n => n.ID == keyValue);

        public async Task<bool> SaveFormAsync(Guid keyValue, Customer entity) => keyValue.IsEmpty()
                ? await Db.Insertable(entity.Create(setter)).ExecuteCommandIdentityIntoEntityAsync()
                : await Db.Updateable<Customer>().SetColumns(n => entity.Modify(keyValue, setter)).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();
    }
}
