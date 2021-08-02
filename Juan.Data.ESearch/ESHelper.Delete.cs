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
        /// <param name="id">主键</param>
        /// <returns></returns>
        public static void DeleteID(string providerName, string index, string type, string id, OperationOptions options = null)
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
            string[] IDArray = id.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (IDArray.Length == 1)
            {
                var connection = CreateConnection(providerName);
                DeleteCommand objDeleteCommand = Commands.Delete(index, type, id);
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
                Delete(providerName, index, type, IDArray, options);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="condition">查询条件</param>
        /// <param name="options">参数</param>
        public static void Delete(string providerName, string index, string type, string condition, OperationOptions options = null)
        {
            SearchOptionInfo objSearchOption = new SearchOptionInfo();
            if (options == null)
            {
                objSearchOption = null;
            }
            else
            {
                objSearchOption.IndexName = index;
                objSearchOption.TypeName = type;
                objSearchOption.Routing = options.Routing;
            }
            while (true)
            {

                List<HitsInfo<object, object>> idList = QueryIDInfo(providerName, index, type, condition, 1000, 0, "", objSearchOption);

                if (idList.Count == 0)
                {
                    break;
                }
                else
                {
                    ConsoleHelper.WriteLineRed("开始删除:" + idList.Count + "条数");
                    foreach (var groupRouting in idList.GroupBy(s => s._routing))
                    {
                        BulkOperationOptions BulkOptions = new BulkOperationOptions();
                        BulkOptions.Refresh = true;
                        if (!string.IsNullOrWhiteSpace(groupRouting.Key))
                        {
                            BulkOptions.Routing = groupRouting.Key;
                        }
                        if (options != null)
                        {
                            BulkOptions.VersionType = options.VersionType;
                            BulkOptions.Version = options.Version;
                            BulkOptions.Percolate = options.Percolate;
                            BulkOptions.Parent = options.Parent;
                            BulkOptions.Timestamp = options.Timestamp;
                            BulkOptions.Ttl = options.Ttl;
                        }
                        Delete(providerName, index, type, groupRouting.Select(s => s._id).ToList(), BulkOptions);
                    }
                }

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static void Delete(string providerName, string index, string type, IEnumerable<string> ids, BulkOperationOptions options = null)
        {
            if (ids.Count() == 0)
            {
                return;
            }
            var connection = CreateConnection(providerName);
            var bulkCommand = new BulkCommand(index, type);

            bulkCommand.OperationOptions(options);

            string bulkJson = new BulkBuilder(Serializer)
                                    .BuildCollection(ids,
                                    (builder, document) => builder.Delete(document, index, type, options)
                                    );
            string result = connection.Post(bulkCommand, bulkJson);

        }



    }
}
