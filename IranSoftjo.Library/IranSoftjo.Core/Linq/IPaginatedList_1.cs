using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Linq
{
    public interface IPaginatedList<T> : IList<T>, IPaginated
    {
    }
}
