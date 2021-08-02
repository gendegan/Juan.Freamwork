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




        string _IndexName;

        string _TypeName;
        string _ProviderName;

        public string IndexName
        {
            get
            {
                return _IndexName;
            }
            set
            {
                _IndexName = value;
            }
        }

        public string TypeName
        {
            get
            {
                return _TypeName;
            }
            set
            {
                _TypeName = value;
            }
        }
        public ESQuery(string providerName, string indexName, string typeName)
        {
            if (string.IsNullOrEmpty(indexName))
            {
                throw new ArgumentNullException("参数indexName不能为空");
            }
            //if (string.IsNullOrEmpty(typeName))
            //{
            //    throw new ArgumentNullException("参数typeName不能为空");
            //}
            if (string.IsNullOrEmpty(providerName))
            {
                throw new ArgumentNullException("参数providerName不能为空");
            }

            _IndexName = indexName;
            _TypeName = typeName;
            _ProviderName = providerName;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objSearchOptions"></param>
        private void ParseSearchOptionInfo(SearchOptionInfo objSearchOptions)
        {
            if (objSearchOptions != null)
            {
                if (string.IsNullOrWhiteSpace(objSearchOptions.IndexName))
                {
                    objSearchOptions.IndexName = _IndexName;
                }
                if (string.IsNullOrWhiteSpace(objSearchOptions.TypeName))
                {
                    objSearchOptions.TypeName = _TypeName;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="providerName">提供者名称</param>
        /// <param name="sql">完整查询语句</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public string QueryResult(string providerName, string sql, SearchOptionInfo option = null)
        {
            return ESHelper.QueryResult(_ProviderName, sql, option);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">完整查询语句</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public QueryResult<T> QueryResult<T>(string sql, SearchOptionInfo option = null)
        {
            return ESHelper.QueryResult<T>(_ProviderName, sql, option);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="FieldT"></typeparam>
        /// <param name="sql">完整查询语句</param>
        /// <param name="option">参数</param>
        /// <returns></returns>
        public QueryResult<T, FieldT> QueryResult<T, FieldT>(string sql, SearchOptionInfo option = null)
        {
            return ESHelper.QueryResult<T, FieldT>(_ProviderName, sql, option);
        }


    }

}
