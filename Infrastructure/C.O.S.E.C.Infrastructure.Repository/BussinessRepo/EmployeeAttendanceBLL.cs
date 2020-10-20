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
    sealed class EmployeeAttendanceBLL : DbContext<EmployeeAttendance>, IEmployeeAttendanceBLL
    {
        public EmployeeAttendanceBLL(AllConfigModel _allConfigModel, IEntityBaseAutoSetter _setter) : base(_allConfigModel, _setter)
        {
        }

        public bool Delete(Guid keyValue) => CurrentDb.Update(n => new EmployeeAttendance { IsDelete = true }.Modify(keyValue, setter), n => n.ID == keyValue);

        public async Task<bool> DeleteAsync(Guid keyValue) => await Db.Updateable<EmployeeAttendance>().SetColumns(n => n.IsDelete == true).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();

        public EmployeeAttendance GetEntity(Guid keyValue) => CurrentDb.GetById(keyValue);

        public async Task<EmployeeAttendance> GetEntityAsync(Guid keyValue)
        {
            try
            {
                return await Db.Queryable<EmployeeAttendance>().InSingleAsync(keyValue);
            }
            catch (Exception)
            {
                return await Db.Queryable<EmployeeAttendance>().FirstAsync(n => n.ID == keyValue);
            }
        }

        public List<EmployeeAttendance> GetList(Expression<Func<EmployeeAttendance, bool>> exp) => CurrentDb.GetList(exp);

        public async Task<List<EmployeeAttendance>> GetListAsync(Expression<Func<EmployeeAttendance, bool>> exp) => await Db.Queryable<EmployeeAttendance>().Where(exp).ToListAsync();

        public List<EmployeeAttendance> GetPageList(Pagination pagination, ref int pageCount) => Db.Queryable<EmployeeAttendance>().OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public List<EmployeeAttendance> GetPageList(Expression<Func<EmployeeAttendance, bool>> exp, Pagination pagination, ref int pageCount) => Db.Queryable<EmployeeAttendance>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public async Task<List<EmployeeAttendance>> GetPageListAsync(Expression<Func<EmployeeAttendance, bool>> exp, Pagination pagination, RefAsync<int> pageCount) => await Db.Queryable<EmployeeAttendance>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageListAsync(pagination.Page, pagination.Rows, pageCount);

        public bool SaveForm(Guid keyValue, EmployeeAttendance entity) => keyValue.IsEmpty()
                ? CurrentDb.Insert(entity.Create(setter))
                : CurrentDb.Update(n => entity.Modify(keyValue, setter), n => n.ID == keyValue);

        public async Task<bool> SaveFormAsync(Guid keyValue, EmployeeAttendance entity) => keyValue.IsEmpty()
                ? await Db.Insertable(entity.Create(setter)).ExecuteCommandIdentityIntoEntityAsync()
                : await Db.Updateable<EmployeeAttendance>().SetColumns(n => entity.Modify(keyValue, setter)).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();
    }
}
