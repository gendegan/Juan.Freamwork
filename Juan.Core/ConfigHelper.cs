using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace Juan.Core
{
    /// <summary>
    /// 配置组帮助类
    /// </summary>
    public class ConfigHelper
    {
        // Fields
        private static ReaderWriterLockSlimHelper objConfigWriterLockSlimHelper = new ReaderWriterLockSlimHelper();

        // Methods
        private static void DefaultUpdateCallback(string key, CacheItemUpdateReason reason, out object expensiveObject, out CacheDependency dependency, out DateTime absoluteExpiration, out TimeSpan slidingExpiration)
        {
            char[] separator = new char[] { '=' };
            string configCode = key.Split(separator)[1];
            string configPath = GetConfigPath(configCode);
            if (string.IsNullOrWhiteSpace(configPath))
            {
                throw new FileNotFoundException("未找到配置文件" + configCode + ".config的配置");
            }
            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException("找不到配置文件" + configPath);
            }
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = configPath
            };
            System.Configuration.Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            expensiveObject = configuration; 
            dependency = new CacheDependency(configPath);
            absoluteExpiration = Cache.NoAbsoluteExpiration;
            slidingExpiration = Cache.NoSlidingExpiration;
        }

        public static bool GetAppBool(string name, bool defaultValue, string configCode = "ApplicationConfig")
        {
            string str = GetAppString(name, "", configCode);
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }
            return (str.Equals("true", StringComparison.OrdinalIgnoreCase) || str.Equals("1"));
        }

        public static bool GetAppBoolFalse(string name, string configCode = "ApplicationConfig")
        {
            return GetAppBool(name, false, configCode);
        }

        public static bool GetAppBoolTrue(string name, string configCode = "ApplicationConfig")
        {
            return GetAppBool(name, true, configCode);
        }

        public static decimal GetAppDecimal(string name, decimal defaultValue, string configCode = "ApplicationConfig")
        {
            return GetAppValue<decimal>(name, defaultValue, configCode);
        }

        public static int GetAppInt(string name, int defaultValue, string configCode = "ApplicationConfig")
        {
            return GetAppValue<int>(name, defaultValue, configCode);
        }

        public static long GetAppInt64(string name, long defaultValue, string configCode = "ApplicationConfig")
        {
            return GetAppValue<long>(name, defaultValue, configCode);
        }

        public static string GetAppString(string name, string defaultValue = "", string configCode = "ApplicationConfig")
        {
            return GetAppValue<string>(name, defaultValue, configCode);
        }

        public static T GetAppValue<T>(string name, T defaultValue, string configCode = "ApplicationConfig")
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("参数name不能为空");
            }
            if (string.IsNullOrWhiteSpace(configCode))
            {
                throw new ArgumentNullException("参数configCode不能为空");
            }
            string str = string.Empty;
            char[] separator = new char[] { ',' };
            foreach (string str2 in name.Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                str = ConfigurationManager.AppSettings[str2];
                if (str != null)
                {
                    break;
                }
                System.Configuration.Configuration configurationCode = GetConfigurationCode(configCode);
                if (configurationCode !=null)
                {
                    KeyValueConfigurationElement element = configurationCode.AppSettings.Settings[str2];
                    if (element !=null)
                    {
                        str = element.Value;
                        break;
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }
            return str.To<T>();
        }

        public static bool GetBool(string name, bool defaultValue, string configCode = "ShareConfig")
        {
            string str = GetString(name, "", configCode);
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }
            return (str.Equals("true", StringComparison.OrdinalIgnoreCase) || str.Equals("1"));
        }

        public static bool GetBoolFalse(string name, string configCode = "ShareConfig")
        {
            return GetBool(name, false, configCode);
        }

        public static bool GetBoolTrue(string name, string configCode = "ShareConfig")
        {
            return GetBool(name, true, configCode);
        }

        public static string GetConfigPath(string ConfigCode)
        {
            if (string.IsNullOrWhiteSpace(ConfigCode))
            {
                throw new ArgumentNullException("参数ConfigCode不能为空");
            }
            string str = ConfigurationManager.AppSettings[ConfigCode];
            if (!string.IsNullOrWhiteSpace(str))
            {
                if (!File.Exists(str))
                {
                    throw new ConfigurationErrorsException("配置文件" + ConfigCode + "配置的路经文件找不到:" + str);
                }
                return str;
            }
            str = (ConfigCode + ".config").CurrentPath();
            if (File.Exists(str))
            {
                return str;
            }
            string str2 = ConfigurationManager.AppSettings["JuanPath"];
            if (string.IsNullOrWhiteSpace(str2))
            {
                str2 = @"C:\Juan";
            }
            str = Path.Combine(str2, ConfigCode + ".config");
            if (File.Exists(str))
            {
                return str;
            }
            return "";
        }

        public static System.Configuration.Configuration GetConfigurationCode(string configCode)
        {
            return GetConfigurationCode(configCode, null);
        }

        public static System.Configuration.Configuration GetConfigurationCode(string configCode, CacheItemUpdateCallback onUpdateCallback)
        {
            if (string.IsNullOrWhiteSpace(configCode))
            {
                throw new ArgumentNullException("参数configCode不能为空");
            }
            System.Configuration.Configuration objConfiguration = CacheHelper.Get<System.Configuration.Configuration>("Juan", configCode);
            if (objConfiguration == null)
            {
                return objConfigWriterLockSlimHelper.AtomWriteLock<System.Configuration.Configuration>(configCode, delegate {
                    objConfiguration = CacheHelper.Get<System.Configuration.Configuration>("Juan", configCode);
                    if (objConfiguration != null)
                    {
                        string configPath = GetConfigPath(configCode);
                        if (string.IsNullOrWhiteSpace(configPath))
                        {
                            return null;
                        }
                        ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
                        {
                            ExeConfigFilename = configPath
                        };
                        objConfiguration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                        if (onUpdateCallback == null)
                        {
                            onUpdateCallback = new CacheItemUpdateCallback(ConfigHelper.DefaultUpdateCallback);
                        }
                        CacheHelper.Insert("Juan", configCode, objConfiguration, configPath, onUpdateCallback);
                    }
                    return objConfiguration;
                });
            }
            return objConfiguration;
        }

        public static decimal GetDecimal(string name, decimal defaultValue, string configCode = "ShareConfig")
        {
            return GetValue<decimal>(name, defaultValue, configCode);
        }

        public static int GetInt(string name, int defaultValue, string configCode = "ShareConfig")
        {
            return GetValue<int>(name, defaultValue, configCode);
        }

        public static long GetInt64(string name, long defaultValue, string configCode = "ShareConfig")
        {
            return GetValue<long>(name, defaultValue, configCode);
        }

        public static object GetSection(string sectionName, string configCode)
        {
            return GetSection(sectionName, configCode, null);
        }

        public static object GetSection(string sectionName, string configCode, CacheItemUpdateCallback onUpdateCallback)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                throw new ArgumentNullException("参数sectionName不能为空");
            }
            if (string.IsNullOrWhiteSpace(configCode))
            {
                throw new ArgumentNullException("参数configCode不能为空");
            }
            object section = null;
            section = ConfigurationManager.GetSection(sectionName);
            if (section == null)
            {
                System.Configuration.Configuration configurationCode = GetConfigurationCode(configCode, onUpdateCallback);
                if (configurationCode != null)
                {
                    throw new ConfigurationErrorsException(configCode + ".config配置文件找不到");
                }
                section = configurationCode.GetSection(sectionName);
            }
            if (section == null)
            {
                throw new ConfigurationErrorsException(configCode + ".config文件未配置节点" + sectionName);
            }
            return section;
        }

        public static string GetString(string name, string defaultValue = "", string configCode = "ShareConfig")
        {
            return GetValue<string>(name, defaultValue, configCode);
        }

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
            string str = string.Empty;
            char[] separator = new char[] { ',' };
            foreach (string str2 in name.Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                str = ConfigurationManager.AppSettings[str2];
                if (str != null)
                {
                    break;
                }
                System.Configuration.Configuration configurationCode = GetConfigurationCode(configCode);
                if (configurationCode !=null)
                {
                    KeyValueConfigurationElement element = configurationCode.AppSettings.Settings[str2];
                    if (element !=null)
                    {
                        str = element.Value;
                        break;
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue;
            }
            return str.To<T>();
        }

        // Properties
        public static bool IsRelease
        {
            get
            {
                return GetValue<bool>("IsRelease", true, "ShareConfig");
            }
        }
    }

}
