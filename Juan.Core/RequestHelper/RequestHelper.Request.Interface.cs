using System.Collections.Generic;
using System.Text;


namespace Juan.Core
{
    /// <summary>
    /// Request包装类。
    /// </summary>
    public static partial class RequestHelper
    {


        /// <summary>
        /// 请求签名接口
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="url">url地址</param>
        /// <param name="encryptKey">密钥</param>
        /// <returns></returns>
        public static T GetPageSignature<T>(this string url, string encryptKey)
        {
            return GetPageSignature<T>(url, encryptKey, System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// 请求签名接口
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="url">url地址</param>
        /// <param name="encryptKey">密钥</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static T GetPageSignature<T>(this string url, string encryptKey, Encoding encoding)
        {
            return url.SignatureInterfaceMD5(encryptKey).GetPageText<T>(encoding);
        }


        /// <summary>
        /// 请求签名接口
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="url">url地址</param>
        /// <param name="encryptKey">密钥</param>
        /// <param name="data">请求数据</param>
        /// <param name="IsCompress">是否压缩</param>
        /// <returns></returns>
        public static T GetPageSignature<T>(this string url, string encryptKey, byte[] data, bool IsCompress = true)
        {
            return GetPageSignature<T>(url, encryptKey, data, System.Text.Encoding.UTF8, IsCompress);
        }
        /// <summary>
        /// 请求签名接口
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="url">url地址</param>
        /// <param name="encryptKey">密钥</param>
        /// <param name="data">请求数据</param>
        /// <param name="encoding">编码</param>
        /// <param name="IsCompress">是否压缩</param>
        /// <returns></returns>
        public static T GetPageSignature<T>(this string url, string encryptKey, byte[] data, Encoding encoding, bool IsCompress = true)
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
            return url.SignatureInterfaceMD5(encryptKey).GetPageText<T>(data, encoding, IsCompress);
        }

        /// <summary>
        /// 请求签名接口
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="url">url地址</param>
        /// <param name="encryptKey">密钥</param>
        /// <param name="data">请求数据</param>
        /// <param name="IsCompress">是否压缩</param>
        /// <returns></returns>
        public static T GetPageSignature<T>(this string url, string encryptKey, object data, bool IsCompress = true)
        {
            return GetPageSignature<T>(url, encryptKey, data, System.Text.Encoding.UTF8, IsCompress);
        }
        /// <summary>
        /// 请求签名接口
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="url">url地址</param>
        /// <param name="encryptKey">密钥</param>
        /// <param name="data">请求数据</param>
        /// <param name="encoding">编码</param>
        /// <param name="IsCompress">是否压缩</param>
        /// <returns></returns>
        public static T GetPageSignature<T>(this string url, string encryptKey, object data, Encoding encoding, bool IsCompress = true)
        {
            byte[] databyte = System.Text.UTF8Encoding.UTF8.GetBytes(data.JsonSerialize());
            return GetPageSignature<T>(url, encryptKey, databyte, encoding, IsCompress);
        }

    }
}
