using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    public static partial class StringHelper
    {

        /// <summary>
        ///是否不是由 null、空还是仅由空白字符组成
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool IsNoNull(this string value)
        {
            return value.IsNoNullOrWhiteSpace();
        }
        /// <summary>
        /// 是否是由 null、空还是仅由空白字符组成
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool IsNull(this string value)
        {
            return value.IsNullOrWhiteSpace();
        }
        /// <summary>
        /// 判断字符串是否不为NULL或空值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool IsNoNullOrEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }
        /// <summary>
        ///  判断字符串是否NULL或空值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        /// <summary>
        /// 判断字符串是否NULL或空值或空白串
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        /// <summary>
        /// 判断字符串是否不为NULL或空值或空白串
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool IsNoNullOrWhiteSpace(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }



        /// <summary>
        /// 是否是Int32
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool IsInt(this string value)
        {
            value = value.Trim();
            int result = 0;
            return int.TryParse(value, out result);
        }
        /// <summary>
        /// 是否Double
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool IsDouble(this string value)
        {
            value = value.Trim();
            double result = 0;

            return double.TryParse(value, out result);
        }
        /// <summary>
        /// 是否长整型 
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static bool IsInt64(this string value)
        {
            value = value.Trim();
            long result = 0;
            return Int64.TryParse(value, out result);
        }
    }
}
