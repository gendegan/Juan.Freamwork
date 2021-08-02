using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    public partial class ClientHeaderInfo
    {
        /// <summary>
        /// 广告标识
        /// </summary>
        public string IDFA
        {
            get;
            set;
        }
        /// <summary>
        /// 开发者唯一标识
        /// </summary>
        public string IDFV
        {
            get;
            set;
        }
        /// <summary>
        /// 开机启动时间
        /// </summary>
        public string RTime
        {
            get;
            set;
        }

        /// <summary>
        /// Token
        /// </summary>
        public string Token
        {
            get;
            set;
        }
        /// <summary>
        /// Token类型
        /// </summary>
        public int TokenType
        {
            get;
            set;
        }
        /// <summary>
        /// 第三方广告标识
        /// </summary>
        public string SimIDFA
        {
            get;
            set;
        }
    }
}
