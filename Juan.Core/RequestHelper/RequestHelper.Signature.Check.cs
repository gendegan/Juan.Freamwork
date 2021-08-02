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
        ///  接口签名验证
        /// </summary>
        /// <param name="encryptKey">密钥</param>
        /// <param name="TimeoutSecond">请求超时限制0不限制</param>
        /// <param name="IpPattern">IP限制</param>
        /// <returns></returns>
        public static bool SignatureInterfaceMD5Check(string encryptKey, int TimeoutSecond = 0, string IpPattern = "")
        {
            return SignatureInterfaceMD5CheckResult(encryptKey, TimeoutSecond, IpPattern).ResultCode == "0";
        }
        /// <summary>
        /// 接口签名验证
        /// </summary>
        /// <param name="TimeoutSecond">请求超时限制0不限制</param>
        /// <param name="IpPattern">IP限制</param>
        /// <param name="encryptKey">密钥</param>
        /// <returns></returns>
        public static InvokeResult SignatureInterfaceMD5CheckResult(string encryptKey, int TimeoutSecond = 0, string IpPattern = "")
        {
            encryptKey.ArgumentNoNull("encryptKey不能为空");
            InvokeResult objInvokeResult = new InvokeResult();
            if (!string.IsNullOrWhiteSpace(IpPattern))
            {
                if (!GetRealIp().IsMatch(IpPattern))
                {
                    objInvokeResult.ResultCode = "Signature";
                    objInvokeResult.ResultMessage = "对不起你请求的IP不允许";
                    return objInvokeResult;
                }
            }
            string SignatureMD5 = GetString("SignatureMD5");
            if (SignatureMD5.IsNull())
            {
                objInvokeResult.ResultCode = "Signature";
                objInvokeResult.ResultMessage = "对不起你缺少签名参数";
                return objInvokeResult;
            }
            if (TimeoutSecond > 0)
            {
                long SignatureStamp = GetLong("SignatureStamp");
                if (SignatureStamp.IsNull())
                {
                    objInvokeResult.ResultCode = "Signature";
                    objInvokeResult.ResultMessage = "对不起你缺少签名时间";
                    return objInvokeResult;
                }

                if (StampHelper.StampSecondLong - SignatureStamp > TimeoutSecond)
                {
                    objInvokeResult.ResultCode = "Signature";
                    objInvokeResult.ResultMessage = "对不起你的签名时间超时";
                    return objInvokeResult;
                }
            }
            string urlQuery = CurrentContext.Request.QueryString.ToString().DecodeUrl().Replace("&SignatureMD5=.+", "", RegexOptions.IgnoreCase);
            if (!urlQuery.SignatureCheckMD5(SignatureMD5, encryptKey))
            {
                objInvokeResult.ResultCode = "Signature";
                objInvokeResult.ResultMessage = "对不起你签名不正确";
                return objInvokeResult;
            }
            return objInvokeResult;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="objT"></param>
        /// <param name="encryptKey">密钥</param>
        /// <param name="TimeoutSecond">请求超时限制0不限制</param>
        /// <param name="IpPattern">IP限制</param>
        /// <param name="IsDecompress">是否内容需要解压</param>
        /// <returns></returns>
        public static bool SignatureInterfaceMD5Check<T>(out T objT, string encryptKey, int TimeoutSecond = 0, string IpPattern = "", bool IsDecompress = true) where T : class, new()
        {
            return SignatureInterfaceMD5CheckResult<T>(out objT, encryptKey, System.Text.Encoding.UTF8, TimeoutSecond, IpPattern, IsDecompress).ResultCode == "0";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="objT"></param>
        /// <param name="encryptKey">密钥</param>
        /// <param name="TimeoutSecond">请求超时限制0不限制</param>
        /// <param name="IpPattern">IP限制</param>
        /// <param name="IsDecompress">是否内容需要解压</param>
        /// <returns></returns>
        public static InvokeResult SignatureInterfaceMD5CheckResult<T>(out T objT, string encryptKey, int TimeoutSecond = 0, string IpPattern = "", bool IsDecompress = true) where T : class, new()
        {
            return SignatureInterfaceMD5CheckResult<T>(out objT, encryptKey, System.Text.Encoding.UTF8, TimeoutSecond, IpPattern, IsDecompress);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="objT"></param>
        /// <param name="encryptKey">密钥</param>
        /// <param name="encoding">编码</param>
        /// <param name="TimeoutSecond">请求超时限制0不限制</param>
        /// <param name="IpPattern">IP限制</param>
        /// <param name="IsDecompress">是否内容需要解压</param>
        /// <returns></returns>
        public static InvokeResult SignatureInterfaceMD5CheckResult<T>(out T objT, string encryptKey, Encoding encoding, int TimeoutSecond = 0, string IpPattern = "", bool IsDecompress = true) where T : class, new()
        {
            encryptKey.ArgumentNoNull("encryptKey不能为空");
            objT = null;
            InvokeResult objInvokeResult = new InvokeResult();
            if (!string.IsNullOrWhiteSpace(IpPattern))
            {
                if (!GetRealIp().IsMatch(IpPattern))
                {
                    objInvokeResult.ResultCode = "Signature";
                    objInvokeResult.ResultMessage = "对不起你请求的IP不允许";
                    return objInvokeResult;
                }
            }
            string SignatureMD5 = GetString("SignatureMD5");
            if (SignatureMD5.IsNull())
            {
                objInvokeResult.ResultCode = "Signature";
                objInvokeResult.ResultMessage = "对不起你缺少签名参数";
                return objInvokeResult;
            }
            string ContentMD5 = GetString("ContentMD5");
            if (ContentMD5.IsNull())
            {
                objInvokeResult.ResultCode = "ContentMD5";
                objInvokeResult.ResultMessage = "对不起你缺少内容签名参数";
                return objInvokeResult;
            }
            if (TimeoutSecond > 0)
            {
                long SignatureStamp = GetLong("SignatureStamp");
                if (SignatureStamp.IsNull())
                {
                    objInvokeResult.ResultCode = "Signature";
                    objInvokeResult.ResultMessage = "对不起你缺少签名时间";
                    return objInvokeResult;
                }

                if (StampHelper.StampSecondLong - SignatureStamp > TimeoutSecond)
                {
                    objInvokeResult.ResultCode = "Signature";
                    objInvokeResult.ResultMessage = "对不起你的签名时间超时";
                    return objInvokeResult;
                }
            }
            string urlQuery = CurrentContext.Request.QueryString.ToString().DecodeUrl().Replace("&SignatureMD5=.+", "", RegexOptions.IgnoreCase).DecodeUrl();
            if (!urlQuery.SignatureCheckMD5(SignatureMD5, encryptKey))
            {
                objInvokeResult.ResultCode = "Signature";
                objInvokeResult.ResultMessage = "对不起你签名不正确";
                return objInvokeResult;
            }

            byte[] contentByte = InputStreamByte(IsDecompress);
            if (!contentByte.MD5Encrypt().Equals(ContentMD5, StringComparison.OrdinalIgnoreCase))
            {
                objInvokeResult.ResultCode = "Signature";
                objInvokeResult.ResultMessage = "对不起你内容签名不正确";
                return objInvokeResult;
            }
            objT = encoding.GetString(contentByte).JsonDeserialize<T>();
            return objInvokeResult;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentByte"></param>
        /// <param name="encryptKey"></param>
        /// <param name="encoding"></param>
        /// <param name="TimeoutSecond"></param>
        /// <param name="IpPattern"></param>
        /// <param name="IsDecompress"></param>
        /// <returns></returns>
        public static InvokeResult SignatureInterfaceMD5CheckResult(out byte[] contentByte, string encryptKey, Encoding encoding, int TimeoutSecond = 0, string IpPattern = "", bool IsDecompress = true)
        {
            encryptKey.ArgumentNoNull("encryptKey不能为空");
            contentByte = null;
            InvokeResult objInvokeResult = new InvokeResult();
            if (!string.IsNullOrWhiteSpace(IpPattern))
            {
                if (!GetRealIp().IsMatch(IpPattern))
                {
                    objInvokeResult.ResultCode = "Signature";
                    objInvokeResult.ResultMessage = "对不起你请求的IP不允许";
                    return objInvokeResult;
                }
            }
            string SignatureMD5 = GetString("SignatureMD5");
            if (SignatureMD5.IsNull())
            {
                objInvokeResult.ResultCode = "Signature";
                objInvokeResult.ResultMessage = "对不起你缺少签名参数";
                return objInvokeResult;
            }
            string ContentMD5 = GetString("ContentMD5");
            if (ContentMD5.IsNull())
            {
                objInvokeResult.ResultCode = "ContentMD5";
                objInvokeResult.ResultMessage = "对不起你缺少内容签名参数";
                return objInvokeResult;
            }
            if (TimeoutSecond > 0)
            {
                long SignatureStamp = GetLong("SignatureStamp");
                if (SignatureStamp.IsNull())
                {
                    objInvokeResult.ResultCode = "Signature";
                    objInvokeResult.ResultMessage = "对不起你缺少签名时间";
                    return objInvokeResult;
                }

                if (StampHelper.StampSecondLong - SignatureStamp > TimeoutSecond)
                {
                    objInvokeResult.ResultCode = "Signature";
                    objInvokeResult.ResultMessage = "对不起你的签名时间超时";
                    return objInvokeResult;
                }
            }
            string urlQuery = CurrentContext.Request.QueryString.ToString().DecodeUrl().Replace("&SignatureMD5=.+", "", RegexOptions.IgnoreCase).DecodeUrl();
            if (!urlQuery.SignatureCheckMD5(SignatureMD5, encryptKey))
            {
                objInvokeResult.ResultCode = "Signature";
                objInvokeResult.ResultMessage = "对不起你签名不正确";
                return objInvokeResult;
            }

            contentByte = InputStreamByte(IsDecompress);
            if (!contentByte.MD5Encrypt().Equals(ContentMD5, StringComparison.OrdinalIgnoreCase))
            {
                objInvokeResult.ResultCode = "Signature";
                objInvokeResult.ResultMessage = "对不起你内容签名不正确";
                return objInvokeResult;
            }

            return objInvokeResult;
        }





    }
}
