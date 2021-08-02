using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 加密连接字符串
    /// </summary>
    public class ContextSection : ConfigurationSection
    {
        /// <summary>
        /// 连接串节点
        /// </summary>
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public ContextCollection ConnectionStrings
        {
            get
            {
                return (ContextCollection)base[""];
            }
        }



    }

    /// <summary>
    /// 权限集合
    /// </summary>
    [ConfigurationCollection(typeof(ContextElement), AddItemName = "Context")]
    public class ContextCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ContextElement();
        }
        /// <summary>
        /// 元素KEY
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ContextElement)element).Name;
        }
        /// <summary>
        /// 
        /// </summary>
        protected override string ElementName
        {
            get
            {
                return "Context";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string[] ContextKeys
        {
            get
            {
                return BaseGetAllKeys().ToArray<string>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        new public ContextElement this[string name]
        {
            get
            {
                return (ContextElement)BaseGet(name);
            }
        }

    }

    /// <summary>
    /// Context元素
    /// </summary>
    public class ContextElement : ConfigurationElement
    {
        /// <summary>
        /// 连接字符串名称
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
        /// 
        /// </summary>
        [ConfigurationProperty("ConnectionString", IsRequired = true)]
        public string ConnectionString
        {
            get
            {
                return (string)base["ConnectionString"];
            }
            set
            {
                base["ConnectionString"] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("ContextType", IsRequired = false, DefaultValue = ContextType.MySql)]
        public ContextType ContextType
        {
            get
            {
                return (ContextType)base["ContextType"];
            }
            set
            {
                base["ContextType"] = value;
            }
        }

        /// <summary>
        /// 连接串节点
        /// </summary>
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public ReadContextCollection Reads
        {
            get
            {
                return (ReadContextCollection)base[""];
            }
        }


    }

    /// <summary>
    /// 
    /// </summary>
    [ConfigurationCollection(typeof(ReadContextElement), AddItemName = "Read")]
    public class ReadContextCollection : ConfigurationElementCollection
    {

        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ReadContextElement();
        }
        /// <summary>
        /// 元素KEY
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ReadContextElement)element).Name;
        }
        /// <summary>
        /// 
        /// </summary>
        public string[] ContextKeys
        {
            get
            {
                return BaseGetAllKeys().ToArray<string>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected override string ElementName
        {
            get
            {
                return "Read";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        new public ReadContextElement this[string name]
        {
            get
            {
                return (ReadContextElement)BaseGet(name);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ReadContextElement this[int index]
        {

            get
            {
                return (ReadContextElement)BaseGet(index);
            }

        }

    }


    /// <summary>
    /// 
    /// </summary>
    public class ReadContextElement : ConfigurationElement
    {
        /// <summary>
        /// 连接字符串名称
        /// </summary>
        [ConfigurationProperty("Name", IsRequired = false, IsKey = true, DefaultValue = "ReadOne")]
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
        /// 
        /// </summary>
        [ConfigurationProperty("ConnectionString", IsRequired = true)]
        public string ConnectionString
        {
            get
            {
                return (string)base["ConnectionString"];
            }
            set
            {
                base["ConnectionString"] = value;
            }
        }


    }

}
