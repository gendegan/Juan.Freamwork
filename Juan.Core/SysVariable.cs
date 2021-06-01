using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace Juan.Core
{
    public static class SysVariable
    {
        // Methods
        public static IPAddress GetLocalIp()
        {
            IPHostEntry entry = new IPHostEntry
            {
                AddressList = new IPAddress[1]
            };
            int index = 0;
            for (index = 0; index < Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length; index++)
            {
                if (Dns.GetHostEntry(Dns.GetHostName()).AddressList[index].AddressFamily == AddressFamily.InterNetwork)
                {
                    entry.AddressList[0] = Dns.GetHostEntry(Dns.GetHostName()).AddressList[index];
                    break;
                }
            }
            return entry.AddressList[0];
        }

        public static string GetRealIp()
        {
            if ((CurrentContext == null) || (CurrentContext.Request == null))
            {
                return "";
            }
            if ((CurrentContext.Request.ServerVariables != null) && (CurrentContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]!=null))
            {
                string str2 = CurrentContext.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                if (!string.IsNullOrWhiteSpace(str2))
                {
                    char[] separator = new char[] { ',', ' ' };
                    string[] strArray = str2.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    if (strArray.Length > 0)
                    {
                        return strArray[0].ToString().Trim();
                    }
                }
            }
            if ((CurrentContext.Request.Headers != null) && (CurrentContext.Request.Headers["X-Real-IP"] != null))
            {
                return CurrentContext.Request.Headers["X-Real-IP"].ToString();
            }
            return CurrentContext.Request.UserHostAddress;
        }

        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int connectionDescription, int reservedValue);
        public static bool IsInternetConnected()
        {
            int num;
            return InternetGetConnectedState(out num, 0);
        }

        // Properties
        public static string ApplicationPath
        {
            get
            {
                return (CurrentContext.Request.ApplicationPath.EndsWith("/") ? "" : CurrentContext.Request.ApplicationPath);
            }
        }

        public static string BaseDirectory
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        public static HttpContext CurrentContext
        {
            get
            {
                return HttpContext.Current;
            }
        }

        public static IHttpHandler CurrentHandler
        {
            get
            {
                return HttpContext.Current.Handler;
            }
        }

        public static Page CurrentPage
        {
            get
            {
                return (Page)HttpContext.Current.Handler;
            }
        }

        public static string CurrentThreadName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Thread.CurrentThread.Name))
                {
                    try
                    {
                        Thread.CurrentThread.Name = Guid.NewGuid().ToString().Replace("-", "");
                    }
                    catch
                    {
                    }
                }
                return Thread.CurrentThread.Name;
            }
        }
    }

}
