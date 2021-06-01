using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;

namespace Juan.Core
{
    /// <summary>
    /// 内存缓存帮助类 
    /// </summary>
    public static class CacheHelper
    {
        /// <summary>
        /// 清理所有的内存缓存
        /// </summary>
        public static void Clear()
        {
            IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                HttpRuntime.Cache.Remove(enumerator.Key.ToString());
            }
        }
        /// <summary>
        /// 获取缓存值 
        /// </summary>
        /// <param name="cacheType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string cacheType, string key)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            return HttpRuntime.Cache.Get(cacheKey);
        }
        /// <summary>
        /// 获取缓存值 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string cacheType, string key)
        {
            object obj2 = Get(cacheType, key);
            if (obj2 == null)
            {
                return default(T);
            }
            return (T)obj2;
        }
        /// <summary>
        /// 获取缓存值 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheType"></param>
        /// <param name="key"></param>
        /// <param name="defalutValue"></param>
        /// <returns></returns>
        public static T Get<T>(string cacheType, string key, T defalutValue)
        {
            object obj2 = Get(cacheType, key);
            if (obj2 == null)
            {
                return defalutValue;
            }
            return (T)obj2;
        }
        /// <summary>
        /// 获取缓存值 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheType"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Get<T>(string cacheType, string key, ref T value)
        {
            object obj2 = Get(cacheType, key);
            if (((T)value) == null)
            {
                value = default(T);
                return false;
            }
            value = (T)obj2;
            return true;
        }
        /// <summary>
        /// 获取当前缓存键值 
        /// </summary>
        /// <param name="cacheType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetCacheKey(string cacheType, string key)
        {
            if (string.IsNullOrWhiteSpace(cacheType))
            {
                throw new ArgumentNullException("cacheType", "请输入缓存类型");
            }
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException("key", "请输入缓存键值");
            }
            return (cacheType + "=" + key);
        }
        /// <summary>
        /// 新增缓存 
        /// </summary>
        /// <param name="cacheType"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Insert(string cacheType, string key, object value)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value);
        }
        /// <summary>
        /// 添加绝对缓存 
        /// </summary>
        /// <param name="cacheType"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="priority"></param>
        public static void Insert(string cacheType, string key, object value, DateTime absoluteExpiration, CacheItemPriority priority = CacheItemPriority.Normal)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, null, absoluteExpiration, Cache.NoSlidingExpiration, priority, null);
        }
        /// <summary>
        /// 添加绝对缓存 
        /// </summary>
        /// <param name="cacheType"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="onUpdateCallback"></param>
        public static void Insert(string cacheType, string key, object value, DateTime absoluteExpiration, CacheItemUpdateCallback onUpdateCallback)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, null, absoluteExpiration, Cache.NoSlidingExpiration, onUpdateCallback);
        }
        /// <summary>
        /// 添加绝对缓存 
        /// </summary>
        /// <param name="cacheType"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="fileName"></param>
        /// <param name="priority"></param>
        public static void Insert(string cacheType, string key, object value, string fileName, CacheItemPriority priority = CacheItemPriority.Normal)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException("参数fileName不能为空");
            }
            CacheDependency dependencies = new CacheDependency(fileName);
            HttpRuntime.Cache.Insert(cacheKey, value, dependencies, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, priority, null);
        }
        /// <summary>
        /// 添加绝对缓存 
        /// </summary>
        /// <param name="cacheType"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="fileName"></param>
        /// <param name="onUpdateCallback"></param>
        public static void Insert(string cacheType, string key, object value, string fileName, CacheItemUpdateCallback onUpdateCallback)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            CacheDependency dependencies = new CacheDependency(fileName);
            HttpRuntime.Cache.Insert(cacheKey, value, dependencies, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, onUpdateCallback);
        }
        /// <summary>
        /// 添加绝对缓存 
        /// </summary>
        /// <param name="cacheType"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="slidingExpiration"></param>
        /// <param name="priority"></param>
        public static void Insert(string cacheType, string key, object value, TimeSpan slidingExpiration, CacheItemPriority priority = CacheItemPriority.Normal)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, null, Cache.NoAbsoluteExpiration, slidingExpiration, priority, null);
        }
        /// <summary>
        /// 添加绝对缓存 
        /// </summary>
        /// <param name="cacheType"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="slidingExpiration"></param>
        /// <param name="onUpdateCallback"></param>
        public static void Insert(string cacheType, string key, object value, TimeSpan slidingExpiration, CacheItemUpdateCallback onUpdateCallback)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, null, Cache.NoAbsoluteExpiration, slidingExpiration, onUpdateCallback);
        }
        /// <summary>
        /// 添加绝对缓存 
        /// </summary>
        /// <param name="cacheType"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dependencies"></param>
        /// <param name="priority"></param>
        public static void Insert(string cacheType, string key, object value, CacheDependency dependencies, CacheItemPriority priority = CacheItemPriority.Normal)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, dependencies, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, priority, null);
        }
        /// <summary>
        /// /添加绝对缓存 
        /// </summary>
        /// <param name="cacheType"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dependencies"></param>
        /// <param name="onUpdateCallback"></param>
        public static void Insert(string cacheType, string key, object value, CacheDependency dependencies, CacheItemUpdateCallback onUpdateCallback)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, dependencies, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, onUpdateCallback);
        }
        /// <summary>
        /// 添加绝对缓存 
        /// </summary>
        /// <param name="cacheType"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="onRemoveCallback"></param>
        /// <param name="priority"></param>
        public static void Insert(string cacheType, string key, object value, DateTime absoluteExpiration, CacheItemRemovedCallback onRemoveCallback, CacheItemPriority priority)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, null, absoluteExpiration, Cache.NoSlidingExpiration, priority, onRemoveCallback);
        }
        /// <summary>
        /// 添加绝对缓存 
        /// </summary>
        /// <param name="cacheType"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="fileName"></param>
        /// <param name="onRemoveCallback"></param>
        /// <param name="priority"></param>
        public static void Insert(string cacheType, string key, object value, string fileName, CacheItemRemovedCallback onRemoveCallback, CacheItemPriority priority = CacheItemPriority.Normal)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException("参数fileName不能为空");
            }
            CacheDependency dependencies = new CacheDependency(fileName);
            HttpRuntime.Cache.Insert(cacheKey, value, dependencies, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, priority, onRemoveCallback);
        }
        /// <summary>
        /// 添加绝对缓存 
        /// </summary>
        /// <param name="cacheType"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="slidingExpiration"></param>
        /// <param name="onRemoveCallback"></param>
        /// <param name="priority"></param>
        public static void Insert(string cacheType, string key, object value, TimeSpan slidingExpiration, CacheItemRemovedCallback onRemoveCallback, CacheItemPriority priority)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, null, Cache.NoAbsoluteExpiration, slidingExpiration, priority, onRemoveCallback);
        }
        /// <summary>
        /// 添加绝对缓存 
        /// </summary>
        /// <param name="cacheType"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dependencies"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        public static void Insert(string cacheType, string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, dependencies, absoluteExpiration, slidingExpiration);
        }
        /// <summary>
        /// 添加绝对缓存 
        /// </summary>
        /// <param name="cacheType"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dependencies"></param>
        /// <param name="onRemoveCallback"></param>
        /// <param name="priority"></param>
        public static void Insert(string cacheType, string key, object value, CacheDependency dependencies, CacheItemRemovedCallback onRemoveCallback, CacheItemPriority priority)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, dependencies, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, priority, onRemoveCallback);
        }
        /// <summary>
        /// 添加绝对缓存 
        /// </summary>
        /// <param name="cacheType"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="dependencies"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="slidingExpiration"></param>
        /// <param name="onUpdateCallback"></param>
        public static void Insert(string cacheType, string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemUpdateCallback onUpdateCallback)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, dependencies, absoluteExpiration, slidingExpiration, onUpdateCallback);
        }
        /// <summary>
        /// 添加绝对缓存 
        /// </summary>
        /// <param name="cacheType">缓存类型</param>
        /// <param name="key">键值</param>
        /// <param name="value">值</param>
        /// <param name="dependencies">依赖项</param>
        /// <param name="absoluteExpiration">绝对时间</param>
        /// <param name="slidingExpiration">相对时间</param>
        /// <param name="priority">优先级</param>
        /// <param name="onRemoveCallback">缓存失效事件</param>
        public static void Insert(string cacheType, string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, dependencies, absoluteExpiration, slidingExpiration, priority, onRemoveCallback);
        }
        /// <summary>
        /// 删除缓存 
        /// </summary>
        /// <param name="cachekey"></param>
        public static void Remove(string cachekey)
        {
            HttpRuntime.Cache.Remove(cachekey);
        }
        /// <summary>
        /// 删除缓存 
        /// </summary>
        /// <param name="cacheType"></param>
        /// <param name="key"></param>
        public static void Remove(string cacheType, string key)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Remove(cacheKey);
        }
        /// <summary>
        /// 删除缓存 
        /// </summary>
        /// <param name="pattern"></param>
        public static void RemoveByPattern(string pattern)
        {
            IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Key.ToString().IsMatch(pattern, RegexOptions.IgnoreCase))
                {
                    HttpRuntime.Cache.Remove(enumerator.Key.ToString());
                }
            }
        }
        /// <summary>
        /// 删除某个缓存类型
        /// </summary>
        /// <param name="cacheType"></param>
        /// .
        public static void RemoveCacheType(string cacheType)
        {
            RemoveByPattern("^" + cacheType.ToString() + "=[^=]+$");
        }

        // Properties
        public static Cache Cache
        {
            get
            {
                return HttpRuntime.Cache;
            }
        }
    }

}