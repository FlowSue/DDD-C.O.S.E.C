using C.O.S.E.C.Domain.Enums;
using SqlSugar;
using System;

namespace C.O.S.E.C.Domain.Entity
{
    public abstract class BaseEntityModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public abstract Guid ID { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public abstract bool IsEnable { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public abstract DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public abstract string CreateUserID { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public abstract string CreateUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public abstract DateTime UpdateTime { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public abstract string UpdateUserID { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public abstract string UpdateUserName { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public abstract bool IsDelete { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public abstract StatusState Status { get; set; }

        /// <summary>
        /// 系统标识符
        /// </summary>
        public abstract string SystemID { get; set; }
    }
}
