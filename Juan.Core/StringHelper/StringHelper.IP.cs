using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    public static partial class StringHelper
    {

        /// <summary>
        /// 判断IP地址是否Loopback
        /// </summary>
        /// <param name="ip">IP</param>
        /// <returns></returns>
        public static bool IsLoopbackIp(this string ip)
        {
            return (ip == "127.0.0.1");
        }

        /// <summary>
        /// 长整型转ip字符串
        /// </summary>
        /// <param name="ip">IP</param>
        /// <returns></returns>
        public static string ToIpString(this long ip)
        {
            int ip1, ip2, ip3, ip4;

            ip4 = (int)(ip % 256);
            ip = ip / 256;
            ip3 = (int)(ip % 256);
            ip = ip / 256;
            ip2 = (int)(ip % 256);
            ip = ip / 256;
            ip1 = (int)(ip % 256);
            string sip1, sip2, sip3, sip4;
            sip1 = ip1.ToString();
            sip2 = ip2.ToString();
            sip3 = ip3.ToString();
            sip4 = ip4.ToString();
            return sip1 + "." + sip2 + "." + sip3 + "." + sip4;
        }

        /// <summary>
        /// ip地址转长整型
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns></returns>
        public static long ToIpInt64(this string ip)
        {
            string[] ipArray = ip.Split(".".ToCharArray());
            int ip1 = 0, ip2 = 0, ip3 = 0, ip4 = 0;
            ip1 = Convert.ToInt32(ipArray[0]);
            ip2 = Convert.ToInt32(ipArray[1]);
            ip3 = Convert.ToInt32(ipArray[2]);
            ip4 = Convert.ToInt32(ipArray[3]);
            long addr = ip1;
            addr = addr * 256 + ip2;
            addr = addr * 256 + ip3;
            addr = addr * 256 + ip4;
            return addr;
        }
        /// <summary>
        /// 地址转长整型
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="partNumber">位数</param>
        /// <returns></returns>
        public static long ToIpInt64(this string ip, int partNumber)
        {
            string[] ipArray = ip.Split(".".ToCharArray());
            for (int i = partNumber; i < ipArray.Length; i++)
            {
                ipArray[i] = "0";
            }
            int ip1 = 0, ip2 = 0, ip3 = 0, ip4 = 0;
            ip1 = Convert.ToInt32(ipArray[0]);
            ip2 = Convert.ToInt32(ipArray[1]);
            ip3 = Convert.ToInt32(ipArray[2]);
            ip4 = Convert.ToInt32(ipArray[3]);
            long addr = ip1;
            addr = addr * 256 + ip2;
            addr = addr * 256 + ip3;
            addr = addr * 256 + ip4;
            return addr;
        }
    }
}
