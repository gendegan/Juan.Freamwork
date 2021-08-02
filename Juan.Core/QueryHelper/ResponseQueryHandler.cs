using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Juan.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class ResponseQueryHandler : SortedDictionary<string, string>
    {


        /// <summary>
        /// 
        /// </summary>
        protected HttpContext HttpContext;

        /// <summary>
        /// 获取页面提交的get和post参数
        /// </summary>
        /// <param name="httpContext"></param>
        public ResponseQueryHandler(HttpContext httpContext)
        {

            this.HttpContext = httpContext ?? HttpContext.Current;
            NameValueCollection collection;
            //post data
            if (this.HttpContext.Request.HttpMethod == "POST")
            {
                collection = this.HttpContext.Request.Form;
                foreach (string k in collection)
                {
                    string v = (string)collection[k];
                    this.Add(k, v);
                }
            }
            //query string
            collection = this.HttpContext.Request.QueryString;
            foreach (string k in collection)
            {
                string v = (string)collection[k];
                this.Add(k, v);
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public new void Add(string key, string value)
        {
            if (this.ContainsKey(key))
            {
                this.Remove(key);
            }
            base.Add(key, value);
        }
    }

}
