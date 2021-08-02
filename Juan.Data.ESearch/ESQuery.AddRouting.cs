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
       /// <param name="data"></param>
       /// <param name="isExistsUpdate"></param>
       /// <param name="id"></param>
       /// <param name="routing"></param>
       /// <param name="options"></param>
        public void AddRouting(T data, bool isExistsUpdate = false, string id = null, string routing = null, OperationOptions options = null)
        {
            ESHelper.AddRouting<T>(_ProviderName, _IndexName, _TypeName, data, isExistsUpdate, id, routing, options);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isExistsUpdate">存在是否更新</param>
        /// <param name="options">参数</param>
        public  void  AddBulkRouting(IEnumerable<T> data, bool isExistsUpdate = false, OperationOptions options = null)
        {
              ESHelper.AddBulkRouting<T>(_ProviderName, _IndexName, _TypeName, data, isExistsUpdate, options);

        }


    }
}
