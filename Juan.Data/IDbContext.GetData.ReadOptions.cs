using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace Juan.Data
{
    public abstract partial class IDbContext<T, Key> where T : class, new()
    {
        #region 查询单个实例


        /// <summary>
        /// 读取单条数据
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public V Get<V>(ReadOptions readOptions) where V : class, new()
        {
            return Context.Get<V>(ToReadData(readOptions));
        }

        /// <summary>
        /// 读取单条数据
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public T Get(ReadOptions readOptions)
        {
            return Get<T>(readOptions);
        }


        /// <summary>
        /// 读取单条数据
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns></returns>
        public T Get(Key id)
        {
            return Get(ReadOptions.Search(ConditionParamKey(), PrimaryKey.PrimaryName.CreateParameter(id)));
        }
        /// <summary>
        /// 读取单条数据
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public V Get<V>(Key id) where V : class, new()
        {
            return Get<V>(ReadOptions.Search(ConditionParamKey(), PrimaryKey.PrimaryName.CreateParameter(id)));
        }

        #endregion



        #region 查询列表

        /// <summary>
        /// 查询数据条数
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public long Count(ReadOptions readOptions)
        {
            return Context.Count(ToReadData(readOptions));
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="ids">主键列表</param>
        /// <param name="sortExpression">排序表达式</param>
        /// <returns></returns>
        public List<T> GetList(IEnumerable<Key> ids, string sortExpression = "")
        {
            if (ids.IsNull())
            {
                return new List<T>();
            }

            return GetList(ReadOptions.Search(Condition(ids), sortExpression));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="ids"></param>
        /// <param name="sortExpression"></param>
        /// <returns></returns>
        public List<V> GetList<V>(IEnumerable<Key> ids, string sortExpression = "") where V : class, new()
        {
            if (ids.IsNull())
            {
                return new List<V>();
            }
            return GetList<V>(ReadOptions.Search(Condition(ids), sortExpression));
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="idString">主键字符串如1,2,3</param>
        /// <param name="sortExpression">排序表达式</param>
        /// <returns></returns>
        public List<T> GetListID(string idString, string sortExpression = "")
        {
            if (idString.IsNullOrWhiteSpace())
            {
                return new List<T>();
            }
            return GetList(ReadOptions.Search(ConditionID(idString), sortExpression));
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public List<T> GetList(ReadOptions readOptions)
        {
            return GetList<T>(readOptions);
        }
        /// <summary>
        /// 读取全部数据
        /// </summary>
        /// <returns></returns>
        public List<T> GetList()
        {
            return GetList<T>(new ReadOptions());
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public List<V> GetList<V>(ReadOptions readOptions) where V : class, new()
        {
            return Context.GetList<V>(ToReadData(readOptions));
        }


        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="ids">主键列表</param>
        /// <param name="sortExpression">排序表达式</param>
        /// <returns></returns>
        public DataTable GetTable(IEnumerable<Key> ids, string sortExpression = "")
        {
         
            return GetTable(ReadOptions.Search(Condition(ids), sortExpression));
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="idString">主键字符串如1,2,3</param>
        /// <param name="sortExpression">排序表达式</param>
        /// <returns></returns>
        public DataTable GetTableID(string idString, string sortExpression = "")
        {
            return GetTable(ReadOptions.Search(ConditionID(idString), sortExpression));
        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public DataTable GetTable(ReadOptions readOptions)
        {
            return Context.GetTable(ToReadData(readOptions));

        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetTable()
        {
            return GetTable(new ReadOptions());

        }
        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="readOptions"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetData(ReadOptions readOptions)
        {
            return Context.GetData(ToReadData(readOptions));
        }
        #endregion


        #region 查询分页

        /// <summary>
        /// 读取分页
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public PageInfo<List<T>> GetPage(ReadOptions readOptions)
        {
            return GetPage<T>(readOptions);
        }
        /// <summary>
        /// 读取分页
        /// </summary>
        /// <typeparam name="V"></typeparam>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public PageInfo<List<V>> GetPage<V>(ReadOptions readOptions) where V : class, new()
        {
            return Context.GetPage<V>(ToReadData(readOptions));
        }
        /// <summary>
        /// 读取分页
        /// </summary>
        /// <param name="readOptions">读取选项</param>
        /// <returns></returns>
        public PageInfo<DataTable> GetPageTable(ReadOptions readOptions)
        {
            return Context.GetPageTable(ToReadData(readOptions));
        }
        /// <summary>
        /// 读取分页
        /// </summary>
        /// <param name="readOptions"></param>
        /// <returns></returns>
        public PageInfo<List<Dictionary<string, object>>> GetPageData(ReadOptions readOptions)
        {
            return Context.GetPageData(ToReadData(readOptions));
        }

        #endregion



    }
}
