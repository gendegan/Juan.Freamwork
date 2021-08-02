using System;
using System.Data;
using System.Data.Common;

namespace Juan.Core
{
    public abstract partial class DbContext
    {
        /// <summary>
        /// 执行返回DataTable
        /// </summary>
        /// <param name="commandText">执行命令</param>
        /// <param name="dataParameters">参数</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string commandText, params DataParameter[] dataParameters)
        {
            return ExecuteDataTable(CommandType.Text, commandText, dataParameters);
        }


        /// <summary>
        ///  执行返回DataTable
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">执行命令</param>
        /// <param name="dataParameters">参数</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(CommandType commandType, string commandText, params DataParameter[] dataParameters)
        {
            DbTransaction objDbTransaction = TryGetConcurrentTransaction();
            if (objDbTransaction == null)
            {


                using (DbConnection connection = CreateConnection(this.SelectConnectionString(this._ContextKey, commandType, commandText)))
                {
                    connection.Open();
                    return ExecuteDataTable(connection, commandType, commandText, dataParameters);
                }
            }
            else
            {
                return ExecuteDataTable(objDbTransaction, commandType, commandText, dataParameters);
            }
        }
        /// <summary>
        ///  执行返回DataTable
        /// </summary>
        /// <param name="connection">连接对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">执行命令</param>
        /// <param name="dataParameters">参数</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(DbConnection connection, CommandType commandType, string commandText, params DataParameter[] dataParameters)
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
                    DataTable objDataTable = new DataTable();
                    dataAdapter.Fill(objDataTable);
                    DataParameterReturn(objDbCommand.Parameters, dataParameters);
                    objDbCommand.Parameters.Clear();

                    if (mustCloseConnection)
                        connection.Close();

                    return objDataTable;
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
        ///  执行返回DataTable
        /// </summary>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">执行命令</param>
        /// <param name="dataParameters">参数</param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(DbTransaction transaction, CommandType commandType, string commandText, params DataParameter[] dataParameters)
        {

            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            DbCommand objDbCommand = CreateCommand();
            bool mustCloseConnection = false;
            PrepareCommand(objDbCommand, transaction.Connection, transaction, commandType, commandText, dataParameters, out mustCloseConnection);
            OnExecuteing(objDbCommand, transaction.Connection, transaction, commandType, commandText, dataParameters);
            try
            {
                using (DbDataAdapter dataAdapter = CreateDataAdapter(objDbCommand))
                {
                    DataTable objDataTable = new DataTable();
                    dataAdapter.Fill(objDataTable);
                    DataParameterReturn(objDbCommand.Parameters, dataParameters);
                    objDbCommand.Parameters.Clear();
                    return objDataTable;
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
