using Domain.Delegates;
using System;

namespace Domain.Model.UserConnection
{
    public abstract class UserConnection
    {
        protected ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        protected ConnectionAbortedHandler? _connectionAbortedHandler;
        public event ConnectionAbortedHandler? ConnectionAbortedHandler{
            add
            {
                _lock.EnterWriteLock();
                try
                {
                    _connectionAbortedHandler += value;
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
            remove
            {
                _lock.EnterWriteLock();
                try
                {
                    _connectionAbortedHandler -= value;
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
        }

        public abstract Task<bool> TrySendAsync(ArraySegment<byte> message);
    }
}
