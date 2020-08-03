using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace C.O.S.E.C.Infrastructure.Treasury.Helpers
{
    public static class EncryptionHelper
    {
        /// <summary>
        /// 小写Base64_MD5
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static string MD5ToBase64String(string inputValue) => Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.Unicode.GetBytes(inputValue))).ToLower();
        /// <summary>
        /// 小写Base64_MD5（加盐）
        /// </summary>
        /// <param name="inputValue"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string MD5ToBase64String(string inputValue, string salt = "") => Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.Unicode.GetBytes(inputValue + salt))).ToLower();
        /// <summary>
        /// 小写32位MD5
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static string MD5_32(string inputValue) => BitConverter.ToString(MD5.Create().ComputeHash(Encoding.Unicode.GetBytes(inputValue))).Replace("-", "").ToLower();
        /// <summary>
        /// 小写32位MD5（加盐）
        /// </summary>
        /// <param name="inputValue"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string MD5_32(string inputValue, string salt = "") => BitConverter.ToString(MD5.Create().ComputeHash(Encoding.Unicode.GetBytes(inputValue + salt))).Replace("-", "").ToLower();
        /// <summary>
        /// 小写16位MD5
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static string MD5_16(string inputValue) => BitConverter.ToString(MD5.Create().ComputeHash(Encoding.Unicode.GetBytes(inputValue)), 4, 8).Replace("-", "").ToLower();
        /// <summary>
        /// 小写16位MD5（加盐）
        /// </summary>
        /// <param name="inputValue"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string MD5_16(string inputValue, string salt = "") => BitConverter.ToString(MD5.Create().ComputeHash(Encoding.Unicode.GetBytes(inputValue + salt)), 4, 8).Replace("-", "").ToLower();
        ///// <summary>
        ///// 生成32位标准MD5
        ///// </summary>
        ///// <param name="inputValue"></param>
        ///// <param name="temp"></param>
        ///// <returns></returns>
        //public static string MD5_32(string inputValue, string temp)
        //{
        //    using var md5 = MD5.Create();
        //    var data = md5.ComputeHash(Encoding.Unicode.GetBytes(inputValue));
        //    StringBuilder builder = new StringBuilder();
        //    for (int i = 0; i < data.Length; i++)
        //    {
        //        builder.Append(data[i].ToString("X2"));
        //    }
        //    return builder.ToString();
        //}
        ///// <summary>
        ///// 生成16位标准MD5
        ///// </summary>
        ///// <param name="inputValue"></param>
        ///// <returns></returns>
        //public static string MD5_16(string inputValue, string temp)
        //{
        //    using var md5 = MD5.Create();
        //    var data = md5.ComputeHash(Encoding.UTF8.GetBytes(inputValue));
        //    StringBuilder builder = new StringBuilder();
        //    for (int i = 0; i < data.Length; i++)
        //    {
        //        builder.Append(data[i].ToString("X2"));
        //    }
        //    return builder.ToString().Substring(8, 16);
        //}
    }
}
