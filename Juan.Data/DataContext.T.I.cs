using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Juan.Data
{
    public partial class DataContext<T, Key> : IDbContext<T, Key> where T : class,new()
    {

        /// <summary>
        /// 
        /// </summary>
        public override string DBName
        {
            get
            {
                return DbContext.DBName;
            }
            set
            {
                DbContext.DBName = value;

            }
        }


        /// <summary>
        /// 替换新增
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public override int ReplaceAdd(T value)
        {
            return DbContext.ReplaceAdd(value);
        }
        /// <summary>
        /// 替换新增
        /// </summary>
        /// <param name="valueList">值</param>
        /// <returns></returns>
        public override int ReplaceAdd(IEnumerable<T> valueList)
        {
            return DbContext.ReplaceAdd(valueList);
        }
        /// <summary>
        /// 导数据
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="isIgnore">是否忽略</param>
        /// <returns></returns>
        public override int Import(T value, bool isIgnore = true)
        {
            return DbContext.Import(value, isIgnore);
        }
        /// <summary>
        /// 导数据
        /// </summary>
        /// <param name="valueList">值</param>
        /// <param name="isIgnore">是否忽略</param>
        /// <returns></returns>
        public override int Import(IEnumerable<T> valueList, bool isIgnore = true)
        {
            return DbContext.Import(valueList, isIgnore);
        }
        /// <summary>
        /// 导数据更新[键值冲突]
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="fields">更新字段</param>
        /// <returns></returns>
        public override int ImportUpdate(T value, params string[] fields)
        {
            return DbContext.ImportUpdate(value, fields);
        }
        /// <summary>
        ///  导数据更新[键值冲突]
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="updateSql">更新语句</param>
        /// <param name="parms">更新参数</param>
        /// <returns></returns>
        public override int ImportUpdateValue(T value, string updateSql, params DataParameter[] parms)
        {
            return DbContext.ImportUpdateValue(value, updateSql, parms);
        }
        /// <summary>
        ///  导数据更新[键值冲突]
        /// </summary>
        /// <param name="valueList">值</param>
        /// <param name="fields">更新字段</param>
        /// <returns></returns>
        public override int ImportUpdate(IEnumerable<T> valueList, params string[] fields)
        {
            return DbContext.ImportUpdate(valueList, fields);
        }
        /// <summary>
        ///  导数据更新[键值冲突]
        /// </summary>
        /// <param name="valueList">值</param>
        /// <param name="updateSql">更新语句</param>
        /// <param name="parms">更新参数</param>
        /// <returns></returns>
        public override int ImportUpdateValue(IEnumerable<T> valueList, string updateSql, params DataParameter[] parms)
        {
            return DbContext.ImportUpdateValue(valueList, updateSql, parms);
        }



    }
}
