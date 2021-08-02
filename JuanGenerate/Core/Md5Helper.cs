using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace JuanGenerate
{
    /// <summary>
    /// Md5帮助类
    /// </summary>
    public static partial class Md5Helper
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="encryptKey">添加KEY</param>
        /// <param name="encoding">编码类型</param>
        /// <returns></returns>
        public static string MD5Encrypt(this string data, string encryptKey, Encoding encoding)
        {
            data = string.IsNullOrEmpty(data) ? "" : data;
            if (!string.IsNullOrWhiteSpace(encryptKey))
            {
                data = data + encryptKey;
            }
            byte[] newSource = data.MD5Byte(encoding);
            string byte2String = "";
            for (int i = 0; i < newSource.Length; i++)
            {
                byte2String += newSource[i].ToString("X2");
            }
            return byte2String.ToUpper();
        }

        /// <summary>
        /// MDFByte
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static byte[] MD5Byte(this string data, Encoding encoding)
        {
            byte[] Bytes = encoding.GetBytes(data);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(Bytes);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Bytes"></param>
        /// <returns></returns>
        public static byte[] MD5Byte(this   byte[] Bytes)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(Bytes);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Bytes"></param>
        /// <returns></returns>
        public static string MD5Encrypt(this byte[] Bytes)
        {
            if (Bytes == null || Bytes.Length == 0)
            {
                return "";
            }
            byte[] newSource = Bytes.MD5Byte();
            string byte2String = "";
            for (int i = 0; i < newSource.Length; i++)
            {
                byte2String += newSource[i].ToString("X2");
            }
            return byte2String.ToUpper();
        }
        /// <summary>
        ///  MD5加密[默认UTF8]
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="encryptKey">添加KEY</param>
        /// <returns></returns>
        public static string MD5Encrypt(this string data, string encryptKey)
        {
            return data.MD5Encrypt(encryptKey, System.Text.Encoding.UTF8);
        }


        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static string MD5Encrypt(this string data)
        {
            return data.MD5Encrypt("");

        }


    }
}
