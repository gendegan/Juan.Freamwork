using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 正则帮助类 
    /// </summary>
    public static class RegexHelper
    {
        /// <summary>
        /// 验证符串是否包含中文 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsChinese(this string value)
        {
            return value.IsMatch(@"[\u4e00-\u9fa5]+", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 验证符串是否全部是中文 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsChineseAll(this string value)
        {
            return value.IsMatch(@"^[\u4e00-\u9fa5]*$", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 是否为日期+时间型字符串 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsDateTime(this string source)
        {
            return Regex.IsMatch(source, @"^(((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$ ");
        }
        /// <summary>
        /// 是否包含双字节字符(允许有单字节字符) 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsDoubleChar(this string source)
        {
            return Regex.IsMatch(source, @"[^\x00-\xff]");
        }
        /// <summary>
        /// 验证邮件地址是否合法 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsEmail(this string email)
        {
            string pattern = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            return email.IsMatch(pattern, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 验证符串是否包含英语 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEnglish(this string value)
        {
            return value.IsMatch("[A-Za-z]+", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 验证符串是否全部是英语 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEnglishAll(this string value)
        {
            return value.IsMatch("^[A-Za-z]*$", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 是否文件名 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsFileName(this string fileName)
        {
            return fileName.IsMatch("^[^\\/\\\\ <> \\*\\?\\: \"\\|]{1,16}\\.\\w{1,5}$", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 是否包含Html标签 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static bool IsHtmlTags(this string input)
        {
            RegexOptions options = RegexOptions.Singleline | RegexOptions.IgnoreCase;
            string pattern = "<[^<>]*>";
            return Regex.IsMatch(input, pattern, options);
        }
        /// <summary>
        /// 判断字符串是否是一个合法的IP地址。
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(this string ip)
        {
            return ip.IsMatch(@"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 检查字符串是否为A-Z、0-9及下划线以内的字符 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsLetterOrNumber(this string str)
        {
            return str.IsMatch("^[a-zA-Z0-9_]+$", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 正则表达式验证 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool IsMatch(this string input, string pattern, RegexOptions options = RegexOptions.IgnoreCase)
        {
            Regex regex = new Regex(pattern, options);
            return regex.IsMatch(input);
        }
        /// <summary>
        /// 判断输入的字符串是否是一个合法的手机号 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsMobilePhone(this string input)
        {
            return input.IsMatch(@"^[1][3-9]\d{9}$", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 配非负整数 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNotNagtive(this string input)
        {
            Regex regex = new Regex(@"^\d+$");
            return regex.IsMatch(input);
        }
        /// <summary>
        /// 验证是否全部是数字（0~9组成）
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsNumber(this string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                return false;
            }
            Regex regex = new Regex(@"^\d*$");
            return regex.IsMatch(number);
        }
        /// <summary>
        /// 验证是否全部是数字（0~9组成）,且位数小于等于指定位数 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static bool IsNumber(this string number, int length)
        {
            return number.IsNumber(length, false);
        }
        /// <summary>
        /// 验证是否全部是数字（0~9组成）,且有长度限制 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="length"></param>
        /// <param name="mustEqual"></param>
        /// <returns></returns>
        public static bool IsNumber(this string number, int length, bool mustEqual)
        {
            Regex regex;
            if (string.IsNullOrEmpty(number))
            {
                return false;
            }
            if (mustEqual)
            {
                regex = new Regex(@"^\d{" + length + "}$");
            }
            else
            {
                regex = new Regex(@"^\d{0," + length + "}$");
            }
            return regex.IsMatch(number);
        }
        /// <summary>
        /// 匹配3位或4位区号的电话号码，其中区号可以用小括号括起来， 也可以不用，区号与本地号间可以用连字号或空格间隔， 也可以没有间隔 \(0\d{2}\)[- ]?\d{8}|0\d{2}[- ]?\d{8}|\(0\d{3}\)[- ]?\d{7}|0\d{3}[- ]?\d{7} 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsPhone(this string input)
        {
            string pattern = @"^\(0\d{2}\)[- ]?\d{8}$|^0\d{2}[- ]?\d{8}$|^\(0\d{3}\)[- ]?\d{7}$|^0\d{3}[- ]?\d{7}$";
            return input.IsMatch(pattern, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 验证路径是绝对路径（返回true），还是相对路径（返回false） 不支持网络路径 
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static bool IsPhysicalPath(this string Path)
        {
            if (string.IsNullOrEmpty(Path))
            {
                return false;
            }
            string pattern = @"\b[a-z]:\\.*";
            return (Regex.Matches(Path, pattern, RegexOptions.IgnoreCase).Count > 0);
        }
        /// <summary>
        /// 验证实数,可以带小数或不带小数，不允许逗号 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsRealNoComma(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            return str.IsMatch(@"^\d+(\.\d+)?$", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 验证实数,可以带小数或不带小数，允许3位数字1个逗号的形式 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsRealWithComma(this string str)
        {
            return (str.IsRealNoComma() || str.IsMatch(@"^(\+|-)?\d{1,3}(,\d{3})*(\.\d+)?$", RegexOptions.IgnoreCase));
        }
        /// <summary>
        /// 验输入字符串是否含有“/\:.?*|$]”特殊字符 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsSpecialChar(this string source)
        {
            Regex regex = new Regex(@"[/\<>:.?*|$]");
            return regex.IsMatch(source);
        }

        /// <summary>
        /// 验证字符串是否只包含:字母和数字,以及如下4个符号 @.-_ 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsString(this string str)
        {
            string pattern = @"[^\w\.@-]";
            return !Regex.IsMatch(str, pattern);
        }

        /// <summary>
        /// 匹配正整数
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsUint(this string input)
        {
            Regex regex = new Regex("^[0-9]*[1-9][0-9]*$");
            return regex.IsMatch(input);
        }

        /// <summary>
        /// 是否Unicode编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsUnicode(this string input)
        {
            return input.IsMatch(@"[\u4E00-\u9FA5\uE815-\uFA29]+", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 是否url 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsUrl(this string input)
        {
            string pattern = @"^[a-zA-Z]+://(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?$";
            return input.IsMatch(pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 获取正则所有匹配项 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static MatchCollection Matches(this string input, string pattern, RegexOptions options = RegexOptions.IgnoreCase)
        {
            return Regex.Matches(input, pattern, options);
        }

        /// <summary>
        /// 获取正则匹配组的值
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static GroupCollection MatchGroups(this string input, string pattern, RegexOptions options = RegexOptions.IgnoreCase)
        {
            Regex regex = new Regex(pattern, options);
            return regex.Match(input).Groups;
        }

        /// <summary>
        /// 获取正则匹配组的值
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string MatchValue(this string input, string pattern, RegexOptions options = RegexOptions.IgnoreCase)
        {
            Match match = new Regex(pattern, options).Match(input);
            if (match.Success)
            {
                return match.Value;
            }
            return "";
        }

        /// <summary>
        /// 获取正则匹配组的值
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="groupName"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string MatchValue(this string input, string pattern, string groupName, RegexOptions options = RegexOptions.IgnoreCase)
        {
            Match match = new Regex(pattern, options).Match(input);
            if (match.Success)
            {
                return match.Groups[groupName].Value;
            }
            return "";
        }

        /// <summary>
        /// 从输入字符串中的第一个字符开始，用指定的替换字符串替换由指定的正则表达式定义的模式的所有匹配项。可指定选项来修改匹配的行为。 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string Replace(this string input, string pattern, string replacement, RegexOptions options)
        {
            if (input.IsNullOrEmpty())
            {
                return input;
            }
            return Regex.Replace(input, pattern, replacement, options);
        }

        /// <summary>
        /// 验证密码强度 0、极弱：长度6位以下 1、弱 ：全部小写、全部大写、全部数字 2、中 ：全部是特殊字符，或者 含全部小写、全部大写、全部数字、特殊字符 其中任意2种 3、强 ：含全部小写、全部大写、全部数字、特殊字符 其中任意3种 4、超强：以上情况以外的密码 
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static int ValidatePassword(this string pwd)
        {
            if (string.IsNullOrEmpty(pwd) || (pwd.Length < 6))
            {
                return 0;
            }
            string str = "~!@#%&_`=;':,/<>\\-\\]\\}\\.\\$\\^\\{\\[\\(\\|\\)\\*\\+\\?\\\\\"";
            string[] strArray = new string[] { "^[a-z]+$", "^[A-Z]+$", "^[0-9]+$" };
            foreach (string str2 in strArray)
            {
                if (Regex.IsMatch(pwd, str2))
                {
                    return 1;
                }
            }
            string[] strArray2 = new string[] { "^[a-zA-Z]+$", "^[a-z0-9]+$", "^[A-Z0-9]+$", "^[a-z" + str + "]+$", "^[0-9" + str + "]+$", "^[A-Z" + str + "]+$" };
            foreach (string str3 in strArray2)
            {
                if (Regex.IsMatch(pwd, str3))
                {
                    return 2;
                }
            }
            string[] strArray3 = new string[] { "^[a-zA-Z0-9]+$", "^[a-zA-Z" + str + "]+$", "^[a-z0-9" + str + "]+$", "^[0-9A-Z" + str + "]+$" };
            foreach (string str4 in strArray3)
            {
                if (Regex.IsMatch(pwd, str4))
                {
                    return 3;
                }
            }
            return 4;
        }
    }



}
