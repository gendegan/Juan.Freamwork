using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Configuration;
using System.Configuration;
using System.IO;
using System.Web;
using System.Threading;

namespace Juan.Core
{
    #region 程序配置
    /// <summary>
    /// 程序配置
    /// </summary>
    public partial class ConfigHelper
    {


        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="name">名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="configCode">配置代码</param>
        /// <returns></returns>
        public static T GetValue<T>(string name, T defaultValue, string configCode = "ShareConfig")
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("参数name不能为空");
            }

            if (string.IsNullOrWhiteSpace(configCode))
            {
                throw new ArgumentNullException("参数configCode不能为空");
            }
            string value = string.Empty;
            foreach (string item in name.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                value = ConfigurationManager.AppSettings[item];
                if (value == null)
                {
                    Configuration objConfiguration = GetConfigurationCode(configCode);
                    if (objConfiguration != null)
                    {
                        KeyValueConfigurationElement objKeyValue = objConfiguration.AppSettings.Settings[item];
                        if (objKeyValue != null)
                        {
                            value = objKeyValue.Value;
                            break;
                        }
                    }

                }
                else
                {
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
        /// 获取配置值
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="configCode">配置代码</param>
        /// <returns></returns>
        public static string GetString(string name, string defaultValue = "", string configCode = "ShareConfig")
        {
            return GetValue(name, defaultValue, configCode);
        }
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="configCode">配置代码</param>
        /// <returns></returns>
        public static int GetInt(string name, int defaultValue, string configCode = "ShareConfig")
        {
            return GetValue(name, defaultValue, configCode);
        }
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="configCode">配置代码</param>
        /// <returns></returns>
        public static decimal GetDecimal(string name, decimal defaultValue, string configCode = "ShareConfig")
        {
            return GetValue(name, defaultValue, configCode);
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="configCode">配置代码</param>
        /// <returns></returns>
        public static Int64 GetInt64(string name, Int64 defaultValue, string configCode = "ShareConfig")
        {
            return GetValue(name, defaultValue, configCode);
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="configCode">配置代码</param>
        /// <returns></returns>
        public static bool GetBool(string name, bool defaultValue, string configCode = "ShareConfig")
        {
            string value = GetString(name, "", configCode);
            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }
            if (value.Equals("true", StringComparison.OrdinalIgnoreCase) || value.Equals("1"))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取配置值[默认为true]
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="configCode">配置代码</param>
        /// <returns></returns>
        public static bool GetBoolTrue(string name, string configCode = "ShareConfig")
        {
            return GetBool(name, true, configCode);
        }
        /// <summary>
        /// 获取配置值[默认为false]
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="configCode">配置代码</param>
        /// <returns></returns>
        public static bool GetBoolFalse(string name, string configCode = "ShareConfig")
        {

            return GetBool(name, false, configCode);
        }
        /// <summary>
        /// 是否发布
        /// </summary>
        public static bool IsRelease
        {
            get
            {
                return GetValue("IsRelease", true);
            }
        }
    }

    #endregion


}
