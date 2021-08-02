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
        /// <typeparam name="GroupT"></typeparam>
        /// <param name="sql">完整查询语句</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public IList<GroupT> QueryGroup<GroupT>(string sql, SearchOptionInfo option = null)
        {
            ParseSearchOptionInfo(option);
            return ESHelper.QueryGroup<GroupT>(_ProviderName, sql, option);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="GroupT"></typeparam>
        /// <param name="objReadOptions"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public IList<GroupT> QueryGroup<GroupT>(ReadOptions objReadOptions, SearchOptionInfo option = null)
        {
            if (objReadOptions.PageSize != null && objReadOptions.PageIndex != null)
            {
                return ESHelper.QueryGroup<GroupT>(_ProviderName, _IndexName, _TypeName, objReadOptions.MergeConditionES(), objReadOptions.SortExpression, objReadOptions.PageSize.Value, objReadOptions.PageIndex.Value, objReadOptions.Fields, option);
            }
            else
            {
                return ESHelper.QueryGroup<GroupT>(_ProviderName, _IndexName, _TypeName, objReadOptions.MergeConditionES(), objReadOptions.SortExpression, objReadOptions.Fields, option);

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql">完整查询语句</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public DataTable QueryGroupTable(string sql, SearchOptionInfo option = null)
        {
            ParseSearchOptionInfo(option);
            return ESHelper.QueryGroupTable(_ProviderName, sql, option);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objReadOptions"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public DataTable QueryGroupTable(ReadOptions objReadOptions, SearchOptionInfo option = null)
        {
            if (objReadOptions.PageSize != null && objReadOptions.PageIndex != null)
            {
                return ESHelper.QueryGroupTable(_ProviderName, _IndexName, _TypeName, objReadOptions.MergeConditionES(), objReadOptions.SortExpression, objReadOptions.PageSize.Value, objReadOptions.PageIndex.Value, objReadOptions.Fields, option);
            }
            else
            {
                return ESHelper.QueryGroupTable(_ProviderName, _IndexName, _TypeName, objReadOptions.MergeConditionES(), objReadOptions.SortExpression, objReadOptions.Fields, option);

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql">完整查询语句</param>
        /// <param name="limit">读取条数</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public DataTable QueryGroupTable(string sql, int limit, SearchOptionInfo option = null)
        {
            ParseSearchOptionInfo(option);
            return ESHelper.QueryGroupTable(_ProviderName, sql, limit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql">完整查询语句</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public string QueryGroupJson(string sql, SearchOptionInfo option = null)
        {
            ParseSearchOptionInfo(option);
            return ESHelper.QueryGroupJson(_ProviderName, sql);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objReadOptions"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public string QueryGroupJson(ReadOptions objReadOptions, SearchOptionInfo option = null)
        {
            if (objReadOptions.PageSize != null && objReadOptions.PageIndex != null)
            {
                return ESHelper.QueryGroupJson(_ProviderName, _IndexName, _TypeName, objReadOptions.MergeConditionES(), objReadOptions.SortExpression, objReadOptions.PageSize.Value, objReadOptions.PageIndex.Value, objReadOptions.Fields, option);
            }
            else
            {
                return ESHelper.QueryGroupJson(_ProviderName, _IndexName, _TypeName, objReadOptions.MergeConditionES(), objReadOptions.SortExpression, objReadOptions.Fields, option);

            }
        }



    }

}
