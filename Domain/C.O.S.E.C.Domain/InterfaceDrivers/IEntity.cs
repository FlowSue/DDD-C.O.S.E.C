using C.O.S.E.C.Domain.Entity;
using C.O.S.E.C.Domain.Enums;
using C.O.S.E.C.Domain.InterfaceDrivers.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace C.O.S.E.C.Domain.InterfaceDrivers
{
    /// <summary>
    /// 实体通用接口
    /// </summary>
    public interface IEntity<T> where T : BaseEntityModel
    {
        /// <summary>
        /// ID
        /// </summary>
        Guid ID { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        bool IsEnable { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        string CreateUserID { get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        string CreateUserName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime UpdateTime { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        string UpdateUserID { get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        string UpdateUserName { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        bool IsDelete { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        StatusState Status { get; set; }
        /// <summary>
        /// 系统标识符
        /// </summary>
        string SystemID { get; set; }
        /// <summary>
        /// 新增调用
        /// </summary>
        T Create(IEntityBaseAutoSetter setter);
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue">主键值</param>
        T Modify(Guid keyValue, IEntityBaseAutoSetter setter);
    }
}
