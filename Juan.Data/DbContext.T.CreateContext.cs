using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Data
{
    public abstract partial class DbContext<T, Key> : IDbContext<T, Key> where T : class, new()
    {

        /// <summary>
        ///  创建上下文
        /// </summary>
        /// <param name="contextKey">上下文键值</param>
        /// <param name="partName">分表名称</param>
        /// <param name="connectionType">连接类型</param>
        /// <returns></returns>
        public static DbContext<T, Key> CreateContext(string contextKey, string partName, ConnectionType connectionType = ConnectionType.Auto)
        {

            contextKey.ArgumentNoNull("contextKey", "contextKey上下文配置值不能空");
            contextKey = contextKey.Replace(".ConnectionString", "").Replace(".ConnectionString.Read", "").Replace("ConnectionString", "");
            ContextType objContextType = ContextHelper.ContextType(contextKey);
            if (objContextType == ContextType.MySql)
            {
                return new MySqlContext<T, Key>(contextKey, partName, connectionType);
            }

            return new MySqlContext<T, Key>(contextKey, partName, connectionType);
        }
        /// <summary>
        /// 创建上下文
        /// </summary>
        /// <param name="partName">分表表名</param>
        /// <param name="connectionType">连接类型</param>
        /// <returns></returns>
        public static DbContext<T, Key> CreateContext(string partName, ConnectionType connectionType = ConnectionType.Auto)
        {

            string contextKey = "";
            ContextAttribute objContextAttribute = ContextHelper.GetContextInfo(typeof(T));
            if (objContextAttribute == null || string.IsNullOrWhiteSpace(objContextAttribute.ContextKey))
            {
                contextKey = typeof(T).Assembly.GetName().Name;
            }
            else
            {
                contextKey = objContextAttribute.ContextKey;
            }

            ContextType objContextType = ContextHelper.ContextType(contextKey);
            if (objContextType == ContextType.MySql)
            {
                return new MySqlContext<T, Key>(contextKey, partName, connectionType);
            }

            return new MySqlContext<T, Key>(contextKey, partName, connectionType);
        }





    }
}
