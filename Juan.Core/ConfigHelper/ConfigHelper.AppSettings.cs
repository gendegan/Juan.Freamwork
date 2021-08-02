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
        /// ��ȡ����ֵ
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="name">����</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <param name="configCode">���ô���</param>
        /// <returns></returns>
        public static T GetValue<T>(string name, T defaultValue, string configCode = "ShareConfig")
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
        /// ��ȡ����ֵ
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <param name="configCode">���ô���</param>
        /// <returns></returns>
        public static string GetString(string name, string defaultValue = "", string configCode = "ShareConfig")
        {
            return GetValue(name, defaultValue, configCode);
        }
        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <param name="configCode">���ô���</param>
        /// <returns></returns>
        public static int GetInt(string name, int defaultValue, string configCode = "ShareConfig")
        {
            return GetValue(name, defaultValue, configCode);
        }
        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <param name="configCode">���ô���</param>
        /// <returns></returns>
        public static decimal GetDecimal(string name, decimal defaultValue, string configCode = "ShareConfig")
        {
            return GetValue(name, defaultValue, configCode);
        }

        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <param name="configCode">���ô���</param>
        /// <returns></returns>
        public static Int64 GetInt64(string name, Int64 defaultValue, string configCode = "ShareConfig")
        {
            return GetValue(name, defaultValue, configCode);
        }

        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
        /// <param name="configCode">���ô���</param>
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
        /// ��ȡ����ֵ[Ĭ��Ϊtrue]
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="configCode">���ô���</param>
        /// <returns></returns>
        public static bool GetBoolTrue(string name, string configCode = "ShareConfig")
        {
            return GetBool(name, true, configCode);
        }
        /// <summary>
        /// ��ȡ����ֵ[Ĭ��Ϊfalse]
        /// </summary>
        /// <param name="name">����</param>
        /// <param name="configCode">���ô���</param>
        /// <returns></returns>
        public static bool GetBoolFalse(string name, string configCode = "ShareConfig")
        {

            return GetBool(name, false, configCode);
        }
        /// <summary>
        /// �Ƿ񷢲�
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
