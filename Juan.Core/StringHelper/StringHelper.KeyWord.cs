using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    public static partial class StringHelper
    {
        /// <summary>
        /// 过滤关键字
        /// </summary>
        /// <param name="source">源串</param>
        /// <param name="Keyword">关键字，隔开+代码且</param>
        /// <param name="isRemove">是否删除</param>
        /// <param name="replaceString">潜换的字 符串</param>
        /// <returns></returns>
        public static string FilterKeyword(this string source, string Keyword, bool isRemove, string replaceString = "***")
        {

            if (string.IsNullOrWhiteSpace(Keyword))
            {
                return source;
            }
            if (string.IsNullOrWhiteSpace(source))
            {
                return source;
            }
            foreach (string key in Keyword.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] keys = key.Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);

                if (keys.Length == 1)
                {
                    source = source.Replace(keys[0], isRemove ? "" : replaceString);
                }
                else
                {
                    bool isReplace = true;
                    foreach (string andKey in keys)
                    {
                        if (!source.Contains(andKey))
                        {
                            isReplace = false;
                        }
                        if (!isReplace)
                        {
                            break;
                        }
                    }
                    if (isReplace)
                    {
                        foreach (string andKey in keys)
                        {
                            source = source.Replace(andKey, isRemove ? "" : replaceString);
                        }
                    }
                }

            }
            return source;

        }

        /// <summary>
        /// 判断是否包含关键字
        /// </summary>
        /// <param name="source"></param>
        /// <param name="Keyword"></param>
        /// <returns></returns>
        public static bool IsContainsKeyword(this string source, string Keyword)
        {

            bool IsContains = false;
            if (string.IsNullOrWhiteSpace(Keyword))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(source))
            {
                return false;
            }
            source = source.ToLower();
            foreach (string key in Keyword.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] keys = key.ToLower().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);

                if (keys.Length == 1)
                {
                    if (source.Contains(keys[0]))
                    {
                        IsContains = true;
                    }

                }
                else
                {
                    bool isReplace = true;
                    foreach (string andKey in keys)
                    {
                        if (!source.Contains(andKey))
                        {
                            isReplace = false;
                        }
                        if (!isReplace)
                        {
                            break;
                        }
                    }
                    if (isReplace)
                    {
                        IsContains = true;
                    }
                }
                if (IsContains)
                {
                    break;
                }

            }
            return IsContains;

        }


        /// <summary>
        /// 是否包括关键字
        /// </summary>
        /// <param name="source"></param>
        /// <param name="Keyword">关键字</param>
        /// <param name="ContainsKey">包含的字多个用|隔开</param>
        /// <returns></returns>
        public static bool IsContainsKeyword(this string source, string Keyword, out string ContainsKey)
        {
            ContainsKey = "";
            bool IsContains = false;
            if (string.IsNullOrWhiteSpace(Keyword))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(source))
            {
                return false;
            }
            source = source.ToLower();
            foreach (string key in Keyword.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] keys = key.ToLower().Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);

                if (keys.Length == 1)
                {
                    if (source.Contains(keys[0]))
                    {
                        IsContains = true;
                        ContainsKey += key + "|";
                    }

                }
                else
                {
                    bool isReplace = true;
                    foreach (string andKey in keys)
                    {
                        if (!source.Contains(andKey))
                        {
                            isReplace = false;
                        }
                        if (!isReplace)
                        {
                            break;
                        }
                    }
                    if (isReplace)
                    {
                        IsContains = true;
                        ContainsKey += key + "|";
                    }
                }

            }
            ContainsKey = ContainsKey.Trim('|');
            return IsContains;

        }
    }
}
