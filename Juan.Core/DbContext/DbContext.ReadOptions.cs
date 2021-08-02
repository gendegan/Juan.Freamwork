using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    public abstract partial class DbContext
    {

        /// <summary>
        /// 读取表名
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        protected string ReadTableName(ReadData readOptions)
        {
            if (!string.IsNullOrWhiteSpace(readOptions.CommandText))
            {
                return " (" + readOptions.CommandText + ") AS CommandTextTable";
            }
            else
            {
                readOptions.TableName.ArgumentNoNull("TableName", "TableName不能为空");
                return readOptions.TableName;

            }
        }


        /// <summary>
        /// 读取条件
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        protected string ReadCondition(ReadData readOptions)
        {
            if (string.IsNullOrWhiteSpace(readOptions.Condition))
            {
                return readOptions.Condition;
            }
            return ParamCharReplace(readOptions.Condition);
        }


        /// <summary>
        /// 读取字段
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        protected string ReadFields(ReadData readOptions)
        {
            return string.IsNullOrWhiteSpace(readOptions.Fields) ? "*" : readOptions.Fields;
        }

        /// <summary>
        /// 读取排序语句
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        protected string ReadSortExpression(ReadData readOptions)
        {

            return readOptions.SortExpression;
        }

        /// <summary>
        /// 返回读取条数语句
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        protected virtual string ReadSqlCount(ReadData readOptions)
        {
            string tableName = ReadTableName(readOptions);
            string condition = ReadCondition(readOptions);
            string fields = ReadFields(readOptions);
            string sqlQuery = "";
            if (fields.ToUpper().IndexOf("DISTINCT") >= 0)
            {

                sqlQuery = "SELECT " + fields + " FROM " + tableName;
                if (!String.IsNullOrEmpty(condition))
                {
                    sqlQuery += " WHERE " + condition;
                }
                sqlQuery = "SELECT COUNT(*) AS RecordCount FROM (" + sqlQuery + ") As Temp";
            }
            else
            {
                sqlQuery = "SELECT COUNT(*) AS RecordCount FROM " + tableName;
                if (!String.IsNullOrEmpty(condition))
                {
                    sqlQuery += " WHERE " + condition;
                }
            }
            return sqlQuery;
        }

        /// <summary>
        /// 返回删除数据语句
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        protected virtual string ReadSqlDelete(ReadData readOptions)
        {
            string tableName = ReadTableName(readOptions);
            string condition = ReadCondition(readOptions);

            string sqlQuery = "DELETE FROM " + tableName;
            if (!String.IsNullOrEmpty(condition))
            {
                sqlQuery += " WHERE " + condition;
            }

            return sqlQuery;
        }


        /// <summary>
        /// 读取数据条件
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        protected abstract string ReadLimitExpression(ReadData readOptions);

        /// <summary>
        /// 返回查询数据语句
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        protected abstract string ReadSqlData(ReadData readOptions);

        /// <summary>
        /// 返回查询单条数据语句
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        protected abstract string ReadSqlDataOne(ReadData readOptions);

        /// <summary>
        /// 返回查询是否存在数据语句
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        protected abstract string ReadSqlDataAny(ReadData readOptions);

        /// <summary>
        /// 读取表某个字段
        /// </summary>
        /// <param name="filedName"></param>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        protected abstract string ReadSqlField(string filedName, ReadData readOptions);

        /// <summary>
        /// 读取某个字段的第一个值
        /// </summary>
        /// <param name="filedName"></param>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        protected abstract string ReadSqlFieldScalar(string filedName, ReadData readOptions);



        /// <summary>
        /// 读取分页查询语句
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        protected abstract string ReadSqlPage(ReadData readOptions);



    }
}
