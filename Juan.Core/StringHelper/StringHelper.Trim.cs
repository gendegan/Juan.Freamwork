using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    public static partial class StringHelper
    {


        /// <summary>
        /// 去除逗号
        /// </summary>
        /// <param name="value">输入值</param>
        /// <returns></returns>
        public static string TrimComma(this string value)
        {
            return value.Trim(',');
        }
        /// <summary>
        ///  去除尾部逗号[,]
        /// </summary>
        /// <param name="value">输入值</param>
        /// <returns></returns>
        public static string TrimEndComma(this string value)
        {
            return value.TrimEnd(',');
        }
        /// <summary>
        /// 去除开始逗号[,]
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string TrimStartComma(this string value)
        {
            return value.TrimStart(',');
        }

        /// <summary>
        /// 去除尾部的指定的字段串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="trim"></param>
        /// <returns></returns>
        public static string TrimEnd(this string value, string trim)
        {
            if (String.IsNullOrEmpty(value) || !value.EndsWith(trim))
            {
                return value;
            }
            return value.Substring(0, value.Length - trim.Length);
        }

        /// <summary>
        /// 去除开始的指定的字段串
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="trim"></param>
        /// <returns></returns>
        public static string TrimStart(this string value, string trim)
        {
            if (String.IsNullOrEmpty(trim) || !value.StartsWith(trim))
            {
                return value;
            }
            return value.Substring(trim.Length);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="trim"></param>
        /// <returns></returns>
        public static string Trim(this string value, string trim)
        {
            value = value.TrimStart(trim);
            value = value.TrimEnd(trim);
            return value;
        }

        /// <summary>
        /// 添加头部和尾部
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="trim"></param>
        /// <returns></returns>
        public static string AddStartEnd(this string value, string trim)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (!value.StartsWith(trim))
                {
                    value = trim + value;
                }
                if (!value.EndsWith(trim))
                {
                    value = value + trim;
                }
            }
            return value;

        }

    }
}
