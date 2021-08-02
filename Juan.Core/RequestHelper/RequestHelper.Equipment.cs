using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    public static partial class RequestHelper
    {
        /// <summary>
        /// 获取当前域名地址
        /// </summary>
        public static string UrlHost
        {
            get
            {
                if (CurrentContext != null)
                {
                    return CurrentContext.Request.Url.Host;
                }
                return "www.Juan.com";
            }
        }

        /// <summary>
        /// 获取当前域名地址并加上Scheme
        /// </summary>
        public static string UrlHostScheme
        {
            get
            {
                return UrlScheme + "://" + UrlHost;
            }
        }
        /// <summary>
        /// UrlScheme
        /// </summary>
        public static string UrlScheme
        {
            get
            {
                if (CurrentContext != null)
                {
                    return CurrentContext.Request.Url.Scheme;
                }
                return "http";
            }
        }


        /// <summary>
        /// 获取主域
        /// </summary>
        public static string UrlHostDomain
        {
            get
            {
                string[] UrlHostArr = UrlHost.Split('.');
                if (UrlHostArr.Length >= 2)
                {
                    return UrlHostArr[UrlHostArr.Length - 2] + "." + UrlHostArr[UrlHostArr.Length - 1];
                }
                else
                {
                    return UrlHost;
                }
            }
        }

        /// <summary>
        /// 获取当前请求原地址
        /// </summary>
        public static string RawUrl
        {
            get
            {

                return CurrentContext.Request.RawUrl;
            }
        }


        /// <summary>
        /// 获取用户请求环境类型
        /// </summary>
        public static EquipmentType RequestEquipment
        {
            get
            {

                if (SysVariable.CurrentContext == null || SysVariable.CurrentContext.Request == null || string.IsNullOrWhiteSpace(SysVariable.CurrentContext.Request.UserAgent))
                {
                    return EquipmentType.Unknown;
                }
                string UserAgent = SysVariable.CurrentContext.Request.UserAgent;
                if (UserAgent.IndexOf("android", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return EquipmentType.Android;
                }
                else if (UserAgent.IndexOf("ipad", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return EquipmentType.Ipad;
                }

                else if (UserAgent.IndexOf("iphone", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return EquipmentType.Iphone;
                }
                else
                {
                    return EquipmentType.PC;
                }

            }
        }

        /// <summary>
        /// 是否是PC
        /// </summary>
        /// <param name="objEquipmentType"></param>
        /// <returns></returns>
        public static bool IsPC(this EquipmentType objEquipmentType)
        {
            return objEquipmentType == EquipmentType.PC || objEquipmentType == EquipmentType.Unknown;
        }
        /// <summary>
        /// 是否是手机
        /// </summary>
        /// <param name="objEquipmentType"></param>
        /// <returns></returns>
        public static bool IsPhone(this EquipmentType objEquipmentType)
        {
            return objEquipmentType != EquipmentType.PC && objEquipmentType != EquipmentType.Unknown;
        }
        /// <summary>
        /// 是否Ipad
        /// </summary>
        /// <param name="objEquipmentType"></param>
        /// <returns></returns>
        public static bool IsIpad(this EquipmentType objEquipmentType)
        {
            return objEquipmentType == EquipmentType.Ipad;
        }
        /// <summary>
        /// 是否Ios
        /// </summary>
        /// <param name="objEquipmentType"></param>
        /// <returns></returns>
        public static bool IsIos(this EquipmentType objEquipmentType)
        {
            return objEquipmentType == EquipmentType.Iphone || objEquipmentType == EquipmentType.Ipad;
        }

        /// <summary>
        /// 是否Android
        /// </summary>
        /// <param name="objEquipmentType"></param>
        /// <returns></returns>
        public static bool IsAndroid(this EquipmentType objEquipmentType)
        {
            return objEquipmentType == EquipmentType.Android;
        }
        /// <summary>
        /// 判断是否微信浏览器访问
        /// </summary>
        /// <returns></returns>
        public static bool IsWeiXinBrowse
        {
            get
            {
                if (string.IsNullOrWhiteSpace(CurrentContext.Request.UserAgent))
                {
                    return false;
                }
                return CurrentContext.Request.UserAgent.IndexOf("micromessenger", StringComparison.OrdinalIgnoreCase) >= 0;
            }
        }
        /// <summary>
        ///  获取当前请求浏览器名称
        /// </summary>
        public static string RequestSystemName
        {
            get
            {
                return SysVariable.CurrentContext.Request.UserAgent.GetOperatingSystemName();
            }
        }
        /// <summary>
        /// 根据 UserAgent 获取操作系统名称
        /// </summary>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public static string GetOperatingSystemName(this string userAgent)
        {
            if (userAgent.Contains("NT 6.1"))
            {
                return "Windows 7";
            }
            if (userAgent.Contains("NT 6.0"))
            {
                return "Windows Vista/Server 2008";
            }
            if (userAgent.Contains("NT 5.2"))
            {
                return "Windows Server 2003";
            }
            if (userAgent.Contains("NT 5.1"))
            {
                return "Windows XP";
            }
            if (userAgent.Contains("NT 5"))
            {
                return "Windows 2000";
            }
            if (userAgent.Contains("Mac"))
            {
                return "Mac";
            }
            if (userAgent.Contains("Unix"))
            {
                return "UNIX";
            }
            if (userAgent.Contains("Linux"))
            {
                return "Linux";
            }
            if (userAgent.Contains("SunOS"))
            {
                return "SunOS";
            }
            return "Other OperatingSystem";
        }
        /// <summary>
        /// 获取当前请求浏览器名称
        /// </summary>
        public static string RequestBrowserName
        {
            get
            {
                return SysVariable.CurrentContext.Request.UserAgent.GetBrowserName();
            }
        }

        /// <summary>
        /// 根据 UserAgent 获取浏览器名称
        /// </summary>
        public static string GetBrowserName(this string userAgent)
        {
            if (userAgent.Contains("Maxthon"))
            {
                return "遨游浏览器";
            }
            if (userAgent.Contains("MetaSr"))
            {
                return "搜狗高速浏览器";
            }
            if (userAgent.Contains("BIDUBrowser"))
            {
                return "百度浏览器";
            }
            if (userAgent.Contains("QQBrowser"))
            {
                return "QQ浏览器";
            }
            if (userAgent.Contains("GreenBrowser"))
            {
                return "Green浏览器";
            }
            if (userAgent.Contains("360se"))
            {
                return "360安全浏览器";
            }
            if (userAgent.Contains("MSIE 6.0"))
            {
                return "Internet Explorer 6.0";
            }
            if (userAgent.Contains("MSIE 7.0"))
            {
                return "Internet Explorer 7.0";
            }
            if (userAgent.Contains("MSIE 8.0"))
            {
                return "Internet Explorer 8.0";
            }
            if (userAgent.Contains("MSIE 9.0"))
            {
                return "Internet Explorer 9.0";
            }
            if (userAgent.Contains("MSIE 10.0"))
            {
                return "Internet Explorer 10.0";
            }
            if (userAgent.Contains("Firefox"))
            {
                return "Firefox";
            }
            if (userAgent.Contains("Opera"))
            {
                return "Opera";
            }
            if (userAgent.Contains("Chrome"))
            {
                return "Chrome";
            }
            if (userAgent.Contains("Safari"))
            {
                return "Safari";
            }
            return "Other Browser";
        }
    }

    /// <summary>
    /// 环境类型
    /// </summary>
    public enum EquipmentType
    {
        /// <summary>
        /// 未知类型
        /// </summary>
        [EnumAttribute("未知类型")]
        Unknown = 0,
        /// <summary>
        /// PC
        /// </summary>
        [EnumAttribute("PC")]
        PC = 1,
        /// <summary>
        /// Android
        /// </summary>
        [EnumAttribute("Android")]
        Android = 2,
        /// <summary>
        /// Ipad
        /// </summary>
        [EnumAttribute("Ipad")]
        Ipad = 3,
        /// <summary>
        /// Iphone
        /// </summary>
        [EnumAttribute("Iphone")]
        Iphone = 4

    }

}
