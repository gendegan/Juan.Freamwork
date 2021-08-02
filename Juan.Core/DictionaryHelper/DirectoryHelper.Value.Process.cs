using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Juan.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class SortedDictionaryHelper
    {






        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>

        public static void ValuesUrlDecode(this   IDictionary<string, string> value)
        {
            value.ValuesUrlDecode(Encoding.UTF8);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="objEncoding"></param>
        /// <returns></returns>
        public static void ValuesUrlDecode(this   IDictionary<string, string> value, Encoding objEncoding)
        {

            List<string> keys = new List<string>();
            foreach (string key in value.Keys)
            {
                keys.Add(key);

            }
            foreach (string key in keys)
            {
                value[key] = HttpUtility.UrlDecode(value[key], objEncoding);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="objEncoding"></param>
        /// <returns></returns>
        public static void ValuesUrlEncode(this   IDictionary<string, string> value, Encoding objEncoding)
        {

            List<string> keys = new List<string>();
            foreach (string key in value.Keys)
            {
                keys.Add(key);

            }
            foreach (string key in keys)
            {
                value[key] = HttpUtility.UrlEncode(value[key], objEncoding);
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void ValuesUrlEncode(this IDictionary<string, string> value)
        {
            value.ValuesUrlEncode(Encoding.UTF8);
        }


    }
}
