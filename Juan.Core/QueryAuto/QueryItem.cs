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
        /// 查询项
        /// </summary>
        public QueryItem()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="queryMethod"></param>
        /// <param name="queryDataType"></param>
        /// <param name="value"></param>
        /// <param name="parameterName"></param>
        public QueryItem(string fieldName, QueryMethod queryMethod, QueryDataType queryDataType, object value, string parameterName = "")
        {
            QueryUnite = QueryUnite.AND;
            FieldName = fieldName;
            QueryMethod = queryMethod;
            QueryDataType = queryDataType;
            Value = value;
            ParameterName = parameterName;

        }


        /// <summary>
        /// 字段名称
        /// </summary>
        /// 
        public string FieldName
        {
            get;
            set;
        }
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParameterName
        {
            get;
            set;
        }

        /// <summary>
        /// 值
        /// </summary>
        public object Value
        {
            get;
            set;
        }

        /// <summary>
        /// 如果使用Or组合，则此组组合为一个Or序列
        /// </summary>
        public string OrGroup { get; set; }
        /// <summary>
        /// 查询方式
        /// </summary>
        public QueryMethod QueryMethod
        {
            get;
            set;
        }
        /// <summary>
        /// 查询合并方式
        /// </summary>
        public QueryUnite QueryUnite
        {
            get;
            set;
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public QueryDataType QueryDataType
        {
            get;
            set;
        }


        /// <summary>
        /// 默认数据类型
        /// </summary>
        public QueryDataType DefaultQueryDataType
        {
            get;
            set;
        }

        private string ToQueryMethod()
        {
            string _Method = "";

            switch (this.QueryMethod)
            {
                case QueryMethod.StartsWith:
                case QueryMethod.EndsWith:

                case QueryMethod.Contains:
                case QueryMethod.Like:
                    _Method = "like";
                    break;

                case QueryMethod.Equal:
                    _Method = "=";
                    break;
                case QueryMethod.GreaterThan:
                    _Method = ">";
                    break;
                case QueryMethod.GreaterThanOrEqual:
                    _Method = ">=";
                    break;
                case QueryMethod.LessThan:
                    _Method = "<";
                    break;
                case QueryMethod.LessThanOrEqual:
                    _Method = "<=";
                    break;
                case QueryMethod.NotEqual:
                    _Method = "<>";
                    break;
                case QueryMethod.StdIn:
                    _Method = "in";
                    break;
                case QueryMethod.In:
                    _Method = "in";
                    break;

            }
            return _Method;


        }

        string ToQueryValue()
        {
            string queryValue = this.Value.ToString();


            switch (this.QueryMethod)
            {
                case QueryMethod.StartsWith:
                    queryValue = string.Format("'{0}%'", queryValue);
                    break;
                case QueryMethod.EndsWith:
                    queryValue = string.Format("'%{0}'", queryValue);
                    break;
                case QueryMethod.Contains:
                    queryValue = string.Format("'%{0}%'", queryValue);
                    break;
                case QueryMethod.Like:
                    queryValue = string.Format("'%{0}%'", queryValue);
                    break;
                default:
                    if (QueryDataType == QueryDataType.ObjectT)
                    {
                        throw new ArgumentNullException("查询字段" + FieldName + "请设置字段查询类型");
                    }
                    if (this.QueryDataType == QueryDataType.Guid || QueryDataType == QueryDataType.String || QueryDataType == QueryDataType.Date)
                    {
                        queryValue = queryValue.ToString().ToFieldValue<string>();
                    }
                    else if (this.QueryDataType == QueryDataType.SecondStamp)
                    {
                        queryValue = this.Value.ToString().ToDateTime().ToStampSecond().ToString();
                    }
                    else if (this.QueryDataType == QueryDataType.TimeStamp)
                    {
                        queryValue = this.Value.ToString().ToDateTime().ToStamp().ToString();
                    }

                    if (this.QueryMethod == QueryMethod.StdIn || this.QueryMethod == QueryMethod.In)
                    {
                        queryValue = string.Format("({0})", queryValue);
                    }

                    break;
            }
            return queryValue;


        }


    }
}
