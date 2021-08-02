using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Data.ESearch
{
    /// <summary>
    /// 查询无结果信息
    /// </summary>
    public class NullHits
    {
        public NullHits()
        {
            total = new TotalInfo();
            max_score = "";
            hits = new object();
        }
        public TotalInfo total
        {
            get;
            set;
        }
        public string max_score
        {
            get;
            set;
        }
        public object hits
        {
            get;
            set;
        }
    }
    public class TotalInfo
    {
        public TotalInfo()
        {
            value = 0;
            relation = "";
        }
        public long value
        {
            get;set;
        }
        public string relation
        {
            get;set;
        }
    }

    public class Hits<T, FieldT>
    {
        public Hits()
        {
            total = new TotalInfo();
            max_score = 0;
            hits = new List<HitsInfo<T, FieldT>>();
        }
        /// <summary>
        /// 查询匹配总数
        /// </summary>
        public TotalInfo total
        {
            get;
            set;
        }
        /// <summary>
        /// 匹配度最大值
        /// </summary>
        public double? max_score
        {
            get;
            set;
        }
        /// <summary>
        /// 查询结果信息
        /// </summary>
        public List<HitsInfo<T, FieldT>> hits
        {
            get;
            set;
        }
    }

    public class HitsInfo<T, FieldT>
    {
        public HitsInfo()
        {
            _index = "";
            _type = "";
            _id = "";
            _score = 0;
            _routing = "";
        }
        /// <summary>
        /// 索引名
        /// </summary>
        public string _index
        {
            get;
            set;
        }
        /// <summary>
        /// 文档
        /// </summary>
        public string _type
        {
            get;
            set;
        }
        /// <summary>
        /// 文档key
        /// </summary>
        public string _id
        {
            get;
            set;
        }
        /// <summary>
        /// 路由
        /// </summary>
        public string _routing
        {
            get;
            set;
        }
        /// <summary>
        /// 文档查询匹配度
        /// </summary>
        public double _score
        {
            get;
            set;
        }

        public T _source
        {
            get;
            set;
        }

        public FieldT fields
        {
            get;
            set;
        }
    }
}
