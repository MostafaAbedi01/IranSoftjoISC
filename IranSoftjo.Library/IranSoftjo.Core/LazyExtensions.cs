using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;

namespace Mehr
{
    public static class LazyExtensions
    {
        public static void ResetPublicationOnly<T>(this Lazy<T> lazy)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.NonPublic;

            LazyThreadSafetyMode mode = (LazyThreadSafetyMode)typeof(Lazy<T>).GetProperty("Mode", flags).GetValue(lazy, null);
            if (mode != LazyThreadSafetyMode.PublicationOnly)
                throw new InvalidOperationException("ResetPublicationOnly only works for Lazy<T> with LazyThreadSafetyMode.PublicationOnly");

            typeof(Lazy<T>).GetField("m_boxed", flags).SetValue(lazy, null);
        }
    }

    public static class Lazy
    {
        public static Lazy<T> Create<T>(Func<T> valueFactory) { return new Lazy<T>(valueFactory); }
    }
}
