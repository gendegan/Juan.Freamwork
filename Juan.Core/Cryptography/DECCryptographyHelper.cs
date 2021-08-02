using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Juan.Core
{
    /// <summary>
    /// 密钥必须为8位英文
    /// </summary>
    public static partial class CryptographyHelper
    {
        /// <summary>
        /// DEC加密
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string EncryptDES(this string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();　//把字符串放到byte数组中
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);

            des.Key = Encoding.UTF8.GetBytes(sKey);
            des.IV = Encoding.UTF8.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }
        /// <summary>
        /// DEC解密
        /// </summary>
        /// <param name="pToDecrypt"></param>
        /// <param name="sKey"></param>
        /// <returns></returns>
        public static string DecryptDES(this string pToDecrypt, string sKey)
        {

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            des.Key = Encoding.UTF8.GetBytes(sKey);
            des.IV = Encoding.UTF8.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.UTF8.GetString(ms.ToArray());

        }


       /// <summary>
       /// 
       /// </summary>
       /// <param name="str"></param>
       /// <param name="EncryptKey"></param>
       /// <returns></returns>
        public static  string EncryptBase64DES(this string str, string EncryptKey)
        {
            DESCryptoServiceProvider desc = new DESCryptoServiceProvider();//实例化加密解密对象
            desc.Key = Encoding.UTF8.GetBytes(EncryptKey);
            desc.IV = Encoding.UTF8.GetBytes(EncryptKey);
            byte[] data = Encoding.UTF8.GetBytes(str);//定义字节数组、用来存储要解密的字符串
            MemoryStream mstream = new MemoryStream();//实例化内存流对象
            //使用内存流实例化加密流对象
            CryptoStream cstream = new CryptoStream(mstream, desc.CreateEncryptor(), CryptoStreamMode.Write);
            cstream.Write(data, 0, data.Length);//想加密流中写入数据
            cstream.FlushFinalBlock();//释放加密流
        
            return Convert.ToBase64String(mstream.ToArray());//返回加密后的字符串
        }
        /// <summary>
        /// 字符串解密
        /// </summary>
        /// <param name="str">要解密的字符串</param>
        /// <param name="EncryptKey">EncryptKey</param>
        /// <returns></returns>
        public static string DecryptBase64DES(this string str, string EncryptKey)
        {
            DESCryptoServiceProvider desc = new DESCryptoServiceProvider();//实例化加密、解密对象
            desc.Key = Encoding.UTF8.GetBytes(EncryptKey);
            desc.IV = Encoding.UTF8.GetBytes(EncryptKey);
            byte[] data = Convert.FromBase64String(str);//定义字节数组用来存储密钥
            MemoryStream mstream = new MemoryStream();//实例化内存流对象
            //使用内存流实例化解密对象
            CryptoStream cstream = new CryptoStream(mstream, desc.CreateDecryptor(), CryptoStreamMode.Write);
            cstream.Write(data, 0, data.Length);//向解密流中写入数据
            cstream.FlushFinalBlock();//释放解密流
            return Encoding.UTF8.GetString(mstream.ToArray());
        }


    }
}
