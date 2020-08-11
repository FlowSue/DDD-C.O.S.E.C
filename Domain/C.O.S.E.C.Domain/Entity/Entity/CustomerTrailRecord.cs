using C.O.S.E.C.Domain.Enums;
using C.O.S.E.C.Domain.InterfaceDrivers;
using C.O.S.E.C.Domain.InterfaceDrivers.Services;
using SqlSugar;
using System;

namespace C.O.S.E.C.Domain.Entity
{
    /// <summary>
    /// 跟进记录
    /// </summary>
    public class CustomerTrailRecord : BaseEntityModel, IEntity<CustomerTrailRecord>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public override Guid ID { get; set; }

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
        /// 商机ID
        /// </summary>
        public string BusinessPoolID { get; set; }

        /// <summary>
        /// 跟进内容
        /// </summary>
        public string TrackContent { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contacts { get; set; }

        /// <summary>
        /// 跟进时间
        /// </summary>
        public DateTime DayDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 跟进类型
        /// </summary>
        public string TrackTypeId { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string CompanyName { get; set; }
        
        /// <summary>
        /// 新增调用
        /// </summary>
        /// <param name="setter"></param>
        /// <returns></returns>
        public CustomerTrailRecord Create(IEntityBaseAutoSetter setter)
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
        /// 更新调用
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="setter"></param>
        /// <returns></returns>
        public CustomerTrailRecord Modify(Guid keyValue, IEntityBaseAutoSetter setter)
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