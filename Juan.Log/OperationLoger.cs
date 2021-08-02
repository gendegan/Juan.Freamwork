using Juan.Core;
using Juan.Log.Context;
using Juan.Log.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Log
{
    /// <summary>
    /// 操作类型
    /// </summary>
    public class OperationLoger
    {



        /// <summary>
        /// 判断Sql语句
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandTextTemp"></param>
        private void JudgeSqlLog(string commandText, string commandTextTemp)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(commandText))
                {
                    string tableName = commandText.MatchValue(@"INSERT\s*INTO\s*(?<tableName>\w+)\s*\(", "tableName");

                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        WriteLog(OperationType.Insert, tableName.Trim(), commandTextTemp);
                        return;
                    }
                    tableName = commandText.MatchValue(@"UPDATE\s*(?<tableName>\w+)\s*SET", "tableName");
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        WriteLog(OperationType.Update, tableName.Trim(), commandTextTemp);
                        return;
                    }
                    tableName = commandText.MatchValue(@"DELETE\s*FROM\s*(?<tableName>\w+)\s*", "tableName");
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        WriteLog(OperationType.Delete, tableName.Trim(), commandTextTemp);
                        return;
                    }
                }
            }
            catch (Exception objExp)
            {
                LogHelper.Write("分类操作日志出现异常", objExp);
            }

        }
        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="operationType"></param>
        /// <param name="tableName"></param>
        /// <param name="sqlQuery"></param>
        public void WriteLog(OperationType operationType, string tableName, string sqlQuery)
        {



        }

        /// <summary>
        /// 记录用户操作行为
        /// </summary>
        /// <param name="operationType"></param>
        /// <param name="MenuPowerID"></param>
        /// <param name="MenuName"></param>
        /// <param name="UserID"></param>
        /// <param name="Account"></param>
        /// <param name="CommandName"></param>
        /// <param name="Title"></param>
        /// <param name="Description"></param>
        /// <param name="OperationData"></param>
        public static void WriteOperatorLog(OperationType operationType, string MenuPowerID, string MenuName, int UserID, string Account, string CommandName, string Title, string Description, object OperationData)
        {


            try
            {
                if (string.IsNullOrWhiteSpace(MenuPowerID))
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(Title))
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(Account))
                {
                    Account = "";
                }

                if (string.IsNullOrWhiteSpace(CommandName))
                {
                    CommandName = operationType.ToString();
                }
                if (string.IsNullOrWhiteSpace(Description))
                {
                    Description = operationType.GetDescription() + ":" + Title;
                }
                if (string.IsNullOrWhiteSpace(MenuName))
                {
                    MenuName = "";
                }
                OperationHistory objOperationHistory = new OperationHistory();
                //菜单标识
                objOperationHistory.MenuPowerID = MenuPowerID;
                //用户标识
                objOperationHistory.UserID = UserID;
                objOperationHistory.MenuName = MenuName;
                //操作帐号
                objOperationHistory.Account = Account;
                //0无1新增2修改3删除4发布5取消发布6审核7取消审核8推荐9取消推荐
                objOperationHistory.OperationTypeID = (int)operationType;
                //操作命名
                objOperationHistory.CommandName = CommandName.ToCutWord(10);
                //创建时间
                objOperationHistory.CreateDate = DateTime.Now;
                //标题
                objOperationHistory.Title = Title.ToCutWord(500);
                //操作描述
                objOperationHistory.Description = Description.ToCutWord(500);
                //操作数据
                objOperationHistory.OperationData = OperationData == null ? "" : OperationData.JsonSerialize();
                //用户IP
                objOperationHistory.UserHostAddress = RequestHelper.GetRealIp();
                OperationHistoryContext objOperationHistoryContext = new OperationHistoryContext();
                objOperationHistoryContext.Add(objOperationHistory);
            }
            catch (Exception objExp)
            {


                LogHelper.Write("记录用户操作行为异常", objExp);

            }


        }

    }
}
