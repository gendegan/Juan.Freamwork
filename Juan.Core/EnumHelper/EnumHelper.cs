using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    public static class EnumHelper
    {
        private static readonly ConcurrentDictionary<string, List<EnumInfo>> _EnumList = new ConcurrentDictionary<string, List<EnumInfo>>();

        public static string GetDescription(this Enum objEnum)
        {
            EnumInfo info = objEnum.GetType().GetEnumMembers().FirstOrDefault<EnumInfo>(p => p.Key == objEnum.ToString());
            if (info !=null)
            {
                return info.Description;
            }
            return string.Empty;
        }

        public static string GetEnumDescription(this Type type, int value)
        {
            EnumInfo info = type.GetEnumMembers().FirstOrDefault<EnumInfo>(p => p.Value == value);
            if (info !=null)
            {
                return info.Description;
            }
            return string.Empty;
        }

        public static string GetEnumDescription(this Type enumType, string objEnumKey)
        {
            EnumInfo info = enumType.GetEnumMembers().FirstOrDefault<EnumInfo>(p => p.Key == objEnumKey);
            if (info !=null)
            {
                return info.Description;
            }
            return string.Empty;
        }

        public static List<EnumInfo> GetEnumMembers(this Type enumType)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException("type", "输入的枚举类型为空");
            }
            string key = enumType.ToString();
            return _EnumList.GetOrAdd(key, delegate (string typeKey)
            {
                List<EnumInfo> list = new List<EnumInfo>();
                foreach (string str in Enum.GetNames(enumType))
                {
                    foreach (Attribute attribute in enumType.GetField(str).GetCustomAttributes(typeof(EnumAttribute), false))
                    {
                        EnumAttribute attribute2 = attribute as EnumAttribute;
                        if (attribute2 !=null)
                        {
                            EnumInfo item = new EnumInfo
                            {
                                Key = str,
                                Description = attribute2.Description
                            };
                            if (attribute2.Value == -2147483648)
                            {
                                item.Value = Convert.ToInt32(Enum.Parse(enumType, str));
                            }
                            else
                            {
                                item.Value = attribute2.Value;
                            }
                            list.Add(item);
                            break;
                        }
                    }
                }
                return list;
            });
        }

        private static string GetShowText(EnumInfo objEnumInfo, ShowEnumType show)
        {
            if (show == ShowEnumType.Key)
            {
                return objEnumInfo.Key;
            }
            if (show == ShowEnumType.Description)
            {
                return objEnumInfo.Description;
            }
            return objEnumInfo.Value.ToString();
        }

        public static T ToEnum<T>(this int value, bool ignoreCase = true)
        {
            return value.ToString().ToEnum<T>(ignoreCase);
        }

        public static T ToEnum<T>(this string value, bool ignoreCase = true)
        {
            return (T)Enum.Parse(typeof(T), value.Trim(), ignoreCase);
        }

    }
}
