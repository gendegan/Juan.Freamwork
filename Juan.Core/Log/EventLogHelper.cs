using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    /// <summary>
    /// 事件查看器日志
    /// </summary>
    public static class EventLogHelper
    {


        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="message">信息</param>
        public static void WriterLog(string message)
        {

            EventLog.WriteEntry("Application", message.ToString(), EventLogEntryType.Error);

        }
        /// <summary>
        /// 获取进程名
        /// </summary>
        /// <returns></returns>
        static string GetProcessName()
        {
            StringBuilder buffer = new StringBuilder(1024);
            int length = NativeMethods.GetModuleFileName(NativeMethods.GetModuleHandle(null), buffer, buffer.Capacity);
            return buffer.ToString();
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="title"></param>
        /// <param name="value">值</param>
        public static void WriterLog(string title, object value)
        {

            StringBuilder objFormattedMessage = new StringBuilder();
            objFormattedMessage.Append("运行程序：").Append(GetProcessName()).Append("\r\n");
            objFormattedMessage.Append("异常日期：").Append(DateTime.Now.ToString()).Append("\r\n");
            objFormattedMessage.Append("日志标题：").Append(title + "\r\n");
            try
            {
                if (value == null)
                {
                    objFormattedMessage.Append("日志内容：").Append("null").Append("\r\n");
                }
                if (value is string)
                {
                    objFormattedMessage.Append("日志内容：").Append(value.ToString()).Append("\r\n");
                }
                else if (value is Exception)
                {

                    Exception objException = (Exception)value;
                    objFormattedMessage.Append("日志内容：").Append(objException.Message).Append("\r\n");
                    objFormattedMessage.Append("调用堆栈：").Append(objException.StackTrace).Append("\r\n");
                    while (objException.InnerException != null)
                    {
                        objFormattedMessage.Append("内部事件信息：").Append(objException.InnerException.Message).Append("\r\n");
                        objFormattedMessage.Append("内部堆栈跟踪：").Append(objException.InnerException.StackTrace).Append("\r\n");
                        objException = objException.InnerException;
                    }
                    objFormattedMessage.Append("\r\n-----------------------------------------\r\n");
                }
                else
                {
                    objFormattedMessage.Append("日志内容：").Append(value.JsonSerialize()).Append("\r\n");

                }
            }
            catch (Exception objExp)
            {
                objFormattedMessage.Append("消息转换出现异常：").Append(objExp.Message).Append("\r\n");
            }

            WriterLog(objFormattedMessage.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="value">值</param>
        /// <param name="resultValue"></param>
        public static void WriterLog(string title, object value, object resultValue)
        {

            StringBuilder objFormattedMessage = new StringBuilder();
            objFormattedMessage.Append("运行程序：").Append(GetProcessName()).Append("\r\n");
            objFormattedMessage.Append("异常日期：").Append(DateTime.Now.ToString()).Append("\r\n");
            objFormattedMessage.Append("日志标题：").Append(title + "\r\n");
            try
            {
                if (value == null)
                {
                    objFormattedMessage.Append("日志内容：").Append("null").Append("\r\n");
                }
                if (value is string)
                {
                    objFormattedMessage.Append("日志内容：").Append(value.ToString()).Append("\r\n");
                }
                else if (value is Exception)
                {

                    Exception objException = (Exception)value;
                    objFormattedMessage.Append("日志内容：").Append(objException.Message).Append("\r\n");
                    objFormattedMessage.Append("调用堆栈：").Append(objException.StackTrace).Append("\r\n");
                    while (objException.InnerException != null)
                    {
                        objFormattedMessage.Append("内部事件信息：").Append(objException.InnerException.Message).Append("\r\n");
                        objFormattedMessage.Append("内部堆栈跟踪：").Append(objException.InnerException.StackTrace).Append("\r\n");
                        objException = objException.InnerException;
                    }
                    objFormattedMessage.Append("\r\n-----------------------------------------\r\n");
                }
                else
                {
                    objFormattedMessage.Append("日志内容：").Append(value.JsonSerialize()).Append("\r\n");

                }



                if (resultValue == null)
                {
                    objFormattedMessage.Append("结果日志内容：").Append("null").Append("\r\n");
                }
                if (resultValue is string)
                {
                    objFormattedMessage.Append("结果日志内容：").Append(resultValue.ToString()).Append("\r\n");
                }
                else if (resultValue is Exception)
                {

                    Exception objException = (Exception)resultValue;
                    objFormattedMessage.Append("结果日志内容：").Append(objException.Message).Append("\r\n");
                    objFormattedMessage.Append("结果调用堆栈：").Append(objException.StackTrace).Append("\r\n");
                    while (objException.InnerException != null)
                    {
                        objFormattedMessage.Append("结果内部事件信息：").Append(objException.InnerException.Message).Append("\r\n");
                        objFormattedMessage.Append("结果内部堆栈跟踪：").Append(objException.InnerException.StackTrace).Append("\r\n");
                        objException = objException.InnerException;
                    }
                    objFormattedMessage.Append("\r\n-----------------------------------------\r\n");
                }
                else
                {
                    objFormattedMessage.Append("结果日志内容：").Append(resultValue.JsonSerialize()).Append("\r\n");

                }
            }
            catch (Exception objExp)
            {
                objFormattedMessage.Append("消息转换出现异常：").Append(objExp.Message).Append("\r\n");
            }

            WriterLog(objFormattedMessage.ToString());
        }



    }
}
