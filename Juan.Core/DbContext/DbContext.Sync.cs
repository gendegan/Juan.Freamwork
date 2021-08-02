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
        /// <param name="objDataTable"></param>
        /// <param name="isIgnore"></param>
        /// <returns></returns>
        public virtual int SyncAdd(string tableName, DataTable objDataTable, bool isIgnore = true)
        {
            if (objDataTable.Rows.Count == 0)
            {
                return 0;
            }
            string sqlQuery = string.Format("INSERT " + (isIgnore ? "IGNORE" : "") + " INTO {0} ({1}) VALUES {2};", FieldName(tableName), SqlInsertFields(objDataTable.Columns), SqlInsertValues(objDataTable.Columns, objDataTable.Rows.Count));
            List<DataParameter> objDataParameterList = SqlInsertParams(objDataTable.Columns, objDataTable.Rows);
            return ExecuteNonQuery(sqlQuery, objDataParameterList.ToArray());
        }

        internal string SqlInsertFields(DataColumnCollection ojDataColumnCollection)
        {
            StringBuilder objQuery = new StringBuilder();
            foreach (DataColumn objDataColumn in ojDataColumnCollection)
            {
                objQuery.Append(FieldName(objDataColumn.ColumnName) + ",");
            }
            return objQuery.ToString().TrimEnd(new char[] { ',' });

        }

        internal string SqlInsertValues(DataColumnCollection ojDataColumnCollection)
        {


            StringBuilder objQuery = new StringBuilder();
            foreach (DataColumn objDataColumn in ojDataColumnCollection)
            {
                objQuery.Append(FieldParamName(objDataColumn.ColumnName) + ",");
            }
            return objQuery.ToString().TrimEnd(new char[] { ',' });

        }

        internal string SqlInsertValues(DataColumnCollection ojDataColumnCollection, int batchCount)
        {
            StringBuilder objQuery = new StringBuilder();
            for (int i = 0; i < batchCount; i++)
            {
                objQuery.Append("( ");
                string RowValue = "";
                foreach (DataColumn objDataColumn in ojDataColumnCollection)
                {
                    RowValue += FieldParamName(objDataColumn.ColumnName + "_" + i) + ",";
                }

                objQuery.Append(RowValue.TrimEnd(new char[] { ',' }));
                objQuery.Append("),");
            }
            return objQuery.ToString().TrimEnd(new char[] { ',' });
        }

        internal List<DataParameter> SqlInsertParams(DataColumnCollection ojDataColumnCollection, DataRow value)
        {

            List<DataParameter> objDataParameterList = new List<DataParameter>();
            foreach (DataColumn objDataColumn in ojDataColumnCollection)
            {
                objDataParameterList.Add(new DataParameter(objDataColumn.ColumnName, value[objDataColumn.ColumnName]));
            }
            return objDataParameterList;

        }


        internal virtual List<DataParameter> SqlInsertParams(DataColumnCollection ojDataColumnCollection, DataRowCollection valueList)
        {

            List<DataParameter> objDataParameterList = new List<DataParameter>();
            int i = 0;
            foreach (DataRow value in valueList)
            {
                foreach (DataColumn objDataColumn in ojDataColumnCollection)
                {
                    objDataParameterList.Add(new DataParameter(objDataColumn.ColumnName + "_" + i, value[objDataColumn.ColumnName]));
                }
                i++;
            }
            return objDataParameterList;
        }


    }
}
