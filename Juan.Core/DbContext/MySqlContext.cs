using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MySqlContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contextKey">上下文连接键值</param>
        /// <param name="connectionType"></param>
        public MySqlContext(string contextKey, ConnectionType connectionType = ConnectionType.Auto)
            : base(contextKey, connectionType)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        public override string ParameterChar
        {
            get { return "?"; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override string FieldLeftChar
        {
            get { return "`"; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override string FieldRightChar
        {
            get { return "`"; }
        }
        /// <summary>
        /// 
        /// </summary>
        public override ContextType ContextType
        {
            get
            {
                return ContextType.MySql;
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        protected override DbConnection CreateConnection(string connectionString)
        {

            return new MySqlConnection(connectionString);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override DbCommand CreateCommand()
        {

            return new MySqlCommand();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command">命令</param>
        /// <returns></returns>
        protected override DbDataAdapter CreateDataAdapter(System.Data.Common.DbCommand command)
        {
            return new MySqlDataAdapter((MySqlCommand)command);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText">执行命令</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        protected override string SqlMark(string commandText, CommandType commandType)
        {
            if (commandType != CommandType.StoredProcedure)
            {
                return "/*" + LogSectionHelper.ApplicationCode + "*/  " + commandText;
            }
            return commandText;
        }
    }
}
