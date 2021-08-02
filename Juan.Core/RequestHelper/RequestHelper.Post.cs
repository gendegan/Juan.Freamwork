using System;
using System.Collections.Generic;
using System.IO;
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
        /// 制造页面请求
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="items">请求参数</param>
        /// <returns></returns>
        public static HttpWebResponse CreateHttpPost(this string url, params HttpPostItem[] items)
        {
            return url.CreateRequest().CreateHttpPost(items);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static HttpWebResponse CreateHttpPost<TValue>(this string url, IDictionary<string, TValue> parameters)
        {
            return url.CreateRequest().CreateHttpPost(parameters);
        }


        /// <summary>
        /// 制造页面请求
        /// </summary>
        /// <param name="wreq">请求对象</param>
        /// <param name="items">请求参数</param>
        /// <returns></returns>
        public static HttpWebResponse CreateHttpPost(this HttpWebRequest wreq, params HttpPostItem[] items)
        {
            if (wreq == null)
                throw new Exception("HttpWebRequest 尚未初始化。");

            if (items == null || items.Length == 0)
                throw new Exception("No HttpPostItems");

            StringBuilder parameters = new StringBuilder();

            foreach (HttpPostItem item in items)
            {
                parameters.Append("&" + item.ToString());
            }

            byte[] payload = Encoding.UTF8.GetBytes(parameters.ToString().Substring(1));


            wreq.Method = "POST";

            wreq.ContentLength = payload.Length;
            if (string.IsNullOrWhiteSpace(wreq.ContentType))
            {
                wreq.ContentType = "application/x-www-form-urlencoded";
            }
            wreq.KeepAlive = false;
            using (Stream st = wreq.GetRequestStream())
            {
                st.Write(payload, 0, payload.Length);
                st.Close();
                return wreq.GetResponse() as HttpWebResponse;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wreq"></param>
        /// <param name="parametersQuery"></param>
        /// <returns></returns>
        public static HttpWebResponse CreateHttpPost(this HttpWebRequest wreq, string parametersQuery)
        {
            if (wreq == null)
                throw new Exception("HttpWebRequest 尚未初始化。");

            byte[] payload = Encoding.UTF8.GetBytes(parametersQuery);

            wreq.Method = "POST";

            wreq.ContentLength = payload.Length;
            if (string.IsNullOrWhiteSpace(wreq.ContentType))
            {
                wreq.ContentType = "application/x-www-form-urlencoded";
            }
            wreq.KeepAlive = false;
            using (Stream st = wreq.GetRequestStream())
            {
                st.Write(payload, 0, payload.Length);
                st.Close();
                return wreq.GetResponse() as HttpWebResponse;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="wreq"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static HttpWebResponse CreateHttpPost<TValue>(this HttpWebRequest wreq, IDictionary<string, TValue> parameters)
        {
            if (wreq == null)
                throw new Exception("HttpWebRequest 尚未初始化。");

            if (parameters == null || parameters.Count == 0)
                throw new Exception("No HttpPostItems");


            byte[] payload = Encoding.UTF8.GetBytes(parameters.ToConcatLinkUrlEncode());


            wreq.Method = "POST";

            wreq.ContentLength = payload.Length;
            if (string.IsNullOrWhiteSpace(wreq.ContentType))
            {
                wreq.ContentType = "application/x-www-form-urlencoded";
            }
            wreq.KeepAlive = false;
            using (Stream st = wreq.GetRequestStream())
            {
                st.Write(payload, 0, payload.Length);
                st.Close();
                return wreq.GetResponse() as HttpWebResponse;
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static HttpWebResponse CreateHttpPost(this string url, IDictionary<string, ICollection<string>> parameters)
        {
            return url.CreateRequest().CreateHttpPost(parameters);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="wreq"></param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static HttpWebResponse CreateHttpPost(this HttpWebRequest wreq, IDictionary<string, ICollection<string>> parameters)
        {
            if (wreq == null)
                throw new Exception("HttpWebRequest 尚未初始化。");

            byte[] payload = Encoding.UTF8.GetBytes(parameters.BuildQueryParams());
            wreq.Method = "POST";
            wreq.ContentLength = payload.Length;
            if (string.IsNullOrWhiteSpace(wreq.ContentType))
            {
                wreq.ContentType = "application/x-www-form-urlencoded";
            }
            wreq.KeepAlive = false;
            using (Stream st = wreq.GetRequestStream())
            {
                st.Write(payload, 0, payload.Length);
                st.Close();
                return wreq.GetResponse() as HttpWebResponse;
            }
        }

        /// <summary>
        /// 创建HTTPOST请求
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">请求数据</param>
        /// <returns></returns>
        public static HttpWebResponse CreateHttpPost(this string url, byte[] data)
        {
            return url.CreateRequest().CreateHttpPost(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objHttpWebRequest"></param>
        /// <param name="data">请求数据</param>
        /// <returns></returns>
        public static HttpWebResponse CreateHttpPost(this HttpWebRequest objHttpWebRequest, byte[] data)
        {
            StringBuilder parameters = new StringBuilder();
            objHttpWebRequest.Method = "POST";
            objHttpWebRequest.ContentLength = data.Length;
            if (string.IsNullOrWhiteSpace(objHttpWebRequest.ContentType))
            {
                objHttpWebRequest.ContentType = "multipart/form-data";
            }
            objHttpWebRequest.KeepAlive = false;
            using (Stream st = objHttpWebRequest.GetRequestStream())
            {
                st.Write(data, 0, data.Length);
                st.Close();
                return objHttpWebRequest.GetResponse() as HttpWebResponse;
            }
        }



    }

    #region Http请求项
    /// <summary>
    /// Http请求项
    /// </summary>
    public class HttpPostItem
    {
        private string _parameter = null;
        private string _value = null;
        private bool _isEncoded = false;

        #region 构造函数
        /// <summary>
        /// 构造数据
        /// </summary>
        /// <param name="paramter">请求参数</param>
        /// <param name="postValue">请求值</param>
        /// <param name="isEncoded">是否编码</param>
        public HttpPostItem(string paramter, string postValue, bool isEncoded)
        {
            _parameter = paramter;
            _value = postValue;
            _isEncoded = isEncoded;
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造数据
        /// </summary>
        /// <param name="paramter">参数名</param>
        /// <param name="postValue">参数值</param>
        public HttpPostItem(string paramter, string postValue)
            : this(paramter, postValue, true)
        {

        }
        #endregion

        #region 转成相对应的
        /// <summary>
        /// 转成相对应的
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}={1}", _parameter, _isEncoded ? _value.EncodeUrl() : _value);
        }
        #endregion
    }
    #endregion
}
