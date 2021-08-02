
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    public abstract partial class DbContext
    {

        /// <summary>
        /// 查询字段
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="fieldName"></param>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <param name="parms"></param>
        /// <param name="sortExpression"></param>
        /// <returns></returns>
        public P GetField<P>(string fieldName, string tableName, string condition, DataParameter[] parms, string sortExpression = "")
        {
            return GetField<P>(fieldName, new ReadData()
             {
                 TableName = tableName,
                 Condition = condition,
                 Parameters = parms,
                 SortExpression = sortExpression

             });
        }
        /// <summary>
        /// 查询字段
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="fieldName"></param>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <param name="parms"></param>
        /// <param name="sortExpression"></param>
        /// <returns></returns>
        public List<P> GetFields<P>(string fieldName, string tableName, string condition, DataParameter[] parms, string sortExpression = "")
        {
            return GetFields<P>(fieldName, new ReadData()
            {
                TableName = tableName,
                Condition = condition,
                Parameters = parms,
                SortExpression = sortExpression

            });
        }
    }
}
