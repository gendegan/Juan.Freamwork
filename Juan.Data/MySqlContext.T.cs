using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Data
{
    /// <summary>
    /// MySqlContext
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <typeparam name="Key">主键类型</typeparam>
    public partial class MySqlContext<T, Key> : DbContext<T, Key> where T : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextKey">上下文键值</param>
        /// <param name="partName"></param>
        /// <param name="connectionType"></param>
        public MySqlContext(string contextKey, string partName = "", ConnectionType connectionType = ConnectionType.Auto)
            : base(new MySqlContext(contextKey, connectionType), partName)
        {

        }







    }
}
