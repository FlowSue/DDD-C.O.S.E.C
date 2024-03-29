﻿using C.O.S.E.C.Domain.Enums;
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
    public class CluePool : IEntity<CluePool>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [DisplayName("主键")]
        public Guid ID { get; set; }
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
        public bool IsEnable { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建用户ID
        /// </summary>
        [DisplayName("创建用户ID")]
        public string CreateUserID { get; set; }
        /// <summary>
        /// 创建用户名称
        /// </summary>
        [DisplayName("创建用户名称")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [DisplayName("更新时间")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 更新用户ID
        /// </summary>
        [DisplayName("更新用户ID")]
        public string UpdateUserID { get; set; }
        /// <summary>
        /// 更新用户名称
        /// </summary>
        [DisplayName("更新用户名称")]
        public string UpdateUserName { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDelete { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("状态")]
        public StatusState Status { get; set; }
        /// <summary>
        /// 系统标识
        /// </summary>
        [DisplayName("系统标识")]
        public string SystemID { get; set; }
        /// <summary>
        /// 新增调用
        /// </summary>
        /// <param name="setter"></param>
        /// <returns></returns>
        public CluePool Create(IEntityBaseAutoSetter setter)
        {
            SnowflakeID = IdGenerateHelper.NewId;
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
