using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    public abstract partial class DbContext
    {


        /// <summary>
        /// 值转成相应数据库的值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        protected virtual string ObjectToDbValue(object value)
        {

            string valueString = "";
            //如果为NULL
            if (value == null)
            {
                valueString = "null";
            }
            else if (value is bool)
            {
                valueString = (bool)value ? "1" : "0";
            }
            else
            {
                valueString = value.ToString().Replace("'", "''");
            }

            if (value != null)
            {
                if (value.GetType() == typeof(String))
                {
                    valueString = "'" + valueString + "'";
                }
                if (value.GetType() == typeof(DateTime))
                {
                    valueString = "'" + ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss") + "'";
                }
            }
            return valueString;

        }
        /// <summary>
        /// 当前执行语句
        /// </summary>
        public string CurrentCommandText
        {
            get;
            set;
        }
        /// <summary>
        /// 转成相应的完整 执行语句
        /// </summary>
        /// <param name="commandText">执行命令</param>
        /// <param name="parms"></param>
        /// <returns></returns>
        protected virtual string ToFullCommandText(string commandText, DataParameter[] parms)
        {
            string fullCommandText = commandText;
            if (parms != null)
            {
                foreach (DataParameter objDataParameter in parms)
                {
                    fullCommandText = fullCommandText.Replace(ParameterChar + objDataParameter.Name, ObjectToDbValue(objDataParameter.Value));
                }
            }
            return fullCommandText;
        }

        /// <summary>
        /// 执行结束
        /// </summary>
        public IDbExecute DbExecute
        {
            get;
            set;
        }

        /// <summary>
        /// 执行前
        /// </summary>
        public bool IsExecuteing
        {
            get;
            set;
        }
        /// <summary>
        /// 关闭事件
        /// </summary>
        public void CloseExecute()
        {
            IsExecuteing = false;
            IsExecuted = false;
            IsExecuteError = false;

        }
        /// <summary>
        /// 打开事件
        /// </summary>
        public void OpenExecute()
        {
            IsExecuteing = true;
            IsExecuted = true;
            IsExecuteError = true;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="connection">连接对象</param>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令执行文本</param>
        /// <param name="dataParameters">参数</param>
        private void OnExecuteing(DbCommand command, DbConnection connection, DbTransaction transaction, CommandType commandType, string commandText, params DataParameter[] dataParameters)
        {

            CurrentCommandText = ToFullCommandText(commandText, dataParameters);
            if (DbExecute != null && IsExecuteing)
            {
                DbExecute.OnExecuteing(command, connection, transaction, commandType, ToFullCommandText(commandText, dataParameters), commandText, dataParameters);
            }

        }
        /// <summary>
        /// 是否执行Execute
        /// </summary>
        public bool IsExecuted
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="connection">连接对象</param>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令执行文本</param>
        /// <param name="dataParameters">参数</param>
        private void OnExecuted(DbCommand command, DbConnection connection, DbTransaction transaction, CommandType commandType, string commandText, params DataParameter[] dataParameters)
        {
            if (DbExecute != null && IsExecuted)
            {
                DbExecute.OnExecuted(command, connection, transaction, commandType, ToFullCommandText(commandText, dataParameters), commandText, dataParameters);
            }

        }

        /// <summary>
        /// 是否执行异常
        /// </summary>
        public bool IsExecuteError
        {
            get;
            set;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="objExp"></param>
        /// <param name="command">命令</param>
        /// <param name="connection">连接对象</param>
        /// <param name="transaction">事务对象</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令执行文本</param>
        /// <param name="dataParameters">参数</param>
        private void OnExecuteError(Exception objExp, DbCommand command, DbConnection connection, DbTransaction transaction, CommandType commandType, string commandText, params DataParameter[] dataParameters)
        {
            string FullCommandText = ToFullCommandText(commandText, dataParameters);

            objExp.Data.Add("CommandText", FullCommandText);
            if (DbExecute != null && IsExecuteError)
            {
                DbExecute.OnExecuteError(objExp, command, connection, transaction, commandType, FullCommandText, commandText, dataParameters);

            }
        }



    }
}
