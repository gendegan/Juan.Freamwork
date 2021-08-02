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
        /// <typeparam name="T"></typeparam>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="id">主键</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static T QueryRecord<T>(string providerName, string index, string type, string id, SearchOptionInfo option = null) where T : class, new()
        {
            string searchResult = "";
            try
            {
                var connection = CreateConnection(providerName);
                GetCommand getCommand = Commands.Get(index, type, id: id).Pretty();
                if (option != null && !string.IsNullOrWhiteSpace(option.Routing))
                {
                    getCommand.Routing(option.Routing);
                }
                searchResult = connection.Get(getCommand);
                var getResult = Serializer.ToGetResult<T>(searchResult);
                return getResult.Document;
            }
            catch (OperationException ex)
            {

                if (ex.HttpStatusCode == 404)
                {
                    var getResult = Serializer.ToGetResult<T>(ex.Message);
                    return getResult.Document;
                }
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="sql">完整查询语句</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static long QueryCount(string providerName, string sql, SearchOptionInfo option = null)
        {
            string queryResult = QueryResult(providerName, sql, option);
            JObject objResultJObject = JObject.Parse(queryResult);
            JObject aggregationsJObject = objResultJObject.GetValue("aggregations") as JObject;
            JObject CountJObject = aggregationsJObject.GetValue("Count") as JObject;
            return CountJObject.GetValue("value").ToObject<long>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static long QueryCount(string providerName, string index, string type, string condition, SearchOptionInfo option = null)
        {

            option.ParseSearchOptionInfo(index, type);
            StringBuilder objSql = new StringBuilder();
            if (string.IsNullOrWhiteSpace(type))
            {
                objSql.AppendFormat("select count(*) as Count from {0} ", index);
            }
            else
            {
                objSql.AppendFormat("select count(*) as Count from {0}/{1} ", index, type);
            }
            if (!string.IsNullOrWhiteSpace(condition))
            {
                objSql.AppendFormat(" where {0}", condition);
            }
            return ESHelper.QueryCount(providerName, objSql.ToString(), option);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="isDistinct"></param>
        /// <param name="fieldName"></param>
        /// <param name="condition">查询条件</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static long QueryCount(string providerName, string index, string type, bool isDistinct, string fieldName, string condition, SearchOptionInfo option = null)
        {
            option.ParseSearchOptionInfo(index, type);
            StringBuilder objSql = new StringBuilder();
            if (!isDistinct)
            {

                if (string.IsNullOrWhiteSpace(type))
                {

                    objSql.AppendFormat("select count({1}) as Count from {0} ", index, fieldName);
                }
                else
                {
                    objSql.AppendFormat("select count({2}) as Count from {0}/{1} ", index, type, fieldName);

                }

            }
            else
            {


                if (string.IsNullOrWhiteSpace(type))
                {

                    objSql.AppendFormat("select count(distinct({1})) Count from {0} ", index, fieldName);
                }
                else
                {
                    objSql.AppendFormat("select count(distinct({2})) Count from {0}/{1} ", index, type, fieldName);

                }
            }
            if (!string.IsNullOrWhiteSpace(condition))
            {
                objSql.AppendFormat(" where {0}", condition);
            }
            return ESHelper.QueryCount(providerName, objSql.ToString(), option);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="providerName">提供者名称</param>
        /// <param name="sql">完整查询语句</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static IList<T> Query<T>(string providerName, string sql, SearchOptionInfo option = null)
        {

            QueryResult<T> objQueryResult = QueryResult<T>(providerName, sql, option);
            return objQueryResult.hits.hits.Select(s => s._source).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="fields">返回字段</param>
        /// <param name="option">参数</param>
        /// <returns></returns>

        public static IList<T> Query<T>(string providerName, string index, string type, string condition, string sortExpression, string fields = "*", SearchOptionInfo option = null)
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
                objSql.AppendFormat(" order by  {0}", sortExpression);
            }

            objSql.Append(" limit " + ESConfigHelper.GetProducerElement(providerName).MaxResult);
            return Query<T>(providerName, objSql.ToString(), option);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
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
        public static PageInfo<List<T>> QueryPage<T>(string providerName, string index, string type, string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*", SearchOptionInfo option = null)
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
            objSql.AppendFormat(" limit {0},{1}", pageSize * pageIndex, pageSize);
            PageInfo<List<T>> objPageInfo = ESHelper.QueryPage<T>(providerName, objSql.ToString(), option);
            objPageInfo.PageSize = pageSize;
            objPageInfo.PageIndex = pageIndex;
            return objPageInfo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="providerName">提供者名称</param>
        /// <param name="sql">完整查询语句</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static PageInfo<List<T>> QueryPage<T>(string providerName, string sql, int pageSize, int pageIndex, SearchOptionInfo option = null)
        {
            sql = sql + string.Format(" limit {0},{1}", pageSize * pageIndex, pageSize);
            PageInfo<List<T>> objPageInfo = ESHelper.QueryPage<T>(providerName, sql, option);
            objPageInfo.PageSize = pageSize;
            objPageInfo.PageIndex = pageIndex;
            return objPageInfo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="providerName">提供者名称</param>
        /// <param name="sql">完整查询语句</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static PageInfo<List<T>> QueryPage<T>(string providerName, string sql, SearchOptionInfo option = null)
        {
            QueryResult<T> objQueryResult = QueryResult<T>(providerName, sql, option);
            PageInfo<List<T>> objPageInfo = new PageInfo<List<T>>();
            objPageInfo.Data = objQueryResult.hits.hits.Select(s => s._source).ToList();
            objPageInfo.RecordCount = objQueryResult.hits.total.value;
            return objPageInfo;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="providerName">提供者名称</param>
        /// <param name="sql">完整查询语句</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static IList<T> Query<T>(string providerName, string sql, int pageSize, int pageIndex, SearchOptionInfo option = null)
        {
            PageInfo<List<T>> objPageInfo = QueryPage<T>(providerName, sql, pageSize, pageIndex, option);
            return objPageInfo.Data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
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
        public static IList<T> Query<T>(string providerName, string index, string type, string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*", SearchOptionInfo option = null)
        {
            PageInfo<List<T>> objPageInfo = QueryPage<T>(providerName, index, type, condition, sortExpression, pageSize, pageIndex, fields, option);
            return objPageInfo.Data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
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
        public static IList<T> QueryLimit<T>(string providerName, string index, string type, string condition, string sortExpression, int offset, int limit, string fields = "*", SearchOptionInfo option = null)
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
            objSql.AppendFormat(" limit {0},{1}", offset, limit);
            return Query<T>(providerName, objSql.ToString(), option);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="providerName">提供者名称</param>
        /// <param name="sql">完整查询语句</param>
        /// <param name="offset">偏移量</param>
        /// <param name="limit">读取条数</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static IList<T> QueryLimit<T>(string providerName, string sql, int offset, int limit, SearchOptionInfo option = null)
        {

            sql = sql + string.Format(" limit {0},{1}", offset, limit);
            return Query<T>(providerName, sql, option);

        }


    }
}
