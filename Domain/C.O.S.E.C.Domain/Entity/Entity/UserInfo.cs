using C.O.S.E.C.Domain.Enums;
using C.O.S.E.C.Domain.InterfaceDrivers;
using C.O.S.E.C.Domain.InterfaceDrivers.Services;
using SqlSugar;
using System;

namespace C.O.S.E.C.Domain.Entity
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo : BaseEntityModel, IEntity<UserInfo>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public override Guid ID { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string EnCode { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 微信
        /// </summary>
        public string WeChat { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 微信OpenID
        /// </summary>
        public string WeiXinFromOpenID { get; set; }

        /// <summary>
        /// 所属组织
        /// </summary>
        /// <returns></returns>
        public string OrganizeID { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        /// <returns></returns>
        public string DepartmentID { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public bool Gender { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 角色身份
        /// </summary>
        public UserRolesEnum Role { get; set; }

        /// <summary>
        /// 是否是超管
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public override bool IsEnable { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public override DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建用户ID
        /// </summary>
        public override string CreateUserID { get; set; }

        /// <summary>
        /// 创建用户名称
        /// </summary>
        public override string CreateUserName { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public override DateTime UpdateTime { get; set; }

        /// <summary>
        /// 更新用户ID
        /// </summary>
        public override string UpdateUserID { get; set; }

        /// <summary>
        /// 更新用户名称
        /// </summary>
        public override string UpdateUserName { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public override bool IsDelete { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public override StatusState Status { get; set; }

        /// <summary>
        /// 系统标识
        /// </summary>
        public override string SystemID { get; set; }

        /// <summary>
        /// 新增调用
        /// </summary>
        public UserInfo Create(IEntityBaseAutoSetter setter)
        {
            CreateTime = UpdateTime = setter.CreateTime;
            CreateUserID = UpdateUserID = setter.CreateId;
            CreateUserName = UpdateUserName = setter.CreateName;
            this.IsEnable = true;
            this.IsDelete = false;
            this.Status = StatusState.Normal;
            if (!setter.SystemId.IsEmpty())
            {
                SystemID = setter.SystemId.ToString();
            }
            return this;
        }

        /// <summary>
        /// 更新调用
        /// </summary>
        public UserInfo Modify(Guid keyValue, IEntityBaseAutoSetter setter)
        {
            this.ID = keyValue;
            this.UpdateUserID = setter.UpdateId;
            this.UpdateUserName = setter.UpdateName;
            this.UpdateTime = setter.UpdateTime;
            if (!setter.SystemId.IsEmpty())
            {
                SystemID = setter.SystemId.ToString();
            }
            return this;
        }

        #region 扩展属性
        /// <summary>
        /// 登录信息
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string LoginMsg { get; set; }

        /// <summary>
        /// 登录状态
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public bool LoginOk { get; set; }

        /// <summary>
        /// 令牌
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string Token { get; set; }
        #endregion

    }
}
