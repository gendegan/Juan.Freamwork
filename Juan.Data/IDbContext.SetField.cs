using Juan.Core;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Juan.Data
{
    public abstract partial class IDbContext<T, Key> where T : class,new()
    {






        #region 更新字段


        /// <summary>
        /// 更新字段
        /// </summary>
        /// <param name="fieldExpression">字段表达式 Name='ee',Sex=1</param>
        /// <param name="readOptions">读取选项</param>
        /// <param name="parms">参数</param>
        /// <returns></returns>
        public int SetFields(string fieldExpression, ReadOptions readOptions, params DataParameter[] parms)
        {
            return Context.SetFields(fieldExpression, ToReadData(readOptions), parms);
        }

        /// <summary>
        /// 更新字段
        /// </summary>
        /// <param name="fieldExpression">字段表达式 Name='ee',Sex=1</param>
        /// <param name="ids">主键</param>
        /// <param name="parms">参数</param>
        /// <returns></returns>
        public int SetFields(string fieldExpression, IEnumerable<Key> ids, params DataParameter[] parms)
        {
            if (ids.IsNull())
            {
                return 0;
            }
            return SetFields(fieldExpression, ReadOptions.Search(Condition(ids)), parms);
        }


        /// <summary>
        /// 更新字段
        /// </summary>
        /// <param name="fieldExpression">字段表达式 Name='ee',Sex=1</param>
        /// <param name="id">主键</param>
        /// <param name="parms">参数</param>
        /// <returns></returns>
        public int SetFields(string fieldExpression, Key id, params DataParameter[] parms)
        {
            return SetFields(fieldExpression, ReadOptions.Search(Condition(id)), parms);
        }

        /// <summary>
        /// 更新字段
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="value">值</param>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public int SetField(string fieldName, object value, ReadOptions readOptions)
        {
            return SetFields(ConditionParam(fieldName), readOptions, fieldName.CreateParameter(value));
        }

        /// <summary>
        ///  更新字段
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="value">值</param>
        /// <param name="ids">主键</param>
        /// <returns></returns>
        public int SetField(string fieldName, object value, IEnumerable<Key> ids)
        {
            return SetFields(ConditionParam(fieldName), ids, fieldName.CreateParameter(value));
        }


        /// <summary>
        ///  更新字段
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="value">值</param>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public int SetField(string fieldName, object value, Key id)
        {
            return SetFields(ConditionParam(fieldName), id, fieldName.CreateParameter(value));
        }


        #endregion


    }
}
