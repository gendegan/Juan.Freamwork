using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    public static partial class RequestHelper
    {


        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="isFilterSql"></param>
        /// <returns></returns>
        public static T GetValue<T>(string key, T defaultValue, bool isFilterSql = false)
        {
            string value = "";
            foreach (string item in key.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                value = SysVariable.CurrentContext.Request[item];
                if (!string.IsNullOrWhiteSpace(value))
                {
                    if (isFilterSql)
                    {
                        value = value.FilterSql();
                    }
                    break;
                }
            }
            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }
            return value.To<T>();
        }
        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <param name="key">键名称</param>
        /// <param name="isFilterSql"></param>
        /// <returns></returns>
        public static string GetString(string key, bool isFilterSql = false)
        {
            return GetValue(key, string.Empty, isFilterSql);
        }

        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <param name="key">键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="isFilterSql"></param>
        /// <returns></returns>
        public static string GetString(string key, string defaultValue, bool isFilterSql = false)
        {
            return GetValue(key, defaultValue, isFilterSql);
        }


        /// <summary>
        /// 获取参数值默认最小值MinValue
        /// </summary>
        /// <param name="key">键名称</param>
        /// <returns></returns>
        public static int GetInt(string key)
        {
            return GetValue(key, ObjectHelper.NullInt);
        }
        /// <summary>
        ///获取参数值
        /// </summary>
        /// <param name="key">键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static int GetInt(string key, int defaultValue)
        {
            return GetValue(key, defaultValue);
        }

        /// <summary>
        /// 获取参数值默认最小值MinValue
        /// </summary>
        /// <param name="key">键名称</param>
        /// <returns></returns>
        public static long GetLong(string key)
        {
            return GetValue(key, ObjectHelper.NullLong);
        }

        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <param name="key">键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static long GetLong(string key, long defaultValue)
        {
            return GetValue(key, defaultValue);
        }


        /// <summary>
        /// 获取参数值默认最小值MinValue
        /// </summary>
        /// <param name="key">键名称</param>
        /// <returns></returns>
        public static double GetDouble(string key)
        {
            return GetValue(key, ObjectHelper.NullDouble);
        }
        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <param name="key">键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static double GetDouble(string key, double defaultValue)
        {
            return GetValue(key, defaultValue);
        }

        /// <summary>
        /// 获取参数值默认最小值MinValue
        /// </summary>
        /// <param name="key">键名称</param>
        /// <returns></returns>
        public static float GetFloat(string key)
        {
            return GetValue(key, ObjectHelper.NullFloat);

        }
        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <param name="key">键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>

        public static float GetFloat(string key, float defaultValue)
        {
            return GetValue(key, defaultValue);
        }
        /// <summary>
        ///获取参数值 默认最小值MinValue
        /// </summary>
        /// <param name="key">键名称</param>
        /// <returns></returns>
        public static decimal GetDecimal(string key)
        {
            return GetValue(key, ObjectHelper.NullDecimal);
        }
        /// <summary>
        /// 获取参数值
        /// </summary>
        /// <param name="key">键名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static decimal GetDecimal(string key, decimal defaultValue)
        {
            return GetValue(key, defaultValue);
        }
    }
}
