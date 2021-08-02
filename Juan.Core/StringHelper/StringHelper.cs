using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Juan.Core
{
    public static partial class StringHelper
    {
        /// <summary>
        /// 获取字符串的字节长度
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static int LengthByte(this string value)
        {
            return System.Text.Encoding.Default.GetByteCount(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="toCheck"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }


    }
}
