using System;
using System.Collections.Generic;
using System.Text;

namespace C.O.S.E.C.Infrastructure.Treasury.Models
{
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
    }
}
