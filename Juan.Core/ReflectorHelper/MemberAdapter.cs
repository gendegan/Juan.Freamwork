using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Juan.Core
{
    /// <summary>
    /// 类成员元素
    /// </summary>
    public struct MemberAdapter
    {
        private object _object;
        private PropertyInfo _PropertyInfo;
        private FieldInfo _FieldInfo;
        /// <summary>
        /// 空属性
        /// </summary>
        public static readonly MemberAdapter Empty = new MemberAdapter();
        /// <summary>
        /// 成员类型
        /// </summary>
        public Type MemberType
        {
            get
            {

                if (_PropertyInfo != null)
                    return _PropertyInfo.PropertyType;
                else if (_FieldInfo != null)
                    return _FieldInfo.FieldType;
                else
                    return null;
            }
        }
        /// <summary>
        /// 值
        /// </summary>
        public object Value
        {
            get
            {
                if (_PropertyInfo != null && _PropertyInfo.CanRead)
                    return _PropertyInfo.GetValue(_object, null);
                else if (_FieldInfo != null)
                    return _FieldInfo.GetValue(_object);
                else
                    return null;
            }
            set
            {
                if (_PropertyInfo != null && _PropertyInfo.CanWrite)
                    _PropertyInfo.SetValue(_object, value, null);
                else if (_FieldInfo != null)
                    _FieldInfo.SetValue(_object, value);
            }
        }
        /// <summary>
        /// 成员适配器构造函数
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="propertyInfo">属性</param>
        public MemberAdapter(object obj, PropertyInfo propertyInfo)
        {
            _object = obj;
            _PropertyInfo = propertyInfo;
            _FieldInfo = null;
        }
        /// <summary>
        /// 成员适配器构造函数
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="fieldInfo">字段</param>
        public MemberAdapter(object obj, FieldInfo fieldInfo)
        {
            _object = obj;
            _FieldInfo = fieldInfo;
            _PropertyInfo = null;
        }
    }
}
