using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Juan.Core;
using System.Web;
using Juan.Log.Context;


namespace Juan.Log
{
    /// <summary>
    /// 日志信息
    /// </summary>
    internal partial class LogInfo
    {

        public static string Host = "";
        public static string ProcessName = "";
        private static ApplicationContext _ApplicationContext = null;
        static string GetProcessName()
        {
            StringBuilder buffer = new StringBuilder(1024);
            int length = NativeMethods.GetModuleFileName(NativeMethods.GetModuleHandle(null), buffer, buffer.Capacity);
            return buffer.ToString();
        }
        static LogInfo()
        {
            _ApplicationContext = new ApplicationContext(ConnectionType.Write);
            _ApplicationContext.Context.CloseExecute();
            Host = LogSectionHelper.Host;
            ProcessName = GetProcessName();
        }


        /// <summary>
        /// 日志分类
        /// </summary>
        public string LogType
        {
            get;
            set;
        }


        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get;
            set;
        }



        /// <summary>
        /// 消息
        /// </summary>
        public string Message
        {
            get;
            set;

        }
        /// <summary>
        /// 结果消息
        /// </summary>
        public string ResultMessage
        {
            get;
            set;
        }
        /// <summary>
        /// 请求地址
        /// </summary>
        public string Url
        {
            get;
            set;
        }

        /// <summary>
        /// 请求地址的原地址
        /// </summary>
        public string UrlReferrer
        {
            get;
            set;
        }

        /// <summary>
        /// UserAgent
        /// </summary>
        public string UserAgent
        {
            get;
            set;
        }

        /// <summary>
        /// 头部信息
        /// </summary>
        public Dictionary<string, string> HeaderData
        {
            get;
            set;
        }

        /// <summary>
        /// 请求信息数据
        /// </summary>
        public Dictionary<string, string> FormData
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> CookieData
        {
            get;
            set;
        }



        /// <summary>
        /// 用户IP地址
        /// </summary>
        public string UserIP
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string UserID
        {
            get;
            set;
        }
        public string MessageID
        {
            get;
            set;
        }
        public string DeviceID
        {
            get;
            set;
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            get;
            set;
        }

        public string CurrentThreadID
        {
            get;
            set;
        }
        /// <summary>
        /// 日志信息
        /// </summary>
        public LogInfo()
        {
            this.CurrentThreadID = SysVariable.CurrentThreadName;
            this.CreateDate = DateTime.Now;
            this.UserID = "";
            this.Url = "";
            this.UrlReferrer = "";
            this.UserAgent = "";
            this.Title = "";
            this.Message = "";
            this.ResultMessage = "";
            this.HeaderData = new Dictionary<string, string>();
            this.FormData = new Dictionary<string, string>();
            this.CookieData = new Dictionary<string, string>();
            this.UserIP = "";
            this.MessageID = "";
            this.DeviceID = "";
        }

        public bool ProcessLogData()
        {

            LogSection objLogSection = LogSectionHelper.GetLogSection();
            try
            {
                if (string.IsNullOrWhiteSpace(objLogSection.Application))
                {
                    return false;
                }

                WritePath = objLogSection.WritePath;


                if (string.IsNullOrWhiteSpace(ApplicationCode))
                {
                    ApplicationCode = objLogSection.Application;
                }
                this.Application = _ApplicationContext.GetCacheApplication(ApplicationCode);
                if (this.Application == null)
                {
                    EventLogHelper.WriterLog("请检查日志程序代码", objLogSection.Application + "读不到日志配置");
                    return false;
                }

                HttpContext objCurrentContext = SysVariable.CurrentContext;
                if (objCurrentContext == null || objCurrentContext.Request == null)
                {
                    return true;
                }
                this.Url = objCurrentContext.Request.Url == null ? "" : SysVariable.CurrentContext.Request.Url.ToString();
                this.UrlReferrer = objCurrentContext.Request.UrlReferrer == null ? "" : objCurrentContext.Request.UrlReferrer.ToString();
                this.UserAgent = objCurrentContext.Request.UserAgent == null ? "" : objCurrentContext.Request.UserAgent;
                this.UserIP = SysVariable.GetRealIp();
                if (!string.IsNullOrWhiteSpace(this.Application.HeaderKey))
                {
                    if (this.Application.HeaderKey == "*")
                    {
                        foreach (string HeaderKey in objCurrentContext.Request.Headers.Keys)
                        {


                            this.HeaderData[HeaderKey] = objCurrentContext.Request.Headers[HeaderKey];
                        }
                    }
                    else
                    {
                        foreach (string HeaderKey in this.Application.HeaderKey.ToList<string>())
                        {


                            this.HeaderData[HeaderKey] = objCurrentContext.Request.Headers[HeaderKey];
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(this.Application.FormKey))
                {
                    if (this.Application.FormKey == "*")
                    {
                        foreach (string FormKey in objCurrentContext.Request.Form.Keys)
                        {
                            if (FormKey.StartsWith("__"))
                            {
                                continue;
                            }
                            this.FormData[FormKey] = objCurrentContext.Request.Form[FormKey];
                        }
                    }
                    else
                    {

                        foreach (string FormKey in this.Application.FormKey.ToList<string>())
                        {
                            this.FormData[FormKey] = objCurrentContext.Request.Form[FormKey];
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(this.Application.CookieKey))
                {
                    if (this.Application.CookieKey == "*")
                    {
                        foreach (string CookieKey in objCurrentContext.Request.Cookies.Keys)
                        {
                            this.CookieData[CookieKey] = objCurrentContext.Request.Cookies[CookieKey].Value;
                        }
                    }
                    else
                    {
                        foreach (string CookieKey in this.Application.CookieKey.ToList<string>())
                        {
                            HttpCookie objHttpCookie = objCurrentContext.Request.Cookies[CookieKey];
                            if (objHttpCookie != null)
                            {
                                this.CookieData[CookieKey] = objHttpCookie.Value;
                            }
                            else
                            {
                                this.CookieData[CookieKey] = "";
                            }
                        }
                    }
                }
                if (objCurrentContext.Session != null)
                {
                    object LogUserID = objCurrentContext.Session["LogUserID"];
                    if (LogUserID != null)
                    {
                        this.UserID = LogUserID.ToString();
                    }
                }
                object log_AccountID = objCurrentContext.Items["Log_AccountID"];
                object log_MessageID = objCurrentContext.Items["Log_MessageID"];
                object log_DeviceID = objCurrentContext.Items["Log_DeviceID"];
                if (log_AccountID != null)
                {
                    this.UserID = log_AccountID.ToString();
                }
                if (log_MessageID != null)
                {
                    this.MessageID = log_MessageID.ToString();
                }
                if (log_DeviceID != null)
                {
                    this.DeviceID = log_DeviceID.ToString();
                }
                return true;
            }
            catch (Exception objExp)
            {
                try
                {
                    EventLogHelper.WriterLog("LogInfo日志信息处理异常ProcessLogData", objExp);
                }
                catch
                {
                }
                if (this.Application != null)
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
        }

    }
}
