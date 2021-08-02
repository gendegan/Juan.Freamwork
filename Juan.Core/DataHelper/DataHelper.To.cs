using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Juan.Core
{
    public static partial class DataHelper
    {

        /// <summary>
        /// DataTable转换成实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objDataTable">表</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable objDataTable) where T : class, new()
        {
            List<T> objList = new List<T>();
            if (objDataTable.Rows.Count == 0)
            {
                return objList;
            }
            PropertyInfo[] objPropertyInfoList = typeof(T).GetProperties();
            foreach (DataRow objDataRow in objDataTable.Rows)
            {
                T objT = new T();
                foreach (DataColumn objDataColumn in objDataTable.Columns)
                {
                    PropertyInfo objPropertyInfo = objPropertyInfoList.FirstOrDefault(s => s.Name.Equals(objDataColumn.ColumnName, StringComparison.OrdinalIgnoreCase));
                    if (objPropertyInfo == null || !objPropertyInfo.CanWrite)
                    {
                        continue;
                    }
                    if (objDataRow.IsNull(objDataColumn.ColumnName))
                    {
                        continue;
                    }
                    object dataValue = objDataRow[objDataColumn.ColumnName];
                    if (dataValue == null)
                    {
                        continue;
                    }
                    if (objPropertyInfo.PropertyType == typeof(Boolean))
                    {
                        string value = dataValue.ToString();
                        objPropertyInfo.SetValue(objT, (value.Equals("1") || value.Equals("true", StringComparison.OrdinalIgnoreCase) ? true : false), null);
                    }
                    else if (objPropertyInfo.PropertyType != dataValue.GetType())
                    {
                        objPropertyInfo.SetValue(objT, Convert.ChangeType(dataValue, objPropertyInfo.PropertyType), null);
                    }
                    else
                    {
                        objPropertyInfo.SetValue(objT, dataValue, null);
                    }

                }
                objList.Add(objT);
            }
            return objList;

        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataReader"></param>
        /// <param name="closeReader">默认读完就关闭</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DbDataReader dataReader, bool closeReader = true) where T : class, new()
        {
            List<T> objList = new List<T>();
            int fieldCount = dataReader.FieldCount;
            string fieldName = string.Empty;
            PropertyInfo[] objPropertyInfoList = typeof(T).GetProperties();
            while (dataReader.Read())
            {
                T objT = new T();
                for (int i = 0; i < fieldCount; i++)
                {
                    fieldName = dataReader.GetName(i);
                    PropertyInfo objPropertyInfo = objPropertyInfoList.FirstOrDefault(s => s.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase));
                    if (objPropertyInfo == null || !objPropertyInfo.CanWrite)
                    {
                        continue;
                    }
                    if (dataReader.IsDBNull(i))
                    {
                        continue;
                    }
                    object dataValue = dataReader[fieldName];
                    if (dataValue == null)
                    {
                        continue;
                    }
                    if (objPropertyInfo.PropertyType == typeof(Boolean))
                    {
                        string value = dataValue.ToString();
                        objPropertyInfo.SetValue(objT, (value.Equals("1") || value.Equals("true", StringComparison.OrdinalIgnoreCase) ? true : false), null);
                    }
                    else if (objPropertyInfo.PropertyType != dataValue.GetType())
                    {
                        objPropertyInfo.SetValue(objT, Convert.ChangeType(dataValue, objPropertyInfo.PropertyType), null);
                    }
                    else
                    {
                        objPropertyInfo.SetValue(objT, dataValue, null);
                    }


                }
                objList.Add(objT);
            }
            if (closeReader)
            {
                dataReader.Close();
            }
            return objList;

        }


        /// <summary>
        ///  DataReader转成列表
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="closeReader"></param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> ToList(this DbDataReader dataReader, bool closeReader = true)
        {
            List<Dictionary<string, object>> objList = new List<Dictionary<string, object>>();
            int fieldCount = dataReader.FieldCount;
            string fieldName = string.Empty;
            while (dataReader.Read())
            {
                Dictionary<string, object> objData = new Dictionary<string, object>();
                for (int i = 0; i < fieldCount; i++)
                {
                    fieldName = dataReader.GetName(i);
                    objData[fieldName] = dataReader[fieldName];
                }
                objList.Add(objData);
            }
            if (closeReader)
            {
                dataReader.Close();
            }
            return objList;

        }

        /// <summary>
        /// DataReader字段转成列表
        /// </summary>
        /// <typeparam name="P">类型</typeparam>
        /// <param name="dataReader"></param>
        /// <param name="index"></param>
        /// <param name="closeReader"></param>
        /// <returns></returns>
        public static List<P> ToList<P>(this DbDataReader dataReader, int index, bool closeReader = true)
        {
            List<P> objList = new List<P>();

            while (dataReader.Read())
            {
                if (!dataReader.IsDBNull(index))
                {
                    objList.Add((P)dataReader[index]);
                }
            }
            if (closeReader)
            {
                dataReader.Close();
            }
            return objList;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objDataTable">表</param>
        /// <returns></returns>
        public static List<Dictionary<string, object>> ToList(this DataTable objDataTable)
        {
            List<Dictionary<string, object>> objList = new List<Dictionary<string, object>>();
            if (objDataTable.Rows.Count == 0)
            {
                return objList;
            }

            foreach (DataRow objDataRow in objDataTable.Rows)
            {
                Dictionary<string, object> objData = new Dictionary<string, object>();
                foreach (DataColumn objDataColumn in objDataTable.Columns)
                {
                    objData[objDataColumn.ColumnName] = objDataRow[objDataColumn.ColumnName];
                }
                objList.Add(objData);
            }
            return objList;

        }



        /// <summary>
        /// DataSet第0个DataTable转换成实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objDataSet"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataSet objDataSet, int index = 0) where T : class, new()
        {
            return objDataSet.Tables[index].ToList<T>();
        }


        /// <summary>
        /// DataTable某个字段转为字符串
        /// </summary>
        /// <param name="objDataTable">表</param>
        /// <param name="fieldName">字段名称</param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string ToConcat(this DataTable objDataTable, string fieldName, string split = ",")
        {

            return objDataTable.Select().Select(cols => cols[fieldName]).ToConcat(split);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objDataTable"></param>
        /// <param name="fieldIndex"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string ToConcat(this DataTable objDataTable, int fieldIndex, string split = ",")
        {
            return objDataTable.Select().Select(cols => cols[fieldIndex]).ToConcat(split);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objDataTable"></param>
        /// <param name="fieldName"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string ToText(this DataTable objDataTable, string fieldName, string split = ",")
        {
            return objDataTable.ToConcat(fieldName, split);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objDataTable"></param>
        /// <param name="fieldIndex"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string ToText(this DataTable objDataTable, int fieldIndex, string split = ",")
        {
            return objDataTable.ToConcat(fieldIndex, split);
        }
        /// <summary>
        /// 某个字段值转列表
        /// </summary>
        /// <typeparam name="P">类型</typeparam>
        /// <param name="objDataTable">表</param>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        public static List<P> ToList<P>(this DataTable objDataTable, string fieldName)
        {
            List<P> objValueList = new List<P>();
            foreach (DataRow objDataRow in objDataTable.Rows)
            {
                if (!objDataRow.IsNull(fieldName))
                {
                    objValueList.Add(objDataRow[fieldName].To<P>());
                }
            }

            return objValueList;
        }
        /// <summary>
        /// 某个字段值转列表
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="objDataTable"></param>
        /// <param name="fieldIndex"></param>
        /// <returns></returns>
        public static List<P> ToList<P>(this DataTable objDataTable, int fieldIndex)
        {

            List<P> objValueList = new List<P>();
            foreach (DataRow objDataRow in objDataTable.Rows)
            {
                if (!objDataRow.IsNull(fieldIndex))
                {
                    objValueList.Add(objDataRow[fieldIndex].To<P>());
                }
            }

            return objValueList;
        }
        /// <summary>
        /// 某个字段值转数组
        /// </summary>
        /// <typeparam name="P">类型</typeparam>
        /// <param name="objDataTable">表</param>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        public static P[] ToArray<P>(this DataTable objDataTable, string fieldName)
        {
            return objDataTable.ToList<P>(fieldName).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="objDataTable"></param>
        /// <param name="fieldIndex"></param>
        /// <returns></returns>
        public static P[] ToArray<P>(this DataTable objDataTable, int fieldIndex)
        {
            return objDataTable.ToList<P>(fieldIndex).ToArray();
        }
        /// <summary>
        /// 某个字段值转列表
        /// </summary>
        /// <typeparam name="P">类型</typeparam>
        /// <param name="objDataSet"></param>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        public static List<P> ToList<P>(this DataSet objDataSet, string fieldName)
        {
            return objDataSet.Tables[0].ToList<P>(fieldName);
        }
        /// <summary>
        /// 某个字段值转数组
        /// </summary>
        /// <typeparam name="P">类型</typeparam>
        /// <param name="objDataSet"></param>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        public static P[] ToArray<P>(this DataSet objDataSet, string fieldName)
        {

            return objDataSet.Tables[0].ToArray<P>(fieldName);
        }





    }
}
