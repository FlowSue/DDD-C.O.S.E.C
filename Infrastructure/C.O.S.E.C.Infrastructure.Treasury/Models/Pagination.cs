using System;
using System.Collections.Generic;
using System.Text;

namespace C.O.S.E.C.Infrastructure.Treasury.Models
{
    /// <summary>
    /// 分页信息
    /// </summary>
    public class Pagination
    {
        int _rows { get; set; }

        /// <summary>
        /// 每页行数 (最大100)
        /// </summary>
        public int rows
        {
            get { return _rows; }
            set
            {
                if (value > 100)
                {
                    _rows = 100;
                }
                else
                {
                    _rows = value;
                }
            }
        }

        /// <summary>
        /// 当前页
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 排序列
        /// </summary>
        public string sidx { get; set; }
        /// <summary>
        /// 排序类型
        /// </summary>
        public string sord { get; set; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int records { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int total
        {
            get
            {
                if (records > 0)
                {
                    return records % this.rows == 0 ? records / this.rows : records / this.rows + 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
