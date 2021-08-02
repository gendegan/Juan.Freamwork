using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
namespace Juan.Core
{
    /// <summary>
    /// RSACryption : 采用RSA不对称加密方式的加密/解密  及数字签名。
    /// </summary>
    public static partial class RSACryptographyHelper
    {

        #region RSA 的密钥产生
        /// <summary>
        /// 产生私钥 和公钥 D:\OpenSolution\Juan.Core\Encrypt\RSACryptographyHelper.cs
        /// </summary>
        /// <param name="xmlPrivateKeys">私钥</param>
        /// <param name="xmlPublicKey">公钥</param>
        public static void RSAKey(out string xmlPrivateKeys, out string xmlPublicKey)
        {
            try
            {
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                xmlPrivateKeys = rsa.ToXmlString(true);
                xmlPublicKey = rsa.ToXmlString(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RSA 的加密
        /// <summary>
        /// RSA的加密函数 
        /// </summary>
        /// <param name="source">要加密的字符串</param>
        /// <param name="xmlPublicKey">公钥</param>
        /// <returns></returns>
        public static string EncryptRSA(this  byte[] source, string xmlPublicKey)
        {
            
                byte[] CypherTextBArray;
                string Result;
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPublicKey);
                CypherTextBArray = rsa.Encrypt(source, false);
                Result = Convert.ToBase64String(CypherTextBArray);
                return Result;
 
        }

        /// <summary>
        /// RSA的加密函数 
        /// </summary>
        /// <param name="source">要加密的字符串</param>
        /// <param name="xmlPublicKey">公钥</param>
        /// <returns></returns>
        public static string EncryptMaxRSA(this  byte[] source, string xmlPublicKey)
        {
          
                using (System.Security.Cryptography.RSACryptoServiceProvider RSACryptography = new RSACryptoServiceProvider())
                {
                    RSACryptography.FromXmlString(xmlPublicKey);

                    int MaxBlockSize = RSACryptography.KeySize / 8 - 11;    //加密块最大长度限制

                    if (source.Length <= MaxBlockSize)
                        return Convert.ToBase64String(RSACryptography.Encrypt(source, false));

                    using (MemoryStream PlaiStream = new MemoryStream(source))
                    using (MemoryStream CrypStream = new MemoryStream())
                    {
                        Byte[] Buffer = new Byte[MaxBlockSize];
                        int BlockSize = PlaiStream.Read(Buffer, 0, MaxBlockSize);

                        while (BlockSize > 0)
                        {
                            Byte[] ToEncrypt = new Byte[BlockSize];
                            Array.Copy(Buffer, 0, ToEncrypt, 0, BlockSize);

                            Byte[] Cryptograph = RSACryptography.Encrypt(ToEncrypt, false);
                            CrypStream.Write(Cryptograph, 0, Cryptograph.Length);

                            BlockSize = PlaiStream.Read(Buffer, 0, MaxBlockSize);
                        }

                        return Convert.ToBase64String(CrypStream.ToArray(), Base64FormattingOptions.None);
                    }

                }
           
        }

        /// <summary>
        /// RSA的加密函数
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="xmlPublicKey">公钥</param>
        /// <returns></returns>
        public static string EncryptRSA(this string source, string xmlPublicKey)
        {

            return Encoding.UTF8.GetBytes(source).EncryptRSA(xmlPublicKey);

        }

        /// <summary>
        /// RSA的加密函数[不受长度限制]
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="xmlPublicKey">公钥</param>
        /// <returns></returns>
        public static string EncryptMaxRSA(this string source, string xmlPublicKey)
        {

            return Encoding.UTF8.GetBytes(source).EncryptMaxRSA(xmlPublicKey);

        }
        #endregion



        #region RSA的解密函数
        /// <summary>
        /// RSA的解密函数 
        /// </summary>
        /// <param name="source">要解密的字符口串</param>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <returns></returns>
        public static string DecryptRSA(this string source, string xmlPrivateKey)
        {

            return Convert.FromBase64String(source).DecryptRSA(xmlPrivateKey);

        }
        /// <summary>
        /// RSA的解密函数 
        /// </summary>
        /// <param name="source">要解密的字符口串</param>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <returns></returns>
        public static string DecryptMaxRSA(this string source, string xmlPrivateKey)
        {

            return Convert.FromBase64String(source).DecryptMaxRSA(xmlPrivateKey);

        }

        /// <summary>
        /// RSA的解密函数
        /// </summary>
        /// <param name="source">要解密的字符口串</param>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <returns></returns>
        public static string DecryptRSA(this byte[] source, string xmlPrivateKey)
        {
            try
            {
                byte[] DypherTextBArray;
                string Result;
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPrivateKey);
                DypherTextBArray = rsa.Decrypt(source, false);

                Result = Encoding.UTF8.GetString(DypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        /// <summary>
        /// DecryptMaxRSA
        /// </summary>
        /// <param name="source"></param>
        /// <param name="xmlPrivateKey"></param>
        /// <returns></returns>
        public static string DecryptMaxRSA(this byte[] source, string xmlPrivateKey)
        {
            

                using (System.Security.Cryptography.RSACryptoServiceProvider RSACryptography = new RSACryptoServiceProvider())
                {
                    RSACryptography.FromXmlString(xmlPrivateKey);
                    int MaxBlockSize = RSACryptography.KeySize / 8;    //解密块最大长度限制

                    if (source.Length <= MaxBlockSize)
                        return (new UnicodeEncoding()).GetString(RSACryptography.Decrypt(source, false));

                    using (MemoryStream CrypStream = new MemoryStream(source))
                    using (MemoryStream PlaiStream = new MemoryStream())
                    {
                        Byte[] Buffer = new Byte[MaxBlockSize];
                        int BlockSize = CrypStream.Read(Buffer, 0, MaxBlockSize);

                        while (BlockSize > 0)
                        {
                            Byte[] ToDecrypt = new Byte[BlockSize];
                            Array.Copy(Buffer, 0, ToDecrypt, 0, BlockSize);

                            Byte[] Plaintext = RSACryptography.Decrypt(ToDecrypt, false);
                            PlaiStream.Write(Plaintext, 0, Plaintext.Length);

                            BlockSize = CrypStream.Read(Buffer, 0, MaxBlockSize);
                        }

                        return Encoding.UTF8.GetString(PlaiStream.ToArray());
                    }
                }

            

        }

        #region 获取Hash描述表

        /// <summary>
        ///  获取Hash描述表
        /// </summary>
        /// <param name="source">字符串</param>
        /// <returns></returns>
        public static byte[] GetSignatureHashByte(this string source)
        {
            try
            {
                //从字符串中取得Hash描述 
                byte[] Buffer;
                System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
                Buffer = Encoding.UTF8.GetBytes(source);
                return MD5.ComputeHash(Buffer);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取Hash描述表
        /// </summary>
        /// <param name="source">字符串</param>
        /// <returns></returns>
        public static string GetSignatureHash(this  string source)
        {
            return Convert.ToBase64String(source.GetSignatureHashByte());
        }

        /// <summary>
        /// 获取Hash描述表 
        /// </summary>
        /// <param name="file">文件流</param>
        /// <returns></returns>
        public static byte[] GetSignatureHashByte(this  System.IO.FileStream file)
        {
            try
            {
                //从文件中取得Hash描述 
                System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
                byte[] HashData = MD5.ComputeHash(file);
                file.Close();

                return HashData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取Hash描述表
        /// </summary>
        /// <param name="file">文件流</param>
        /// <returns></returns>
        public static string GetSignatureHash(this  System.IO.FileStream file)
        {
            return Convert.ToBase64String(file.GetSignatureHashByte());
        }
        #endregion


        #region RSA签名
        /// <summary>
        ///  签名档[用户自行取得Hash]
        /// </summary>
        /// <param name="hashSignatureSignatureByte">hash签名字节</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>返回签名档</returns>
        public static byte[] EncryptSignatureHashByte(this byte[] hashSignatureSignatureByte, string privateKey)
        {
            try
            {
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

                RSA.FromXmlString(privateKey);
                System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
                //设置签名的算法为MD5 
                RSAFormatter.SetHashAlgorithm("MD5");
                //执行签名 
                return RSAFormatter.CreateSignature(hashSignatureSignatureByte);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 签名档[用户自行取得Hash字节]
        /// </summary>
        /// <param name="hashSignatureSignatureByte">hash签名字节</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>返回签名档</returns>
        public static string EncryptSignatureHash(this byte[] hashSignatureSignatureByte, string privateKey)
        {
            return Convert.ToBase64String(hashSignatureSignatureByte.EncryptSignatureHashByte(privateKey));
        }

        /// <summary>
        /// 签名档[用户自行取得Hash]
        /// </summary>
        /// <param name="hashSignatureSource">hash签名字符串[本框架提供]</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>返回签名档</returns>
        public static byte[] EncryptSignatureHashByte(this  string hashSignatureSource, string privateKey)
        {
            return Convert.FromBase64String(hashSignatureSource).EncryptSignatureHashByte(privateKey);
        }

        /// <summary>
        /// 签名档[用户自行取得Hash]
        /// </summary>
        /// <param name="hashSignatureSource">hash签名字符串[本框架提供]</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>返回签名档</returns>
        public static string EncryptSignatureHash(this string hashSignatureSource, string privateKey)
        {
            return Convert.FromBase64String(hashSignatureSource).EncryptSignatureHash(privateKey);
        }
        /// <summary>
        /// 签名档[自动取得Hash]
        /// </summary>
        /// <param name="source">原字符串</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>返回签名档</returns>
        public static string EncryptSignature(this string source, string privateKey)
        {
            return source.GetSignatureHash().EncryptSignatureHash(privateKey);
        }

        #endregion


        #region RSA 签名验证

        /// <summary>
        /// 验证签名档
        /// </summary>
        /// <param name="signatureByte">签名档字节</param>
        /// <param name="hashSignatureByteSource">源签名档Hash字节</param>
        /// <param name="publicKey">公钥</param>
        /// <returns>返回签名验证是否正确</returns>
        public static bool DecryptSignatureHash(this   byte[] signatureByte, byte[] hashSignatureByteSource, string publicKey)
        {
            try
            {
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

                RSA.FromXmlString(publicKey);
                System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
                //指定解密的时候HASH算法为MD5 
                RSADeformatter.SetHashAlgorithm("MD5");
                if (RSADeformatter.VerifySignature(hashSignatureByteSource, signatureByte))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 验证签名档
        /// </summary>
        /// <param name="signatureByte">签名档字节</param>
        /// <param name="hashSignatureSource">源签名档</param>
        /// <param name="publicKey">公钥</param>
        /// <returns>返回签名验证是否正确</returns>
        public static bool DecryptSignatureHash(this byte[] signatureByte, string hashSignatureSource, string publicKey)
        {
            return signatureByte.DecryptSignatureHash(Convert.FromBase64String(hashSignatureSource), publicKey);

        }

        /// <summary>
        /// 验证签名档
        /// </summary>
        /// <param name="signature">签名档</param>
        /// <param name="hashSignatureByteSource">源签名档HashByte</param>
        /// <param name="publicKey">公钥</param>
        /// <returns>返回签名验证是否正确></returns>
        public static bool DecryptSignatureHash(this  string signature, byte[] hashSignatureByteSource, string publicKey)
        {
            return Convert.FromBase64String(signature).DecryptSignatureHash(hashSignatureByteSource, publicKey);
        }

        /// <summary>
        /// 验证签名档
        /// </summary>
        /// <param name="signature">签名档</param>
        /// <param name="hashSignatureSource">hash签名字符串[本框架提供]</param>
        /// <param name="publicKey">公钥</param>
        /// <returns>返回签名验证是否正确</returns>
        public static bool DecryptSignatureHash(this  string signature, string hashSignatureSource, string publicKey)
        {
            return signature.DecryptSignatureHash(Convert.FromBase64String(hashSignatureSource), publicKey);
        }

        /// <summary>
        /// 验证签名档[自动取得Hash]
        /// </summary>
        /// <param name="source">原字符串</param>
        /// <param name="signature">签名</param>
        /// <param name="publicKey">公钥</param>
        /// <returns>返回签名验证是否正确</returns>
        public static bool DecryptSignature(this  string source, string signature, string publicKey)
        {
            return signature.DecryptSignatureHash(source.GetSignatureHash(), publicKey);

        }

        #endregion





    }
}
