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
    class CustomerTrailRecordBLL : DbContext<CustomerTrailRecord>, ICustomerTrailRecordBLL
    {
        public CustomerTrailRecordBLL(AllConfigModel _allConfigModel, IEntityBaseAutoSetter _setter) : base(_allConfigModel, _setter)
        {
        }

        public CustomerTrailRecord GetEntity(Guid KeyValue) => CurrentDb.GetById(KeyValue);

        //public List<UserTrailRecord> GetList()
        //{
        //    var db = DbConfig.GetDbInstance();
        //    //var list = db.Queryable<UserTrailRecord>().ToList();
        //    var list = db.Queryable<UserTrailRecord, Entity.Business>((n, m) => new object[] { JoinType.Left, n.CompanyId == m.CompanyId })
        //        .Select((n, m) => new UserTrailRecord()
        //        {
        //            CompanyId = n.CompanyId,
        //            CompanyName = m.CompanyName,
        //            Contacts = n.Contacts,
        //            CreateTime = n.CreateTime,
        //            CreateUserID = n.CreateUserID,
        //            CreateUserName = n.CreateUserName,
        //            DayDate = n.DayDate,
        //            Description = n.Description,
        //            RecordId = n.RecordId,
        //            TrackContent = n.TrackContent,
        //            TrackTypeId = n.TrackTypeId
        //        }).ToList();
        //    return list;
        //}

        public List<CustomerTrailRecord> GetList(Expression<Func<CustomerTrailRecord, bool>> exp) => CurrentDb.GetList(exp);

        public List<CustomerTrailRecord> GetPageList(Pagination pagination, ref int pageCount) => Db.Queryable<CustomerTrailRecord>().OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public List<CustomerTrailRecord> GetPageList(Expression<Func<CustomerTrailRecord, bool>> exp, Pagination pagination, ref int pageCount) => Db.Queryable<CustomerTrailRecord>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public bool SaveForm(Guid KeyValue, CustomerTrailRecord entity) => KeyValue.IsEmpty()
                ? CurrentDb.Insert(entity.Create(setter))
                : CurrentDb.Update(n => entity.Modify(KeyValue, setter), n => n.ID == KeyValue);

        public bool Delete(Guid KeyValue) => CurrentDb.DeleteById(KeyValue);

        public async Task<CustomerTrailRecord> GetEntityAsync(Guid keyValue)
        {
            try
            {
                return await Db.Queryable<CustomerTrailRecord>().InSingleAsync(keyValue);
            }
            catch (Exception)
            {
                return await Db.Queryable<CustomerTrailRecord>().FirstAsync(n => n.ID == keyValue);
            }
        }

        public async Task<List<CustomerTrailRecord>> GetListAsync(Expression<Func<CustomerTrailRecord, bool>> exp) => await Db.Queryable<CustomerTrailRecord>().Where(exp).ToListAsync();

        public async Task<List<CustomerTrailRecord>> GetPageListAsync(Expression<Func<CustomerTrailRecord, bool>> exp, Pagination pagination, RefAsync<int> pageCount) => await Db.Queryable<CustomerTrailRecord>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageListAsync(pagination.Page, pagination.Rows, pageCount);

        public async Task<bool> SaveFormAsync(Guid keyValue, CustomerTrailRecord entity) => keyValue.IsEmpty()
                ? await Db.Insertable(entity.Create(setter)).ExecuteCommandIdentityIntoEntityAsync()
                : await Db.Updateable<CustomerTrailRecord>().SetColumns(n => entity.Modify(keyValue, setter)).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();

        public async Task<bool> DeleteAsync(Guid keyValue) => await Db.Updateable<CustomerTrailRecord>().SetColumns(n => n.IsDelete == true).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();
    }
}