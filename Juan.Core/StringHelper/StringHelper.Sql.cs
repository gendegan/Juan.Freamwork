using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Juan.Core
{
    public static partial class StringHelper
    {
        /// <summary>
        /// SQL语句关键字
        /// </summary>
        ///左边关键字
        public const string SqlKeyLeft = @"and |or |exec |execute |insert |select |delete |union |update |alter |create |drop |count |\* |chr |char |limit |asc |mid |'%|%'|substring |master |truncate |declare |xp_cmdshell |xp_ |sp_ |restore |backup |net +user |net +localgroup +administrators";
        /// <summary>
        /// 右边关键字
        /// </summary>
        public const string SqlKeyRight = @" and| or| exec| execute| insert| select| delete| union| update| alter| create| drop| count|\*|chr\(|char\(| limit| asc| mid| substring| master| truncate| declare| xp_cmdshell| xp_| sp_| restore| backup| net +user| net +localgroup +administrators";
        /// <summary>
        /// 总SQL语句
        /// </summary>
        public const string SqlKey = SqlKeyLeft + "|" + SqlKeyRight;
        #region SQL语句过滤
        /// <summary>
        ///  原始SQL语句
        /// </summary>
        /// <param name="sqlString"> 原始SQL语句</param>
        /// <returns>过滤后的SQL语句</returns>
        public static string FilterSql(this string sqlString)
        {
            if (sqlString == null)
            {
                return "";
            }
            sqlString = sqlString.Replace("'", "''");
            sqlString = sqlString.Replace(";", "");
            sqlString = sqlString.Replace("(", "（");
            sqlString = sqlString.Replace(")", "）");
            sqlString = sqlString.Replace("0x", "0 x");
            sqlString = sqlString.FilterSqlKey();
            return sqlString;
        }
        #endregion

        /// <summary>
        /// 过虑SQL关键字
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public static string FilterSqlKey(this string sqlString)
        {
            if (sqlString == null)
            {
                return "";
            }
            sqlString = sqlString.Replace(SqlKey, "", RegexOptions.IgnoreCase);

            return sqlString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlString"></param>
        /// <returns></returns>
        public static string FilterSqlSpecialKey(this string sqlString)
        {
            if (sqlString.IsNullOrWhiteSpace())
            {
                return "";
            }

            sqlString = sqlString.Replace(@"\[", @"\[".MD5Encrypt());
            sqlString = sqlString.Replace(@"\%", @"\%".MD5Encrypt());
            sqlString = sqlString.Replace(@"\_", @"\_".MD5Encrypt());
            sqlString = sqlString.Replace(@"\^", @"\^".MD5Encrypt());
            sqlString = sqlString.Replace("[", @"\[");
            sqlString = sqlString.Replace("%", @"\%");
            sqlString = sqlString.Replace("_", @"\_");
            sqlString = sqlString.Replace("^", @"\^");
            sqlString = sqlString.Replace(@"\[".MD5Encrypt(), @"\[");
            sqlString = sqlString.Replace(@"\%".MD5Encrypt(), @"\%");
            sqlString = sqlString.Replace(@"\_".MD5Encrypt(), @"\_");
            sqlString = sqlString.Replace(@"\^".MD5Encrypt(), @"\^");
            sqlString = sqlString.ClearEmoji();
            return sqlString;
        }



        #region 判断字符串中是否有SQL攻击代码


        /// <summary>
        /// 判断字符串中是否有SQL攻击代码
        /// </summary>
        /// <param name="inputString">用户提交的数据</param>
        /// <param name="sqlStr">SQL攻击代码过滤规则(and|or|exec|execute|insert|select|delete|update|alter|create|drop|count|/add|chr|char|nchar|unicode|substring|abc|asc|mid|'%|substring|master|truncate|declare|xp_cmdshell|restore|backup|net +user|net +localgroup +administrators)</param>
        /// <returns>true-安全；false-有注入攻击；</returns>
        public static bool CheckSqlStr(this string inputString, string sqlStr = "")
        {
            if (String.IsNullOrEmpty(sqlStr))
            {
                sqlStr = SqlKey;
            }
            try
            {
                inputString = inputString.Trim();
                if (!String.IsNullOrEmpty(inputString))
                {
                    string str_Regex = @"\b(" + sqlStr + @")\b";
                    Regex Regex = new Regex(str_Regex, RegexOptions.IgnoreCase);
                    if (Regex.IsMatch(inputString))
                        return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
