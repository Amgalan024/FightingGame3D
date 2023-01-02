using System;

namespace MVC.Utils.Disposable
{
    public class Disposable : IDisposable
    {
        protected bool IsDisposed;

        protected void ThrowIfDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        public virtual void Dispose()
        {
            ThrowIfDisposed();

            IsDisposed = true;
        }
    }
}