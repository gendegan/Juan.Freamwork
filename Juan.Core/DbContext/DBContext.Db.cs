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
        /// 
        /// </summary>
        /// <returns></returns>
        internal DbConnection CreateConnection()
        {
            return CreateConnection(ContextHelper.ConnectionWriteString(this._ContextKey));

        }
        /// <summary>
        /// 创建连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        protected abstract DbConnection CreateConnection(string connectionString);
        /// <summary>
        /// 创建DbCommand
        /// </summary>
        /// <returns></returns>
        protected abstract DbCommand CreateCommand();
        /// <summary>
        /// 创建DbDataAdapter
        /// </summary>
        /// <param name="command">命令</param>
        /// <returns></returns>
        protected abstract DbDataAdapter CreateDataAdapter(DbCommand command);
        /// <summary>
        /// sql备注
        /// </summary>
        /// <param name="commandText">执行命令</param>
        /// <param name="commandType">命令类型</param>
        /// <returns></returns>
        protected abstract string SqlMark(string commandText, CommandType commandType);



        /// <summary>
        /// 参数返回值
        /// </summary>
        /// <param name="objDbParameterList"></param>
        /// <param name="dataParameters"></param>
        protected void DataParameterReturn(DbParameterCollection objDbParameterList, DataParameter[] dataParameters)
        {

            if (dataParameters != null && dataParameters.Length > 0 && objDbParameterList != null && objDbParameterList.Count > 0)
            {
                foreach (DataParameter objDataParameter in dataParameters.Where(s => s.Direction == ParameterDirection.Output || s.Direction == ParameterDirection.InputOutput))
                {
                    objDataParameter.Value = objDbParameterList[ParameterChar + objDataParameter.Name].Value;
                }

            }


        }


    }
}
