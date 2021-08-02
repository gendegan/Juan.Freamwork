using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    /// <summary>
    /// 查询选项
    /// </summary>
    public partial class QueryItem
    {

        /// <summary>
        /// 条件
        /// </summary>
        /// <returns></returns>
        public string ToConditionES()
        {
            return QueryUnite.ToString() + " " + this.FieldName + " " + ToQueryMethod() + " " + this.ToQueryValue() + " ";
        }
        
    }
}
