using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Juan.Core;
using Juan.Data;
using Juan.Log.Entity;
namespace Juan.Log.Context
{
    /// <summary>
    /// 日志程序表上下文
    /// </summary>
    public partial class ApplicationContext : DataContext<Application, Int32>
    {

        #region 构造函数
       /// <summary>
        /// 构造函数[Auto]
        /// </summary>
        public ApplicationContext()
        {
         }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionType">连接类型</param>
        public ApplicationContext(ConnectionType connectionType)
            : base(connectionType)
      {
       }
        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="partName">分表表名</param>
        /// <param name="connectionType">连接类型</param>
        public ApplicationContext(string partName, ConnectionType connectionType = ConnectionType.Auto)
            : base(partName, connectionType)
        {
          }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="contextKey">上下文键值</param>
        /// <param name="partName">分表表名</param>
        /// <param name="connectionType">连接类型</param>
        public ApplicationContext(string contextKey, string partName, ConnectionType connectionType = ConnectionType.Auto)
            : base(contextKey, partName, connectionType)
        {
        }
        #endregion


 


    }
}
