using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Mehr.Linq
{
    [Pure]
    public static class QueryableExtensions
    {
        public static PaginatedList<T> ToPaginatedList<T>(this IQueryable<T> source, int pageIndex = PaggingInfo.DefaultPage, int pageSize = PaggingInfo.DefaultRowCount)
        {
            Contract.Requires(source != null);
            Contract.Requires(pageIndex > 0);
            Contract.Requires(pageSize > 0);
            return new PaginatedList<T>(source, pageIndex, pageSize);
        }

        public static PaginatedList<T> ToPaginatedList<T1,T>(this IQueryable<T1> source, Func<T1, T> selector, int pageIndex = PaggingInfo.DefaultPage, int pageSize = PaggingInfo.DefaultRowCount)
        {
            Contract.Requires(source != null);
            Contract.Requires(pageIndex > 0);
            Contract.Requires(pageSize > 0);
            return PaginatedList<T>.BySelector<T1>(source, pageIndex, pageSize, selector);
        }

        public static PaginatedList<T> ToPaginatedList<T>(this IQueryable<T> source, PaggingInfo paggingInfo)
        {
            return source.ToPaginatedList(paggingInfo.Page, paggingInfo.RowCount);
        }
    }
}
