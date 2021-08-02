using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{

    /// <summary>
    /// 日志节点
    /// </summary>
    public class LogSection : ConfigurationSection
    {

        /// <summary>
        /// 运行程序代码
        /// </summary>
        [ConfigurationProperty("Application", IsRequired = true, DefaultValue = "")]
        public string Application
        {
            get
            {
                return (string)base["Application"];
            }
            set
            {
                base["Application"] = value;
            }
        }
        /// <summary>
        /// 运行程序IP
        /// </summary>
        [ConfigurationProperty("Host", IsRequired = false, DefaultValue = "")]
        public string Host
        {
            get
            {
                return (string)base["Host"];
            }
            set
            {
                base["Host"] = value;
            }
        }
        /// <summary>
        /// 是否自动释放
        /// </summary>
        [ConfigurationProperty("IsDispose", IsRequired = false, DefaultValue = true)]
        public bool IsDispose
        {
            get
            {
                return (bool)base["IsDispose"];
            }
            set
            {
                base["IsDispose"] = value;
            }
        }
        /// <summary>
        /// 是否记录SQl语句
        /// </summary>
        [ConfigurationProperty("IsSql", IsRequired = false, DefaultValue = false)]
        public bool IsSql
        {
            get
            {
                return (bool)base["IsSql"];
            }
            set
            {
                base["IsSql"] = value;
            }
        }

        /// <summary>
        /// 是否记录操作日志
        /// </summary>
        [ConfigurationProperty("IsOperation", IsRequired = false, DefaultValue = false)]
        public bool IsOperation
        {
            get
            {
                return (bool)base["IsOperation"];
            }
            set
            {
                base["IsOperation"] = value;
            }
        }
        /// <summary>
        /// 是否记录SolrSQl语句
        /// </summary>
        [ConfigurationProperty("IsSolr", IsRequired = false, DefaultValue = false)]
        public bool IsSolr
        {

            get
            {
                return (bool)base["IsSolr"];
            }
            set
            {
                base["IsSolr"] = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty("IsESearch", IsRequired = false, DefaultValue = false)]
        public bool IsESearch
        {

            get
            {
                return (bool)base["IsESearch"];
            }
            set
            {
                base["IsESearch"] = value;
            }
        }


        /// <summary>
        /// 日志存储路经
        /// </summary>
        [ConfigurationProperty("WritePath", IsRequired = false, DefaultValue = "")]
        public string WritePath
        {
            get
            {
                return (string)base["WritePath"];
            }
            set
            {
                base["WritePath"] = value;
            }
        }


        /// <summary>
        /// 错误节点
        /// </summary>
        [ConfigurationProperty("Error", IsRequired = false)]
        public LogErrorElement Error
        {
            get
            {
                return (LogErrorElement)base["Error"];

            }
            set
            {
                base["Error"] = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class LogErrorElement : ConfigurationElement
        {

            /// <summary>
            /// 错误页跳转地址
            /// </summary>
            [ConfigurationProperty("ErrorUrl", IsRequired = false, DefaultValue = "Error.htm")]
            public string ErrorUrl
            {
                get
                {
                    return (string)base["ErrorUrl"];
                }
                set
                {
                    base["ErrorUrl"] = value;
                }
            }

            /// <summary>
            /// 错误页视图
            /// </summary>
            [ConfigurationProperty("ErrorView", IsRequired = false, DefaultValue = "")]
            public string ErrorView
            {
                get
                {
                    return (string)base["ErrorView"];
                }
                set
                {
                    base["ErrorView"] = value;
                }
            }

            /// <summary>
            /// ErrorNoFoundUrl
            /// </summary>
            [ConfigurationProperty("NoFoundUrl", IsRequired = false, DefaultValue = "404.htm")]
            public string NoFoundUrl
            {
                get
                {
                    return (string)base["NoFoundUrl"];
                }
                set
                {
                    base["NoFoundUrl"] = value;
                }
            }



            /// <summary>
            /// 错误页跳转地址
            /// </summary>
            [ConfigurationProperty("NoFoundView", IsRequired = false, DefaultValue = "")]
            public string NoFoundView
            {
                get
                {
                    return (string)base["NoFoundView"];
                }
                set
                {
                    base["NoFoundView"] = value;
                }
            }


            /// <summary>
            /// 错误提示信息
            /// </summary>
            [ConfigurationProperty("ErrorHint", IsRequired = false, DefaultValue = "对不起页面出错，请与管理员联系")]
            public string ErrorHint
            {
                get
                {
                    return (string)base["ErrorHint"];
                }
                set
                {
                    base["ErrorHint"] = value;
                }
            }
            /// <summary>
            /// 错误是否跳转
            /// </summary>
            [ConfigurationProperty("IsRedirect", IsRequired = false, DefaultValue = false)]
            public bool IsRedirect
            {
                get
                {
                    return (bool)base["IsRedirect"];
                }
                set
                {
                    base["IsRedirect"] = value;
                }
            }
        }
    }




    /// <summary>
    /// 获取日志节点配置
    /// </summary>
    public class LogSectionHelper
    {

        /// <summary>
        /// 获取日志节点
        /// </summary>
        /// <returns></returns>
        public static LogSection GetLogSection()
        {
            return (LogSection)ConfigHelper.GetSection("LogConfig", "LogConfig");
        }
        /// <summary>
        /// 程序运行
        /// </summary>
        public static string ApplicationCode
        {
            get
            {
                string application = GetLogSection().Application;
                if (string.IsNullOrWhiteSpace(application))
                {
                    throw new Exception("LogConfig 未配置Application值");
                }
                return application;

            }
        }
        /// <summary>
        /// 日志存储目录
        /// </summary>
        public static string WritePath
        {
            get
            {
                return GetLogSection().WritePath;
            }
        }
        /// <summary>
        /// 是否自动释放
        /// </summary>
        public static bool IsDispose
        {
            get
            {
                return GetLogSection().IsDispose;
            }
        }

        /// <summary>
        /// 是否记录Sql
        /// </summary>
        public static bool IsSql
        {
            get
            {
                return GetLogSection().IsSql;
            }
        }

        /// <summary>
        /// 是否记录操作日志
        /// </summary>
        public static bool IsOperation
        {
            get
            {
                return GetLogSection().IsOperation;
            }
        }
        /// <summary>
        /// 是否记录Sql
        /// </summary>
        public static bool IsSolr
        {
            get
            {
                return GetLogSection().IsSolr;
            }
        }
        /// <summary>
        /// 是否记录Es查询语句
        /// </summary>
        public static bool IsESearch
        {
            get
            {
                return GetLogSection().IsESearch;
            }
        }


        /// <summary>
        /// 日志记录IP
        /// </summary>
        public static string Host
        {
            get
            {
                LogSection objLogSection = GetLogSection();
                string host = objLogSection != null ? objLogSection.Host : "";
                if (host.IsNull())
                {
                    host = SysVariable.GetLocalIp().ToString().ToString();
                }
                return host;
            }
        }

    }
}
