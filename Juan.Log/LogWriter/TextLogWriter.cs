using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.Concurrent;
using Juan.Core;
namespace Juan.Log
{
    /// <summary>
    /// 文本日志
    /// </summary>
    internal class TextLogWriter : ILogWriter
    {

        static ConcurrentDictionary<string, FileTextLog> _FileLogList = new ConcurrentDictionary<string, FileTextLog>();
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="objLogMessage"></param>
        public void WriterLog(LogInfo objLogInfo)
        {


            string WritePath = objLogInfo.WritePath;
            if (string.IsNullOrWhiteSpace(WritePath))
            {

                return;
            }
            string fileKey = objLogInfo.Application.ApplicationCode.ToString() + objLogInfo.LogType;
            FileTextLog _FileLog = null;
            if (!_FileLogList.TryGetValue(fileKey, out _FileLog))
            {

                _FileLog = new FileTextLog(objLogInfo.WritePath, objLogInfo.Application.ApplicationCode, objLogInfo.LogType.ToString());
                _FileLogList.TryAdd(fileKey, _FileLog);
            }
            _FileLog.WriteMap = objLogInfo.WritePath;
            _FileLog.WriterLog(objLogInfo);

        }
    }
}
