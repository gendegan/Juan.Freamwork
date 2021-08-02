using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JuanGenerate
{
    /// <summary>
    /// 打断出处理
    /// </summary>
    public static partial class AssertHelper
    {
        /// <summary>
        /// 参数不为空判断
        /// </summary>
        /// <param name="argumentValue"></param>
        /// <param name="paramName">参数名称</param>
        /// <param name="message">提示信息</param>
        public static void ArgumentNoNull(this object argumentValue, string paramName, string message = "")
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(paramName, message);
            }
        }


        /// <summary>
        /// 参数不为空判断[null ,Empty,WhiteSpace]
        /// </summary>
        /// <param name="argumentValue"></param>
        /// <param name="paramName">参数名称</param>
        /// <param name="message">提示信息</param>
        public static void ArgumentNoNull(this string argumentValue, string paramName, string message = "")
        {
            if (string.IsNullOrWhiteSpace(argumentValue))
            {
                throw new ArgumentNullException(paramName, message);
            }
        }
        /// <summary>
        ///  参数不为空判断
        /// </summary>
        /// <param name="argumentValue"></param>
        /// <param name="paramName">参数名称</param>
        /// <param name="message">提示信息</param>
        public static void ArgumentNoNullOrEmpty(this string argumentValue, string paramName, string message = "")
        {
            if (string.IsNullOrEmpty(argumentValue))
            {
                throw new ArgumentNullException(paramName, message);
            }
        }

 


    }
}
