using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Juan.Core
{
    public static partial class ReflectorHelper
    {
        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="typeName">类完整名称</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static object CreateInstace(this string assemblyName, string typeName, params object[] args)
        {

            return Activator.CreateInstance(FindType(assemblyName, typeName), args);
        }
        /// <summary>
        /// 创建实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="typeName">类完整名称</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static T CreateInstace<T>(this string assemblyName, string typeName, params object[] args)
        {
            return (T)CreateInstace(assemblyName, typeName, args);
        }
    }
}
