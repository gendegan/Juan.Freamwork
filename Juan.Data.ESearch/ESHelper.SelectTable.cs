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
using System.Data;
using System.Reflection;
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
        public static DataTable QueryTable(string providerName, string sql, SearchOptionInfo option = null)
        {
            QueryResult<Dictionary<string, object>> objQueryResult = QueryResult<Dictionary<string, object>>(providerName, sql, option);
            return objQueryResult.hits.hits.Select(s => s._source).ToDataTable();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="fields">返回字段</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static DataTable QueryTable(string providerName, string index, string type, string condition, string sortExpression, string fields = "*", SearchOptionInfo option = null)
        {
            option.ParseSearchOptionInfo(index, type);
            if (string.IsNullOrWhiteSpace(fields))
            {
                fields = "*";
            }
            StringBuilder objSql = new StringBuilder();

            if (string.IsNullOrWhiteSpace(type))
            {

                objSql.AppendFormat("select {0} from {1} ", fields, index);
            }
            else
            {
                objSql.AppendFormat("select {0} from {1}/{2} ", fields, index, type);

            }
            if (!string.IsNullOrWhiteSpace(condition))
            {
                objSql.AppendFormat(" where {0}", condition);
            }
            if (!string.IsNullOrWhiteSpace(sortExpression))
            {
                objSql.AppendFormat(" order by  {0}", sortExpression);
            }
            objSql.Append(" limit " + ESConfigHelper.GetProducerElement(providerName).MaxResult);
            return QueryTable(providerName, objSql.ToString(), option);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="fields">返回字段</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static PageInfo<DataTable> QueryPageTable(string providerName, string index, string type, string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*", SearchOptionInfo option = null)
        {
            option.ParseSearchOptionInfo(index, type);
            if (string.IsNullOrWhiteSpace(fields))
            {
                fields = "*";
            }
            StringBuilder objSql = new StringBuilder();

            if (string.IsNullOrWhiteSpace(type))
            {

                objSql.AppendFormat("select {0} from {1} ", fields, index);
            }
            else
            {
                objSql.AppendFormat("select {0} from {1}/{2} ", fields, index, type);

            }
            if (!string.IsNullOrWhiteSpace(condition))
            {
                objSql.AppendFormat(" where {0}", condition);
            }
            if (!string.IsNullOrWhiteSpace(sortExpression))
            {
                objSql.AppendFormat(" order by {0}", sortExpression);
            }
            objSql.AppendFormat(" limit {0},{1}", pageSize * pageIndex, pageSize);
            PageInfo<DataTable> objPageInfo = QueryPageTable(providerName, objSql.ToString(), option);
            objPageInfo.PageSize = pageSize;
            objPageInfo.PageIndex = pageIndex;
            return objPageInfo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="sql">完整查询语句</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static PageInfo<DataTable> QueryPageTable(string providerName, string sql, int pageSize, int pageIndex, SearchOptionInfo option = null)
        {
            sql = sql + string.Format(" limit {0},{1}", pageSize * pageIndex, pageSize);
            PageInfo<DataTable> objPageInfo = QueryPageTable(providerName, sql, option);
            objPageInfo.PageSize = pageSize;
            objPageInfo.PageIndex = pageIndex;
            return objPageInfo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="sql">完整查询语句</param>
        /// <param name="option">参数</param>
        /// <returns></returns>

        private static PageInfo<DataTable> QueryPageTable(string providerName, string sql, SearchOptionInfo option = null)
        {
            PageInfo<DataTable> objPageInfo = new PageInfo<DataTable>();
            QueryResult<Dictionary<string, object>> objQueryResult = QueryResult<Dictionary<string, object>>(providerName, sql, option);
            objPageInfo.Data = objQueryResult.hits.hits.Select(s => s._source).ToDataTable();
            objPageInfo.RecordCount = objQueryResult.hits.total.value;
            return objPageInfo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="fields">返回字段</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static DataTable QueryTable(string providerName, string index, string type, string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*", SearchOptionInfo option = null)
        {

            PageInfo<DataTable> objPageInfo = QueryPageTable(providerName, index, type, condition, sortExpression, pageSize, pageIndex, fields, option);
            return objPageInfo.Data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="sql">完整查询语句</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="option">参数</param>
        /// <returns></returns>

        public static DataTable QueryTable(string providerName, string sql, int pageSize, int pageIndex, SearchOptionInfo option = null)
        {
            PageInfo<DataTable> objPageInfo = QueryPageTable(providerName, sql, pageSize, pageIndex, option);
            return objPageInfo.Data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="offset">偏移量</param>
        /// <param name="limit">读取条数</param>
        /// <param name="fields">返回字段</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static DataTable QueryLimitTable(string providerName, string index, string type, string condition, string sortExpression, int offset, int limit, string fields = "*", SearchOptionInfo option = null)
        {
            option.ParseSearchOptionInfo(index, type);
            StringBuilder objSql = new StringBuilder();
            if (string.IsNullOrWhiteSpace(fields))
            {
                fields = "*";
            }

            if (string.IsNullOrWhiteSpace(type))
            {

                objSql.AppendFormat("select {0} from {1} ", fields, index);
            }
            else
            {
                objSql.AppendFormat("select {0} from {1}/{2} ", fields, index, type);

            }
            if (!string.IsNullOrWhiteSpace(condition))
            {
                objSql.AppendFormat(" where {0}", condition);
            }
            if (!string.IsNullOrWhiteSpace(sortExpression))
            {
                objSql.AppendFormat(" order by {0}", sortExpression);
            }
            objSql.AppendFormat(" limit {0},{1}", offset, limit);
            return QueryTable(providerName, objSql.ToString(), option);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="sql">完整查询语句</param>
        /// <param name="offset">偏移量</param>
        /// <param name="limit">读取条数</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static DataTable QueryLimitTable(string providerName, string sql, int offset, int limit, SearchOptionInfo option = null)
        {

            sql = sql + string.Format(" limit {0},{1}", offset, limit);
            return QueryTable(providerName, sql, option);

        }



    }
}
