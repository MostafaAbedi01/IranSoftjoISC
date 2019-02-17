using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Mehr.Linq
{
    [Serializable]
    public class PaginatedList<T> : List<T>, IPaginatedList<T>
    {

        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(IQueryable<T> source, int pageIndex, int pageSize)
            : this(source.Count(), pageIndex, pageSize)
        {
            Contract.Requires(source != null);
            this.AddRange(GetPagged(source));
        }

        private PaginatedList(int count, int pageIndex, int pageSize)
        {
            Contract.Requires(pageIndex > 0);
            Contract.Requires(pageSize > 0);

            PageIndex = pageIndex;
            PageSize = pageSize;

            TotalCount = count;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);
            if (TotalPages == 0) TotalPages = 1;

            if (pageIndex > TotalPages)
                PageIndex = TotalPages;
            //Contract.Assume(pageIndex <= TotalPages, string.Format("Accepted range is between 1 and {0}.", TotalPages));
        }

        public static PaginatedList<T> BySelector<T1>(IQueryable<T1> source, int pageIndex, int pageSize, Func<T1, T> selector)
        {
            Contract.Requires(source != null);

            var paginatedList = new PaginatedList<T>(source.Count(), pageIndex, pageSize);
            paginatedList.AddRange(paginatedList.GetPagged(source).Select(selector));
            return paginatedList;
        }

        private IQueryable<T2> GetPagged<T2>(IQueryable<T2> source)
        {
            return source.Skip((PageIndex - 1) * PageSize).Take(PageSize);
        }

        private PaginatedList(IEnumerable<T> dataList, IPaginated paginated)
        {
            Contract.Requires(dataList != null);
            Contract.Requires(paginated != null);

            this.PageIndex = paginated.PageIndex;
            this.PageSize = paginated.PageSize;
            this.TotalCount = paginated.TotalCount;
            this.TotalPages = paginated.TotalPages;

            this.AddRange(dataList);
        }

        public IPaginatedList<TResult> Convert<TResult>(Func<T, TResult> selector)
        {
            return new PaginatedList<TResult>(this.Select(selector), this);
        }

        public bool HasPreviousPage { get { return (PageIndex > 1); } }

        public bool HasNextPage { get { return (PageIndex < TotalPages); } }

        [ContractInvariantMethod]
        void CheckConstraints()
        {
            Contract.Invariant(PageIndex <= TotalPages);
            Contract.Invariant(TotalCount / PageSize <= TotalPages);
        }
    }

}
