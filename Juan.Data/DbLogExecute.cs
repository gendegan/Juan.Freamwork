using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Data
{
    /// <summary>
    /// 执行日志
    /// </summary>
    public class DbLogExecute : IDbExecute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="connection"></param>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType"></param>
        /// <param name="fullCommandText"></param>
        /// <param name="commandText"></param>
        /// <param name="dataParameters"></param>
        public override void OnExecuteing(System.Data.Common.DbCommand command, System.Data.Common.DbConnection connection, System.Data.Common.DbTransaction transaction, System.Data.CommandType commandType, string fullCommandText, string commandText, params DataParameter[] dataParameters)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="connection"></param>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType"></param>
        /// <param name="fullCommandText"></param>
        /// <param name="commandText"></param>
        /// <param name="dataParameters"></param>
        public override void OnExecuted(System.Data.Common.DbCommand command, System.Data.Common.DbConnection connection, System.Data.Common.DbTransaction transaction, System.Data.CommandType commandType, string fullCommandText, string commandText, params DataParameter[] dataParameters)
        {
            if (LogSectionHelper.IsSql)
            {
                LogHelper.Write(LogType.Sql, fullCommandText, fullCommandText);
            }

           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objExp"></param>
        /// <param name="command"></param>
        /// <param name="connection"></param>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType"></param>
        /// <param name="fullCommandText"></param>
        /// <param name="commandText"></param>
        /// <param name="dataParameters"></param>
        public override void OnExecuteError(Exception objExp, System.Data.Common.DbCommand command, System.Data.Common.DbConnection connection, System.Data.Common.DbTransaction transaction, System.Data.CommandType commandType, string fullCommandText, string commandText, params DataParameter[] dataParameters)
        {

            LogHelper.Write(LogType.Error, "执行语句异常:" + fullCommandText, fullCommandText, objExp);


        }
    }
}
