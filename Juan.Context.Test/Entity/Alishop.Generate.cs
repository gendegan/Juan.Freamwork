using System;
using System.Collections;
using System.Runtime.Serialization;
using Juan.Core;

namespace Juan.Context.Test.Entity
{
    #region Alishop
    /// <summary>
    /// 壁纸淘宝电商推荐信息 实体层
    /// </summary>
    [Table(TableName = "bizhi_alishop_tb",VerNo = "B349BD1A83FAC76ADB1F07A8D27B0762", ViewName = "bizhi_alishop_vw", TableFormat = "bizhi_alishop_{0}_tb", ViewFormat = "bizhi_alishop_{0}_vw")]
    [Context("Gao7.BiZhi")]
    [Serializable]
    public partial class Alishop : IData
    {
        #region  构造函数 
        public Alishop()
        {
         ID=0;
         GUID = System.Guid.NewGuid().ToString();
         Title=String.Empty;
         SubTitle=String.Empty;
         TitleColor=String.Empty;
         LinkUrl=String.Empty;
         Image1=String.Empty;
         Image2=String.Empty;
         Image3=String.Empty;
         Type=0;
         PID=0;
         CH=0;
         OrderNum=0;
         IsRelease=0;
         AddTime=System.DateTime.Now;
         Creator=String.Empty;
         D1=System.DateTime.Now;
         I1=0;
         I2=0;
         I3=0;
         S1=String.Empty;
         S2=String.Empty;
         S3=String.Empty;
        }
        #endregion 

        #region 属性 
        /// <summary>
        /// 标识ID
        /// </summary>
        [PrimaryKey]
        public Int32 ID
        {
            get;
            set;
        }
        /// <summary>
        /// GUID
        /// </summary>
        public String GUID
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
        /// 副标题
        /// </summary>
        public String SubTitle
        {
            get;
            set;
        }
        /// <summary>
        /// 标题字体颜色
        /// </summary>
        public String TitleColor
        {
            get;
            set;
        }
        /// <summary>
        /// 跳转链接
        /// </summary>
        public String LinkUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 图片1
        /// </summary>
        public String Image1
        {
            get;
            set;
        }
        /// <summary>
        /// 图片2
        /// </summary>
        public String Image2
        {
            get;
            set;
        }
        /// <summary>
        /// 图片3
        /// </summary>
        public String Image3
        {
            get;
            set;
        }
        /// <summary>
        /// 类型
        /// </summary>
        public Int32 Type
        {
            get;
            set;
        }
        /// <summary>
        /// PID
        /// </summary>
        public Int32 PID
        {
            get;
            set;
        }
        /// <summary>
        /// CH
        /// </summary>
        public Int32 CH
        {
            get;
            set;
        }
        /// <summary>
        /// 排序号
        /// </summary>
        public Int32 OrderNum
        {
            get;
            set;
        }
        /// <summary>
        /// 是否发布
        /// </summary>
        public Int32 IsRelease
        {
            get;
            set;
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime
        {
            get;
            set;
        }
        /// <summary>
        /// 添加人
        /// </summary>
        public String Creator
        {
            get;
            set;
        }
        /// <summary>
        /// 时间预留字段
        /// </summary>
        public DateTime D1
        {
            get;
            set;
        }
        /// <summary>
        /// 数字预留字段
        /// </summary>
        public Int32 I1
        {
            get;
            set;
        }
        /// <summary>
        /// 数字预留字段
        /// </summary>
        public Int32 I2
        {
            get;
            set;
        }
        /// <summary>
        /// 数字预留字段
        /// </summary>
        public Int32 I3
        {
            get;
            set;
        }
        /// <summary>
        /// 字符串预留字段
        /// </summary>
        public String S1
        {
            get;
            set;
        }
        /// <summary>
        /// 字符串预留字段
        /// </summary>
        public String S2
        {
            get;
            set;
        }
        /// <summary>
        /// 字符串预留字段
        /// </summary>
        public String S3
        {
            get;
            set;
        }
    #endregion 
    }
    #endregion 




}
