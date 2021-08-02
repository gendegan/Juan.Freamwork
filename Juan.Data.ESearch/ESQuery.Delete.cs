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
      /// <param name="id"></param>
      /// <param name="options"></param>
        public void DeleteID(string id, OperationOptions options = null)
        {
            ESHelper.DeleteID(_ProviderName, _IndexName, _TypeName, id, options);

        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="ids"></param>
       /// <param name="options"></param>
        public void Delete(IEnumerable<string> ids, OperationOptions options = null)
        {
            ESHelper.Delete(_ProviderName, _IndexName, _TypeName, ids);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="options">参数</param>
        public void Delete(string condition, OperationOptions options = null)
        {
            ESHelper.Delete(_ProviderName, _IndexName, _TypeName, condition, options);
        }


    }
}
