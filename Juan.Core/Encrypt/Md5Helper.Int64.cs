using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    public static partial class Md5Helper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encryptKey">密钥</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static Int64 MD5EncryptInt64(this string data, string encryptKey, Encoding encoding)
        {
            data = string.IsNullOrEmpty(data) ? "" : data;
            if (encryptKey.Length > 0)
            {
                data = data + encryptKey;
            }
            byte[] newSource = data.MD5Byte(encoding);
            Int64 hashCodeStart = BitConverter.ToInt64(newSource, 0);
            Int64 hashCodeEnd = BitConverter.ToInt64(newSource, 8);
            return hashCodeStart ^ hashCodeEnd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encryptKey">密钥</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static ulong MD5EncryptUInt64(this string data, string encryptKey, Encoding encoding)
        {
            data = string.IsNullOrEmpty(data) ? "" : data;
            if (encryptKey.Length > 0)
            {
                data = data + encryptKey;
            }
            byte[] newSource = data.MD5Byte(encoding);
            ulong hashCodeStart = BitConverter.ToUInt64(newSource, 0);
            ulong hashCodeEnd = BitConverter.ToUInt64(newSource, 8);
            return hashCodeStart ^ hashCodeEnd;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encryptKey">密钥</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static Int64 MD5EncryptInt64Abs(this string data, string encryptKey, Encoding encoding)
        {
            return System.Math.Abs(data.MD5EncryptInt64(encryptKey, encoding));
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encryptKey">密钥</param>
        /// <returns></returns>
        public static Int64 MD5EncryptInt64(this string data, string encryptKey)
        {
            return data.MD5EncryptInt64(encryptKey, System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Int64 MD5EncryptInt64(this string data)
        {
            return data.MD5EncryptInt64("");

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encryptKey">密钥</param>
        /// <returns></returns>
        public static UInt64 MD5EncryptUInt64(this string data, string encryptKey)
        {
            return data.MD5EncryptUInt64(encryptKey, System.Text.Encoding.UTF8);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static UInt64 MD5EncryptUInt64(this string data)
        {
            return data.MD5EncryptUInt64("");

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encryptKey">密钥</param>
        /// <returns></returns>
        public static Int64 MD5EncryptInt64Abs(this string data, string encryptKey)
        {
            return data.MD5EncryptInt64Abs(encryptKey, System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Int64 MD5EncryptInt64Abs(this string data)
        {
            return data.MD5EncryptInt64Abs("");
        }

    }
}
