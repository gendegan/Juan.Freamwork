
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    public abstract partial class DbContext
    {

        /// <summary>
        /// 执行类型
        /// </summary>
        internal enum ConnectionOwnership
        {

            /// <summary>Connection 是自己内部生成的</summary>
            Internal,
            /// <summary>Connection 外部调用进来的</summary>
            External
        }
        /// <summary>
        /// 执行返回DataReader
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">执行命令</param>
        /// <param name="connectionOwnership"></param>
        /// <param name="dataParameters">参数</param>
        /// <returns></returns>
        private DbDataReader ExecuteReader(DbConnection connection, DbTransaction transaction, CommandType commandType, string commandText, ConnectionOwnership connectionOwnership, params DataParameter[] dataParameters)
        {

            if (connection == null) throw new ArgumentNullException("connection");

            bool mustCloseConnection = false;
            // 创建命令对象
            DbCommand objDbCommand = CreateCommand();
            OnExecuteing(objDbCommand, connection, transaction, commandType, commandText, dataParameters);
            try
            {

                PrepareCommand(objDbCommand, connection, transaction, commandType, commandText, dataParameters, out mustCloseConnection);

                // 创建Reader对象
                DbDataReader dataReader;

                // 调用ExecuteReader
                if (connectionOwnership == ConnectionOwnership.External)
                {
                    dataReader = objDbCommand.ExecuteReader();
                }
                else
                {
                    dataReader = objDbCommand.ExecuteReader(CommandBehavior.CloseConnection);

                }
                DataParameterReturn(objDbCommand.Parameters, dataParameters);
                bool canClear = true;
                foreach (DbParameter commandParameter in objDbCommand.Parameters)
                {
                    if (commandParameter.Direction != ParameterDirection.Input)
                        canClear = false;
                }

                if (canClear)
                {
                    objDbCommand.Parameters.Clear();
                }

                return dataReader;
            }
            catch (Exception objExp)
            {
                OnExecuteError(objExp, objDbCommand, connection, transaction, commandType, commandText, dataParameters);
                if (mustCloseConnection)
                    connection.Close();
                throw;
            }
            finally
            {
                OnExecuted(objDbCommand, connection, transaction, commandType, commandText, dataParameters);
            }

        }

        /// <summary>
        /// 执行返回DataReader
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">执行命令</param>
        /// <param name="dataParameters">参数</param>
        /// <returns></returns>
        public DbDataReader ExecuteReader(DbConnection connection, CommandType commandType, string commandText, params DataParameter[] dataParameters)
        {

            return ExecuteReader(connection, null, commandType, commandText, ConnectionOwnership.External, dataParameters);
        }

        /// <summary>
        /// 执行返回DataReader
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">执行命令</param>
        /// <param name="dataParameters">参数</param>
        /// <returns></returns>
        public DbDataReader ExecuteReader(DbTransaction transaction, CommandType commandType, string commandText, params DataParameter[] dataParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            return ExecuteReader(transaction.Connection, transaction, commandType, commandText, ConnectionOwnership.External, dataParameters);
        }
        /// <summary>
        /// 执行返回DataReader
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">执行命令</param>
        /// <param name="dataParameters">参数</param>
        /// <returns></returns>
        public DbDataReader ExecuteReader(CommandType commandType, string commandText, params DataParameter[] dataParameters)
        {
            DbTransaction objDbTransaction = TryGetConcurrentTransaction();
            if (objDbTransaction == null)
            {
                DbConnection connection = null;
                try
                {
                    connection = CreateConnection(this.SelectConnectionString(this._ContextKey, commandType, commandText));
                    connection.Open();
                    return ExecuteReader(connection, null, commandType, commandText, ConnectionOwnership.Internal, dataParameters);
                }
                catch
                {
                    if (connection != null) connection.Close();
                    throw;
                }
            }
            else
            {
                return ExecuteReader(objDbTransaction, commandType, commandText, dataParameters);
            }


        }

        /// <summary>
        /// 执行返回DataReader
        /// </summary>
        /// <param name="commandText">执行命令</param>
        /// <param name="dataParameters">参数</param>
        /// <returns></returns>
        public DbDataReader ExecuteReader(string commandText, params DataParameter[] dataParameters)
        {
            return ExecuteReader(CommandType.Text, commandText, dataParameters);
        }

    }
}
