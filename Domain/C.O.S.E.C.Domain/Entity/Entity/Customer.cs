using C.O.S.E.C.Domain.Enums;
using C.O.S.E.C.Domain.InterfaceDrivers;
using C.O.S.E.C.Domain.InterfaceDrivers.Services;
using C.O.S.E.C.Infrastructure.Treasury.Helpers;
using System;
using System.ComponentModel;

namespace C.O.S.E.C.Domain.Entity
{
    /// <summary>
    /// 客户
    /// </summary>
    public class Customer : BaseEntityModel, IEntity<Customer>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public override Guid ID { get; set; }

        /// <summary>
        /// 自增ID
        /// </summary>
        [DisplayName("雪花")]
        public long SnowflakeID { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string EnCode { get; set; }

        /// <summary>
        /// 全称
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string CompanyAddress { get; set; }

        /// <summary>
        /// 公司站点
        /// </summary>
        public string CompanyNetSite { get; set; }

        /// <summary>
        /// 公司性质
        /// </summary>
        public string CompanyNatureId { get; set; }

        /// <summary>
        /// 客户行业
        /// </summary>
        public string CustIndustryId { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        public string Mobile { get; set; }

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
        /// 备注
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 跟进人员ID
        /// </summary>
        public string TraceUserID { get; set; }

        /// <summary>
        /// 跟进人员名字
        /// </summary>
        public string TraceUserName { get; set; }

        /// <summary>
        /// 是否公共
        /// </summary>
        public bool IsPublic { get; set; }

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
        public Customer Create(IEntityBaseAutoSetter setter)
        {
            if (this.ID.IsNullOrEmpty())
            {
                ID = Guid.NewGuid();
            }
            if (SnowflakeID.IsNullOrEmpty())
            {
                SnowflakeID = IdGenerateHelper.NewId;
            }
            CreateUserID = UpdateUserID = setter.CreateId;
            CreateUserName = UpdateUserName = setter.CreateName;
            CreateTime = UpdateTime = setter.CreateTime;
            IsPublic = false;
            IsEnable = true;
            IsDelete = false;
            Status = StatusState.Normal;
            SystemID = setter.SystemId.ToString();
            return this;
        }

        /// <summary>
        /// 更新调用
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        public Customer Modify(Guid keyValue, IEntityBaseAutoSetter setter)
        {
            ID = keyValue;
            UpdateUserID = setter.UpdateId;
            UpdateUserName = setter.UpdateName;
            UpdateTime = setter.UpdateTime;
            SystemID = setter.SystemId.ToString();
            return this;
        }

        #region 商机转客户
        /// <summary>
        /// 商机转客户
        /// </summary>
        /// <param name="business"></param>
        public static implicit operator Customer(BusinessPool business)
        {
            return new Customer
            {
                EnCode = business.EnCode,
                FullName = business.FullName,
                CompanyAddress = business.CompanyAddress,
                CompanyNetSite = business.CompanyNetSite,
                CustIndustryId = business.CompanyNatureId,
                Contact = business.Contact,
                Mobile = business.Mobile,
                Province = business.Province,
                City = business.City,
                Description = business.Description,
                TraceUserID = business.TraceUserID,
                TraceUserName = business.TraceUserName,
                Status = business.Status,
                SystemID = business.SystemID,
            };
        }
        /// <summary>
        /// 线索转客户
        /// </summary>
        /// <param name="clue"></param>
        public static implicit operator Customer(CluePool clue)
        {
            return new Customer
            {
                EnCode = clue.EnCode,
                FullName = clue.FullName,
                CompanyAddress = clue.CompanyAddress,
                CompanyNetSite = clue.CompanyNetSite,
                CustIndustryId = clue.CompanyNatureId,
                Contact = clue.Contact,
                Mobile = clue.Mobile,
                Province = clue.Province,
                City = clue.City,
                Description = clue.Description,
                Status = clue.Status,
                SystemID = clue.SystemID,
                Capital = clue.Capital,
                Cods = clue.Cods,
                CompanyNatureId = clue.CompanyNatureId,
                District = clue.District,
                ID = clue.ID,
                CreateUserID = clue.CreateUserID,
                CreateUserName = clue.CreateUserName,
                IsDelete = clue.IsDelete,
                IsEnable = clue.IsEnable,
                IsPublic = clue.IsEnable,
                Legal = clue.Legal,
                Levels = clue.Levels,
                License = clue.License,
                Source = clue.Source
            };
        }
        #endregion
    }
}
