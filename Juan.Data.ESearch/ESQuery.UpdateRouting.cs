using PlainElastic7.Net.Serialization;
using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Data.ESearch
{
    public partial class ESQuery<T> where T : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isNoExistsAdd">不存在是否新增</param>
        /// <param name="id">主键</param>
        /// <param name="routing"></param>
        /// <param name="options">参数</param>
        public void UpdateRouting(T data, bool isNoExistsAdd = false, string id = null, string routing = null, OperationOptions options = null)
        {
            ESHelper.UpdateRouting<T>(_ProviderName, _IndexName, _TypeName, data, isNoExistsAdd, id, routing, options);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isNoExistsAdd">不存在是否新增</param>
        /// <param name="options">参数</param>
        public void UpdateBulkRouting(IEnumerable<T> data, bool isNoExistsAdd = false, OperationOptions options = null)
        {
            ESHelper.UpdateBulkRouting<T>(_ProviderName, _IndexName, _TypeName, data, isNoExistsAdd, options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routing"></param>
        /// <param name="id">主键</param>
        /// <param name="updateScript"></param>
        /// <param name="options">参数</param>
        public void UpdateRouting(string routing, string id, UpdateScript updateScript, OperationOptions options = null)
        {
            ESHelper.UpdateRouting(_ProviderName, _IndexName, _TypeName, routing, id, updateScript, options);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="routing"></param>
        /// <param name="id">主键</param>
        /// <param name="scriptiInline"></param>
        /// <param name="scirptParams"></param>
        /// <param name="options">参数</param>
        public void UpdateRouting(string routing, string id, string scriptiInline, Dictionary<string, object> scirptParams, OperationOptions options = null)
        {
            ESHelper.UpdateRouting(_ProviderName, _IndexName, _TypeName, routing, id, scriptiInline, scirptParams, options);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="scriptiInline"></param>
        /// <param name="scirptParams"></param>
        /// <param name="id">主键</param>
        /// <param name="routing"></param>
        /// <param name="options">参数</param>
        public void UpdateAddRouting(T data, string scriptiInline, Dictionary<string, object> scirptParams = null, string id = null, string routing = null, OperationOptions options = null)
        {
            ESHelper.UpdateAddRouting<T>(_ProviderName, _IndexName, _TypeName, data, scriptiInline, scirptParams, id, routing, options);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="scriptiInline"></param>
        /// <param name="isNoExistsAdd">不存在是否新增</param>
        /// <param name="options">参数</param>
        public void UpdateAddBulkRouting(IEnumerable<T> data, string scriptiInline, bool isNoExistsAdd = true, OperationOptions options = null)
        {
            ESHelper.UpdateAddBulkRouting<T>(_ProviderName, _IndexName, _TypeName, data, scriptiInline, isNoExistsAdd, options);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateScripts"></param>
        /// <param name="options">参数</param>
        public void UpdateAddBulkRouting(Dictionary<string, UpdateScript> updateScripts, OperationOptions options = null)
        {
            ESHelper.UpdateAddBulkRouting(_ProviderName, _IndexName, _TypeName, updateScripts, options);
        }
    }
}
