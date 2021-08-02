using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 
    /// </summary>
    public enum QueryUnite
    {
        /// <summary>
        /// 或查询
        /// </summary>
        OR,
        /// <summary>
        /// 与查询
        /// </summary>
        AND

    }
    /// <summary>
    /// 查询方式
    /// </summary>
    public enum QueryMethod
    {
        /// <summary>
        /// 等于
        /// </summary>
        Equal,
        /// <summary>
        /// 大于
        /// </summary>
        GreaterThan,
        /// <summary>
        /// 大于等于
        /// </summary>
        GreaterThanOrEqual,
        /// <summary>
        /// 小于
        /// </summary>
        LessThan,
        /// <summary>
        /// 小于等于
        /// </summary>
        LessThanOrEqual,
        /// <summary>
        /// 不等于
        /// </summary>
        NotEqual,
        /// <summary>
        ///  LIKE '值%'
        /// </summary>
        StartsWith,
        /// <summary>
        ///   LIKE '%值'
        /// </summary>
        EndsWith,
        /// <summary>
        ///  LIKE '%值%'
        /// </summary>
        Contains,
        /// <summary>
        /// Solr String 专用
        /// </summary>
        Like,
        /// <summary>
        ///  IN
        /// </summary>
        StdIn,
        /// <summary>
        /// In
        /// </summary>
        In,

        /// <summary>
        /// 多值查询
        /// </summary>
        ORLike,


    }
    /// <summary>
    /// 查询数据类型
    /// </summary>
    public enum QueryDataType
    {
        /// <summary>
        /// 字符串型
        /// </summary>
        String,
        /// <summary>
        /// 数字型
        /// </summary>
        Int,


        /// <summary>
        /// 全球唯一码
        /// </summary>
        Guid,
        /// <summary>
        /// 时间
        /// </summary>
        Date,
        /// <summary>
        /// 模型选择
        /// </summary>
        ObjectT,
        /// <summary>
        /// 时间戳[毫秒数]
        /// </summary>
        TimeStamp,
        /// <summary>
        /// 时间戳[秒数]
        /// </summary>
        SecondStamp
    }
}
