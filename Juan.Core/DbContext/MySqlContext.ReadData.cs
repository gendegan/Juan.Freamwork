using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    public partial class MySqlContext : DbContext
    {



        /// <summary>
        /// 
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        protected override string ReadLimitExpression(ReadData readOptions)
        {

            if (readOptions.PageSize != null && readOptions.PageIndex != null)
            {
                return string.Format(" LIMIT {0},{1}", readOptions.PageSize * readOptions.PageIndex, readOptions.PageSize);
            }
            else if (readOptions.Skip != null && readOptions.Take != null)
            {
                return string.Format(" LIMIT {0},{1}", readOptions.Skip, readOptions.Take);
            }
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        protected override string ReadSqlData(ReadData readOptions)
        {

            string tableName = ReadTableName(readOptions);
            string condition = ReadCondition(readOptions);
            string sortExpression = ReadSortExpression(readOptions);
            string fields = ReadFields(readOptions);
            string limitExpression = ReadLimitExpression(readOptions);
            string sqlQuery = "SELECT " + fields + " FROM " + tableName + " ";
            if (!String.IsNullOrEmpty(condition))
            {
                sqlQuery += " WHERE " + condition;
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                sqlQuery += " ORDER BY  " + sortExpression;
            }
            if (!String.IsNullOrEmpty(limitExpression))
            {
                sqlQuery += " " + limitExpression;
            }
            return sqlQuery;
        }

        /// <summary>
        /// 返回查询单条数据语句
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        protected override string ReadSqlDataOne(ReadData readOptions)
        {
            string tableName = ReadTableName(readOptions);
            string condition = ReadCondition(readOptions);
            string sortExpression = ReadSortExpression(readOptions);

            string fields = ReadFields(readOptions);
            string limitExpression = ReadLimitExpression(readOptions);
            string sqlQuery = "SELECT " + fields + " FROM " + tableName + " ";
            if (!String.IsNullOrEmpty(condition))
            {
                sqlQuery += " WHERE " + condition;
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                sqlQuery += " ORDER BY  " + sortExpression;
            }
            sqlQuery += " Limit 0,1 ";
            return sqlQuery;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="readOptions"></param>
        /// <returns></returns>
        protected override string ReadSqlDataAny(ReadData readOptions)
        {
            string tableName = ReadTableName(readOptions);
            string condition = ReadCondition(readOptions);
            string sortExpression = ReadSortExpression(readOptions);

            string fields = ReadFields(readOptions);
            string limitExpression = ReadLimitExpression(readOptions);
            string sqlQuery = "SELECT 1 as AnyCount FROM " + tableName + " ";
            if (!String.IsNullOrEmpty(condition))
            {
                sqlQuery += " WHERE " + condition;
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                sqlQuery += " ORDER BY  " + sortExpression;
            }
            sqlQuery += " Limit 0,1 ";
            return sqlQuery;
        }

        /// <summary>
        /// 读取表某个字段
        /// </summary>
        /// <param name="filedName"></param>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        protected override string ReadSqlField(string filedName, ReadData readOptions)
        {
            if (string.IsNullOrWhiteSpace(filedName))
            {
                throw new ArgumentNullException("请设置filedName要读取的字段的名称");
            }
            string tableName = ReadTableName(readOptions);
            string condition = ReadCondition(readOptions);
            string sortExpression = ReadSortExpression(readOptions);

            string limitExpression = ReadLimitExpression(readOptions);
            string sqlQuery = "SELECT " + filedName + " FROM " + tableName + " ";
            if (!String.IsNullOrEmpty(condition))
            {
                sqlQuery += " WHERE " + condition;
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                sqlQuery += " ORDER BY  " + sortExpression;
            }
            if (!String.IsNullOrEmpty(limitExpression))
            {
                sqlQuery += " " + limitExpression;
            }
            return sqlQuery;
        }

        /// <summary>
        /// 读取某个字段的第一个值
        /// </summary>
        /// <param name="filedName"></param>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        protected override string ReadSqlFieldScalar(string filedName, ReadData readOptions)
        {
            if (string.IsNullOrWhiteSpace(filedName))
            {
                throw new ArgumentNullException("请设置filedName要读取的字段的名称");
            }
            string tableName = ReadTableName(readOptions);
            string condition = ReadCondition(readOptions);
            string sortExpression = ReadSortExpression(readOptions);
            string limitExpression = ReadLimitExpression(readOptions);
            string sqlQuery = "SELECT " + filedName + " FROM " + tableName + " ";
            if (!String.IsNullOrEmpty(condition))
            {
                sqlQuery += " WHERE " + condition;
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                sqlQuery += " ORDER BY  " + sortExpression;
            }
            sqlQuery += " Limit 0,1 ";
            return sqlQuery;
        }



        /// <summary>
        /// 读取分页查询语句
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        protected override string ReadSqlPage(ReadData readOptions)
        {
            string tableName = ReadTableName(readOptions);
            string condition = ReadCondition(readOptions);
            string sortExpression = ReadSortExpression(readOptions);
            string fields = ReadFields(readOptions);
            string limitExpression = ReadLimitExpression(readOptions);
            string sqlQuery = "SELECT " + fields + " FROM " + tableName + " ";
            if (!String.IsNullOrEmpty(condition))
            {
                sqlQuery += " WHERE " + condition;
            }
            if (!String.IsNullOrEmpty(sortExpression))
            {
                sqlQuery += " ORDER BY  " + sortExpression;
            }
            if (!String.IsNullOrEmpty(limitExpression))
            {
                sqlQuery += " " + limitExpression;
            }
            sqlQuery += "; ";
            sqlQuery += ReadSqlCount(readOptions) + ";";
            return sqlQuery;
        }
    }
}
