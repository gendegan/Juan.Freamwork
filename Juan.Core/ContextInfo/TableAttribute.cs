using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{

    /// <summary>
    /// 表结构信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TableAttribute : Attribute
    {
        string _TableName = "";

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_TableName))
                    throw new ArgumentNullException("TableName", "表名不能为空");
                else
                    return _TableName;
            }
            set
            {
                _TableName = value;
            }
        }
        string _ViewName = "";

        /// <summary>
        /// 视图名
        /// </summary>
        public string ViewName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_ViewName))
                    throw new ArgumentNullException("ViewName", "视图名不能为空");
                else
                    return _ViewName;
            }
            set
            {
                _ViewName = value;
            }
        }

        string _ViewFormat = "";
        /// <summary>
        /// 视图分表格式化sys_Juan_{0}_vw
        /// </summary>
        public string ViewFormat
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_ViewFormat))
                    throw new ArgumentNullException("ViewFormat", "视图分表格式化不能为空");
                else
                    return _ViewFormat;
            }
            set
            {
                _ViewFormat = value;
            }
        }
        string _TableFormat = "";
        /// <summary>
        /// 分表格式化sys_Juan_{0}_tb
        /// </summary>
        public string TableFormat
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_TableFormat))
                    throw new Exception("TableFormat分表格式化不能为空");
                else
                    return _TableFormat;
            }
            set
            {
                _TableFormat = value;
            }
        }
        /// <summary>
        /// 版本号
        /// </summary>
        public string VerNo
        {
            get;
            set;
        }




    }

    /// <summary>
    /// 实体表信息
    /// </summary>
    public static class TableHelper
    {
        /// <summary>
        /// 获取表结构信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static TableAttribute GetTableAttribute<T>() where T : class,new()
        {
            object[] attributes = typeof(T).GetCustomAttributes(typeof(TableAttribute), true);
            if (attributes != null && attributes.Length > 0)
            {
                try
                {
                    return attributes[0] as TableAttribute;
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
