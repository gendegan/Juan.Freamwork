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
        /// Http404
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        public static void Http404Assert(bool condition, string message = "对不起，此页面不存在")
        {
            if (condition)
            {
                throw new Http404Exception(message);
            }
        }
        /// <summary>
        /// Http404
        /// </summary>
        /// <param name="message"></param>
        public static void Http404Assert(string message = "对不起，此页面不存在")
        {
            Http404Assert(true, message);
        }

        /// <summary>
        /// Http500
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="message"></param>
        public static void Http500Assert(bool condition, string message = "")
        {
            if (condition)
            {
                throw new Http500Exception(message);
            }
        }
        /// <summary>
        /// Http500
        /// </summary>
        /// <param name="message"></param>
        public static void Http500Assert(string message = "")
        {
            Http500Assert(true, message);
        }


        /// <summary>
        /// Http301
        /// </summary>
        /// <param name="redirectUrl"></param>
        public static void Http301Assert(string redirectUrl)
        {
            Http301Assert(true, redirectUrl);
        }
        /// <summary>
        /// Http301
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="redirectUrl"></param>
        public static void Http301Assert(bool condition, string redirectUrl)
        {
            if (condition)
            {
                throw new Http301Exception(redirectUrl);
            }
        }


        /// <summary>
        ///  Http302
        /// </summary>
        /// <param name="redirectUrl"></param>
        /// <param name="message"></param>
        public static void Http302Assert(string redirectUrl, string message = "临时移动,页面进行跳转")
        {
            Http302Assert(true, redirectUrl, message);
        }
        /// <summary>
        ///  Http302
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="redirectUrl"></param>
        /// <param name="message"></param>
        public static void Http302Assert(bool condition, string redirectUrl, string message = "临时移动,页面进行跳转")
        {
            if (condition)
            {
                throw new Http302Exception(redirectUrl, message);
            }
        }

    }
}
