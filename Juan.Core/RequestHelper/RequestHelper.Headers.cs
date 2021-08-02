using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    public static partial class RequestHelper
    {


        /// <summary>
        /// 获取头部值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T GetHeadersValue<T>( string key, T defaultValue)
        {
            string value = SysVariable.CurrentContext.Request.Headers[key];
            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }
            return value.To<T>();
        }

        /// <summary>
        /// 获取头部值
        /// </summary>
        /// <param name="key">键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string GetHeadersString( string key, string defaultValue = "")
        {
            return GetHeadersValue(key, defaultValue);
        }
        /// <summary>
        ///  获取头部值
        /// </summary>
        /// <param name="key">键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetHeadersInt( string key, int defaultValue)
        {
            return GetHeadersValue(key, defaultValue);
        }
        /// <summary>
        /// 获取头部值默认最小值MinValue
        /// </summary>
        /// <param name="key">键名称</param>
        /// <returns></returns>
        public static int GetHeadersInt( string key)
        {
            return GetHeadersValue(key, ObjectHelper.NullInt);

        }
        /// <summary>
        /// 获取头部值
        /// </summary>
        /// <param name="key">键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static double GetHeadersDouble( string key, double defaultValue)
        {
            return GetHeadersValue(key, defaultValue);
        }
        /// <summary>
        /// 获取头部值默认最小值MinValue
        /// </summary>
        /// <param name="key">键名称</param>
        /// <returns></returns>
        public static double GetHeadersDouble( string key)
        {
            return GetHeadersValue(key, ObjectHelper.NullDouble);

        }
        /// <summary>
        /// 获取头部值
        /// </summary>
        /// <param name="key">键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static float GetHeadersFloat( string key, float defaultValue)
        {
            return GetHeadersValue(key, defaultValue);

        }
        /// <summary>
        /// 获取头部值默认最小值MinValue
        /// </summary>
        /// <param name="key">键名称</param>
        /// <returns></returns>
        public static double GetHeadersFloat( string key)
        {
            return GetHeadersValue(key, ObjectHelper.NullFloat);


        }
        /// <summary>
        /// 获取头部值
        /// </summary>
        /// <param name="key">键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static decimal GetHeadersDecimal( string key, decimal defaultValue)
        {
            return GetHeadersValue(key, defaultValue);

        }
        /// <summary>
        /// 获取头部值默认最小值MinValue
        /// </summary>
        /// <param name="key">键名称</param>
        /// <returns></returns>
        public static decimal GetHeadersDecimal( string key)
        {
            return GetHeadersValue(key, ObjectHelper.NullDecimal);
        }

        /// <summary>
        /// 获取头部值
        /// </summary>
        /// <param name="key">键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static long GetHeadersLong( string key, long defaultValue)
        {
            return GetHeadersValue(key, defaultValue);
        }
        /// <summary>
        /// 获取头部值默认最小值MinValue
        /// </summary>
        /// <param name="key">键名称</param>
        /// <returns></returns>
        public static long GetHeadersLong( string key)
        {

            return GetHeadersValue(key, ObjectHelper.NullLong);
        }
    }
}
