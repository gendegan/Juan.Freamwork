﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juan.Core
{
    /// <summary>
    /// 打断出处理 
    /// </summary>
    public static class AssertHelper
    {
        // Methods
        public static void ArgumentAssert(this bool condition, string paramName, string message = "")
        {
            if (condition)
            {
                throw new ArgumentException(message, paramName);
            }
        }
        /// <summary>
        /// 参数不为空判断 
        /// </summary>
        /// <param name="argumentValue"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
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
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void ArgumentNoNull(this string argumentValue, string paramName, string message = "")
        {
            if (string.IsNullOrWhiteSpace(argumentValue))
            {
                throw new ArgumentNullException(paramName, message);
            }
        }
        /// <summary>
        /// 参数不为空判断 
        /// </summary>
        /// <param name="argumentValue"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void ArgumentNoNullOrEmpty(this string argumentValue, string paramName, string message = "")
        {
            if (string.IsNullOrEmpty(argumentValue))
            {
                throw new ArgumentNullException(paramName, message);
            }
        }

        public static void Http301Assert(string redirectUrl)
        {
            Http301Assert(true, redirectUrl);
        }

        public static void Http301Assert(bool condition, string redirectUrl)
        {
            if (condition)
            {
                throw new Http301Exception(redirectUrl);
            }
        }

        public static void Http302Assert(string redirectUrl, string message = "临时移动,页面进行跳转")
        {
            Http302Assert(true, redirectUrl, message);
        }

        public static void Http302Assert(bool condition, string redirectUrl, string message = "临时移动,页面进行跳转")
        {
            if (condition)
            {
                throw new Http302Exception(redirectUrl, message);
            }
        }

        public static void Http404Assert(string message = "对不起，此页面不存在")
        {
            Http404Assert(true, message);
        }

        public static void Http404Assert(bool condition, string message = "对不起，此页面不存在")
        {
            if (condition)
            {
                throw new Http404Exception(message);
            }
        }

        public static void Http500Assert(string message = "")
        {
            Http500Assert(true, message);
        }

        public static void Http500Assert(bool condition, string message = "")
        {
            if (condition)
            {
                throw new Http500Exception(message);
            }
        }
        /// <summary>
        /// 信息提示
        /// </summary>
        /// <param name="hintMessage"></param>
        public static void InfoHintAssert(this string hintMessage)
        {
            throw new InfoHintException(hintMessage);
        }
        /// <summary>
        /// 信息提示
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="hintMessage"></param>
        public static void InfoHintAssert(this bool condition, string hintMessage)
        {
            if (condition)
            {
                throw new InfoHintException(hintMessage);
            }
        }
        /// <summary>
        /// 检查前端输入参数 
        /// </summary>
        /// <param name="argumentValue"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void InputNoNull(this object argumentValue, string paramName, string message = "")
        {
            if (argumentValue == null)
            {
                InputNullAssert(paramName, message);
            }
        }

        /// <summary>
        /// 检查前端输入参数 
        /// </summary>
        /// <param name="argumentValue"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
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
        /// <param name="argumentValue"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void InputNoNullOrEmpty(this string argumentValue, string paramName, string message = "")
        {
            if (string.IsNullOrEmpty(argumentValue))
            {
                InputNullAssert(paramName, message);
            }
        }
        /// <summary>
        /// 前端输入参数异常
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void InputNullAssert(string paramName, string message = "")
        {
            throw new InputNullException(paramName, message);
        }
        /// <summary>
        /// 前端输入参数异常
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="paramName"></param>
        /// <param name="message"></param>
        public static void InputNullAssert(bool condition, string paramName, string message = "")
        {
            if (condition)
            {
                throw new InputNullException(paramName, message);
            }
        }

        public static void InvokeAssert(this InvokeResult objInvokeResult)
        {
            throw new InvokeException(objInvokeResult);
        }

        public static void InvokeAssert(string resultCode, string resultMessage, object data = null)
        {
            InvokeResult objInvokeResult = new InvokeResult
            {
                ResultCode = resultCode,
                ResultMessage = resultMessage,
                Data = data
            };
            throw new InvokeException(objInvokeResult);
        }
    }


}
