using System;
using System.Collections.Generic;
using System.Text;

namespace C.O.S.E.C.Domain.Models
{
    /// <summary>
    /// 分页结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagingResult
    {
        /// <summary>
        /// 分页信息
        /// </summary>
        public Pagination Pagination { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public dynamic Data { get; set; }
    }
}
