using C.O.S.E.C.Domain.Enums;
using C.O.S.E.C.Domain.InterfaceDrivers;
using C.O.S.E.C.Domain.InterfaceDrivers.Services;
using System;

namespace C.O.S.E.C.Domain.Entity
{
    /// <summary>
    /// 用户文件
    /// </summary>
    public class UserFile : IEntity<UserFile>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExt { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 文件原名
        /// </summary>
        public string FormerName { get; set; }
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
        public string SystemID { get; set; }
        /// <summary>
        /// 新增调用
        /// </summary>
        /// <param name="setter"></param>
        /// <returns></returns>
        public UserFile Create(IEntityBaseAutoSetter setter)
        {
            CreateTime = setter.CreateTime;
            CreateUserID = setter.CreateId;
            CreateUserName = setter.CreateName;
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
        public UserFile Modify(Guid keyValue, IEntityBaseAutoSetter setter)
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
