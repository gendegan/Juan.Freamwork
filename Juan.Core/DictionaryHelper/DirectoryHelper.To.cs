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
    public static partial class DictionaryHelper
    {

        /// <summary>
        /// 转成排序字典
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SortedDictionary<TKey, TValue> ToSorted<TKey, TValue>(this IDictionary<TKey, TValue> value)
        {
            SortedDictionary<TKey, TValue> dicArray = new SortedDictionary<TKey, TValue>();
            foreach (KeyValuePair<TKey, TValue> item in value)
            {
                dicArray.Add(item.Key, item.Value);
            }
            return dicArray;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static IDictionary<string, TValue> ToFilterKey<TValue>(this IDictionary<string, TValue> value, params string[] keys)
        {

            IDictionary<string, TValue> dicArray = new Dictionary<string, TValue>();
            if (value is SortedDictionary<string, TValue>)
            {
                dicArray = new SortedDictionary<string, TValue>();
            }
            foreach (KeyValuePair<string, TValue> item in value)
            {
                if (!keys.Contains(item.Key, StringComparer.OrdinalIgnoreCase))
                {
                    dicArray.Add(item.Key, item.Value);
                }
            }
            return dicArray;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static void FilterKey<TValue>(this IDictionary<string, TValue> value, params string[] keys)
        {

            foreach (string key in keys)
            {
                value.Remove(key);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToConcatLink<TKey, TValue>(this IDictionary<TKey, TValue> value)
        {
            return value.ToConcatLink(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <param name="isRemoveNullOrEmpty"></param>
        /// <returns></returns>
        public static string ToConcatLink<TKey, TValue>(this IDictionary<TKey, TValue> value, bool isRemoveNullOrEmpty)
        {
            StringBuilder objStringBuilder = new StringBuilder(256);
            foreach (KeyValuePair<TKey, TValue> item in value)
            {

                if (isRemoveNullOrEmpty)
                {

                    if (item.Value != null && item.Value.ToString().IsNoNullOrWhiteSpace())
                    {
                        objStringBuilder.Append(item.Key + "=" + item.Value.ToString() + "&");
                    }
                }
                else
                {

                    objStringBuilder.Append(item.Key + "=" + (item.Value == null ? "" : item.Value.ToString()) + "&");
                }
            }
            return objStringBuilder.ToString().TrimEnd('&');
        }
        /// <summary>
        /// 拼接链接串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToConcatLinkUrlEncode<TKey, TValue>(this IDictionary<TKey, TValue> value)
        {
            return value.ToConcatLinkUrlEncode(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <param name="isRemoveNullOrEmpty"></param>
        /// <returns></returns>
        public static string ToConcatLinkUrlEncode<TKey, TValue>(this IDictionary<TKey, TValue> value, bool isRemoveNullOrEmpty)
        {
            return value.ToConcatLinkUrlEncode(Encoding.UTF8, isRemoveNullOrEmpty);
        }
        /// <summary>
        /// 拼接链接串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="objEncoding"></param>
        /// <returns></returns>
        public static string ToConcatLinkUrlEncode<TKey, TValue>(this IDictionary<TKey, TValue> value, Encoding objEncoding)
        {
            return value.ToConcatLinkUrlEncode(objEncoding, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <param name="objEncoding"></param>
        /// <param name="isRemoveNullOrEmpty"></param>
        /// <returns></returns>
        public static string ToConcatLinkUrlEncode<TKey, TValue>(this IDictionary<TKey, TValue> value, Encoding objEncoding, bool isRemoveNullOrEmpty)
        {
            StringBuilder objStringBuilder = new StringBuilder(256);
            foreach (KeyValuePair<TKey, TValue> item in value)
            {
                if (isRemoveNullOrEmpty)
                {
                    if (item.Value != null && item.Value.ToString().IsNoNullOrWhiteSpace())
                    {
                        objStringBuilder.Append(item.Key + "=" + HttpUtility.UrlEncode(item.Value.ToString(), objEncoding) + "&");
                    }
                }
                else
                {
                    objStringBuilder.Append(item.Key + "=" + HttpUtility.UrlEncode((item.Value == null ? "" : item.Value.ToString()), objEncoding) + "&");

                }
            }

            return objStringBuilder.ToString().TrimEnd('&');
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>

        public static IDictionary<string, string> ToValuesUrlDecode(this IDictionary<string, string> value)
        {
            return value.ToValuesUrlDecode(Encoding.UTF8);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="objEncoding"></param>
        /// <returns></returns>
        public static IDictionary<string, string> ToValuesUrlDecode(this IDictionary<string, string> value, Encoding objEncoding)
        {

            IDictionary<string, string> dicArray = new Dictionary<string, string>();
            if (value is SortedDictionary<string, string>)
            {
                dicArray = new SortedDictionary<string, string>();
            }
            foreach (KeyValuePair<string, string> item in value)
            {
                dicArray.Add(item.Key, HttpUtility.UrlDecode((item.Value == null ? "" : item.Value.ToString()), objEncoding));
            }
            return dicArray;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="objEncoding"></param>
        /// <returns></returns>
        public static IDictionary<string, string> ToValuesUrlEncode(this IDictionary<string, string> value, Encoding objEncoding)
        {
            IDictionary<string, string> dicArray = new Dictionary<string, string>();
            if (value is SortedDictionary<string, string>)
            {
                dicArray = new SortedDictionary<string, string>();
            }
            foreach (KeyValuePair<string, string> item in value)
            {
                dicArray.Add(item.Key, HttpUtility.UrlEncode((item.Value == null ? "" : item.Value.ToString()), objEncoding));
            }
            return dicArray;
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IDictionary<string, string> ToValuesUrlEncode(this IDictionary<string, string> value)
        {
            return value.ToValuesUrlEncode(Encoding.UTF8);
        }


    }
}
