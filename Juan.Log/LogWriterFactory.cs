using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Juan.Core;
using System.Diagnostics;
using Juan.Log.Context;
using Juan.Log.Entity;
namespace Juan.Log
{
    /// <summary>
    /// 日志工厂
    /// </summary>
    internal class LogWriterFactory
    {

        QueuePoolHelper<LogInfo> _QueuePoolHelper = null;

        /// <summary>
        /// 日志消息池
        /// </summary>
        public QueuePoolHelper<LogInfo> QueuePool
        {
            get
            {
                return _QueuePoolHelper;
            }
        }

        /// <summary>
        /// 构造
        /// </summary>
        public LogWriterFactory()
        {
            _QueuePoolHelper = new QueuePoolHelper<LogInfo>(1);
            _QueuePoolHelper.SendMessage += new EventHandler<QueuePoolEventArgs<LogInfo>>(_QueuePoolHelper_SendMessage);
            _QueuePoolHelper.StartProcess();

        }

        void _QueuePoolHelper_SendMessage(object sender, QueuePoolEventArgs<LogInfo> e)
        {
            LogInfo objLogInfo = e.Message;
            try
            {
                List<LogStore> objLogStoreList = objLogInfo.Application.GetLogStoreInfo();

                LogStore objLogStore = objLogStoreList.FirstOrDefault(s => s.LogType == objLogInfo.LogType);
                if (objLogStore != null)
                {
                    foreach (LogWriterType writeType in objLogStore.WriterType)
                    {

                        if (writeType == LogWriterType.DataWriter)
                        {
                            WriteLog(objLogInfo, writeType);
                        }
                        else if (writeType == LogWriterType.FileWriter && objLogInfo.WritePath.IsNoNullOrWhiteSpace())
                        {
                            WriteLog(objLogInfo, writeType);
                        }

                    }
                }
            }
            catch (Exception objExp)
            {
                try
                {
                    WriterEventLog(objLogInfo, objExp);

                }
                catch
                {
                }
            }
        }


        private void WriterEventLog(LogInfo objLogInfo, Exception objException)
        {
            try
            {
                StringBuilder objFormattedMessage = new StringBuilder();
                objFormattedMessage.Append("程序代码：").Append(objLogInfo.Application.ApplicationCode.ToString()).Append("\r\n");
                objFormattedMessage.Append("模块分类：").Append(objLogInfo.LogType.ToString()).Append("\r\n");
                objFormattedMessage.Append("运行程序：").Append(LogInfo.ProcessName).Append("\r\n");
                objFormattedMessage.Append("异常日期：").Append(objLogInfo.CreateDate.ToString("yyyy-MM-dd HH:mm:ss")).Append("\r\n");
                objFormattedMessage.Append("用户IP：").Append(objLogInfo.UserIP).Append("\r\n");
                objFormattedMessage.Append("DeviceID：").Append(objLogInfo.DeviceID).Append("\r\n");
                objFormattedMessage.Append("MessageID：").Append(objLogInfo.MessageID).Append("\r\n");
                objFormattedMessage.Append("UserID：").Append(objLogInfo.UserID).Append("\r\n");
                objFormattedMessage.Append("运行主机：").Append(LogInfo.Host).Append("\r\n");
                objFormattedMessage.Append("出错页面：").Append(objLogInfo.Url).Append("\r\n");
                objFormattedMessage.Append("页面来源：").Append(objLogInfo.UrlReferrer).Append("\r\n");
                objFormattedMessage.Append("日志内容：").Append(objLogInfo.Message).Append("\r\n");
                objFormattedMessage.Append("日志结果：").Append(objLogInfo.ResultMessage).Append("\r\n");
                objFormattedMessage.Append("\r\n-----------------------------------------\r\n");
                objFormattedMessage.Append("异常内容：").Append(objException.Message).Append("\r\n");
                objFormattedMessage.Append("调用堆栈：").Append(objException.StackTrace).Append("\r\n");
                while (objException.InnerException != null)
                {
                    objFormattedMessage.Append("内部事件信息：").Append(objException.InnerException.Message).Append("\r\n");
                    objFormattedMessage.Append("内部堆栈跟踪：").Append(objException.InnerException.StackTrace).Append("\r\n");
                    objException = objException.InnerException;
                }
                objFormattedMessage.Append("\r\n-----------------------------------------\r\n");

                EventLog.WriteEntry("Application", objFormattedMessage.ToString(), EventLogEntryType.Error);
            }
            catch
            {
            }
        }


        #region 写日志
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="objLogMessage"></param>
        /// <param name="objLogWriterType"></param>
        private void WriteLog(LogInfo objLogMessage, LogWriterType objLogWriterType)
        {
            ILogWriter objILogWriter = CreateLogWriter(objLogWriterType);
            objILogWriter.WriterLog(objLogMessage);


        }
        #endregion

        /// <summary>
        /// 压入队列
        /// </summary>
        /// <param name="objLogInfo"></param>
        public void EnqueueLog(LogInfo objLogInfo)
        {

            _QueuePoolHelper.Enqueue(objLogInfo);
        }

        #region 创建日志类
        DataLogWriter _DataLogWriter = new DataLogWriter();
        /// <summary>
        /// 创建日志类
        /// </summary>
        /// <param name="objLogWriterType">日志类型</param>
        /// <returns></returns>
        private ILogWriter CreateLogWriter(LogWriterType objLogWriterType)
        {
            ILogWriter objILogWriter;
            switch (objLogWriterType)
            {
                case LogWriterType.DataWriter:
                    objILogWriter = _DataLogWriter;
                    break;

                case LogWriterType.FileWriter:

                    objILogWriter = new TextLogWriter();
                    break;
                default:
                    objILogWriter = _DataLogWriter;
                    break;


            }
            return objILogWriter;

        }
        #endregion
    }

}
