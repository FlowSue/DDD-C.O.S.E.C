using C.O.S.E.C.Domain.Enums;
using C.O.S.E.C.Domain.InterfaceDrivers;
using C.O.S.E.C.Domain.InterfaceDrivers.Services;
using System;

namespace C.O.S.E.C.Domain.Entity
{
    /// <summary>
    /// 系统访问日志
    /// </summary>
    public class _SystemActionLog : BaseEntityModel, IEntity<_SystemActionLog>
    {
        /// <summary>
        /// 调用API地址
        /// </summary>
        public string ActionPath { get; set; }

        /// <summary>
        /// 请求者IP
        /// </summary>
        public string RequestIP { get; set; }

        /// <summary>
        /// 访问控制器
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// 执行动作
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 主键
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

        /// <summary></summary>
        public override DateTime UpdateTime { get; set; }

        /// <summary></summary>
        public override string UpdateUserID { get; set; }

        /// <summary></summary>
        public override string UpdateUserName { get; set; }

        /// <summary></summary>
        public override bool IsDelete { get; set; } = false;

        /// <summary></summary>
        public override StatusState Status { get; set; } = StatusState.Normal;

        /// <summary>
        /// 系统标识
        /// </summary>
        public override string SystemID { get; set; }

        /// <summary>
        /// 新增调用
        /// </summary>
        /// <param name="setter"></param>
        /// <returns></returns>
        public _SystemActionLog Create(IEntityBaseAutoSetter setter)
        {
            ID = Guid.NewGuid();
            if (!setter.CreateId.IsNullOrEmpty())
            {
                CreateUserID = UpdateUserID = setter.CreateId;
                CreateUserName = UpdateUserName = setter.CreateName;
            }
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
        public _SystemActionLog Modify(Guid keyValue, IEntityBaseAutoSetter setter)
        {
            throw new NotImplementedException();
        }
    }
}
