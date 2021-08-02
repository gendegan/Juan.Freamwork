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
        /// 请求签名接口[有按排序]
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="url">url地址</param>
        /// <param name="encryptKey">密钥</param>
        /// <returns></returns>
        public static T GetPageSignatureSort<T>(this string url, string encryptKey)
        {
            return GetPageSignatureSort<T>(url, encryptKey, System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// 请求签名接口[有按排序]
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="url">url地址</param>
        /// <param name="encryptKey">密钥</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static T GetPageSignatureSort<T>(this string url, string encryptKey, Encoding encoding)
        {
            return url.SignatureInterfaceSortMD5(encryptKey).GetPageText<T>(encoding);
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
        public static T GetPageSignatureSort<T>(this string url, string encryptKey, byte[] data, bool IsCompress = true)
        {
            return GetPageSignatureSort<T>(url, encryptKey, data, System.Text.Encoding.UTF8, IsCompress);
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
        public static T GetPageSignatureSort<T>(this string url, string encryptKey, byte[] data, Encoding encoding, bool IsCompress = true)
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
            return url.SignatureInterfaceSortMD5(encryptKey).GetPageText<T>(data, encoding, IsCompress);
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
        public static T GetPageSignatureSort<T>(this string url, string encryptKey, object data, bool IsCompress = true)
        {
            return GetPageSignatureSort<T>(url, encryptKey, data, System.Text.Encoding.UTF8, IsCompress);
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
        public static T GetPageSignatureSort<T>(this string url, string encryptKey, object data, Encoding encoding, bool IsCompress = true)
        {
            byte[] databyte = System.Text.UTF8Encoding.UTF8.GetBytes(data.JsonSerialize());
            return GetPageSignatureSort<T>(url, encryptKey, databyte, encoding, IsCompress);
        }


        /// <summary>
        /// 请求签名接口[有按排序]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">url地址</param>
        /// <param name="encryptKey">密钥</param>
        /// <param name="parameters">请求参数</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static T GetPageSignatureSort<T>(this string url, string encryptKey, IDictionary<string, string> parameters, Encoding encoding)
        {

            if (parameters == null)
            {
                parameters = new Dictionary<string, string>();
            }

            parameters.FilterKey("SignatureMD5", "SignatureStamp");
            parameters.Add("SignatureStamp", StampHelper.StampSecondLong.ToString());
            QuerySorted objQuerySorted = new QuerySorted(url);
            objQuerySorted.Add(parameters);
            string source = objQuerySorted.ToConcatLink();
            string SignatureMD5 = source.SignatureMD5(encryptKey);
            parameters.Add("SignatureMD5", SignatureMD5);
            return url.GetPageText<T>(parameters, encoding);
        }


        /// <summary>
        /// 请求签名接口[有按排序]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">url地址</param>
        /// <param name="encryptKey">密钥</param>
        /// <param name="parameters">请求参数</param>
        /// <returns></returns>
        public static T GetPageSignatureSort<T>(this string url, string encryptKey, IDictionary<string, string> parameters)
        {
            return GetPageSignatureSort<T>(url, encryptKey, parameters, System.Text.Encoding.UTF8);
        }
    }
}
