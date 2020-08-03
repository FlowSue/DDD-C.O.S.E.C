using System.Collections.Generic;
using System.Linq;

namespace C.O.S.E.C
{
    /// <summary>
    /// 扩展 - 可空类型
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 安全返回值
        /// </summary>
        /// <param name="value">可空值</param>
        public static T SafeValue<T>(this T? value) where T : struct
        {
            return value ?? default;
        }

        public static IEnumerable<T> CheckNull<T>(this IEnumerable<T> list)
        {
            return list == default ? new List<T>(0) : list;
        }

        public static TSource FirstOrNew<TSource>(this IEnumerable<TSource> source) where TSource : class, new()
        {
            TSource _TSource = source.FirstOrDefault();
            return _TSource == default ? new TSource() : _TSource;
        }

        public static TSource FirstOrNew<TSource>(this List<TSource> source) where TSource : class, new()
        {
            TSource _TSource = source.FirstOrDefault();
            return _TSource == default ? new TSource() : _TSource;
        }
        public static TSource FirstOrNew<TSource>(this IOrderedEnumerable<TSource> source) where TSource : class, new()
        {
            TSource _TSource = source.FirstOrDefault();
            return _TSource == default ? new TSource() : _TSource;
        }

        public static TSource FirstOrNew<TSource>(this TSource source) where TSource : class, new()
        {
            TSource _TSource = source;
            return _TSource == default ? new TSource() : _TSource;
        }
    }
}
