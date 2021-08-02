using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    public abstract partial class DbContext
    {

        #region 判断是否存在

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public bool Any(ReadData readOptions)
        {
            return ExecuteDataTable(ReadSqlDataAny(readOptions), readOptions.Parameters).Rows.Count > 0;
        }

        #endregion



    }
}
