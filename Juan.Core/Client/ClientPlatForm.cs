using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 客户端平台
    /// </summary>
    public enum ClientPlatForm
    {
        /// <summary>
        /// 电脑
        /// </summary>
        [Enum("电脑")]
        PC = 0,
        /// <summary>
        /// iPhone
        /// </summary>
        [Enum("iPhone")]
        iPhone = 1,
        /// <summary>
        /// iPad
        /// </summary>
        [Enum("iPad")]
        iPad = 2,
        /// <summary>
        /// 安桌
        /// </summary>
        [Enum("安卓")]
        Android = 11,
        /// <summary>
        /// 微信小程序
        /// </summary>
        [Enum("微信小程序")]
        MiniApp = 21,
        /// <summary>
        /// 快应用
        /// </summary>
        [Enum("快应用")]
        QuickApp = 22,
        /// <summary>
        /// 小程序
        /// </summary>
        [Enum("百度小程序")]
        BaiduMini = 23
    }
}
