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
    #region ��������
    /// <summary>
    /// ��������
    /// </summary>
    public partial class ConfigHelper
    {

        /// <summary>
        /// ��ȡApp����ֵ
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="name">����</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <param name="configCode">���ô���</param>
        /// <returns></returns>
        public static T GetAppValue<T>(string name, T defaultValue, string configCode = "ApplicationConfig")
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("����name����Ϊ��");
            }

            if (string.IsNullOrWhiteSpace(configCode))
            {
                throw new ArgumentNullException("����configCode����Ϊ��");
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
        /// ��ȡApp����ֵ
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <param name="configCode">���ô���</param>
        /// <returns></returns>
        public static string GetAppString(string name, string defaultValue = "", string configCode = "ApplicationConfig")
        {
            return GetAppValue(name, defaultValue, configCode);
        }


        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <param name="configCode">���ô���</param>
        /// <returns></returns>
        public static int GetAppInt(string name, int defaultValue, string configCode = "ApplicationConfig")
        {
            return GetAppValue(name, defaultValue, configCode);
        }
        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <param name="configCode">���ô���</param>
        /// <returns></returns>
        public static decimal GetAppDecimal(string name, decimal defaultValue, string configCode = "ApplicationConfig")
        {
            return GetAppValue(name, defaultValue, configCode);
        }
        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <param name="configCode">���ô���</param>
        /// <returns></returns>
        public static Int64 GetAppInt64(string name, Int64 defaultValue, string configCode = "ApplicationConfig")
        {
            return GetAppValue(name, defaultValue, configCode);
        }

        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <param name="configCode">���ô���</param>
        /// <returns></returns>
        public static bool GetAppBool(string name, bool defaultValue, string configCode = "ApplicationConfig")
        {
            string value = GetAppString(name, "", configCode);
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
        /// ��ȡ����ֵ[Ĭ��Ϊtrue]
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="configCode">���ô���</param>
        /// <returns></returns>
        public static bool GetAppBoolTrue(string name, string configCode = "ApplicationConfig")
        {
            return GetAppBool(name, true, configCode);
        }
        /// <summary>
        /// ��ȡ����ֵ[Ĭ��Ϊfalse]
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="configCode">���ô���</param>
        /// <returns></returns>
        public static bool GetAppBoolFalse(string name, string configCode = "ApplicationConfig")
        {

            return GetAppBool(name, false, configCode);
        }
    }

    #endregion


}
