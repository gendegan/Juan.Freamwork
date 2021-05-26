using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// Md5帮助类 Md5帮助类 
    /// </summary>
    public static class Md5Helper
    {
        // Methods
        public static byte[] MD5Byte(this byte[] Bytes)
        {
            MD5 md = new MD5CryptoServiceProvider();
            return md.ComputeHash(Bytes);
        }

        public static byte[] MD5Byte(this Stream objStream)
        {
            if (objStream.CanSeek && objStream.CanRead)
            {
                objStream.Seek(0L, SeekOrigin.Begin);
                byte[] buffer = new MD5CryptoServiceProvider().ComputeHash(objStream);
                objStream.Seek(0L, SeekOrigin.Begin);
                return buffer;
            }
            return objStream.ToResponseByte().MD5Byte();
        }

        public static byte[] MD5Byte(this string data, Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(data);
            MD5 md = new MD5CryptoServiceProvider();
            return md.ComputeHash(bytes);
        }

        public static string MD5Encrypt(this Stream objStream)
        {
            if (objStream == null)
            {
                return "";
            }
            byte[] buffer = objStream.MD5Byte();
            string str = "";
            for (int i = 0; i < buffer.Length; i++)
            {
                str = str + buffer[i].ToString("X2");
            }
            return str.ToUpper();
        }

        public static string MD5Encrypt(this byte[] Bytes)
        {
            if ((Bytes == null) || (Bytes.Length == 0))
            {
                return "";
            }
            byte[] buffer = Bytes.MD5Byte();
            string str = "";
            for (int i = 0; i < buffer.Length; i++)
            {
                str = str + buffer[i].ToString("X2");
            }
            return str.ToUpper();
        }

        public static string MD5Encrypt(this string data)
        {
            return data.MD5Encrypt("");
        }

        public static string MD5Encrypt(this string data, string encryptKey)
        {
            return data.MD5Encrypt(encryptKey, Encoding.UTF8);
        }

        public static string MD5Encrypt(this string data, string encryptKey, Encoding encoding)
        {
            data = string.IsNullOrEmpty(data) ? "" : data;
            if (encryptKey.IsNoNullOrWhiteSpace())
            {
                data = data + encryptKey;
            }
            byte[] buffer = data.MD5Byte(encoding);
            string str = "";
            for (int i = 0; i < buffer.Length; i++)
            {
                str = str + buffer[i].ToString("X2");
            }
            return str.ToUpper();
        }

        public static int MD5EncryptInt32(this string data)
        {
            return data.MD5EncryptInt32("");
        }

        public static int MD5EncryptInt32(this string data, string encryptKey)
        {
            return data.MD5EncryptInt32(encryptKey, Encoding.UTF8);
        }

        public static int MD5EncryptInt32(this string data, string encryptKey, Encoding encoding)
        {
            data = string.IsNullOrEmpty(data) ? "" : data;
            if (encryptKey.Length > 0)
            {
                data = data + encryptKey;
            }
            byte[] buffer = data.MD5Byte(encoding);
            int num = BitConverter.ToInt32(buffer, 0);
            int num2 = BitConverter.ToInt32(buffer, 4);
            int num3 = BitConverter.ToInt32(buffer, 8);
            int num4 = BitConverter.ToInt32(buffer, 12);
            return (((num ^ num2) ^ num3) ^ num4);
        }

        public static int MD5EncryptInt32Abs(this string data)
        {
            return data.MD5EncryptInt32Abs("");
        }

        public static int MD5EncryptInt32Abs(this string data, string encryptKey)
        {
            return data.MD5EncryptInt32Abs(encryptKey, Encoding.UTF8);
        }

        public static int MD5EncryptInt32Abs(this string data, string encryptKey, Encoding encoding)
        {
            return Math.Abs(data.MD5EncryptInt32(encryptKey, encoding));
        }

        public static long MD5EncryptInt64(this byte[] data)
        {
            byte[] buffer = data.MD5Byte();
            long num = BitConverter.ToInt64(buffer, 0);
            long num2 = BitConverter.ToInt64(buffer, 8);
            return (num ^ num2);
        }

        public static long MD5EncryptInt64(this string data)
        {
            return data.MD5EncryptInt64("");
        }

        public static long MD5EncryptInt64(this string data, string encryptKey)
        {
            return data.MD5EncryptInt64(encryptKey, Encoding.UTF8);
        }

        public static long MD5EncryptInt64(this string data, string encryptKey, Encoding encoding)
        {
            data = string.IsNullOrEmpty(data) ? "" : data;
            if (encryptKey.Length > 0)
            {
                data = data + encryptKey;
            }
            byte[] buffer = data.MD5Byte(encoding);
            long num = BitConverter.ToInt64(buffer, 0);
            long num2 = BitConverter.ToInt64(buffer, 8);
            return (num ^ num2);
        }

        public static long MD5EncryptInt64Abs(this byte[] data)
        {
            return Math.Abs(data.MD5EncryptInt64());
        }

        public static long MD5EncryptInt64Abs(this string data)
        {
            return data.MD5EncryptInt64Abs("");
        }

        public static long MD5EncryptInt64Abs(this string data, string encryptKey)
        {
            return data.MD5EncryptInt64Abs(encryptKey, Encoding.UTF8);
        }

        public static long MD5EncryptInt64Abs(this string data, string encryptKey, Encoding encoding)
        {
            return Math.Abs(data.MD5EncryptInt64(encryptKey, encoding));
        }

        public static string MD5EncryptShortCode(this string data)
        {
            return data.MD5EncryptShortCode("");
        }

        public static string MD5EncryptShortCode(this string data, string encryptKey)
        {
            return data.MD5EncryptShortCode(encryptKey, Encoding.UTF8);
        }

        public static string MD5EncryptShortCode(this string data, string encryptKey, Encoding encoding)
        {
            data = string.IsNullOrEmpty(data) ? "" : data;
            if (encryptKey.Length > 0)
            {
                data = data + encryptKey;
            }
            byte[] buffer = data.MD5Byte(encoding);
            string[] strArray = new string[] {
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p",
            "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5",
            "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
            "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
         };
            string str = string.Empty;
            for (int i = 0; i <= 1; i++)
            {
                long num2 = 0x3fffffffL & BitConverter.ToInt64(buffer, 8 * i);
                for (int j = 0; j < 6; j++)
                {
                    long num4 = 0x3dL & num2;
                    str = str + strArray[(int)((IntPtr)num4)];
                    num2 = num2 >> 5;
                }
            }
            return str;
        }

        public static uint MD5EncryptUInt32(this string data)
        {
            return data.MD5EncryptUInt32("");
        }

        public static uint MD5EncryptUInt32(this string data, string encryptKey)
        {
            return data.MD5EncryptUInt32(encryptKey, Encoding.UTF8);
        }

        public static uint MD5EncryptUInt32(this string data, string encryptKey, Encoding encoding)
        {
            data = string.IsNullOrEmpty(data) ? "" : data;
            if (encryptKey.Length > 0)
            {
                data = data + encryptKey;
            }
            byte[] buffer = data.MD5Byte(encoding);
            uint num = BitConverter.ToUInt32(buffer, 0);
            uint num2 = BitConverter.ToUInt32(buffer, 4);
            uint num3 = BitConverter.ToUInt32(buffer, 8);
            uint num4 = BitConverter.ToUInt32(buffer, 12);
            return (((num ^ num2) ^ num3) ^ num4);
        }

        public static ulong MD5EncryptUInt64(this byte[] data)
        {
            byte[] buffer = data.MD5Byte();
            ulong num = BitConverter.ToUInt64(buffer, 0);
            ulong num2 = BitConverter.ToUInt64(buffer, 8);
            return (num ^ num2);
        }

        public static ulong MD5EncryptUInt64(this string data)
        {
            return data.MD5EncryptUInt64("");
        }

        public static ulong MD5EncryptUInt64(this string data, string encryptKey)
        {
            return data.MD5EncryptUInt64(encryptKey, Encoding.UTF8);
        }

        public static ulong MD5EncryptUInt64(this string data, string encryptKey, Encoding encoding)
        {
            data = string.IsNullOrEmpty(data) ? "" : data;
            if (encryptKey.Length > 0)
            {
                data = data + encryptKey;
            }
            byte[] buffer = data.MD5Byte(encoding);
            ulong num = BitConverter.ToUInt64(buffer, 0);
            ulong num2 = BitConverter.ToUInt64(buffer, 8);
            return (num ^ num2);
        }

        public static bool SignatureCheckMD5(this string sourceData, string signatureMd5, string encryptKey)
        {
            signatureMd5.ArgumentNoNull("signatureMd5不能为空", "");
            encryptKey.ArgumentNoNull("encryptKey不能为空", "");
            char[] separator = new char[] { '|' };
            foreach (string str in encryptKey.Split(separator, StringSplitOptions.RemoveEmptyEntries))
            {
                if (sourceData.SignatureMD5(str).Equals(signatureMd5, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public static string SignatureMD5(this string data, string encryptKey)
        {
            encryptKey.ArgumentNoNull("encryptKey不能为空", "");
            char[] separator = new char[] { '|' };
            string[] strArray = encryptKey.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            return data.ToLower().MD5Encrypt(strArray[0]);
        }
    }




}
