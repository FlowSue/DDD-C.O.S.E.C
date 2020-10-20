using C.O.S.E.C.Domain.Enums;
using C.O.S.E.C.Domain.InterfaceDrivers;
using C.O.S.E.C.Domain.InterfaceDrivers.Services;
using C.O.S.E.C.Infrastructure.Treasury.Helpers;
using System;
using System.ComponentModel;

namespace C.O.S.E.C.Domain.Entity
{
    /// <summary>
    /// 线索池
    /// </summary>
    public class CluePool : BaseEntityModel, IEntity<CluePool>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [DisplayName("主键")]
        public override Guid ID { get; set; }

        /// <summary>
        /// 自增ID
        /// </summary>
        [DisplayName("雪花")]
        public long SnowflakeID { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [DisplayName("编码")]
        public string EnCode { get; set; }

        /// <summary>
        /// 全称
        /// </summary>
        [DisplayName("公司名称")]
        public string FullName { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        [DisplayName("公司地址")]
        public string CompanyAddress { get; set; }

        /// <summary>
        /// 公司站点
        /// </summary>
        [DisplayName("公司站点")]
        public string CompanyNetSite { get; set; }

        /// <summary>
        /// 公司性质
        /// </summary>
        [DisplayName("公司性质")]
        public string CompanyNatureId { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [DisplayName("编码")]
        public string Contact { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        [DisplayName("移动电话")]
        public string Mobile { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        [DisplayName("省")]
        public string Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        [DisplayName("市")]
        public string City { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        [DisplayName("区")]
        public string District { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Description { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public override bool IsEnable { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public override DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建用户ID
        /// </summary>
        [DisplayName("创建用户ID")]
        public override string CreateUserID { get; set; }

        /// <summary>
        /// 创建用户名称
        /// </summary>
        [DisplayName("创建用户名称")]
        public override string CreateUserName { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [DisplayName("更新时间")]
        public override DateTime UpdateTime { get; set; }

        /// <summary>
        /// 更新用户ID
        /// </summary>
        [DisplayName("更新用户ID")]
        public override string UpdateUserID { get; set; }

        /// <summary>
        /// 更新用户名称
        /// </summary>
        [DisplayName("更新用户名称")]
        public override string UpdateUserName { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public override bool IsDelete { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("状态")]
        public override StatusState Status { get; set; }

        /// <summary>
        /// 系统标识
        /// </summary>
        [DisplayName("系统标识")]
        public override string SystemID { get; set; }

        /// <summary>
        /// 统一社会信用代码
        /// </summary>
        [DisplayName("统一社会信用代码")]
        public string Cods { get; set; }

        /// <summary>
        /// 法人
        /// </summary>
        [DisplayName("法人")]
        public string Legal { get; set; }

        /// <summary>
        /// 营业执照
        /// </summary>
        [DisplayName("营业执照")]
        public string License { get; set; }

        /// <summary>
        /// 客户等级
        /// </summary>
        [DisplayName("客户等级")]
        public string Levels { get; set; }

        /// <summary>
        /// 客户来源
        /// </summary>
        [DisplayName("客户来源")]
        public string Source { get; set; }

        /// <summary>
        /// 注册资本
        /// </summary>
        [DisplayName("注册资本")]
        public string Capital { get; set; }

        /// <summary>
        /// 新增调用
        /// </summary>
        /// <param name="setter"></param>
        /// <returns></returns>
        public CluePool Create(IEntityBaseAutoSetter setter)
        {
            SnowflakeID = IdGenerateHelper.NewId;
            CreateUserID = UpdateUserID = setter.CreateId;
            CreateUserName = UpdateUserName = setter.CreateName;
            CreateTime = UpdateTime = setter.CreateTime;
            IsEnable = true;
            IsDelete = false;
            Status = StatusState.Normal;
            if (!setter.SystemId.IsEmpty())
            {
                SystemID = setter.SystemId.ToString();
            }
            return this;
        }

        /// <summary>
        /// 更新调用
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        public CluePool Modify(Guid keyValue, IEntityBaseAutoSetter setter)
        {
            ID = keyValue;
            UpdateUserID = setter.UpdateId;
            UpdateUserName = setter.UpdateName;
            UpdateTime = setter.UpdateTime;
            if (!setter.SystemId.IsEmpty())
            {
                SystemID = setter.SystemId.ToString();
            }
            return this;
        }

    }
}
