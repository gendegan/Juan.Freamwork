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
        /// <param name="routing"></param>
        /// <param name="id">主键</param>
        public void DeleteRoutingID(string routing, string id, OperationOptions options = null)
        {
            ESHelper.DeleteRoutingID(_ProviderName, _IndexName, _TypeName, routing, id, options);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="index">索引名称</param>
        /// <param name="type">类型名称</param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public void DeleteRouting(string routing, IEnumerable<string> ids, OperationOptions options = null)
        {
            ESHelper.DeleteRouting(_ProviderName, _IndexName, _TypeName, routing, ids, options);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DeleteData"></param>
        /// <param name="options">参数</param>
        public void DeleteRouting(IEnumerable<RoutingDelete> DeleteData, OperationOptions options = null)
        {
            ESHelper.DeleteRouting(_ProviderName, _IndexName, _TypeName, DeleteData, options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routing"></param>
        /// <param name="condition">查询条件</param>
        /// <param name="options">参数</param>
        public void DeleteRouting(string routing, string condition, OperationOptions options = null)
        {
            ESHelper.DeleteRouting(_ProviderName, _IndexName, _TypeName, routing, condition, options);
        }


    }
}
