using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

using MySql.Data.MySqlClient;
namespace JuanGenerate
{
    public partial class MySqlHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="MySqlParameters"></param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText, params MySqlParameter[] MySqlParameters)
        {
            return ExecuteScalar(this._ConnectionKeyOrConnectionString, CommandType.Text, commandText, MySqlParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="MySqlParameters"></param>
        /// <returns></returns>
        public object ExecuteScalar(CommandType commandType, string commandText, params MySqlParameter[] MySqlParameters)
        {
            return ExecuteScalar(this._ConnectionKeyOrConnectionString, commandType, commandText, MySqlParameters);
        }

        public static object ExecuteScalar(string connectionKeyOrConnectionString, string commandText, params MySqlParameter[] MySqlParameters)
        {
            return ExecuteScalar(connectionKeyOrConnectionString, CommandType.Text, commandText, MySqlParameters);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionKeyOrConnectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="MySqlParameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string connectionKeyOrConnectionString, CommandType commandType, string commandText, params MySqlParameter[] MySqlParameters)
        {
            connectionKeyOrConnectionString.ArgumentNoNull("connectionKeyOrConnectionString");

            using (DbConnection connection = CreateConnection(connectionKeyOrConnectionString))
            {
                connection.Open();

                return ExecuteScalar(connection, commandType, commandText, MySqlParameters);
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
        public static object ExecuteScalar(DbConnection connection, CommandType commandType, string commandText, params MySqlParameter[] MySqlParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");


            DbCommand objDbCommand = CreateCommand();

            bool mustCloseConnection = false;
            PrepareCommand(objDbCommand, connection, null, commandType, commandText, MySqlParameters, out mustCloseConnection);

         
                object retval = objDbCommand.ExecuteScalar();
                objDbCommand.Parameters.Clear();
                if (mustCloseConnection)
                    connection.Close();
                return retval;
             
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="MySqlParameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(DbTransaction transaction, CommandType commandType, string commandText, params MySqlParameter[] MySqlParameters)
        {


            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            DbCommand objDbCommand = CreateCommand();
            bool mustCloseConnection = false;
            PrepareCommand(objDbCommand, transaction.Connection, transaction, commandType, commandText, MySqlParameters, out mustCloseConnection);
           
                object retval = objDbCommand.ExecuteScalar();
                objDbCommand.Parameters.Clear();
                return retval;
            
        }


    }
}
