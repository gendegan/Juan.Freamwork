using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Juan.Data.ESearch
{
    public partial class ESQuery<T> where T : class, new()
    {

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="sortExpression"></param>
        /// <param name="fields"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public DataTable QueryTable(string condition, string sortExpression, string fields = "*", SearchOptionInfo option = null)
        {

            return ESHelper.QueryTable(_ProviderName, _IndexName, _TypeName, condition, sortExpression, fields, option);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objReadOptions"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public DataTable QueryTable(ReadOptions objReadOptions, SearchOptionInfo option = null)
        {
            if (objReadOptions.PageSize != null && objReadOptions.PageIndex != null)
            {
                return ESHelper.QueryTable(_ProviderName, _IndexName, _TypeName, objReadOptions.MergeConditionES(), objReadOptions.SortExpression, objReadOptions.PageSize.Value, objReadOptions.PageIndex.Value, objReadOptions.Fields, option);
            }
            else
            {
                return ESHelper.QueryTable(_ProviderName, _IndexName, _TypeName, objReadOptions.MergeConditionES(), objReadOptions.SortExpression, objReadOptions.Fields, option);

            }
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public DataTable QueryTable(string sql, SearchOptionInfo option = null)
        {

            return ESHelper.QueryTable(_ProviderName, sql, option);

        }
        ///
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="sortExpression"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="fields"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public DataTable QueryTable(string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*", SearchOptionInfo option = null)
        {

            return ESHelper.QueryTable(_ProviderName, _IndexName, _TypeName, condition, sortExpression, pageSize, pageIndex, fields, option);
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="option"></param>
        /// <returns></returns>

        public DataTable QueryTable(string sql, int pageSize, int pageIndex, SearchOptionInfo option = null)
        {

            return ESHelper.QueryTable(_ProviderName, sql, pageSize, pageIndex, option);
        }



        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="sortExpression"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="fields"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public PageInfo<DataTable> QueryPageTable(string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*", SearchOptionInfo option = null)
        {
            return ESHelper.QueryPageTable(_ProviderName, _IndexName, _TypeName, condition, sortExpression, pageSize, pageIndex, fields, option);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objReadOptions"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public PageInfo<DataTable> QueryPageTable(ReadOptions objReadOptions, SearchOptionInfo option = null)
        {

            return ESHelper.QueryPageTable(_ProviderName, _IndexName, _TypeName, objReadOptions.MergeConditionES(), objReadOptions.SortExpression, objReadOptions.PageSize.Value, objReadOptions.PageIndex.Value, objReadOptions.Fields, option);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="sortExpression"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public PageInfo<DataTable> QueryPageTableInfo(string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return ESHelper.QueryPageTable(_ProviderName, _IndexName, _TypeName, condition, sortExpression, pageSize, pageIndex, fields, null);

        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public PageInfo<DataTable> QueryPageTable(string sql, int pageSize, int pageIndex, SearchOptionInfo option = null)
        {
            return ESHelper.QueryPageTable(_ProviderName, sql, pageSize, pageIndex, option);

        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>

        public PageInfo<DataTable> QueryPageTableInfo(string sql, int pageSize, int pageIndex)
        {
            return ESHelper.QueryPageTable(_ProviderName, sql, pageSize, pageIndex, null);

        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="sortExpression"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="fields"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public DataTable QueryLimitTable(string condition, string sortExpression, int offset, int limit, string fields = "*", SearchOptionInfo option = null)
        {

            return ESHelper.QueryLimitTable(_ProviderName, _IndexName, _TypeName, condition, sortExpression, offset, limit, fields, option);

        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public DataTable QueryLimitTable(string sql, int offset, int limit, SearchOptionInfo option = null)
        {

            return ESHelper.QueryLimitTable(_ProviderName, sql, offset, limit, option);

        }
    }
}
