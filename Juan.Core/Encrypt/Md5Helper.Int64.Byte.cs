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
        /// <returns></returns>
        public static Int64 MD5EncryptInt64(this byte[] data)
        {
            byte[] newSource = data.MD5Byte();
            Int64 hashCodeStart = BitConverter.ToInt64(newSource, 0);
            Int64 hashCodeEnd = BitConverter.ToInt64(newSource, 8);
            return hashCodeStart ^ hashCodeEnd;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static ulong MD5EncryptUInt64(this byte[] data)
        {

            byte[] newSource = data.MD5Byte();
            ulong hashCodeStart = BitConverter.ToUInt64(newSource, 0);
            ulong hashCodeEnd = BitConverter.ToUInt64(newSource, 8);
            return hashCodeStart ^ hashCodeEnd;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Int64 MD5EncryptInt64Abs(this byte[] data)
        {
            return System.Math.Abs(data.MD5EncryptInt64());
        }

    }
}
