using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Juan.Data
{
    public partial class MySqlContext<T, Key> : DbContext<T, Key> where T : class,new()
    {
        /// <summary>
        /// 替换新增
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public override int ReplaceAdd(T value)
        {
            List<PropertyInfo> objPropertyInfoList = GetInsertPropertys(true);
            string sqlQuery = string.Format("REPLACE  INTO {0} ({1}) VALUES ({2});", DBTableName, SqlInsertFields(objPropertyInfoList), SqlInsertValues(objPropertyInfoList));
            List<DataParameter> objDataParameterList = SqlInsertParams(objPropertyInfoList, value);
            return Context.ExecuteNonQuery(sqlQuery, objDataParameterList.ToArray());
        }
        /// <summary>
        /// 替换新增
        /// </summary>
        /// <param name="valueList">值</param>
        /// <returns></returns>
        public override int ReplaceAdd(IEnumerable<T> valueList)
        {
            if (valueList.Count() == 0)
            {
                return 0;
            }
            List<PropertyInfo> objPropertyInfoList = GetInsertPropertys(true);
            string sqlQuery = string.Format("REPLACE  INTO {0} ({1}) VALUES {2};", DBTableName, SqlInsertFields(objPropertyInfoList), SqlInsertValues(objPropertyInfoList, valueList.Count()));
            List<DataParameter> objDataParameterList = SqlInsertParams(objPropertyInfoList, valueList);
            return Context.ExecuteNonQuery(sqlQuery, objDataParameterList.ToArray());
        }
        /// <summary>
        /// 导数据
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="isIgnore"></param>
        /// <returns></returns>
        public override int Import(T value, bool isIgnore = true)
        {
            List<PropertyInfo> objPropertyInfoList = GetInsertPropertys(true);
            string sqlQuery = string.Format("INSERT " + (isIgnore ? "IGNORE" : "") + " INTO {0} ({1}) VALUES ({2});", DBTableName, SqlInsertFields(objPropertyInfoList), SqlInsertValues(objPropertyInfoList));
            List<DataParameter> objDataParameterList = SqlInsertParams(objPropertyInfoList, value);
            return Context.ExecuteNonQuery(sqlQuery, objDataParameterList.ToArray());
        }
        /// <summary>
        /// 导数据
        /// </summary>
        /// <param name="valueList">值</param>
        /// <param name="isIgnore"></param>
        /// <returns></returns>
        public override int Import(IEnumerable<T> valueList, bool isIgnore = true)
        {
            if (valueList.Count() == 0)
            {
                return 0;
            }
            List<PropertyInfo> objPropertyInfoList = GetInsertPropertys(true);
            string sqlQuery = string.Format("INSERT " + (isIgnore ? "IGNORE" : "") + " INTO {0} ({1}) VALUES {2};", DBTableName, SqlInsertFields(objPropertyInfoList), SqlInsertValues(objPropertyInfoList, valueList.Count()));
            List<DataParameter> objDataParameterList = SqlInsertParams(objPropertyInfoList, valueList);
            return Context.ExecuteNonQuery(sqlQuery, objDataParameterList.ToArray());
        }
        /// <summary>
        /// 导数据并更新
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public override int ImportUpdate(T value, params string[] fields)
        {

            string DuplicateSql = ProcessDuplicateField(fields);
            return ImportUpdateValue(value, DuplicateSql);
        }
        /// <summary>
        /// 导数据并更新
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="updateSql">更新语句</param>
        /// <param name="parms">更新参数</param>
        /// <returns></returns>
        public override int ImportUpdateValue(T value, string updateSql, params DataParameter[] parms)
        {
            List<PropertyInfo> objPropertyInfoList = GetInsertPropertys(true);
            string sqlQuery = string.Format("INSERT  INTO {0} ({1}) VALUES ({2})", DBTableName, SqlInsertFields(objPropertyInfoList), SqlInsertValues(objPropertyInfoList));
            updateSql = ParamCharReplace(updateSql);
            if (!string.IsNullOrWhiteSpace(updateSql))
            {
                sqlQuery += " on duplicate key update " + updateSql;
            }
            sqlQuery += ";";
            List<DataParameter> objDataParameterList = SqlInsertParams(objPropertyInfoList, value);
            return Context.ExecuteNonQuery(sqlQuery, objDataParameterList.MergeParameter(parms));
        }
        /// <summary>
        /// 导数据并更新
        /// </summary>
        /// <param name="valueList">值</param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public override int ImportUpdate(IEnumerable<T> valueList, params string[] fields)
        {
            string DuplicateSql = ProcessDuplicateField(fields);
            return ImportUpdateValue(valueList, DuplicateSql);
        }
        /// <summary>
        /// 导数据并更新
        /// </summary>
        /// <param name="valueList">值</param>
        /// <param name="updateSql">更新语句</param>
        /// <param name="parms">更新参数</param>
        /// <returns></returns>
        public override int ImportUpdateValue(IEnumerable<T> valueList, string updateSql, params DataParameter[] parms)
        {
            if (valueList.Count() == 0)
            {
                return 0;
            }
            List<PropertyInfo> objPropertyInfoList = GetInsertPropertys(true);
            string sqlQuery = string.Format("INSERT  INTO {0} ({1}) VALUES {2}", DBTableName, SqlInsertFields(objPropertyInfoList), SqlInsertValues(objPropertyInfoList, valueList.Count()));
            updateSql = ParamCharReplace(updateSql);
            if (!string.IsNullOrWhiteSpace(updateSql))
            {
                sqlQuery += " on duplicate key update " + updateSql;
            }
            sqlQuery += ";";
            List<DataParameter> objDataParameterList = SqlInsertParams(objPropertyInfoList, valueList);
            return Context.ExecuteNonQuery(sqlQuery, objDataParameterList.MergeParameter(parms));
        }


    }
}
