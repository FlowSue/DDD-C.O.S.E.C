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
    class EmployeeWorkPlanBLL : DbContext<EmployeeWorkPlan>, IEmployeeWorkPlanBLL
    {
        public EmployeeWorkPlanBLL(AllConfigModel _allConfigModel, IEntityBaseAutoSetter _setter) : base(_allConfigModel, _setter)
        {
        }

        public bool Delete(Guid keyValue) => CurrentDb.Update(n => new EmployeeWorkPlan { IsDelete = false }.Modify(keyValue, setter), n => n.ID == keyValue);

        public async Task<bool> DeleteAsync(Guid keyValue) => await Db.Updateable<EmployeeWorkPlan>().SetColumns(n => n.IsDelete == true).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();

        public EmployeeWorkPlan GetEntity(Guid keyValue) => CurrentDb.GetById(keyValue);

        public async Task<EmployeeWorkPlan> GetEntityAsync(Guid keyValue)
        {
            try
            {
                return await Db.Queryable<EmployeeWorkPlan>().InSingleAsync(keyValue);
            }
            catch (Exception)
            {
                return await Db.Queryable<EmployeeWorkPlan>().FirstAsync(n => n.ID == keyValue);
            }
        }

        public List<EmployeeWorkPlan> GetList(Expression<Func<EmployeeWorkPlan, bool>> exp) => CurrentDb.GetList(exp);

        public async Task<List<EmployeeWorkPlan>> GetListAsync(Expression<Func<EmployeeWorkPlan, bool>> exp) => await Db.Queryable<EmployeeWorkPlan>().Where(exp).ToListAsync();

        public List<EmployeeWorkPlan> GetPageList(Pagination pagination, ref int pageCount) => Db.Queryable<EmployeeWorkPlan>().OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public List<EmployeeWorkPlan> GetPageList(Expression<Func<EmployeeWorkPlan, bool>> exp, Pagination pagination, ref int pageCount) => Db.Queryable<EmployeeWorkPlan>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public async Task<List<EmployeeWorkPlan>> GetPageListAsync(Expression<Func<EmployeeWorkPlan, bool>> exp, Pagination pagination, RefAsync<int> pageCount) => await Db.Queryable<EmployeeWorkPlan>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageListAsync(pagination.Page, pagination.Rows, pageCount);

        public bool SaveForm(Guid keyValue, EmployeeWorkPlan entity) => keyValue.IsEmpty() && entity.ID.IsEmpty()
                ? CurrentDb.Insert(entity.Create(setter))
                : CurrentDb.Update(n => entity.Modify(keyValue, setter), n => n.ID == keyValue);

        public async Task<bool> SaveFormAsync(Guid keyValue, EmployeeWorkPlan entity) => keyValue.IsEmpty()
                ? await Db.Insertable(entity.Create(setter)).ExecuteCommandIdentityIntoEntityAsync()
                : await Db.Updateable<EmployeeWorkPlan>().SetColumns(n => entity.Modify(keyValue, setter)).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();
    }
}
