using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    /// <summary>
    /// 组配置
    /// </summary>
    public class GroupConfigSection : ConfigurationSection
    {

        /// <summary>
        /// 组配置
        /// </summary>
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public GroupConfigCollection Groups
        {
            get
            {
                return (GroupConfigCollection)base[""];
            }
        }
    }

    /// <summary>
    /// 权限集合
    /// </summary>
    [ConfigurationCollection(typeof(GroupConfigElement), AddItemName = "Group")]
    public class GroupConfigCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new GroupConfigElement();
        }
        /// <summary>
        /// 节点类型
        /// </summary>
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }
        /// <summary>
        /// 元素KEY
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((GroupConfigElement)element).name;
        }
        /// <summary>
        /// 
        /// </summary>
        protected override string ElementName
        {
            get
            {
                return "Group";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        new public GroupConfigElement this[string name]
        {
            get
            {
                return (GroupConfigElement)BaseGet(name);
            }
        }


    }

    /// <summary>
    /// 组元素
    /// </summary>
    public class GroupConfigElement : ConfigurationElement
    {
        /// <summary>
        /// 组名称
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string name
        {
            get
            {
                return (string)base["name"];
            }
            set
            {
                base["name"] = value;
            }
        }
        /// <summary>
        /// 配置值集合
        /// </summary>
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public GroupValueCollection GroupValues
        {
            get
            {
                return (GroupValueCollection)base[""];
            }
        }
    }

    /// <summary>
    /// 组值集合
    /// </summary>
    [ConfigurationCollection(typeof(GroupValueElement), AddItemName = "add")]
    public class GroupValueCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// 创建
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new GroupValueElement();
        }
        /// <summary>
        /// 节点类型
        /// </summary>
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }
        /// <summary>
        /// 元素KEY
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((GroupValueElement)element).Key;
        }
        /// <summary>
        /// 
        /// </summary>
        protected override string ElementName
        {
            get
            {
                return "add";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        new public GroupValueElement this[string key]
        {
            get
            {
                return (GroupValueElement)BaseGet(key);
            }
        }


    }
    /// <summary>
    /// 组键值
    /// </summary>
    public class GroupValueElement : ConfigurationElement
    {
        /// <summary>
        /// 键名称
        /// </summary>
        [ConfigurationProperty("key", IsRequired = true, IsKey = true)]
        public string Key
        {
            get
            {
                return (string)base["key"];
            }
            set
            {
                base["key"] = value;
            }
        }
        /// <summary>
        /// 键值
        /// </summary>
        [ConfigurationProperty("value", DefaultValue = "")]
        public string Value
        {
            get
            {
                return base["value"].ToString();
            }
            set
            {
                base["value"] = value;
            }
        }
    }

}
