using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    /// <summary>
    /// 对队池事件参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueuePoolExceptionEventArgs<T> : EventArgs where T : class
    {
        /// <summary>
        /// 消息
        /// </summary>
        public T Message
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Exception Exception
        {
            get;
            set;
        }
        /// <summary>
        /// 自动对队
        /// </summary>
        public QueuePoolHelper<T> QueuePool
        {
            get;
            set;
        }
        /// <summary>
        /// 发送调用消息参数 构造函数
        /// </summary>
        /// <param name="queuePool">自身对队</param>
        /// <param name="message">调用消息</param>
        /// <param name="exception">异常信息</param>
        public QueuePoolExceptionEventArgs(QueuePoolHelper<T> queuePool, T message, Exception exception)
        {
            QueuePool = queuePool;
            Message = message;
            Exception = exception;
        }
    }
}
