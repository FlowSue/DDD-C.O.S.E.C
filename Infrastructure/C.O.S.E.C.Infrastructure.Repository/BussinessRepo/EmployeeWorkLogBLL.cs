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

    class EmployeeWorkLogBLL : DbContext<EmployeeWorkLog>, IEmployeeWorkLogBLL
    {
        public EmployeeWorkLogBLL(AllConfigModel _allConfigModel, IEntityBaseAutoSetter _setter) : base(_allConfigModel, _setter)
        {
        }

        public bool Delete(Guid keyValue) => CurrentDb.Update(n => new EmployeeWorkLog { IsDelete = true }.Modify(keyValue, setter), n => n.ID == keyValue);

        public async Task<bool> DeleteAsync(Guid keyValue) => await Db.Updateable<EmployeeWorkLog>().SetColumns(n => n.IsDelete == true).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();

        public EmployeeWorkLog GetEntity(Guid keyValue) => CurrentDb.GetById(keyValue);

        public async Task<EmployeeWorkLog> GetEntityAsync(Guid keyValue)
        {
            try
            {
                return await Db.Queryable<EmployeeWorkLog>().InSingleAsync(keyValue);
            }
            catch (Exception)
            {
                return await Db.Queryable<EmployeeWorkLog>().FirstAsync(n => n.ID == keyValue);
            }
        }

        public List<EmployeeWorkLog> GetList(Expression<Func<EmployeeWorkLog, bool>> exp) => CurrentDb.GetList(exp);

        public async Task<List<EmployeeWorkLog>> GetListAsync(Expression<Func<EmployeeWorkLog, bool>> exp) => await Db.Queryable<EmployeeWorkLog>().Where(exp).ToListAsync();

        public List<EmployeeWorkLog> GetPageList(Pagination pagination, ref int pageCount) => Db.Queryable<EmployeeWorkLog>().OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public List<EmployeeWorkLog> GetPageList(Expression<Func<EmployeeWorkLog, bool>> exp, Pagination pagination, ref int pageCount) => Db.Queryable<EmployeeWorkLog>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public async Task<List<EmployeeWorkLog>> GetPageListAsync(Expression<Func<EmployeeWorkLog, bool>> exp, Pagination pagination, RefAsync<int> pageCount) => await Db.Queryable<EmployeeWorkLog>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageListAsync(pagination.Page, pagination.Rows, pageCount);

        public bool SaveForm(Guid keyValue, EmployeeWorkLog entity) => keyValue.IsEmpty()
                ? CurrentDb.Insert(entity.Create(setter))
                : CurrentDb.Update(n => entity.Modify(keyValue, setter), n => n.ID == keyValue);

        public async Task<bool> SaveFormAsync(Guid keyValue, EmployeeWorkLog entity) => keyValue.IsEmpty()
                ? await Db.Insertable(entity.Create(setter)).ExecuteCommandIdentityIntoEntityAsync()
                : await Db.Updateable<EmployeeWorkLog>().SetColumns(n => entity.Modify(keyValue, setter)).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();
    }
}
