using System;
using System.Collections.Generic;
using System.Text;

namespace C.O.S.E.C.Domain.Models
{
    /// <summary>
    /// 分页结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagingResult<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pagination"></param>
        public PagingResult(Pagination pagination)
        {
            this.Pagination = pagination;
        }
        /// <summary>
        /// 分页信息
        /// </summary>
        public Pagination Pagination { get; private set; }
        /// <summary>
        /// 数据
        /// </summary>
        public List<T> Data { get; set; }
    }
}
