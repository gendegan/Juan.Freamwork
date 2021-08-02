using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Juan.Core
{
    /// <summary>
    /// 地址帮助类
    /// </summary>
    public static class UrlHelper
    {
        /// <summary>
        /// 解析链接中的参数
        /// </summary>
        /// <param name="url">需要解析的字符串</param>
        /// <param name="key">参数</param>
        /// <returns></returns>
        public static string GetUrlParameter(this string url, string key)
        {
            if (String.IsNullOrWhiteSpace(url) || String.IsNullOrWhiteSpace(key))
            {
                return "";
            }
            try
            {
                if (url.IndexOf('?') >= 0)
                {
                    url = url.Substring(url.IndexOf('?'));
                }
                url = url.TrimStart('?', '&');
                url = "&" + url;
                Match objMatch = Regex.Match(url, "&" + key + "=([^&]*)", RegexOptions.IgnoreCase);
                if (objMatch.Success)
                {
                    return objMatch.Value.Substring(2 + key.Length);
                }
                else
                {
                    return "";
                }

            }
            catch
            {

            }

            return "";
        }
        /// <summary>
        /// 添加地址参数
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="addParamter"></param>
        /// <param name="value">值</param>
        /// <param name="existsReplace"></param>
        /// <returns></returns>
        public static string AddUrlParameter(this string url, string addParamter, object value, bool existsReplace = true)
        {
            if (url.IndexOf(addParamter + "=") != -1)
            {
                if (!existsReplace)
                {
                    return url;
                }
                string urlPath = "";
                string query = url;
                if (url.IndexOf('?') >= 0)
                {
                    urlPath = url.Split('?')[0];
                    query = url.Split('?')[1];
                }
                string newQuery = "";
                foreach (string item in query.Split('&'))
                {
                    if (item.IndexOf(addParamter + "=") == -1)
                    {
                        newQuery += item + "&";
                    }
                }

                newQuery = newQuery.TrimEnd('&');
                if (string.IsNullOrWhiteSpace(newQuery))
                {
                    url = urlPath;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(urlPath))
                    {
                        url = newQuery;
                    }
                    else
                    {
                        url = urlPath + "?" + newQuery;
                    }

                }


            }


            string Query = string.Format("{0}={1}", addParamter, value.ToString());
            if (url.IndexOf('?') != -1)
                url += "&" + Query;
            else
                url += "?" + Query;
            return url;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public static string EncodeUrlQuery(this string url)
        {
            string[] urlarr = url.Split('?');
            if (urlarr.Length > 1)
            {
                QueryCollection objQueryStringHelper = new QueryCollection(urlarr[1]);
                return urlarr[0] + objQueryStringHelper.CreateQuery(true, true);
            }
            return url;

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public static string BuildQueryParams(this IDictionary<string, ICollection<string>> parameters)
        {
            if (parameters == null)
            {
                return null;
            }
            StringBuilder stringBuilder = new StringBuilder();
            int num = 0;
            foreach (KeyValuePair<string, ICollection<string>> current in parameters)
            {
                if (num != 0)
                {
                    stringBuilder.Append('&');
                }
                if (current.Value != null)
                {
                    if (current.Value.Count == 1)
                    {
                        stringBuilder.AppendFormat("{0}={1}", HttpUtility.UrlEncode(current.Key), HttpUtility.UrlEncode(current.Value.Take(1).Single<string>()));
                    }
                    else
                    {
                        int num2 = 0;
                        foreach (string current2 in current.Value)
                        {
                            if (num2 > 0)
                            {
                                stringBuilder.Append('&');
                            }
                            stringBuilder.AppendFormat("{0}={1}", HttpUtility.UrlEncode(current.Key), HttpUtility.UrlEncode(current2));
                            num2++;
                        }
                    }
                }
                num++;
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static IDictionary<string, ICollection<string>> CreateQueryParam(this string key, params string[] values)
        {
            IDictionary<string, ICollection<string>> options = new Dictionary<string, ICollection<string>>();
            return options.AddQueryParam(key, values);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static IDictionary<string, ICollection<string>> CreateQueryParam(this string key, ICollection<string> value)
        {
            IDictionary<string, ICollection<string>> options = new Dictionary<string, ICollection<string>>();
            return options.AddQueryParam(key, value);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static IDictionary<string, ICollection<string>> AddQueryParam(this IDictionary<string, ICollection<string>> options, string key, params string[] values)
        {
            List<string> objValuesList = new List<string>();
            if (values == null || values.Length == 0)
            {
            }
            foreach (string value in values)
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    objValuesList.Add(value);

                }
            }
            options.AddQueryParam(key, objValuesList);
            return options;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="key"></param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static IDictionary<string, ICollection<string>> AddQueryParam(this IDictionary<string, ICollection<string>> options, string key, ICollection<string> value)
        {
            if (!options.ContainsKey(key) && value != null && value.Count > 0)
            {
                options[key] = value;
            }
            return options;
        }

    }
}
