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
        /// 
        /// </summary>
        /// <param name="encryptKey"></param>
        /// <param name="TimeoutSecond"></param>
        /// <param name="IpPattern"></param>
        /// <returns></returns>
        public static bool SignatureMD5SortCheck(string encryptKey, int TimeoutSecond = 0, string IpPattern = "")
        {
            return SignatureMD5SortCheckResult(encryptKey, TimeoutSecond, IpPattern).ResultCode == "0";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptKey"></param>
        /// <param name="TimeoutSecond"></param>
        /// <param name="IpPattern"></param>
        /// <returns></returns>
        public static InvokeResult SignatureMD5SortCheckResult(string encryptKey, int TimeoutSecond = 0, string IpPattern = "")
        {
            encryptKey.ArgumentNoNull("encryptKey不能为空");
            InvokeResult objInvokeResult = new InvokeResult();
            if (!string.IsNullOrWhiteSpace(IpPattern))
            {
                if (!GetRealIp().ToLower().IsMatch(IpPattern))
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
            QuerySorted objQuerySorted = new QuerySorted(CurrentContext);
            objQuerySorted.FilterKey("SignatureMD5");
            objQuerySorted.FilterKey("signatureMD5");
            objQuerySorted.FilterKey("signaturemd5");
            string urlQuery = objQuerySorted.ToConcatLink();
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
        public static bool SignatureMD5SortCheck<T>(out T objT, string encryptKey, int TimeoutSecond = 0, string IpPattern = "", bool IsDecompress = true) where T : class,new()
        {
            return SignatureMD5SortCheckResult<T>(out objT, encryptKey, System.Text.Encoding.UTF8, TimeoutSecond, IpPattern, IsDecompress).ResultCode == "0";
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
        public static InvokeResult SignatureMD5SortCheckResult<T>(out T objT, string encryptKey, int TimeoutSecond = 0, string IpPattern = "", bool IsDecompress = true) where T : class,new()
        {
            return SignatureMD5SortCheckResult<T>(out objT, encryptKey, System.Text.Encoding.UTF8, TimeoutSecond, IpPattern, IsDecompress);
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
        public static InvokeResult SignatureMD5SortCheckResult<T>(out T objT, string encryptKey, Encoding encoding, int TimeoutSecond = 0, string IpPattern = "", bool IsDecompress = true) where T : class,new()
        {
            encryptKey.ArgumentNoNull("encryptKey不能为空");
            objT = null;
            InvokeResult objInvokeResult = new InvokeResult();
            if (!string.IsNullOrWhiteSpace(IpPattern))
            {
                if (!GetRealIp().ToLower().IsMatch(IpPattern))
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
            QuerySorted objQuerySorted = new QuerySorted(CurrentContext);
            objQuerySorted.FilterKey("SignatureMD5");
            objQuerySorted.FilterKey("signatureMD5");
            objQuerySorted.FilterKey("signaturemd5");
            string urlQuery = objQuerySorted.ToConcatLink();
            if (!urlQuery.SignatureCheckMD5(SignatureMD5, encryptKey))
            {
                objInvokeResult.ResultCode = "Signature";
                objInvokeResult.ResultMessage = "对不起你签名不正确";
                return objInvokeResult;
            }

            byte[] contentByte = InputStreamByte(IsDecompress);
            if (contentByte.MD5Encrypt().ToLower() != ContentMD5.ToLower())
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
        public static InvokeResult SignatureMD5SortCheckResult(out  byte[] contentByte, string encryptKey, Encoding encoding, int TimeoutSecond = 0, string IpPattern = "", bool IsDecompress = true)
        {
            encryptKey.ArgumentNoNull("encryptKey不能为空");
            contentByte = null;
            InvokeResult objInvokeResult = new InvokeResult();
            if (!string.IsNullOrWhiteSpace(IpPattern))
            {
                if (!GetRealIp().ToLower().IsMatch(IpPattern))
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
            QuerySorted objQuerySorted = new QuerySorted(CurrentContext);
            objQuerySorted.FilterKey("SignatureMD5");
            objQuerySorted.FilterKey("signatureMD5");
            objQuerySorted.FilterKey("signaturemd5");
            string urlQuery = objQuerySorted.ToConcatLink();
            if (!urlQuery.SignatureCheckMD5(SignatureMD5, encryptKey))
            {
                objInvokeResult.ResultCode = "Signature";
                objInvokeResult.ResultMessage = "对不起你签名不正确";
                return objInvokeResult;
            }

            contentByte = InputStreamByte(IsDecompress);
            if (contentByte.MD5Encrypt().ToLower() != ContentMD5.ToLower())
            {
                objInvokeResult.ResultCode = "Signature";
                objInvokeResult.ResultMessage = "对不起你内容签名不正确";
                return objInvokeResult;
            }

            return objInvokeResult;
        }



    }
}
