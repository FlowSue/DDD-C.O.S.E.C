using C.O.S.E.C.Domain.InterfaceDrivers.Services;
using SqlSugar;
using System;
using System.ComponentModel;

namespace C.O.S.E.C.Domain.Entity.Internal
{
    /// <summary>
    /// 目标客户（内部）
    /// </summary>
    public class TargetCustomers
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
        /// <summary>
        /// 状态类型 0：目标客户，1：商机客户，-1：成交客户
        /// </summary>
        [DisplayName("状态类型")]
        public int ClientType { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        [DisplayName("行业")]
        public string Industry { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Description { get; set; }
        /// <summary>
        /// 推荐产品
        /// </summary>
        [DisplayName("推荐产品")]
        public string Recommended { get; set; }
        /// <summary>
        /// 拜访时间
        /// </summary>
        [DisplayName("拜访时间")]
        public DateTime? VisitTime { get; set; }
        /// <summary>
        /// 拜访人
        /// </summary>
        [DisplayName("拜访人")]
        public string VisitUser { get; set; }
        /// <summary>
        /// 产品金额
        /// </summary>
        [DisplayName("产品金额")]
        public decimal Money { get; set; }
        /// <summary>
        /// 预计成交时间
        /// </summary>
        [DisplayName("预计成交时间")]
        public DateTime? ExpectedTime { get; set; }
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
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDelete { get; set; }

        /// <summary>
        /// 新增调用
        /// </summary>
        /// <param name="setter"></param>
        /// <returns></returns>
        public TargetCustomers Create(IEntityBaseAutoSetter setter)
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
        public TargetCustomers Modify(int keyValue, IEntityBaseAutoSetter setter)
        {
            ID = keyValue;
            UpdateUserID = setter.UpdateId;
            UpdateUserName = setter.UpdateName;
            UpdateTime = setter.UpdateTime;
            return this;
        }

    }
}
