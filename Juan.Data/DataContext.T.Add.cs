using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Data
{
    public partial class DataContext<T, Key> : IDbContext<T, Key> where T : class, new()
    {    /// <summary>
         /// 新增
         /// </summary>
         /// <param name="value">值</param>
         /// <returns></returns>
        public override Key Add(T value)
        {
            return DbContext.Add(value);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="isIgnore">是否忽略</param>
        /// <returns></returns>
        public override int Add(T value, bool isIgnore)
        {
            return DbContext.Add(value, isIgnore);
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="valueList">值</param>
        /// <param name="isIgnore">是否忽略</param>
        /// <returns></returns>
        public override int Add(IEnumerable<T> valueList, bool isIgnore)
        {
            return DbContext.Add(valueList, isIgnore);
        }
        /// <summary>
        ///  新增更新[键值冲突]
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="fields">更新字段</param>
        /// <returns></returns>
        public override int AddUpdate(T value, params string[] fields)
        {
            return DbContext.AddUpdate(value, fields);
        }
        /// <summary>
        ///   新增更新[键值冲突]
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="updateSql">更新语句</param>
        /// <param name="parms">更新参数</param>
        /// <returns></returns>
        public override int AddUpdateValue(T value, string updateSql, params DataParameter[] parms)
        {
            return DbContext.AddUpdateValue(value, updateSql, parms);
        }
        /// <summary>
        ///   新增更新[键值冲突]
        /// </summary>
        /// <param name="valueList">值</param>
        /// <param name="fields">更新字段</param>
        /// <returns></returns>
        public override int AddUpdate(IEnumerable<T> valueList, params string[] fields)
        {
            return DbContext.AddUpdate(valueList, fields);
        }
        /// <summary>
        ///   新增更新[键值冲突]
        /// </summary>
        /// <param name="valueList">值</param>
        /// <param name="updateSql">更新语句</param>
        /// <param name="parms">更新参数</param>
        /// <returns></returns>
        public override int AddUpdateValue(IEnumerable<T> valueList, string updateSql, params DataParameter[] parms)
        {
            return DbContext.AddUpdateValue(valueList, updateSql, parms);
        }

        /// <summary>
        /// 新增更新[键值冲突]
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="fields">需要排除更新的字段</param>
        /// <returns></returns>
        public override int AddUpdateExclude(T value, params string[] fields)
        {
            return DbContext.AddUpdateExclude(value, fields);
        }
        /// <summary>
        /// 新增更新[键值冲突]
        /// </summary>
        /// <param name="valueList">值</param>
        /// <param name="fields">需要排除更新的字段</param>
        /// <returns></returns>
        public override int AddUpdateExclude(IEnumerable<T> valueList, params string[] fields)
        {
            return DbContext.AddUpdateExclude(valueList, fields);
        }
    }
}
