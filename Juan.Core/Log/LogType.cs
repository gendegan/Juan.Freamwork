using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {

        /// <summary>
        /// 参数输入错误
        /// </summary>
        [EnumAttribute("参数输入错误")]
        InputError = 1,

        /// <summary>
        /// 异常错误
        /// </summary>
        [EnumAttribute("异常错误")]
        Error = 2,
        /// <summary>
        /// 致命错误
        /// </summary>
        [EnumAttribute("致命错误")]
        Fatal = 3,

        /// <summary>
        ///跟踪信息 
        /// </summary>
        [EnumAttribute("跟踪信息")]
        Trace = 4,
        /// <summary>
        ///跟踪信息 
        /// </summary>
        [EnumAttribute("调试信息")]
        Debug = 5,

        /// <summary>
        /// 记录信息
        /// </summary>
        [EnumAttribute("记录信息")]
        Info = 6,
        /// <summary>
        /// 警告信息
        /// </summary>
        [EnumAttribute("警告信息")]
        Warn = 7,
        /// <summary>
        /// 攻击记录
        /// </summary>
        [EnumAttribute("攻击信息")]
        Attack = 8,

        /// <summary>
        /// Sql信息"
        /// </summary>
        [EnumAttribute("Sql语句")]
        Sql = 9,
        /// <summary>
        /// Sql信息
        /// </summary>
        [EnumAttribute("Solr语句")]
        Solr = 10,
        /// <summary>
        /// ESearch
        /// </summary>
        [EnumAttribute("ESearch语句")]
        ESearch = 11
    }
}
