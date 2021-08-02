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
     /// <param name="options"></param>
        public   void Add(T data, bool isExistsUpdate = false, string id = null,OperationOptions options = null)
        {
            ESHelper.Add<T>(_ProviderName, _IndexName, _TypeName, data, isExistsUpdate, id, options);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="isExistsUpdate">存在是否更新</param>
        /// <param name="options">参数</param>
        public void AddBulk(IEnumerable<T> data, bool isExistsUpdate = false, OperationOptions options = null)
        {

              ESHelper.AddBulk<T>(_ProviderName, _IndexName, _TypeName, data, isExistsUpdate, options);

        }


    }
}
