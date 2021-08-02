using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 查询选项
    /// </summary>
    public partial class QueryItem
    {



        /// <summary>
        /// 查询条件
        /// </summary>
        /// <returns></returns>
        public string ToCondition()
        {
            if (this.QueryMethod == QueryMethod.StdIn || this.QueryMethod == QueryMethod.In)
            {
                string queryValue = this.Value.ToString().FilterSql();
                if (this.QueryDataType == QueryDataType.Guid || QueryDataType == QueryDataType.String || QueryDataType == QueryDataType.Date)
                {
                    queryValue = queryValue.ToFieldValue<string>();
                }
                return QueryUnite.ToString() + " " + this.FieldName + " in (" + queryValue + ")";
            }
            return QueryUnite.ToString() + " " + this.FieldName + " " + ToQueryMethod() + " ?" + this.ParameterName + " ";
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataParameter ToParameter()
        {
            if (this.QueryMethod == QueryMethod.StdIn || this.QueryMethod == QueryMethod.In)
            {
                return null;
            }
            object ParameterValue = this.Value;
            if (this.QueryDataType == QueryDataType.Int)
            {
                ParameterValue = this.Value.ToInt64();
            }
            else if (this.QueryDataType == QueryDataType.Date)
            {
                ParameterValue = Convert.ToDateTime(this.Value);
            }
            else if (this.QueryDataType == QueryDataType.String)
            {
                ParameterValue = this.Value.ToString();
            }
            switch (this.QueryMethod)
            {
                case QueryMethod.Equal:

                case QueryMethod.GreaterThan:

                case QueryMethod.GreaterThanOrEqual:

                case QueryMethod.LessThan:

                case QueryMethod.LessThanOrEqual:

                case QueryMethod.NotEqual:
                    break;

                case QueryMethod.StartsWith:
                    ParameterValue = string.Format("{0}%", ParameterValue.ToString());
                    break;
                case QueryMethod.EndsWith:
                    ParameterValue = string.Format("%{0}", ParameterValue.ToString());
                    break;
                case QueryMethod.Contains:
                    ParameterValue = string.Format("%{0}%", ParameterValue.ToString());
                    break;
                case QueryMethod.Like:
                    ParameterValue = string.Format("%{0}%", ParameterValue.ToString());
                    break;
            }
            DataParameter objDataParameter = new DataParameter(this.ParameterName, ParameterValue);
            return objDataParameter;

        }



    }
}
