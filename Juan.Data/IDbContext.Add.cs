using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Data
{
    public abstract partial class IDbContext<T, Key> where T : class, new()
    {



        #region 新增操作
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public abstract Key Add(T value);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="isIgnore">是否忽略</param>
        /// <returns></returns>
        public abstract int Add(T value, bool isIgnore);

        /// <summary>
        /// 指量新增
        /// </summary>
        /// <param name="valueList">值</param>
        /// <param name="isIgnore">是否忽略</param>
        /// <returns></returns>
        public abstract int Add(IEnumerable<T> valueList, bool isIgnore);

        /// <summary>
        /// 新增更新操作
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public abstract int AddUpdate(T value, params string[] fields);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public abstract int AddUpdateExclude(T value, params string[] fields);

        /// <summary>
        /// 新增更新操作
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="updateSql">更新语句</param>
        /// <param name="parms">更新参数</param>
        /// <returns></returns>
        public abstract int AddUpdateValue(T value, string updateSql, params DataParameter[] parms);
        /// <summary>
        /// 新增更新操作
        /// </summary>
        /// <param name="valueList">值</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public abstract int AddUpdate(IEnumerable<T> valueList, params string[] fields);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueList"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public abstract int AddUpdateExclude(IEnumerable<T> valueList, params string[] fields);

        /// <summary>
        /// 新增更新操作
        /// </summary>
        /// <param name="valueList">值</param>
        /// <param name="updateSql">更新语句</param>
        /// <param name="parms">更新参数</param>
        /// <returns></returns>
        public abstract int AddUpdateValue(IEnumerable<T> valueList, string updateSql, params DataParameter[] parms);
        #endregion
    }
}
