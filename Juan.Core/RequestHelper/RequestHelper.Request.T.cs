using System;
using System.Collections.Generic;
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
        /// 
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public static T GetPageText<T>(this string url)
        {
            return GetPageText<T>(url, System.Text.Encoding.UTF8);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="url">url地址</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static T GetPageText<T>(this string url, Encoding encoding)
        {
            string json = url.GetPageText(encoding);
            try
            {
                return json.JsonDeserialize<T>();
            }
            catch (Exception objExp)
            {
                objExp.Data.Add("Url", url);
                objExp.Data.Add("ResponseData", json);
                LogHelper.Write(LogType.Error, "请求结果转Json异常", json, objExp);
                throw ;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="url">url地址</param>
        /// <param name="data">请求数据</param>
        /// <param name="IsCompress">是否压缩</param>
        /// <returns></returns>
        public static T GetPageText<T>(this string url, byte[] data, bool IsCompress = true)
        {
            return GetPageText<T>(url, data, System.Text.Encoding.UTF8, IsCompress);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="url">url地址</param>
        /// <param name="data">请求数据</param>
        /// <param name="encoding">编码</param>
        /// <param name="IsCompress">是否压缩</param>
        /// <returns></returns>
        public static T GetPageText<T>(this string url, byte[] data, Encoding encoding, bool IsCompress = true)
        {


            string json = url.GetPageText(data, encoding, IsCompress);
            try
            {
                return json.JsonDeserialize<T>();
            }
            catch (Exception objExp)
            {

                objExp.Data.Add("Url", url);
                objExp.Data.Add("ResponseData", json);
                LogHelper.Write(LogType.Error, "请求结果转Json异常", json, objExp);
                throw ;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static T GetPageText<T>(this string url, IDictionary<string, string> parameters)
        {
            return url.GetPageText<T>(parameters, System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static T GetPageText<T>(this string url, IDictionary<string, string> parameters, Encoding encoding)
        {


            string json = url.GetPageText(parameters, encoding);
            try
            {
                return json.JsonDeserialize<T>();
            }
            catch (Exception objExp)
            {

                objExp.Data.Add("Url", url);
                objExp.Data.Add("ResponseData", json);
                LogHelper.Write(LogType.Error, "请求结果转Json异常", json, objExp);
                throw ;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="url">url地址</param>
        /// <param name="data">请求数据</param>
        /// <param name="IsCompress">是否压缩</param>
        /// <returns></returns>
        public static T GetPageText<T>(this string url, object data, bool IsCompress = true)
        {
            return GetPageText<T>(url, data, System.Text.Encoding.UTF8, IsCompress);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="url">url地址</param>
        /// <param name="data">请求数据</param>
        /// <param name="encoding">编码</param>
        /// <param name="IsCompress">是否压缩</param>
        /// <returns></returns>
        public static T GetPageText<T>(this string url, object data, Encoding encoding, bool IsCompress = true)
        {
            byte[] databyte = System.Text.UTF8Encoding.UTF8.GetBytes(data.JsonSerialize());
            return GetPageText<T>(url, databyte, encoding, IsCompress);
        }




        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static T GetPageText<T>(this HttpWebResponse response, Encoding encoding)
        {
            string json = response.GetPageText(encoding);

            try
            {
                return json.JsonDeserialize<T>();
            }
            catch (Exception objExp)
            {

                objExp.Data.Add("ResponseData", json);
                LogHelper.Write(LogType.Error, "请求结果转Json异常", json, objExp);
                throw ;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static T GetPageText<T>(this HttpWebResponse response)
        {
            return response.GetPageText<T>(System.Text.Encoding.UTF8);
        }


        /// <summary>
        ///  获取指定页面内容来自URL参数。
        /// </summary>
        /// <param name="httpWebRequest">WebRequest对象</param>
        /// <returns></returns>
        public static T GetPageText<T>(this HttpWebRequest httpWebRequest)
        {
            return httpWebRequest.GetPageText<T>(System.Text.Encoding.UTF8);
        }


        /// <summary>
        /// 获取指定页面内容来自URL参数。
        /// </summary>
        /// <param name="httpWebRequest">WebRequest对象</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static T GetPageText<T>(this HttpWebRequest httpWebRequest, Encoding encoding)
        {
            return httpWebRequest.GetWebResponse().GetPageText<T>(encoding);
        }


    }
}
