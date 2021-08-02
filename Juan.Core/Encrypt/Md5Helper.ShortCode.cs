using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    public static partial class Md5Helper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encryptKey">密钥</param>
        /// <param name="encoding">编码</param>
        /// <returns></returns>
        public static string MD5EncryptShortCode(this string data, string encryptKey, Encoding encoding)
        {
            data = string.IsNullOrEmpty(data) ? "" : data;
            if (encryptKey.Length > 0)
            {
                data = data + encryptKey;
            }
            byte[] newSource = data.MD5Byte(encoding);
            string[] chars = new string[]{  
              "a","b","c","d","e","f","g","h",  
              "i","j","k","l","m","n","o","p",  
              "q","r","s","t","u","v","w","x",  
               "y","z","0","1","2","3","4","5",  
               "6","7","8","9","A","B","C","D",  
               "E","F","G","H","I","J","K","L",  
               "M","N","O","P","Q","R","S","T",  
               "U","V","W","X","Y","Z"  
           };
            string ShortCode = string.Empty;
            for (int i = 0; i <= 1; i++)
            {
                long hexint = 0x3FFFFFFF & BitConverter.ToInt64(newSource, 8 * i);
                for (int j = 0; j < 6; j++)
                {
                    //把得到的值与0x0000003D进行位与运算，取得字符数组chars索引  
                    long index = 0x0000003D & hexint;
                    //把取得的字符相加  
                    ShortCode += chars[index];
                    //每次循环按位右移5位  
                    hexint = hexint >> 5;
                }
            }
            return ShortCode;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encryptKey">密钥</param>
        /// <returns></returns>
        public static string MD5EncryptShortCode(this string data, string encryptKey)
        {
            return data.MD5EncryptShortCode(encryptKey, System.Text.Encoding.UTF8);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string MD5EncryptShortCode(this string data)
        {
            return data.MD5EncryptShortCode("");

        }



    }
}
