using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr
{
    public static class CommonExtensions
    {
        public static bool IsNullOrDefault<T>(this Nullable<T> value)
             where T : struct, IEquatable<T>
       {
           return !value.HasValue || value.Value.Equals(default(T));
       }

    }
}
