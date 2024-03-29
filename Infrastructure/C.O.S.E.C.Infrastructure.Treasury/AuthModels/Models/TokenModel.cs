﻿//本地项目包
using C.O.S.E.C.Infrastructure.Auth.Enums;

namespace C.O.S.E.C.Infrastructure.Auth.Models
{
    /// <summary>
    /// 令牌
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// 登录用户Id
        /// </summary>
        public string Uid { get; set; }
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string Uname { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string Rname { get; set; }
        /// <summary>
        /// 身份
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Project { get; set; }
        /// <summary>
        /// 系统标识
        /// </summary>
        public string SystemId { get; set; }
        /// <summary>
        /// 令牌类型（终端类型）
        /// </summary>
        public TokenTypeEnum TokenType { get; set; }
    }
}
