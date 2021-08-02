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
    /// <summary>
    /// 
    /// </summary>
    public static partial class ESHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="sql">完整查询语句</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static DataTable QueryGroupTable(string providerName, string sql, SearchOptionInfo option = null)
        {
            return QueryGroupJson(providerName, sql, option).ToDataTable();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="index"></param>
        /// <param name="type"></param>
        /// <param name="condition"></param>
        /// <param name="sortExpression"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="fields"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static DataTable QueryGroupTable(string providerName, string index, string type, string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*", SearchOptionInfo option = null)
        {
            return QueryGroupJson(providerName, index, type, condition, sortExpression, pageSize, pageIndex, fields, option).ToDataTable();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="index"></param>
        /// <param name="type"></param>
        /// <param name="condition"></param>
        /// <param name="sortExpression"></param>
        /// <param name="fields"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static DataTable QueryGroupTable(string providerName, string index, string type, string condition, string sortExpression, string fields = "*", SearchOptionInfo option = null)
        {
            return QueryGroupJson(providerName, index, type, condition, sortExpression, fields, option).ToDataTable();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="sql">完整查询语句</param>
        /// <param name="limit">读取条数</param>
        /// <param name="option">参数</param>
        /// <returns></returns>

        public static DataTable QueryGroupTable(string providerName, string sql, int limit, SearchOptionInfo option = null)
        {
            return QueryGroupTable(providerName, sql + string.Format(" limit {0}", limit), option);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="providerName">提供者名称</param>
        /// <param name="sql">完整查询语句</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public static IList<T> QueryGroup<T>(string providerName, string sql, SearchOptionInfo option = null)
        {
            return QueryGroupJson(providerName, sql, option).JsonDeserialize<List<T>>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="providerName"></param>
        /// <param name="index"></param>
        /// <param name="type"></param>
        /// <param name="condition"></param>
        /// <param name="sortExpression"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="fields"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static IList<T> QueryGroup<T>(string providerName, string index, string type, string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*", SearchOptionInfo option = null)
        {
            return QueryGroupJson(providerName, index, type, condition, sortExpression, pageSize, pageIndex, fields, option).JsonDeserialize<List<T>>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="providerName"></param>
        /// <param name="index"></param>
        /// <param name="type"></param>
        /// <param name="condition"></param>
        /// <param name="sortExpression"></param>
        /// <param name="fields"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static IList<T> QueryGroup<T>(string providerName, string index, string type, string condition, string sortExpression, string fields = "*", SearchOptionInfo option = null)
        {
            return QueryGroupJson(providerName, index, type, condition, sortExpression, fields, option).JsonDeserialize<List<T>>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="index"></param>
        /// <param name="type"></param>
        /// <param name="condition"></param>
        /// <param name="sortExpression"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="fields"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static string QueryGroupJson(string providerName, string index, string type, string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*", SearchOptionInfo option = null)
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

            return QueryGroupJson(providerName, objSql.ToString(), option);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="index"></param>
        /// <param name="type"></param>
        /// <param name="condition"></param>
        /// <param name="sortExpression"></param>
        /// <param name="fields"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static string QueryGroupJson(string providerName, string index, string type, string condition, string sortExpression, string fields = "*", SearchOptionInfo option = null)
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

            return QueryGroupJson(providerName, objSql.ToString(), option);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="sql">完整查询语句</param>
        /// <param name="option">参数</param>

        /// <returns></returns>
        public static string QueryGroupJson(string providerName, string sql, SearchOptionInfo option = null)
        {
            string queryResult = QueryResult(providerName, sql, option);

            return ParseGroupResultJson(queryResult);
        }

        /// <summary>
        /// 解析分组查询结果
        /// </summary>
        /// <param name="data"></param>

        /// <returns></returns>
        private static string ParseGroupResultJson(string data)
        {
            JObject objJObject = JObject.Parse(data);
            List<string> objJsonValue = GroupResult("", objJObject.GetValue("aggregations").ToString(), new List<string>());
            StringBuilder objStringBuilder = new StringBuilder();
            if (objJsonValue.Count > 0)
            {
                foreach (string value in objJsonValue)
                {
                    objStringBuilder.AppendFormat("{0},", value);
                }

            }
            return string.Format("[{0}]", objStringBuilder.ToString().TrimEnd(','));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="bucket"></param>
        /// <param name="additionalResult"></param>
        /// <returns></returns>
        private static List<string> GroupResult(string bucketName, string bucket, List<string> additionalResult)
        {
            List<string> groupResult = new List<string>();
            Dictionary<string, List<JToken>> subBuckets = GetSubBuckets(bucket);
            if (subBuckets.Values.Count > 0)
            {
                foreach (string key in subBuckets.Keys)
                {
                    string subBucketName = key;
                    List<JToken> subBucketList = subBuckets[subBucketName];
                    foreach (JToken subBucket in subBucketList)
                    {
                        JObject jObject = (JObject)subBucket;
                      
                        if (!string.IsNullOrEmpty(bucketName))
                        {
                            JObject objJObject = JObject.Parse(bucket);
                            JToken objJToken = objJObject["key"];
                            bool isString = objJToken.Type == JTokenType.String;
                            additionalResult.Add(string.Format("{0}:{1}{2}{3}", bucketName, isString ? "\"" : "", objJObject["key"], isString ? "\"" : ""));
                        }
                        List<string> newGroupResult = GroupResult(subBucketName, JsonHelper.JsonSerialize(subBucket), additionalResult);
                        groupResult.AddRange(newGroupResult);
                    }
                }
            }
            else
            {
                List<string> result = additionalResult;
                JObject objJObject = JObject.Parse(bucket);
                if (!string.IsNullOrEmpty(bucketName))
                {
                    if (objJObject.Property("key_as_string") != null)
                    {
                        JToken objJToken = objJObject.Property("key_as_string").Value;
                        bool isString = objJToken.Type == JTokenType.String;
                        result.Add(string.Format("{0}:{1}{2}{3}", bucketName, isString ? "\"" : "", objJToken.ToString(), isString ? "\"" : ""));
                    }
                    else
                    {
                        JToken objJToken = objJObject.Property("key").Value;
                        bool isString = objJToken.Type == JTokenType.String;
                        result.Add(string.Format("{0}:{1}{2}{3}", bucketName, isString ? "\"" : "", objJToken.ToString(), isString ? "\"" : ""));
                    }
                }
                IEnumerable<JProperty> properties = objJObject.Properties();
                foreach (JProperty iJProperty in properties)
                {
                    JToken bucketValue = iJProperty.Value;
                    if (bucketValue.Type == JTokenType.Object && bucketValue["value"] != null)
                    {
                        if (bucketValue["value_as_string"] != null)
                        {
                            JToken objJToken = bucketValue["value_as_string"];
                            bool isString = objJToken.Type == JTokenType.String;
                            result.Add(string.Format("value_as_string:{0}{1}{2}", isString ? "\"" : "", objJToken.ToString(), isString ? "\"" : ""));
                        }
                        else
                        {
                            JToken objJToken = bucketValue["value"];
                            bool isString = objJToken.Type == JTokenType.String;
                            result.Add(string.Format("{0}:{1}{2}{3}", iJProperty.Name, isString ? "\"" : "", objJToken.ToString(), isString ? "\"" : ""));
                        }
                    }
                    else
                    {
                        if (bucketValue.Type == JTokenType.Object)
                        {
                            fillFieldsForSpecificAggregation(result, bucketValue, iJProperty.Name);
                        }
                    }
                }
                groupResult.Add("{" + result.ToConcat() + "}");
            }


            return groupResult;
        }

        private static Dictionary<string, List<JToken>> GetSubBuckets(string strBucket)
        {
            Dictionary<string, List<JToken>> dic = new Dictionary<string, List<JToken>>();
            JObject objJObject = JObject.Parse(strBucket);
            IEnumerable<JProperty> properties = objJObject.Properties();
            foreach (JProperty jProperty in properties)
            {
                List<JToken> list = new List<JToken>();
                if (jProperty.Value.Type == JTokenType.Object)
                {
                    var buckets = jProperty.Value["buckets"];
                    if (buckets != null)
                    {
                        for (int i = 0; i < buckets.Count(); i++)
                        {
                            list.Add(buckets[i]);
                        }
                        dic.Add(jProperty.Name, list);
                    }
                }
            }
            return dic;
        }

        private static List<string> fillFieldsForSpecificAggregation(List<string> result, JToken value, string field)
        {
            JObject objJObject = (JObject)value.Values();
            IEnumerable<JProperty> properties = objJObject.Properties();
            foreach (JProperty iJProperty in properties)
            {
                if (iJProperty.Name == "values")
                {
                    fillFieldsForSpecificAggregation(result, iJProperty.Value, field);
                }
                else
                {
                    JToken objJToken = iJProperty.Value;
                    bool isString = objJToken.Type == JTokenType.String;
                    result.Add(string.Format("{0}:{1}{2}{3}", field + "." + iJProperty.Name, isString ? "\"" : "", objJToken.ToString(), isString ? "\"" : ""));
                }
            }
            return result;
        }



    }
}
