using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Data
{
    public abstract partial class IDbContext<T, Key> where T : class,new()
    {






        /// <summary>
        ///  查询主键
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="parms"></param>
        /// <param name="sortExpression"></param>
        /// <returns></returns>
        public Key GetKey(string condition, DataParameter[] parms, string sortExpression = "")
        {
            return GetField<Key>(PrimaryKey.PrimaryName, condition, parms, sortExpression);
        }


        /// <summary>
        ///  查询主键
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<Key> GetKeys(string condition, DataParameter[] parms)
        {
            return GetFields<Key>(PrimaryKey.PrimaryName, condition, parms);
        }

        /// <summary>
        /// 查询字段 
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="fieldName"></param>
        /// <param name="condition"></param>
        /// <param name="parms"></param>
        /// <param name="sortExpression"></param>
        /// <returns></returns>
        public P GetField<P>(string fieldName, string condition, DataParameter[] parms, string sortExpression = "")
        {
            return Context.GetField<P>(fieldName, ToReadData(ReadOptions.Search(condition, parms, sortExpression)));
        }


        /// <summary>
        /// 查询字段 
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="fieldName"></param>
        /// <param name="condition"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public List<P> GetFields<P>(string fieldName, string condition, DataParameter[] parms)
        {
            return Context.GetFields<P>(fieldName, ToReadData(ReadOptions.Search(condition, parms)));
        }


        
    }
}
