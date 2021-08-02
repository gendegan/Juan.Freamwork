using System;
using System.Collections;
using System.Runtime.Serialization;
using Juan.Core;

namespace Juan.Log.Entity
{
    #region OperationHistory
    /// <summary>
    /// 用户操作历史表 实体层
    /// </summary>
    [Table(TableName = "log_operationhistory",VerNo = "A64E82147D865F8D9C9C8FC572F8CEE6", ViewName = "log_operationhistory_vw", TableFormat = "log_operationhistory_{0}_tb", ViewFormat = "log_operationhistory_{0}_vw")]
    [Context()]
    [Serializable]
    public partial class OperationHistory : IData
    {
        #region  构造函数 
        public OperationHistory()
        {
         OperationHistoryID=0;
         MenuPowerID=String.Empty;
         MenuName=String.Empty;
         UserID=0;
         Account=String.Empty;
         OperationTypeID=0;
         CommandName=String.Empty;
         CreateDate=System.DateTime.Now;
         Title=String.Empty;
         Description=String.Empty;
         OperationData=String.Empty;
         UserHostAddress=String.Empty;
        }
        #endregion 

        #region 属性 
        /// <summary>
        /// 操作历史标识
        /// </summary>
        [PrimaryKey]
        public Int32 OperationHistoryID
        {
            get;
            set;
        }
        /// <summary>
        /// 菜单标识
        /// </summary>
        public String MenuPowerID
        {
            get;
            set;
        }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public String MenuName
        {
            get;
            set;
        }
        /// <summary>
        /// 用户标识
        /// </summary>
        public Int32 UserID
        {
            get;
            set;
        }
        /// <summary>
        /// 操作帐号
        /// </summary>
        public String Account
        {
            get;
            set;
        }
        /// <summary>
        /// 0无1新增2修改3删除4发布5取消发布6审核7取消审核8推荐9取消推荐
        /// </summary>
        public Int32 OperationTypeID
        {
            get;
            set;
        }
        /// <summary>
        /// 操作命名
        /// </summary>
        public String CommandName
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
        /// 标题
        /// </summary>
        public String Title
        {
            get;
            set;
        }
        /// <summary>
        /// 操作描述
        /// </summary>
        public String Description
        {
            get;
            set;
        }
        /// <summary>
        /// 操作数据
        /// </summary>
        public String OperationData
        {
            get;
            set;
        }
        /// <summary>
        /// 用户IP
        /// </summary>
        public String UserHostAddress
        {
            get;
            set;
        }
    #endregion 
    }
    #endregion 




}
