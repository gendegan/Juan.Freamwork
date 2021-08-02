using Juan.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Data
{
    public partial class MySqlContext<T, Key> : DbContext<T, Key> where T : class, new()
    {


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="value">值</param>
        /// <returns></returns>
        public override Key Add(T value)
        {
            List<PropertyInfo> objPropertyInfoList = GetInsertPropertys();
            string sqlQuery = string.Format("INSERT INTO {0} ({1}) VALUES ({2});", DBTableName, SqlInsertFields(objPropertyInfoList), SqlInsertValues(objPropertyInfoList));
            List<DataParameter> objDataParameterList = SqlInsertParams(objPropertyInfoList, value);

            if (!PrimaryKey.Identity)
            {
                Context.ExecuteNonQuery(sqlQuery, objDataParameterList.ToArray());
                return (Key)PrimaryKey.PrimaryProperty.GetValue(value, null);
            }
            else
            {
                long ID = Convert.ToInt64(PrimaryKey.PrimaryProperty.GetValue(value, null).ToString());
                if (ID >= 0)
                {
                    PropertyInfo objGuidPropertyInfo = objPropertyInfoList.FirstOrDefault(s => s.Name.Equals("guid", StringComparison.OrdinalIgnoreCase));

                    if (objGuidPropertyInfo != null)
                    {
                        sqlQuery += string.Format(" SELECT {0} FROM {1} WHERE {2}={3} LIMIT 1;", FieldName(PrimaryKey.PrimaryName), DBTableName, FieldName(objGuidPropertyInfo.Name), FieldParamName(objGuidPropertyInfo.Name));
                    }
                    else
                    {
                        sqlQuery += " SELECT LAST_INSERT_ID();";
                    }

                    object objValue = Context.ExecuteScalar(sqlQuery, objDataParameterList.ToArray());
                    if (objValue == null)
                    {
                        return default(Key);
                    }
                    else
                    {
                        PrimaryKey.PrimaryProperty.SetValue(value, objValue.To(PrimaryKey.PrimaryType), null);
                        return objValue.To<Key>();
                    }
                }
                else
                {
                    Context.ExecuteNonQuery(sqlQuery, objDataParameterList.ToArray());
                    return default(Key);
                }

            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="isIgnore">是否忽略</param>
        /// <returns></returns>
        public override int Add(T value, bool isIgnore)
        {
            List<PropertyInfo> objPropertyInfoList = GetInsertPropertys();
            string sqlQuery = string.Format("INSERT " + (isIgnore ? "IGNORE" : "") + " INTO {0} ({1}) VALUES ({2});", DBTableName, SqlInsertFields(objPropertyInfoList), SqlInsertValues(objPropertyInfoList));
            List<DataParameter> objDataParameterList = SqlInsertParams(objPropertyInfoList, value);
            return Context.ExecuteNonQuery(sqlQuery, objDataParameterList.ToArray());
        }

        /// <summary>
        /// 指量新增
        /// </summary>
        /// <param name="valueList">值</param>
        /// <param name="isIgnore">是否忽略</param>
        /// <returns></returns>
        public override int Add(IEnumerable<T> valueList, bool isIgnore)
        {
            if (valueList.Count() == 0)
            {
                return 0;
            }
            List<PropertyInfo> objPropertyInfoList = GetInsertPropertys();
            string sqlQuery = string.Format("INSERT " + (isIgnore ? "IGNORE" : "") + " INTO {0} ({1}) VALUES {2};", DBTableName, SqlInsertFields(objPropertyInfoList), SqlInsertValues(objPropertyInfoList, valueList.Count()));
            List<DataParameter> objDataParameterList = SqlInsertParams(objPropertyInfoList, valueList);
            return Context.ExecuteNonQuery(sqlQuery, objDataParameterList.ToArray());
        }

        string ProcessDuplicateField(params string[] fields)
        {
            string DuplicateField = "";
            foreach (string field in fields)
            {
                foreach (string itemField in field.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    DuplicateField += string.Format("{0}=VALUES({0}),", FieldName(itemField));
                }
            }
            return DuplicateField.TrimEndComma();
        }
        /// <summary>
        /// 新增更新操作
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public override int AddUpdate(T value, params string[] fields)
        {

            string DuplicateSql = ProcessDuplicateField(fields);
            return AddUpdateValue(value, DuplicateSql);
        }

        /// <summary>
        /// 新增更新操作
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="updateSql">更新语句</param>
        /// <param name="parms">更新参数</param>
        /// <returns></returns>
        public override int AddUpdateValue(T value, string updateSql, params DataParameter[] parms)
        {
            List<PropertyInfo> objPropertyInfoList = GetInsertPropertys();
            string sqlQuery = string.Format("INSERT  INTO {0} ({1}) VALUES ({2})", DBTableName, SqlInsertFields(objPropertyInfoList), SqlInsertValues(objPropertyInfoList));
            List<DataParameter> objDataParameterList = SqlInsertParams(objPropertyInfoList, value);

            updateSql = ParamCharReplace(updateSql);
            if (!string.IsNullOrWhiteSpace(updateSql))
            {
                sqlQuery += " on duplicate key update " + updateSql;
            }
            sqlQuery += ";";

            return Context.ExecuteNonQuery(sqlQuery, objDataParameterList.MergeParameter(parms));
        }
        /// <summary>
        ///  新增更新操作
        /// </summary>
        /// <param name="value"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public override int AddUpdateExclude(T value, params string[] fields)
        {
            List<PropertyInfo> objPropertyInfoList = GetInsertPropertys();

            string[] updateFields = objPropertyInfoList.Select(s => s.Name).ToList().Except(fields).ToArray();
            string updateSql = ProcessDuplicateField(updateFields);
            string sqlQuery = string.Format("INSERT  INTO {0} ({1}) VALUES ({2})", DBTableName, SqlInsertFields(objPropertyInfoList), SqlInsertValues(objPropertyInfoList));
            List<DataParameter> objDataParameterList = SqlInsertParams(objPropertyInfoList, value);

            updateSql = ParamCharReplace(updateSql);
            if (!string.IsNullOrWhiteSpace(updateSql))
            {
                sqlQuery += " on duplicate key update " + updateSql;
            }
            sqlQuery += ";";

            return Context.ExecuteNonQuery(sqlQuery, objDataParameterList.ToArray());
        }

        /// <summary>
        /// 新增更新操作
        /// </summary>
        /// <param name="valueList">值</param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        public override int AddUpdate(IEnumerable<T> valueList, params string[] fields)
        {
            string DuplicateSql = ProcessDuplicateField(fields);
            return AddUpdateValue(valueList, DuplicateSql);
        }

        /// <summary>
        /// 新增更新操作
        /// </summary>
        /// <param name="valueList">值</param>
        /// <param name="updateSql">更新语句</param>
        /// <param name="parms">更新参数</param>
        /// <returns></returns>
        public override int AddUpdateValue(IEnumerable<T> valueList, string updateSql, params DataParameter[] parms)
        {
            if (valueList.Count() == 0)
            {
                return 0;
            }
            List<PropertyInfo> objPropertyInfoList = GetInsertPropertys();
            string sqlQuery = string.Format("INSERT  INTO {0} ({1}) VALUES {2}", DBTableName, SqlInsertFields(objPropertyInfoList), SqlInsertValues(objPropertyInfoList, valueList.Count()));
            List<DataParameter> objDataParameterList = SqlInsertParams(objPropertyInfoList, valueList);
            updateSql = ParamCharReplace(updateSql);
            if (!string.IsNullOrWhiteSpace(updateSql))
            {
                sqlQuery += " on duplicate key update " + updateSql;
            }
            sqlQuery += ";";

            return Context.ExecuteNonQuery(sqlQuery, objDataParameterList.MergeParameter(parms));
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueList"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public override int AddUpdateExclude(IEnumerable<T> valueList, params string[] fields)
        {

            if (valueList.Count() == 0)
            {
                return 0;
            }
            List<PropertyInfo> objPropertyInfoList = GetInsertPropertys();

            string[] updateFields = objPropertyInfoList.Select(s => s.Name).ToList().Except(fields).ToArray();
            string updateSql = ProcessDuplicateField(updateFields);
            string sqlQuery = string.Format("INSERT  INTO {0} ({1}) VALUES {2}", DBTableName, SqlInsertFields(objPropertyInfoList), SqlInsertValues(objPropertyInfoList, valueList.Count()));
            List<DataParameter> objDataParameterList = SqlInsertParams(objPropertyInfoList, valueList);
            updateSql = ParamCharReplace(updateSql);
            if (!string.IsNullOrWhiteSpace(updateSql))
            {
                sqlQuery += " on duplicate key update " + updateSql;
            }
            sqlQuery += ";";

            return Context.ExecuteNonQuery(sqlQuery, objDataParameterList.ToArray());
        }
    }
}
