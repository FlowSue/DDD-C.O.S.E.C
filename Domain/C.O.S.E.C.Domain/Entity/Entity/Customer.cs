using C.O.S.E.C.Domain.Enums;
using C.O.S.E.C.Domain.InterfaceDrivers;
using C.O.S.E.C.Domain.InterfaceDrivers.Services;
using System;

namespace C.O.S.E.C.Domain.Entity
{
    /// <summary>
    /// 客户
    /// </summary>
    public class Customer : BaseEntityModel,IEntity<Customer>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public override Guid ID { get; set; }

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
        /// 新增调用
        /// </summary>
        /// <param name="setter"></param>
        /// <returns></returns>
        public Customer Create(IEntityBaseAutoSetter setter)
        {
            CreateUserID = UpdateUserID = setter.CreateId;
            CreateUserName = UpdateUserName = setter.CreateName;
            CreateTime = UpdateTime = setter.CreateTime;
            IsPublic = false;
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
        public Customer Modify(Guid keyValue, IEntityBaseAutoSetter setter)
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

        #region 商机转客户
        /// <summary>
        /// 商机转客户
        /// </summary>
        /// <param name="business"></param>
        public static implicit operator Customer(BusinessPool business)
        {
            return new Customer()
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
        #endregion
    }
}
