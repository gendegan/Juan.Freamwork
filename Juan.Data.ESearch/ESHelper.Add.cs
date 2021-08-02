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
using System.Threading.Tasks;
namespace Juan.Data.ESearch
{
    public static partial class ESHelper
    {

        /// <summary>
        ///  新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="data">数据</param>
        /// <param name="isExistsUpdate">存在是否更新</param>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public static void Add<T>(string providerName, string index, string type, T data, bool isExistsUpdate = false, string id = null, OperationOptions options = null) where T : class,new()
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
            AddData(providerName, index, type, dataValue, isExistsUpdate, id, options);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="data">数据</param>
        /// <param name="isExistsUpdate">存在是否更新</param>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public static void AddData(string providerName, string index, string type, string data, bool isExistsUpdate = false, string id = null, OperationOptions options = null)
        {
            try
            {
                var connection = CreateConnection(providerName);
                var indexCommand = new IndexCommand(index, type, id);

                indexCommand.OperationOptions(options);
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
        public static void AddBulk<T>(string providerName, string index, string type, IEnumerable<T> data, bool isExistsUpdate = false, OperationOptions options = null) where T : class,new()
        {
            if (data.Count() == 0)
            {
                return;
            }
            PropertyInfo objPropertyInfo = ESearchIDHelper.GetESearchIDProperty<T>();
            if (objPropertyInfo == null)
            {
                List<string> objDataJson = new List<string>();
                foreach (T item in data)
                {
                    objDataJson.Add(Serializer.ToJson(item));
                }
                AddBulkData(providerName, index, type, objDataJson, options);
            }
            else
            {
                Dictionary<string, object> objData = new Dictionary<string, object>();
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
                    objData[PrimaryKeyValue.ToString()] = item;
                }
                AddBulkData(providerName, index, type, objData, isExistsUpdate, options);
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
        public static void AddBulkData(string providerName, string index, string type, IEnumerable<string> data, OperationOptions options = null)
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
                                    (builder, document) => builder.Index(document, index, type, null, options)
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
        public static void AddBulkData(string providerName, string index, string type, Dictionary<string, object> data, bool isExistsUpdate = false, OperationOptions options = null)
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
                                      (builder, document) => builder.Index(document.Value, index, type, document.Key, options)
                                      );
            }
            else
            {
                bulkJson = new BulkBuilder(Serializer)
                                     .BuildCollection(data,
                                     (builder, document) => builder.Create(document.Value, index, type, document.Key, options)
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
        /// <param name="options">参数</param>
        /// <returns></returns>
        public static Task<OperationResult> AddBulkDataAsync(string providerName, string index, string type, Dictionary<string, object> data, bool isExistsUpdate = false, OperationOptions options = null)
        {

            var connection = CreateConnection(providerName);
            var bulkCommand = new BulkCommand(index, type);
            bulkCommand.OperationOptions(options);
            string bulkJson = "";
            if (isExistsUpdate)
            {
                bulkJson = new BulkBuilder(Serializer)
                                      .BuildCollection(data,
                                      (builder, document) => builder.Index(document.Value, index, type, document.Key, options)
                                      );
            }
            else
            {
                bulkJson = new BulkBuilder(Serializer)
                                     .BuildCollection(data,
                                     (builder, document) => builder.Create(document.Value, index, type, document.Key, options)
                                     );
            }
            return connection.PostAsync(bulkCommand, bulkJson);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="objBulkCommand"></param>
        /// <param name="bulkData"></param>
        public static void BulkData(string providerName, BulkCommand objBulkCommand, string bulkData)
        {
            var connection = CreateConnection(providerName);
            var result = connection.Post(objBulkCommand, bulkData);

            Serializer.ToBulkResult(result);
        }



    }
}
