using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Juan.Core
{
    public abstract partial class DbContext
    {


        /// <summary>
        /// 
        /// </summary>
        public static readonly ThreadLocal<Dictionary<string, DbTransaction>> _ThreadLocalTransaction = new ThreadLocal<Dictionary<string, DbTransaction>>(() => new Dictionary<string, DbTransaction>());
        private string TransactionKey
        {
            get
            {

                return this._ContextKey.ToLower();
            }

        }

        /// <summary>
        /// 是否当前有事务
        /// </summary>
        public bool IsTransaction
        {

            get
            {
                return _ThreadLocalTransaction.Value.ContainsKey(TransactionKey);
            }

        }


        /// <summary>
        /// 获取当前事务
        /// </summary>
        /// <returns></returns>
        public DbTransaction TryGetConcurrentTransaction()
        {


            if (_ThreadLocalTransaction.Value.ContainsKey(TransactionKey))
            {
                return _ThreadLocalTransaction.Value[TransactionKey];
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns></returns>
        public void BeingTransaction()
        {
            BeingTransaction(IsolationLevel.RepeatableRead);
        }




        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="isolationLevel">事务类型</param>
        /// <returns></returns>
        public void BeingTransaction(IsolationLevel isolationLevel)
        {
            if (!_ThreadLocalTransaction.Value.ContainsKey(TransactionKey))
            {
                _ThreadLocalTransaction.Value[TransactionKey] = CreateTransaction(isolationLevel, this._ContextKey);
            }

        }

        /// <summary>
        /// 创建事务
        /// </summary>
        /// <param name="isolationLevel">事务类型</param>
        /// <param name="contextKey">上下文连接键值</param>
        /// <returns></returns>
        private DbTransaction CreateTransaction(IsolationLevel isolationLevel, string contextKey)
        {
            DbConnection objDbConnection = CreateConnection(ContextHelper.ConnectionWriteString(contextKey));
            objDbConnection.Open();
            DbTransaction objDbTransaction = objDbConnection.BeginTransaction(isolationLevel);
            return objDbTransaction;
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {

            DbTransaction objDbTransaction;
            if (!_ThreadLocalTransaction.Value.ContainsKey(TransactionKey))
            {
                return;
                //  throw new Exception("请先调用方法BeingTransaction");
            }
            else
            {
                objDbTransaction = _ThreadLocalTransaction.Value[TransactionKey];
            }

            try
            {
                objDbTransaction.Rollback();
            }
            finally
            {
                if (objDbTransaction.Connection != null)
                {
                    objDbTransaction.Connection.Dispose();
                }
                objDbTransaction = null;
                _ThreadLocalTransaction.Value.Remove(TransactionKey);


            }
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {

            DbTransaction objDbTransaction;
            if (!_ThreadLocalTransaction.Value.ContainsKey(TransactionKey))
            {
                throw new Exception("请先调用方法BeingTransaction");
            }
            else
            {
                objDbTransaction = _ThreadLocalTransaction.Value[TransactionKey];
            }
            try
            {
                objDbTransaction.Commit();
            }
            finally
            {
                if (objDbTransaction.Connection != null)
                {
                    objDbTransaction.Connection.Dispose();
                }
                objDbTransaction = null;
                _ThreadLocalTransaction.Value.Remove(TransactionKey);

            }

        }






    }
}
