using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Juan.Core;

namespace Juan.Log
{
    /// <summary>
    /// 文本日志
    /// </summary>
    internal class FileTextLog : FileLog
    { 
        /// <summary>
        /// 文本日志
        /// </summary>
        /// <param name="logWriteMap"></param>
        /// <param name="applicationCode"></param>
        /// <param name="logType"></param>
        public FileTextLog(string logWriteMap, string applicationCode, string logType)
            : base(logWriteMap, applicationCode, logType)
        {

        }

        public override string FormantMessage(LogInfo objLogInfo)
        {
            StringBuilder objFormattedMessage = new StringBuilder();
            objFormattedMessage.Append("程序代码：").Append(objLogInfo.Application.ApplicationCode.ToString()).Append("\r\n");
            objFormattedMessage.Append("模块分类：").Append(objLogInfo.LogType.ToString()).Append("\r\n");
            objFormattedMessage.Append("运行程序：").Append(LogInfo.ProcessName).Append("\r\n");
            objFormattedMessage.Append("异常日期：").Append(objLogInfo.CreateDate.ToString("yyyy-MM-dd HH:mm:ss")).Append("\r\n");
            objFormattedMessage.Append("用户IP：").Append(objLogInfo.UserIP).Append("\r\n");
            objFormattedMessage.Append("运行主机：").Append(LogInfo.Host).Append("\r\n");
            objFormattedMessage.Append("出错页面：").Append(objLogInfo.Url).Append("\r\n");
            objFormattedMessage.Append("页面来源：").Append(objLogInfo.UrlReferrer).Append("\r\n");
            objFormattedMessage.Append("日志内容：").Append(objLogInfo.Message).Append("\r\n");
            objFormattedMessage.Append("日志结果：").Append(objLogInfo.ResultMessage).Append("\r\n");
            return objFormattedMessage.ToString();
        }

        /// <summary>
        /// 文件开始
        /// </summary>
        public override string FileBegin
        {
            get
            {
                return "\r\n-----------------------------------------\r\n";
            }
        }
        /// <summary>
        /// 文件结束
        /// </summary>
        public override string FileEnd
        {
            get
            {
                return "\r\n-----------------------------------------\r\n";
            }
        }

    }
}
