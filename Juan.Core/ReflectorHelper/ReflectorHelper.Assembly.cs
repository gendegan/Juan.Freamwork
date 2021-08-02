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
        /// 
        /// </summary>
        private readonly static ConcurrentDictionary<string, Assembly> _AssemblyCacheList = new ConcurrentDictionary<string, Assembly>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns></returns>
        public static Assembly FindAssembly(this string assemblyName)
        {
            return _AssemblyCacheList.GetOrAdd(assemblyName, assembly => { return Assembly.Load(assembly); });
        }
    }
}
