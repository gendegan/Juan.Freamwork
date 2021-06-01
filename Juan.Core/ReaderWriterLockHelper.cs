using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Juan.Core
{
    public static class ReaderWriterLockHelper
    {
        // Methods
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

        public static T AtomReadLock<T>(this ReaderWriterLockSlim objReaderWriterLockSlim, Func<T> func)
        {
            T local;
            try
            {
                objReaderWriterLockSlim.EnterReadLock();
                local = func();
            }
            finally
            {
                objReaderWriterLockSlim.ExitReadLock();
            }
            return local;
        }

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

        public static T AtomWriteLock<T>(this ReaderWriterLockSlim objReaderWriterLockSlim, Func<T> func)
        {
            T local;
            try
            {
                objReaderWriterLockSlim.EnterWriteLock();
                local = func();
            }
            finally
            {
                objReaderWriterLockSlim.ExitWriteLock();
            }
            return local;
        }
    }


}
