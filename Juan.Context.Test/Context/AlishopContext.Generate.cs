using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Juan.Core;
using Juan.Data;
using Juan.Context.Test.Entity;
namespace Juan.Context.Test.Context
{
    /// <summary>
    /// 壁纸淘宝电商推荐信息上下文
    /// </summary>
    public partial class AlishopContext : DataContext<Alishop, Int32>
    {

        #region 构造函数
       /// <summary>
        /// 构造函数[Auto]
        /// </summary>
        public AlishopContext()
        {
         }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionType">连接类型</param>
        public AlishopContext(ConnectionType connectionType)
            : base(connectionType)
      {
       }
        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="partName">分表表名</param>
        /// <param name="connectionType">连接类型</param>
        public AlishopContext(string partName, ConnectionType connectionType = ConnectionType.Auto)
            : base(partName, connectionType)
        {
          }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="contextKey">上下文键值</param>
        /// <param name="partName">分表表名</param>
        /// <param name="connectionType">连接类型</param>
        public AlishopContext(string contextKey, string partName, ConnectionType connectionType = ConnectionType.Auto)
            : base(contextKey, partName, connectionType)
        {
        }
        #endregion


 


    }
}
