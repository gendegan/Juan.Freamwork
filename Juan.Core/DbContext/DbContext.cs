using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public abstract partial class DbContext
    {


        string _ContextKey = "";
        /// <summary>
        /// 
        /// </summary>
        public string ContextKey
        {
            get
            {
                return _ContextKey;
            }
            set
            {
                _ContextKey = value;
            }
        }





        /// <summary>
        /// 
        /// </summary>
        public abstract ContextType ContextType
        {
            get;

        }

        ConnectionType _ConnectionType = ConnectionType.Auto;
        /// <summary>
        /// 连接类型
        /// </summary>
        public ConnectionType ConnectionType
        {
            get
            {
                return _ConnectionType;
            }
            set
            {
                _ConnectionType = value;
            }

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextKey">上下文连接键值</param>
        /// <param name="connectionType"></param>
        public DbContext(string contextKey, ConnectionType connectionType = ConnectionType.Auto)
        {

            IsExecuted = true;
            IsExecuteing = true;
            IsExecuteError = true;
            contextKey.ArgumentNoNull("contextKey", "contextKey上下文配置值不能空");
            _ContextKey = contextKey;
            _ConnectionType = connectionType;


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextKey">上下文连接键值</param>
        /// <param name="connectionType"></param>
        /// <returns></returns>
        public static DbContext Create(string contextKey, ConnectionType connectionType = ConnectionType.Auto)
        {
            contextKey.ArgumentNoNull("contextKey", "contextKey上下文配置值不能空");
            contextKey = contextKey.Replace(".ConnectionString", "").Replace(".ConnectionString.Read", "").Replace("ConnectionString", "");
            ContextType objContextType = ContextHelper.ContextType(contextKey);
            if (objContextType == ContextType.MySql)
            {
                return new MySqlContext(contextKey, connectionType);
            }
            return new MySqlContext(contextKey, connectionType);
        }


        private void PrepareCommand(DbCommand command, DbConnection connection, DbTransaction transaction, CommandType commandType, string commandText, DataParameter[] dataParameters, out bool mustCloseConnection)
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


            List<string> likeFields = new List<string>();
            if (commandType == CommandType.Text && dataParameters != null && dataParameters.Count() > 0)
            {
                if (commandText != null && commandText.Contains("SELECT ", StringComparison.OrdinalIgnoreCase))
                {
                    GroupCollection objGroupCollection = commandText.MatchGroups(@"\w+\s+LIKE\s+\?(?<ParameterName>\w+)");
                    for (int i = 0; i < objGroupCollection.Count; i++)
                    {
                        Group objGroup = objGroupCollection[i];
                        likeFields.Add(objGroup.Value);
                    }
                }
            }

            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("请使用已经打开连接中的事务对象", "transaction");
                command.Transaction = transaction;
            }
            command.CommandType = commandType;
            if (dataParameters != null && dataParameters.Count() > 0)

                foreach (DataParameter objDataParameter in dataParameters)
                {
                    DbParameter objDbParameter = command.CreateParameter();
                    objDbParameter.ParameterName = ParameterChar + objDataParameter.Name;
                    if (objDataParameter.Direction != ParameterDirection.Output || objDataParameter.Value != null)
                    {
                        if (objDataParameter.Value is DateTime && ContextType == Core.ContextType.MySql)
                        {
                            DateTime objDateTime = ((DateTime)objDataParameter.Value);
                            objDbParameter.Value = objDateTime.AddMilliseconds(-objDateTime.Millisecond);
                        }
                        else if (objDataParameter.Value is string && likeFields.Contains(objDataParameter.Name, StringComparer.OrdinalIgnoreCase))
                        {
                            string ParameterValue = objDataParameter.Value.ToString();
                            string startLikeChar = ParameterValue.StartsWith("%") ? "%" : "";
                            string endLikeChar = ParameterValue.EndsWith("%") ? "%" : "";
                            ParameterValue = ParameterValue.Trim("%");
                            ParameterValue = ParameterValue.FilterSqlSpecialKey();
                            objDbParameter.Value = startLikeChar + ParameterValue + endLikeChar;
                        }
                        else
                        {
                            objDbParameter.Value = objDataParameter.Value;
                        }

                    }
                    objDbParameter.DbType = objDataParameter.DbType;
                    objDbParameter.Direction = objDataParameter.Direction;
                    command.Parameters.Add(objDbParameter);
                }

        }





        /// <summary>
        /// 选连接串类型
        /// </summary>
        /// <param name="contextKey">上下文连接键值</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">执行命令</param>
        /// <returns></returns>
        public string SelectConnectionString(string contextKey, CommandType commandType, string commandText)
        {

            contextKey.ArgumentNoNull("contextKey");
            bool IsContextWrite = ConfigHelper.GetBool("IsContextWrite", false);
            if (_ConnectionType == ConnectionType.Write || IsContextWrite)
            {
                return ContextHelper.ConnectionWriteString(contextKey);
            }
            bool isWriteOpertor = IsWriteOpertor(commandType, commandText);
            if (isWriteOpertor)
            {
                return ContextHelper.ConnectionWriteString(contextKey);
            }
            else
            {
                return ContextHelper.ConnectionReadString(contextKey);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">执行命令</param>
        /// <returns></returns>
        public virtual bool IsWriteOpertor(CommandType commandType, string commandText)
        {
            commandText = commandText.Trim().ToUpper();
            if (commandType == CommandType.Text)
            {
                if (commandText.StartsWith("SELECT ")
                     && !commandText.Contains("INSERT ")
                     && !commandText.Contains("UPDATE ")
                     && !commandText.Contains("DELETE ")
                     && !commandText.Contains("REPLACE ")
                     && !commandText.Contains("CREATE ")
                     && !commandText.Contains("DROP ")
                     && !commandText.Contains("SHOW ")
                     && !commandText.Contains("ALTER "))
                {
                    return false;

                }
                else if (commandText.StartsWith("INSERT ")
                      || commandText.StartsWith("UPDATE ")
                      || commandText.StartsWith("DELETE ")
                      || commandText.StartsWith("REPLACE ")
                      || commandText.StartsWith("CREATE ")
                      || commandText.StartsWith("DROP ")
                      || commandText.StartsWith("SHOW ")
                      || commandText.StartsWith("ALTER "))
                {
                    return true;
                }


                if (commandText.IsMatch(@"INSERT\s*INTO\s*(?<tableName>[^\s]+)\s*"))
                {
                    return true;
                }
                if (commandText.IsMatch(@"INSERT\s*IGNORE\s*INTO\s*(?<tableName>[^\s]+)\s*"))
                {
                    return true;
                }
                if (commandText.IsMatch(@"UPDATE\s*(?<tableName>[^\s]+)\s*SET"))
                {
                    return true;
                }
                if (commandText.IsMatch(@"DELETE\s*FROM\s*(?<tableName>[^\s]+)\s*"))
                {
                    return true;
                }

                if (commandText.IsMatch(@"REPLACE\s*INTO\s*(?<tableName>[^\s]+)\s*"))
                {
                    return true;
                }
                if (commandText.IsMatch(@"CREATE\s*TABLE\s*(?<tableName>[^\s]+)\s*"))
                {
                    return true;
                }
                if (commandText.IsMatch(@"CREATE\s*VIEW\s*(?<tableName>[^\s]+)\s*"))
                {
                    return true;
                }
                if (commandText.IsMatch(@"DROP\s*TABLE\s*(?<tableName>[^\s]+)\s*"))
                {
                    return true;
                }
                if (commandText.IsMatch(@"ALTER\s*TABLE\s*(?<tableName>[^\s]+)\s*"))
                {
                    return true;
                }
                if (commandText.IsMatch(@"SHOW\s*TABLES\s*"))
                {
                    return true;
                }

                return false;

            }
            else if (commandType == CommandType.StoredProcedure)
            {
                if (commandText.IsMatch("_WRITE$"))
                {
                    return true;
                }
                if (commandText.IsMatch("_READ$"))
                {
                    return false;
                }
                return true;

            }
            else
            {
                return true;
            }
        }




    }
}
