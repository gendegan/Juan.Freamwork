using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    /// <summary>
    /// Console信息帮助类
    /// </summary>
    public static class ConsoleHelper
    {


        /// <summary>
        /// 格式化信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="title">标题</param>
        /// <returns></returns>
        private static string FormatMessage(object message, string title)
        {
            

            return DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss:fff]") + title + "\r\n" + message.ToMessage();
        }
        /// <summary>
        /// 写Console信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="title">标题</param>
        public static void WriteLine(this object message, string title = "")
        {
            Console.WriteLine(FormatMessage(message, title));
        }
        /// <summary>
        /// 写Console信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="title">标题</param>
        public static void WriteLineRed(this object message, string title = "")
        {
            WriteLine(message, ConsoleColor.Red, title);
        }

        /// <summary>
        ///  写Console信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="title">标题</param>
        public static void WriteLineYellow(this object message, string title = "")
        {
            WriteLine(message, ConsoleColor.Yellow, title);
        }
        /// <summary>
        /// 写Console信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="objConsoleColor">颜色</param>
        /// <param name="title">标题</param>
        public static void WriteLine(this object message, ConsoleColor objConsoleColor, string title = "")
        {
            Console.ForegroundColor = objConsoleColor;
            Console.WriteLine(FormatMessage(message, title));
            Console.ResetColor();
        }


    }

}
