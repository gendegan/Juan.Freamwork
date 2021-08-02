using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Reflection;
namespace Juan.Data
{
    public abstract partial class IDbContext<T, Key> where T : class, new()
    {




        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public virtual int Update(T value, params string[] fields)
        {
            List<PropertyInfo> objUpdatePropertyInfo = GetUpdatePropertys(fields);
            string sqlQuery = string.Format("UPDATE {0} SET  {1} WHERE {2}={3}", DBTableName, SqlUpdateFields(objUpdatePropertyInfo), FieldName(PrimaryKey.PrimaryName), FieldParamName(PrimaryKey.PrimaryName));
            return Context.ExecuteNonQuery(sqlQuery, SqlUpdateParameter(objUpdatePropertyInfo, value, true).ToArray());
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="objbatchValue"></param>
        /// <returns></returns>
        public virtual int Update(BatchValueInfo<T> objbatchValue)
        {
            if (objbatchValue.Data.Count == 0)
            {
                return 0;
            }

            if (objbatchValue.UpdateFields.Length == 0)
            {
                throw new ArgumentNullException("UpdateFields", "不能为空");
            }
            return Update(objbatchValue.Data, objbatchValue.UpdateFields);
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="valueList">值</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public virtual int Update(IEnumerable<T> valueList, params string[] fields)
        {

            if (valueList.Count() == 0)
            {
                return 0;
            }
            StringBuilder sqlBuilder = new StringBuilder();
            int i = 0;

            List<PropertyInfo> objPropertyInfoList = GetUpdatePropertys(fields);
            List<DataParameter> objDataParameterList = new List<DataParameter>();
            foreach (T value in valueList)
            {
                sqlBuilder.Append("UPDATE " + DBTableName + " SET ");
                string updateFields = "";
                foreach (PropertyInfo objPropertyInfo in objPropertyInfoList)
                {
                    updateFields += string.Format("{0}={1},", FieldName(objPropertyInfo.Name), FieldParamName(objPropertyInfo.Name + "_" + i));
                    objDataParameterList.Add(new DataParameter(objPropertyInfo.Name + "_" + i, objPropertyInfo.GetValue(value, null)));
                }
                sqlBuilder.Append(updateFields.TrimEndComma());
                sqlBuilder.AppendFormat(" WHERE {0}={1};", FieldName(PrimaryKey.PrimaryName), FieldParamName(PrimaryKey.PrimaryName + "_" + i));
                objDataParameterList.Add(new DataParameter(PrimaryKey.PrimaryName + "_" + i, PrimaryKey.PrimaryProperty.GetValue(value, null)));
                i++;
            }
            return Context.ExecuteNonQuery(sqlBuilder.ToString(), objDataParameterList.ToArray());
        }
        /// <summary>
        /// 更新批量
        /// </summary>
        /// <param name="objValueList"></param>
        /// <returns></returns>
        public virtual int Update(List<Dictionary<string, object>> objValueList)
        {

            if (objValueList.Count() == 0)
            {
                return 0;
            }

            StringBuilder sqlBuilder = new StringBuilder();
            int i = 0;
            List<PropertyInfo> objPropertyInfoList = GetUpdatePropertys();
            List<DataParameter> objDataParameterList = new List<DataParameter>();
            foreach (Dictionary<string, object> valueDict in objValueList)
            {

                if (valueDict.Keys.Count == 0)
                {
                    continue;
                }
                if (!valueDict.Keys.Contains(PrimaryKey.PrimaryName))
                {
                    throw new ArgumentNullException("未包含主键数据，因此无法更新");
                }
                if (valueDict.Keys.Count == 1)
                {
                    continue;
                }
                sqlBuilder.Append("UPDATE " + DBTableName + " SET ");
                string updateFields = "";
                foreach (var fieldKeyValue in valueDict)
                {
                    if (fieldKeyValue.Key.Equals(PrimaryKey.PrimaryName, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                    updateFields += string.Format("{0}={1},", FieldName(fieldKeyValue.Key), FieldParamName(fieldKeyValue.Key + "_" + i));
                    objDataParameterList.Add(new DataParameter(fieldKeyValue.Key + "_" + i, fieldKeyValue.Value));
                }
                sqlBuilder.Append(updateFields.TrimEndComma());
                sqlBuilder.AppendFormat(" WHERE {0}={1};", FieldName(PrimaryKey.PrimaryName), FieldParamName(PrimaryKey.PrimaryName + "_" + i));
                objDataParameterList.Add(new DataParameter(PrimaryKey.PrimaryName + "_" + i, valueDict[PrimaryKey.PrimaryName]));
                i++;
            }
            string query = sqlBuilder.ToString();
            if (query.IsNoNullOrWhiteSpace())
            {

                return Context.ExecuteNonQuery(query, objDataParameterList.ToArray());
            }
            else
            {
                return 0;
            }
        }




        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fields">需要排除的字段</param>
        /// <returns></returns>
        public virtual int UpdateExclude(T value, params string[] fields)
        {
            List<PropertyInfo> objUpdatePropertyInfo = GetUpdateExcludePropertys(fields);
            string sqlQuery = string.Format("UPDATE {0} SET  {1} WHERE {2}={3}", DBTableName, SqlUpdateFields(objUpdatePropertyInfo), FieldName(PrimaryKey.PrimaryName), FieldParamName(PrimaryKey.PrimaryName));
            return Context.ExecuteNonQuery(sqlQuery, SqlUpdateParameter(objUpdatePropertyInfo, value, true).ToArray());
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="valueList">值</param>
        /// <param name="fields">需要排除的字段</param>
        /// <returns></returns>
        public virtual int UpdateExclude(IEnumerable<T> valueList, params string[] fields)
        {
            if (valueList.Count() == 0)
            {
                return 0;
            }
            StringBuilder sqlBuilder = new StringBuilder();
            int i = 0;

            List<PropertyInfo> objPropertyInfoList = GetUpdateExcludePropertys(fields);
            List<DataParameter> objDataParameterList = new List<DataParameter>();
            foreach (T value in valueList)
            {
                sqlBuilder.Append("UPDATE " + DBTableName + " SET ");
                string updateFields = "";
                foreach (PropertyInfo objPropertyInfo in objPropertyInfoList)
                {
                    updateFields += string.Format("{0}={1},", FieldName(objPropertyInfo.Name), FieldParamName(objPropertyInfo.Name + "_" + i));
                    objDataParameterList.Add(new DataParameter(objPropertyInfo.Name + "_" + i, objPropertyInfo.GetValue(value, null)));
                }
                sqlBuilder.Append(updateFields.TrimEndComma());
                sqlBuilder.AppendFormat(" WHERE {0}={1};", FieldName(PrimaryKey.PrimaryName), FieldParamName(PrimaryKey.PrimaryName + "_" + i));
                objDataParameterList.Add(new DataParameter(PrimaryKey.PrimaryName + "_" + i, PrimaryKey.PrimaryProperty.GetValue(value, null)));
                i++;
            }
            return Context.ExecuteNonQuery(sqlBuilder.ToString(), objDataParameterList.ToArray());
        }





    }
}
