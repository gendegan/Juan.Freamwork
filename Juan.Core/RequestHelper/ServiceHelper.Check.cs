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
        /// <typeparam name="T"></typeparam>
        /// <param name="objT"></param>
        /// <param name="encryptKey"></param>
        /// <param name="responseSettings"></param>
        /// <param name="TimeoutSecond"></param>
        /// <param name="IpPattern"></param>
        /// <returns></returns>
        public static InvokeResult RequestServiceCheck<T>(out T objT, string encryptKey, JsonSerializerSettings responseSettings = null, int TimeoutSecond = 0, string IpPattern = "") where T : class, new()
        {
            ClientHeaderInfo clientHeaderInfo = null;
            return RequestServiceCheck<T>(out objT, out clientHeaderInfo, encryptKey, responseSettings, TimeoutSecond, IpPattern);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objT"></param>
        /// <param name="clientHeaderInfo"></param>
        /// <param name="encryptKey"></param>
        /// <param name="responseSettings"></param>
        /// <param name="TimeoutSecond"></param>
        /// <param name="IpPattern"></param>
        /// <returns></returns>
        public static InvokeResult RequestServiceCheck<T>(out T objT, out ClientHeaderInfo clientHeaderInfo, string encryptKey, JsonSerializerSettings responseSettings = null, int TimeoutSecond = 0, string IpPattern = "") where T : class, new()
        {

            encryptKey.ArgumentNoNull("encryptKey不能为空");
            objT = null;
            clientHeaderInfo = null;
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

            string urlQuery = objQuerySorted.ToConcatLink();
            if (!urlQuery.SignatureCheckMD5(SignatureMD5, encryptKey))
            {
                objInvokeResult.ResultCode = "Signature";
                objInvokeResult.ResultMessage = "对不起你签名不正确";
                return objInvokeResult;
            }

            string ClientHeaderInfo = GetHeadersString("ClientHeaderInfo");
            if (ClientHeaderInfo.IsNoNullOrWhiteSpace())
            {
                string HeaderMD5 = GetString("HeaderMD5");
                if (HeaderMD5.IsNull())
                {
                    objInvokeResult.ResultCode = "HeaderMD5";
                    objInvokeResult.ResultMessage = "对不起你缺少头部签名参数";
                    return objInvokeResult;
                }

                if (ClientHeaderInfo.MD5Encrypt().ToLower() != HeaderMD5.ToLower())
                {
                    objInvokeResult.ResultCode = "Signature";
                    objInvokeResult.ResultMessage = "对不起你头部签名不正确";
                    return objInvokeResult;
                }
                clientHeaderInfo = ClientHeaderInfo.Json<ClientHeaderInfo>();
            }

            byte[] contentByte = InputStreamByte(false);
            if (contentByte.MD5Encrypt().ToLower() != ContentMD5.ToLower())
            {
                objInvokeResult.ResultCode = "Signature";
                objInvokeResult.ResultMessage = "对不起你内容签名不正确";
                return objInvokeResult;
            }
            string inputStreamString = System.Text.UTF8Encoding.UTF8.GetString(contentByte);
            try
            {
                objT = inputStreamString.Json<T>(responseSettings);
            }
            catch (Exception objExp)
            {
                objExp.Data.Add("RequestData", inputStreamString);
                LogHelper.Write(LogType.Error, "请求结果转Json异常", inputStreamString, objExp);
                throw objExp;
            }
            return objInvokeResult;
        }
    }
}
