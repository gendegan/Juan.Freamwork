using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    public partial class ReadOptions
    {
        /// <summary>
        /// 合并条件
        /// </summary>
        /// <returns></returns>
        public string MergeCondition()
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
                    mergeCondition += objQueryItem.ToCondition();
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
        /// <summary>
        /// 合并参数
        /// </summary>
        /// <returns></returns>
        public DataParameter[] MergeParameters()
        {
            if (QueryItemList.IsNull())
            {
                return Parameters;
            }
            else
            {

                List<DataParameter> objMergeParameterList = new List<DataParameter>();
                foreach (QueryItem objQueryItem in QueryItemList)
                {

                    DataParameter objDataParameter = objQueryItem.ToParameter();
                    if (objDataParameter != null)
                    {
                        objMergeParameterList.Add(objDataParameter);
                    }

                }
                if (Parameters == null || Parameters.Length == 0)
                {
                    return objMergeParameterList.ToArray();

                }
                else
                {
                    return objMergeParameterList.MergeParameter(Parameters);
                }

            }

        }
    }
}
