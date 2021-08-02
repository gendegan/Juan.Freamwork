using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Juan.Core;

namespace Juan.Data
{
    public abstract partial class IDbContext<T, Key> where T : class, new()
    {




        /// <summary>
        /// sql字段处理前后加不同数据的特征符号
        /// </summary>
        /// <param name="filedName">字段名称</param>
        /// <returns></returns>
        public string FieldName(string filedName)
        {
            return Context.FieldName(filedName);
        }
        /// <summary>
        /// sql字段参数名称
        /// </summary>
        /// <param name="filedName">字段名称</param>
        /// <returns></returns>
        public string FieldParamName(string filedName)
        {
            return Context.FieldParamName(filedName);
        }


        /// <summary>
        /// 替换参数相应数据库特征符
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected string ParamCharReplace(string expression)
        {
            return Context.ParamCharReplace(expression);
        }

        /// <summary>
        /// 主键参数条件表达式
        /// </summary>
        /// <returns></returns>
        public string ConditionParamKey()
        {
            return ConditionParam(PrimaryKey.PrimaryName);
        }

        /// <summary>
        /// 参数条件表达式
        /// </summary>
        /// <param name="filedName">字段名称</param>
        /// <returns></returns>
        public string ConditionParam(string filedName)
        {
            return string.Format("{0}={1}", FieldName(filedName), FieldParamName(filedName));
        }
        /// <summary>
        /// 主键条件表达式
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns></returns>
        public string Condition(Key id)
        {
            return Condition(PrimaryKey.PrimaryName, id);
        }
        /// <summary>
        /// 主键条件表达式
        /// </summary>
        /// <param name="ids">主键列表</param>
        /// <returns></returns>
        public string Condition(IEnumerable ids)
        {
            return Condition(PrimaryKey.PrimaryName, ids);
        }
        /// <summary>
        /// 主键查询条件
        /// </summary>
        /// <param name="idString">主键字符串如1,2,3</param>
        /// <returns></returns>
        public string ConditionID(string idString)
        {
            return Condition<Key>(PrimaryKey.PrimaryName, idString);
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="filedName">字段名称</param>
        /// <param name="value">值[单个值或IEnumerable类型]</param>
        /// <returns></returns>
        public string Condition(string filedName, object value)
        {

            return Context.Condition(filedName, value);
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

            return Context.Condition<P>(filedName, idString);
        }

        /// <summary>
        ///  转成查询条件值
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public string ToFieldValue(object value)
        {

            return Context.ToFieldValue(value);
        }

        /// <summary>
        /// 主键字符串[1,2,3]转成相应的数据库条件
        /// </summary>
        /// <typeparam name="P">类型</typeparam>
        /// <param name="idString">主键字符串如1,2,3</param>
        /// <returns></returns>
        public string ToFieldValue<P>(string idString)
        {
            return Context.ToFieldValue<P>(idString);
        }

    }
}
