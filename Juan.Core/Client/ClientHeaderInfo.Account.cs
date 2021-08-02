using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    public partial class ClientHeaderInfo
    {


        /// <summary>
        /// 帐号标识
        /// </summary>
        public int AccountID
        {
            get;
            set;
        }

        /// <summary>
        /// 登录时间戳
        /// </summary>
        public long LoginStamp
        {
            get;
            set;
        }
        /// <summary>
        /// 登录签名
        /// </summary>
        public string LoginSignature
        {
            get;
            set;
        }
        /// <summary>
        /// 帐号签名
        /// </summary>
        public string AccountIDSignature
        {
            get;
            set;
        }
        /// <summary>
        /// 同步签名
        /// </summary>
        public string SyncSignature
        {
            get;
            set;
        }
        /// <summary>
        /// VIP签名
        /// </summary>
        public string VipSignature
        {
            get;
            set;
        }
        /// <summary>
        /// 登录代码
        /// </summary>
        public string LoginCode
        {
            get;
            set;
        }


        /// <summary>
        /// Vip类型
        /// </summary>
        public Int32 VipType
        {
            get;
            set;
        }
        /// <summary>
        /// VIP权益标识
        /// </summary>
        public string VipRightsID
        {
            get;
            set;
        }

        /// <summary>
        /// Vip开始时间
        /// </summary>
        public long VipStartTime
        {
            get;
            set;
        }
        /// <summary>
        /// Vip结束时间
        /// </summary>
        public long VipEndTime
        {
            get;
            set;
        }
        /// <summary>
        /// 会话标识
        /// </summary>
        public string SessionID
        {
            get;
            set;
        }
    }
}
