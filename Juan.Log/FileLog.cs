using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Xml;
using System.Threading;
using Juan.Core;
namespace Juan.Log
{
    /// <summary>
    /// 文件日志
    /// </summary>
    internal abstract class FileLog
    {
        /// <summary>
        /// 文件信息
        /// </summary>
        protected FileInfo _FileInfo = null;
        /// <summary>
        /// 流写
        /// </summary>
        protected StreamWriter _StreamWriter = null;

        object FileLock = new object();
   
        /// <summary>
        /// 日志路经
        /// </summary>
        public string WriteMap
        {
            get;
            set;
        }

        private string _CurrentWriteMap = "";
        /// <summary>
        /// 文件开始
        /// </summary>
        public virtual string FileBegin
        {
            get
            {
                return "";
            }
        }
        /// <summary>
        /// 文件结束
        /// </summary>
        public virtual string FileEnd
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// 类型分类
        /// </summary>
        public string LogType
        {
            get;
            set;
        }
        /// <summary>
        /// 类型分类
        /// </summary>
        public string ApplicationCode
        {
            get;
            set;
        }

        /// <summary>
        /// 日志路经
        /// </summary>
        public string LogDirectoryPath
        {
            get
            {
                string directoryPath = Path.Combine(WriteMap, ApplicationCode,  LogType, DateTime.Now.ToString("yyyy-MM-dd"));
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                return directoryPath;

            }
        }
        /// <summary>
        /// 文件日志
        /// </summary>
        /// <param name="logWriteMap"></param>
        /// <param name="applicationCode"></param>
        /// <param name="logType"></param>
        /// <param name="logWriterType"></param>
        public FileLog(string logWriteMap, string applicationCode, string logType)
        {
          
            WriteMap = logWriteMap;
            LogType = logType;
            ApplicationCode = applicationCode;
        }
        /// <summary>
        /// 释放
        /// </summary>

        ~FileLog()
        {

            if (_StreamWriter != null)
            {

                FileInfo objFileInfo = new FileInfo(_FileInfo.FullName);
                StreamWriter objStreamWriter = objFileInfo.AppendText();
                objStreamWriter.AutoFlush = true;
                objStreamWriter.Write(FileBegin);
                objStreamWriter.Flush();
                objStreamWriter.Close();
                objStreamWriter = null;
            }
        }
        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="objLogMessage"></param>
        /// <returns></returns>
        public abstract string FormantMessage(LogInfo objLogInfo);
      
        /// <summary>
        /// 日志写入
        /// </summary>
        /// <param name="objLogMessage"></param>
        public void WriterLog(LogInfo objLogInfo)
        {
            lock (FileLock)
            {
                if (_StreamWriter == null)
                {
                    CreateFile();

                }
                else
                {
                    if (_FileInfo.DirectoryName.IndexOf(DateTime.Now.ToString("yyyy-MM-dd")) == -1)
                    {

                        CreateFile();

                    }

                    else if (_StreamWriter.BaseStream.Length >= 10240000)
                    {
                        CreateFile();
                    }

                }
                if (_CurrentWriteMap != objLogInfo.WritePath)
                {
                    CreateFile();
                }
                _StreamWriter.Write(FormantMessage(objLogInfo));

            }

        }
        /// <summary>
        /// 创建文件
        /// </summary>
        public void CreateFile()
        {
            if (_StreamWriter != null)
            {
                _StreamWriter.Write(FileEnd);
                _StreamWriter.Flush();
                _StreamWriter.Dispose();
                _StreamWriter = null;
            }
            do
            {
                string fileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") +  ".txt";
                string logFilePath = Path.Combine(LogDirectoryPath, fileName);
                _FileInfo = new FileInfo(logFilePath);
                if (_FileInfo.Exists)
                {
                    Thread.Sleep(1500);
                }

            } while (_FileInfo.Exists);
            _CurrentWriteMap = WriteMap;
            _StreamWriter = _FileInfo.AppendText();
            _StreamWriter.AutoFlush = true;
            _StreamWriter.Write(FileBegin);
        }
    }
}
