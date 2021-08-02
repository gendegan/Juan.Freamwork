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
        /// <param name="data">数据</param>
        /// <param name="isNoExistsAdd">不存在是否新增</param>
        /// <param name="id">主键</param>
        /// <param name="routing"></param>
        /// <param name="options">参数</param>
        public static void UpdateRouting<T>(string providerName, string index, string type, T data, bool isNoExistsAdd = false, string id = null, string routing = null, OperationOptions options = null) where T : class, new()
        {
            string dataValue = Serializer.ToJson(data);
            if (id == null)
            {
                object PrimaryKeyValue = data.GetESearchIDValue();
                if (PrimaryKeyValue != null)
                {
                    string idValue = PrimaryKeyValue.ToString();
                    if (idValue != "0" && !string.IsNullOrWhiteSpace(idValue))
                    {
                        id = idValue;
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(id) || id == "0")
            {
                throw new ArgumentNullException("未设置主键值");
            }
            if (routing == null)
            {
                object RoutingValueValue = data.GetESearchRoutingValue();
                if (RoutingValueValue != null)
                {
                    string routingValue = RoutingValueValue.ToString();
                    if (!string.IsNullOrWhiteSpace(routingValue))
                    {
                        routing = routingValue;
                    }
                    else
                    {
                        throw new ArgumentNullException("未设置Routing值");
                    }
                }
                else
                {
                    throw new ArgumentNullException("未设置Routing值");
                }

            }
            dataValue = string.Format("{{\"doc\":{0},\"doc_as_upsert\" : {1}}}", dataValue, isNoExistsAdd.ToString().ToLower());
            UpdateDataRouting(providerName, index, type, routing, id, dataValue, options);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="routing"></param>
        /// <param name="id">主键</param>
        /// <param name="data">数据</param>
        /// <param name="options">参数</param>
        public static void UpdateDataRouting(string providerName, string index, string type, string routing, string id, string data, OperationOptions options = null)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("id不能为空值");
            }
            if (string.IsNullOrWhiteSpace(routing))
            {
                throw new ArgumentNullException("未设置Routing值");
            }
            try
            {
                var connection = CreateConnection(providerName);
                OperationResult result;
                var indexCommand = new IndexUpdateCommand(index, null, id);
                indexCommand.OperationOptions(options);
                indexCommand.Routing(routing);
                result = connection.Post(indexCommand, data);
                Serializer.ToIndexResult(result);
                return;
            }
            catch (OperationException ex)
            {
                if (ex.HttpStatusCode == 404)
                    return;
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="data">数据</param>
        /// <param name="isNoExistsAdd">不存在是否新增</param>
        /// <param name="options">参数</param>
        public static void UpdateBulkRouting<T>(string providerName, string index, string type, IEnumerable<T> data, bool isNoExistsAdd = false, OperationOptions options = null) where T : class, new()
        {
            if (data.Count() == 0)
            {
                return;
            }
            PropertyInfo objPropertyInfo = ESearchIDHelper.GetESearchIDProperty<T>();
            if (objPropertyInfo == null)
            {
                throw new ArgumentNullException("类未设置PrimaryKey特性");
            }
            PropertyInfo objPropertyRoutingInfo = ESearchIDHelper.GetESearchRoutingProperty<T>();
            if (objPropertyRoutingInfo == null)
            {
                throw new ArgumentNullException("请在类加上ESearchRouting特性作为Routing值");
            }

            Dictionary<string, RoutingData> objData = new Dictionary<string, RoutingData>();
            foreach (T item in data)
            {
                object PrimaryKeyValue = objPropertyInfo.GetValue(item, null);
                if (PrimaryKeyValue != null)
                {
                    string idValue = PrimaryKeyValue.ToString();
                    if (idValue == "0" || string.IsNullOrWhiteSpace(idValue))
                    {
                        throw new ArgumentNullException("未设置主键值");
                    }
                }
                else
                {
                    throw new ArgumentNullException("未设置主键值");
                }

                RoutingData objRoutingData = new RoutingData();
                objRoutingData.Data = item;
                objRoutingData.Routing = objPropertyRoutingInfo.GetValue(item, null).ToString();
                objData[PrimaryKeyValue.ToString()] = objRoutingData;
            }
            UpdateBulkDataRouting(providerName, index, type, objData, isNoExistsAdd, options);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="data">数据</param>
        /// <param name="isNoExistsAdd">不存在是否新增</param>
        /// <param name="options">参数</param>
        public static void UpdateBulkDataRouting(string providerName, string index, string type, Dictionary<string, RoutingData> data, bool isNoExistsAdd = false, OperationOptions options = null)
        {
            if (data.Count() == 0)
            {
                return;
            }
            var connection = CreateConnection(providerName);
            var bulkCommand = new BulkCommand(index, type);
            bulkCommand.OperationOptions(options);
            string bulkJson = new BulkBuilder(Serializer)
                                      .BuildCollection(data,
                                      (builder, document) => builder.Update(document.Value.Data, index, type, document.Key, isNoExistsAdd, new BulkOperationOptions() { Routing = document.Value.Routing })
                                      );

            var result = connection.Post(bulkCommand, bulkJson);

        }

    }
}
