using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Mehr.Threading
{
    public class ThreadSafeCacheProvider : ICacheProvider
    {
        public ICacheProvider InternalCacheProvider { get; set; }
        public ThreadSafeCacheProvider(ICacheProvider internalCacheProvider)
        {
            this.InternalCacheProvider = internalCacheProvider;
        }

        public T Get<T>(string key, Func<string, T> builder)
        {
            Mutex mutex = new Mutex(true, key);
            try
            {
                mutex.WaitOne();
                return Get(key, builder);
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        public T Get<T>(string key, Func<string, T> builder, DateTime absoluteExpiration, TimeSpan? slidingExpiration = null)
        {
            Mutex mutex = new Mutex(true, key);
            try
            {
                mutex.WaitOne();
                return Get(key, builder, absoluteExpiration, slidingExpiration);
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        public bool Remove(string key)
        {
            Mutex mutex = new Mutex(true, key);
            try
            {
                mutex.WaitOne();
                return Remove(key);
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }
}
