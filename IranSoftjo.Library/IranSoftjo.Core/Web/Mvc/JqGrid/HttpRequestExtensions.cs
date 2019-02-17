using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Mehr.Linq.Dynamic;
using Mehr.Linq;
using System.Reflection;
using Mehr.Web.Mvc.JqGrid.FilterTranslation;

namespace Mehr.Web.Mvc.JqGrid
{
    public static class HttpRequestExtensions
    {
        public static FilterInfo GetQueryFilter<T>(this HttpRequest request, IFilterTranslator filterTranslator = null)
        {
            FilterInfo filter = null;
            if (request.Params["_search"] == "true")
            {
                var searchFilter = new JavaScriptSerializer().Deserialize<SearchFilter>(request.Params["filters"]);
                if (filterTranslator == null)
                    filterTranslator = ServiceLocator.Current.Resolve<IFilterTranslator>() ?? new DefaultFilterTranslator();
                filter = filterTranslator.Translate<T>(searchFilter);
            }
            else if (!string.IsNullOrWhiteSpace(request.Params["sText"]))
            {
                string[] parts = request.Params["sText"].Split(',');
                string condition = string.Format("{0}.StartsWith(@0)", parts[0]);

                filter = new FilterInfo()
                {
                    Predicate = condition,
                    ParameterValues = new object[] { parts[1] }
                };
            }

            return filter;
        }

        public static ExportInfo GetExportInfo(this HttpRequest request)
        {
            if (request.Params["exportInfo"] != null)
                return new JavaScriptSerializer().Deserialize<ExportInfo>(request.Params["exportInfo"]);
            return null;
        }

        public static SortInfo GetQuerySort(this HttpRequest request, string defaultSortFieldName, string defaultSortDir = "desc")
        {
            string sortColumnName = request.Params["sidx"];
            string sortDirection = request.Params["sord"] ?? string.Empty;
            if (string.IsNullOrWhiteSpace(sortColumnName))
            {
                sortColumnName = defaultSortFieldName;
                sortDirection = defaultSortDir;
            }

            return new SortInfo()
            {
                ColumnName = sortColumnName,
                Direction = sortDirection
            };
        }

        public static PaggingInfo GetQueryPagging(this HttpRequest request)
        {
            int page = 1;
            int rows = 10;

            string pageString = request.Params["page"];
            if (!string.IsNullOrWhiteSpace(pageString))
                int.TryParse(pageString, out page);

            string rowsString = request.Params["rows"];
            if (!string.IsNullOrWhiteSpace(rowsString))
                int.TryParse(rowsString, out rows);

            return new PaggingInfo()
            {
                Page = page,
                RowCount = rows
            };
        }
    }
}
