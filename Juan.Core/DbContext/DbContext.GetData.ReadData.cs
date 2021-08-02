using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Juan.Core
{
    public abstract partial class DbContext
    {
        #region 查询单条
        /// <summary>
        /// 读取单条数据
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public T Get<T>(ReadData readOptions) where T : class, new()
        {
            return ExecuteReader(CommandType.Text, ReadSqlDataOne(readOptions), readOptions.Parameters).ToList<T>().FirstOrDefault();
        }

        #endregion


        #region 查询列表
        /// <summary>
        /// 获取条数
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public long Count(ReadData readOptions)
        {
            object value = ExecuteScalar(ReadSqlCount(readOptions), readOptions.Parameters);
            if (value != null)
            {
                return Convert.ToInt64(value);
            }
            return -1;
        }






        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public List<T> GetList<T>(ReadData readOptions) where T : class, new()
        {
            return ExecuteReader(CommandType.Text, ReadSqlData(readOptions), readOptions.Parameters).ToList<T>();
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public DataTable GetTable(ReadData readOptions)
        {
            return ExecuteDataTable(CommandType.Text, ReadSqlData(readOptions), readOptions.Parameters);
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="readOptions"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetData(ReadData readOptions)
        {
            return ExecuteReader(CommandType.Text, ReadSqlData(readOptions), readOptions.Parameters).ToList();
        }
        #endregion


        #region 查询分页

        /// <summary>
        /// 读取分页
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public PageInfo<List<T>> GetPage<T>(ReadData readOptions) where T : class, new()
        {
            readOptions.PageSize.ArgumentNoNull("PageSize", "未设置页大小");
            readOptions.PageIndex.ArgumentNoNull("PageIndex", "未设置第几页");

            PageInfo<List<T>> objPageInfo = new PageInfo<List<T>>();
            objPageInfo.PageSize = readOptions.PageSize.Value;
            objPageInfo.PageIndex = readOptions.PageIndex.Value;
            DataSet objDataSet = ExecuteDataSet(CommandType.Text, ReadSqlPage(readOptions), readOptions.Parameters);
            objPageInfo.Data = objDataSet.Tables[0].ToList<T>();
            objPageInfo.RecordCount = Convert.ToInt64((objDataSet.Tables[1].Rows[0][0]));
            return objPageInfo;
        }


        /// <summary>
        /// 读取分页
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public PageInfo<DataTable> GetPageTable(ReadData readOptions)
        {
            PageInfo<DataTable> objPageInfo = new PageInfo<DataTable>();
            readOptions.PageSize.ArgumentNoNull("PageSize", "未设置页大小");
            readOptions.PageIndex.ArgumentNoNull("PageIndex", "未设置第几页");
            objPageInfo.PageSize = readOptions.PageSize.Value;
            objPageInfo.PageIndex = readOptions.PageIndex.Value;

            DataSet objDataSet = ExecuteDataSet(ReadSqlPage(readOptions), readOptions.Parameters);
            objPageInfo.Data = objDataSet.Tables[0];
            objPageInfo.RecordCount = Convert.ToInt64((objDataSet.Tables[1].Rows[0][0]));
            return objPageInfo;
        }
        /// <summary>
        /// 读取分页
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public PageInfo<List<Dictionary<string, object>>> GetPageData(ReadData readOptions)
        {
            PageInfo<List<Dictionary<string, object>>> objPageInfo = new PageInfo<List<Dictionary<string, object>>>();
            readOptions.PageSize.ArgumentNoNull("PageSize", "未设置页大小");
            readOptions.PageIndex.ArgumentNoNull("PageIndex", "未设置第几页");
            objPageInfo.PageSize = readOptions.PageSize.Value;
            objPageInfo.PageIndex = readOptions.PageIndex.Value;

            DataSet objDataSet = ExecuteDataSet(ReadSqlPage(readOptions), readOptions.Parameters);
            objPageInfo.Data = objDataSet.Tables[0].ToList();
            objPageInfo.RecordCount = Convert.ToInt64((objDataSet.Tables[1].Rows[0][0]));
            return objPageInfo;
        }

        #endregion


    }
}
