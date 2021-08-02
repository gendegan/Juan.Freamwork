using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
        /// 获取页面byte数据
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <returns></returns>
        public static byte[] GetPageByte(this string url, IDictionary<string, string> parameters)
        {
            return url.CreateRequest().GetPageByte(parameters);
        }

        /// <summary>
        /// 获取页面byte数据
        /// </summary>
        /// <param name="httpWebRequest">WebRequest对象</param>
        /// <param name="parameters">请求参数</param>
        /// <returns></returns>
        public static byte[] GetPageByte(this HttpWebRequest httpWebRequest, IDictionary<string, string> parameters)
        {
            HttpWebResponse objHttpWebResponse = httpWebRequest.CreateHttpPost(parameters);
            return objHttpWebResponse.GetResponseByte();
        }

        /// <summary>
        ///  获取指定页面内容来自URL参数。
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public static string GetPageText(this string url)
        {
            return url.GetPageText(System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// 获取指定页面内容来自URL参数。
        /// </summary>
        public static string GetPageText(this string url, Encoding encoding)
        {
            return url.CreateRequest().GetPageText(encoding);
        }

        /// <summary>
        /// 获取指定页面内容
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">请求数据</param>
        /// <param name="IsCompress">是否压缩</param>
        /// <returns></returns>
        public static string GetPageText(this string url, byte[] data, bool IsCompress = true)
        {
            return GetPageText(url, data, System.Text.Encoding.UTF8, IsCompress);
        }
        /// <summary>
        /// 获取指定页面内容
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">请求数据</param>
        /// <param name="encoding">编码</param>
        /// <param name="IsCompress">是否压缩</param>
        /// <returns></returns>
        public static string GetPageText(this string url, byte[] data, Encoding encoding, bool IsCompress = true)
        {

            if (IsCompress)
            {
                data = data.CompressGZip();
            }
            return url.CreateHttpPost(data).GetPageText(encoding);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetPageText(this string url, IDictionary<string, string> parameters, Encoding encoding)
        {

            return url.CreateHttpPost(parameters).GetPageText(encoding);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string GetPageText(this string url, IDictionary<string, string> parameters)
        {
            return url.CreateHttpPost(parameters).GetPageText(System.Text.Encoding.UTF8);
        }


        /// <summary>
        ///  获取指定页面内容来自URL参数。
        /// </summary>
        /// <param name="httpWebRequest">WebRequest对象</param>
        /// <returns></returns>
        public static string GetPageText(this HttpWebRequest httpWebRequest)
        {
            return httpWebRequest.GetPageText(System.Text.Encoding.UTF8);
        }


        /// <summary>
        /// 获取指定页面内容来自URL参数。
        /// </summary>
        /// <param name="httpWebRequest">WebRequest对象</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string GetPageText(this HttpWebRequest httpWebRequest, Encoding encoding)
        {
            return httpWebRequest.GetWebResponse().GetPageText(encoding);
        }

        /// <summary>
        /// 获取指定页面内容来自URL参数。
        /// </summary>
        /// <param name="httpWebRequest">WebRequest对象</param>
        /// <returns></returns>
        public static string GetPageText(this WebRequest httpWebRequest)
        {
            HttpWebRequest wreq = httpWebRequest as HttpWebRequest;
            return wreq.GetPageText(System.Text.Encoding.UTF8);
        }






        /// <summary>
        /// 获取指定页面内容来自URL参数。
        /// </summary>
        public static string GetPageText(this HttpWebResponse response, Encoding encoding)
        {
            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(s, encoding))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string GetGZipPageText(this HttpWebResponse response, Encoding encoding)
        {
            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(new GZipStream(s, CompressionMode.Decompress), encoding))
                {
                    return sr.ReadToEnd();
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string GetPageText(this HttpWebResponse response)
        {
            return response.GetPageText(System.Text.Encoding.UTF8);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string GetGZipPageText(this HttpWebResponse response)
        {
            return response.GetGZipPageText(System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="data">请求数据</param>
        /// <returns></returns>
        public static Stream GetPageStream(this string url, byte[] data)
        {
            return url.CreateRequest().GetPageStream(data);
        }
        /// <summary>
        /// 获取界面
        /// </summary>
        /// <param name="httpWebRequest">WebRequest对象</param>
        /// <param name="data">请求数据</param>
        /// <returns></returns>
        public static Stream GetPageStream(this HttpWebRequest httpWebRequest, byte[] data)
        {
            HttpWebResponse objHttpWebResponse = httpWebRequest.CreateHttpPost(data);
            return objHttpWebResponse.GetResponseStream();
        }

        /// <summary>
        /// 判断远程图片是否存在
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        public static bool RemoteFileExists(this string fileUrl)
        {
            bool result = false;//下载结果

            WebResponse response = null;
            try
            {
                WebRequest req = WebRequest.Create(fileUrl);
                response = req.GetResponse();
                result = response == null ? false : true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return result;
        }
        /// <summary>
        /// 判断远程图片是否存在
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public static int GetUrlIsExists(this string url)
        {
            int num = 1;
            if (url.Length > 1)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
                ServicePointManager.Expect100Continue = false;
                try
                {
                    ((HttpWebResponse)request.GetResponse()).Close();
                }
                catch (WebException exception)
                {
                    if (exception.Status != WebExceptionStatus.ProtocolError)
                    {
                        return num;
                    }
                    if (exception.Message.IndexOf("500") > 0)
                    {
                        return 500;
                    }
                    if (exception.Message.IndexOf("401") > 0)
                    {
                        return 401;
                    }
                    if (exception.Message.IndexOf("403") > 0)
                    {
                        return 403;
                    }
                    if (exception.Message.IndexOf("404") > 0)
                    {
                        num = 404;
                    }
                }
            }
            return num;
        }
        /// <summary>
        /// 检查地址是否存在
        /// </summary>
        /// <param name="objHttpWebRequest"></param>
        /// <returns></returns>
        public static int GetUrlIsExists(this HttpWebRequest objHttpWebRequest)
        {
            int num = 1;
            ServicePointManager.Expect100Continue = false;
            try
            {
                ((HttpWebResponse)objHttpWebRequest.GetResponse()).Close();
            }
            catch (WebException exception)
            {
                if (exception.Status != WebExceptionStatus.ProtocolError)
                {
                    return num;
                }
                if (exception.Message.IndexOf("500") > 0)
                {
                    return 500;
                }
                if (exception.Message.IndexOf("401") > 0)
                {
                    return 401;
                }
                if (exception.Message.IndexOf("403") > 0)
                {
                    return 403;
                }
                if (exception.Message.IndexOf("404") > 0)
                {
                    num = 404;
                }
            }
            return num;
        }


    }

}
