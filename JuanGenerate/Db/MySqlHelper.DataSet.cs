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


        public DataSet ExecuteDataSet(string commandText, params MySqlParameter[] MySqlParameters)
        {
            return ExecuteDataSet(this._ConnectionKeyOrConnectionString, CommandType.Text, commandText, MySqlParameters);
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="MySqlParameters"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(CommandType commandType, string commandText, params MySqlParameter[] MySqlParameters)
        {
            return ExecuteDataSet(this._ConnectionKeyOrConnectionString, commandType, commandText, MySqlParameters);
        }

        public static DataSet ExecuteDataSet(string connectionKeyOrConnectionString, string commandText, params MySqlParameter[] MySqlParameters)
        {
            return ExecuteDataSet(connectionKeyOrConnectionString, CommandType.Text, commandText, MySqlParameters);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionKeyOrConnectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="MySqlParameters"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(string connectionKeyOrConnectionString, CommandType commandType, string commandText, params MySqlParameter[] MySqlParameters)
        {
            connectionKeyOrConnectionString.ArgumentNoNull("connectionKeyOrConnectionString");


            using (DbConnection connection = CreateConnection(connectionKeyOrConnectionString))
            {
                connection.Open();
                return ExecuteDataSet(connection, commandType, commandText, MySqlParameters);
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
        public static DataSet ExecuteDataSet(DbConnection connection, CommandType commandType, string commandText, params MySqlParameter[] MySqlParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");


            DbCommand objDbCommand = CreateCommand();

            bool mustCloseConnection = false;
            PrepareCommand(objDbCommand, connection, null, commandType, commandText, MySqlParameters, out mustCloseConnection);

            using (DbDataAdapter dataAdapter = CreateDataAdapter(objDbCommand))
            {
                DataSet ds = new DataSet();

                dataAdapter.Fill(ds);
                objDbCommand.Parameters.Clear();

                if (mustCloseConnection)
                    connection.Close();


                return ds;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="MySqlParameters"></param>
        /// <returns></returns>
        public static DataSet ExecuteDataSet(DbTransaction transaction, CommandType commandType, string commandText, params MySqlParameter[] MySqlParameters)
        {

            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            commandText.ArgumentNoNull("commandText");
            DbCommand objDbCommand = CreateCommand();
            bool mustCloseConnection = false;
            PrepareCommand(objDbCommand, transaction.Connection, transaction, commandType, commandText, MySqlParameters, out mustCloseConnection);


            using (DbDataAdapter dataAdapter = CreateDataAdapter(objDbCommand))
            {
                DataSet objDataSet = new DataSet();
                dataAdapter.Fill(objDataSet);
                objDbCommand.Parameters.Clear();
                return objDataSet;
            }

        }


    }
}
