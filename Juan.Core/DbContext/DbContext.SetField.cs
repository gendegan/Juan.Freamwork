
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    public abstract partial class DbContext
    {

        #region 更新字段

        /// <summary>
        /// 更新字段
        /// </summary>
        /// <param name="fieldExpression">字段表达式 Name='ee',Sex=1</param>
        /// <param name="readOptions">读取选项</param>
        /// <param name="parms">参数</param>
        /// <returns></returns>
        public int SetFields(string fieldExpression, ReadData readOptions, params DataParameter[] parms)
        {
            if (string.IsNullOrWhiteSpace(fieldExpression))
            {
                return -1;
            }
            string sqlQuery = "UPDATE " + ReadTableName(readOptions) + " SET " + ParamCharReplace(fieldExpression);

            string condition = ReadCondition(readOptions);
            if (!String.IsNullOrEmpty(condition))
            {
                sqlQuery += " WHERE " + condition;
            }


            return ExecuteNonQuery(sqlQuery, readOptions.Parameters.MergeParameter(parms));
        }



        /// <summary>
        /// 更新字段
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="value">值</param>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public int SetField(string fieldName, object value, ReadData readOptions)
        {
            return SetFields(ConditionParam(fieldName), readOptions, fieldName.CreateParameter(value));
        }







        #endregion


    }
}
