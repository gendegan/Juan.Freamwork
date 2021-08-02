using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace Juan.Core
{
    public static partial class ReflectorHelper
    {


        private static MemberAdapter GetMemberAdapter(object instance, string propertyName)
        {
            if (instance != null && !string.IsNullOrEmpty(propertyName))
            {
                Type type = instance.GetType();
                PropertyInfo pi = type.GetProperty(propertyName);
                if (pi != null)
                    return new MemberAdapter(instance, pi);
                else
                {
                    FieldInfo fi = type.GetField(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (fi != null)
                        return new MemberAdapter(instance, fi);
                }
            }
            return MemberAdapter.Empty;
        }
        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="instance">值对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        public static object GetPropertyValue(object instance, string propertyName)
        {
            return GetMemberAdapter(instance, propertyName).Value;
        }
        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="instance">值对象</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="value">值</param>
        public static void SetPropertyValue(this object instance, string propertyName, object value)
        {

            MemberAdapter ma = GetMemberAdapter(instance, propertyName);
            ma.Value = value;
        }
        /// <summary>
        /// 查询属性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static PropertyInfo[] FindPropertys(this Type type)
        {
            return type.GetProperties();
        }
        /// <summary>
        /// 查询属性
        /// </summary>
        /// <param name="type">类</param>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public static PropertyInfo[] FindPropertys(this Type type, BindingFlags bindingAttr)
        {
            return type.GetProperties(bindingAttr);

        }
        /// <summary>
        /// 查询属性
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="typeName">完整类名称</param>
        /// <param name="bindingAttr"></param>
        /// <returns></returns>
        public static PropertyInfo[] FindPropertys(string assemblyName, string typeName, BindingFlags bindingAttr)
        {
            return FindPropertys(FindType(assemblyName, typeName), bindingAttr);
        }

        /// <summary>
        /// 查询属性
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="typeName">完整类名称</param>
        /// <returns></returns>
        public static PropertyInfo[] FindPropertys(string assemblyName, string typeName)
        {

            return FindPropertys(FindType(assemblyName, typeName));
        }

      
    }
}
