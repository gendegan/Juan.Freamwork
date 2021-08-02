namespace Juan.Core
{
    /// <summary>
    /// 读取参数
    /// </summary>
    public partial class ReadData
    {

        /// <summary>
        /// 创健实例
        /// </summary>
        /// <returns></returns>
        public static ReadData Create()
        {
            return new ReadData();

        }
        /// <summary>
        /// 创建分页查询条件
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="fields">返回字段</param>
        /// <returns></returns>
        public static ReadData Page(string tableName, string condition, DataParameter[] parms, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return new ReadData()
            {
                TableName = tableName,
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
        /// <param name="tableName">表名</param>
        /// <param name="condition">条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="fields">返回字段</param>
        /// <returns></returns>
        public static ReadData Page(string tableName, string condition, string sortExpression, int pageSize, int pageIndex, string fields = "*")
        {
            return new ReadData()
            {
                TableName = tableName,
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
        /// <param name="tableName">表名</param>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="fields">返回字段</param>
        /// <returns></returns>
        public static ReadData Search(string tableName, string condition, DataParameter[] parms, string sortExpression, string fields = "*")
        {
            return new ReadData()
            {
                TableName = tableName,
                Condition = condition,
                SortExpression = sortExpression,
                Parameters = parms,
                Fields = fields
            };
        }
        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <returns></returns>
        public static ReadData Search(string tableName, string condition = "", params DataParameter[] parms)
        {
            return new ReadData()
            {
                TableName = tableName,
                Condition = condition,
                Parameters = parms,
            };
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="condition">条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="fields">返回字段</param>
        /// <returns></returns>
        public static ReadData Search(string tableName, string condition, string sortExpression, string fields = "*")
        {
            return new ReadData()
            {
                TableName = tableName,
                Condition = condition,
                SortExpression = sortExpression,
                Fields = fields
            };
        }
        /// <summary>
        /// Limit条件
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="condition">条件</param>
        /// <param name="parms">参数</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="offset">偏移量</param>
        /// <param name="limit">返回条数</param>
        /// <param name="fields">返回字段</param>
        /// <returns></returns>
        public static ReadData Limit(string tableName, string condition, DataParameter[] parms, string sortExpression, int offset, int limit, string fields = "*")
        {
            return new ReadData()
            {
                TableName = tableName,
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
        /// <param name="tableName">表名</param>
        /// <param name="condition">条件</param>
        /// <param name="sortExpression">排序</param>
        /// <param name="offset">偏移量</param>
        /// <param name="limit">返回条数</param>
        /// <param name="fields">返回字段</param>
        /// <returns></returns>
        public static ReadData Limit(string tableName, string condition, string sortExpression, int offset, int limit, string fields = "*")
        {
            return new ReadData()
            {
                TableName = tableName,
                Condition = condition,
                SortExpression = sortExpression,
                Fields = fields,
                Skip = offset,
                Take = limit
            };
        }

    }
}