using System;
using System.IO;
using System.Net;
using System.Web;


namespace Juan.Core
{
    /// <summary>
    /// Request包装类。
    /// </summary>
    public static partial class RequestHelper
    {
        /// <summary>
        /// 获取<seealso cref="HttpResponse"/>对象来自URL参数。
        /// </summary>
        public static HttpWebResponse GetWebResponse(this string url)
        {
            return url.CreateRequest().GetWebResponse();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpWebRequest">WebRequest对象</param>
        /// <returns></returns>
        public static HttpWebResponse GetWebResponse(this HttpWebRequest httpWebRequest)
        {
            try
            {
                return (HttpWebResponse)httpWebRequest.GetResponse();
            }
            catch (Exception objExp)
            {
                objExp.Data.Add("请求地址", httpWebRequest.Address.ToText());
                throw ;

            }
        }

        /// <summary>
        /// 获取指定页面内容来自URL参数
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public static byte[] GetResponseByte(this string url)
        {
            string ContentType = "";
            return GetResponseByte(url, out  ContentType);
        }
        /// <summary>
        /// 获取指定页面内容来自URL参数
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="ContentType">ContentType</param>
        /// <returns></returns>
        public static byte[] GetResponseByte(this string url, out string ContentType)
        {
            return url.CreateRequest().GetResponseByte(out ContentType);
        }
        /// <summary>
        ///  获取指定页面内容来自URL参数
        /// </summary>
        /// <param name="httpWebRequest">WebRequest对象</param>
        /// <returns></returns>
        public static byte[] GetResponseByte(this HttpWebRequest httpWebRequest)
        {
            string ContentType = "";
            return GetResponseByte(httpWebRequest, out ContentType);
        }
        /// <summary>
        /// 获取指定页面内容来自URL参数
        /// </summary>
        /// <param name="httpWebRequest">WebRequest对象</param>
        /// <param name="ContentType">ContentType</param>
        /// <returns></returns>
        public static byte[] GetResponseByte(this HttpWebRequest httpWebRequest, out string ContentType)
        {
            HttpWebResponse res = httpWebRequest.GetWebResponse();
            ContentType = res.ContentType;
            Stream stream = res.GetResponseStream();
            return stream.ToResponseByte();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpWebResponse"></param>
        /// <returns></returns>
        public static byte[] GetResponseByte(this HttpWebResponse httpWebResponse)
        {

            byte[] photocontent = new Byte[httpWebResponse.ContentLength];
            Stream stream = httpWebResponse.GetResponseStream();
            return stream.ToResponseByte();
        }
    }
}
