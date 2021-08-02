using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Juan.Core;

namespace Juan.Context.Test.Entity
{
    #region WhiteBlack
    /// <summary>
    /// 评论黑白名单 实体层
    /// </summary>
    [Table(TableName = "bizhi_whiteblack_tb", VerNo = "1922FA888C9F86CC73874EB7ABDCE211", ViewName = "bizhi_whiteblack_vw", TableFormat = "bizhi_whiteblack_{0}_tb", ViewFormat = "bizhi_whiteblack_{0}_vw")]
    [Context("Gao7.BiZhi")]
    [Serializable]
    public partial class WhiteBlack : IData
    {
        #region  构造函数 
        public WhiteBlack()
        {
            ID = 0;
            GUID = System.Guid.NewGuid().ToString();
            UserID = 0;
            ListType = 0;
            ContentType = 0;
            StartTime = System.DateTime.Now;
            EndTime = System.DateTime.Now;
            CreateTime = System.DateTime.Now;
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
        /// UserID
        /// </summary>
        public Int32 UserID
        {
            get;
            set;
        }
        /// <summary>
        /// 名单类型(1-白名单,2-黑名单)
        /// </summary>
        public Int32 ListType
        {
            get;
            set;
        }
        /// <summary>
        /// 内容类型(1-评论,2-图集图片上传)
        /// </summary>
        public Int32 ContentType
        {
            get;
            set;
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime
        {
            get;
            set;
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime
        {
            get;
            set;
        }
        /// <summary>
        /// 最后一次举报时间
        /// </summary>
        public DateTime CreateTime
        {
            get;
            set;
        }
        #endregion
    }
    #endregion
}
