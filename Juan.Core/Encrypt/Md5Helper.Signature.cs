using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace Juan.Core
{
    /// <summary>
    /// Md5帮助类
    /// </summary>
    public static partial class Md5Helper
    {

        /// <summary>
        /// 生成Md5签名
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encryptKey">密钥</param>
        /// <returns></returns>
        public static string SignatureMD5(this string data, string encryptKey)
        {


            encryptKey.ArgumentNoNull("encryptKey不能为空");
            string[] encryptKeyArrary = encryptKey.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            return data.ToLower().MD5Encrypt(encryptKeyArrary[0]);

        }

        /// <summary>
        /// 检查Md5签名
        /// </summary>
        /// <param name="sourceData">源值</param>
        /// <param name="signatureMd5">签名值</param>
        /// <param name="encryptKey">Key值</param>
        /// <returns></returns>
        public static bool SignatureCheckMD5(this string sourceData, string signatureMd5, string encryptKey)
        {

            signatureMd5.ArgumentNoNull("signatureMd5不能为空");
            encryptKey.ArgumentNoNull("encryptKey不能为空");
            foreach (string encryptValue in encryptKey.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries))
            {

                string CheckSignatureMd5 = sourceData.SignatureMD5(encryptValue);
                bool IsCheckSucess = CheckSignatureMd5.Equals(signatureMd5, StringComparison.OrdinalIgnoreCase);
                if (IsCheckSucess)
                {
                    return true;
                }
            }
            return false;

        }

    }
}
