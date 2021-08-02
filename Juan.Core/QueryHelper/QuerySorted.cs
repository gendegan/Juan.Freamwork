using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace Juan.Core
{
    /// <summary>
    /// 
    /// </summary>
    public partial class QuerySorted : SortedDictionary<string, string>
    {

        /// <summary>
        /// 
        /// </summary>
        public QuerySorted()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        public QuerySorted(IDictionary<string, string> dictionary)
            : base(dictionary)
        {
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public QuerySorted(NameValueCollection collection)
        {

            foreach (string k in collection)
            {
                string v = (string)collection[k];
                this.Add(k, v);
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public new void Add(string key, string value)
        {
            this[key] = value;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public QuerySorted(string url)
        {

            if (url.IndexOf('?') >= 0)
            {
                string[] urlarr = url.Split('?');
                if (urlarr.Length >= 2)
                {
                    foreach (string query in urlarr[1].Split('&'))
                    {
                        string[] Params = query.Split('=');
                        if (Params.Length >= 2)
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
                    if (Params.Length >= 2)
                    {
                        this.Add(Params[0], Params[1]);
                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public void Add(NameValueCollection collection)
        {
            foreach (string key in collection)
            {
                string value = (string)collection[key];
                this.Add(key, value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dict"></param>
        public void Add(IDictionary<string, string> dict)
        {
            foreach (var item in dict)
            {
                this.Add(item.Key, item.Value);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        public QuerySorted(HttpContext httpContext)
        {
            //post data
            if (httpContext.Request.HttpMethod == "POST")
            {
                this.Add(httpContext.Request.Form);
            }

            this.Add(httpContext.Request.QueryString);

        }


    }
}
