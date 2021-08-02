using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Juan.Core
{
    public static partial class RequestHelper
    {
        /// <summary>
        /// 当前请求上下文
        /// </summary>
        public static HttpContext CurrentContext
        {
            get
            {
                return HttpContext.Current;
            }
        }
        

        /// <summary>
        /// 获取真实地址
        /// </summary>
        /// <returns></returns>
        public static string GetRealIp()
        {
            return SysVariable.GetRealIp();
        }
    }
}
