
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    public abstract partial class DbContext
    {



        #region 查询字段




        /// <summary>
        ///查询字段 
        /// </summary>
        /// <typeparam name="P">类型</typeparam>
        /// <param name="fieldName">字段名称</param>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public P GetField<P>(string fieldName, ReadData readOptions)
        {
            string sqlQuery = ReadSqlFieldScalar(fieldName, readOptions);
            object result = ExecuteScalar(sqlQuery, readOptions.Parameters);
            if (result == null || result is DBNull)
            {
                return default(P);
            }
            return (P)result;
        }

   

        /// <summary>
        ///查询字段 
        /// </summary>
        /// <typeparam name="P">类型</typeparam>
        /// <param name="fieldName">字段名称</param>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public List<P> GetFields<P>(string fieldName, ReadData readOptions)
        {
            string sqlQuery = ReadSqlField(fieldName, readOptions);
            return ExecuteReader(sqlQuery, readOptions.Parameters).ToList<P>(0);
        }

        #endregion
    }
}
