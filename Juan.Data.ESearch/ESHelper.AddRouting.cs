﻿using System;
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
using System.Threading.Tasks;
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
        /// <param name="isExistsUpdate">存在是否更新</param>
        /// <param name="id">主键</param>
        /// <param name="routing"></param>
        public static void AddRouting<T>(string providerName, string index, string type, T data, bool isExistsUpdate = false, string id = null, string routing = null, OperationOptions options = null) where T : class,new()
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
                    else
                    {
                        throw new ArgumentNullException("未设置主键值");
                    }
                }
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
            AddDataRouting(providerName, index, type, routing, dataValue, isExistsUpdate, id, options);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="routing"></param>
        /// <param name="data">数据</param>
        /// <param name="isExistsUpdate">存在是否更新</param>
        /// <param name="id">主键</param>
        public static void AddDataRouting(string providerName, string index, string type, string routing, string data, bool isExistsUpdate = false, string id = null, OperationOptions options = null)
        {
            if (string.IsNullOrWhiteSpace(routing))
            {
                throw new ArgumentNullException("未设置Routing值");
            }
            try
            {
                var connection = CreateConnection(providerName);
                var indexCommand = new IndexCommand(index, type, id);
                indexCommand.OperationOptions(options);
                indexCommand.Routing(routing);
                OperationResult result;
                if (string.IsNullOrWhiteSpace(id))
                {
                    result = connection.Post(indexCommand, data);
                }
                else
                {
                    if (isExistsUpdate)
                    {
                        result = connection.Put(indexCommand, data);

                    }
                    else
                    {

                        result = connection.Put(indexCommand.OperationType(IndexOperation.create), data);
                    }
                }
                Serializer.ToIndexResult(result);
            }
            catch (OperationException ex)
            {
                if (ex.HttpStatusCode == 404 || ex.HttpStatusCode == 409)
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
        /// <param name="isExistsUpdate">存在是否更新</param>
        /// <param name="options">参数</param>
        public static void AddBulkRouting<T>(string providerName, string index, string type, IEnumerable<T> data, bool isExistsUpdate = false, OperationOptions options = null) where T : class,new()
        {
            if (data.Count() == 0)
            {
                return;
            }
            PropertyInfo objPropertyInfo = ESearchIDHelper.GetESearchIDProperty<T>();
            PropertyInfo objPropertyRoutingInfo = ESearchIDHelper.GetESearchRoutingProperty<T>();
            if (objPropertyRoutingInfo == null)
            {
                throw new ArgumentNullException("请在类加上ESearchRouting特性作为Routing值");
            }

            if (objPropertyInfo == null)
            {
                List<RoutingData> objRoutingDataList = new List<RoutingData>();
                foreach (T item in data)
                {

                    RoutingData objRoutingData = new RoutingData();
                    objRoutingData.Data = item;
                    objRoutingData.Routing = objPropertyRoutingInfo.GetValue(item, null).ToString();
                    objRoutingDataList.Add(objRoutingData);
                }
                AddBulkDataRouting(providerName, index, type, objRoutingDataList, options);
            }
            else
            {
                Dictionary<string, RoutingData> objData = new Dictionary<string, RoutingData>();
                foreach (T item in data)
                {
                    RoutingData objRoutingData = new RoutingData();
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
                    objRoutingData.Data = item;
                    objRoutingData.Routing = objPropertyRoutingInfo.GetValue(item, null).ToString();
                    objData[PrimaryKeyValue.ToString()] = objRoutingData;
                }
                AddBulkDataRouting(providerName, index, type, objData, isExistsUpdate, options);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="data">数据</param>
        /// <param name="options">参数</param>
        public static void AddBulkDataRouting(string providerName, string index, string type, IEnumerable<RoutingData> data, OperationOptions options = null)
        {
            if (data.Count() == 0)
            {
                return;
            }
            var connection = CreateConnection(providerName);
            var bulkCommand = new BulkCommand(index: index, type: type);
            bulkCommand.OperationOptions(options);

            string bulkJson = new BulkBuilder(Serializer)
                                    .BuildCollection(data,
                                    (builder, document) => builder.Index(document.Data, index, type, null, new BulkOperationOptions() { Routing = document.Routing })
                                    );
            var result = connection.Post(bulkCommand, bulkJson);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="data">数据</param>
        /// <param name="isExistsUpdate">存在是否更新</param>
        /// <param name="options">参数</param>
        public static void AddBulkDataRouting(string providerName, string index, string type, Dictionary<string, RoutingData> data, bool isExistsUpdate = false, OperationOptions options = null)
        {
            if (data.Count() == 0)
            {
                return;
            }
            var connection = CreateConnection(providerName);
            var bulkCommand = new BulkCommand(index, type);
            bulkCommand.OperationOptions(options);
            string bulkJson = "";
            if (isExistsUpdate)
            {
                bulkJson = new BulkBuilder(Serializer)
                                      .BuildCollection(data,
                                      (builder, document) => builder.Index(document.Value.Data, index, type, document.Key, new BulkOperationOptions() { Routing = document.Value.Routing })
                                      );
            }
            else
            {
                bulkJson = new BulkBuilder(Serializer)
                                     .BuildCollection(data,
                                     (builder, document) => builder.Create(document.Value.Data, index, type, document.Key, new BulkOperationOptions() { Routing = document.Value.Routing })
                                     );
            }
            var result = connection.Post(bulkCommand, bulkJson);


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="data">数据</param>
        /// <param name="isExistsUpdate">存在是否更新</param>
        /// <returns></returns>
        public static Task<OperationResult> AddBulkDataRoutingAsync(string providerName, string index, string type, Dictionary<string, RoutingData> data, bool isExistsUpdate = false)
        {

            var connection = CreateConnection(providerName);
            var bulkCommand = new BulkCommand(index, type);
            string bulkJson = "";
            if (isExistsUpdate)
            {
                bulkJson = new BulkBuilder(Serializer)
                                      .BuildCollection(data,
                                      (builder, document) => builder.Index(document.Value.Data, index, type, document.Key, new BulkOperationOptions() { Routing = document.Value.Routing })
                                      );
            }
            else
            {
                bulkJson = new BulkBuilder(Serializer)
                                     .BuildCollection(data,
                                     (builder, document) => builder.Create(document.Value.Data, index, type, document.Key, new BulkOperationOptions() { Routing = document.Value.Routing })
                                     );
            }
            return connection.PostAsync(bulkCommand, bulkJson);

        }




    }
}
