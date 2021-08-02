using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Data
{
    public abstract partial class IDbContext<T, Key> where T : class, new()
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        internal virtual ReadData ToReadData(ReadOptions readOptions)
        {

            ReadData objReadData = ReadData.Create();
            if (readOptions != null)
            {

                objReadData.CommandText = readOptions.CommandText;
                objReadData.Condition = readOptions.MergeCondition();
                objReadData.Fields = readOptions.Fields;
                objReadData.PageIndex = readOptions.PageIndex;
                objReadData.PageSize = readOptions.PageSize;
                objReadData.Parameters = readOptions.MergeParameters();
                objReadData.Skip = readOptions.Skip;
                objReadData.Take = readOptions.Take;
                objReadData.SortExpression = readOptions.SortExpression;
                objReadData.TableName = readOptions.TableName;
            }
            else
            {
                readOptions = new ReadOptions();
            }


            if (objReadData.PageSize != null || objReadData.PageIndex != null || objReadData.Skip != null || objReadData.Take != null)
            {
                if (Context.ContextType == ContextType.Sql)
                {
                    if (string.IsNullOrWhiteSpace(objReadData.SortExpression))
                    {
                        objReadData.SortExpression = FieldName(PrimaryKey.PrimaryName);

                    }
                }
            }
            if (string.IsNullOrWhiteSpace(objReadData.CommandText) && string.IsNullOrWhiteSpace(objReadData.TableName))
            {
                if (!readOptions.IsView)
                {
                    objReadData.TableName = DBTableName;
                }
                else
                {
                    objReadData.TableName = DBViewName;
                }
            }
            return objReadData;

        }





        /// <summary>
        /// 新增字段语句
        /// </summary>
        /// <returns></returns>
        internal string SqlInsertFields(List<PropertyInfo> objPropertyInfoList)
        {
            StringBuilder objQuery = new StringBuilder();
            foreach (PropertyInfo objPropertyInfo in objPropertyInfoList)
            {
                objQuery.Append(FieldName(objPropertyInfo.Name) + ",");
            }
            return objQuery.ToString().TrimEnd(new char[] { ',' });

        }
        /// <summary>
        /// 新增参数
        /// </summary>
        /// <param name="objPropertyInfoList"></param>
        /// <returns></returns>
        internal string SqlInsertValues(List<PropertyInfo> objPropertyInfoList)
        {
            StringBuilder objQuery = new StringBuilder();
            foreach (PropertyInfo objPropertyInfo in objPropertyInfoList)
            {
                objQuery.Append(FieldParamName(objPropertyInfo.Name) + ",");
            }
            return objQuery.ToString().TrimEnd(new char[] { ',' });
        }
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="objPropertyInfoList"></param>
        /// <param name="batchCount"></param>
        /// <returns></returns>
        internal string SqlInsertValues(List<PropertyInfo> objPropertyInfoList, int batchCount)
        {
            StringBuilder objQuery = new StringBuilder();
            for (int i = 0; i < batchCount; i++)
            {
                objQuery.Append("( ");
                string RowValue = "";
                foreach (PropertyInfo objPropertyInfo in objPropertyInfoList)
                {
                    RowValue += FieldParamName(objPropertyInfo.Name + "_" + i) + ",";
                }

                objQuery.Append(RowValue.TrimEnd(new char[] { ',' }));
                objQuery.Append("),");
            }
            return objQuery.ToString().TrimEnd(new char[] { ',' });
        }


        /// <summary>
        /// 新增参数
        /// </summary>
        /// <param name="objPropertyInfoList"></param>
        /// <param name="value">值</param>
        /// <returns></returns>
        internal List<DataParameter> SqlInsertParams(List<PropertyInfo> objPropertyInfoList, T value)
        {

            List<DataParameter> objDataParameterList = new List<DataParameter>();
            foreach (PropertyInfo objPropertyInfo in objPropertyInfoList)
            {
                objDataParameterList.Add(new DataParameter(objPropertyInfo.Name, objPropertyInfo.GetValue(value, null)));
            }
            return objDataParameterList;
        }
        /// <summary>
        /// 批量新增参数
        /// </summary>
        /// <param name="objPropertyInfoList"></param>
        /// <param name="valueList">值</param>
        /// <returns></returns>
        internal virtual List<DataParameter> SqlInsertParams(List<PropertyInfo> objPropertyInfoList, IEnumerable<T> valueList)
        {
            List<DataParameter> objDataParameterList = new List<DataParameter>();
            int i = 0;
            foreach (T value in valueList)
            {
                foreach (PropertyInfo objPropertyInfo in objPropertyInfoList)
                {
                    objDataParameterList.Add(new DataParameter(objPropertyInfo.Name + "_" + i, objPropertyInfo.GetValue(value, null)));
                }
                i++;
            }
            return objDataParameterList;
        }

        /// <summary>
        /// 更新字段语句
        /// </summary>
        /// <param name="objPropertyInfoList"></param>
        /// <returns></returns>
        internal string SqlUpdateFields(List<PropertyInfo> objPropertyInfoList)
        {

            StringBuilder objQuery = new StringBuilder();
            foreach (PropertyInfo objPropertyInfo in objPropertyInfoList)
            {
                objQuery.AppendFormat("{0}={1},", FieldName(objPropertyInfo.Name), FieldParamName(objPropertyInfo.Name));
            }
            return objQuery.ToString().TrimEnd(new char[] { ',' });

        }
        /// <summary>
        /// 更新参数
        /// </summary>
        /// <param name="objPropertyInfoList"></param>
        /// <param name="value">值</param>
        /// <param name="isAddPrimaryKey"></param>
        /// <returns></returns>
        internal List<DataParameter> SqlUpdateParameter(List<PropertyInfo> objPropertyInfoList, T value, bool isAddPrimaryKey)
        {


            List<DataParameter> objDataParameterList = new List<DataParameter>();
            foreach (PropertyInfo objPropertyInfo in objPropertyInfoList)
            {
                objDataParameterList.Add(new DataParameter(objPropertyInfo.Name, objPropertyInfo.GetValue(value, null)));
            }
            if (isAddPrimaryKey)
            {
                objDataParameterList.Add(new DataParameter(PrimaryKey.PrimaryName, PrimaryKey.PrimaryProperty.GetValue(value, null)));
            }
            return objDataParameterList;
        }






    }
}
