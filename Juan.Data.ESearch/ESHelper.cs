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
using PlainElastic7.Net.Queries;

namespace Juan.Data.ESearch
{
    public static partial class ESHelper
    {

        public static JsonNetSerializer Serializer = new JsonNetSerializer();

        private static ElasticConnection CreateConnection(string providerName)
        {

            ESProviderElement objESProviderElement = ESConfigHelper.GetProducerElement(providerName);

            return new ElasticConnection(new Uri(objESProviderElement.ProcessServerUrl()), objESProviderElement.SslPath, objESProviderElement.SslPassword);
        }


        public static void Optimize(string providerName, string index, string type)
        {
            var connection = CreateConnection(providerName);
            string result = connection.Post(Commands.Optimize(index, type));

        }
        public static Dictionary<string, object> CreateScirptParams(this string parameterNames, params object[] parmsValue)
        {
            if (string.IsNullOrWhiteSpace(parameterNames))
            {
                throw new ArgumentException("parameterNames,不能为空值");
            }

            if (parmsValue == null || parmsValue.Length == 0)
            {
                throw new ArgumentException("parmsValue,不能为空值");
            }
            string[] paramNames = parameterNames.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (paramNames.Length == 0)
            {
                throw new ArgumentException("parameterNames,不能为空值：" + paramNames.Length);
            }

            Dictionary<string, object> parmsList = new Dictionary<string, object>();
            if (paramNames.Length == parmsValue.Length)
            {
                for (int i = 0; i < paramNames.Length; i++)
                {
                    parmsList.Add(paramNames[i], parmsValue[i]);

                }
                return parmsList;
            }
            else
            {
                throw new ArgumentException("传入的参数个数和值的个数不一致");
            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="providerName"></param>
        /// <param name="sql"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static QueryResult<T> QueryResult<T>(string providerName, string sql, SearchOptionInfo option = null)
        {
            string queryResult = QueryResult(providerName, sql, option);
            QueryResult<T> objQueryResult = Serializer.Deserialize<QueryResult<T>>(queryResult);
            return objQueryResult;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="FieldT"></typeparam>
        /// <param name="providerName"></param>
        /// <param name="sql"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static QueryResult<T, FieldT> QueryResult<T, FieldT>(string providerName, string sql, SearchOptionInfo option = null)
        {
            string queryResult = QueryResult(providerName, sql, option);
            QueryResult<T, FieldT> objQueryResult = Serializer.Deserialize<QueryResult<T, FieldT>>(queryResult);
            return objQueryResult;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="providerName"></param>
        /// <param name="indexName"></param>
        /// <param name="indexType"></param>
        /// <param name="query"></param>
        /// <param name="from"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static SearchResult<T> QueryResult<T>(string providerName, string indexName, string indexType, QueryBuilder<T> query, int from, int size)
        {

            var connection = CreateConnection(providerName);
            var queryString = query.From(from).Size(size).Build();

            var cmd = new SearchCommand(indexName, indexType);

            var result = connection.Post(cmd, queryString);

            var serializer = new JsonNetSerializer();

            return serializer.ToSearchResult<T>(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="sql"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static string QueryResult(string providerName, string sql, SearchOptionInfo option = null)
        {
            var connection = CreateConnection(providerName);
            try
            {
                if (option != null && option.IsNoInfo())
                {
                    string ExplainSql = QueryExplain(providerName, sql);
                    SearchCommand objSearchCommand = Commands.Search(option.IndexName, option.TypeName).Pretty();
                    objSearchCommand.Routing(option.Routing);
                    var result = connection.Post(objSearchCommand, ExplainSql);
                    return result.Result;
                }
                else
                {
                    var result = connection.Post("_nlpcn/sql", sql); 
                    return result.Result;
                }
            }
            catch (OperationException ex)
            {

                LogHelper.Write(LogType.Error, "查询ES异常", sql, ex);
                throw ex;

            }
            finally
            {

                if (LogSectionHelper.IsESearch)
                {
                    LogHelper.Write(LogType.ESearch, sql, sql);

                }
            }

        }
        private static void ParseSearchOptionInfo(this SearchOptionInfo objSearchOptions, string indexName, string typeName)
        {
            if (objSearchOptions != null)
            {
                if (string.IsNullOrWhiteSpace(objSearchOptions.IndexName))
                {
                    objSearchOptions.IndexName = indexName;
                }
                if (string.IsNullOrWhiteSpace(objSearchOptions.TypeName))
                {
                    objSearchOptions.TypeName = typeName;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static string QueryExplain(string providerName, string sql)
        {
            var connection = CreateConnection(providerName);
            try
            {
                var result = connection.Post("_nlpcn/sql/explain", sql);
                return result.Result;
            }
            catch (OperationException ex)
            {

                LogHelper.Write(LogType.Error, "查询ES异常", sql, ex);
                throw ex;
            }
            finally
            {
                if (LogSectionHelper.IsESearch)
                {
                    LogHelper.Write(LogType.ESearch, sql, sql);

                }
            }

        }

        public static OperationResult Post(string providerName, string command, string jsonData)
        {
            var connection = CreateConnection(providerName);
            return connection.Post(command, jsonData);

        }
        public static OperationResult Put(string providerName, string command, string jsonData)
        {
            var connection = CreateConnection(providerName);
            return connection.Put(command, jsonData);
        }
        public static OperationResult Head(string providerName, string command, string jsonData)
        {
            var connection = CreateConnection(providerName);
            return connection.Head(command, jsonData);
        }
        public static OperationResult Get(string providerName, string command, string jsonData)
        {
            var connection = CreateConnection(providerName);
            return connection.Get(command, jsonData);
        }
    }
}
