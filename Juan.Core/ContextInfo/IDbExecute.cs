using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    /// <summary>
    /// 数据执行步骤
    /// </summary>
    public abstract class IDbExecute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="connection">连接对象</param>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="fullCommandText"></param>
        /// <param name="commandText">执行命令</param>
        /// <param name="dataParameters">参数</param>
        public abstract void OnExecuteing(DbCommand command, DbConnection connection, DbTransaction transaction, CommandType commandType, string fullCommandText, string commandText, params DataParameter[] dataParameters);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="connection">连接对象</param>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="fullCommandText"></param>
        /// <param name="commandText">执行命令</param>
        /// <param name="dataParameters">参数</param>
        public abstract void OnExecuted(DbCommand command, DbConnection connection, DbTransaction transaction, CommandType commandType, string fullCommandText, string commandText, params DataParameter[] dataParameters);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objExp"></param>
        /// <param name="command">命令</param>
        /// <param name="connection">连接对象</param>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="fullCommandText"></param>
        /// <param name="commandText">执行命令</param>
        /// <param name="dataParameters">参数</param>
        public abstract void OnExecuteError(Exception objExp, DbCommand command, DbConnection connection, DbTransaction transaction, CommandType commandType, string fullCommandText, string commandText, params DataParameter[] dataParameters);

    }
}
