using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


using Juan.Core;
using System.Reflection;


namespace Juan.Data.ESearch
{
    public partial class ESQuery<T> where T : class, new()
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public IList<string> QueryID(string condition, string sortExpression = "", SearchOptionInfo option = null)
        {
            return ESHelper.QueryID(_ProviderName, _IndexName, _TypeName, condition, sortExpression, option);

        }

        /// <summary>
        /// 查询字段
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public List<HitsInfo<object, object>> QueryIDInfo(string condition, string sortExpression = "", SearchOptionInfo option = null)
        {
            return ESHelper.QueryIDInfo(_ProviderName, _IndexName, _TypeName, condition, sortExpression, option);

        }
        /// <summary>
        /// 查询字段
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public IList<string> QueryID(string condition, int pageSize, int pageIndex, string sortExpression = "", SearchOptionInfo option = null)
        {
            return ESHelper.QueryID(_ProviderName, _IndexName, _TypeName, condition, pageSize, pageIndex, sortExpression, option);
        }
        /// <summary>
        /// 查询字段
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public List<HitsInfo<object, object>> QueryIDInfo(string condition, int pageSize, int pageIndex, string sortExpression = "", SearchOptionInfo option = null)
        {
            return ESHelper.QueryIDInfo(_ProviderName, _IndexName, _TypeName, condition, pageSize, pageIndex, sortExpression, option);
        }
        /// <summary>
        /// 查询字段
        /// </summary>
        /// <typeparam name="FieldKey"></typeparam>
        /// <param name="fieldName"></param>
        /// <param name="condition">查询条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public IList<FieldKey> QueryField<FieldKey>(string fieldName, string condition, string sortExpression = "", SearchOptionInfo option = null)
        {
            return ESHelper.QueryField<FieldKey>(_ProviderName, _IndexName, _TypeName, fieldName, condition, sortExpression, option);
        }
        /// <summary>
        /// 查询字段
        /// </summary>
        /// <typeparam name="FieldKey"></typeparam>
        /// <param name="fieldName"></param>
        /// <param name="condition">查询条件</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public IList<FieldKey> QueryField<FieldKey>(string fieldName, string condition, int pageSize, int pageIndex, string sortExpression = "", SearchOptionInfo option = null)
        {
            return ESHelper.QueryField<FieldKey>(_ProviderName, _IndexName, _TypeName, fieldName, condition, pageSize, pageIndex, sortExpression, option);
        }





    }

}
