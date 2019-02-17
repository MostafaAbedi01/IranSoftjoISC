using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Linq
{
    public interface IPaginated
    {
        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
}
