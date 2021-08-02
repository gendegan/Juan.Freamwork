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
        /// <param name="providerName"></param>
        /// <param name="index"></param>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <param name="fields"></param>
        /// <param name="isExclude"></param>
        /// <param name="id"></param>
        /// <param name="options"></param>
        public static void UpdateAddFields<T>(string providerName, string index, string type, T data, string fields, bool isExclude = false, string id = null, OperationOptions options = null) where T : class, new()
        {
            string scriptiInline = "";
            if (string.IsNullOrWhiteSpace(fields))
            {
                throw new ArgumentNullException("fields", "参数fields不能不空");
            }
            string[] updateFields = fields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (isExclude)
            {
                PropertyInfo[] objPropertyInfoList = typeof(T).GetProperties();
                updateFields = objPropertyInfoList.Select(s => s.Name).ToList().Except(updateFields).ToArray();
            }
            foreach (var item in updateFields)
            {
                scriptiInline += string.Format("ctx._source.{0}=params.{0};", item);
            }
            UpdateAdd<T>(providerName, index, type, data, scriptiInline, null, id, options);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="providerName"></param>
        /// <param name="index"></param>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <param name="fields"></param>
        /// <param name="isExclude"></param>
        /// <param name="options"></param>
        public static void UpdateAddFieldsBulk<T>(string providerName, string index, string type, IEnumerable<T> data, string fields, bool isExclude = false,  OperationOptions options = null) where T : class, new()
        {

            string scriptiInline = "";
            if (string.IsNullOrWhiteSpace(fields))
            {
                throw new ArgumentNullException("fields", "参数fields不能不空");
            }
            string[] updateFields = fields.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (isExclude)
            {
                PropertyInfo[] objPropertyInfoList = typeof(T).GetProperties();
                updateFields = objPropertyInfoList.Select(s => s.Name).ToList().Except(updateFields).ToArray();
            }
            foreach (var item in updateFields)
            {
                scriptiInline += string.Format("ctx._source.{0}=params.{0};", item);
            }
            UpdateAddBulk<T>(providerName, index, type, data, scriptiInline, true, options);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="data">数据</param>
        /// <param name="scriptiInline"></param>
        /// <param name="scirptParams"></param>
        /// <param name="id">主键</param>
        /// <param name="options">参数</param>
        public static void UpdateAdd<T>(string providerName, string index, string type, T data, string scriptiInline, Dictionary<string, object> scirptParams = null, string id = null, OperationOptions options = null) where T : class, new()
        {

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
            if (string.IsNullOrWhiteSpace(scriptiInline))
            {
                throw new ArgumentNullException("参数scriptiInline不能不空");
            }
            if (string.IsNullOrWhiteSpace(id) || id == "0")
            {
                throw new ArgumentNullException("未设置主键值");
            }
            UpdateScript updateScript = new UpdateScript();
            updateScript.Script.source = scriptiInline;
            if (scirptParams != null && scirptParams.Count > 0)
            {
                updateScript.Script.@params = scirptParams;
            }
            else
            {
                updateScript.Script.@params = data;
            }
            updateScript.Upsert = data;
            Update(providerName, index, type, id, updateScript, options);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="id">主键</param>
        /// <param name="scriptiInline"></param>
        /// <param name="scirptParams"></param>
        /// <returns></returns>
        public static void Update(string providerName, string index, string type, string id, string scriptiInline, Dictionary<string, object> scirptParams, OperationOptions options = null)
        {
            if (string.IsNullOrWhiteSpace(id) || id == "0")
            {
                throw new ArgumentNullException("参数id不能不空");
            }
            if (string.IsNullOrWhiteSpace(scriptiInline))
            {
                throw new ArgumentNullException("参数scriptiInline不能不空");
            }
            UpdateScript updateScript = new UpdateScript();
            updateScript.Script.source = scriptiInline;
            if (scirptParams != null)
            {
                updateScript.Script.@params = scirptParams;
            }
            Update(providerName, index, type, id, updateScript, options);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="id">主键</param>
        /// <param name="updateScript"></param>
        /// <param name="options">参数</param>
        public static void Update(string providerName, string index, string type, string id, UpdateScript updateScript, OperationOptions options = null)
        {
            UpdateData(providerName, index, type, id, updateScript.ToJsonString(), options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="data">数据</param>
        /// <param name="scriptiInline"></param>
        /// <param name="isNoExistsAdd">不存在是否新增</param>
        /// <param name="options">参数</param>
        public static void UpdateAddBulk<T>(string providerName, string index, string type, IEnumerable<T> data, string scriptiInline, bool isNoExistsAdd = true, OperationOptions options = null) where T : class, new()
        {
            if (data.Count() == 0)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(scriptiInline))
            {
                throw new ArgumentNullException("参数scriptiInline不能不空");
            }
            PropertyInfo objPropertyInfo = ESearchIDHelper.GetESearchIDProperty<T>();
            if (objPropertyInfo == null)
            {
                throw new ArgumentNullException("类未设置PrimaryKey特性");

            }

            Dictionary<string, UpdateScript> objScriptData = new Dictionary<string, UpdateScript>();
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
                UpdateScript objUpdateScript = new UpdateScript();
                objUpdateScript.Script.source = scriptiInline;
                objUpdateScript.Script.@params = item;
                if (isNoExistsAdd)
                {
                    objUpdateScript.Upsert = item;
                }

                objScriptData[PrimaryKeyValue.ToString()] = objUpdateScript;


            }
            UpdateAddBulk(providerName, index, type, objScriptData, options);

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="updateScripts"></param>
        /// <param name="options">参数</param>

        public static void UpdateAddBulk(string providerName, string index, string type, Dictionary<string, UpdateScript> updateScripts, OperationOptions options = null)
        {
            if (updateScripts.Count() == 0)
            {
                return;
            }
            var connection = CreateConnection(providerName);
            var bulkCommand = new BulkCommand(index, type);
            bulkCommand.OperationOptions(options);
            string bulkJson = "";
            bulkJson = new BulkBuilder(Serializer)
                                 .BuildCollection(updateScripts,
                                 (builder, document) => builder.UpdateScirpt(document.Value.ToJsonString(), index, type, document.Key, options)
                                 );
            var result = connection.Post(bulkCommand, bulkJson);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="scriptiInline"></param>
        /// <param name="condition">查询条件</param>
        /// <param name="options">参数</param>
        public static void UpdateBulk(string providerName, string index, string type, string scriptiInline, string condition, OperationOptions options = null)
        {
            UpdateScript objUpdateScript = new UpdateScript();
            objUpdateScript.Script.source = scriptiInline;
            UpdateBulk(providerName, index, type, objUpdateScript, condition, options);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="updateScripts"></param>
        /// <param name="condition">查询条件</param>
        /// <param name="options">参数</param>
        public static void UpdateBulk(string providerName, string index, string type, UpdateScript updateScripts, string condition, OperationOptions options = null)
        {
            int pageIndex = 0;
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

                IList<string> idList = QueryID(providerName, index, type, condition, 1000, pageIndex, "_id", objSearchOption);
                if (idList.Count == 0)
                {
                    break;
                }
                else
                {
                    UpdateBulk(providerName, index, type, idList, updateScripts, options);
                }
                pageIndex++;

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="ids"></param>
        /// <param name="updateScripts"></param>
        /// <param name="options">参数</param>
        public static void UpdateBulk(string providerName, string index, string type, IEnumerable<string> ids, UpdateScript updateScripts, OperationOptions options = null)
        {
            var connection = CreateConnection(providerName);
            var bulkCommand = new BulkCommand(index, type);
            bulkCommand.OperationOptions(options);
            string bulkJson = "";
            bulkJson = new BulkBuilder(Serializer)
                                 .BuildCollection(ids,
                                 (builder, document) => builder.UpdateScirpt(updateScripts.ToJsonString(), index, type, document, options)
                                 );
            var result = connection.Post(bulkCommand, bulkJson);

        }

    }
}
