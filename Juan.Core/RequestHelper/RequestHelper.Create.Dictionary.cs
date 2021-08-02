using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text.RegularExpressions;


namespace Juan.Core
{
    /// <summary>
    /// Request包装类。
    /// </summary>
    public static partial class RequestHelper
    {

        #region 使用指定的URL参数建立一个链接


        /// <summary>
        /// 创建请求链接
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="Headers"></param>
        /// <param name="Method"></param>
        /// <returns></returns>
        public static HttpWebRequest CreateRequest<TValue>(this  string url, IDictionary<string, TValue> Headers, string Method = "Get")
        {
            return url.CreateRequest(null, Headers, Method);
        }
        /// <summary>
        ///  创建请求链接
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="Accept"></param>
        /// <param name="Headers"></param>
        /// <param name="Method"></param>
        /// <returns></returns>
        public static HttpWebRequest CreateRequest<TValue>(this  string url, string Accept, IDictionary<string, TValue> Headers, string Method = "Get")
        {
            return url.CreateRequest(Accept, Headers, _UserAgent, null, _Timeout, Method);
        }

        /// <summary>
        /// 创建请求链接
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="Accept"></param>
        /// <param name="Headers"></param>
        /// <param name="userAgent"></param>
        /// <param name="referer"></param>
        /// <param name="timeout"></param>
        /// <param name="Method"></param>
        /// <returns></returns>
        public static HttpWebRequest CreateRequest<TValue>(this  string url, string Accept, IDictionary<string, TValue> Headers, string userAgent, string referer, int timeout, string Method = "Get")
        {


            WebRequest req = WebRequest.Create(url);
            HttpWebRequest wreq = req as HttpWebRequest;
            if (wreq != null)
            {
                if (!string.IsNullOrWhiteSpace(userAgent))
                {
                    wreq.UserAgent = userAgent;
                }
                else
                {

                    wreq.UserAgent = _UserAgent;
                }
                if (!string.IsNullOrWhiteSpace(referer))
                {
                    wreq.Referer = referer;
                }
                else
                {
                    referer = url.Replace("^http://", "", RegexOptions.IgnoreCase);
                    if (referer.IndexOf('/') > 0)
                    {
                        referer = referer.Substring(0, referer.IndexOf('/') + 1);
                    }
                    wreq.Referer = "http://" + referer;
                }
                if (!string.IsNullOrWhiteSpace(Method))
                {
                    wreq.Method = Method;
                }

                if (!string.IsNullOrWhiteSpace(Accept))
                {
                    wreq.Accept = Accept;
                }
                if (timeout > 0)
                {
                    wreq.Timeout = timeout;
                }
                if (Headers != null)
                {
                    foreach (var item in Headers)
                    {
                        wreq.Headers.Add(item.Key, item.Value.ToString());

                    }
                }
            }
            return wreq;
        }
        #endregion


    }
}
