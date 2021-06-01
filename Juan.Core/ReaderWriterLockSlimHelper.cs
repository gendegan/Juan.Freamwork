using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Juan.Core
{
    public class ReaderWriterLockSlimHelper
    {
        // Fields
        private Dictionary<string, ReaderWriterLockSlim> _ReaderWriterLockSlimList = new Dictionary<string, ReaderWriterLockSlim>();

        // Methods
        public void AtomReadLock(string lockKey, Action action)
        {
            ReaderWriterLockSlim readerWriterLockSlim = this.CreateLock(lockKey);
            try
            {
                readerWriterLockSlim.EnterReadLock();
                action();
            }
            finally
            {
                this.RemoveLock(readerWriterLockSlim, lockKey);
                readerWriterLockSlim.ExitReadLock();
            }
        }

        public T AtomReadLock<T>(string lockKey, Func<T> func)
        {
            T local;
            ReaderWriterLockSlim readerWriterLockSlim = this.CreateLock(lockKey);
            try
            {
                readerWriterLockSlim.EnterReadLock();
                local = func();
            }
            finally
            {
                this.RemoveLock(readerWriterLockSlim, lockKey);
                readerWriterLockSlim.ExitReadLock();
            }
            return local;
        }

        public void AtomWriteLock(string lockKey, Action action)
        {
            ReaderWriterLockSlim readerWriterLockSlim = this.CreateLock(lockKey);
            try
            {
                readerWriterLockSlim.EnterWriteLock();
                action();
            }
            finally
            {
                this.RemoveLock(readerWriterLockSlim, lockKey);
                readerWriterLockSlim.ExitWriteLock();
            }
        }

        public T AtomWriteLock<T>(string lockKey, Func<T> func)
        {
            T local;
            ReaderWriterLockSlim readerWriterLockSlim = this.CreateLock(lockKey);
            try
            {
                readerWriterLockSlim.EnterWriteLock();
                local = func();
            }
            finally
            {
                this.RemoveLock(readerWriterLockSlim, lockKey);
                readerWriterLockSlim.ExitWriteLock();
            }
            return local;
        }

        public ReaderWriterLockSlim CreateLock(string lockKey)
        {
            if (string.IsNullOrWhiteSpace(lockKey))
            {
                throw new ArgumentNullException("参数lockKey不能为空");
            }
            Dictionary<string, ReaderWriterLockSlim> dictionary = this._ReaderWriterLockSlimList;
            lock (dictionary)
            {
                if (this._ReaderWriterLockSlimList.ContainsKey(lockKey))
                {
                    return this._ReaderWriterLockSlimList[lockKey];
                }
                ReaderWriterLockSlim slim2 = new ReaderWriterLockSlim();
                this._ReaderWriterLockSlimList[lockKey] = slim2;
                return slim2;
            }
        }


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
            Dictionary<string, ReaderWriterLockSlim> dictionary = this._ReaderWriterLockSlimList;
            lock (dictionary)
            {
                if (((readerWriterLockSlim.WaitingReadCount == 0) && (readerWriterLockSlim.WaitingWriteCount == 0)) && (readerWriterLockSlim.WaitingUpgradeCount == 0))
                {
                    this._ReaderWriterLockSlimList.Remove(lockKey);
                }
            }
        }
    }





}
