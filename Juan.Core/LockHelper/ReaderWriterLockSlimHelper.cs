using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;

namespace Juan.Core
{

    /// <summary>
    /// KEY锁帮助类
    /// </summary>
    public partial class ReaderWriterLockSlimHelper
    {


        Dictionary<string, ReaderWriterLockSlim> _ReaderWriterLockSlimList = new Dictionary<string, ReaderWriterLockSlim>();

        /// <summary>
        /// 创建锁
        /// </summary>
        /// <param name="lockKey"></param>
        /// <returns></returns>
        public ReaderWriterLockSlim CreateLock(string lockKey)
        {
            if (string.IsNullOrWhiteSpace(lockKey))
            {
                throw new ArgumentNullException("参数lockKey不能为空");
            }
            lock (_ReaderWriterLockSlimList)
            {
                if (_ReaderWriterLockSlimList.ContainsKey(lockKey))
                {
                    return _ReaderWriterLockSlimList[lockKey];
                }
                else
                {
                    ReaderWriterLockSlim objReaderWriterLockSlim = new ReaderWriterLockSlim();
                    _ReaderWriterLockSlimList[lockKey] = objReaderWriterLockSlim;
                    return objReaderWriterLockSlim;
                }
            }
        }
        /// <summary>
        /// 删除Lock
        /// </summary>
        /// <param name="readerWriterLockSlim"></param>
        /// <param name="lockKey"></param>
        public void RemoveLock(ReaderWriterLockSlim readerWriterLockSlim, string lockKey)
        {
            if (string.IsNullOrWhiteSpace(lockKey))
            {
                throw new ArgumentNullException("参数lockKey不能为空");
            }
            if (string.IsNullOrWhiteSpace(lockKey))
            {
                throw new ArgumentNullException("参数lockKey不能为空");
            }
            lock (_ReaderWriterLockSlimList)
            {
                if (readerWriterLockSlim.WaitingReadCount == 0 && readerWriterLockSlim.WaitingWriteCount == 0 && readerWriterLockSlim.WaitingUpgradeCount == 0)
                {
                    _ReaderWriterLockSlimList.Remove(lockKey);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lockKey"></param>
        [Obsolete("此方法已经暂停使用", true)]
        public void RemoveLock(string lockKey)
        {
            if (string.IsNullOrWhiteSpace(lockKey))
            {
                throw new ArgumentNullException("参数lockKey不能为空");
            }
            lock (_ReaderWriterLockSlimList)
            {
                _ReaderWriterLockSlimList.Remove(lockKey);
            }
        }
    }
}
