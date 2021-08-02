using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    /// <summary>
    /// 配置组帮助类
    /// </summary>
    public partial class ConfigHelper
    {

        /// <summary>
        /// 获取组配置值列表
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <param name="configCode">配置代码</param>
        /// <returns></returns>
        public static GroupValueCollection GetGroupValues(string groupName, string configCode = "GroupConfig")
        {
            if (string.IsNullOrWhiteSpace(groupName))
            {
                throw new ArgumentNullException("groupName不能为空值");
            }
            if (string.IsNullOrWhiteSpace(configCode))
            {
                throw new ArgumentNullException("configCode不能为空值");
            }
            GroupConfigSection objGroupConfigSection = (GroupConfigSection)GetSection("GroupConfig", configCode);
            GroupConfigElement objGroupConfigElement = objGroupConfigSection.Groups[groupName];
            if (objGroupConfigElement == null)
            {
                throw new ConfigurationErrorsException("找不到组名" + groupName + "配置节点");
            }
            GroupValueCollection objGroupValueCollection = objGroupConfigElement.GroupValues;

            return objGroupValueCollection;

        }
        /// <summary>
        /// 获取组配置值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="groupName">组名</param>
        /// <param name="key">键值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="configCode">配置代码</param>
        /// <returns></returns>
        public static T GetGroupValue<T>(string groupName, string key, T defaultValue, string configCode = "GroupConfig")
        {

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key不能为空值");
            }

            GroupValueCollection objGroupValueCollection = GetGroupValues(groupName, configCode);
            string value = string.Empty;
            foreach (string item in key.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                GroupValueElement objGroupValueElement = objGroupValueCollection[item];

                if (objGroupValueElement != null)
                {
                    value = objGroupValueElement.Value;
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
        /// 获取组配置值
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <param name="key">键值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="configCode">配置代码</param>
        /// <returns></returns>
        public static string GetGroupString(string groupName, string key, string defaultValue = "", string configCode = "GroupConfig")
        {
            return GetGroupValue(groupName, key, defaultValue, configCode);
        }



        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <param name="key">键值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="configCode">配置代码</param>
        /// <returns></returns>
        public static int GetGroupInt(string groupName, string key, int defaultValue, string configCode = "GroupConfig")
        {
            return GetGroupValue(groupName, key, defaultValue, configCode);
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <param name="key">键值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="configCode">配置代码</param>
        /// <returns></returns>
        public static decimal GetGroupDecimal(string groupName, string key, decimal defaultValue, string configCode = "GroupConfig")
        {
            return GetGroupValue(groupName, key, defaultValue, configCode);
        }
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <param name="key">键值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="configCode">配置代码</param>
        /// <returns></returns>
        public static Int64 GetGroupInt64(string groupName, string key, Int64 defaultValue, string configCode = "GroupConfig")
        {
            return GetGroupValue(groupName, key, defaultValue, configCode);
        }

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <param name="key">键值</param>
        /// <param name="defaultValue">默认值</param>
        /// <param name="configCode">配置代码</param>
        /// <returns></returns>
        public static bool GetGroupBool(string groupName, string key, bool defaultValue, string configCode = "GroupConfig")
        {
            string value = GetGroupString(groupName, key, "", configCode);
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
        ///  获取配置值[默认为true]
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="key"></param>
        /// <param name="configCode"></param>
        /// <returns></returns>
        public static bool GetGroupBoolTrue(string groupName, string key, string configCode = "GroupConfig")
        {
            return GetGroupBool(groupName, key, true, configCode);
        }
        /// <summary>
        /// 获取配置值[默认为false]
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="key"></param>
        /// <param name="configCode"></param>
        /// <returns></returns>
        public static bool GetGroupBoolFalse(string groupName, string key, string configCode = "GroupConfig")
        {

            return GetGroupBool(groupName, key, false, configCode);
        }

    }
}
