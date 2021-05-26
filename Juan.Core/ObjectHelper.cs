using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    public static class ObjectHelper
    {

        private static string ExceptionToMessage(this Exception value)
        {
            StringBuilder builder = new StringBuilder();
            Exception innerException = value;
            if ((innerException.Data != null) && (innerException.Data.Count > 0))
            {
                builder.Append("异常数据：").Append("\r\n").AppendLine(innerException.Data.JsonSerialize(null));
            }
            builder.Append("\r\n").Append("事件信息：").Append("\r\n").AppendLine(innerException.Message);
            builder.Append("\r\n").Append("堆栈跟踪：").Append("\r\n").AppendLine(innerException.StackTrace);
            while (innerException.InnerException!=null)
            {
                if ((innerException.InnerException.Data != null) && (innerException.InnerException.Data.Count > 0))
                {
                    builder.Append("内部异常数据：").Append("\r\n").AppendLine(innerException.InnerException.Data.JsonSerialize(null));
                }
                builder.Append("\r\n").Append("内部事件信息：").Append("\r\n").AppendLine(innerException.InnerException.Message);
                builder.Append("\r\n").Append("内部堆栈跟踪：").Append("\r\n").AppendLine(innerException.InnerException.StackTrace);
                innerException = innerException.InnerException;
            }
            return builder.ToString();
        }

        public static Type GetUnNullableType(this Type type)
        {
            if (type.IsNullableType())
            {
                NullableConverter converter = new NullableConverter(type);
                return converter.UnderlyingType;
            }
            return type;
        }

        public static bool IsDBNull(this object value)
        {
            return ((value == null) || (value == DBNull.Value));
        }

        public static bool IsNoNull(this object instance)
        {
            return !instance.IsNull();
        }

        public static bool IsNull(this object instance)
        {
            if (instance!=null)
            {
                if (instance is string)
                {
                    return instance.ToString().IsNull();
                }
                if (instance is int)
                {
                    return instance.Equals(NullInt);
                }
                if (instance is long)
                {
                    return instance.Equals(NullLong);
                }
                if (instance is Guid)
                {
                    return instance.Equals(NullGuid);
                }
                if (instance is DateTime)
                {
                    DateTime time = (DateTime)instance;
                    return time.Date.Equals(NullDate.Date);
                }
                if (instance is bool)
                {
                    return instance.Equals(NullBoolean);
                }
                if (instance is float)
                {
                    return instance.Equals(NullSingle);
                }
                if (instance is double)
                {
                    return instance.Equals(NullDouble);
                }
                if (instance is decimal)
                {
                    return instance.Equals(NullDecimal);
                }
                return ((instance is float) && instance.Equals(NullFloat));
            }
            return true;
        }

        public static bool IsNullableType(this Type type)
        {
            return (((type != null) && type.IsGenericType) && (type.GetGenericTypeDefinition() == typeof(Nullable<>)));
        }

        public static T To<T>(this object value)
        {
            return (T)value.To(typeof(T));
        }

        public static object To(this object value, Type type)
        {
            if ((value == null) || (value == DBNull.Value))
            {
                if (((((type == typeof(int)) || (type == typeof(long))) || ((type == typeof(short)) || (type == typeof(long)))) || ((type == typeof(double)) || (type == typeof(float)))) || (type == typeof(decimal)))
                {
                    return Convert.ChangeType(0, type);
                }
                if (type == typeof(bool))
                {
                    return false;
                }
                return value;
            }
            if (string.IsNullOrWhiteSpace(value.ToString()))
            {
                if (((((type == typeof(int)) || (type == typeof(long))) || ((type == typeof(short)) || (type == typeof(long)))) || ((type == typeof(double)) || (type == typeof(float)))) || (type == typeof(decimal)))
                {
                    return Convert.ChangeType(0, type);
                }
                if (type == typeof(bool))
                {
                    return false;
                }
            }
            if (type.IsNullableType())
            {
                type = type.GetUnNullableType();
            }
            if (type.IsEnum)
            {
                return Enum.Parse(type, value.ToString(), true);
            }
            if (type == typeof(Guid))
            {
                return Guid.Parse(value.ToString());
            }
            return Convert.ChangeType(value, type);
        }

        public static T To<T>(this object value, T defaultValue)
        {
            try
            {
                return value.To<T>();
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public static T ToAssign<T>(this NameValueCollection source) where T : class, new()
        {
            T target = Activator.CreateInstance<T>();
            return source.ToAssign<T>(target);
        }

        public static T ToAssign<T, TValue>(this IDictionary<string, TValue> source) where T : class, new()
        {
            T target = Activator.CreateInstance<T>();
            return source.ToAssign<T, TValue>(target);
        }

        public static T ToAssign<T, TValue>(this IDictionary<string, TValue> source, T target) where T : class, new()
        {
            if (source != null)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                using (IEnumerator<KeyValuePair<string, TValue>> enumerator = source.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        KeyValuePair<string, TValue> item = enumerator.Current;
                        PropertyInfo info = properties.FirstOrDefault<PropertyInfo>(s => s.Name.Equals(item.Key, StringComparison.OrdinalIgnoreCase));
                        if ((info != null) && info.CanWrite)
                        {
                            info.SetValue(target, item.Value.To(info.PropertyType), null);
                        }
                    }
                }
            }
            return target;
        }

        public static T ToAssign<T>(this NameValueCollection source, T target) where T : class, new()
        {
            if (source != null)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                string[] allKeys = source.AllKeys;
                for (int i = 0; i < allKeys.Length; i++)
                {
                    string key = allKeys[i];
                    PropertyInfo info = properties.FirstOrDefault<PropertyInfo>(s => s.Name.Equals(key, StringComparison.OrdinalIgnoreCase));
                    if ((info != null) && info.CanWrite)
                    {
                        string str = source[key];
                        if (!string.IsNullOrWhiteSpace(str))
                        {
                            info.SetValue(target, str.To(info.PropertyType), null);
                        }
                    }
                }
            }
            return target;
        }

        public static bool ToBool(this object value)
        {
            if (value == null)
            {
                return false;
            }
            string str = value.ToString();
            return (str.Equals("true", StringComparison.OrdinalIgnoreCase) || str.Equals("1"));
        }

        public static string ToCutByte(this object value, int Length, string tail = "")
        {
            if ((value == null) || (value == DBNull.Value))
            {
                return "";
            }
            return value.ToString().ToCutByte(Length, tail);
        }

        public static string ToCutWord(this object value, int Length, string tail = "")
        {
            if ((value == null) || (value == DBNull.Value))
            {
                return "";
            }
            return value.ToString().ToCutWord(Length, tail);
        }

        public static string ToDateShortText(this object dateTime, string format = "yyyy-MM-dd")
        {
            if (dateTime is DateTime)
            {
                DateTime time = (DateTime)dateTime;
                return time.ToString(format);
            }
            return "此对象不是DateTime类型";
        }

        public static string ToDateText(this object dateTime, string format = "yyyy-MM-dd HH:mm:ss")
        {
            if (dateTime is DateTime)
            {
                DateTime time = (DateTime)dateTime;
                return time.ToString(format);
            }
            return "此对象不是DateTime类型";
        }

        public static decimal ToDecimal(this object value)
        {
            return value.To<decimal>();
        }

        public static double ToDouble(this object value)
        {
            return value.To<double>();
        }

        public static float ToFloat(this object value)
        {
            return value.To<float>();
        }

        public static Guid ToGuid(this object value)
        {
            return value.To<Guid>();
        }

        public static int ToInt(this object value)
        {
            return value.To<int>();
        }

        public static long ToInt64(this object value)
        {
            return value.To<long>();
        }

        public static long ToLong(this object value)
        {
            return value.To<long>();
        }

        public static string ToMenuFieldValue(this object value)
        {
            if (value == null)
            {
                return "";
            }
            if (value is bool)
            {
                return (((bool)value) ? "1" : "0");
            }
            return value.ToString();
        }

        public static string ToMessage(this object[] values)
        {
            StringBuilder builder = new StringBuilder();
            foreach (object obj2 in values)
            {
                builder.AppendLine(obj2.ToMessage());
            }
            return builder.ToString();
        }

        public static string ToMessage(this object value)
        {
            try
            {
                if (value == null)
                {
                    return "null";
                }
                if (value is string)
                {
                    return (string)value;
                }
                if (value.GetType().IsValueType)
                {
                    return value.ToString();
                }
                if (value is AggregateException)
                {
                    StringBuilder builder = new StringBuilder();
                    Exception innerException = (Exception)value;
                    if ((innerException.Data != null) && (innerException.Data.Count > 0))
                    {
                        builder.Append("异常数据：").Append("\r\n").AppendLine(innerException.Data.JsonSerialize(null));
                    }
                    builder.Append("\r\n").Append("事件信息：").Append("\r\n").AppendLine(innerException.Message);
                    builder.Append("\r\n").Append("堆栈跟踪：").Append("\r\n").AppendLine(innerException.StackTrace);
                    while (innerException.InnerException!=null)
                    {
                        if ((innerException.InnerException.Data != null) && (innerException.InnerException.Data.Count > 0))
                        {
                            builder.Append("内部异常数据：").Append("\r\n").AppendLine(innerException.InnerException.Data.JsonSerialize(null));
                        }
                        builder.Append("\r\n").Append("内部事件信息：").Append("\r\n").AppendLine(innerException.InnerException.Message);
                        builder.Append("\r\n").Append("内部堆栈跟踪：").Append("\r\n").AppendLine(innerException.InnerException.StackTrace);
                        innerException = innerException.InnerException;
                    }
                    builder.Append("聚合异常\r\n");
                    AggregateException exception2 = (AggregateException)value;
                    foreach (Exception exception3 in exception2.InnerExceptions)
                    {
                        builder.Append(exception3.ExceptionToMessage());
                    }
                    return builder.ToString();
                }
                if (value is Exception)
                {
                    StringBuilder builder2 = new StringBuilder();
                    Exception exception4 = (Exception)value;
                    if ((exception4.Data != null) && (exception4.Data.Count > 0))
                    {
                        builder2.Append("异常数据：").Append("\r\n").AppendLine(exception4.Data.JsonSerialize(null));
                    }
                    builder2.Append("\r\n").Append("事件信息：").Append("\r\n").AppendLine(exception4.Message);
                    builder2.Append("\r\n").Append("堆栈跟踪：").Append("\r\n").AppendLine(exception4.StackTrace);
                    while (exception4.InnerException!=null)
                    {
                        if ((exception4.InnerException.Data != null) && (exception4.InnerException.Data.Count > 0))
                        {
                            builder2.Append("内部异常数据：").Append("\r\n").AppendLine(exception4.InnerException.Data.JsonSerialize(null));
                        }
                        builder2.Append("\r\n").Append("内部事件信息：").Append("\r\n").AppendLine(exception4.InnerException.Message);
                        builder2.Append("\r\n").Append("内部堆栈跟踪：").Append("\r\n").AppendLine(exception4.InnerException.StackTrace);
                        exception4 = exception4.InnerException;
                    }
                    return builder2.ToString();
                }
                return value.JsonSerialize(null);
            }
            catch (Exception exception5)
            {
                return ("消息转换出现异常:" + exception5.Message);
            }
        }

        public static string ToText(this object value)
        {
            if (value == null)
            {
                return "";
            }
            if (value is string)
            {
                return ((string)value).ToText();
            }
            if (value.IsList())
            {
                return ((IEnumerable)value).ToText(",", true);
            }
            if (value is DateTime)
            {
                return ((DateTime)value).ToText("yyyy-MM-dd HH:mm:ss");
            }
            return value.ToString();
        }

        // Properties
        public static bool NullBoolean
        {
            get
            {
                return false;
            }
        }

        public static byte NullByte
        {
            get
            {
                return 0;
            }
        }

        public static DateTime NullDate
        {
            get
            {
                return Convert.ToDateTime("1900-01-01 00:00:00");
            }
        }

        public static decimal NullDecimal
        {
            get
            {
                return -79228162514264337593543950335M;
            }
        }

        public static double NullDouble
        {
            get
            {
                return double.MinValue;
            }
        }

        public static float NullFloat
        {
            get
            {
                return float.MinValue;
            }
        }

        public static Guid NullGuid
        {
            get
            {
                return Guid.Empty;
            }
        }

        public static int NullInt
        {
            get
            {
                return -2147483648;
            }
        }

        public static long NullLong
        {
            get
            {
                return -9223372036854775808L;
            }
        }

        public static short NullShort
        {
            get
            {
                return -32768;
            }
        }

        public static float NullSingle
        {
            get
            {
                return float.MinValue;
            }
        }

        public static string NullString
        {
            get
            {
                return string.Empty;
            }
        }
    }

}
