using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Juan.Core
{
    public static partial class StringHelper
    {


        /// <summary>
        /// 从此实例检索子字符串。子字符串从指定的字符位置开始且具有指定的长度
        /// </summary>
        /// <param name="src"></param>
        /// <param name="startIndex"></param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string SubText(this string src, int startIndex, int length)
        {

            if (startIndex < 0 || startIndex >= src.Length)
                return "";
            else if (startIndex + length >= src.Length)
                return src.Substring(startIndex);
            else
                return src.Substring(startIndex, length);
        }
        /// <summary>
        /// 从此实例检索子字符串。子字符串从指定的字符位置开始且具有指定的长度
        /// </summary>
        /// <param name="src"></param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static string SubText(this string src, int length)
        {

            return src.SubText(0, length);
        }

  
        /// <summary>
        /// 按字节截取
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="length">长度</param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public static string ToCutByte(this string value, int length, string tail = "")
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            if (Encoding.Default.GetByteCount(value) > length)
            {
                if (value == null)
                {
                    return "";
                }
                int i = 0;
                int j = 0;
                foreach (char Char in value)
                {
                    if (Char > '\x007f')
                    {
                        i += 2;
                    }
                    else
                    {
                        i++;
                    }
                    if (i > length)
                    {
                        if (!string.IsNullOrWhiteSpace(tail))
                        {
                            value = value.Substring(0, j - tail.Length) + tail;
                            return value;
                        }
                        value = value.Substring(0, j);
                        return value;
                    }
                    j++;
                }
            }
            return value;
        }

        /// <summary>
        /// 按字截取
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="length">长度</param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public static string ToCutWord(this string value, int length, string tail = "")
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            if (value.Length > length)
            {
                if (!string.IsNullOrWhiteSpace(tail))
                {
                    value = value.Substring(0, length - tail.Length) + tail;
                }
                else
                {
                    value = value.Substring(0, length);
                }

            }
            return value;
        }


      




    }

}
