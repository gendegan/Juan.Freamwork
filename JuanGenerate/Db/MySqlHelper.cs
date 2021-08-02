using MySql.Data.MySqlClient;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace JuanGenerate
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public partial class MySqlHelper
    {


        string _ConnectionKeyOrConnectionString = "";
        /// <summary>
        /// 
        /// </summary>
        public string ConnectionKeyOrConnectionString
        {
            get
            {
                return _ConnectionKeyOrConnectionString;
            }
            set
            {
                _ConnectionKeyOrConnectionString = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionKeyOrConnectionString"></param>
        /// <returns></returns>
        public static string GetConnectionString(string connectionKeyOrConnectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionKeyOrConnectionString))
            {
                throw new ArgumentNullException(connectionKeyOrConnectionString, "参数connectionKeyOrConnectionString不能为空值");
            }

            string connectionString = "";

            if (connectionKeyOrConnectionString.Split('=').Count() > 2)
            {
                connectionString = connectionKeyOrConnectionString;
            }
            else
            {

                connectionString = ConfigurationManager.ConnectionStrings[connectionKeyOrConnectionString].ToString();
            }
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException("对不起你设置的连接:" + connectionKeyOrConnectionString + "获取不到相关值");
            }
            return connectionString;

        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionKeyOrConnectionString"></param>
        public MySqlHelper(string connectionKeyOrConnectionString)
        {
            connectionKeyOrConnectionString.ArgumentNoNull("connectionKeyOrConnectionString", "请设置数据库连接串");
            _ConnectionKeyOrConnectionString = connectionKeyOrConnectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataParameters"></param>
        /// <returns></returns>
        public static List<DbParameter> AttachParameters(MySqlParameter[] dataParameters)
        {
            List<DbParameter> objMySqlParameterList = new List<DbParameter>();

            if (dataParameters != null)
            {
                foreach (MySqlParameter objMySqlParameter in dataParameters)
                {
                    objMySqlParameterList.Add(objMySqlParameter);
                }
            }
            return objMySqlParameterList;
        }

        private static void PrepareCommand(DbCommand command, DbConnection connection, DbTransaction transaction, CommandType commandType, string commandText, MySqlParameter[] MySqlParameters, out bool mustCloseConnection)
        {
            command.ArgumentNoNull("command");
            commandText.ArgumentNoNull("commandText");


            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }
            command.Connection = connection;


            command.CommandText = SqlMark(commandText, commandType);

            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }
            command.CommandType = commandType;
            if (MySqlParameters != null)
            {
                List<DbParameter> objDbParameterList = AttachParameters(MySqlParameters);
                if (objDbParameterList != null && objDbParameterList.Count > 0)
                {
                    command.Parameters.AddRange(objDbParameterList.ToArray());
                }
            }
            return;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DbConnection CreateConnection()
        {
            return CreateConnection(this._ConnectionKeyOrConnectionString);

        }
        public static DbConnection CreateConnection(string connectionString)
        {


            return new MySqlConnection(GetConnectionString(connectionString));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DbCommand CreateCommand()
        {

            return new MySqlCommand();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static DbDataAdapter CreateDataAdapter(System.Data.Common.DbCommand command)
        {
            return new MySqlDataAdapter((MySqlCommand)command);
        }
        /// <summary>
        /// sql备注
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        protected static string SqlMark(string commandText, CommandType commandType)
        {
            if (commandType != CommandType.StoredProcedure)
            {
                return "/*Juan*/  " + commandText;
            }
            return commandText;
        }





    }
}
