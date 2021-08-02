using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace Juan.Core
{



    /// <summary>
    /// Url参数集合
    /// </summary>
    public class QueryCollection : NameValueCollection
    {

        Encoding _Encoding = Encoding.UTF8;
        /// <summary>
        /// 编码
        /// </summary>
        public Encoding Encoding
        {


            get
            {
                return _Encoding;
            }
            set
            {
                _Encoding = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public QueryCollection()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">url地址</param>
        public QueryCollection(string url)
            : this(url, Encoding.UTF8)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="objEncoding">编码</param>
        public QueryCollection(string url, Encoding objEncoding)
        {

            _Encoding = objEncoding;
            if (url.IndexOf('?') >= 0)
            {
                string[] urlarr = url.Split('?');
                if (urlarr.Length >= 2)
                {
                    foreach (string query in urlarr[1].Split('&'))
                    {
                        string[] Params = query.Split('=');
                        if (Params.Length >= 2 && !Params[0].Equals("signaturemd5", StringComparison.OrdinalIgnoreCase) && !Params[0].Equals("signaturestamp", StringComparison.OrdinalIgnoreCase))
                        {
                            this.Add(Params[0], Params[1]);
                        }
                    }
                }
            }
            else
            {
                foreach (string query in url.Split('&'))
                {
                    string[] Params = query.Split('=');
                    if (Params.Length >= 2 && !Params[0].Equals("signaturemd5", StringComparison.OrdinalIgnoreCase) && !Params[0].Equals("signaturestamp", StringComparison.OrdinalIgnoreCase))
                    {
                        this.Add(Params[0], Params[1]);
                    }
                }
            }
        }

        /// <summary>
        /// 是否自动添加前缀[?]
        /// </summary>
        /// <param name="isEncode">是否自动编码</param>
        /// <param name="IsAutoMark">是否有参数自动加上前缀?</param>
        /// <returns></returns>
        public string CreateQuery(bool isEncode, bool IsAutoMark)
        {
            if (this.Count == 0)
            {
                return "";
            }
            StringBuilder strBuilder = new StringBuilder(256);
            if (IsAutoMark)
            {
                strBuilder.Append("?");
            }
            foreach (string key in this.AllKeys)
            {
                strBuilder.AppendFormat("{0}={1}&", key, isEncode ? this[key].EncodeUrl(_Encoding) : this[key]);
            }
            return strBuilder.ToString().TrimEnd('&');
        }





    }
}
