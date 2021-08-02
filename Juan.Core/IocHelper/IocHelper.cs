using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Juan.Core
{
    /// <summary>
    /// Ioc帮助类
    /// </summary>
    public class IocHelper
    {


        private readonly static ConcurrentDictionary<string, object> _IocList = new ConcurrentDictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        private static ReaderWriterLockSlimHelper objIocWriterLockSlimHelper = new ReaderWriterLockSlimHelper();

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isSingle">是否是单实例</param>
        /// <param name="assemblyName">程序集名称[Juan.Log]</param>
        /// <param name="typeName">完整的类名称[Juan.Log.Logger]</param>
        /// <returns></returns>
        public static T Instance<T>(bool isSingle = false, string assemblyName = "", string typeName = "")
        {
            object value;
            if (string.IsNullOrWhiteSpace(assemblyName) && string.IsNullOrWhiteSpace(typeName))
            {
                IocAttribute objIocAttribute = GetIocAttribute<T>();
                objIocAttribute.ArgumentNoNull("IocAttribute", typeof(T).ToString() + "未设置Ioc特性");
                assemblyName = objIocAttribute.AssemblyName;
                typeName = objIocAttribute.TypeName;

            }

            if (isSingle)
            {
                if (_IocList.TryGetValue(typeName, out value))
                {
                    return (T)value;
                }

                string lockKey = typeName;
                return objIocWriterLockSlimHelper.AtomWriteLock(lockKey, () =>
                 {
                     if (_IocList.TryGetValue(typeName, out value))
                     {
                         return (T)value;
                     }
                     value = ReflectorHelper.CreateInstace(assemblyName, typeName);
                     _IocList.TryAdd(typeName, value);
                     return (T)value;
                 });
            }
            else
            {
                return ReflectorHelper.CreateInstace<T>(assemblyName, typeName);
            }
        }


        private static object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }
        /// <summary>
        /// 获取类Ioc特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static IocAttribute GetIocAttribute<T>()
        {
            object[] attributes = typeof(T).GetCustomAttributes(typeof(IocAttribute), true);
            if (attributes != null && attributes.Length > 0)
            {
                try
                {
                    return attributes[0] as IocAttribute;
                }
                catch
                {
                    return null;

                }
            }
            else
            {
                return null;
            }
        }


    }
}
