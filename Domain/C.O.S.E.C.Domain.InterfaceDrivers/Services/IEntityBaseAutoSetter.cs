//系统包
using System;

namespace C.O.S.E.C.Domain.InterfaceDrivers.Services
{
    /// <summary>
    /// 实体库自动设置器
    /// </summary>
    public interface IEntityBaseAutoSetter
    {
        /// <summary>创建人姓名</summary>
        /// <value>The name of the create.</value>
        string CreateName { get; }
        /// <summary>创建人Id</summary>
        /// <value>The create identifier.</value>
        string CreateId { get; }
        /// <summary>创建时间</summary>
        /// <value>The create time.</value>
        DateTime CreateTime { get; }
        /// <summary>更新人姓名</summary>
        /// <value>The name of the update.</value>
        string UpdateName { get; }
        /// <summary>更新人Id</summary>
        /// <value>The update identifier.</value>
        string UpdateId { get; }
        /// <summary>更新时间</summary>
        /// <value>The update time.</value>
        DateTime UpdateTime { get; }
        /// <summary>jwt token</summary>
        string TokenStr { get; }
        /// <summary>系统ID</summary>
        string SystemId { get; }
    }
}
