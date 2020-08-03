using C.O.S.E.C.Domain.Enums;
using C.O.S.E.C.Domain.InterfaceDrivers;
using C.O.S.E.C.Domain.InterfaceDrivers.Services;
using SqlSugar;
using System;

namespace C.O.S.E.C.Domain.Entity
{
    /// <summary>
    /// 系统订单
    /// </summary>
    public class SystemOrder : IEntity<SystemOrder>
    {
        /// <summary>
        /// 系统ID（SystemID）
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime ExpiryTime { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public string CustomerId { get; set; }
        /// <summary>
        /// 是否定制
        /// </summary>
        public bool IsCustomize { get; set; }
        /// <summary>
        /// 定制控制器
        /// </summary>
        public string CustomizeController { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 前台样式
        /// </summary>
        public string StyleTemplate { get; set; }
        /// <summary>
        /// 基础功能
        /// </summary>
        public string ModuleCategoryId { get; set; }
        /// <summary>
        /// 对应的订单编号
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// 定制工程师
        /// </summary>
        public string CustomizeUserId { get; set; }
        /// <summary>
        /// 定制价格
        /// </summary>
        public double CustomizeTotalMoney { get; set; }
        /// <summary>
        /// 定制描述
        /// </summary>
        public string CustomizeDescription { get; set; }
        /// <summary>
        /// 基础价格
        /// </summary>
        public double BaseTotalMoney { get; set; }
        /// <summary>
        /// 次年续费价格
        /// </summary>
        public double RenewTotalMoney { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public double TotalMoney { get; set; }
        /// <summary>
        /// 合同ID
        /// </summary>
        public string ContractId { get; set; }
        /// <summary>
        /// 初始账号
        /// </summary>
        public string InitAccount { get; set; }
        /// <summary>
        /// 初始密码
        /// </summary>
        public string InitPassWord { get; set; }
        /// <summary>
        /// 初始密码
        /// </summary>
        public string InitEncryptPassWord { get; set; }
        /// <summary>
        /// Api服务密码
        /// </summary>
        public string SystemPassWord { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建用户ID
        /// </summary>
        public string CreateUserID { get; set; }
        /// <summary>
        /// 创建用户名称
        /// </summary>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 更新用户ID
        /// </summary>
        public string UpdateUserID { get; set; }
        /// <summary>
        /// 更新用户名称
        /// </summary>
        public string UpdateUserName { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public StatusState Status { get; set; }
        /// <summary>
        /// 系统标识
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string SystemID { get => ID.ToString(); set => ID.ToString(); }
        /// <summary>
        /// 新增调用
        /// </summary>
        public SystemOrder Create(IEntityBaseAutoSetter setter)
        {
            CreateUserID = setter.CreateId;
            CreateUserName = setter.CreateName;
            CreateTime = setter.CreateTime;
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
        public SystemOrder Modify(Guid keyValue, IEntityBaseAutoSetter setter)
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
