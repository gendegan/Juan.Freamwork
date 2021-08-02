using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;

namespace Juan.Core
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public static partial class LogHelper
    {

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="logTitle">日志标题</param>
        /// <param name="exception">异常信息</param>
        public static void Write(string logTitle, Exception exception)
        {
            Write(LogType.Error, logTitle, exception, null);
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="logTitle"></param>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        public static void Write(string logTitle, Exception exception, object message)
        {
            Write(LogType.Error, logTitle, exception, message);
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="logTitle">日志标题</param>
        /// <param name="exception">输入日志</param>
        public static void Write(string logTitle, InputNullException exception)
        {
            Write(LogType.InputError, logTitle, exception, null);
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="logTitle">日志标题</param>
        /// <param name="message">日志内容</param>
        public static void Write(LogType logType, string logTitle, object message)
        {
            Write(logType, logTitle, message, null);
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="accountID">帐号标识</param>
        /// <param name="messageID">消息标识</param>
        /// <param name="deviceID">设备标识</param>
        public static void SetInitValue(string accountID, string messageID, string deviceID)
        {
            if (SysVariable.CurrentContext == null)
            {

                return;
            }
            if (accountID.IsNoNullOrWhiteSpace())
            {
                SysVariable.CurrentContext.Items["Log_AccountID"] = accountID;
            }
            if (messageID.IsNoNullOrWhiteSpace())
            {
                SysVariable.CurrentContext.Items["Log_MessageID"] = messageID;

            }
            if (deviceID.IsNoNullOrWhiteSpace())
            {
                SysVariable.CurrentContext.Items["Log_DeviceID"] = deviceID;
            }
        }
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="logType">日志类型</param>
        /// <param name="logTitle">日志标题</param>
        /// <param name="message">日志内容</param>
        /// <param name="resultMessage">结果内容</param>
        public static void Write(LogType logType, string logTitle, object message, object resultMessage)
        {
            try
            {
                ILog.Log.Write(logType, logTitle, message, resultMessage);
            }
            catch (Exception objExp)
            {
                EventLogHelper.WriterLog(logTitle, message, resultMessage);
                EventLogHelper.WriterLog("日志帮助类写入数据异常", objExp);
            }
        }




        private static void ProcessHttp404(string message)
        {

            HttpResponse objHttpResponse = SysVariable.CurrentContext.Response;
            LogSection objLogSection = LogSectionHelper.GetLogSection();
            string NoFoundUrl = objLogSection.Error.NoFoundUrl;
            objHttpResponse.ContentType = "text/html";
            if (NoFoundUrl.IsNoNullOrWhiteSpace())
            {
                try
                {
                    objHttpResponse.WriteFile(NoFoundUrl);
                }
                catch (Exception objExp)
                {
                    LogHelper.Write("Http404文件写入异常", objExp);
                    objHttpResponse.Write(message);
                }
            }
            else
            {
                objHttpResponse.Write(message);
            }
            objHttpResponse.StatusCode = (int)HttpStatusCode.NotFound;
            SysVariable.CurrentContext.Server.ClearError();
            objHttpResponse.TrySkipIisCustomErrors = true;
        }

        private static void ProcessHttp500(string message)
        {
            HttpResponse objHttpResponse = SysVariable.CurrentContext.Response;
            LogSection objLogSection = LogSectionHelper.GetLogSection();
            string ErrorUrl = LogSectionHelper.GetLogSection().Error.ErrorUrl;
            if (message.IsNullOrWhiteSpace())
            {
                message = LogSectionHelper.GetLogSection().Error.ErrorHint;
            }
            objHttpResponse.ContentType = "text/html";

            if (objLogSection.Error.IsRedirect && ErrorUrl.IsNoNullOrWhiteSpace())
            {
                objHttpResponse.StatusCode = 302;
                objHttpResponse.AddHeader("Location", ErrorUrl);
            }
            else
            {
                if (ErrorUrl.IsNoNullOrWhiteSpace())
                {
                    try
                    {
                        objHttpResponse.WriteFile(ErrorUrl);
                    }
                    catch (Exception objExp)
                    {
                        LogHelper.Write("Http500文件写入异常", objExp);
                        objHttpResponse.Write(message);
                    }
                }
                else
                {
                    objHttpResponse.Write(message);
                }
                objHttpResponse.StatusCode = 500;
            }
            SysVariable.CurrentContext.Server.ClearError();
            objHttpResponse.TrySkipIisCustomErrors = true;

        }

        /// <summary>
        /// 释放异常信息
        /// </summary>
        /// <param name="objException"></param>
        /// <param name="Disposeing"></param>
        public static void DisposeException(Exception objException, Func<Exception, bool> Disposeing = null)
        {
            Exception objProcessException = objException;
            while (objProcessException.InnerException != null)
            {
                objProcessException = objProcessException.InnerException;
            }
            Type type = objProcessException.GetType();
            if (Disposeing != null)
            {
                if (Disposeing(objException))
                {
                    return;
                }
            }

            HttpResponse objHttpResponse = SysVariable.CurrentContext.Response;

            if (type == typeof(ThreadAbortException))
            {
                SysVariable.CurrentContext.Server.ClearError();
            }

            else if (objProcessException is Http404Exception)
            {
                Juan.Core.Http404Exception objHttp404Exception = (Http404Exception)objProcessException;
                ProcessHttp404(objHttp404Exception.Message);
            }
            else if (objProcessException is HttpException && ((HttpException)objProcessException).GetHttpCode() == 404)
            {
                HttpException objHttpException = (HttpException)objProcessException;
                ProcessHttp404(objHttpException.Message);

            }
            //请求地址无效
            else if (objProcessException is HttpException && ((HttpException)objProcessException).GetHttpCode() == 400)
            {
                ProcessHttp500("");
            }

            else if (objProcessException is Http301Exception)
            {
                Http301Exception objHttp301Exception = objProcessException as Http301Exception;
                objHttpResponse.StatusCode = (int)HttpStatusCode.Moved;
                objHttpResponse.Status = "301 Moved Permanently";
                objHttpResponse.AddHeader("Location", objHttp301Exception.RedirectUrl);
                SysVariable.CurrentContext.Server.ClearError();
            }


            else if (objProcessException is Http302Exception)
            {

                Http302Exception objHttp302Exception = objProcessException as Http302Exception;
                objHttpResponse.StatusCode = (int)HttpStatusCode.Found;
                objHttpResponse.AddHeader("Location", objHttp302Exception.RedirectUrl);
                SysVariable.CurrentContext.Server.ClearError();
            }

            else if (objProcessException is Http500Exception)
            {
                Http500Exception objHttp500Exception = objProcessException as Http500Exception;
                ProcessHttp500(objHttp500Exception.Message);

            }

            else if (objProcessException is HttpRequestValidationException)
            {
                objHttpResponse.ContentType = "text/html";
                Write(LogType.Attack, "Application_Error", objException);
                ProcessHttp500("");
            }
            else
            {

                if (LogSectionHelper.IsDispose)
                {
                    Write(LogType.Error, "Application_Error", objException);
                    ProcessHttp500("");
                }
            }

        }

    }

}
