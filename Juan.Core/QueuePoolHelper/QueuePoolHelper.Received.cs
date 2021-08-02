
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Juan.Core
{
    /// <summary>
    /// 消息接收
    /// </summary>
    public partial class QueuePoolHelper<T>
    {

        /// <summary>
        /// 消息接收
        /// </summary>
        /// <param name="invokeMessage"></param>
        public virtual void Enqueue(T invokeMessage)
        {
            int PoolIndex = _ReceivedRandom.Next(0, PoolCount);
            MessageList[PoolIndex].Enqueue(invokeMessage);
        }

        /// <summary>
        /// 消息接收
        /// </summary>
        /// <param name="invokeMessage"></param>
        /// <param name="PoolIndex"></param>
        public virtual void Enqueue(T invokeMessage, int PoolIndex)
        {
            if (PoolIndex >= MessageList.Length)
            {
                throw new ArgumentNullException("超过消息池索引请传入0-" + MessageList.Length + "值");
            }
            MessageList[PoolIndex].Enqueue(invokeMessage);
        }

    }
}
