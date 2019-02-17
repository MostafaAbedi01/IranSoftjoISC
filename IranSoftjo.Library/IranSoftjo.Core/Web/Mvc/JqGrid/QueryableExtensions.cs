using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Routing;
using Mehr.Linq;
using Mehr.Linq.Dynamic;
using Mehr.Web.Mvc.JqGrid.FilterTranslation;

namespace Mehr.Web.Mvc.JqGrid
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyJqGridSearchFilter<T>(this IQueryable<T> source) { return ApplyJqGridSearchFilter(source, false); }

        public static IQueryable<T> ApplyJqGridSearchFilter<T>(this IQueryable<T> source, bool notSwallowFilterDataConvertExceptions = false)
        {
            try
            {
                FilterInfo filter = HttpContext.Current.Request.GetQueryFilter<T>();
                return source.Where(filter);
            }
            catch (JqGridFilterException jqGridFilterException)
            {
                if (notSwallowFilterDataConvertExceptions ||
                    jqGridFilterException.ErrorCode != ErrorCode.DataTypeConvert)
                    throw;
                return source;
            }
        }

        public const string DefaultSortFieldNameValue = "Id";
        public const string DefaultSortDirValue = "desc";

        public static IQueryable<T> ApplyJqGridSorting<T>(this IQueryable<T> source,
            string defaultSortFieldName = DefaultSortFieldNameValue,
            string defaultSortDir = DefaultSortDirValue)
        {
            SortInfo sort = HttpContext.Current.Request.GetQuerySort(defaultSortFieldName, defaultSortDir);
            return source.OrderBy(sort);
        }

        public static object ToJsonObject<T>(this IQueryable<T> source)
        {
            return ToJsonObject<T, T>(source, t => t);
        }

        public static object ToJsonObject<T, TResult>(this IQueryable<T> source, Func<T, TResult> formatter, string idFieldName = "Id")
        {
            var paginated = ApplyJqGridPagging<T>(source);
            return FormatAsJqGridData<T, TResult>(paginated, formatter, idFieldName);
        }

        [Obsolete]
        public static object FromatAsJqGridData<T, TResult>(this IPaginatedList<T> paginated, Func<T, TResult> formatter, string idFieldName = "Id"
            , object userdata = null)
        {
            return FormatAsJqGridData(paginated, formatter, idFieldName, userdata);
        }

        public static object FormatAsJqGridData<T, TResult>(this IPaginatedList<T> paginated, Func<T, TResult> formatter, string idFieldName = "Id"
            , object userdata = null)
        {
            return FormatAsJqGridData(paginated,
                 formatter,
                 result => new RouteValueDictionary(result).Values.ToArray(),
                 idFieldName,
                 userdata);
        }

        public static object FormatAsJqGridData<T, TResult>(this IPaginatedList<T> paginated,
            Func<T, TResult> formatter,
            Func<TResult, object[]> valuesExtracter,
            string idFieldName = "Id",
            object userdata = null)
        {
            return new
            {
                total = paginated.TotalPages,
                page = paginated.PageIndex,
                records = paginated.TotalCount,
                rows = paginated.Select(item => formatter(item)).Select(item => new
                {
                    id = item.GetType().GetProperty(idFieldName).GetValue(item, null),
                    cell = valuesExtracter(item)
                }),
                userdata = userdata,
            };
        }

        public static PaginatedList<T> ApplyJqGridPagging<T>(this IQueryable<T> source)
        {
            PaggingInfo paggingInfo = HttpContext.Current.Request.GetQueryPagging();
            return source.ToPaginatedList(paggingInfo);
        }

        public static ExportFileInfo GetExportFileInfo<T>(this IEnumerable<T> sortedList, string fileName = "DataList")
        {
            var exportInfo = HttpContext.Current.Request.GetExportInfo();
            //if (exportInfo != null)
            //    using (var excelExport = new Mehr.OpenOffice.Excel.ExcelExporter<T>())
            //    {
            //        excelExport.ColumnTitles = exportInfo.Columns;
            //        return GetExportFileInfo<T>(sortedList, fileName, excelExport);
            //    }
            return null;
        }

        //public static ExportFileInfo GetExportFileInfo<T>(
        //    this IEnumerable<T> sortedList,
        //    string fileName,
        //    OpenOffice.Excel.ExcelExporter<T> excelExport)
        //{
        //    excelExport.FillData(sortedList);
        //    byte[] outputBytes = excelExport.SaveAs();
        //    return new ExportFileInfo()
        //    {
        //        Content = outputBytes,
        //        ContentType = "application/vnd.ms-excel",
        //        FileName = fileName + ".xlsx"
        //    };
        //}
    }
}
