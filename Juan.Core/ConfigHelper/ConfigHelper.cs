using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Configuration;
using System.Configuration;
using System.IO;
using System.Web;
using System.Threading;
using System.Web.Caching;

namespace Juan.Core
{

    /// <summary>
    /// 程序配置
    /// </summary>
    public partial class ConfigHelper
    {


        /// <summary>
        /// 获取配置代码相应的路经
        /// </summary>
        /// <param name="ConfigCode">配置代码</param>
        /// <returns></returns>
        public static string GetConfigPath(string ConfigCode)
        {

            if (string.IsNullOrWhiteSpace(ConfigCode))
            {
                throw new ArgumentNullException("参数ConfigCode不能为空");
            }
            string PathConfigCode = ConfigurationManager.AppSettings[ConfigCode];
            if (!string.IsNullOrWhiteSpace(PathConfigCode))
            {
                if (!File.Exists(PathConfigCode))
                {
                    throw new ConfigurationErrorsException("配置文件" + ConfigCode + "配置的路经文件找不到:" + PathConfigCode);
                }
                return PathConfigCode;
            }

            PathConfigCode = PathHelper.CurrentPath(ConfigCode + ".config");
            if (File.Exists(PathConfigCode))
            {
                return PathConfigCode;
            }
            string JuanPath = ConfigurationManager.AppSettings["JuanPath"];
            if (string.IsNullOrWhiteSpace(JuanPath))
            {
                JuanPath = @"C:\Juan";
            }
            PathConfigCode = Path.Combine(JuanPath, ConfigCode + ".config");
            if (File.Exists(PathConfigCode))
            {
                return PathConfigCode;
            }
            return "";

        }
        /// <summary>
        ///获取配置节[兼容系统框架] 
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="configCode">配置代码</param>
        /// <returns></returns>
        public static object GetSection(string sectionName, string configCode)
        {
            return GetSection(sectionName, configCode, null);
        }

        /// <summary>
        /// 获取配置节[兼容系统框架]
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="configCode">配置代码</param>
        /// <param name="onUpdateCallback">更新事件</param>
        /// <returns></returns>
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
            object objSection = null;
            objSection = ConfigurationManager.GetSection(sectionName);
            if (objSection == null)
            {
                Configuration objConfiguration = GetConfigurationCode(configCode, onUpdateCallback);
                if (objConfiguration != null)
                {
                    objSection = objConfiguration.GetSection(sectionName);
                }
                else
                {
                    throw new ConfigurationErrorsException(configCode + ".config配置文件找不到");
                }
            }
            if (objSection == null)
            {
                throw new ConfigurationErrorsException(configCode + ".config文件未配置节点" + sectionName);
            }
            return objSection;

        }
        private static ReaderWriterLockSlimHelper objConfigWriterLockSlimHelper = new ReaderWriterLockSlimHelper();

        /// <summary>
        /// 获取配置代码缓存
        /// </summary>
        /// <param name="configCode"></param>
        /// <returns></returns>
        public static Configuration GetConfigurationCode(string configCode)
        {
            return GetConfigurationCode(configCode, null);
        }
        /// <summary>
        /// 获取配置代码缓存
        /// </summary>
        /// <param name="configCode">配置代码</param>
        /// <param name="onUpdateCallback">更新事件</param>
        /// <returns></returns>
        public static Configuration GetConfigurationCode(string configCode, CacheItemUpdateCallback onUpdateCallback)
        {
            if (string.IsNullOrWhiteSpace(configCode))
            {
                throw new ArgumentNullException("参数configCode不能为空");
            }
            Configuration objConfiguration = CacheHelper.Get<Configuration>("Juan", configCode);
            if (objConfiguration == null)
            {

                return objConfigWriterLockSlimHelper.AtomWriteLock(configCode, () =>
                {

                    objConfiguration = CacheHelper.Get<Configuration>("Juan", configCode);
                    if (objConfiguration != null)
                    {
                        return objConfiguration;
                    }

                    string configPath = GetConfigPath(configCode);
                    if (string.IsNullOrWhiteSpace(configPath))
                    {
                        return null;
                    }

                    ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                    fileMap.ExeConfigFilename = configPath;
                    objConfiguration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                    if (onUpdateCallback == null)
                    {
                        onUpdateCallback = DefaultUpdateCallback;
                    }
                    CacheHelper.Insert("Juan", configCode, objConfiguration, configPath, onUpdateCallback);
                    return objConfiguration;
                });
            }
            else
            {
                return objConfiguration;
            }
        }






        /// <summary>
        /// 配置文件失效更新事件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="reason"></param>
        /// <param name="expensiveObject"></param>
        /// <param name="dependency"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        private static void DefaultUpdateCallback(
  string key, CacheItemUpdateReason reason,
  out object expensiveObject,
  out CacheDependency dependency,
  out DateTime absoluteExpiration,
  out TimeSpan slidingExpiration)
        {

            string configCode = key.Split('=')[1];
            string configPath = GetConfigPath(configCode);
            if (string.IsNullOrWhiteSpace(configPath))
            {
                throw new FileNotFoundException("未找到配置文件" + configCode + ".config的配置");
            }
            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException("找不到配置文件" + configPath);
            }
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = configPath;
            Configuration objConfiguration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            expensiveObject = objConfiguration;
            dependency = new CacheDependency(configPath);
            absoluteExpiration = Cache.NoAbsoluteExpiration;
            slidingExpiration = Cache.NoSlidingExpiration;

        }
    }


}





