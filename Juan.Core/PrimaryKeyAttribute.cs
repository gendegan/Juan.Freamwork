using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 主键标识
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PrimaryKeyAttribute : Attribute
    {


        bool _Identity = true;
        /// <summary>
        /// 是否自增
        /// </summary>
        public bool Identity
        {
            get
            {
                return _Identity;
            }
            set
            {
                _Identity = value;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="identity">是否是自增</param>
        public PrimaryKeyAttribute(bool identity = true)
        {
            _Identity = identity;
        }
        PropertyInfo _PrimaryProperty = null;
        /// <summary>
        /// 主键属性
        /// </summary>
        public PropertyInfo PrimaryProperty
        {
            get
            {

                return _PrimaryProperty;
            }
            set
            {
                _PrimaryProperty = value;
            }

        }
        /// <summary>
        /// 主键名称
        /// </summary>
        public string PrimaryName
        {
            get
            {
                return _PrimaryProperty == null ? "" : _PrimaryProperty.Name;
            }

        }
        /// <summary>
        /// 主键类型
        /// </summary>
        public Type PrimaryType
        {
            get
            {
                return _PrimaryProperty == null ? null : _PrimaryProperty.PropertyType;
            }

        }
    }

    /// <summary>
    /// 实体主键帮助类
    /// </summary>
    public static class PrimaryKeyHelper
    {


        /// <summary>
        /// 获取主键特性
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static PrimaryKeyAttribute GetPrimaryKeyAttribute(this PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                return null;
            }
            object objAttribute = propertyInfo.GetCustomAttributes(typeof(PrimaryKeyAttribute), false).FirstOrDefault();
            if (objAttribute != null)
            {
                PrimaryKeyAttribute objPrimaryKeyAttribute = (PrimaryKeyAttribute)objAttribute;
                objPrimaryKeyAttribute.PrimaryProperty = propertyInfo;
                return objPrimaryKeyAttribute;
            }
            return null;
        }

        /// <summary>
        /// 获取主键特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static PrimaryKeyAttribute GetPrimaryKeyAttribute<T>() where T : class, new()
        {
            foreach (PropertyInfo objPropertyInfo in typeof(T).GetProperties())
            {
                PrimaryKeyAttribute objPrimaryKeyAttribute = objPropertyInfo.GetPrimaryKeyAttribute();
                if (objPrimaryKeyAttribute != null)
                {
                    return objPrimaryKeyAttribute;
                }
            }
            return null;

        }
        /// <summary>
        ///获取主键特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static PrimaryKeyAttribute GetPrimaryKeyAttribute<T>(this T value) where T : class, new()
        {
            return GetPrimaryKeyAttribute<T>();
        }

        /// <summary>
        /// 获取主键值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static object GetPrimaryKeyValue<T>(this T value) where T : class, new()
        {
            PrimaryKeyAttribute objPrimaryKeyAttribute = value.GetPrimaryKeyAttribute();
            if (objPrimaryKeyAttribute == null)
            {
                throw new Exception(value.GetType().FullName + "未设置主键特性");
            }
            return objPrimaryKeyAttribute.PrimaryProperty.GetValue(value, null);
        }

        /// <summary>
        /// 获取主键值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Key"></typeparam>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static Key GetPrimaryKeyValue<T, Key>(this T value) where T : class, new()
        {
            object keyValue = value.GetPrimaryKeyValue();
            if (keyValue == null)
            {
                throw new Exception(value.GetType().FullName + "未设置主键特性");
            }
            return (Key)keyValue;
        }

        /// <summary>
        /// 尝试获取主键值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Key"></typeparam>
        /// <param name="value">值</param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public static bool TryPrimaryKeyValue<T, Key>(this T value, out Key keyValue) where T : class, new()
        {

            keyValue = default(Key);
            object keyObject = value.GetPrimaryKeyValue();
            if (keyObject == null)
            {
                return false;
            }
            keyValue = (Key)keyObject;
            return true;
        }

    }
}
