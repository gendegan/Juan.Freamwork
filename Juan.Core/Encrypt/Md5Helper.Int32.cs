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
        public static int MD5EncryptInt32Abs(this string data, string encryptKey, Encoding encoding)
        {
            return System.Math.Abs(data.MD5EncryptInt32(encryptKey, encoding));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encryptKey">密钥</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static int MD5EncryptInt32(this string data, string encryptKey, Encoding encoding)
        {
            data = string.IsNullOrEmpty(data) ? "" : data;
            if (encryptKey.Length > 0)
            {
                data = data + encryptKey;
            }
            byte[] newSource = data.MD5Byte(encoding);

            int hashCode1 = BitConverter.ToInt32(newSource, 0);
            int hashCode2 = BitConverter.ToInt32(newSource, 4);
            int hashCode3 = BitConverter.ToInt32(newSource, 8);
            int hashCode4 = BitConverter.ToInt32(newSource, 12);
            return hashCode1 ^ hashCode2 ^ hashCode3 ^ hashCode4;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encryptKey">密钥</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static UInt32 MD5EncryptUInt32(this string data, string encryptKey, Encoding encoding)
        {
            data = string.IsNullOrEmpty(data) ? "" : data;
            if (encryptKey.Length > 0)
            {
                data = data + encryptKey;
            }
            byte[] newSource = data.MD5Byte(encoding);

            UInt32 hashCode1 = BitConverter.ToUInt32(newSource, 0);
            UInt32 hashCode2 = BitConverter.ToUInt32(newSource, 4);
            UInt32 hashCode3 = BitConverter.ToUInt32(newSource, 8);
            UInt32 hashCode4 = BitConverter.ToUInt32(newSource, 12);
            return hashCode1 ^ hashCode2 ^ hashCode3 ^ hashCode4;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encryptKey">密钥</param>
        /// <returns></returns>
        public static Int32 MD5EncryptInt32(this string data, string encryptKey)
        {
            return data.MD5EncryptInt32(encryptKey, System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Int32 MD5EncryptInt32(this string data)
        {
            return data.MD5EncryptInt32("");

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encryptKey">密钥</param>
        /// <returns></returns>
        public static UInt32 MD5EncryptUInt32(this string data, string encryptKey)
        {
            return data.MD5EncryptUInt32(encryptKey, System.Text.Encoding.UTF8);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static UInt32 MD5EncryptUInt32(this string data)
        {
            return data.MD5EncryptUInt32("");

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encryptKey">密钥</param>
        /// <returns></returns>
        public static Int32 MD5EncryptInt32Abs(this string data, string encryptKey)
        {
            return data.MD5EncryptInt32Abs(encryptKey, System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Int32 MD5EncryptInt32Abs(this string data)
        {
            return data.MD5EncryptInt32Abs("");
        }

    }
}
