using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Juan.Core;

namespace Juan.Data
{
    public abstract partial class IDbContext<T, Key> where T : class, new()
    {

        /// <summary>
        /// 检查属性是否对应数据库
        /// </summary>
        /// <param name="objPropertyInfo"></param>
        /// <returns></returns>
        private bool CheckAutoProperty(PropertyInfo objPropertyInfo)
        {

            if (objPropertyInfo.Name == "_id")
            {
                return false;
            }
            if (!objPropertyInfo.CanWrite || !objPropertyInfo.CanRead || typeof(IDictionary).IsAssignableFrom(objPropertyInfo.PropertyType) || (typeof(IList).IsAssignableFrom(objPropertyInfo.PropertyType) && !typeof(Array).IsAssignableFrom(objPropertyInfo.PropertyType)))
            {

                return false;
            }

            return true;

        }

        /// <summary>
        /// 获取新增属性
        /// </summary>
        /// <param name="isMustPrimaryKey">是否强制添加主键</param>
        /// <returns></returns>
        protected List<PropertyInfo> GetInsertPropertys(bool isMustPrimaryKey = false)
        {


            List<PropertyInfo> _InsertPropertyList = new List<PropertyInfo>();
            foreach (PropertyInfo objPropertyInfo in typeof(T).GetProperties())
            {

                if (!CheckAutoProperty(objPropertyInfo))
                {
                    continue;
                }

                object[] objAttributes = objPropertyInfo.GetCustomAttributes(false);
                if (objAttributes.Length == 0)
                {
                    _InsertPropertyList.Add(objPropertyInfo);
                }
                else
                {
                    bool IsAddProperty = true;
                    foreach (object objAttribute in objAttributes)
                    {
                        if (objAttribute is PrimaryKeyAttribute)
                        {
                            if (isMustPrimaryKey)
                            {
                                break;
                            }
                            PrimaryKeyAttribute objPrimaryKeyAttribute = (PrimaryKeyAttribute)objAttribute;
                            if (objPrimaryKeyAttribute.Identity)
                            {
                                IsAddProperty = false;
                            }
                        }
                        else if (objAttribute is DbIgnoreAttribute)
                        {
                            DbIgnoreAttribute objDbIgnoreAttribute = (DbIgnoreAttribute)objAttribute;
                            if (objDbIgnoreAttribute.Ignore == IgnoreType.All || objDbIgnoreAttribute.Ignore == IgnoreType.Add)
                            {
                                IsAddProperty = false;
                            }

                        }
                    }
                    if (IsAddProperty)
                    {
                        _InsertPropertyList.Add(objPropertyInfo);
                    }
                }
            }
            return _InsertPropertyList;


        }

        /// <summary>
        /// 获取更新属性
        /// </summary>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        protected List<PropertyInfo> GetUpdatePropertys(params string[] fields)
        {

            List<PropertyInfo> _UpdatePropertyList = new List<PropertyInfo>();
            PropertyInfo[] objPropertyInfoList = typeof(T).GetProperties();
            if (fields.Length > 0)
            {
                _UpdatePropertyList = objPropertyInfoList.Where(s => fields.Contains(s.Name)).ToList();
                if (_UpdatePropertyList.Count != fields.Length)
                {
                    string NoFieldExists = fields.Except(_UpdatePropertyList.Select(s => s.Name)).ToConcat();
                    throw new ArgumentException(NoFieldExists + "字段不存在,请输入正确的需要更新字段", "fields");
                }
                return _UpdatePropertyList;

            }
            foreach (PropertyInfo objPropertyInfo in objPropertyInfoList)
            {
                if (!CheckAutoProperty(objPropertyInfo))
                {
                    continue;
                }

                object[] objAttributes = objPropertyInfo.GetCustomAttributes(false);
                if (objAttributes.Length == 0)
                {
                    _UpdatePropertyList.Add(objPropertyInfo);
                }
                else
                {
                    bool IsAddProperty = true;
                    foreach (object objAttribute in objAttributes)
                    {
                        if (objAttribute is PrimaryKeyAttribute)
                        {
                            IsAddProperty = false;
                        }
                        else if (objAttribute is DbIgnoreAttribute)
                        {
                            DbIgnoreAttribute objDbIgnoreAttribute = (DbIgnoreAttribute)objAttribute;
                            if (objDbIgnoreAttribute.Ignore == IgnoreType.All || objDbIgnoreAttribute.Ignore == IgnoreType.Update)
                            {
                                IsAddProperty = false;
                            }

                        }
                    }
                    if (IsAddProperty)
                    {
                        _UpdatePropertyList.Add(objPropertyInfo);
                    }
                }
            }
            return _UpdatePropertyList;

        }

        /// <summary>
        /// 获取更新属性
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        protected List<PropertyInfo> GetUpdateExcludePropertys(params string[] fields)
        {

            List<PropertyInfo> _UpdatePropertyList = new List<PropertyInfo>();
            PropertyInfo[] objPropertyInfoList = typeof(T).GetProperties();


            if (fields.Length > 0)
            {
                objPropertyInfoList = objPropertyInfoList.Where(s => !fields.Contains(s.Name)).ToArray();
            }
            foreach (PropertyInfo objPropertyInfo in objPropertyInfoList)
            {
                if (!CheckAutoProperty(objPropertyInfo))
                {
                    continue;
                }

                object[] objAttributes = objPropertyInfo.GetCustomAttributes(false);
                if (objAttributes.Length == 0)
                {
                    _UpdatePropertyList.Add(objPropertyInfo);
                }
                else
                {
                    bool IsAddProperty = true;
                    foreach (object objAttribute in objAttributes)
                    {
                        if (objAttribute is PrimaryKeyAttribute)
                        {
                            IsAddProperty = false;
                        }
                        else if (objAttribute is DbIgnoreAttribute)
                        {
                            DbIgnoreAttribute objDbIgnoreAttribute = (DbIgnoreAttribute)objAttribute;
                            if (objDbIgnoreAttribute.Ignore == IgnoreType.All || objDbIgnoreAttribute.Ignore == IgnoreType.Update)
                            {
                                IsAddProperty = false;
                            }

                        }
                    }
                    if (IsAddProperty)
                    {
                        _UpdatePropertyList.Add(objPropertyInfo);
                    }
                }
            }
            return _UpdatePropertyList;

        }



    }
}
