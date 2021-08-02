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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lockKey"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public T AtomWriteLock<T>(string lockKey, Func<T> func)
        {
            ReaderWriterLockSlim objReaderWriterLockSlim = CreateLock(lockKey);
            try
            {
                objReaderWriterLockSlim.EnterWriteLock();
                return func();
            }
            finally
            {
                RemoveLock(objReaderWriterLockSlim, lockKey);
                objReaderWriterLockSlim.ExitWriteLock();




            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lockKey"></param>
        /// <param name="action"></param>
        public void AtomWriteLock(string lockKey, Action action)
        {
            ReaderWriterLockSlim objReaderWriterLockSlim = CreateLock(lockKey);
            try
            {
                objReaderWriterLockSlim.EnterWriteLock();
                action();
            }
            finally
            {
                RemoveLock(objReaderWriterLockSlim, lockKey);
                objReaderWriterLockSlim.ExitWriteLock();


            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lockKey"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public T AtomReadLock<T>(string lockKey, Func<T> func)
        {
            ReaderWriterLockSlim objReaderWriterLockSlim = CreateLock(lockKey);
            try
            {
                objReaderWriterLockSlim.EnterReadLock();
                return func();
            }
            finally
            {
                RemoveLock(objReaderWriterLockSlim, lockKey);
                objReaderWriterLockSlim.ExitReadLock();



            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lockKey"></param>
        /// <param name="action"></param>
        public void AtomReadLock(string lockKey, Action action)
        {
            ReaderWriterLockSlim objReaderWriterLockSlim = CreateLock(lockKey);
            try
            {
                objReaderWriterLockSlim.EnterReadLock();
                action();
            }
            finally
            {


                RemoveLock(objReaderWriterLockSlim, lockKey);
                objReaderWriterLockSlim.ExitReadLock();



            }

        }


    }
}
