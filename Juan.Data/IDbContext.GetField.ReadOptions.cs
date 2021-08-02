using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Data
{
    public abstract partial class IDbContext<T, Key> where T : class,new()
    {


        #region 查询字段

        /// <summary>
        /// 查询主键
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public Key GetKey(ReadOptions readOptions)
        {
            return GetField<Key>(PrimaryKey.PrimaryName, readOptions);
        }


        /// <summary>
        /// 查询主键
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public List<Key> GetKeys(ReadOptions readOptions)
        {
            return GetFields<Key>(PrimaryKey.PrimaryName, readOptions);
        }

        /// <summary>
        ///查询字段 
        /// </summary>
        /// <typeparam name="P">类型</typeparam>
        /// <param name="fieldName">字段名称</param>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public P GetField<P>(string fieldName, ReadOptions readOptions)
        {
            return Context.GetField<P>(fieldName, ToReadData(readOptions));
        }


        /// <summary>
        ///查询字段 
        /// </summary>
        /// <typeparam name="P">类型</typeparam>
        /// <param name="fieldName">字段名称</param>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public List<P> GetFields<P>(string fieldName, ReadOptions readOptions)
        {
            return Context.GetFields<P>(fieldName, ToReadData(readOptions));
        }



        /// <summary>
        /// 查询字段 
        /// </summary>
        /// <typeparam name="P">类型</typeparam>
        /// <param name="fieldName">字段名称</param>
        /// <param name="id">主键值</param>
        /// <returns></returns>
        public P GetField<P>(string fieldName, Key id)
        {
            return GetField<P>(fieldName, ReadOptions.Search(ConditionParamKey(), PrimaryKey.PrimaryName.CreateParameter(id)));
        }




        #endregion
    }
}
