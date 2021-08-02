using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 数据库操作忽略
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DbIgnoreAttribute : Attribute
    {

        IgnoreType _IgnoreType = IgnoreType.All;
        /// <summary>
        /// 忽略操作
        /// </summary>
        public IgnoreType Ignore
        {
            get
            {
                return _IgnoreType;
            }
            set
            {
                _IgnoreType = value;
            }
        }

    }
    /// <summary>
    /// 忽略类型
    /// </summary>
    public enum IgnoreType
    {
        /// <summary>
        /// 全部
        /// </summary>
        All,
        /// <summary>
        /// 新增
        /// </summary>
        Add,
        /// <summary>
        /// 更新
        /// </summary>
        Update
    }

}
