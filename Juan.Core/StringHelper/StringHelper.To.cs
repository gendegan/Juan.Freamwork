using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    public static partial class StringHelper
    {

        /// <summary>
        ///   格式化字符串
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FormatValue(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="split"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string FormatConcat(this string split, params object[] values)
        {

            if (values == null || values.Length == 0)
            {
                return "";
            }
            StringBuilder objStringBuilder = new StringBuilder(256);
            foreach (object value in values)
            {
                objStringBuilder.Append(value.ToString() + split);

            }
            return objStringBuilder.ToString().Trim(split);
        }
        /// <summary>
        /// 字符串间隔转列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">值</param>
        /// <param name="isRemoveEmpty">是否移除空格</param>
        /// <param name="split">间隔符号</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this string value, bool isRemoveEmpty = true, char split = ',')
        {

            string result = string.Empty;
            List<T> objValueList = new List<T>();
            if (value.IsNullOrWhiteSpace())
            {
                return objValueList;
            }
            string[] valueArrary;
            if (isRemoveEmpty)
            {
                valueArrary = value.Split(new char[] { split }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                valueArrary = value.Split(split);
            }
            foreach (string item in valueArrary)
            {
                objValueList.Add(item.To<T>());
            }
            return objValueList;
        }
        /// <summary>
        /// 字符串间隔转列表
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="isRemoveEmpty">是否移除空格</param>
        /// <param name="split">间隔符号</param>
        /// <returns></returns>
        public static List<int> ToListInt(this string value, bool isRemoveEmpty = true, char split = ',')
        {

            return value.ToList<int>(isRemoveEmpty, split);
        }
        /// <summary>
        /// 字符串间隔转列表
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="isRemoveEmpty">是否移除空格</param>
        /// <param name="split">间隔符号</param>
        /// <returns></returns>
        public static List<Int64> ToListInt64(this string value, bool isRemoveEmpty = true, char split = ',')
        {

            return value.ToList<Int64>(isRemoveEmpty, split);
        }
        /// <summary>
        /// 字符串间隔转列表
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="isRemoveEmpty">是否移除空格</param>
        /// <param name="split">间隔符号</param>
        /// <returns></returns>
        public static List<string> ToListString(this string value, bool isRemoveEmpty = true, char split = ',')
        {

            return value.ToList<string>(isRemoveEmpty, split);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isRemoveEmpty"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static List<Guid> ToListGuid(this string value, bool isRemoveEmpty = true, char split = ',')
        {

            return value.ToList<Guid>(isRemoveEmpty, split);
        }
        /// <summary>
        /// 字符串相隔转数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">值</param>
        /// <param name="isRemoveEmpty">是否移除空格</param>
        /// <param name="split">间隔符号</param>
        /// <returns></returns>
        public static T[] ToArray<T>(this string value, bool isRemoveEmpty = true, char split = ',')
        {
            return value.ToList<T>(isRemoveEmpty, split).ToArray();
        }

        /// <summary>
        /// 字符串相隔转数组
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="isRemoveEmpty">是否移除空格</param>
        /// <param name="split">间隔符号</param>
        /// <returns></returns>
        public static int[] ToArrayInt(this string value, bool isRemoveEmpty = true, char split = ',')
        {
            return value.ToList<int>(isRemoveEmpty, split).ToArray();
        }
        /// <summary>
        /// 字符串相隔转数组
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="isRemoveEmpty">是否移除空格</param>
        /// <param name="split">间隔符号</param>
        /// <returns></returns>
        public static Int64[] ToArrayInt64(this string value, bool isRemoveEmpty = true, char split = ',')
        {
            return value.ToList<Int64>(isRemoveEmpty, split).ToArray();
        }


        /// <summary>
        /// 字符串相隔转数组
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="isRemoveEmpty">是否移除空格</param>
        /// <param name="split">间隔符号</param>
        /// <returns></returns>
        public static string[] ToArrayString(this string value, bool isRemoveEmpty = true, char split = ',')
        {
            return value.ToList<string>(isRemoveEmpty, split).ToArray();
        }

        /// <summary>
        /// Json转DataTable
        /// </summary>
        /// <param name="jsonValue"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(this string jsonValue)
        {
            DataTable dataTable = new DataTable();  //实例化
            if (string.IsNullOrWhiteSpace(jsonValue))
            {
                return dataTable;
            }
            List<Dictionary<string, object>> objValueList = jsonValue.JsonDeserialize<List<Dictionary<string, object>>>();
            return objValueList.ToDataTable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToText(this string value)
        {

            return value == null ? "" : value.ToString();

        }
        /// <summary>
        /// 字符串转字节
        /// </summary>
        /// <param name="value"></param>
        /// <param name="objEncoding"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string value, Encoding objEncoding)
        {
            return objEncoding.GetBytes(value);
        }
        /// <summary>
        /// 字符串转字节
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ToBytesUTF8(this string value)
        {
            return value.ToBytes(System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// 字符串转字节
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ToBytesDefault(this string value)
        {
            return value.ToBytes(System.Text.Encoding.Default);
        }

        /// <summary>
        /// 半角转全角
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String ToSBC(this String input)
        {
            // 半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new String(c);
        }
        /// <summary>
        /// 全角转半角
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String ToDBC(this String input)
        {
            // 全角转半角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new String(c);
        }
        /// <summary>
        /// 判断是否全角
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsDBC(this String input)
        {

            // 全角转半角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {

                    return true;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToCamelCase(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            if (!char.IsUpper(s[0]))
                return s;

            StringBuilder sb = new StringBuilder(256);
            for (int i = 0; i < s.Length; i++)
            {
                bool hasNext = (i + 1 < s.Length);
                if ((i == 0 || !hasNext) || char.IsUpper(s[i + 1]))
                {
                    char lowerCase;

                    lowerCase = char.ToLower(s[i], CultureInfo.InvariantCulture);


                    sb.Append(lowerCase);
                }
                else
                {
                    sb.Append(s.Substring(i));
                    break;
                }
            }

            return sb.ToString();
        }
    }
}
