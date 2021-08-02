using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 上下文信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ContextAttribute : Attribute
    {
        /// <summary>
        /// 上下文键值
        /// </summary>
        public string ContextKey
        {
            get;
            set;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public ContextAttribute()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="contextKey">上下文键值</param>
        public ContextAttribute(string contextKey)
        {
            ContextKey = contextKey;
        }
    }
}
