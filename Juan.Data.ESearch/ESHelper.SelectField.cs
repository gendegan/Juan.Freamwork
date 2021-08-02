using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Juan.Core;
using Newtonsoft.Json.Linq;

using PlainElastic7.Net;
using PlainElastic7.Net.IndexSettings;
using PlainElastic7.Net.Serialization;
using System.Reflection;
using System.Data;
namespace Juan.Data.ESearch
{
    public static partial class ESHelper
    { 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="sql">完整查询语句</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static IList<string> QueryID(string providerName, string sql, SearchOptionInfo option = null)
        {
            return QueryIDInfo(providerName, sql, option).Select(s => s._id).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="sql">完整查询语句</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static List<HitsInfo<object, object>> QueryIDInfo(string providerName, string sql, SearchOptionInfo option = null)
        {
            QueryResult<object> objQueryResult = QueryResult<object>(providerName, sql, option);
            return objQueryResult.hits.hits;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static List<HitsInfo<object, object>> QueryIDInfo(string providerName, string index, string type, string condition, string sortExpression = "", SearchOptionInfo option = null)
        {
            option.ParseSearchOptionInfo(index, type);
            StringBuilder objSql = new StringBuilder();


            if (string.IsNullOrWhiteSpace(type))
            {

                objSql.AppendFormat("select _id from {0} ", index);
            }
            else
            {
                objSql.AppendFormat("select _id from {0}/{1} ", index, type);

            }
            if (!string.IsNullOrWhiteSpace(condition))
            {
                objSql.AppendFormat(" where {0}", condition);
            } if (!string.IsNullOrWhiteSpace(sortExpression))
            {
                objSql.AppendFormat(" order by  {0}", sortExpression);
            }
            objSql.Append(" limit " + ESConfigHelper.GetProducerElement(providerName).MaxResult);
            return QueryIDInfo(providerName, objSql.ToString(), option);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static IList<string> QueryID(string providerName, string index, string type, string condition, string sortExpression = "", SearchOptionInfo option = null)
        {
            return QueryIDInfo(providerName, index, type, condition, sortExpression, option).Select(s => s._id).ToList();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static List<HitsInfo<object, object>> QueryIDInfo(string providerName, string index, string type, string condition, int pageSize, int pageIndex, string sortExpression = "", SearchOptionInfo option = null)
        {
            option.ParseSearchOptionInfo(index, type);
            StringBuilder objSql = new StringBuilder();
            if (string.IsNullOrWhiteSpace(type))
            {

                objSql.AppendFormat("select _id from {0} ", index);
            }
            else
            {
                objSql.AppendFormat("select _id from {0}/{1} ", index, type);

            }
            if (!string.IsNullOrWhiteSpace(condition))
            {
                objSql.AppendFormat(" where {0}", condition);
            } if (!string.IsNullOrWhiteSpace(sortExpression))
            {
                objSql.AppendFormat(" order by  {0}", sortExpression);
            }
            objSql.AppendFormat(" limit {0},{1}", pageSize * pageIndex, pageSize);
            return QueryIDInfo(providerName, objSql.ToString(), option);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static IList<string> QueryID(string providerName, string index, string type, string condition, int pageSize, int pageIndex, string sortExpression = "", SearchOptionInfo option = null)
        {
            return QueryIDInfo(providerName, index, type, condition, pageSize, pageIndex, sortExpression, option).Select(s => s._id).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="FieldKey"></typeparam>
        /// <param name="providerName">提供者名称</param>
        /// <param name="fieldName"></param>
        /// <param name="sql">完整查询语句</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static IList<FieldKey> QueryField<FieldKey>(string providerName, string fieldName, string sql, SearchOptionInfo option = null)
        {
            DataTable objDataTable = QueryTable(providerName, sql, option);
            if (objDataTable.Rows.Count > 0)
            {
                return objDataTable.ToList<FieldKey>(fieldName);
            }
            else
            {
                return new List<FieldKey>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="FieldKey"></typeparam>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="fieldName"></param>
        /// <param name="condition">查询条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static IList<FieldKey> QueryField<FieldKey>(string providerName, string index, string type, string fieldName, string condition, string sortExpression = "", SearchOptionInfo option = null)
        {
            option.ParseSearchOptionInfo(index, type);
            StringBuilder objSql = new StringBuilder();


            if (string.IsNullOrWhiteSpace(type))
            {

                objSql.AppendFormat("select {0} from {1} ", fieldName, index);
            }
            else
            {
                objSql.AppendFormat("select {0} from {1}/{2} ", fieldName, index, type);

            }
            if (!string.IsNullOrWhiteSpace(condition))
            {
                objSql.AppendFormat(" where {0}", condition);
            } if (!string.IsNullOrWhiteSpace(sortExpression))
            {
                objSql.AppendFormat(" order by  {0}", sortExpression);
            }
            objSql.Append(" limit " + ESConfigHelper.GetProducerElement(providerName).MaxResult);
            return QueryField<FieldKey>(providerName, fieldName, objSql.ToString(), option);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="FieldKey"></typeparam>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="fieldName"></param>
        /// <param name="condition">查询条件</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static IList<FieldKey> QueryField<FieldKey>(string providerName, string index, string type, string fieldName, string condition, int pageSize, int pageIndex, string sortExpression = "", SearchOptionInfo option = null)
        {
            option.ParseSearchOptionInfo(index, type);
            StringBuilder objSql = new StringBuilder();
            if (string.IsNullOrWhiteSpace(type))
            {

                objSql.AppendFormat("select {0} from {1} ", fieldName, index);
            }
            else
            {
                objSql.AppendFormat("select {0} from {1}/{2} ", fieldName, index, type);

            }
            if (!string.IsNullOrWhiteSpace(condition))
            {
                objSql.AppendFormat(" where {0}", condition);
            }
            if (!string.IsNullOrWhiteSpace(sortExpression))
            {
                objSql.AppendFormat(" order by  {0}", sortExpression);
            }
            objSql.AppendFormat(" limit {0},{1}", pageSize * pageIndex, pageSize);
            return QueryField<FieldKey>(providerName, fieldName, objSql.ToString(), option);
        }



    }
}
