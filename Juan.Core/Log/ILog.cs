using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    /// <summary>
    /// 日志接口
    /// </summary>
    [Ioc("Juan.Log", "Juan.Log.Logger")]
    public abstract class ILog
    {
        static ILog _ILog = null;
        /// <summary>
        /// 
        /// </summary>
        public static ILog Log
        {
            get
            {
                if (_ILog == null)
                {

                    _ILog = IocHelper.Instance<ILog>();
                }
                return _ILog;
            }
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="logTitle">日志标题</param>
        /// <param name="message">日志内容</param>
        /// <param name="resultMessage">结果内容</param>
        public abstract void Write(LogType logType, string logTitle, object message, object resultMessage);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationCode"></param>
        /// <param name="logType"></param>
        /// <param name="logTitle"></param>
        /// <param name="message"></param>
        /// <param name="resultMessage"></param>
        public abstract void Target(string applicationCode, LogType logType, string logTitle, object message, object resultMessage);




    }
}
