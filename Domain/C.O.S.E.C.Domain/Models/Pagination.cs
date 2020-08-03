using System;
using System.Collections.Generic;
using System.Text;

namespace C.O.S.E.C.Domain.Models
{
    /// <summary>
    /// 分页
    /// </summary>
    public class Pagination
    {
        private int _rows;
        /// <summary>
        /// 每页行数 (最大100)
        /// </summary>
        public int Rows
        {
            get { return _rows; }
            set => _rows = value > 100 ? 100 : value;
        }
        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { get; set; }
        /// <summary>
        /// 排序列
        /// </summary>
        public string Sidx { get; set; }
        /// <summary>
        /// 排序类型
        /// </summary>
        public string Sord { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int Records { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int Total => Records > 0 ? Records % Rows == 0 ? Records / Rows : (Records / Rows) + 1 : 0;
    }
}
