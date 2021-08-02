using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Threading;

namespace Juan.Core
{

    /// <summary>
    /// WebHelper ±àÂëÓë½âÂë
    /// </summary>
    public static partial class HttpUtilityHelper
    {

        #region Html±àÂë
        /// <summary>
        ///¶ÔHtmlDecode½âÂë[ EncodeHtml±àÂë×Ö·û´®]
        /// </summary>
        /// <param name="text">±àÂë×Ö·û´®[EncodeHtml]</param>
        /// <returns></returns>
        public static string DecodeHtml(this string text)
        {
            if (text.IsNullOrEmpty())
                return text;
            return HttpUtility.HtmlDecode(text);
        }

        /// <summary>
        ///¶Ô×Ö·û´®[HtmlEncode]±àÂë
        /// </summary>
        /// <param name="text">×Ö·û´®</param>
        /// <returns></returns>
        public static string EncodeHtml(this string text)
        {
            if (text.IsNullOrEmpty())
                return text;

            return HttpUtility.HtmlEncode(text);
        }

        #endregion

        #region Url±àÂë

        /// <summary>
        /// ¶Ô×Ö·û´®[EncodeUrl]±àÂë[Ä¬ÈÏEncoding.UTF8]
        /// </summary>
        /// <param name="text">×Ö·û´®</param>
        /// <returns></returns>
        public static string EncodeUrl(this string text)
        {
            if (text.IsNullOrEmpty())
                return text;

            return text.EncodeUrl(Encoding.UTF8);
        }



        /// <summary>
        ///¶Ô×Ö·û´®[EncodeUrl]±àÂë
        /// </summary>
        /// <param name="text">Òª±àÂëµÄ×Ö·û´®</param>
        /// <param name="objEncoding">±àÂëÂë±ð</param>
        /// <returns></returns>
        public static string EncodeUrl(this string text, Encoding objEncoding)
        {
            if (text.IsNullOrEmpty())
                return text;

            return HttpUtility.UrlEncode(text, objEncoding);
        }



        /// <summary>
        /// ¶ÔEncodeUrl½âÂë[Ä¬ÈÏEncoding.UTF8]
        /// </summary>
        /// <param name="text">×Ö·û´®[[Ä¬ÈÏEncoding.UTF8]]</param>
        /// <returns></returns>
        public static string DecodeUrl(this string text)
        {
            if (text.IsNullOrEmpty())
                return text;

            return text.DecodeUrl(Encoding.UTF8);
        }

        /// <summary>
        /// ¶ÔEncodeUrl½âÂë
        /// </summary>
        /// <param name="text">×Ö·û´®</param>
        /// <param name="objEncoding">±àÂëÂë±ð</param>
        /// <returns></returns>
        public static string DecodeUrl(this string text, Encoding objEncoding)
        {
            if (text.IsNullOrEmpty())
                return text;

            return HttpUtility.UrlDecode(text, objEncoding);
        }

        #endregion


        /// <summary>
        /// ÓÃRfc3986±ê×¼¶ÔUrlEncode½øÐÐ±àÂë
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string EncodeUrlRaw(this string text)
        {
            if (text.IsNullOrEmpty())
                return text;

            return Uri.EscapeDataString(text);
        }

    }


}
