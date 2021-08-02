using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Data.ESearch
{
    [Serializable]
    /// <summary>
    /// 查询无结果
    /// </summary>
    public class QueryResult
    {
        public QueryResult()
        {
            took = 0;
            timed_out = false;
            _shards = new Shards();
            hits = new NullHits();
        }
        /// <summary>
        /// 执行时间
        /// </summary>
        public int took
        {
            get;
            set;
        }
        /// <summary>
        /// 是否查询超时
        /// </summary>
        public bool timed_out
        {
            get;
            set;
        }
        /// <summary>
        /// 查询返回分片信息
        /// </summary>
        public Shards _shards
        {
            get;
            set;
        }
        public NullHits hits
        {
            get;
            set;
        }
    }


    [Serializable]
    public class QueryResult<T, FieldT>
    {
        public QueryResult()
        {
            took = 0;
            timed_out = false;
            _shards = new Shards();
            hits = new Hits<T, FieldT>();
        }

        /// <summary>
        /// 执行时间
        /// </summary>
        public int took
        {
            get;
            set;
        }
        /// <summary>
        /// 是否查询超时
        /// </summary>
        public bool timed_out
        {
            get;
            set;
        }
        /// <summary>
        /// 查询返回分片信息
        /// </summary>
        public Shards _shards
        {
            get;
            set;
        }
        /// <summary>
        /// 查询信息
        /// </summary>
        public Hits<T, FieldT> hits
        {
            get;
            set;
        }
    }

    [Serializable]
    public class QueryResult<T> : QueryResult<T, object>
    {
        

    }
    /// <summary>
    /// 查询分片信息
    /// </summary>
    public class Shards
    {
        /// <summary>
        /// 查询总分片
        /// </summary>
        public int total
        {
            get;
            set;
        }
        /// <summary>
        /// 查询成功分片数
        /// </summary>
        public int successful
        {
            get;
            set;
        }
        /// <summary>
        /// 查询跳过分片数
        /// </summary>
        public int skipped
        {
            get;
            set;
        }
        /// <summary>
        /// 查询失败分片数
        /// </summary>
        public int failed
        {
            get;
            set;
        }
    }
}
