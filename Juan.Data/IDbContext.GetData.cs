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
        /// <param name="sortExpression">排序</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public V Get<V>(string condition, string sortExpression = "", string fields = "*") where V : class,new()
        {
            return this.Get<V>(new ReadOptions()
            {
                Condition = condition,
                SortExpression = sortExpression,
                Fields = fields
            });
        }

        /// <summary>
        /// 读取单条数据
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public T Get(string condition, string sortExpression = "", string fields = "*")
        {
            return this.Get<T>(condition, sortExpression, fields);
        }



        #endregion



        #region 查询列表



        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public List<T> GetList(string condition, string sortExpression = "", string fields = "*")
        {
            return GetList<T>(condition, sortExpression, fields);
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="condition">条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public List<V> GetList<V>(string condition, string sortExpression = "", string fields = "*") where V : class,new()
        {
            return this.GetList<V>(new ReadOptions()
            {
                Condition = condition,
                SortExpression = sortExpression,

                Fields = fields
            });
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public List<T> GetList(string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return GetList<T>(condition, sortExpression, pageSize, pageIndex, fields);
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="condition">条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public List<V> GetList<V>(string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*") where V : class,new()
        {
            return this.GetList<V>(new ReadOptions()
            {
                Condition = condition,
                PageSize = pageSize,
                PageIndex = pageIndex,
                SortExpression = sortExpression,
                Fields = fields
            });
        }




        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public DataTable GetTable(string condition, string sortExpression = "", string fields = "*")
        {
            return this.GetTable(new ReadOptions()
            {
                Condition = condition,
                SortExpression = sortExpression,

                Fields = fields
            });

        }
        /// <summary>
        /// /// 读取数据
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public DataTable GetTable(string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return this.GetTable(new ReadOptions()
            {
                Condition = condition,
                PageSize = pageSize,
                PageIndex = pageIndex,
                SortExpression = sortExpression,

                Fields = fields
            });

        }

        #endregion


        #region 查询分页

        /// <summary>
        /// 读取分页
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public PageInfo<List<T>> GetPage(string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return GetPage<T>(condition, sortExpression, pageSize, pageIndex, fields);
        }


        /// <summary>
        /// 读取分页
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="condition">条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public PageInfo<List<V>> GetPage<V>(string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*") where V : class,new()
        {
            return this.GetPage<V>(new ReadOptions()
            {
                Condition = condition,
                SortExpression = sortExpression,

                Fields = fields,
                PageSize = pageSize,
                PageIndex = pageIndex
            });
        }

        /// <summary>
        /// 读取分页
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public PageInfo<DataTable> GetPageTable(string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return this.GetPageTable(new ReadOptions()
            {
                Condition = condition,
                SortExpression = sortExpression,
                Fields = fields,
                PageSize = pageSize,
                PageIndex = pageIndex
            });
        }



        #endregion



    }
}
