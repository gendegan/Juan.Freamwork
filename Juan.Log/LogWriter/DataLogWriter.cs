using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Juan.Core;
using Juan.Log.Entity;
using Juan.Log.Context;
namespace Juan.Log
{
    /// <summary>
    /// 数据库日志
    /// </summary>
    internal class DataLogWriter : ILogWriter
    {

        static LogDataContext _LogDataContext = new LogDataContext();

        static DataLogWriter()
        {
            _LogDataContext.Context.CloseExecute();
        }

        public DataLogWriter()
        {

        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="objLogMessage"></param>
        public void WriterLog(LogInfo objLogInfo)
        {


            LogData objLogData = new LogData();
            objLogData.LogID = -1;
            objLogData.ApplicationID = objLogInfo.Application.ApplicationID;
            objLogData.ApplicationName = objLogInfo.Application.ApplicationName;
            objLogData.ApplicationHost = LogInfo.Host;
            objLogData.Url = objLogInfo.Url.ToCutWord(1000);
            objLogData.UrlReferrer = objLogInfo.UrlReferrer.ToCutWord(1000);
            objLogData.UserAgent = objLogInfo.UserAgent.ToCutWord(3000);
            objLogData.UserID = objLogInfo.UserID;
            objLogData.UserIP = objLogInfo.UserIP;
            objLogData.ProcessName = LogInfo.ProcessName;
            objLogData.CreateDate = objLogInfo.CreateDate;
            objLogData.Title = objLogInfo.Title.ToCutWord(1000).Replace(@"\p{Cs}", "[表情]", System.Text.RegularExpressions.RegexOptions.IgnoreCase);//过滤表情;
            objLogData.Message = objLogInfo.Message.Replace(@"\p{Cs}", "[表情]", System.Text.RegularExpressions.RegexOptions.IgnoreCase);//过滤表情;
            objLogData.ResultMessage = objLogInfo.ResultMessage.Replace(@"\p{Cs}", "[表情]", System.Text.RegularExpressions.RegexOptions.IgnoreCase);//过滤表情;
            objLogData.LogType = objLogInfo.LogType;
            objLogData.IDPath = objLogInfo.Application.IDPath;
            objLogData.HeadersData = objLogInfo.HeaderData.JsonSerialize();
            objLogData.FormData = objLogInfo.FormData.JsonSerialize();
            objLogData.CookiesData = objLogInfo.CookieData.JsonSerialize();
            objLogData.CurrentThreadID = objLogInfo.CurrentThreadID;
            objLogData.DeviceID = objLogInfo.DeviceID;
            objLogData.MessageID = objLogInfo.MessageID;
            _LogDataContext.Add(objLogData);


        }
    }
}
