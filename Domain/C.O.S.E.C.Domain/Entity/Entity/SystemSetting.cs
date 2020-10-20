using C.O.S.E.C.Domain.Enums;
using C.O.S.E.C.Domain.InterfaceDrivers;
using C.O.S.E.C.Domain.InterfaceDrivers.Services;
using System;

namespace C.O.S.E.C.Domain.Entity
{
    /// <summary>
    /// 系统设置
    /// </summary>
    public class SystemSetting : BaseEntityModel, IEntity<SystemSetting>
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
        /// 新增调用
        /// </summary>
        /// <param name="setter"></param>
        /// <returns></returns>
        public SystemSetting Create(IEntityBaseAutoSetter setter)
        {
            CreateTime = UpdateTime = setter.CreateTime;
            CreateUserID = UpdateUserID = setter.CreateId;
            CreateUserName = UpdateUserName = setter.CreateName;
            IsDelete = false;
            IsEnable = true;
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
        public SystemSetting Modify(Guid keyValue, IEntityBaseAutoSetter setter)
        {
            ID = keyValue;
            UpdateTime = setter.UpdateTime;
            UpdateUserID = setter.UpdateId;
            UpdateUserName = setter.UpdateName;
            if (!setter.SystemId.IsEmpty())
            {
                SystemID = setter.SystemId.ToString();
            }
            return this;
        }
    }
}
