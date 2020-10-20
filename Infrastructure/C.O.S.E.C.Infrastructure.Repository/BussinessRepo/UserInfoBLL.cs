using C.O.S.E.C.Domain.Entity;
using C.O.S.E.C.Domain.Factory;
using C.O.S.E.C.Domain.InterfaceDrivers.Business;
using C.O.S.E.C.Domain.InterfaceDrivers.Services;
using C.O.S.E.C.Domain.Models;
using C.O.S.E.C.Infrastructure.Config;
using C.O.S.E.C.Infrastructure.Treasury.Helpers;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace C.O.S.E.C.Infrastructure.Repository.BussinessRepo
{

    sealed class UserInfoBLL : DbContext<UserInfo>, IUserInfoBLL
    {
        public UserInfoBLL(AllConfigModel allConfigModel, IEntityBaseAutoSetter _setter) : base(allConfigModel, _setter)
        {
        }

        public bool Delete(Guid KeyValue) => CurrentDb.Update(n => new UserInfo { IsDelete = false }.Modify(KeyValue, setter), n => n.ID == KeyValue);

        public UserInfo GetEntity(Guid KeyValue) => CurrentDb.GetById(KeyValue);

        public async Task<UserInfo> GetEntityAsync(Guid keyvalue)
        {
            try
            {
                return await Db.Queryable<UserInfo>().InSingleAsync(keyvalue);
            }
            catch (Exception)
            {
                return await Db.Queryable<UserInfo>().FirstAsync(n => n.ID == keyvalue);
            }

        }

        public List<UserInfo> GetList(Expression<Func<UserInfo, bool>> exp) => CurrentDb.GetList(exp);

        public async Task<List<UserInfo>> GetListAsync(Expression<Func<UserInfo, bool>> exp) => await Db.Queryable<UserInfo>().Where(exp).ToListAsync();

        public List<UserInfo> GetPageList(Pagination pagination, ref int pageCount) => Db.Queryable<UserInfo>().ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public List<UserInfo> GetPageList(Expression<Func<UserInfo, bool>> exp, Pagination pagination, ref int pageCount) => Db.Queryable<UserInfo>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageList(pagination.Page, pagination.Rows, ref pageCount);

        public async Task<List<UserInfo>> GetPageListAsync(Expression<Func<UserInfo, bool>> exp, Pagination pagination, RefAsync<int> pageCount) => await Db.Queryable<UserInfo>().Where(exp).OrderByIF(pagination.Sidx.IsNullOrEmpty(), n => n.CreateTime, OrderByType.Desc).ToPageListAsync(pagination.Page, pagination.Rows, pageCount);

        public bool SaveForm(Guid KeyValue, UserInfo entity)
        {
            if (KeyValue.IsEmpty())
            {
                entity.Password = EncryptionHelper.MD5ToBase64String(entity.Password);
                return CurrentDb.Insert(entity.Create(setter));
            }
            else
            {
                if (!entity.Password.IsEmpty())
                {
                    entity.Password = EncryptionHelper.MD5ToBase64String(entity.Password);
                }
                return CurrentDb.Update(n => entity.Modify(KeyValue, setter), n => n.ID == KeyValue);
            }
        }

        public UserInfo GetEntityForAccount(string account) => Db.Queryable<UserInfo>().Filter(null, true).Single(n => n.Account == account || n.Mobile == account || n.Email == account);

        public UserInfo CheckLogin(string username, string password)
        {
            UserInfo userEntity = GetEntityForAccount(username);
            if (userEntity == default)
            {
                userEntity = new UserInfo()
                {
                    LoginMsg = "账户不存在!",
                    LoginOk = false
                };
                return userEntity;
            }
            userEntity.LoginOk = false;
            if (userEntity.IsEnable)
            {
                //Convert.ToBase64String(md5.ComputeHash(Encoding.Unicode.GetBytes(password))).ToLower();
                if (userEntity.Password == password) userEntity.LoginOk = true;
                else if (userEntity.Password == EncryptionHelper.MD5ToBase64String(password)) userEntity.LoginOk = true;
                else userEntity.LoginMsg = "密码和账户名不匹配!";
            }
            else
            {
                userEntity.LoginMsg = "账户被系统锁定,请联系管理员!";
            }
            return userEntity;
        }

        /// <summary>
        /// 修改用户登录密码
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="Password">新密码（MD5 小写）</param>
        public async Task<bool> RevisePasswordAsync(Guid keyValue, string Password) => await Db.Updateable<UserInfo>().SetColumns(n => new UserInfo { Password = Password }.Modify(keyValue, setter)).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();
        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="State">状态：1-启动；0-禁用</param>
        public bool UpdateState(Guid keyValue, int State) => CurrentDb.Update(n => new UserInfo { IsEnable = State.ToBool() }.Modify(keyValue, setter), n => n.ID == keyValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> SaveFormAsync(Guid keyValue, UserInfo entity) => keyValue.IsEmpty()
                ? await Db.Insertable(entity.Create(setter)).ExecuteCommandIdentityIntoEntityAsync()
                : await Db.Updateable<UserInfo>().SetColumns(n => entity.Modify(keyValue, setter)).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid keyValue) => await Db.Updateable<UserInfo>().SetColumns(n => n.IsDelete == true).Where(n => n.ID == keyValue).ExecuteCommandHasChangeAsync();
    }
}
