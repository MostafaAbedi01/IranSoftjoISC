using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Diagnostics.Contracts;

namespace Mehr.Linq.Dynamic
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Where<T>(this IQueryable<T> source, FilterInfo filter)
        {
            if (filter != null)
                return source.Where(filter.Predicate, filter.ParameterValues);
            return source;
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, SortInfo sortInfo)
        {
            return source.OrderBy(sortInfo.ColumnName + " " + sortInfo.Direction);
        }
    }
}
