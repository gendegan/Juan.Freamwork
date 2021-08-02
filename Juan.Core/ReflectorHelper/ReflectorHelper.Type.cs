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
        /// 查询类型
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="typeName">完整类名称</param>
        /// <returns></returns>
        public static Type FindType(this string assemblyName, string typeName)
        {

            Assembly objAssembly = FindAssembly(assemblyName);
            return objAssembly.FindType(typeName);
        }
        /// <summary>
        /// 查询类型
        /// </summary>
        /// <param name="assembly">程序集</param>
        /// <param name="typeName">完整类名称</param>
        /// <returns></returns>
        public static Type FindType(this Assembly assembly, string typeName)
        {

            Type objTypeType = assembly.GetType(typeName);

            if (objTypeType == null)
            {
                throw new ArgumentNullException("程序集" + assembly.FullName + "找不到类型" + typeName);
            }
            return objTypeType;

        }
    }


}
