using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Juan.Core
{
    public abstract partial class DbContext
    {

        /// <summary>
        /// 替换参数相应数据库特征符
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public string ParamCharReplace(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return expression;
            }

            expression = expression.Replace("=?", "=" + ParameterChar);
            expression = expression.Replace(">?", ">" + ParameterChar);
            expression = expression.Replace("<?", "<" + ParameterChar);
            expression = expression.Replace(@"like\s+\?", "like " + ParameterChar, RegexOptions.IgnoreCase);
            return expression;
        }

        /// <summary>
        /// 参数符号
        /// </summary>
        public abstract string ParameterChar
        {
            get;

        }

        /// <summary>
        /// 字段左特征符号
        /// </summary>
        public abstract string FieldLeftChar
        {
            get;
        }
        /// <summary>
        ///  字段右特征符号
        /// </summary>
        public abstract string FieldRightChar
        {
            get;
        }

        /// <summary>
        /// sql字段处理前后加不同数据的特征符号
        /// </summary>
        /// <param name="filedName"></param>
        /// <returns></returns>
        public string FieldName(string filedName)
        {
            return string.Format("{0}{1}{2}", FieldLeftChar, filedName, FieldRightChar);
        }
        /// <summary>
        /// sql字段参数名称
        /// </summary>
        /// <param name="filedName"></param>
        /// <returns></returns>
        public string FieldParamName(string filedName)
        {
            return string.Format("{0}{1}", ParameterChar, filedName);
        }




        /// <summary>
        /// 参数条件表达式
        /// </summary>
        /// <param name="filedName"></param>
        /// <returns></returns>
        public string ConditionParam(string filedName)
        {
            return string.Format("{0}={1}", FieldName(filedName), FieldParamName(filedName));
        }


        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="filedName">字段名称</param>
        /// <param name="value">值[单个值或IEnumerable类型]</param>
        /// <returns></returns>
        public string Condition(string filedName, object value)
        {

            if (value.IsList())
            {
                return string.Format("{0} in ({1})", FieldName(filedName), ToFieldValue(value));
            }
            else
            {
                return string.Format("{0}={1}", FieldName(filedName), ToFieldValue(value));
            }
        }

        /// <summary>
        /// 字段条件
        /// </summary>
        /// <typeparam name="P">类型</typeparam>
        /// <param name="filedName">字段名称</param>
        /// <param name="idString">主键字符串如1,2,3</param>
        /// <returns></returns>
        public string Condition<P>(string filedName, string idString)
        {
            return string.Format("{0} in ({1})", FieldName(filedName), ToFieldValue<P>(idString));
        }

        /// <summary>
        ///  转成查询条件值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public virtual string ToFieldValue(object value)
        {

            if (value.IsList())
            {
                IEnumerable objIEnumerable = value as IEnumerable;
                string outString = "";

                if (value == null)
                {
                    return outString;
                }
                Type type = objIEnumerable.AsQueryable().ElementType;
                string split = "";
                if (type == typeof(string) || type == typeof(DateTime) || type == typeof(Guid))
                {
                    split = "'";
                }
                bool isDateTimeValue = type == typeof(DateTime);
                IEnumerator enumerator = objIEnumerable.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (isDateTimeValue)
                    {
                        outString += split + ((DateTime)enumerator.Current).ToString("yyyy-MM-dd HH:mm:ss") + split + ",";
                    }
                    else
                    {
                        outString += split + enumerator.Current.ToString() + split + ",";
                    }
                }
                outString = outString.Trim(',');
                return outString;

            }
            else
            {

                Type type = value.GetType();
                string split = "";
                if (type == typeof(string) || type == typeof(DateTime) || type == typeof(Guid))
                {
                    split = "'";
                }
                if (type == typeof(DateTime))
                {
                    return split + ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss") + split;
                }
                else
                {
                    return split + value.ToString() + split;
                }
            }
        }

        /// <summary>
        /// 主键字符串[1,2,3]转成相应的数据库条件
        /// </summary>
        /// <typeparam name="P">类型</typeparam>
        /// <param name="idString">主键字符串如1,2,3</param>
        /// <returns></returns>
        public virtual string ToFieldValue<P>(string idString)
        {
            string[] arrString = idString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            Type type = typeof(P);
            string split = "";
            if (type == typeof(string) || type == typeof(DateTime) || type == typeof(Guid))
            {
                split = "'";
            }
            string outString = "";
            foreach (string rowString in arrString)
            {
                outString += split + rowString + split + ",";

            }
            outString = outString.TrimEnd(',');
            return outString;
        }


    }
}

