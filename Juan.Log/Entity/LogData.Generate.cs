using System;
using System.Collections;
using System.Runtime.Serialization;
using Juan.Core;

namespace Juan.Log.Entity
{
    #region LogData
    /// <summary>
    /// 日志数据表 实体层
    /// </summary>
    [Table(TableName = "log_logdata_tb",VerNo = "3F2DED0D8975126D6111FBDDBED0FF01", ViewName = "log_logdata_vw", TableFormat = "log_logdata_{0}_tb", ViewFormat = "log_logdata_{0}_vw")]
    [Context()]
    [Serializable]
    public partial class LogData : IData
    {
        #region  构造函数 
        public LogData()
        {
         LogID=0;
         ApplicationID=0;
         ApplicationName=String.Empty;
         ApplicationHost=String.Empty;
         CurrentThreadID=String.Empty;
         Url=String.Empty;
         UrlReferrer=String.Empty;
         UserAgent=String.Empty;
         UserID=String.Empty;
         UserIP=String.Empty;
         ProcessName=String.Empty;
         CreateDate=System.DateTime.Now;
         LogDate=System.DateTime.Now;
         Title=String.Empty;
         Message=String.Empty;
         ResultMessage=String.Empty;
         LogType=String.Empty;
         IDPath=String.Empty;
         HeadersData=String.Empty;
         FormData=String.Empty;
         CookiesData=String.Empty;
         MessageID=String.Empty;
         DeviceID=String.Empty;
        }
        #endregion 

        #region 属性 
        /// <summary>
        /// 日志标识
        /// </summary>
        [PrimaryKey]
        public Int32 LogID
        {
            get;
            set;
        }
        /// <summary>
        /// 程序标识
        /// </summary>
        public Int32 ApplicationID
        {
            get;
            set;
        }
        /// <summary>
        /// 程序名称
        /// </summary>
        public String ApplicationName
        {
            get;
            set;
        }
        /// <summary>
        /// 运行程序名称
        /// </summary>
        public String ApplicationHost
        {
            get;
            set;
        }
        /// <summary>
        /// 当前线程标识
        /// </summary>
        public String CurrentThreadID
        {
            get;
            set;
        }
        /// <summary>
        /// 当前地址
        /// </summary>
        public String Url
        {
            get;
            set;
        }
        /// <summary>
        /// 上一次地址
        /// </summary>
        public String UrlReferrer
        {
            get;
            set;
        }
        /// <summary>
        /// UserAgent信息
        /// </summary>
        public String UserAgent
        {
            get;
            set;
        }
        /// <summary>
        /// 用户标识
        /// </summary>
        public String UserID
        {
            get;
            set;
        }
        /// <summary>
        /// 用户IP
        /// </summary>
        public String UserIP
        {
            get;
            set;
        }
        /// <summary>
        /// 处理名称
        /// </summary>
        public String ProcessName
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
        /// <summary>
        /// 日志时间
        /// </summary>
        [DbIgnore(Ignore = IgnoreType.All)]
        public DateTime LogDate
        {
            get;
            set;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public String Title
        {
            get;
            set;
        }
        /// <summary>
        /// 日志消息
        /// </summary>
        public String Message
        {
            get;
            set;
        }
        /// <summary>
        /// 结果消息
        /// </summary>
        public String ResultMessage
        {
            get;
            set;
        }
        /// <summary>
        /// 日志类型
        /// </summary>
        public String LogType
        {
            get;
            set;
        }
        /// <summary>
        /// 路经
        /// </summary>
        public String IDPath
        {
            get;
            set;
        }
        /// <summary>
        /// 头部信息
        /// </summary>
        public String HeadersData
        {
            get;
            set;
        }
        /// <summary>
        /// Post信息
        /// </summary>
        public String FormData
        {
            get;
            set;
        }
        /// <summary>
        /// Cookie信息
        /// </summary>
        public String CookiesData
        {
            get;
            set;
        }
        /// <summary>
        /// MessageID
        /// </summary>
        public String MessageID
        {
            get;
            set;
        }
        /// <summary>
        /// DeviceID
        /// </summary>
        public String DeviceID
        {
            get;
            set;
        }
    #endregion 
    }
    #endregion 




}
