using C.O.S.E.C.Infrastructure.Treasury.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace C.O.S.E.C.Infrastructure.Treasury.Models
{
    /// <summary>
    /// 接口响应数据
    /// </summary>
    public class ResponseParameter
    {
        /// <summary>
        /// 接口响应码
        /// </summary>
        public ResponseCode Code { get; set; }
        /// <summary>
        /// 接口响应消息
        /// </summary>
        public string Info { get; set; }
        /// <summary>
        /// 接口响应数据
        /// </summary>
        public dynamic Data { get; set; }
    }
}