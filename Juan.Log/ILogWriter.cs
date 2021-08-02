using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Log
{
    /// <summary>
    /// 日志接口
    /// </summary>
    internal interface ILogWriter
    {
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="objLogInfo">日志信息</param>
        void WriterLog(LogInfo objLogInfo);

    }
}
