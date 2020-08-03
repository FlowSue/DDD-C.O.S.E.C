using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C.O.S.E.C
{
    public static partial class Extensions
    {
        public static bool IsNullOrEmpty(this byte? g)
        {
            if (g == default) return true;
            if (g.Value == new byte()) return true;
            return false;
        }
        public static bool IsNullOrEmpty(this byte g)
        {
            if (g == new byte()) return true;
            return false;
        }
        public static bool IsNullOrEmpty(this double? g)
        {
            if (g == default) return true;
            if (g.Value == new double()) return true;
            return false;
        }
        public static bool IsNullOrEmpty(this double g)
        {
            if (g == new double()) return true;
            return false;
        }
        public static bool IsNullOrEmpty(this float? g)
        {
            if (g == default) return true;
            if (g.Value == new float()) return true;
            return false;
        }
        public static bool IsNullOrEmpty(this float g)
        {
            if (g == new float()) return true;
            return false;
        }
        public static bool IsNullOrEmpty(this long? g)
        {
            if (g == default) return true;
            if (g.Value == new long()) return true;
            return false;
        }
        public static bool IsNullOrEmpty(this long g)
        {
            if (g == new long()) return true;
            return false;
        }
        public static bool IsNullOrEmpty(this short? g)
        {
            if (g == default) return true;
            if (g.Value == new short()) return true;
            return false;
        }
        public static bool IsNullOrEmpty(this short g)
        {
            if (g == new short()) return true;
            return false;
        }
        public static bool IsNullOrEmpty(this Decimal? g)
        {
            if (g == default) return true;
            if (g.Value == new Decimal()) return true;
            return false;
        }
        public static bool IsNullOrEmpty(this Decimal g)
        {
            if (g == new Decimal()) return true;
            return false;
        }
        public static bool IsNullOrEmpty(this DateTime? g)
        {
            if (g == default) return true;
            if (g.Value == new DateTime()) return true;
            return false;
        }

        public static bool IsNullOrEmpty(this DateTime g)
        {
            if (g == default) return true;
            if (g == new DateTime()) return true;
            return false;
        }

        public static bool IsNullOrEmpty(this string g)
        {
            if (g == default) return true;
            if (string.IsNullOrEmpty(g.Trim())) return true;
            return false;
        }
        public static bool IsNullOrEmpty(this Guid? g)
        {
            if (g == default) return true;
            if (g.Value == new Guid()) return true;
            return false;
        }
        public static bool IsNullOrEmpty(this Guid g)
        {
            if (g == default) return true;
            if (g == new Guid()) return true;
            return false;
        }
        public static bool IsNullOrEmpty(this Int32? g)
        {
            if (g == default) return true;
            if (g.Value == 0) return true;
            return false;
        }
        public static bool IsNullOrEmpty(this Int32 g)
        {
            if (g == 0) return true;
            return false;
        }
        /// <summary>
        /// 检测空值,为default则抛出ArgumentNullException异常
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckNull(this object obj, string parameterName)
        {
            if (obj == default)
                throw new ArgumentNullException(parameterName);
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this Guid? value)
        {
            if (value == default)
                return true;
            return IsEmpty(value.Value);
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this Guid value)
        {
            if (value == Guid.Empty)
                return true;
            return false;
        }
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="value">值</param>
        public static bool IsEmpty(this object value)
        {
            if (value != default && !string.IsNullOrEmpty(value.ToString()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}