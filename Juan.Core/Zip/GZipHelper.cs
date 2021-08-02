using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    /// <summary>
    /// GZip帮助类
    /// </summary>
    public static class GZipHelper
    {


        /// <summary>
        ///  压缩
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string CompressGZipBase64(this string value)
        {
            return CompressGZipBase64(value, System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="objEncoding">编码</param>
        /// <returns></returns>
        public static string CompressGZipBase64(this string value, Encoding objEncoding)
        {
            if (string.IsNullOrEmpty(value) || value.Length == 0)
            {
                return "";
            }
            else
            {
                byte[] rawData = objEncoding.GetBytes(value.ToString());
                byte[] zippedData = CompressGZip(rawData);
                return (string)(Convert.ToBase64String(zippedData));
            }

        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string DecompressGZipBase64(this string value)
        {

            return DecompressGZipBase64(value, System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// /// 解压
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="objEncoding">编码</param>
        /// <returns></returns>
        public static string DecompressGZipBase64(this string value, Encoding objEncoding)
        {
            if (string.IsNullOrEmpty(value) || value.Length == 0)
            {
                return "";
            }
            else
            {
                byte[] zippedData = Convert.FromBase64String(value.ToString());
                return (string)(objEncoding.GetString(DecompressGZip(zippedData)));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static byte[] CompressGZipByte(this string value)
        {
            return CompressGZipByte(value, System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="objEncoding">编码</param>
        /// <returns></returns>
        public static byte[] CompressGZipByte(this string value, Encoding objEncoding)
        {
            if (string.IsNullOrEmpty(value) || value.Length == 0)
            {
                return new byte[0];
            }
            else
            {
                byte[] rawData = objEncoding.GetBytes(value.ToString());
                byte[] zippedData = CompressGZip(rawData);
                return zippedData;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static byte[] DecompressGZipByte(this Stream value)
        {

            return DecompressGZip(value).ToResponseByte();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static Stream CompressGZipStream(this string value)
        {
            return CompressGZipStream(value, System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="objEncoding">编码</param>
        /// <returns></returns>
        public static Stream CompressGZipStream(this string value, Encoding objEncoding)
        {
            return CompressGZipByte(value, objEncoding).ToStream();
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string DecompressGZipString(this Stream value)
        {
            return DecompressGZipString(value, System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="objEncoding">编码</param>
        /// <returns></returns>
        public static string DecompressGZipString(this Stream value, Encoding objEncoding)
        {
            return objEncoding.GetString(DecompressGZip(value).ToResponseByte());
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objStream"></param>
        /// <returns></returns>
        public static Stream CompressGZip(this Stream objStream)
        {

            objStream.Seek(0, SeekOrigin.Begin);
            GZipStream compressedzipStream = new GZipStream(objStream, CompressionMode.Compress);
            objStream.Seek(0, SeekOrigin.Begin);
            return compressedzipStream;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objStream"></param>
        /// <returns></returns>
        public static Stream DecompressGZip(this Stream objStream)
        {

            objStream.Seek(0, SeekOrigin.Begin);
            GZipStream compressedzipStream = new GZipStream(objStream, CompressionMode.Decompress);
            objStream.Seek(0, SeekOrigin.Begin);
            return compressedzipStream;
        }

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static byte[] CompressGZip(this byte[] array)
        {
            MemoryStream ms = new MemoryStream();
            GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Compress, true);
            compressedzipStream.Write(array, 0, array.Length);
            compressedzipStream.Close();
            return ms.ToArray();
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static byte[] DecompressGZip(this byte[] array)
        {
            MemoryStream ms = new MemoryStream(array);
            GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Decompress);
            MemoryStream outBuffer = new MemoryStream();
            byte[] block = new byte[1024];
            while (true)
            {
                int bytesRead = compressedzipStream.Read(block, 0, block.Length);
                if (bytesRead <= 0)
                    break;
                else
                    outBuffer.Write(block, 0, bytesRead);
            }
            compressedzipStream.Close();
            return outBuffer.ToArray();
        }

    }
}
