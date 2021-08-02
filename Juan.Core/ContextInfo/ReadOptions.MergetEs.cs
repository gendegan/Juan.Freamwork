using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    public partial class ReadOptions
    {
        /// <summary>
        /// 合并条件
        /// </summary>
        /// <returns></returns>
        public string MergeConditionES()
        {
            if (QueryItemList.IsNull())
            {
                return Condition;
            }
            else
            {
                string mergeCondition = "";
                foreach (QueryItem objQueryItem in QueryItemList)
                {
                    mergeCondition += objQueryItem.ToConditionES();
                }

                if (Condition.IsNullOrWhiteSpace())
                {
                    return mergeCondition.TrimStart(QueryUnite.AND.ToString()).TrimStart(QueryUnite.OR.ToString());

                }
                else
                {
                    return Condition + " " + mergeCondition;
                }

            }

        }

    }
}
