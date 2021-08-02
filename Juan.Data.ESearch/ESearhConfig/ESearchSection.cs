
using Juan.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Juan.Data.ESearch
{
    public class ESearchSection : ConfigurationSection
    {
        /// <summary>
        /// 配置提供者
        /// </summary>
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public ESProviderCollection Providers
        {
            get
            {
                return (ESProviderCollection)base[""];
            }
        }
    }

    /// <summary>
    /// Redis配置集合
    /// </summary>
    [ConfigurationCollection(typeof(ESProviderElement), AddItemName = "Provider")]
    public class ESProviderCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ESProviderElement();
        }
        /// <summary>
        /// 元素KEY
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ESProviderElement)element).Name;
        }
        /// <summary>
        /// 
        /// </summary>
        protected override string ElementName
        {
            get
            {
                return "Provider";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        new public ESProviderElement this[string name]
        {
            get
            {
                return (ESProviderElement)BaseGet(name);
            }
        }
    }

    public class ESProviderElement : ConfigurationElement
    {    /// <summary>
        /// 资源设置名称
        /// </summary>
        [ConfigurationProperty("Name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)base["Name"];
            }
            set
            {
                base["Name"] = value;
            }
        }

        /// <summary>
        /// 服务地址
        /// </summary>
        [ConfigurationProperty("ServerUrl", DefaultValue = "", IsRequired = true)]
        public string ServerUrl
        {
            get
            {
                return ((string)base["ServerUrl"]);
            }
            set
            {
                base["ServerUrl"] = value;
            }
        }


        [ConfigurationProperty("MaxResult", DefaultValue = 10000, IsRequired = false)]
        public int MaxResult
        {
            get
            {
                return ((int)base["MaxResult"]);
            }
            set
            {
                base["MaxResult"] = value;
            }
        }
        [ConfigurationProperty("SslPath", DefaultValue = "", IsRequired = false)]
        public string SslPath
        {
            get
            {
                return ((string)base["SslPath"]);
            }
            set
            {
                base["SslPath"] = value;
            }
        }

        [ConfigurationProperty("SslPassword", DefaultValue = "", IsRequired = false)]
        public string SslPassword
        {
            get
            {
                return ((string)base["SslPassword"]);
            }
            set
            {
                base["SslPassword"] = value;
            }
        }


    }

    public static class ESConfigHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ESearchSection GetESearchSection()
        {
            object objESearchSection = ConfigHelper.GetSection("ESearch7Config", "ESearchConfig");
            if (objESearchSection == null)
            {
                throw new ConfigurationErrorsException("没有找ESearch7Config配置节点");
            }
            return (ESearchSection)objESearchSection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <returns></returns>
        public static ESProviderElement GetProducerElement(string providerName)
        {
            ESearchSection objESConfigSection = GetESearchSection();
            ESProviderElement objProviderElement = objESConfigSection.Providers[providerName];
            if (objProviderElement == null)
            {
                throw new ConfigurationErrorsException("没有找到ESearch7Config节点名：" + providerName + "配置节点");
            }
            return objProviderElement;
        }

        public static string GetProducerServerUrl(string providerName)
        {

            ESProviderElement objESProviderElement = GetProducerElement(providerName);
            string UriUrl = "";
            string[] ArrUriUrl = objESProviderElement.ServerUrl.Split(new char[] { ',', '，', ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (ArrUriUrl.Length == 1)
            {
                UriUrl = ArrUriUrl[0];
            }
            else
            {
                UriUrl = ArrUriUrl[RandomHelper.GetRandomNumber(0, ArrUriUrl.Length)];
            }
            return UriUrl;
        }


        public static string ProcessServerUrl(this ESProviderElement objESProviderElement)
        {
            string UriUrl = "";
            string[] ArrUriUrl = objESProviderElement.ServerUrl.Split(new char[] { ',', '，', ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (ArrUriUrl.Length == 1)
            {
                UriUrl = ArrUriUrl[0];
            }
            else
            {
                UriUrl = ArrUriUrl[RandomHelper.GetRandomNumber(0, ArrUriUrl.Length)];
            }
            return UriUrl;
        }


    }
}
