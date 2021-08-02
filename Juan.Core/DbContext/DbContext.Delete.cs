
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Juan.Core
{
    public abstract partial class DbContext
    {



        #region 删除操作


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <returns></returns>
        public int Delete(string tableName, string condition, params DataParameter[] parms)
        {
            return Delete(new ReadData()
            {
                TableName = tableName,
                Condition = condition,
                Parameters = parms,
            });
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public int Delete(ReadData readOptions)
        {
            return ExecuteNonQuery(ReadSqlDelete(readOptions), readOptions.Parameters);
        }


        #endregion





    }
}
