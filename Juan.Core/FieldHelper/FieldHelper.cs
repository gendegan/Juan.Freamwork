using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    /// <summary>
    /// 字段帮助类
    /// </summary>
    public static partial class FieldHelper
    {


        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="filedName">字段名称</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static string Condition(this string filedName, object value)
        {

            if (value.IsList())
            {
                return string.Format("{0} in ({1})", filedName, ToFieldValue(value));
            }
            else
            {
                return string.Format("{0}={1}", filedName, ToFieldValue(value));
            }
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="filedName"></param>
        /// <param name="idString"></param>
        /// <returns></returns>
        public static string Condition<P>(this string filedName, string idString)
        {

            return string.Format("{0} in ({1})", filedName, ToFieldValue<P>(idString));
        }
        /// <summary>
        /// 转成查询条件值
        /// </summary>
        /// <param name="fieldValue">值</param>
        /// <returns></returns>
        public static string ToFieldValue(this object fieldValue)
        {
            if (fieldValue.IsList())
            {
                IEnumerable objIEnumerable = fieldValue as IEnumerable;
                string outString = "";

                if (fieldValue == null)
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

                Type type = fieldValue.GetType();
                string split = "";
                if (type == typeof(string) || type == typeof(DateTime) || type == typeof(Guid))
                {
                    split = "'";
                }
                if (type == typeof(DateTime))
                {
                    return split + ((DateTime)fieldValue).ToString("yyyy-MM-dd HH:mm:ss") + split;
                }
                else
                {
                    return split + fieldValue.ToString() + split;
                }
            }
        }
        /// <summary>
        /// 转成数据库字段相应的值
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="idString"></param>
        /// <returns></returns>
        public static string ToFieldValue<P>(this string idString)
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="idString"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string ToFieldValue(this string idString, char split)
        {
            string[] arrString = idString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

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
