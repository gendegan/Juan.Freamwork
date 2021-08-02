using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Log
{
    public class Logger : ILog
    {

        static LogWriterFactory objLogWriterFactory = new LogWriterFactory();
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="logTitle">日志标题</param>
        /// <param name="message">日志内容</param>
        /// <param name="resultMessage">结果内容</param>
        public override void Write(LogType logType, string logTitle, object message, object resultMessage)
        {
            LogInfo objLogInfo = new LogInfo();
            objLogInfo.LogType = logType.ToString();
            objLogInfo.Title = logTitle;
            objLogInfo.Message = message.ToMessage();
            objLogInfo.ResultMessage = resultMessage.ToMessage();
            bool IsCheck = objLogInfo.ProcessLogData();
            ConsoleHelper.WriteLineRed(objLogInfo, logTitle);
            if (IsCheck)
            {
                objLogWriterFactory.EnqueueLog(objLogInfo);
            }
        }

        public override void Target(string applicationCode, LogType logType, string logTitle, object message, object resultMessage)
        {
            LogInfo objLogInfo = new LogInfo();
            objLogInfo.LogType = logType.ToString();
            objLogInfo.Title = logTitle;
            objLogInfo.Message = message.ToMessage();
            objLogInfo.ResultMessage = resultMessage.ToMessage();
            objLogInfo.ApplicationCode = applicationCode;
            bool IsCheck = objLogInfo.ProcessLogData();
            ConsoleHelper.WriteLineRed(objLogInfo, logTitle);
            if (IsCheck)
            {
                objLogWriterFactory.EnqueueLog(objLogInfo);
            }
        }
    }
}
