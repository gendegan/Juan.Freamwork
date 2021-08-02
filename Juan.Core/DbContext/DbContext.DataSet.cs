
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
        ///  执行返回DataSet
        /// </summary>
        /// <param name="commandText">执行命令</param>
        /// <param name="dataParameters">参数</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string commandText, params DataParameter[] dataParameters)
        {
            return ExecuteDataSet(CommandType.Text, commandText, dataParameters);
        }


        /// <summary>
        /// 执行返回DataSet
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">执行命令</param>
        /// <param name="dataParameters">参数</param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(CommandType commandType, string commandText, params DataParameter[] dataParameters)
        {
            DbTransaction objDbTransaction = TryGetConcurrentTransaction();
            if (objDbTransaction == null)
            {
                using (DbConnection connection = CreateConnection(this.SelectConnectionString(this._ContextKey, commandType, commandText)))
                {
                    connection.Open();
                    return ExecuteDataSet(connection, commandType, commandText, dataParameters);
                }
            }
            else
            {
                return ExecuteDataSet(objDbTransaction, commandType, commandText, dataParameters);
            }
        }
        /// <summary>
        /// 执行返回DataSet
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">执行命令</param>
        /// <param name="dataParameters">参数</param>
        /// <returns></returns>
        private DataSet ExecuteDataSet(DbConnection connection, CommandType commandType, string commandText, params DataParameter[] dataParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");


            DbCommand objDbCommand = CreateCommand();

            bool mustCloseConnection = false;
            PrepareCommand(objDbCommand, connection, null, commandType, commandText, dataParameters, out mustCloseConnection);
            OnExecuteing(objDbCommand, connection, null, commandType, commandText, dataParameters);
            try
            {


                using (DbDataAdapter dataAdapter = CreateDataAdapter(objDbCommand))
                {
                    DataSet ds = new DataSet();

                    dataAdapter.Fill(ds);
                    DataParameterReturn(objDbCommand.Parameters, dataParameters);
                    objDbCommand.Parameters.Clear();

                    if (mustCloseConnection)
                        connection.Close();


                    return ds;
                }
            }
            catch (Exception objExp)
            {
                OnExecuteError(objExp, objDbCommand, connection, null, commandType, commandText, dataParameters);
                throw;
            }
            finally
            {
                OnExecuted(objDbCommand, connection, null, commandType, commandText, dataParameters);
            }
        }

        /// <summary>
        /// 执行返回DataSet
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">执行命令</param>
        /// <param name="dataParameters">参数</param>
        /// <returns></returns>
        private DataSet ExecuteDataSet(DbTransaction transaction, CommandType commandType, string commandText, params DataParameter[] dataParameters)
        {

            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            commandText.ArgumentNoNull("commandText");
            DbCommand objDbCommand = CreateCommand();
            bool mustCloseConnection = false;
            PrepareCommand(objDbCommand, transaction.Connection, transaction, commandType, commandText, dataParameters, out mustCloseConnection);
            OnExecuteing(objDbCommand, transaction.Connection, transaction, commandType, commandText, dataParameters);
            try
            {

                using (DbDataAdapter dataAdapter = CreateDataAdapter(objDbCommand))
                {
                    DataSet objDataSet = new DataSet();
                    dataAdapter.Fill(objDataSet);
                    DataParameterReturn(objDbCommand.Parameters, dataParameters);
                    objDbCommand.Parameters.Clear();
                    return objDataSet;
                }
            }
            catch (Exception objExp)
            {
                OnExecuteError(objExp, objDbCommand, transaction.Connection, transaction, commandType, commandText, dataParameters);
                throw;
            }
            finally
            {
                OnExecuted(objDbCommand, transaction.Connection, transaction, commandType, commandText, dataParameters);
            }
        }


    }
}
