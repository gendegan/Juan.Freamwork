using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 打断出处理
    /// </summary>
    public static partial class AssertHelper
    {
        /// <summary>
        /// 参数不为空判断
        /// </summary>
        /// <param name="argumentValue">参数值</param>
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
        /// <param name="argumentValue">参数值</param>
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
        /// <param name="argumentValue">参数值</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="message">提示信息</param>
        public static void ArgumentNoNullOrEmpty(this string argumentValue, string paramName, string message = "")
        {
            if (string.IsNullOrEmpty(argumentValue))
            {
                throw new ArgumentNullException(paramName, message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void ArgumentAssert(this bool condition, string paramName, string message = "")
        {

            if (condition)
            {
                throw new ArgumentException(message, paramName);
            }
        }

        /// <summary>
        /// 检查前端输入参数
        /// </summary>
        /// <param name="argumentValue">值</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="message">信息名称</param>
        public static void InputNoNull(this object argumentValue, string paramName, string message = "")
        {
            if (argumentValue == null)
            {
                InputNullAssert(paramName, message);
            }


        }

        /// <summary>
        /// 检查前端输入参数[null ,Empty,WhiteSpace]
        /// </summary>
        /// <param name="argumentValue">值</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="message">信息名称</param>
        public static void InputNoNull(this string argumentValue, string paramName, string message = "")
        {
            if (string.IsNullOrWhiteSpace(argumentValue))
            {
                InputNullAssert(paramName, message);
            }
        }
        /// <summary>
        /// 检查前端输入参数
        /// </summary>
        /// <param name="argumentValue">值</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="message">信息名称</param>
        public static void InputNoNullOrEmpty(this string argumentValue, string paramName, string message = "")
        {
            if (string.IsNullOrEmpty(argumentValue))
            {
                InputNullAssert(paramName, message);
            }
        }

        /// <summary>
        ///  前端输入参数异常
        /// </summary>
        /// <param name="condition">条件为真异常</param>
        /// <param name="paramName">参数名称</param>
        /// <param name="message">信息名称</param>
        public static void InputNullAssert(bool condition, string paramName, string message = "")
        {
            if (condition)
            {
                throw new InputNullException(paramName, message);
            }
        }
        /// <summary>
        /// 前端输入参数异常
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="message">信息名称</param>
        public static void InputNullAssert(string paramName, string message = "")
        {
            throw new InputNullException(paramName, message);
        }




        /// <summary>
        /// 信息提示
        /// </summary>
        /// <param name="hintMessage">提示信息</param>
        public static void InfoHintAssert(this string hintMessage)
        {

            throw new InfoHintException(hintMessage);
        }

        /// <summary>
        /// 信息提示
        /// </summary>
        /// <param name="condition">条件为真异常</param>
        /// <param name="hintMessage">提示信息</param>
        public static void InfoHintAssert(this bool condition, string hintMessage)
        {

            if (condition)
            {
                throw new InfoHintException(hintMessage);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objInvokeResult"></param>
        public static void InvokeAssert(this InvokeResult objInvokeResult)
        {
            throw new InvokeException(objInvokeResult);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resultCode"></param>
        /// <param name="resultMessage"></param>
        /// <param name="data"></param>
        public static void InvokeAssert(string resultCode, string resultMessage, object data = null)
        {
            throw new InvokeException(new InvokeResult() { ResultCode = resultCode, ResultMessage = resultMessage, Data = data });
        }
    }


}
