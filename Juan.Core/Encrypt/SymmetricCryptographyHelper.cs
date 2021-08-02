using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
namespace Juan.Core
{
    /// <summary>
    /// Symmetric 加密及解密
    /// </summary>
    public static partial class SymmetricCryptographyHelper
    {
        /// <remarks> 
        /// 使用.Net SymmetricAlgorithm 类的构造器. 
        /// </remarks> 
        public static SymmetricAlgorithm GetSymmetricCryptoService(SymmetricProvider netSelected)
        {
            SymmetricAlgorithm mobjCryptoService = new DESCryptoServiceProvider(); ;
            switch (netSelected)
            {
                case SymmetricProvider.Default:
                    mobjCryptoService = SymmetricAlgorithm.Create();
                    break;
                case SymmetricProvider.DES:
                    mobjCryptoService = new DESCryptoServiceProvider();
                    break;
                case SymmetricProvider.RC2:
                    mobjCryptoService = new RC2CryptoServiceProvider();
                    break;
                case SymmetricProvider.Rijndael:
                    mobjCryptoService = new RijndaelManaged();
                    break;


            }
            return mobjCryptoService;
        }

        /// <summary>
        ///  对称加密算法
        /// </summary>
        /// <param name="CryptString"></param>
        /// <param name="netSelected"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public static string EncryptSymmetric(this  string CryptString, SymmetricProvider netSelected, string Key, string IV)
        {
            return EncryptSymmetric(CryptString, netSelected, CryptogramHelper.HexStringToByteArray(Key), CryptogramHelper.HexStringToByteArray(IV));
        }
        /// <summary>
        ///  对称加密算法
        /// </summary>
        /// <param name="CryptString"></param>
        /// <param name="netSelected"></param>
        /// <param name="Key"></param>
        /// <param name="IV"></param>
        /// <returns></returns>
        public static string EncryptSymmetric(this  string CryptString, SymmetricProvider netSelected, byte[] Key, byte[] IV)
        {
            SymmetricAlgorithm sa = GetSymmetricCryptoService(netSelected);
            string str;
            try
            {
                sa.Key = Key;
                sa.IV = IV;
                byte[] bytes = Encoding.UTF8.GetBytes(CryptString);
                str = Convert.ToBase64String(sa.CreateEncryptor(Key, IV).TransformFinalBlock(bytes, 0, bytes.Length));
                return str;
            }
            catch 
            {

                str = string.Empty;
            }
            finally
            {
                sa.Clear();
            }
            return str;
        }
      /// <summary>
        ///  对称解密算法
      /// </summary>
      /// <param name="DecryptString"></param>
      /// <param name="netSelected"></param>
      /// <param name="Key"></param>
      /// <param name="IV"></param>
      /// <returns></returns>
        public static string DecryptSymmetric(this string DecryptString, SymmetricProvider netSelected, string Key, string IV)
        {
            return DecryptSymmetric(DecryptString, netSelected, CryptogramHelper.HexStringToByteArray(Key), CryptogramHelper.HexStringToByteArray(IV));
        }
      /// <summary>
        /// 对称解密算法
      /// </summary>
      /// <param name="DecryptString"></param>
      /// <param name="netSelected"></param>
      /// <param name="Key"></param>
      /// <param name="IV"></param>
      /// <returns></returns>
        public static string DecryptSymmetric(this string DecryptString, SymmetricProvider netSelected, byte[] Key, byte[] IV)
        {
            SymmetricAlgorithm sa = GetSymmetricCryptoService(netSelected);
            string str;
            try
            {
                sa.Key = Key;
                sa.IV = IV;
                byte[] inputBuffer = Convert.FromBase64String(DecryptString);
                ICryptoTransform transform = sa.CreateDecryptor(Key, IV);
                str = Encoding.UTF8.GetString(transform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
                return str;
            }
            catch
            {
                str = string.Empty;
            }
            finally
            {
                sa.Clear();
            }

            return str;
        }





        #region 共用方法。
        /// <summary>
        /// 
        /// </summary>
        /// <param name="netSelected"></param>
        /// <returns></returns>
        public static byte[] GetLegalKey(SymmetricProvider netSelected)
        {

            SymmetricAlgorithm objSymmetricAlgorithm = GetSymmetricCryptoService(netSelected);
            objSymmetricAlgorithm.GenerateKey();
            return objSymmetricAlgorithm.Key;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="netSelected"></param>
        /// <returns></returns>
        public static byte[] GetLegalIV(SymmetricProvider netSelected)
        {
            SymmetricAlgorithm objSymmetricAlgorithm = GetSymmetricCryptoService(netSelected);
            objSymmetricAlgorithm.GenerateIV();
            return objSymmetricAlgorithm.IV;
        }
        #endregion
        /// <summary>
        /// 生成KEY
        /// </summary>
        /// <param name="netSelected"></param>
        /// <returns></returns>
        public static string GenerateKey(SymmetricProvider netSelected)
        {
            return CryptogramHelper.ByteArrayToHexString(GetLegalKey(netSelected));
        }
        /// <summary>
        /// 生成IV
        /// </summary>
        /// <param name="netSelected"></param>
        /// <returns></returns>
        public static string GenerateIV(SymmetricProvider netSelected)
        {
            return CryptogramHelper.ByteArrayToHexString(GetLegalIV(netSelected));
        }
    }

    /// <summary>
    /// 加密/解密算法的方式
    /// </summary>
    public enum SymmetricProvider : int
    {

        /// <summary>
        /// 默认算法
        /// </summary>
        Default,
        /// <summary>
        /// DES算法
        /// </summary>
        DES,

        /// <summary>
        /// RC2算法
        /// </summary>
        RC2,

        /// <summary>
        /// Rijndael算法
        /// </summary>
        Rijndael
    }


}
