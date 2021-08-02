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
        /// <param name="options">参数</param>
        public void Update(T data, bool isNoExistsAdd = false, string id = null, OperationOptions options = null)
        {
            ESHelper.Update<T>(_ProviderName, _IndexName, _TypeName, data, isNoExistsAdd, id, options);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isNoExistsAdd">不存在是否新增</param>
        /// <param name="options">参数</param>
        public void UpdateBulk(IEnumerable<T> data, bool isNoExistsAdd = false, OperationOptions options = null)
        {
            ESHelper.UpdateBulk<T>(_ProviderName, _IndexName, _TypeName, data, isNoExistsAdd, options);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="updateScript"></param>
        /// <param name="options">参数</param>
        public void Update(string id, UpdateScript updateScript, OperationOptions options = null)
        {
            ESHelper.Update(_ProviderName, _IndexName, _TypeName, id, updateScript, options);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="scriptiInline"></param>
        /// <param name="scirptParams"></param>
        /// <param name="options">参数</param>
        public void Update(string id, string scriptiInline, Dictionary<string, object> scirptParams, OperationOptions options = null)
        {
            ESHelper.Update(_ProviderName, _IndexName, _TypeName, id, scriptiInline, scirptParams, options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fields"></param>
        /// <param name="isExclude"></param>
        /// <param name="id"></param>
        /// <param name="options"></param>
        public void UpdateAddFields(T data, string fields, bool isExclude = false, string id = null, OperationOptions options = null)
        {
            ESHelper.UpdateAddFields<T>(_ProviderName, _IndexName, _TypeName, data, fields, isExclude, id, options);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fields"></param>
        /// <param name="isExclude"></param>
        /// <param name="options"></param>

        public void UpdateAddFieldsBulk(IEnumerable<T> data, string fields, bool isExclude = false, OperationOptions options = null)
        {
            ESHelper.UpdateAddFieldsBulk<T>(_ProviderName, _IndexName, _TypeName, data, fields, isExclude, options);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="scriptiInline"></param>
        /// <param name="scirptParams"></param>
        /// <param name="id">主键</param>
        /// <param name="options">参数</param>
        public void UpdateAdd(T data, string scriptiInline, Dictionary<string, object> scirptParams = null, string id = null, OperationOptions options = null)
        {
            ESHelper.UpdateAdd<T>(_ProviderName, _IndexName, _TypeName, data, scriptiInline, scirptParams, id, options);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="scriptiInline"></param>
        /// <param name="isNoExistsAdd">不存在是否新增</param>
        /// <param name="options">参数</param>
        public void UpdateAddBulk(IEnumerable<T> data, string scriptiInline, bool isNoExistsAdd = true, OperationOptions options = null)
        {
            ESHelper.UpdateAddBulk<T>(_ProviderName, _IndexName, _TypeName, data, scriptiInline, isNoExistsAdd, options);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateScripts"></param>
        /// <param name="options">参数</param>
        public void UpdateAddBulk(Dictionary<string, UpdateScript> updateScripts, OperationOptions options = null)
        {
            ESHelper.UpdateAddBulk(_ProviderName, _IndexName, _TypeName, updateScripts, options);
        }
    }
}
