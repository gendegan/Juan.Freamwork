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
        /// 替换solr关键字
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public static string FilterSolr(this string keyWord)
        {
            if (string.IsNullOrEmpty(keyWord))
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < keyWord.Length; i++)
            {
                char c = keyWord[i];
                if (c == '\\' || c == '+' || c == '-' || c == '!' || c == '(' || c == ')' || c == ':'
                  || c == '^' || c == '[' || c == ']' || c == '\"' || c == '{' || c == '}' || c == '~'
                  || c == '*' || c == '?' || c == '|' || c == '&' || c == ';' || c == '/'
                  || c == ' ')
                {
                    sb.Append('\\');
                }
                sb.Append(c);
            }
            return sb.ToString();
        }

    }
}
