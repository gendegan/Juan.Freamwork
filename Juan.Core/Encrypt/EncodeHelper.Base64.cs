using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Juan.Core
{
    /// <summary>
    /// 编码帮助
    /// </summary>
    public static partial class EncodeHelper
    {



        /// <summary>
        /// 原始Base64编码[普通]
        /// </summary>
        /// <param name="source">byte数组</param>
        /// <returns></returns>
        public static byte[] EncodeBase64Byte(this byte[] source)
        {
            if ((source == null) || (source.Length == 0))
            {
                throw new ArgumentException("source is not valid");
            }

            ToBase64Transform tb64 = new ToBase64Transform();
            MemoryStream stm = new MemoryStream();
            int pos = 0;
            byte[] buff;

            try
            {
                while (pos + 3 < source.Length)
                {
                    buff = tb64.TransformFinalBlock(source, pos, 3);
                    stm.Write(buff, 0, buff.Length);
                    pos += 3;
                }

                buff = tb64.TransformFinalBlock(source, pos, source.Length - pos);
                stm.Write(buff, 0, buff.Length);

                return stm.ToArray();
            }
            catch (Exception ex)
            {
                throw  ;
            }
            finally
            {
                if (stm != null)
                    stm.Close();
            }
        }

        /// <summary>
        /// 原始base64解码:[普通]
        /// </summary>
        /// <param name="source">byte数组</param>
        /// <returns></returns>
        public static byte[] DecodeBase64Byte(this byte[] source)
        {
            if ((source == null) || (source.Length == 0))
            {
                throw new ArgumentException("source is not valid");
            }
            FromBase64Transform fb64 = new FromBase64Transform();
            MemoryStream stm = new MemoryStream();
            int pos = 0;
            byte[] buff;

            try
            {
                while (pos + 4 < source.Length)
                {
                    buff = fb64.TransformFinalBlock(source, pos, 4);
                    stm.Write(buff, 0, buff.Length);
                    pos += 4;
                }

                buff = fb64.TransformFinalBlock(source, pos, source.Length - pos);
                stm.Write(buff, 0, buff.Length);
                return stm.ToArray();
            }
            
            finally
            {
                if (stm != null)
                    stm.Close();
            }
        }

        /// <summary>
        /// 改进后的Base64编码:[改进]
        /// </summary>
        /// <param name="source">byte数组</param>
        /// <returns></returns>
        public static string EncodeEnhancedBase64(this byte[] source)
        {
            byte[] dest;

            dest = EncodeBase64Byte(source);

            string ret;
            ret = Encoding.UTF8.GetString(dest);
            ret = ret.Replace("+", "~");
            ret = ret.Replace("/", "@");
            ret = ret.Replace("=", "$");
            return ret;

        }

        /// <summary>
        /// 改进后的Base64进行编码:[改进]
        /// </summary>
        /// <param name="source">要编码的源</param>
        /// <param name="objEncoding">编码类型</param>
        /// <returns></returns>
        public static string EncodeEnhancedBase64(this string source, Encoding objEncoding)
        {
            byte[] dest = objEncoding.GetBytes(source);

            return dest.EncodeEnhancedBase64();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="objEncoding">编码</param>
        /// <returns></returns>
        public static string EncodeInterfaceBase64(this string source, Encoding objEncoding)
        {
            byte[] dest = objEncoding.GetBytes(source);

            return Encoding.UTF8.GetString(dest);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string EncodeInterfaceBase64(this string source)
        {
            return source.EncodeInterfaceBase64(Encoding.UTF8);

        }
        /// <summary>
        /// 改进后的Base64进行编码[改进,默认UTF8]
        /// </summary>
        /// <param name="source">要编码的源</param>
        /// <returns></returns>
        public static string EncodeEnhancedBase64(this string source)
        {
            return source.EncodeEnhancedBase64(Encoding.UTF8);

        }



        /// <summary>
        /// 改进的Base64字符串进行解码[改进]
        /// </summary>
        /// <param name="source">要解码字符串</param>
        /// <returns></returns>
        public static byte[] DecodeEnhancedBase64Byte(this string source)
        {
            string dest;
            dest = source.Replace("~", "+");
            dest = dest.Replace("@", "/");
            dest = dest.Replace("$", "=");

            byte[] ret;
            ret = DecodeBase64Byte(Encoding.UTF8.GetBytes(dest));

            return ret;
        }

        #region Base64[改进]

        /// <summary>
        ///  改进的Base64字符串进行解码[改进]
        /// </summary>
        /// <param name="source">要解码字符串</param>
        /// <param name="objEncoding">编码</param>
        /// <returns></returns>
        public static string DecodeEnhancedBase64(this string source, Encoding objEncoding)
        {
            return objEncoding.GetString(source.DecodeEnhancedBase64Byte());
        }

        /// <summary>
        /// 接口
        /// </summary>
        /// <param name="source"></param>
        /// <param name="objEncoding">编码</param>
        /// <returns></returns>
        public static string DecodeInterfaceBase64(this string source, Encoding objEncoding)
        {
            return objEncoding.GetString(DecodeBase64Byte(Encoding.UTF8.GetBytes(source)));
        }
        /// <summary>
        ///  接口
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string DecodeInterfaceBase64(this string source)
        {
            return DecodeInterfaceBase64(source, Encoding.UTF8);
        }

        /// <summary>
        /// 改进的Base64字符串进行解码[改进][普通UTF8]
        /// </summary>
        /// <param name="source">要解码字符串</param>
        /// <returns></returns>
        public static string DecodeEnhancedBase64(this string source)
        {
            return DecodeEnhancedBase64(source, Encoding.UTF8);

        }
        #endregion



        #region 普通Base64
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string EncodeBase64(this byte[] source)
        {

            return source.EncodeBase64(Encoding.UTF8);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string EncodeBase64(this byte[] source, Encoding encoding)
        {
            byte[] dest;

            dest = EncodeBase64Byte(source);

            string ret;
            ret = encoding.GetString(dest);
            return ret;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static byte[] DecodeBase64Byte(this string source)
        {


            return source.DecodeBase64Byte(Encoding.UTF8);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static byte[] DecodeBase64Byte(this string source, Encoding encoding)
        {


            byte[] ret;
            ret = DecodeBase64Byte(encoding.GetBytes(source));
            return ret;
        }
        /// <summary>
        /// 将字符串编码为Base64字符串[普通UTF8]
        /// </summary>
        /// <param name="str">要编码的字符串</param>
        public static string EncodeBase64(this string str)
        {
            return EncodeBase64(str, Encoding.UTF8);
        }

        /// <summary>
        /// 将字符串编码为Base64字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string EncodeBase64(this string str, Encoding encoding)
        {
            byte[] barray;

            barray = encoding.GetBytes(str);
            return Convert.ToBase64String(barray);
        }

        /// <summary>
        /// 将Base64字符串解码为字符串[普通[UTF8]]
        /// </summary>
        /// <param name="str">要解码的字符串</param>
        public static string DecodeBase64(this string str)
        {
            return DecodeBase64(str, Encoding.UTF8);
        }
        /// <summary>
        ///  将Base64字符串解码为字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string DecodeBase64(this string str, Encoding encoding)
        {
            byte[] barray;
            barray = Convert.FromBase64String(str);

            return encoding.GetString(barray);
        }

        #endregion
    }
}
