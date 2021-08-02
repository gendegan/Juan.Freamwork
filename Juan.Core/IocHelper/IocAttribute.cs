using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    /// <summary>
    /// Ioc注入信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class IocAttribute : Attribute
    {
        /// <summary>
        /// 程序集名称
        /// </summary>
        public string AssemblyName
        {
            get;
            set;
        }
        /// <summary>
        /// 完整类型名称
        /// </summary>
        public string TypeName
        {
            get;
            set;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <param name="typeName">完整类型名称</param>
        public IocAttribute(string assemblyName, string typeName)
        {
            assemblyName.ArgumentNoNull("assemblyName");
            assemblyName.ArgumentNoNull("typeName");
            AssemblyName = assemblyName;
            TypeName = typeName;

        }
    }

}
