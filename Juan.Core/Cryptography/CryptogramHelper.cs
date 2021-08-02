using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{

    /// <summary>
    /// 加密帮忙类
    /// </summary>
    public static class CryptogramHelper
    {
        // Fields
        private static TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        private static readonly byte[] pIV = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static readonly byte[] pKEY = new byte[] { 
        0xda, 0xef, 0xe3, 0x16, 0x1f, 0x35, 120, 0xe0, 0xdf, 0xdf, 0xab, 210, 140, 0x9e, 0x2f, 0x56, 
        0x7a, 0x27, 0xee, 0x5f, 0x2f, 0x8a, 0x2c, 0x9b
     };

        // Methods
        static CryptogramHelper()
        {
            des.Mode = CipherMode.CBC;
            des.Padding = PaddingMode.PKCS7;
        }

        /// <summary>
        /// 字符生成十六进制字 符串
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static string ByteArrayToHexString(byte[] buf)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < buf.Length; i++)
            {
                builder.Append((buf[i].ToString("X").Length == 2) ? buf[i].ToString("X") : ("0" + buf[i].ToString("X")));
            }
            return builder.ToString();
        }

        private static byte Chr2Hex(string chr)
        {
            switch (chr)
            {
                case "0":
                    return 0;

                case "1":
                    return 1;

                case "2":
                    return 2;

                case "3":
                    return 3;

                case "4":
                    return 4;

                case "5":
                    return 5;

                case "6":
                    return 6;

                case "7":
                    return 7;

                case "8":
                    return 8;

                case "9":
                    return 9;

                case "A":
                    return 10;

                case "B":
                    return 11;

                case "C":
                    return 12;

                case "D":
                    return 13;

                case "E":
                    return 14;

                case "F":
                    return 15;
            }
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static byte[] ComputeHash(byte[] buf)
        {
            return ((HashAlgorithm)CryptoConfig.CreateFromName("SHA1")).ComputeHash(buf);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ComputeHashString(string s)
        {
            return ToBase64String(ComputeHash(StringToByteArray(s)));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static string ByteArrayToString(byte[] buf)
        {
            return Encoding.UTF8.GetString(buf);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] StringToByteArray(string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="KEY"></param>
        /// <param name="IV"></param>
        /// <param name="TobeDecrypted"></param>
        /// <param name="Decrypted"></param>
        /// <returns></returns>
        public static bool Decrypt(byte[] KEY, byte[] IV, byte[] TobeDecrypted, out byte[] Decrypted)
        {
            Decrypted = null;
            
                int num;
                byte[] rgbIV = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 };
                for (num = 0; num < 8; num++)
                {
                    rgbIV[num] = IV[num];
                }
                byte[] rgbKey = new byte[] { 
                0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7, 
                0, 1, 2, 3, 4, 5, 6, 7
             };
                for (num = 0; num < 0x18; num++)
                {
                    rgbKey[num] = KEY[num];
                }
                Decrypted = des.CreateDecryptor(rgbKey, rgbIV).TransformFinalBlock(TobeDecrypted, 0, TobeDecrypted.Length);
                des.Clear();
            
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="KEY"></param>
        /// <param name="IV"></param>
        /// <param name="TobeEncrypted"></param>
        /// <param name="Encrypted"></param>
        /// <returns></returns>
        public static bool Encrypt(byte[] KEY, byte[] IV, byte[] TobeEncrypted, out byte[] Encrypted)
        {
            Encrypted = null;
           
                int num;
                byte[] rgbIV = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 };
                for (num = 0; num < 8; num++)
                {
                    rgbIV[num] = IV[num];
                }
                byte[] rgbKey = new byte[] { 
                0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7, 
                0, 1, 2, 3, 4, 5, 6, 7
             };
                for (num = 0; num < 0x18; num++)
                {
                    rgbKey[num] = KEY[num];
                }
                Encrypted = des.CreateEncryptor(rgbKey, rgbIV).TransformFinalBlock(TobeEncrypted, 0, TobeEncrypted.Length);
                des.Clear();
            
           
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] FromBase64String(string s)
        {
            return Convert.FromBase64String(s);
        }
        /// <summary>
        /// 十六进制生成字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(this string s)
        {
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte)((Chr2Hex(s.Substring(i * 2, 1)) * 0x10) + Chr2Hex(s.Substring((i * 2) + 1, 1)));
            }
            return buffer;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buf"></param>
        /// <returns></returns>
        public static string ToBase64String(byte[] buf)
        {
            return Convert.ToBase64String(buf);
        }
    }
}
