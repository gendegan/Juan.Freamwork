using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 批量值 信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BatchValueInfo<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public BatchValueInfo()
        {
            Data = new List<T>();
            UpdateFields = new string[0];
        }
        /// <summary>
        /// 数据
        /// </summary>
        public List<T> Data
        {
            get;
            set;
        }
        /// <summary>
        /// 更新字段
        /// </summary>
        public string[] UpdateFields
        {
            get;
            set;
        }
    }
}
