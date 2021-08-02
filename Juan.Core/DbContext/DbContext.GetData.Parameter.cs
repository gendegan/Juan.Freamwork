
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Juan.Core
{
    public abstract partial class DbContext
    {
        #region 查询单个实例


        /// <summary>
        /// 读取单条数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName">表名</param>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public T Get<T>(string tableName, string condition, DataParameter[] parms, string sortExpression = "", string fields = "*") where T : class,new()
        {
            return this.Get<T>(new ReadData()
            {
                TableName = tableName,
                Condition = condition,
                SortExpression = sortExpression,
                Parameters = parms,
                Fields = fields
            });
        }

        #endregion



        #region 查询列表

        /// <summary>
        ///  查询数据条数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <returns></returns>
        public long Count(string tableName, string condition, params DataParameter[] parms)
        {
            return this.Count(new ReadData()
            {
                TableName = tableName,
                Condition = condition,
                Parameters = parms,
            });
        }


        /// <summary>
        /// 读取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName">表名</param>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public List<T> GetList<T>(string tableName, string condition, DataParameter[] parms, string sortExpression = "", string fields = "*") where T : class,new()
        {
            return this.GetList<T>(new ReadData()
            {
                TableName = tableName,
                Condition = condition,
                SortExpression = sortExpression,
                Parameters = parms,
                Fields = fields
            });
        }


        /// <summary>
        /// 读取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName">表名</param>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public List<T> GetList<T>(string tableName, string condition, DataParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*") where T : class,new()
        {
            return this.GetList<T>(new ReadData()
            {
                TableName = tableName,
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
        /// <param name="tableName">表名</param>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public DataTable GetTable(string tableName, string condition, DataParameter[] parms, string sortExpression = "", string fields = "*")
        {
            return this.GetTable(new ReadData()
            {
                TableName = tableName,
                Condition = condition,
                SortExpression = sortExpression,
                Parameters = parms,
                Fields = fields
            });

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public DataTable GetTable(string tableName, string condition, DataParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return this.GetTable(new ReadData()
            {
                TableName = tableName,
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
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName">表名</param>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public PageInfo<List<T>> GetPage<T>(string tableName, string condition, DataParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*") where T : class,new()
        {
            return this.GetPage<T>(new ReadData()
            {
                TableName = tableName,
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
        /// <param name="tableName">表名</param>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public PageInfo<DataTable> GetPageTable(string tableName, string condition, DataParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return this.GetPageTable(new ReadData()
            {
                TableName = tableName,
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
