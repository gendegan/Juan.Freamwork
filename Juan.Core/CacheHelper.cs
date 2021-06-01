using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;

namespace Juan.Core
{
    public static class CacheHelper
    {
        // Methods
        public static void Clear()
        {
            IDictionaryEnumerator enumerator = HttpRuntime.Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                HttpRuntime.Cache.Remove(enumerator.Key.ToString());
            }
        }

        public static object Get(string cacheType, string key)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            return HttpRuntime.Cache.Get(cacheKey);
        }

        public static T Get<T>(string cacheType, string key)
        {
            object obj2 = Get(cacheType, key);
            if (obj2 == null)
            {
                return default(T);
            }
            return (T)obj2;
        }

        public static T Get<T>(string cacheType, string key, T defalutValue)
        {
            object obj2 = Get(cacheType, key);
            if (obj2 == null)
            {
                return defalutValue;
            }
            return (T)obj2;
        }

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

        public static void Insert(string cacheType, string key, object value)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value);
        }

        public static void Insert(string cacheType, string key, object value, DateTime absoluteExpiration, CacheItemPriority priority = CacheItemPriority.Normal)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, null, absoluteExpiration, Cache.NoSlidingExpiration, priority, null);
        }

        public static void Insert(string cacheType, string key, object value, DateTime absoluteExpiration, CacheItemUpdateCallback onUpdateCallback)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, null, absoluteExpiration, Cache.NoSlidingExpiration, onUpdateCallback);
        }

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

        public static void Insert(string cacheType, string key, object value, string fileName, CacheItemUpdateCallback onUpdateCallback)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            CacheDependency dependencies = new CacheDependency(fileName);
            HttpRuntime.Cache.Insert(cacheKey, value, dependencies, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, onUpdateCallback);
        }

        public static void Insert(string cacheType, string key, object value, TimeSpan slidingExpiration, CacheItemPriority priority = CacheItemPriority.Normal)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, null, Cache.NoAbsoluteExpiration, slidingExpiration, priority, null);
        }

        public static void Insert(string cacheType, string key, object value, TimeSpan slidingExpiration, CacheItemUpdateCallback onUpdateCallback)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, null, Cache.NoAbsoluteExpiration, slidingExpiration, onUpdateCallback);
        }

        public static void Insert(string cacheType, string key, object value, CacheDependency dependencies, CacheItemPriority priority = CacheItemPriority.Normal)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, dependencies, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, priority, null);
        }

        public static void Insert(string cacheType, string key, object value, CacheDependency dependencies, CacheItemUpdateCallback onUpdateCallback)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, dependencies, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, onUpdateCallback);
        }

        public static void Insert(string cacheType, string key, object value, DateTime absoluteExpiration, CacheItemRemovedCallback onRemoveCallback, CacheItemPriority priority)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, null, absoluteExpiration, Cache.NoSlidingExpiration, priority, onRemoveCallback);
        }

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

        public static void Insert(string cacheType, string key, object value, TimeSpan slidingExpiration, CacheItemRemovedCallback onRemoveCallback, CacheItemPriority priority)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, null, Cache.NoAbsoluteExpiration, slidingExpiration, priority, onRemoveCallback);
        }

        public static void Insert(string cacheType, string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, dependencies, absoluteExpiration, slidingExpiration);
        }

        public static void Insert(string cacheType, string key, object value, CacheDependency dependencies, CacheItemRemovedCallback onRemoveCallback, CacheItemPriority priority)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, dependencies, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, priority, onRemoveCallback);
        }

        public static void Insert(string cacheType, string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemUpdateCallback onUpdateCallback)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, dependencies, absoluteExpiration, slidingExpiration, onUpdateCallback);
        }

        public static void Insert(string cacheType, string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Insert(cacheKey, value, dependencies, absoluteExpiration, slidingExpiration, priority, onRemoveCallback);
        }

        public static void Remove(string cachekey)
        {
            HttpRuntime.Cache.Remove(cachekey);
        }

        public static void Remove(string cacheType, string key)
        {
            string cacheKey = GetCacheKey(cacheType, key);
            HttpRuntime.Cache.Remove(cacheKey);
        }

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