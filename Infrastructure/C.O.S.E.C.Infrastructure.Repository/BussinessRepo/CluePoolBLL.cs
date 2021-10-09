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
    sealed class CluePoolBLL : DbContext<CluePool>, ICluePoolBLL
    {
        public CluePoolBLL(AllConfigModel _allConfigModel, IEntityBaseAutoSetter _setter) : base(_allConfigModel, _setter)
        {
        }

        public bool ConversionBusiness(Guid keyValue)
        {
            var result = Db.Ado.UseTran(() =>
            {
                var clue = CurrentDb.GetById(keyValue);
                Db.Insertable<BusinessPool>(((BusinessPool)clue).Create(setter)).ExecuteCommand();
                Db.Updateable<CluePool>(clue.Modify(keyValue, setter)).SetColumns(n => n.IsDelete == true && n.Status == default).ExecuteCommand();
            });
            if (result.IsSuccess)
            {
                return result.Data;
            }
            else
            {
                throw ExceptionEx.ThrowDataAccessException(result.ErrorException, result.ErrorMessage);
            }
        }

        public bool Delete(Guid keyValue) => CurrentDb.Update(n => new CluePool { IsDelete = true }.Modify(keyValue, setter), n => n.ID == keyValue);

        public async Task<bool> DeleteAsync(Guid keyValue) => await Db.Updateable<CluePool>().SetColumns(n => n.IsDelete == true).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();

        public CluePool GetEntity(Guid keyValue) => CurrentDb.GetById(keyValue);

        public async Task<CluePool> GetEntityAsync(Guid keyValue)
        {
            try
            {
                return await Db.Queryable<CluePool>().InSingleAsync(keyValue);
            }
            catch (Exception)
            {
                return await Db.Queryable<CluePool>().FirstAsync(n => n.ID == keyValue);
            }
        }

        public List<CluePool> GetList(Expression<Func<CluePool, bool>> exp) => CurrentDb.GetList(exp);

        public async Task<List<CluePool>> GetListAsync(Expression<Func<CluePool, bool>> exp) => await Db.Queryable<CluePool>().Where(exp).ToListAsync();

        public List<CluePool> GetPageList(Pagination pagination, ref int pageCount) => Db.Queryable<CluePool>().OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public List<CluePool> GetPageList(Expression<Func<CluePool, bool>> exp, Pagination pagination, ref int pageCount) => Db.Queryable<CluePool>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public async Task<List<CluePool>> GetPageListAsync(Expression<Func<CluePool, bool>> exp, Pagination pagination, RefAsync<int> pageCount) => await Db.Queryable<CluePool>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageListAsync(pagination.Page, pagination.Rows, pageCount);

        public bool Range(List<CluePool> list)
        {
            var result = Db.Ado.UseTran(() => CurrentDb.InsertRange(list));
            if (result.IsSuccess)
            {
                return result.Data;
            }
            else
                throw ExceptionEx.ThrowDataAccessException(result.ErrorException, result.ErrorMessage);
        }

        public async Task<bool> RangeAsync(List<CluePool> list)
        {
            var result = await Db.Ado.UseTranAsync(async () => await this.Db.Insertable(list).ExecuteCommandAsync() > 0);
            if (result.IsSuccess)
            {
                return result.Data;
            }
            else
                throw ExceptionEx.ThrowDataAccessException(result.ErrorException, result.ErrorMessage);
        }

        public bool SaveForm(Guid KeyValue, CluePool entity) => KeyValue.IsEmpty()
                ? CurrentDb.Insert(entity.Create(setter))
                : CurrentDb.Update(n => entity.Modify(KeyValue, setter), n => n.ID == KeyValue);

        public async Task<bool> SaveFormAsync(Guid keyValue, CluePool entity) => keyValue.IsEmpty()
                ? await Db.Insertable(entity.Create(setter)).ExecuteCommandIdentityIntoEntityAsync()
                : await Db.Updateable<CluePool>().SetColumns(n => entity.Modify(keyValue, setter)).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();
    }
}