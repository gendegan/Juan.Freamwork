using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Threading;

namespace Juan.Core
{

    /// <summary>
    /// WebHelper ���������
    /// </summary>
    public static partial class HttpUtilityHelper
    {

        #region Html����
        /// <summary>
        ///��HtmlDecode����[ EncodeHtml�����ַ���]
        /// </summary>
        /// <param name="text">�����ַ���[EncodeHtml]</param>
        /// <returns></returns>
        public static string DecodeHtml(this string text)
        {
            if (text.IsNullOrEmpty())
                return text;
            return HttpUtility.HtmlDecode(text);
        }

        /// <summary>
        ///���ַ���[HtmlEncode]����
        /// </summary>
        /// <param name="text">�ַ���</param>
        /// <returns></returns>
        public static string EncodeHtml(this string text)
        {
            if (text.IsNullOrEmpty())
                return text;

            return HttpUtility.HtmlEncode(text);
        }

        #endregion

        #region Url����

        /// <summary>
        /// ���ַ���[EncodeUrl]����[Ĭ��Encoding.UTF8]
        /// </summary>
        /// <param name="text">�ַ���</param>
        /// <returns></returns>
        public static string EncodeUrl(this string text)
        {
            if (text.IsNullOrEmpty())
                return text;

            return text.EncodeUrl(Encoding.UTF8);
        }



        /// <summary>
        ///���ַ���[EncodeUrl]����
        /// </summary>
        /// <param name="text">Ҫ������ַ���</param>
        /// <param name="objEncoding">�������</param>
        /// <returns></returns>
        public static string EncodeUrl(this string text, Encoding objEncoding)
        {
            if (text.IsNullOrEmpty())
                return text;

            return HttpUtility.UrlEncode(text, objEncoding);
        }



        /// <summary>
        /// ��EncodeUrl����[Ĭ��Encoding.UTF8]
        /// </summary>
        /// <param name="text">�ַ���[[Ĭ��Encoding.UTF8]]</param>
        /// <returns></returns>
        public static string DecodeUrl(this string text)
        {
            if (text.IsNullOrEmpty())
                return text;

            return text.DecodeUrl(Encoding.UTF8);
        }

        /// <summary>
        /// ��EncodeUrl����
        /// </summary>
        /// <param name="text">�ַ���</param>
        /// <param name="objEncoding">�������</param>
        /// <returns></returns>
        public static string DecodeUrl(this string text, Encoding objEncoding)
        {
            if (text.IsNullOrEmpty())
                return text;

            return HttpUtility.UrlDecode(text, objEncoding);
        }

        #endregion


        /// <summary>
        /// ��Rfc3986��׼��UrlEncode���б���
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
