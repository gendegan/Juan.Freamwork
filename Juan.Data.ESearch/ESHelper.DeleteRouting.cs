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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="routing"></param>
        /// <param name="id">主键</param>
        public static void DeleteRoutingID(string providerName, string index, string type, string routing, string id, OperationOptions options = null)
        {
            if (string.IsNullOrWhiteSpace(index))
            {
                throw new ArgumentNullException("参数index不能为空");
            }
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentNullException("参数type不能为空");
            }
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentNullException("参数id不能为空");
            }
            if (string.IsNullOrWhiteSpace(routing))
            {
                throw new ArgumentNullException("参数routing不能为空");
            }
            string[] IDArray = id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (IDArray.Length == 1)
            {
                var connection = CreateConnection(providerName);
                DeleteCommand objDeleteCommand = Commands.Delete(index, type, id);
                objDeleteCommand.Routing(routing);
                objDeleteCommand.OperationOptions(options);

                try
                {
                    var result = connection.Delete(objDeleteCommand);

                }
                catch (OperationException ex)
                {
                    if (ex.HttpStatusCode == 404)
                    {
                        DeleteResult deleteResult = Serializer.ToDeleteResult(ex.Message);
                        return;
                    }
                    throw ex;
                }
            }
            else
            {
                DeleteRouting(providerName, index, type, routing, IDArray, options);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="routing"></param>
        /// <param name="condition">查询条件</param>
        public static void DeleteRouting(string providerName, string index, string type, string routing, string condition, OperationOptions options = null)
        {
            SearchOptionInfo objSearchOption = new SearchOptionInfo();

            objSearchOption.IndexName = index;
            objSearchOption.TypeName = type;
            objSearchOption.Routing = routing;

            while (true)
            {

                IList<string> idList = QueryID(providerName, index, type, condition, 1000, 0, "", objSearchOption);
                if (idList.Count == 0)
                {
                    break;
                }
                else
                {
                    DeleteRouting(providerName, index, type, routing, idList, options);
                }

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="routing"></param>
        /// <param name="ids"></param>
        public static void DeleteRouting(string providerName, string index, string type, string routing, IEnumerable<string> ids, OperationOptions options = null)
        {
            if (string.IsNullOrWhiteSpace(routing))
            {
                throw new ArgumentNullException("参数routing不能为空");
            }
            if (ids.Count() == 0)
            {
                return;
            }
            var connection = CreateConnection(providerName);
            var bulkCommand = new BulkCommand(index, type);
            bulkCommand.OperationOptions(options);
            string bulkJson = new BulkBuilder(Serializer)
                                    .BuildCollection(ids,
                                    (builder, document) => builder.Delete(document, index, type, new BulkOperationOptions() { Routing = routing })
                                    );
            string result = connection.Post(bulkCommand, bulkJson);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="DeleteData"></param>
        /// <param name="options">参数</param>
        public static void DeleteRouting(string providerName, string index, string type, IEnumerable<RoutingDelete> DeleteData, OperationOptions options = null)
        {
            if (DeleteData.Count() == 0)
            {
                return;
            }
            var connection = CreateConnection(providerName);
            var bulkCommand = new BulkCommand(index, type);
            bulkCommand.OperationOptions(options);
            string bulkJson = new BulkBuilder(Serializer)
                                    .BuildCollection(DeleteData,
                                    (builder, document) => builder.Delete(document.ID, index, type, new BulkOperationOptions() { Routing = document.Routing })
                                    );
            string result = connection.Post(bulkCommand, bulkJson);

        }

    }
}
