using System;
using System.Collections.Generic;
using System.Text;

namespace C.O.S.E.C.Infrastructure.Treasury.Models
{
    public class PagingResult<T> : Pagination
    {
        public PagingResult(Pagination pagination)
        {
            base.page = pagination.page;
            base.records = pagination.records;
            base.rows = pagination.rows;
            base.sidx = pagination.sidx;
            base.sord = pagination.sord;
        }

        /// <summary>
        /// 元素集合
        /// </summary>
        public List<T> List { get; set; }
    }
}
