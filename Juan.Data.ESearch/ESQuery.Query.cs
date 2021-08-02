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
        /// <param name="condition">查询条件</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public long QueryCount(string condition, SearchOptionInfo option = null)
        {
            return ESHelper.QueryCount(_ProviderName, _IndexName, _TypeName, condition, option);
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="isDistinct"></param>
        /// <param name="fieldName"></param>
        /// <param name="condition">查询条件</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public long QueryCount(bool isDistinct, string fieldName, string condition, SearchOptionInfo option = null)
        {
            return ESHelper.QueryCount(_ProviderName, _IndexName, _TypeName, isDistinct, fieldName, condition, option);
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public T QueryRecord(string id, SearchOptionInfo option = null)
        {
            return ESHelper.QueryRecord<T>(_ProviderName, _IndexName, _TypeName, id, option);
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public T QueryRecord(string condition, string sortExpression, SearchOptionInfo option = null)
        {
            return ESHelper.Query<T>(_ProviderName, _IndexName, _TypeName, condition, sortExpression, "*", option).FirstOrDefault();
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql">完整查询语句</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public IList<T> Query(string sql, SearchOptionInfo option = null)
        {
            ParseSearchOptionInfo(option);
            return ESHelper.Query<T>(_ProviderName, sql, option);
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="fields">返回字段</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public IList<T> Query(string condition, string sortExpression, string fields = "*", SearchOptionInfo option = null)
        {

            return ESHelper.Query<T>(_ProviderName, _IndexName, _TypeName, condition, sortExpression, fields, option);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objReadOptions"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public IList<T> Query(ReadOptions objReadOptions, SearchOptionInfo option = null)
        {

            if (objReadOptions.PageSize != null && objReadOptions.PageIndex != null)
            {
                return ESHelper.Query<T>(_ProviderName, _IndexName, _TypeName, objReadOptions.MergeConditionES(), objReadOptions.SortExpression, objReadOptions.PageSize.Value, objReadOptions.PageIndex.Value, objReadOptions.Fields, option);
            }
            else
            {
                return ESHelper.Query<T>(_ProviderName, _IndexName, _TypeName, objReadOptions.MergeConditionES(), objReadOptions.SortExpression, objReadOptions.Fields, option);

            }
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="fields">返回字段</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public IList<T> Query(string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*", SearchOptionInfo option = null)
        {
            return ESHelper.Query<T>(_ProviderName, _IndexName, _TypeName, condition, sortExpression, pageSize, pageIndex, fields, option);
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql">完整查询语句</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="option">参数</param>
        /// <returns></returns>

        public IList<T> Query(string sql, int pageSize, int pageIndex, SearchOptionInfo option = null)
        {
            return ESHelper.Query<T>(_ProviderName, sql, pageSize, pageIndex, option);
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="fields">返回字段</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public PageInfo<List<T>> QueryPage(string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*", SearchOptionInfo option = null)
        {
            return ESHelper.QueryPage<T>(_ProviderName, _IndexName, _TypeName, condition, sortExpression, pageSize, pageIndex, fields, option);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="condition"></param>
        /// <param name="sortExpression"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="fields"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public PageInfo<List<V>> QueryPage<V>(string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*", SearchOptionInfo option = null)
        {
            return ESHelper.QueryPage<V>(_ProviderName, _IndexName, _TypeName, condition, sortExpression, pageSize, pageIndex, fields, option);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objReadOptions"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public PageInfo<List<T>> QueryPage(ReadOptions objReadOptions, SearchOptionInfo option = null)
        {

            return ESHelper.QueryPage<T>(_ProviderName, _IndexName, _TypeName, objReadOptions.MergeConditionES(), objReadOptions.SortExpression, objReadOptions.PageSize.Value, objReadOptions.PageIndex.Value, objReadOptions.Fields, option);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="objReadOptions"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public PageInfo<List<V>> QueryPage<V>(ReadOptions objReadOptions, SearchOptionInfo option = null)
        {

            return ESHelper.QueryPage<V>(_ProviderName, _IndexName, _TypeName, objReadOptions.MergeConditionES(), objReadOptions.SortExpression, objReadOptions.PageSize.Value, objReadOptions.PageIndex.Value, objReadOptions.Fields, option);
        }



        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql">完整查询语句</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public PageInfo<List<T>> QueryPage(string sql, int pageSize, int pageIndex, SearchOptionInfo option = null)
        {
            return ESHelper.QueryPage<T>(_ProviderName, sql, pageSize, pageIndex, option);

        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql">完整查询语句</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <returns></returns>
        public PageInfo<List<T>> QueryPageInfo(string sql, int pageSize, int pageIndex)
        {
            return ESHelper.QueryPage<T>(_ProviderName, sql, pageSize, pageIndex, null);

        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="offset">偏移量</param>
        /// <param name="limit">读取条数</param>
        /// <param name="fields">返回字段</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public IList<T> QueryLimit(string condition, string sortExpression, int offset, int limit, string fields = "*", SearchOptionInfo option = null)
        {

            return ESHelper.QueryLimit<T>(_ProviderName, _IndexName, _TypeName, condition, sortExpression, offset, limit, fields, option);

        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public IList<T> QueryLimit(string sql, int offset, int limit, SearchOptionInfo option = null)
        {

            return ESHelper.QueryLimit<T>(_ProviderName, sql, offset, limit, option);

        }
    }
}
