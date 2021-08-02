using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Juan.Core
{
    public static partial class StringHelper
    {

        /// <summary>
        /// 处理是过滤标签
        /// </summary>
        /// <param name="inputValue"></param>
        /// <param name="isTrimSplit">是否清理前后间隔符</param>
        /// <param name="splitChar">间隔符</param>
        /// <returns></returns>
        public static string TagProcess(this string inputValue, bool isTrimSplit = true, char splitChar = ',')
        {

            if (string.IsNullOrWhiteSpace(inputValue))
            {
                return "";
            }

            inputValue = inputValue.Replace('，', splitChar);
            inputValue = inputValue.Replace(":", "");
            inputValue = inputValue.Replace(";", "");
            inputValue = inputValue.Replace("；", "");
            string TagValue = "";

            foreach (string tag in inputValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                TagValue += tag.Trim() + splitChar.ToString();
            }
            if (isTrimSplit)
            {
                return TagValue.Trim(splitChar);
            }
            else
            {
                return string.IsNullOrWhiteSpace(TagValue) ? "" : splitChar.ToString() + TagValue;
            }
        }
        /// <summary>
        /// 处理是过滤标签
        /// </summary>
        /// <param name="inputValue"></param>
        /// <param name="isTrimSplit">是否清理前后间隔符</param>
        /// <param name="splitChar">间隔符</param>
        /// <returns></returns>
        public static string TagProcess(this IEnumerable inputValue, bool isTrimSplit = true, char splitChar = ',')
        {

            if (inputValue.IsNull())
            {
                return "";
            }

            string TagValue = "";
            IEnumerator enumerator = inputValue.GetEnumerator();

            while (enumerator.MoveNext())
            {
                TagValue += enumerator.Current.ToString() + splitChar.ToString();
            }
            if (isTrimSplit)
            {
                return TagValue.Trim(splitChar);
            }
            else
            {
                return string.IsNullOrWhiteSpace(TagValue) ? "" : splitChar.ToString() + TagValue;
            }
        }
    }
}
