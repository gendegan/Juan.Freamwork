using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    public static class ListHelper
    {
        // Methods
        public static bool Contains<T>(this IEnumerable array, T value)
        {
            foreach (T local in array)
            {
                if (local.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsList(this object value)
        {
            return ((value is IEnumerable) && !(value is string));
        }

        public static bool IsNoNull(this IEnumerable list)
        {
            return !list.IsNull();
        }

        public static bool IsNull(this IEnumerable list)
        {
            if (list != null)
            {
                IEnumerator enumerator = list.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    return false;
                }
            }
            return true;
        }

        public static IEnumerable<T> TakeMod<T>(this IEnumerable<T> objList, int modCount)
        {
            int num = objList.Count<T>();
            int num2 = num % modCount;
            if (num2 == 0)
            {
                return objList;
            }
            return objList.Take<T>((num - num2));
        }

        public static IEnumerable<T> TakeMod<T>(this List<T> objList, int modCount)
        {
            int num = objList.Count<T>();
            int num2 = num % modCount;
            if (num2 == 0)
            {
                return objList;
            }
            return objList.Take<T>((num - num2));
        }

        public static T[] ToArray<T>(this IEnumerable value)
        {
            return value.ToList<T>().ToArray();
        }

        public static int[] ToArrayInt(this IEnumerable value)
        {
            return value.ToArray<int>();
        }

        public static long[] ToArrayInt64(this IEnumerable value)
        {
            return value.ToArray<long>();
        }

        public static string[] ToArrayString(this IEnumerable value)
        {
            return value.ToArray<string>();
        }

        public static List<List<T>> ToCartesian<T>(this List<List<T>> objList)
        {
            int num;
            int count = 1;
            objList.ForEach(delegate (List<T> item)
            {
                count *= item.Count;
            });
            List<List<T>> list = new List<List<T>>();
            for (int i = 0; i < count; i = num)
            {
                List<T> lstTemp = new List<T>();
                int j = 1;
                objList.ForEach(delegate (List<T> item)
                {
                    j *= item.Count;
                    lstTemp.Add(item[(i / (count / j)) % item.Count]);
                });
                list.Add(lstTemp);
                num = i + 1;
            }
            return list;
        }

        public static string ToConcat(this IEnumerable value, string split = ",", bool isRemoveEmpty = true)
        {
            if (value == null)
            {
                return "";
            }
            if (value is string)
            {
                return (string)value;
            }
            StringBuilder builder = new StringBuilder(0x100);
            IEnumerator enumerator = value.GetEnumerator();
            while (enumerator.MoveNext())
            {
                string str2 = (enumerator.Current == null) ? "" : enumerator.Current.ToString();
                if (isRemoveEmpty)
                {
                    if (!string.IsNullOrWhiteSpace(str2))
                    {
                        builder.Append(str2 + split);
                    }
                }
                else
                {
                    builder.Append(str2 + split);
                }
            }
            return builder.ToString().Trim(split);
        }

        public static DataTable ToDataTable(this IEnumerable<Dictionary<string, object>> data)
        {
            DataTable table = new DataTable();
            if (data.Count<Dictionary<string, object>>() > 0)
            {
                foreach (Dictionary<string, object> dictionary in data)
                {
                    if (dictionary.Keys.Count == 0)
                    {
                        return table;
                    }
                    foreach (string str in dictionary.Keys)
                    {
                        if ((table.Columns[str] == null) && (dictionary[str] != null))
                        {
                            table.Columns.Add(str, dictionary[str].GetType());
                        }
                    }
                    DataRow row = table.NewRow();
                    foreach (string str2 in dictionary.Keys)
                    {
                        if (table.Columns.Contains(str2))
                        {
                            row[str2] = dictionary[str2];
                        }
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<Dictionary<string, T>> data)
        {
            DataTable table = new DataTable();
            if (data.Count<Dictionary<string, T>>() > 0)
            {
                foreach (Dictionary<string, T> dictionary in data)
                {
                    if (dictionary.Keys.Count == 0)
                    {
                        return table;
                    }
                    foreach (string str in dictionary.Keys)
                    {
                        if ((table.Columns[str] == null) && (dictionary[str] != null))
                        {
                            T local = dictionary[str];
                            table.Columns.Add(str, local.GetType());
                        }
                    }
                    DataRow row = table.NewRow();
                    foreach (string str2 in dictionary.Keys)
                    {
                        if (table.Columns.Contains(str2))
                        {
                            row[str2] = dictionary[str2];
                        }
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> value) where T : class, new()
        {
            List<PropertyInfo> pList = new List<PropertyInfo>();
            Type type = typeof(T);
            DataTable dt = new DataTable();
            Array.ForEach<PropertyInfo>(type.GetProperties(), delegate (PropertyInfo p)
            {
                pList.Add(p);
                dt.Columns.Add(p.Name, p.PropertyType);
            });
            using (IEnumerator<T> enumerator = value.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    T item = enumerator.Current;
                    DataRow row = dt.NewRow();
                    pList.ForEach(delegate (PropertyInfo p)
                    {
                        row[p.Name] = p.GetValue(item, null);
                    });
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        public static Dictionary<string, object> ToDictionary<T>(this T value) where T : class
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo info in properties)
            {
                if (info.CanRead)
                {
                    dictionary.Add(info.Name, info.GetValue(value, null));
                }
            }
            return dictionary;
        }

        public static List<Dictionary<string, object>> ToDictionary<T>(this IEnumerable<T> valueList) where T : class
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (T local in valueList)
            {
                Dictionary<string, object> item = new Dictionary<string, object>();
                foreach (PropertyInfo info in properties)
                {
                    if (info.CanRead)
                    {
                        item.Add(info.Name, info.GetValue(local, null));
                    }
                }
                list.Add(item);
            }
            return list;
        }

        public static SortedDictionary<string, object> ToDictionarySorted<T>(this T value) where T : class
        {
            SortedDictionary<string, object> dictionary = new SortedDictionary<string, object>();
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo info in properties)
            {
                if (info.CanRead)
                {
                    dictionary.Add(info.Name, info.GetValue(value, null));
                }
            }
            return dictionary;
        }

        public static List<T> ToList<T>(this IEnumerable value)
        {
            List<T> list = new List<T>();
            if (value != null)
            {
                if (value is string)
                {
                    return value.ToString().ToList<T>(true, ',');
                }
                IEnumerator enumerator = value.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    list.Add(enumerator.Current.To<T>());
                }
            }
            return list;
        }

        public static List<int> ToListInt(this IEnumerable value)
        {
            return value.ToList<int>();
        }

        public static List<long> ToListInt64(this IEnumerable value)
        {
            return value.ToList<long>();
        }

        public static List<string> ToListString(this IEnumerable value)
        {
            return value.ToList<string>();
        }

        public static string ToText(this IEnumerable value, string split = ",", bool isRemoveEmpty = true)
        {
            return value.ToConcat(split, isRemoveEmpty);
        }
    }
}
