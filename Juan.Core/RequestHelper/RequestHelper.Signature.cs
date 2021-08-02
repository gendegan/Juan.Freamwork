using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Juan.Core
{
    public static partial class RequestHelper
    {
        /// <summary>
        /// 检查上一级地址
        /// </summary>
        /// <param name="hostPattern"></param>
        /// <returns></returns>
        public static bool CheckUrlReferrer(this string hostPattern)
        {
            if (string.IsNullOrWhiteSpace(hostPattern))
            {
                throw new ArgumentNullException("hostPattern参数不能为空");
            }
            string referHost = CurrentContext.Request.UrlReferrer != null ? CurrentContext.Request.UrlReferrer.Host.ToLower() : "";
            if (string.IsNullOrWhiteSpace(referHost) || !referHost.IsMatch(hostPattern))
            {
                return false;
            }
            return true;
        }



        /// <summary>
        /// 接口签名调用
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="encryptKey">密钥</param>
        /// <returns></returns>
        public static string SignatureInterfaceMD5(this string url, string encryptKey)
        {
            encryptKey.ArgumentNoNull("encryptKey不能为空");
            QueryCollection objQueryStringHelper = new QueryCollection(url);
            objQueryStringHelper.Add("SignatureStamp", StampHelper.StampSecondLong.ToString());
            string SignatureMD5 = (objQueryStringHelper.CreateQuery(false, false)).SignatureMD5(encryptKey);
            objQueryStringHelper.Add("SignatureMD5", SignatureMD5);
            string[] urlArr = url.Split('?');
            return urlArr[0] + objQueryStringHelper.CreateQuery(true, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encryptKey"></param>
        /// <returns></returns>

        public static string SignatureInterfaceSortMD5(this string url, string encryptKey)
        {
            return SignatureInterfaceSortMD5(url, encryptKey, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encryptKey"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string SignatureInterfaceSortMD5(this string url, string encryptKey, IDictionary<string, string> parameters)
        {
            encryptKey.ArgumentNoNull("encryptKey不能为空");
            QuerySorted objQuerySorted = new QuerySorted(url);
            if (parameters != null && parameters.Count > 0)
            {
                parameters.FilterKey("SignatureMD5", "SignatureStamp");
                objQuerySorted.Add(parameters);
            }
            objQuerySorted.Add("SignatureStamp", StampHelper.StampSecondLong.ToString());
            string source = objQuerySorted.ToConcatLink();
            string SignatureMD5 = source.SignatureMD5(encryptKey);
            objQuerySorted.Add("SignatureMD5", SignatureMD5);
            string[] urlArr = url.Split('?');
            return urlArr[0] + "?" + objQuerySorted.ToConcatLinkUrlEncode();
        }

    }
}
