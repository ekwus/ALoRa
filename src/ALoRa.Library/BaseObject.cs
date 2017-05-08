using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALoRa.Library
{
    public abstract class BaseObject : IDisposable
    {
        private object m_lock = new object();
        public bool IsDisposed { get; private set; }
        public bool IsDisposing { get; private set; }

        protected abstract void Dispose(bool disposing);

        public void Dispose()
        {
            (this as IDisposable).Dispose();
        }

        protected void CheckDisposed()
        {
            lock(m_lock)
            {
                if (IsDisposed || IsDisposing)
                {
                    throw new ObjectDisposedException(
                                    this.GetType().FullName
                                    );
                }
            }
        }

        void IDisposable.Dispose()
        {
            lock(m_lock)
            {
                if (IsDisposed || IsDisposing)
                {
                    return;
                }

                IsDisposing = true;
            }

            try
            {
                Dispose(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Execption while disposing {0} {1}", GetType().FullName, ex);
            }
            finally
            {
                IsDisposed = true;
            }
        }
    }
}
