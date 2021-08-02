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
        public static string MD5EncryptHex62(this string data, string encryptKey, Encoding encoding)
        {
            data = string.IsNullOrEmpty(data) ? "" : data;
            if (encryptKey.Length > 0)
            {
                data = data + encryptKey;
            }
            byte[] newSource = data.MD5Byte(encoding);
            ulong hashCodeStart = BitConverter.ToUInt64(newSource, 0);
            ulong hashCodeEnd = BitConverter.ToUInt64(newSource, 8);
            UInt64 value = hashCodeStart ^ hashCodeEnd;
            return value.IntToHex62();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encryptKey">密钥</param>
        /// <returns></returns>
        public static string MD5EncryptHex62(this string data, string encryptKey)
        {
            return data.MD5EncryptHex62(encryptKey, System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string MD5EncryptHex62(this string data)
        {
            return data.MD5EncryptHex62("");

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string MD5EncryptHex62(this byte[] data)
        {
            byte[] newSource = data.MD5Byte();
            ulong hashCodeStart = BitConverter.ToUInt64(newSource, 0);
            ulong hashCodeEnd = BitConverter.ToUInt64(newSource, 8);
            UInt64 value = hashCodeStart ^ hashCodeEnd;
            return value.IntToHex62();
        }

    }
}
