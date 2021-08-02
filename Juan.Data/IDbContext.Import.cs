using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Data
{
    public abstract partial class IDbContext<T, Key> where T : class,new()
    {
        #region 导数据操作

        /// <summary>
        /// 替换新增
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public abstract int ReplaceAdd(T value);
        /// <summary>
        /// 替换新增
        /// </summary>
        /// <param name="valueList">值</param>
        /// <returns></returns>
        public abstract int ReplaceAdd(IEnumerable<T> valueList);
        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="isIgnore">是否忽略</param>
        /// <returns></returns>
        public abstract int Import(T value, bool isIgnore = true);
        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="valueList">值</param>
        /// <param name="isIgnore">是否忽略</param>
        /// <returns></returns>
        public abstract int Import(IEnumerable<T> valueList, bool isIgnore = true);

        /// <summary>
        /// 导入更新操作
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public abstract int ImportUpdate(T value, params string[] fields);

        /// <summary>
        /// 导入更新操作
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="updateSql">更新语句</param>
        /// <param name="parms">更新参数</param>
        /// <returns></returns>
        public abstract int ImportUpdateValue(T value, string updateSql, params DataParameter[] parms);
        /// <summary>
        /// 导入更新操作
        /// </summary>
        /// <param name="valueList">值</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public abstract int ImportUpdate(IEnumerable<T> valueList, params string[] fields);


        /// <summary>
        /// 导入更新操作
        /// </summary>
        /// <param name="valueList">值</param>
        /// <param name="updateSql">更新语句</param>
        /// <param name="parms">更新参数</param>
        /// <returns></returns>
        public abstract int ImportUpdateValue(IEnumerable<T> valueList, string updateSql, params DataParameter[] parms);
        #endregion
    }
}
