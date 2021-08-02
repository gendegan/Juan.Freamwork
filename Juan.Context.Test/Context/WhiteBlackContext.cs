using Juan.Context.Test.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Juan.Data;
using Juan.Core;

namespace Juan.Context.Test.Context
{
    /// <summary>
    /// 评论黑白名单上下文
    /// </summary>
    public partial class WhiteBlackContext : DataContext<WhiteBlack, Int32>
    {

        #region 构造函数
        /// <summary>
        /// 构造函数[Auto]
        /// </summary>
        public WhiteBlackContext()
        {
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionType">连接类型</param>
        public WhiteBlackContext(ConnectionType connectionType)
            : base(connectionType)
        {
        }
        /// <summary>
        ///  构造函数
        /// </summary>
        /// <param name="partName">分表表名</param>
        /// <param name="connectionType">连接类型</param>
        public WhiteBlackContext(string partName, ConnectionType connectionType = ConnectionType.Auto)
            : base(partName, connectionType)
        {
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="contextKey">上下文键值</param>
        /// <param name="partName">分表表名</param>
        /// <param name="connectionType">连接类型</param>
        public WhiteBlackContext(string contextKey, string partName, ConnectionType connectionType = ConnectionType.Auto)
            : base(contextKey, partName, connectionType)
        {
        }
        #endregion





    }
}
