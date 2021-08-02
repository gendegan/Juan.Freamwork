using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class RequestHelper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        private static string RequestService(this string url, byte[] data, NameValueCollection header)
        {
            HttpWebRequest objHttpWebRequest = RequestHelper.CreateRequest(url, header, "Post");
            objHttpWebRequest.ContentType = "application/octet-stream";
            HttpWebResponse objHttpWebResponse = objHttpWebRequest.CreateHttpPost(data);
            return objHttpWebResponse.GetPageText();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encryptKey"></param>
        /// <param name="data"></param>
        /// <param name="clientHeaderInfo"></param>
        /// <returns></returns>
        public static string RequestServiceSign(this string url, string encryptKey, byte[] data, ClientHeaderInfo clientHeaderInfo = null)
        {
            string bodyMD5 = data.MD5Encrypt();
            if (url.IndexOf("?") >= 0)
            {
                url = url + "&ContentMD5=" + bodyMD5;
            }
            else
            {
                url = url + "?ContentMD5=" + bodyMD5;
            }
            NameValueCollection objNameValueCollection = new NameValueCollection();
            if (clientHeaderInfo != null)
            {
                string clientHeaderInfoBody = clientHeaderInfo.Json();

                if (url.IndexOf("?") >= 0)
                {
                    url = url + "&HeaderMD5=" + clientHeaderInfoBody.MD5Encrypt();
                }
                else
                {
                    url = url + "?HeaderMD5=" + clientHeaderInfoBody.MD5Encrypt();
                }
                objNameValueCollection.Add("ClientHeaderInfo", clientHeaderInfoBody);
            }
            return url.SignatureInterfaceSortMD5(encryptKey).RequestService(data, objNameValueCollection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encryptKey"></param>
        /// <param name="data"></param>
        /// <param name="clientHeaderInfo"></param>
        /// <param name="requestSettings"></param>
        /// <returns></returns>
        public static string RequestServiceSign(this string url, string encryptKey, object data, ClientHeaderInfo clientHeaderInfo = null, JsonSerializerSettings requestSettings = null)
        {
            return url.RequestServiceSign(encryptKey, System.Text.UTF8Encoding.UTF8.GetBytes(data.Json(requestSettings)), clientHeaderInfo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="encryptKey"></param>
        /// <param name="data"></param>
        /// <param name="clientHeaderInfo"></param>
        /// <param name="responseSettings"></param>
        /// <returns></returns>
        public static T RequestServiceSign<T>(this string url, string encryptKey, byte[] data, ClientHeaderInfo clientHeaderInfo = null, JsonSerializerSettings responseSettings = null)
        {
            string json = url.RequestServiceSign(encryptKey, data, clientHeaderInfo);
            try
            {

                return json.Json<T>(responseSettings);
            }
            catch (Exception objExp)
            {
                objExp.Data.Add("Url", url);
                objExp.Data.Add("ResponseData", json);
                LogHelper.Write(LogType.Error, "请求结果转Json异常", json, objExp);
                throw objExp;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="encryptKey"></param>
        /// <param name="data"></param>
        /// <param name="clientHeaderInfo"></param>
        /// <param name="requestSettings"></param>
        /// <param name="responseSettings"></param>
        /// <returns></returns>
        public static T RequestServiceSign<T>(this string url, string encryptKey, object data, ClientHeaderInfo clientHeaderInfo = null, JsonSerializerSettings requestSettings = null, JsonSerializerSettings responseSettings = null)
        {
            string json = url.RequestServiceSign(encryptKey, System.Text.UTF8Encoding.UTF8.GetBytes(data.Json(requestSettings)), clientHeaderInfo);
            try
            {
                return json.Json<T>(responseSettings);
            }
            catch (Exception objExp)
            {
                objExp.Data.Add("Url", url);
                objExp.Data.Add("ResponseData", json);
                LogHelper.Write(LogType.Error, "请求结果转Json异常", json, objExp);
                throw objExp;
            }
        }
    }
}
