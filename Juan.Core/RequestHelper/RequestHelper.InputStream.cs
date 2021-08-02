using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Xml;
using System.Xml.Serialization;


namespace Juan.Core
{
    /// <summary>
    /// Request包装类。
    /// </summary>
    public static partial class RequestHelper
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="IsDecompress">是否内容需要解压</param>
        /// <returns></returns>
        public static byte[] InputStreamByte(bool IsDecompress = true)
        {
            byte[] data = CurrentContext.Request.InputStream.ToByte();
            if (IsDecompress)
            {
                data = data.DecompressGZip();
            }
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encoding">编码</param>
        /// <param name="IsDecompress">是否内容需要解压</param>
        /// <returns></returns>
        public static string InputStreamString(Encoding encoding, bool IsDecompress = true)
        {
            return encoding.GetString(InputStreamByte(IsDecompress));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IsDecompress">是否内容需要解压</param>
        /// <returns></returns>
        public static string InputStreamString(bool IsDecompress = true)
        {
            return InputStreamString(System.Text.Encoding.UTF8, IsDecompress);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="encoding">编码</param>
        /// <param name="IsDecompress">是否内容需要解压</param>
        /// <returns></returns>
        public static T InputStream<T>(Encoding encoding, bool IsDecompress = true)
        {
            return InputStreamString(encoding, IsDecompress).JsonDeserialize<T>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="IsDecompress">是否内容需要解压</param>
        /// <returns></returns>
        public static T InputStream<T>(bool IsDecompress = true)
        {
            return InputStream<T>(System.Text.Encoding.UTF8, IsDecompress);
        }
    }
}
