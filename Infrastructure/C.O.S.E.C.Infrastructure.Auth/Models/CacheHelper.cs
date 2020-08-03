using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace C.O.S.E.C.Infrastructure.Helper
{
    public class CacheHelper
    {
        static readonly MemoryCache Cache = new MemoryCache(new MemoryCacheOptions());
        private bool disposed = false;

        /// <summary>
        /// 获取缓存中的值
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public static object GetCacheValue(string key)
        {
            if (!string.IsNullOrEmpty(key) && Cache.TryGetValue(key, out var val))
            {
                return val;
            }
            return default;
        }

        /// <summary>
        /// 设置缓存（默认20分钟）
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SetCacheValue(string key, object value)
        {
            SetCacheValue(key, value, 20);
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="minutes"></param>
        public static void SetCacheValue(string key, object value, double minutes)
        {
            if (!string.IsNullOrEmpty(key))
            {
                Cache.Set(key, value, new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(minutes),
                });

            }
        }
    }
}
