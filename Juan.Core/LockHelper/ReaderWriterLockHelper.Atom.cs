using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Juan.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class ReaderWriterLockHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objReaderWriterLockSlim"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T AtomWriteLock<T>(this ReaderWriterLockSlim objReaderWriterLockSlim, Func<T> func)
        {
            try
            {
                objReaderWriterLockSlim.EnterWriteLock();
                return func();
            }
            finally
            {
                objReaderWriterLockSlim.ExitWriteLock();

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objReaderWriterLockSlim"></param>
        /// <param name="action"></param>
        public static void AtomWriteLock(this ReaderWriterLockSlim objReaderWriterLockSlim, Action action)
        {
            try
            {
                objReaderWriterLockSlim.EnterWriteLock();
                action();
            }
            finally
            {
                objReaderWriterLockSlim.ExitWriteLock();

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objReaderWriterLockSlim"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T AtomReadLock<T>(this ReaderWriterLockSlim objReaderWriterLockSlim, Func<T> func)
        {
            try
            {
                objReaderWriterLockSlim.EnterReadLock();
                return func();
            }
            finally
            {
                objReaderWriterLockSlim.ExitReadLock();

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objReaderWriterLockSlim"></param>
        /// <param name="action"></param>
        public static void AtomReadLock(this ReaderWriterLockSlim objReaderWriterLockSlim, Action action)
        {
            try
            {
                objReaderWriterLockSlim.EnterReadLock();
                action();
            }
            finally
            {
                objReaderWriterLockSlim.ExitReadLock();

            }

        }
    }
}
