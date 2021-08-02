using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{

    /// <summary>
    /// 3DES加密
    /// </summary>
    public static class DES3CryptographyHelper
    {

        /// <summary>
        /// 3DES加密
        /// </summary>
        /// <param name="CryptString"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public static string Encrypt3DESToBase64(this string CryptString, string Key, string IV)
        {
            return Encrypt3DESToBase64(CryptString, CryptogramHelper.HexStringToByteArray(Key), CryptogramHelper.HexStringToByteArray(IV));
        }
        /// <summary>
        /// 3DES加密
        /// </summary>
        /// <param name="CryptString"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public static string Encrypt3DESToBase64(this string CryptString, byte[] Key, byte[] IV)
        {
            string str;
            TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
            try
            {

                byte[] bytes = Encoding.UTF8.GetBytes(CryptString);
                provider.Mode = CipherMode.CBC;
                provider.Padding = PaddingMode.PKCS7;
                str = Convert.ToBase64String(provider.CreateEncryptor(Key, IV).TransformFinalBlock(bytes, 0, bytes.Length));
            }
            catch 
            {

                str = string.Empty;
            }
            finally
            {
                provider.Clear();
            }
            return str;
        }
        /// <summary>
        /// 3DES解密
        /// </summary>
        /// <param name="DecryptString"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public static string Decrypt3DESFromBase64(this string DecryptString, string Key, string IV)
        {
            return Decrypt3DESFromBase64(DecryptString, CryptogramHelper.HexStringToByteArray(Key), CryptogramHelper.HexStringToByteArray(IV));
        }
        /// <summary>
        ///  3DES解密
        /// </summary>
        /// <param name="DecryptString"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public static string Decrypt3DESFromBase64(this string DecryptString, byte[] Key, byte[] IV)
        {
            string str;
            TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
            try
            {

                byte[] inputBuffer = Convert.FromBase64String(DecryptString);
                provider.Mode = CipherMode.CBC;
                provider.Padding = PaddingMode.PKCS7;
                ICryptoTransform transform = provider.CreateDecryptor(Key, IV);
                str = Encoding.UTF8.GetString(transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            }
            catch 
            {
                str = string.Empty;
            }
            finally
            {
                provider.Clear();
            }
            return str;
        }

        /// <summary>
        /// 生成密码
        /// </summary>
        /// <returns></returns>
        public static byte[] GetLegalKey()
        {
            byte[] key = null;
           
                TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
                provider.GenerateKey();
                key = provider.Key;
          
            return key;
        }
        /// <summary>
        /// 生成密码
        /// </summary>
        /// <returns></returns>
        public static string GenerateKey()
        {
            return CryptogramHelper.ByteArrayToHexString(GetLegalKey());
        }
        /// <summary>
        /// 生成IV
        /// </summary>
        /// <returns></returns>
        public static string GenerateIV()
        {
            return CryptogramHelper.ByteArrayToHexString(GetLegalIV());
        }
        /// <summary>
        /// 生成IV
        /// </summary>
        /// <returns></returns>
        public static byte[] GetLegalIV()
        {
            byte[] iV = null;
            
                TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
                provider.GenerateIV();
                iV = provider.IV;
             
            return iV;
        }

    }
}
