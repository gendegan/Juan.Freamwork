using System;
using System.Configuration;
using System.Net;
using System.Text;


namespace Juan.Core
{
    /// <summary>
    /// Request包装类。
    /// </summary>
    public static partial class RequestHelper
    {
        /// <summary>
        /// 设置代理服务器
        /// </summary>
        /// <param name="objHttpWebRequest"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static HttpWebRequest SetWebProxy(this  HttpWebRequest objHttpWebRequest, string host, int port)
        {
            objHttpWebRequest.Proxy = new WebProxy(host, port);
            return objHttpWebRequest;
        }

        /// <summary>
        /// 设置代理
        /// </summary>
        /// <param name="objHttpWebRequest"></param>
        /// <param name="ProxyConfigKey">[默认值Seven_ProxyHost]</param>
        /// <returns></returns>
        public static HttpWebRequest SetWebProxy(this  HttpWebRequest objHttpWebRequest, string ProxyConfigKey = "")
        {
            if (string.IsNullOrWhiteSpace(ProxyConfigKey))
            {
                ProxyConfigKey = "Seven_ProxyHost";
            }

            string proxy = ConfigHelper.GetValue(ProxyConfigKey, "");
            if (string.IsNullOrWhiteSpace(proxy))
            {
                throw new ConfigurationErrorsException("找不到" + ProxyConfigKey + "代理配置值");
            }

            string[] proxyHostList = proxy.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string proxyValue = "";
            if (proxyHostList.Length == 1)
            {
                proxyValue = proxyHostList[0];
            }
            else
            {
                proxyValue = proxyHostList[RandomHelper.GetRandomNumber(0, proxyHostList.Length)];
            }
            string host = proxyValue.Split(':')[0];
            int port = int.Parse(proxyValue.Split(':')[1]);
            objHttpWebRequest.Proxy = new WebProxy(host, port);
            return objHttpWebRequest;
        }


        /// <summary>
        /// 获取代理
        /// </summary>
        /// <param name="ProxyConfigKey"></param>
        /// <returns></returns>
        public static WebProxy GetWebProxy(string ProxyConfigKey = "")
        {
            if (string.IsNullOrWhiteSpace(ProxyConfigKey))
            {
                ProxyConfigKey = "Seven_ProxyHost";
            }
            string proxy = ConfigHelper.GetValue(ProxyConfigKey, "");
            if (string.IsNullOrWhiteSpace(proxy))
            {
                throw new ConfigurationErrorsException("找不到" + ProxyConfigKey + "代理配置值");
            }

            string[] proxyHostList = proxy.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string proxyValue = "";
            if (proxyHostList.Length == 1)
            {
                proxyValue = proxyHostList[0];
            }
            else
            {
                proxyValue = proxyHostList[RandomHelper.GetRandomNumber(0, proxyHostList.Length)];
            }
            string host = proxyValue.Split(':')[0];
            int port = int.Parse(proxyValue.Split(':')[1]);
            return new WebProxy(host, port);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static string GetPageProxyText(this string url, string host, int port)
        {
            return url.CreateRequest().SetWebProxy(host, port).GetPageText();
        }
        /// <summary>
        /// 获取指定页面内容来自URL参数。
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="encoding">编码</param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static string GetPageProxyText(this string url, Encoding encoding, string host, int port)
        {
            return url.CreateRequest().SetWebProxy(host, port).GetPageText(encoding);
        }
        /// <summary>
        /// 获取指定页面内容来自URL参数。
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="encoding">编码</param>
        /// <param name="Seven_ProxyHost"></param>
        /// <returns></returns>
        public static string GetPageProxyText(this string url, Encoding encoding, string Seven_ProxyHost = "")
        {
            return url.CreateRequest().SetWebProxy(Seven_ProxyHost).GetPageText(encoding);
        }

        /// <summary>
        /// 获取指定页面内容来自URL参数。
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="Seven_ProxyHost"></param>
        /// <returns></returns>
        public static string GetPageProxyText(this string url, string Seven_ProxyHost = "")
        {
            return url.CreateRequest().SetWebProxy(Seven_ProxyHost).GetPageText();
        }

    }
}
