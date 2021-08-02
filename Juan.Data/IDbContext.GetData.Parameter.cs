using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Juan.Data
{
    public abstract partial class IDbContext<T, Key> where T : class,new()
    {
        #region 查询单个实例


        /// <summary>
        /// 读取单条数据
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public V Get<V>(string condition, DataParameter[] parms, string sortExpression = "", string fields = "*") where V : class,new()
        {
            return this.Get<V>(new ReadOptions()
            {
                Condition = condition,
                SortExpression = sortExpression,
                Parameters = parms,
                Fields = fields
            });
        }

        /// <summary>
        /// 读取单条数据
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public T Get(string condition, DataParameter[] parms, string sortExpression = "", string fields = "*")
        {
            return this.Get<T>(condition, parms, sortExpression, fields);
        }



        #endregion



        #region 查询列表

        /// <summary>
        ///  查询数据条数
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <returns></returns>
        public long Count(string condition, params DataParameter[] parms)
        {
            return this.Count(new ReadOptions()
           {
               Condition = condition,
               Parameters = parms,
           });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public List<T> GetList(string condition, DataParameter[] parms, string sortExpression = "", string fields = "*")
        {
            return GetList<T>(condition, parms, sortExpression, fields);
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public List<V> GetList<V>(string condition, DataParameter[] parms, string sortExpression = "", string fields = "*") where V : class,new()
        {
            return this.GetList<V>(new ReadOptions()
            {
                Condition = condition,
                SortExpression = sortExpression,
                Parameters = parms,
                Fields = fields
            });
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public List<T> GetList(string condition, DataParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return GetList<T>(condition, parms, sortExpression, pageSize, pageIndex, fields);
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public List<V> GetList<V>(string condition, DataParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*") where V : class,new()
        {
            return this.GetList<V>(new ReadOptions()
            {
                Condition = condition,
                PageSize = pageSize,
                PageIndex = pageIndex,
                SortExpression = sortExpression,
                Parameters = parms,
                Fields = fields
            });
        }




        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public DataTable GetTable(string condition, DataParameter[] parms, string sortExpression = "", string fields = "*")
        {
            return this.GetTable(new ReadOptions()
            {
                Condition = condition,
                SortExpression = sortExpression,
                Parameters = parms,
                Fields = fields
            });

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public DataTable GetTable(string condition, DataParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
       
            return this.GetTable(new ReadOptions()
            {
                Condition = condition,
                PageSize = pageSize,
                PageIndex = pageIndex,
                SortExpression = sortExpression,
                Parameters = parms,
                Fields = fields
            });

        }

        #endregion


        #region 查询分页

        /// <summary>
        /// 读取分页
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public PageInfo<List<T>> GetPage(string condition, DataParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return GetPage<T>(condition, parms, sortExpression, pageSize, pageIndex, fields);
        }


        /// <summary>
        /// 读取分页
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public PageInfo<List<V>> GetPage<V>(string condition, DataParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*") where V : class,new()
        {
            return this.GetPage<V>(new ReadOptions()
            {
                Condition = condition,
                SortExpression = sortExpression,
                Parameters = parms,
                Fields = fields,
                PageSize = pageSize,
                PageIndex = pageIndex
            });
        }

        /// <summary>
        /// 读取分页
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public PageInfo<DataTable> GetPageTable(string condition, DataParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return this.GetPageTable(new ReadOptions()
            {
                Condition = condition,
                SortExpression = sortExpression,
                Parameters = parms,
                Fields = fields,
                PageSize = pageSize,
                PageIndex = pageIndex
            });
        }



        #endregion



    }
}
