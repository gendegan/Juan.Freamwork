using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    /// <summary>
    /// Dictionary帮助类
    /// </summary>
    public static partial class DictionaryHelper
    {

        /// <summary>
        ///  尝试将键和值添加到字典中：如果不存在，才添加；存在，不添加也不抛导常
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (!dict.ContainsKey(key))
                dict.Add(key, value);
            return dict;
        }
        /// <summary>
        ///  将键和值添加或替换到字典中：如果不存在，则添加；存在，则更新
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> AddOrUpdate<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            dict[key] = value;
            return dict;
        }

        /// <summary>
        /// 获取与指定的键相关联的值，如果没有则返回输入的默认值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue)
        {
            return dict.ContainsKey(key) ? dict[key] : defaultValue;
        }
        /// <summary>
        /// 向字典中批量添加键值对
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="values"></param>
        /// <param name="updateExisted">如果已存在，是否更新</param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> values, bool updateExisted = true)
        {

            foreach (var item in values)
            {
                if (!dict.ContainsKey(item.Key) || updateExisted)
                    dict[item.Key] = item.Value;
            }
            return dict;

        }

        /// <summary>
        /// 合并俩个字典
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictOne"></param>
        /// <param name="dictTwo"></param>
        /// <param name="updateExisted">第一个存在，是否更新第一个值</param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> MergeDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dictOne, IEnumerable<KeyValuePair<TKey, TValue>> dictTwo, bool updateExisted = true)
        {
            Dictionary<TKey, TValue> objDictionaryNew = new Dictionary<TKey, TValue>();
            foreach (var item in dictOne)
            {
                objDictionaryNew[item.Key] = item.Value;
            }

            foreach (var item in dictTwo)
            {
                if (!objDictionaryNew.ContainsKey(item.Key) || updateExisted)
                    objDictionaryNew[item.Key] = item.Value;
            }
            return objDictionaryNew;

        }

       

    }
}
