using C.O.S.E.C.Domain.Enums;
using C.O.S.E.C.Domain.InterfaceDrivers;
using C.O.S.E.C.Domain.InterfaceDrivers.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace C.O.S.E.C.Domain.Entity
{
    /// <summary>
    /// 商机池
    /// </summary>
    public class BusinessPool : BaseEntityModel, IEntity<BusinessPool>
    {
        /// <summary>
        /// 主键
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
        /// 跟进用户ID
        /// </summary>
        public string TraceUserID { get; set; }
        /// <summary>
        /// 跟进用户名称
        /// </summary>
        public string TraceUserName { get; set; }
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
        public BusinessPool Create(IEntityBaseAutoSetter setter)
        {
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
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新调用
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        public BusinessPool Modify(Guid keyValue, IEntityBaseAutoSetter setter)
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
        /// <summary>
        /// 修改调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(Guid keyValue)
        {
            throw new NotImplementedException();
        }

        #region 线索转商机
        /// <summary>
        /// 线索转商机
        /// </summary>
        /// <param name="clue"></param>
        public static implicit operator BusinessPool(CluePool clue) => new BusinessPool()
        {
            EnCode = clue.EnCode,
            FullName = clue.FullName,
            CompanyAddress = clue.CompanyAddress,
            CompanyNetSite = clue.CompanyNetSite,
            Contact = clue.Contact,
            Mobile = clue.Mobile,
            Province = clue.Province,
            City = clue.City,
            Description = clue.Description,
            Status = clue.Status,
            SystemID = clue.SystemID,
        };
        #endregion
    }
}
