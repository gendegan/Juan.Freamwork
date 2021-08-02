using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace Juan.Core
{
    /// <summary>
    ///  Byte帮助类
    /// </summary>
    public static partial class ByteHelper
    {

        private static char[] hexDigits;

        /// <summary>
        /// 初始化
        /// </summary>
        static ByteHelper()
        {
            hexDigits = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        }




        /// <summary>
        /// 将字节数组转换成16进制字符串
        /// </summary>
        /// <param name="bytes">转换的字节数组</param>
        /// <returns></returns>
        public static string ToHexText(this byte[] bytes)
        {
            if (bytes == null)
            {
                return "";
            }
            char[] chars = new char[bytes.Length * 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                int b = bytes[i];
                chars[i * 2] = hexDigits[b >> 4];
                chars[(i * 2) + 1] = hexDigits[b & 15];
            }

            return new string(chars);
        }

        /// <summary>
        /// 字节转成流
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Stream ToStream(this byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary>
        /// 字符转图片
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Image ToImage(this byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                return ms.ToImage();
            }

        }
    }
}
