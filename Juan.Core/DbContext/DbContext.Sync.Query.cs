using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Juan.Core
{
    public abstract partial class DbContext
    {




        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="primaryKey"></param>
        /// <param name="objDataTable"></param>
        /// <returns></returns>
        public virtual string SyncUpdateQuery(string tableName, string primaryKey, DataTable objDataTable)
        {
            if (objDataTable.Rows.Count == 0)
            {
                return "";
            }
            StringBuilder sqlBuilder = new StringBuilder(256);
            List<DataParameter> objDataParameterList = new List<DataParameter>();
            foreach (DataRow value in objDataTable.Rows)
            {
                sqlBuilder.AppendLine("UPDATE " + FieldName(tableName) + " SET ");
                StringBuilder sqlRowBuilder = new StringBuilder();
                foreach (DataColumn objDataColumn in objDataTable.Columns)
                {
                    if (!objDataColumn.ColumnName.Equals(primaryKey, StringComparison.OrdinalIgnoreCase))
                    {
                        sqlRowBuilder.AppendFormat("{0}={1},", FieldName(objDataColumn.ColumnName), ToFieldValue(value[objDataColumn.ColumnName]));
                    }
                }
                sqlBuilder.Append(sqlRowBuilder.ToString().Trim(','));

                sqlBuilder.AppendFormat(" WHERE {0}={1};", FieldName(primaryKey), ToFieldValue(value[primaryKey]));
            }
            return sqlBuilder.ToString();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="objDataTable"></param>
        /// <param name="isIgnore"></param>
        /// <returns></returns>
        public virtual string SyncAddQuery(string tableName, DataTable objDataTable, bool isIgnore = true)
        {
            if (objDataTable.Rows.Count == 0)
            {
                return "";
            }
            StringBuilder sqlBuilder = new StringBuilder(256);
            sqlBuilder.AppendFormat("INSERT " + (isIgnore ? "IGNORE" : "") + " INTO {0} ({1}) VALUES", FieldName(tableName), SqlInsertFields(objDataTable.Columns));

            foreach (DataRow value in objDataTable.Rows)
            {
                StringBuilder sqlRowBuilder = new StringBuilder();
                sqlBuilder.AppendLine("(");
                foreach (DataColumn objDataColumn in objDataTable.Columns)
                {
                    sqlRowBuilder.Append(ToFieldValue(value[objDataColumn.ColumnName]) + ",");
                }
                sqlBuilder.Append(sqlRowBuilder.ToString().Trim(',') + "),");
            }
            return sqlBuilder.ToString().Trim(',');
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="objDataTable"></param>
        /// <returns></returns>
        public virtual string SyncDataQuery(string tableName, DataTable objDataTable)
        {
            if (objDataTable.Rows.Count == 0)
            {
                return "";
            }
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("REPLACE INTO {0} ({1}) VALUES", FieldName(tableName), SqlInsertFields(objDataTable.Columns));

            foreach (DataRow value in objDataTable.Rows)
            {

                sqlBuilder.AppendLine("(");
                StringBuilder sqlRowBuilder = new StringBuilder();
                foreach (DataColumn objDataColumn in objDataTable.Columns)
                {
                    sqlRowBuilder.Append(ToFieldValue(value[objDataColumn.ColumnName]) + ",");
                }
                sqlBuilder.Append(sqlRowBuilder.ToString().Trim(',') + "),");
            }
            return sqlBuilder.ToString().Trim(',');
        }

    }
}
