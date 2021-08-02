using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Juan.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class DictionaryHelper
    {




        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>

        public static T GetValue<T>(this IDictionary<string, string> dict, string key, T defaultValue)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("参数key不能为空");
            }

            if (dict.Keys.Contains(key))
            {
                string value = dict[key];
                if (string.IsNullOrWhiteSpace(value))
                {
                    return defaultValue;
                }
                return value.To<T>();
            }
            else
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key">名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string GetString(this IDictionary<string, string> dict, string key, string defaultValue = "")
        {
            return GetValue(dict, key, defaultValue);
        }
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key">名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetInt(this IDictionary<string, string> dict, string key, int defaultValue)
        {
            return GetValue(dict, key, defaultValue);
        }


        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key">名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static Int64 GetInt64(this IDictionary<string, string> dict, string key, Int64 defaultValue)
        {
            return GetValue(dict, key, defaultValue);
        }
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double GetDouble(this IDictionary<string, string> dict, string key, double defaultValue)
        {
            return GetValue(dict, key, defaultValue);
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float GetFloat(this IDictionary<string, string> dict, string key, float defaultValue)
        {
            return GetValue(dict, key, defaultValue);
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal GetDecimal(this IDictionary<string, string> dict, string key, decimal defaultValue)
        {
            return GetValue(dict, key, defaultValue);
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key">名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static bool GetBool(this IDictionary<string, string> dict, string key, bool defaultValue)
        {
            string value = dict.GetString(key);

            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }
            if (value.Equals("true", StringComparison.OrdinalIgnoreCase) || value.Equals("1", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <param name="dateFormat"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(this IDictionary<string, string> dict, string key, DateTime defaultValue, string dateFormat = "yyyy-MM-dd HH:mm:ss")
        {
            string value = dict.GetString(key);
            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }
            return value.ToDateTime(dateFormat);
        }
        /// <summary>
        /// 获取配置值[默认为true]
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key">名称</param>
        /// <returns></returns>
        public static bool GetBoolTrue(this IDictionary<string, string> dict, string key)
        {
            return GetBool(dict, key, true);
        }
        /// <summary>
        /// 获取配置值[默认为false]
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="key">名称</param>
        /// <returns></returns>
        public static bool GetBoolFalse(this IDictionary<string, string> dict, string key)
        {

            return GetBool(dict, key, false);
        }

    }
}
