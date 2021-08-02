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
        /// <param name="providerName"></param>
        /// <returns></returns>
        public static string AllInfo(string providerName)
        {

            var connection = CreateConnection(providerName);
            var result = connection.Get("_all?pretty=true");
            return result.Result;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="shardsNumber"></param>
        /// <param name="replicasNumber"></param>
        /// <returns></returns>
        public static bool CreateIndex(string providerName, string index, int shardsNumber = 5, int replicasNumber = 1)
        {
            if (string.IsNullOrWhiteSpace(index))
            {
                throw new ArgumentNullException("参数index不能为空");
            }
            var indexSettings = new IndexSettingsBuilder()
                   .NumberOfShards(shardsNumber)
                   .NumberOfReplicas(replicasNumber);
            return CreateIndex(providerName, new IndexCommand(index), indexSettings);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="indexCommand"></param>
        /// <param name="indexSettings"></param>
        /// <returns></returns>
        public static bool CreateIndex(string providerName, IndexCommand indexCommand, IndexSettingsBuilder indexSettings)
        {
            try
            {
                var connection = CreateConnection(providerName);
                var result = connection.Put(indexCommand, indexSettings.Build());
                return Serializer.ToCommandResult(result).acknowledged;
            }
            catch (OperationException ex)
            {
                if (ex.HttpStatusCode == 404 || ex.Message.Contains("already"))
                    return false;
                throw;
            }
        }

        //public static bool IndexDelete(string providerName, string indexName)
        //{

        //    var connection = CreateConnection(providerName);
        //    var result = connection.Delete(new IndexCommand(indexName));
        //    return Serializer.ToCommandResult(result).acknowledged;

        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <returns></returns>
        public static bool IndexExists(string providerName, string index)
        {
            if (string.IsNullOrWhiteSpace(index))
            {
                throw new ArgumentNullException("参数index不能为空");
            }

            var connection = CreateConnection(providerName);
            string indexExistsCommand = Commands.IndexExists(index);
            try
            {
                connection.Head(indexExistsCommand);
                return true;
            }
            catch (OperationException ex)
            {
                if (ex.HttpStatusCode == 404)
                    return false;
                throw;
            }
        }

        public static bool TypeExists(string providerName, string index, string type)
        {
            if (string.IsNullOrWhiteSpace(index))
            {
                throw new ArgumentNullException("参数index不能为空");
            }

            var connection = CreateConnection(providerName);
            string indexExistsCommand = Commands.Index(index, type);
            try
            {
                connection.Head(indexExistsCommand);
                return true;
            }
            catch (OperationException ex)
            {
                if (ex.HttpStatusCode == 404)
                    return false;
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="operatorType"></param>
        /// <param name="index">索引名称</param>
        /// <param name="alias"></param>
        public static void Alias(string providerName, OperatorType operatorType, string index, string alias)
        {
            AliasesActions objAliasesActions = new AliasesActions();
            objAliasesActions.Add(operatorType, index, alias);
            Alias(providerName, objAliasesActions);
        }
        /// <summary>
        /// 别名转移
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="sIndex"></param>
        /// <param name="sAlias"></param>
        /// <param name="tIndex"></param>
        public static void AliasTransfer(string providerName, string sIndex, string sAlias, string tIndex)
        {
            AliasesActions objAliasesActions = new AliasesActions();
            objAliasesActions.Add(OperatorType.remove, sIndex, sAlias);
            objAliasesActions.Add(OperatorType.add, tIndex, sAlias);
            Alias(providerName, objAliasesActions);
        }
        public static void Alias(string providerName, AliasesActions aliasesActions)
        {
            var connection = CreateConnection(providerName);
            string result = connection.Post("_aliases", aliasesActions.JsonSerialize());

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="replicasNumber"></param>
        /// <returns></returns>
        public static bool IndexReplicas(string providerName, string index, int replicasNumber)
        {
            return IndexSettings(providerName, new UpdateSettingsCommand(index), new IndexSettingsBuilder()
                                          .NumberOfReplicas(replicasNumber)
                                          .Build());
        }
        public static string GetMapping(string providerName, string index, string type)
        {
            var connection = CreateConnection(providerName);
            return connection.Get(Commands.GetMapping(index, type).Pretty());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="updateSettingsCommand"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        private static bool IndexSettings(string providerName, UpdateSettingsCommand updateSettingsCommand, string settings)
        {
            var connection = CreateConnection(providerName);
            string result = connection.Put(updateSettingsCommand, settings);
            return Serializer.ToCommandResult(result).acknowledged;
        }


    }
}
