using C.O.S.E.C.Domain.InterfaceDrivers.Services;
using SqlSugar;
using System;
using System.ComponentModel;

namespace C.O.S.E.C.Domain.Entity.Internal
{
    /// <summary>
    /// 成交客户（内部）
    /// </summary>
    public class InternalCustomers
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true), DisplayName("序号")]
        public int ID { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [DisplayName("编码")]
        public string EnCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        public string FullName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [DisplayName("联系人")]
        public string Contact { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [DisplayName("联系电话")]
        public string Mobile { get; set; }
        //public string ClientType { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        [DisplayName("行业")]
        public string Industry { get; set; }
        /// <summary>
        /// 状态类型 0：成交客户，1：售后
        /// </summary>
        [DisplayName("状态类型")]
        public int ClientType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Description { get; set; }
        /// <summary>
        /// 购买产品
        /// </summary>
        [DisplayName("购买产品")]
        public string Buyproduct { get; set; }
        /// <summary>
        /// 签单时间
        /// </summary>
        [DisplayName("签单时间")]
        public DateTime? SigningTime { get; set; }
        /// <summary>
        /// 局域站点
        /// </summary>
        [DisplayName("局域站点")]
        public string Localsite { get; set; }
        /// <summary>
        /// 全局站点
        /// </summary>
        [DisplayName("全局站点")]
        public string Globalsite { get; set; }
        /// <summary>
        /// 续费到期
        /// </summary>
        [DisplayName("续费到期")]
        public DateTime? Renewaldue { get; set; }
        /// <summary>
        /// 产品实施
        /// </summary>
        [DisplayName("产品实施")]
        public DateTime? ImplementationTime { get; set; }
        /// <summary>
        /// 售后技术
        /// </summary>
        [DisplayName("售后技术")]
        public DateTime? TechnologyTime { get; set; }
        /// <summary>
        /// 收费方式
        /// </summary>
        [DisplayName("收费方式")]
        public string ChargeMethod { get; set; }
        /// <summary>
        /// 服务到期
        /// </summary>
        [DisplayName("服务到期")]
        public DateTime? ServiceExpired { get; set; }
        /// <summary>
        /// 产品交付
        /// </summary>
        [DisplayName("产品交付")]
        public DateTime? ProductDelivery { get; set; }
        /// <summary>
        /// 工程师
        /// </summary>
        [DisplayName("工程师")]
        public string Engineer { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        [DisplayName("合同金额")]
        public string ContractAmount { get; set; }
        /// <summary>
        /// 续年服务包
        /// </summary>
        [DisplayName("续年服务包")]
        public string ServicePack { get; set; }
        /// <summary>
        /// 操作员ID
        /// </summary>
        [DisplayName("操作员ID")]
        public string CreateUserID { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        [DisplayName("操作员")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        [DisplayName("录入时间")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDelete { get; set; }
        /// <summary>
        /// 操作员ID
        /// </summary>
        [DisplayName("操作员ID")]
        public string UpdateUserID { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        [DisplayName("操作员")]
        public string UpdateUserName { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        [DisplayName("录入时间")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 新增调用
        /// </summary>
        /// <param name="setter"></param>
        /// <returns></returns>
        public InternalCustomers Create(IEntityBaseAutoSetter setter)
        {
            CreateUserID = UpdateUserID = setter.CreateId;
            CreateUserName = UpdateUserName = setter.CreateName;
            CreateTime = UpdateTime = setter.CreateTime;
            IsDelete = false;
            return this;
        }
        /// <summary>
        /// 更新调用
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        public InternalCustomers Modify(int keyValue, IEntityBaseAutoSetter setter)
        {
            ID = keyValue;
            UpdateUserID = setter.UpdateId;
            UpdateUserName = setter.UpdateName;
            UpdateTime = setter.UpdateTime;
            return this;
        }

    }
}
