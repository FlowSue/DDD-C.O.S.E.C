using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Web;

namespace C.O.S.E.C
{
    public static partial class Extensions
    {
        #region 数值转换
        /// <summary>
        /// 转换为整型
        /// </summary>
        /// <param name="data">数据</param>
        public static int ToInt(this object data)
        {
            if (data == default)
                return 0;
            var success = int.TryParse(data.ToString(), out int result);
            if (success)
                return result;
            try
            {
                return Convert.ToInt32(ToDouble(data, 0));
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int ToInt(this bool hr)
        {
            int str = 0;
            if (hr == true)
            {
                str = 1;
            }

            return str;
        }
        /// <summary>
        /// 转换为可空整型
        /// </summary>
        /// <param name="data">数据</param>
        public static int? ToIntOrNull(this object data)
        {
            if (data == default)
                return default;
            bool isValid = int.TryParse(data.ToString(), out int result);
            if (isValid)
                return result;
            return default;
        }
        /// <summary>
        /// 转换为双精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static double ToDouble(this object data)
        {
            if (data == default)
                return 0;
            return double.TryParse(data.ToString(), out double result) ? result : 0;
        }
        /// <summary>
        /// 转换为双精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static double ToDouble(this object data, int digits)
        {
            return Math.Round(ToDouble(data), digits);
        }
        /// <summary>
        /// 转换为可空双精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static double? ToDoubleOrNull(this object data)
        {
            if (data == default)
                return default;
            bool isValid = double.TryParse(data.ToString(), out double result);
            if (isValid)
                return result;
            return default;
        }
        /// <summary>
        /// 转换为高精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static decimal ToDecimal(this object data)
        {
            if (data == default)
                return 0;
            return decimal.TryParse(data.ToString(), out decimal result) ? result : 0;
        }
        /// <summary>
        /// 转换为高精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static decimal ToDecimal(this object data, int digits)
        {
            return Math.Round(ToDecimal(data), digits);
        }
        /// <summary>
        /// 转换为可空高精度浮点数
        /// </summary>
        /// <param name="data">数据</param>
        public static decimal? ToDecimalOrNull(this object data)
        {
            if (data == default)
                return default;
            bool isValid = decimal.TryParse(data.ToString(), out decimal result);
            if (isValid)
                return result;
            return default;
        }
        /// <summary>
        /// 转换为可空高精度浮点数,并按指定的小数位4舍5入
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="digits">小数位数</param>
        public static decimal? ToDecimalOrNull(this object data, int digits)
        {
            var result = ToDecimalOrNull(data);
            if (result == default)
                return default;
            return Math.Round(result.Value, digits);
        }
        #endregion

        #region 日期转换
        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="data">数据</param>
        public static DateTime ToDate(this object data)
        {
            if (data == default)
                return DateTime.MinValue;
            return DateTime.TryParse(data.ToString(), out DateTime result) ? result : DateTime.MinValue;
        }
        /// <summary>
        /// 转换为可空日期
        /// </summary>
        /// <param name="data">数据</param>
        public static DateTime? ToDateOrNull(this object data)
        {
            if (data == default)
                return default;
            bool isValid = DateTime.TryParse(data.ToString(), out DateTime result);
            if (isValid)
                return result;
            return default;
        }

        #endregion

        #region 布尔转换
        /// <summary>
        /// 转换为布尔值
        /// </summary>
        /// <param name="data">数据</param>
        public static bool ToBool(this object data)
        {
            if (data == default)
                return false;
            bool? value = GetBool(data);
            if (value != default)
                return value.Value;
            return bool.TryParse(data.ToString(), out bool result) && result;
        }
        /// <summary>
        /// 获取布尔值
        /// </summary>
        private static bool? GetBool(this object data)
        {
            return (data.ToString().Trim().ToLower()) switch
            {
                "0" => false,
                "1" => true,
                "是" => true,
                "否" => false,
                "yes" => true,
                "no" => false,
                _ => default,
            };
        }
        /// <summary>
        /// 转换为可空布尔值
        /// </summary>
        /// <param name="data">数据</param>
        public static bool? ToBoolOrNull(this object data)
        {
            if (data == default)
                return default;
            bool? value = GetBool(data);
            if (value != default)
                return value.Value;
            bool isValid = bool.TryParse(data.ToString(), out bool result);
            if (isValid)
                return result;
            return default;
        }
        #endregion

        #region 字符串转换
        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <param name="data">数据</param>
        public static string ToString(this object data)
        {
            return data == default ? string.Empty : data.ToString().Trim();
        }

        public static string ToStr(this double hr)
        {
            string str = hr.ToString("F0");
            return str;
        }
        public static string ToStrF2(this double hr)
        {
            string str = hr.ToString("#0.00");
            return str;
        }
        public static string ToStrF2(this decimal hr)
        {
            string str = hr.ToString("#0.00");
            return str;
        }
        public static string ToStr(this double? number)
        {
            string str = number.GetValueOrDefault().ToString("F0");
            return str;
        }
        public static string ToStrF2(this double? number)
        {
            string str = number.GetValueOrDefault().ToString("#0.00");
            return str;
        }
        public static string GetValueOrDefault(this string g)
        {
            if (g == default) g = string.Empty;
            g = g.Trim();
            return g;
        }
        #endregion

        #region 实体转换  
        /// <summary>
        /// 将object对象转换为实体对象
        /// </summary>
        /// <typeparam name="T">实体对象类名</typeparam>
        /// <param name="asObject">object对象</param>
        /// <returns></returns>
        private static T ConvertObject<T>(this object asObject) where T : new()
        {
            //创建实体对象实例
            var t = Activator.CreateInstance<T>();
            if (asObject != null)
            {
                Type type = asObject.GetType();
                //遍历实体对象属性
                foreach (var info in typeof(T).GetProperties())
                {
                    object obj = null;
                    //取得object对象中此属性的值
                    var val = type.GetProperty(info.Name)?.GetValue(asObject);
                    if (val != null)
                    {
                        //非泛型
                        if (!info.PropertyType.IsGenericType)
                            obj = Convert.ChangeType(val, info.PropertyType);
                        else//泛型Nullable<>
                        {
                            Type genericTypeDefinition = info.PropertyType.GetGenericTypeDefinition();
                            if (genericTypeDefinition == typeof(Nullable<>))
                            {
                                obj = Convert.ChangeType(val, Nullable.GetUnderlyingType(info.PropertyType));
                            }
                            else
                            {
                                obj = Convert.ChangeType(val, info.PropertyType);
                            }
                        }
                        info.SetValue(t, obj, null);
                    }
                }
            }
            return t;
        }
        /// <summary>
        /// 将object对象转换为实体对象
        /// </summary>
        /// <typeparam name="T">实体对象类名</typeparam>
        /// <param name="asObject">object对象</param>
        /// <returns></returns>
        public static T ConvertObjectByJson<T>(this object asObject) where T : new()
        {
            //将object对象转换为json字符
            var json = JsonSerializer.Serialize(asObject);
            //将json字符转换为实体对象
            var t = JsonSerializer.Deserialize<T>(json);
            return t;
        }
        /// <summary>
        /// 将object尝试转为指定对象
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T ConvertObjToModel<T>(object data) where T : new()
        {
            if (data == null) return new T();
            // 定义集合    
            T result = new T();

            // 获得此模型的类型   
            Type type = typeof(T);
            string tempName = "";

            // 获得此模型的公共属性 
            PropertyInfo[] propertys = result.GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                tempName = pi.Name;  // 检查object是否包含此列    

                // 判断此属性是否有Setter      
                if (!pi.CanWrite) continue;

                try
                {
                    object value = GetPropertyValue(data, tempName);
                    if (value != DBNull.Value)
                    {
                        Type tempType = pi.PropertyType;
                        pi.SetValue(result, GetDataByType(value, tempType), null);

                    }
                }
                catch
                { }

            }

            return result;
        }

        /// <summary>
        /// 获取一个类指定的属性值
        /// </summary>
        /// <param name="info">object对象</param>
        /// <param name="field">属性名称</param>
        /// <returns></returns>
        public static object GetPropertyValue(object info, string field)
        {
            if (info == null) return null;
            Type t = info.GetType();
            IEnumerable<System.Reflection.PropertyInfo> property = from pi in t.GetProperties() where pi.Name.ToLower() == field.ToLower() select pi;
            return property.First().GetValue(info, null);
        }

        /// <summary>
        /// 将数据转为制定类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data1"></param>
        /// <returns></returns>
        public static object GetDataByType(this object data1, Type itype, params object[] myparams)
        {
            object result = new object();
            try
            {
                if (itype == typeof(decimal))
                {
                    result = Convert.ToDecimal(data1);
                    if (myparams.Length > 0)
                    {
                        result = Convert.ToDecimal(Math.Round(Convert.ToDecimal(data1), Convert.ToInt32(myparams[0])));
                    }
                }
                else if (itype == typeof(double))
                {

                    if (myparams.Length > 0)
                    {
                        result = Convert.ToDouble(Math.Round(Convert.ToDouble(data1), Convert.ToInt32(myparams[0])));
                    }
                    else
                    {
                        result = double.Parse(Convert.ToDecimal(data1).ToString("0.00"));
                    }
                }
                else if (itype == typeof(Int32))
                {
                    result = Convert.ToInt32(data1);
                }
                else if (itype == typeof(DateTime))
                {
                    result = Convert.ToDateTime(data1);
                }
                else if (itype == typeof(Guid))
                {
                    result = new Guid(data1.ToString());
                }
                else if (itype == typeof(string))
                {
                    result = data1.ToString();
                }
            }
            catch
            {
                if (itype == typeof(decimal))
                {
                    result = 0;
                }
                else if (itype == typeof(double))
                {
                    result = 0;
                }
                else if (itype == typeof(Int32))
                {
                    result = 0;
                }
                else if (itype == typeof(DateTime))
                {
                    result = null;
                }
                else if (itype == typeof(Guid))
                {
                    result = Guid.Empty;
                }
                else if (itype == typeof(string))
                {
                    result = "";
                }
            }
            return result;
        }
        #endregion
    }
}