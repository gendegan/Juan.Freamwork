
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Juan.Core
{
    /// <summary>
    /// 消息发送
    /// </summary>
    public partial class QueuePoolHelper<T>
    {
        /// <summary>
        /// 发送消息事件
        /// </summary>
        public event EventHandler<QueuePoolEventArgs<T>> SendMessage;

        /// <summary>
        /// 发送消息事件异常
        /// </summary>
        public event EventHandler<QueuePoolExceptionEventArgs<T>> SendException;
        void OnSendMessage(T message)
        {
            if (SendMessage != null)
            {
                try
                {
                    SendMessage(this, new QueuePoolEventArgs<T>(this, message));
                }
                catch (Exception objMessageExp)
                {
                    if (SendException != null)
                    {
                        try
                        {
                            SendException(this, new QueuePoolExceptionEventArgs<T>(this, message, objMessageExp));
                        }
                        catch (Exception objSendExp)
                        {
                            EventLogHelper.WriterLog("队列池处理SendException发送异常", objSendExp);
                        }
                    }
                    else
                    {
                        EventLogHelper.WriterLog("队列池SendMessage发送消息异常", objMessageExp);

                    }
                }
            }
        }

        /// <summary>
        /// 读发送消息
        /// </summary>
        /// <returns></returns>
        private T Dequeue()
        {

            T objMessage = null;
            for (int i = 0; i < MessageList.Length; i++)
            {

                if (MessageList[i].TryDequeue(out objMessage))
                {
                    return objMessage;
                }
            }
            return objMessage;
        }
        /// <summary>
        /// 消息对队数
        /// </summary>
        public int MessageQueueCount
        {
            get
            {
                int count = 0;
                for (int i = 0; i < MessageList.Length; i++)
                {
                    count += MessageList[i].Count;
                }
                return count;
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>

        public virtual void ProcessSendMessage()
        {
            while (true)
            {
                T objMessage = Dequeue();
                if (objMessage != null)
                {
                    OnSendMessage(objMessage);
                    if (OneSleepMilliseconds > 0)
                    {
                        Thread.Sleep(OneSleepMilliseconds);
                    }
                }
                else
                {
                    Thread.Sleep(SleepMilliseconds <= 0 ? 1 : SleepMilliseconds);
                }
            }
        }
    }
}
