using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Data.Common;

namespace Juan.Data
{
    public abstract partial class IDbContext<T, Key> where T : class,new()
    {







        #region 删除操作



        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public virtual int Delete(ReadOptions readOptions)
        {

            return Context.Delete(ToReadData(readOptions));
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public virtual int Delete(string condition, DataParameter[] parms)
        {


            return Delete( new ReadOptions()
            {
                Condition = condition,
                Parameters = parms,
            });


        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns></returns>
        public virtual int Delete(Key id)
        {
            return Delete(ReadOptions.Search(ConditionParamKey(), PrimaryKey.PrimaryName.CreateParameter(id)));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">主键列表</param>
        /// <returns></returns>
        public virtual int Delete(IEnumerable<Key> ids)
        {
            if (ids.IsNull())
            {
                return 0;
            }
            return Delete(ReadOptions.Search(Condition(ids)));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="idString">主键字符串如1,2,3</param>
        /// <returns></returns>
        public virtual int DeleteID(string idString)
        {
            if (string.IsNullOrWhiteSpace(idString))
            {
                return 0;
            }
            return Delete(ReadOptions.Search(ConditionID(idString)));
        }

        #endregion

    }
}
