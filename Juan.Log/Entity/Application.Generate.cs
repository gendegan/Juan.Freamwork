using System;
using System.Collections;
using System.Runtime.Serialization;
using Juan.Core;

namespace Juan.Log.Entity
{
    #region Application
    /// <summary>
    /// 日志程序表 实体层
    /// </summary>
    [Table(TableName = "log_application_tb",VerNo = "C10750C002188176A92EBFC2F5E4E3F9", ViewName = "log_application_vw", TableFormat = "log_application_{0}_tb", ViewFormat = "log_application_{0}_vw")]
    [Context()]
    [Serializable]
    public partial class Application : IData
    {
        #region  构造函数 
        public Application()
        {
         ApplicationID=0;
         ApplicationCode=String.Empty;
         ApplicationName=String.Empty;
         ParentID=0;
         IDPath=String.Empty;
         SortIndex=0;
         CreateDate=System.DateTime.Now;
         NoticeCheckInfo=String.Empty;
         LogStoreInfo=String.Empty;
         NoticePhone=String.Empty;
         NoticeInterval=0;
         IsNotice=0;
         HeaderKey=String.Empty;
         FormKey=String.Empty;
         CookieKey=String.Empty;
        }
        #endregion 

        #region 属性 
        /// <summary>
        /// 程序标识
        /// </summary>
        [PrimaryKey]
        public Int32 ApplicationID
        {
            get;
            set;
        }
        /// <summary>
        /// 程序代码
        /// </summary>
        public String ApplicationCode
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
        /// 父标识
        /// </summary>
        public Int32 ParentID
        {
            get;
            set;
        }
        /// <summary>
        /// 父路经
        /// </summary>
        public String IDPath
        {
            get;
            set;
        }
        /// <summary>
        /// 排序
        /// </summary>
        public Int32 SortIndex
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
        /// 日志通知检查
        /// </summary>
        public String NoticeCheckInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 日志存储信息
        /// </summary>
        public String LogStoreInfo
        {
            get;
            set;
        }
        /// <summary>
        /// 通知手机号码
        /// </summary>
        public String NoticePhone
        {
            get;
            set;
        }
        /// <summary>
        /// 通知间隔分钟数
        /// </summary>
        public Int32 NoticeInterval
        {
            get;
            set;
        }
        /// <summary>
        /// 是否通知
        /// </summary>
        public Int32 IsNotice
        {
            get;
            set;
        }
        /// <summary>
        /// 头部键值
        /// </summary>
        public String HeaderKey
        {
            get;
            set;
        }
        /// <summary>
        /// Form键值
        /// </summary>
        public String FormKey
        {
            get;
            set;
        }
        /// <summary>
        /// Cookie键值
        /// </summary>
        public String CookieKey
        {
            get;
            set;
        }
    #endregion 
    }
    #endregion 




}
