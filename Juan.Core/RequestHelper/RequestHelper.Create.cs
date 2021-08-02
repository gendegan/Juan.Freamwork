using System;
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
        private const int _Timeout = 60000 * 5;
        /// <summary>
        /// 用户代理
        /// </summary>
        private const string _UserAgent = @"Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/34.0.1847.116 Safari/537.36";


        #region 使用指定的URL参数建立一个链接
        /// <summary>
        ///  创建请求链接
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public static HttpWebRequest CreateRequest(this string url)
        {
            return url.CreateRequest(null);
        }

        /// <summary>
        /// 创建请求链接
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="Headers"></param>
        /// <param name="Method"></param>
        /// <returns></returns>
        public static HttpWebRequest CreateRequest(this string url, NameValueCollection Headers, string Method = "Get")
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
        public static HttpWebRequest CreateRequest(this string url, string Accept, NameValueCollection Headers, string Method = "Get")
        {
            return url.CreateRequest(Accept, Headers, _UserAgent, null, _Timeout, Method);
        }
        /// <summary>
        /// 创建请求链接
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="userAgent"></param>
        /// <param name="referer"></param>
        /// <param name="timeout"></param>
        /// <param name="Method"></param>
        /// <returns></returns>
        public static HttpWebRequest CreateRequest(this string url, string userAgent, string referer, int timeout, string Method = "Get")
        {
            return url.CreateRequest("", null, userAgent, referer, _Timeout, Method);
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
        public static HttpWebRequest CreateRequest(this string url, string Accept, NameValueCollection Headers, string userAgent, string referer, int timeout, string Method = "Get")
        {

            WebRequest req = WebRequest.Create(url);
            HttpWebRequest wreq = req as HttpWebRequest;
            if (wreq != null)
            {
                Uri objUri = new Uri(url);
                wreq.Host = objUri.Host;
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

                    wreq.Referer = objUri.Scheme + "//" + objUri.Host;

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
                    wreq.Headers.Add(Headers);
                }
            }
            return wreq;
        }
        #endregion


    }
}
