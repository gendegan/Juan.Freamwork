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
    /// ��������
    /// </summary>
    public partial class ConfigHelper
    {


        /// <summary>
        /// ��ȡ���ô�����Ӧ��·��
        /// </summary>
        /// <param name="ConfigCode">���ô���</param>
        /// <returns></returns>
        public static string GetConfigPath(string ConfigCode)
        {

            if (string.IsNullOrWhiteSpace(ConfigCode))
            {
                throw new ArgumentNullException("����ConfigCode����Ϊ��");
            }
            string PathConfigCode = ConfigurationManager.AppSettings[ConfigCode];
            if (!string.IsNullOrWhiteSpace(PathConfigCode))
            {
                if (!File.Exists(PathConfigCode))
                {
                    throw new ConfigurationErrorsException("�����ļ�" + ConfigCode + "���õ�·���ļ��Ҳ���:" + PathConfigCode);
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
        ///��ȡ���ý�[����ϵͳ���] 
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="configCode">���ô���</param>
        /// <returns></returns>
        public static object GetSection(string sectionName, string configCode)
        {
            return GetSection(sectionName, configCode, null);
        }

        /// <summary>
        /// ��ȡ���ý�[����ϵͳ���]
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="configCode">���ô���</param>
        /// <param name="onUpdateCallback">�����¼�</param>
        /// <returns></returns>
        public static object GetSection(string sectionName, string configCode, CacheItemUpdateCallback onUpdateCallback)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
            {
                throw new ArgumentNullException("����sectionName����Ϊ��");
            }
            if (string.IsNullOrWhiteSpace(configCode))
            {
                throw new ArgumentNullException("����configCode����Ϊ��");
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
                    throw new ConfigurationErrorsException(configCode + ".config�����ļ��Ҳ���");
                }
            }
            if (objSection == null)
            {
                throw new ConfigurationErrorsException(configCode + ".config�ļ�δ���ýڵ�" + sectionName);
            }
            return objSection;

        }
        private static ReaderWriterLockSlimHelper objConfigWriterLockSlimHelper = new ReaderWriterLockSlimHelper();

        /// <summary>
        /// ��ȡ���ô��뻺��
        /// </summary>
        /// <param name="configCode"></param>
        /// <returns></returns>
        public static Configuration GetConfigurationCode(string configCode)
        {
            return GetConfigurationCode(configCode, null);
        }
        /// <summary>
        /// ��ȡ���ô��뻺��
        /// </summary>
        /// <param name="configCode">���ô���</param>
        /// <param name="onUpdateCallback">�����¼�</param>
        /// <returns></returns>
        public static Configuration GetConfigurationCode(string configCode, CacheItemUpdateCallback onUpdateCallback)
        {
            if (string.IsNullOrWhiteSpace(configCode))
            {
                throw new ArgumentNullException("����configCode����Ϊ��");
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
        /// �����ļ�ʧЧ�����¼�
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
                throw new FileNotFoundException("δ�ҵ������ļ�" + configCode + ".config������");
            }
            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException("�Ҳ��������ļ�" + configPath);
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





