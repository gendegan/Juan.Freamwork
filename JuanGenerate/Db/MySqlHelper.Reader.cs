using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace JuanGenerate
{
    public partial class MySqlHelper
    {

        /// <summary>
        /// 
        /// </summary>
        public enum ConnectionOwnership
        {

            /// <summary>Connection 是自己内部生成的</summary>
            Internal,
            /// <summary>Connection 外部调用进来的</summary>
            External
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="connectionOwnership"></param>
        /// <param name="MySqlParameters"></param>
        /// <returns></returns>
        private static DbDataReader ExecuteReader(DbConnection connection, DbTransaction transaction, CommandType commandType, string commandText, ConnectionOwnership connectionOwnership, params MySqlParameter[] MySqlParameters)
        {

            if (connection == null) throw new ArgumentNullException("connection");

            bool mustCloseConnection = false;
            // 创建命令对象
            DbCommand objDbCommand = CreateCommand();
          
            try
            {

                PrepareCommand(objDbCommand, connection, transaction, commandType, commandText, MySqlParameters, out mustCloseConnection);

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
              
                if (mustCloseConnection)
                    connection.Close();
                throw objExp;
            }
          

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="MySqlParameters"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(DbConnection connection, CommandType commandType, string commandText, params MySqlParameter[] MySqlParameters)
        {

            return ExecuteReader(connection, null, commandType, commandText, ConnectionOwnership.External, MySqlParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="MySqlParameters"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(DbTransaction transaction, CommandType commandType, string commandText, params MySqlParameter[] MySqlParameters)
        {
            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            return ExecuteReader(transaction.Connection, transaction, commandType, commandText, ConnectionOwnership.External, MySqlParameters);
        }


        public static DbDataReader ExecuteReader(string connectionKeyOrConnectionString, string commandText, params MySqlParameter[] MySqlParameters)
        {
            return ExecuteReader(connectionKeyOrConnectionString, CommandType.Text, commandText, MySqlParameters);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionKeyOrConnectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="MySqlParameters"></param>
        /// <returns></returns>
        public static DbDataReader ExecuteReader(string connectionKeyOrConnectionString, CommandType commandType, string commandText, params MySqlParameter[] MySqlParameters)
        {
            connectionKeyOrConnectionString.ArgumentNoNull("connectionKeyOrConnectionString");
            DbConnection connection = null;
            try
            {
                connection = CreateConnection(connectionKeyOrConnectionString);
                connection.Open();
                return ExecuteReader(connection, null, commandType, commandText, ConnectionOwnership.Internal, MySqlParameters);
            }
            catch
            {
                if (connection != null) connection.Close();
                throw;
            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="MySqlParameters"></param>
        /// <returns></returns>
        public DbDataReader ExecuteReader(CommandType commandType, string commandText, params MySqlParameter[] MySqlParameters)
        {
            return ExecuteReader(this._ConnectionKeyOrConnectionString, commandType, commandText, MySqlParameters);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="MySqlParameters"></param>
        /// <returns></returns>
        public DbDataReader ExecuteReader(string commandText, params MySqlParameter[] MySqlParameters)
        {
            return ExecuteReader(this._ConnectionKeyOrConnectionString, CommandType.Text, commandText, MySqlParameters);
        }

    }
}
