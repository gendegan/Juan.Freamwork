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

        public DataTable ExecuteDataTable(string commandText, params MySqlParameter[] MySqlParameters)
        {
            return ExecuteDataTable(this._ConnectionKeyOrConnectionString, CommandType.Text, commandText, MySqlParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="MySqlParameters"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(CommandType commandType, string commandText, params MySqlParameter[] MySqlParameters)
        {
            return ExecuteDataTable(this._ConnectionKeyOrConnectionString, commandType, commandText, MySqlParameters);
        }


        public static DataTable ExecuteDataTable(string connectionKeyOrConnectionString, string commandText, params MySqlParameter[] MySqlParameters)
        {
            return ExecuteDataTable(connectionKeyOrConnectionString, CommandType.Text, commandText, MySqlParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionKeyOrConnectionString"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="MySqlParameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string connectionKeyOrConnectionString, CommandType commandType, string commandText, params MySqlParameter[] MySqlParameters)
        {
            connectionKeyOrConnectionString.ArgumentNoNull("connectionKeyOrConnectionString");


            using (DbConnection connection = CreateConnection(connectionKeyOrConnectionString))
            {
                connection.Open();
                return ExecuteDataTable(connection, commandType, commandText, MySqlParameters);
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
        public static DataTable ExecuteDataTable(DbConnection connection, CommandType commandType, string commandText, params MySqlParameter[] MySqlParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            DbCommand objDbCommand = CreateCommand();
            bool mustCloseConnection = false;
            PrepareCommand(objDbCommand, connection, null, commandType, commandText, MySqlParameters, out mustCloseConnection);
           
          
                using (DbDataAdapter dataAdapter = CreateDataAdapter(objDbCommand))
                {
                    DataTable objDataTable = new DataTable();
                    dataAdapter.Fill(objDataTable);
                    objDbCommand.Parameters.Clear();

                    if (mustCloseConnection)
                        connection.Close();

                    return objDataTable;
                }
         
        }


        public static DataTable ExecuteDataTable(DbTransaction transaction, CommandType commandType, string commandText, params MySqlParameter[] MySqlParameters)
        {

            if (transaction == null) throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");

            DbCommand objDbCommand = CreateCommand();
            bool mustCloseConnection = false;
            PrepareCommand(objDbCommand, transaction.Connection, transaction, commandType, commandText, MySqlParameters, out mustCloseConnection);
          
                using (DbDataAdapter dataAdapter = CreateDataAdapter(objDbCommand))
                {
                    DataTable objDataTable = new DataTable();
                    dataAdapter.Fill(objDataTable);
                    objDbCommand.Parameters.Clear();
                    return objDataTable;
                }
            }
           
       

    }
}
