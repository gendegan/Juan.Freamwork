using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 加密类型
    /// </summary>
    public enum EncryptType
    {
        /// <summary>
        /// 自动
        /// </summary>
        Auto = -1,
        /// <summary>
        /// 没有加密
        /// </summary>
        None = 0,
        /// <summary>
        /// XXTea加密
        /// </summary>
        XXTea = 1,
        /// <summary>
        /// DES加密
        /// </summary>
        DES = 2
    }
}
