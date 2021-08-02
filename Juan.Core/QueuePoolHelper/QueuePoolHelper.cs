
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace Juan.Core
{
    /// <summary>
    /// 能力消息处理池
    /// </summary>
    public partial class QueuePoolHelper<T> where T : class
    {


        private ConcurrentQueue<T>[] _MessageList;
        /// <summary>
        /// 消息发送池
        /// </summary>
        public ConcurrentQueue<T>[] MessageList
        {
            get
            {
                return _MessageList;
            }

        }

        /// <summary>
        /// 对队池数
        /// </summary>
        public int PoolCount
        {
            get;
            set;
        }
        /// <summary>
        /// 休眠时间
        /// </summary>
        public int SleepMilliseconds
        {
            get;
            set;
        }
        /// <summary>
        /// 一次执行时间
        /// </summary>
        public int OneSleepMilliseconds
        {
            get;
            set;
        }
        Thread _ProcessThread = null;
        Random _ReceivedRandom = new Random();
        /// <summary>
        /// 对队池构造函数
        /// </summary>
        /// <param name="poolCount"></param>
        public QueuePoolHelper(int poolCount = 1)
        {
            PoolCount = poolCount;
            OneSleepMilliseconds = 0;
            _MessageList = new ConcurrentQueue<T>[poolCount];

            for (int i = 0; i < poolCount; i++)
            {
                _MessageList[i] = new ConcurrentQueue<T>();
            }

        }


        /// <summary>
        /// 开始监听处理
        /// </summary>
        /// <param name="sleepMilliseconds">没有数据休眠时间</param>
        /// <param name="oneSleepMilliseconds">执行一次休眠时间</param>
        public void StartProcess(int sleepMilliseconds = 1, int oneSleepMilliseconds = 0)
        {
            SleepMilliseconds = sleepMilliseconds;
            OneSleepMilliseconds = oneSleepMilliseconds;
            if (SendMessage == null)
            {
                throw new ArgumentNullException("未注册SendMessage事件，因此无法处理");
            }
            if (_ProcessThread == null)
            {
                _ProcessThread = new Thread(new ThreadStart(this.ProcessSendMessage));
                _ProcessThread.Start();
            }
        }
    }
}
