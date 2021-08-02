using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Juan.Core;

namespace Juan.Data
{
    public abstract partial class IDbContext<T, Key> where T : class, new()
    {


        #region 判断是否存在


        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public bool Any(ReadOptions readOptions)
        {
            return Context.Any(ToReadData(readOptions));
        }
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <returns></returns>
        public bool Any(string condition, DataParameter[] parms)
        {
            return Any(new ReadOptions()
            {
                Condition = condition,
                Parameters = parms

            });
        }


        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns></returns>
        public bool Any(Key id)
        {
            return Any(ReadOptions.Search(ConditionParamKey(), PrimaryKey.PrimaryName.CreateParameter(id)));
        }


        #endregion


    }
}
