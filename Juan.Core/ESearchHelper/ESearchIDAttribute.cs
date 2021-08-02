using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Juan.Core
{
    /// <summary>
    /// 主键标识
    /// </summary>
    public class ESearchIDAttribute : Attribute
    {
    }
    /// <summary>
    /// 路由
    /// </summary>
    public class ESearchRoutingAttribute : Attribute
    {

    }

    /// <summary>
    /// ESearchKey帮助类
    /// </summary>
    public static class ESearchIDHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static PropertyInfo GetESearchIDProperty<T>() where T : class, new()
        {
            PropertyInfo[] objPropertyInfoList = typeof(T).GetProperties();
            foreach (PropertyInfo objPropertyInfo in objPropertyInfoList)
            {
                if (objPropertyInfo.GetCustomAttributes(typeof(ESearchIDAttribute), false).Any())
                {
                    return objPropertyInfo;
                }
            }
            foreach (PropertyInfo objPropertyInfo in objPropertyInfoList)
            {
                if (objPropertyInfo.GetCustomAttributes(typeof(PrimaryKeyAttribute), false).Any())
                {
                    return objPropertyInfo;
                }
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static PropertyInfo GetESearchRoutingProperty<T>() where T : class, new()
        {
            PropertyInfo[] objPropertyInfoList = typeof(T).GetProperties();
            foreach (PropertyInfo objPropertyInfo in objPropertyInfoList)
            {
                if (objPropertyInfo.GetCustomAttributes(typeof(ESearchRoutingAttribute), false).Any())
                {
                    return objPropertyInfo;
                }
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static object GetESearchIDValue<T>(this T value) where T : class, new()
        {
            PropertyInfo objPropertyInfo = GetESearchIDProperty<T>();
            if (objPropertyInfo == null)
            {
                return null;
            }
            return objPropertyInfo.GetValue(value, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static object GetESearchRoutingValue<T>(this T value) where T : class, new()
        {
            PropertyInfo objPropertyInfo = GetESearchRoutingProperty<T>();
            if (objPropertyInfo == null)
            {
                return null;
            }
            return objPropertyInfo.GetValue(value, null);
        }

    }
}
