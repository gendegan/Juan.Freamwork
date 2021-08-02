using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Juan.Core
{
    public static partial class StringHelper
    {


        /// <summary>
        /// 过滤HTML中不同类型的s
        /// </summary>
        /// <param name="html"></param>
        /// <param name="objHtmlTypes"></param>
        /// <returns></returns>
        public static string ClearHtmlFilter(this string html, params HtmlType[] objHtmlTypes)
        {
            if (String.IsNullOrEmpty(html))
            {
                return html;
            }

            System.Text.RegularExpressions.Regex r;
            System.Text.RegularExpressions.Match m;
            if (objHtmlTypes.Contains(HtmlType.Tag) || objHtmlTypes.Length == 0)
            {

                html = Regex.Replace(html, "<[^>]*>", string.Empty, RegexOptions.IgnoreCase);
            }

            if (objHtmlTypes.Contains(HtmlType.Script) || objHtmlTypes.Length == 0)
            {

                //不允许使用javascript,vbscript等，事件onclick,ondlbclick等
                html = Regex.Replace(html, "</?script[^>]*>", string.Empty);
                r = new Regex(@"</?script[^>]*>", RegexOptions.IgnoreCase);
                for (m = r.Match(html); m.Success; m = m.NextMatch())
                {
                    html = html.Replace(m.Groups[0].ToString(), string.Empty);
                }
                r = new Regex(@"(javascript|jscript|vbscript|vbs):", RegexOptions.IgnoreCase);
                for (m = r.Match(html); m.Success; m = m.NextMatch())
                {
                    html = html.Replace(m.Groups[0].ToString(), m.Groups[1].ToString() + "：");
                }
                r = new Regex(@"on(mouse|exit|error|click|key)", RegexOptions.IgnoreCase);
                for (m = r.Match(html); m.Success; m = m.NextMatch())
                {
                    html = html.Replace(m.Groups[0].ToString(), "<I>on" + m.Groups[1].ToString() + "</I>");
                }
                r = new Regex(@"&#", RegexOptions.IgnoreCase);
                for (m = r.Match(html); m.Success; m = m.NextMatch())
                {
                    html = html.Replace(m.Groups[0].ToString(), "<I>&#</I>");
                }

            }

            if (objHtmlTypes.Contains(HtmlType.Table) || objHtmlTypes.Length == 0)
            {
                html = Regex.Replace(html, "</?table[^>]*>", string.Empty);
                html = Regex.Replace(html, "</?tr[^>]*>", string.Empty);
                html = Regex.Replace(html, "</?th[^>]*>", string.Empty);
                html = Regex.Replace(html, "</?td[^>]*>", string.Empty);
            }


            if (objHtmlTypes.Contains(HtmlType.Class) || objHtmlTypes.Length == 0)
            {
                r = new Regex(@"(<[^>]+) class=[^ |^>]*([^>]*>)", RegexOptions.IgnoreCase);
                for (m = r.Match(html); m.Success; m = m.NextMatch())
                {
                    html = html.Replace(m.Groups[0].ToString(), m.Groups[1].ToString() + " " + m.Groups[2].ToString());
                }
            }
            if (objHtmlTypes.Contains(HtmlType.Style) || objHtmlTypes.Length == 0)
            {
                r = new Regex("(<[^>]+) style=\"[^\"]*\"([^>]*>)", RegexOptions.IgnoreCase);
                for (m = r.Match(html); m.Success; m = m.NextMatch())
                {
                    html = html.Replace(m.Groups[0].ToString(), m.Groups[1].ToString() + " " + m.Groups[2].ToString());
                }
            }

            if (objHtmlTypes.Contains(HtmlType.Xml) || objHtmlTypes.Length == 0)
            {
                html = Regex.Replace(html, "<\\?xml[^>]*>", string.Empty);
            }


            if (objHtmlTypes.Contains(HtmlType.Font) || objHtmlTypes.Length == 0)
            {
                html = Regex.Replace(html, "</?font[^>]*>", string.Empty);
            }


            if (objHtmlTypes.Contains(HtmlType.Font) || objHtmlTypes.Length == 0)
            {
                html = Regex.Replace(html, "</?marquee[^>]*>", string.Empty);
            }
            if (objHtmlTypes.Contains(HtmlType.Font) || objHtmlTypes.Length == 0)
            {
                //不允许 object, param, embed 标签，不能嵌入对象
                html = Regex.Replace(html, "</?object[^>]*>", string.Empty);
                html = Regex.Replace(html, "</?param[^>]*>", string.Empty);
                html = Regex.Replace(html, "</?embed[^>]*>", string.Empty);
            }


            return html;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ClearHtmlScript(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "";
            }
            RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Singleline;
            string replacement = " ";

            input = Regex.Replace(input, "<script[^>]*>.*?</script[^><]*>", replacement, options);
            input = Regex.Replace(input, "<input[^>]*>.*?</input[^><]*>", replacement, options);
            input = Regex.Replace(input, "<object[^>]*>.*?</object[^><]*>", replacement, options);
            input = Regex.Replace(input, "<embed[^>]*>.*?</embed[^><]*>", replacement, options);
            input = Regex.Replace(input, "<applet[^>]*>.*?</applet[^><]*>", replacement, options);
            input = Regex.Replace(input, "<form[^>]*>.*?</form[^><]*>", replacement, options);
            input = Regex.Replace(input, "<option[^>]*>.*?</option[^><]*>", replacement, options);
            input = Regex.Replace(input, "<select[^>]*>.*?</select[^><]*>", replacement, options);
            input = Regex.Replace(input, "<iframe[^>]*>.*?</iframe[^><]*>", replacement, options);
            input = Regex.Replace(input, "<ilayer[^>]*>.*?</ilayer[^><]*>", replacement, options);
            input = Regex.Replace(input, "<form[^>]*>", replacement, options);
            input = Regex.Replace(input, "</form[^><]*>", replacement, options);
            input = Regex.Replace(input, "javascript:", replacement, options);
            input = Regex.Replace(input, "vbscript:", replacement, options);


            return input;
        }

        /// <summary>
        /// 去掉HTML中的一些特殊符号nbsp;gt;等等。
        /// </summary>
        /// <param name="html">html</param>
        /// <param name="replacement">是否去除空白</param>
        /// <returns></returns>
        public static string ClearHtmlSymbol(this string html, string replacement = "")
        {
            if (string.IsNullOrWhiteSpace(html))
            {
                return "";
            }

            html = Regex.Replace(html, "&[^;]*;", replacement);
            html = Regex.Replace(html, "\\s+", replacement);

            string punctuationMatch = "[~!#\\$%\\^&*\\(\\)-+=\\{\\[\\}\\]\\|;:\\x22\'<,>\\.\\?\\\\\\t\\r\\v\\f\\n]";
            Regex afterRegEx = new Regex(punctuationMatch + "\\s");
            Regex beforeRegEx = new Regex("\\s" + punctuationMatch);

            while (beforeRegEx.IsMatch(html))
            {
                html = beforeRegEx.Replace(html, replacement);
            }

            while (afterRegEx.IsMatch(html))
            {
                html = afterRegEx.Replace(html, replacement);
            }
            return html;
        }
        /// <summary>
        /// 清理html所有Html标签
        /// </summary>
        /// <param name="html"></param>
        /// <param name="replaceValue"></param>
        /// <returns></returns>
        public static string ClearHtmlTags(this string html, string replaceValue = "")
        {
            if (string.IsNullOrWhiteSpace(html))
            {
                return "";
            }
            return Regex.Replace(html, "<[^>]*>", replaceValue, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 清理所有HTML相关内容
        /// </summary>
        /// <param name="html"></param>
        /// <param name="replaceValue"></param>
        /// <returns></returns>
        public static string ClearHtml(this string html, string replaceValue = "")
        {
            if (string.IsNullOrWhiteSpace(html))
            {
                return "";
            }
            html = html.ClearHtmlScript();
            html = html.ClearHtmlTags(replaceValue);
            html = html.ClearHtmlSymbol();
            return html;
        }
        /// <summary>
        /// 清理换行\r\n标识
        /// </summary>
        /// <param name="html"></param>
        /// <param name="replaceValue"></param>
        /// <returns></returns>
        public static string ClearBreakLine(this string html, string replaceValue = "")
        {
            if (string.IsNullOrWhiteSpace(html))
            {
                return "";
            }
            html = Regex.Replace(html, "\r\n", replaceValue, RegexOptions.IgnoreCase);
            return html;
        }

        /// <summary>
        /// 清理Emoji表情
        /// </summary>
        /// <param name="value"></param>
        /// <param name="replaceValue"></param>
        /// <returns></returns>
        public static string ClearEmoji(this string value, string replaceValue = "")
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return "";
            }
            value = Regex.Replace(value, @"\p{Cs}", replaceValue, RegexOptions.IgnoreCase);
            return value;
        }
    }

}



