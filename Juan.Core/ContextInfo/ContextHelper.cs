using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 上下文配置类
    /// </summary>
    public class ContextHelper
    {
        /// <summary>
        /// 数据库类型
        /// </summary>
        /// <returns></returns>
        public static ContextSection GetContextSection()
        {
            return (ContextSection)ConfigHelper.GetSection("ContextConfig", "ContextConfig");
        }


        /// <summary>
        /// 获取数据库类型
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ContextType ContextType(string name)
        {

            name.ArgumentNoNull("name", "name不能为空");
            ContextSection objContextConnectionSection = GetContextSection();
            ContextElement objConnectionStringElement = objContextConnectionSection.ConnectionStrings[name];
            if (objConnectionStringElement == null)
            {
                throw new ConfigurationErrorsException("未设置连接串名:" + name);
            }

            return objConnectionStringElement.ContextType;


        }

        /// <summary>
        ///获取写连接串
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ConnectionWriteString(string name)
        {

            ContextSection objContextSection = GetContextSection();
            ContextElement objContextElement = objContextSection.ConnectionStrings[name];
            if (objContextElement == null)
            {
                throw new ConfigurationErrorsException("未设置连接串名:" + name);
            }
            return objContextElement.ConnectionString;
        }
        /// <summary>
        /// 获取读连接串
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ConnectionReadString(string name)
        {

            ContextSection objContextSection = GetContextSection();
            ContextElement objContextElement = objContextSection.ConnectionStrings[name];
            if (objContextElement == null)
            {
                throw new ConfigurationErrorsException("未设置连接串名:" + name);
            }
            if (objContextElement.Reads.Count == 0)
            {
                return objContextElement.ConnectionString;
            }
            else if (objContextElement.Reads.Count == 1)
            {
                return objContextElement.Reads[0].ConnectionString;
            }
            else
            {
                int readIndex = RandomHelper.GetRandomNumber(0, objContextElement.Reads.Count);
                return objContextElement.Reads[readIndex].ConnectionString;
            }
        }
        /// <summary>
        /// 获取上下文信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ContextAttribute GetContextInfo(Type type)
        {
            object[] attributes = type.GetCustomAttributes(typeof(ContextAttribute), true);
            if (attributes != null && attributes.Length > 0)
            {
                try
                {
                    return attributes[0] as ContextAttribute;
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
