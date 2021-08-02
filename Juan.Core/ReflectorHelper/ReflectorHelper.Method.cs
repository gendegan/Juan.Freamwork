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

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="type">类</param>
        /// <param name="methodname">方法名称</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static object ExecuteMethod(string assemblyName, string type, string methodname, object[] args)
        {

            object obj = CreateInstace(assemblyName, type);
            MethodInfo mi = obj.GetType().GetMethod(methodname);
            return mi.Invoke(obj, args);
        }


    }
}
