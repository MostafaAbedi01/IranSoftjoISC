using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Linq
{
    public class ExTuple<T1>
    {
        public T1 Item1 { get; set; }
    }

    public class ExTuple<T1, T2> : ExTuple<T1>
    {
        public T2 Item2 { get; set; }
    }
}
