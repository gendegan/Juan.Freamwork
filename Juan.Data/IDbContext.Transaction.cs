using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Data.Common;
using System.Collections.Concurrent;
namespace Juan.Data
{
    public abstract partial class IDbContext<T, Key> where T : class,new()
    {







        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns></returns>
        public void BeingTransaction()
        {
              Context.BeingTransaction();
        }


        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="isolationLevel">事务类型</param>
        /// <returns></returns>
        public  void BeingTransaction(IsolationLevel isolationLevel)
        {
              Context.BeingTransaction(isolationLevel);

        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {

            Context.Rollback();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {

            Context.Commit();

        }


    }
}
