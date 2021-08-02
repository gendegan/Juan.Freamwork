using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    /// <summary>
    /// 读取参数
    /// </summary>
    public partial class ReadOptions
    {


        /// <summary>
        /// 创建分页查询条件
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="fields">返回字段</param>
        /// <returns></returns>
        public static ReadOptions Page(string condition, DataParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return new ReadOptions()
            {
                Condition = condition,
                SortExpression = sortExpression,
                Parameters = parms,
                Fields = fields,
                PageSize = pageSize,
                PageIndex = pageIndex
            };
        }
        /// <summary>
        /// 创建分页查询条件
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="fields">返回字段</param>
        /// <returns></returns>
        public static ReadOptions Page(string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return new ReadOptions()
            {
                Condition = condition,
                SortExpression = sortExpression,
                Fields = fields,
                PageSize = pageSize,
                PageIndex = pageIndex
            };
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="fields">返回字段</param>
        /// <returns></returns>
        public static ReadOptions Search(string condition, DataParameter[] parms, string sortExpression, string fields = "*")
        {
            return new ReadOptions()
            {
                Condition = condition,
                SortExpression = sortExpression,
                Parameters = parms,
                Fields = fields
            };
        }
        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <returns></returns>
        public static ReadOptions Search(string condition = "", params DataParameter[] parms)
        {
            return new ReadOptions()
            {
                Condition = condition,
                Parameters = parms,
            };
        }
        /// <summary>
        /// 创健实例
        /// </summary>
        /// <returns></returns>
        public static ReadOptions Create()
        {
            return new ReadOptions();

        }
        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="fields">返回字段</param>
        /// <returns></returns>
        public static ReadOptions Search(string condition, string sortExpression, string fields = "*")
        {
            return new ReadOptions()
            {
                Condition = condition,
                SortExpression = sortExpression,
                Fields = fields
            };
        }
        /// <summary>
        /// Limit条件
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="offset">偏移量</param>
        /// <param name="limit">返回条数</param>
        /// <param name="fields">返回字段</param>
        /// <returns></returns>
        public static ReadOptions Limit(string condition, DataParameter[] parms, string sortExpression, int offset, int limit, string fields = "*")
        {
            return new ReadOptions()
             {
                 Condition = condition,
                 SortExpression = sortExpression,
                 Parameters = parms,
                 Fields = fields,
                 Skip = offset,
                 Take = limit,

             };

        }
        /// <summary>
        /// Limit条件
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="offset">偏移量</param>
        /// <param name="limit">返回条数</param>
        /// <param name="fields">返回字段</param>
        /// <returns></returns>
        public static ReadOptions Limit(string condition, string sortExpression, int offset, int limit, string fields = "*")
        {
            return new ReadOptions()
            {
                Condition = condition,
                SortExpression = sortExpression,
                Fields = fields,
                Skip = offset,
                Take = limit
            };
        }

    }

    /// <summary>
    /// 读取选项帮助类
    /// </summary>
    public static class ReadOptionsHelper
    {
        /// <summary>
        /// 设置表名
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static ReadOptions SetTableName(this ReadOptions readOptions, string tableName)
        {
            readOptions.TableName = tableName;
            return readOptions;
        }
        /// <summary>
        /// 设置执行命令
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <param name="commandText">执行命令</param>
        /// <returns></returns>
        public static ReadOptions SetCommandText(this ReadOptions readOptions, string commandText)
        {
            readOptions.CommandText = commandText;
            return readOptions;
        }
        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <param name="dataParms"></param>
        /// <returns></returns>
        public static ReadOptions SetParameter(this ReadOptions readOptions, params DataParameter[] dataParms)
        {
            readOptions.Parameters = readOptions.Parameters.MergeParameter(dataParms);
            return readOptions;
        }
        /// <summary>
        /// 设置查询视图
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public static ReadOptions SetIsView(this ReadOptions readOptions)
        {
            readOptions.IsView = true;
            return readOptions;
        }

    }
}
