using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Mehr.Linq;
using System.Web;
using Mehr.Web.Mvc.JqGrid.ClientModel.ColumnPropertyModel;
using Mehr.Web.Mvc.JqGrid.ClientModel;

namespace Mehr.Web.Mvc.JqGrid
{
    public interface IGridLogic
    {
        object JqGridFormattedData { get; }
        ExportFileInfo ExportFileInfo { get; }

        string DefaultSortFieldName { get; set; }
        string DefaultSortDir { get; set; }
    }

    public abstract class GridLogicBase<TQueryRecord, TDisplayRecord> : IGridLogic
    {
        static PropertyDescriptorCollection properties;
        Grid gridModel;
        static GridLogicBase()
        {
            properties = TypeDescriptor.GetProperties(typeof(TDisplayRecord));
        }

        public GridLogicBase(Grid gridModel)
        {
            this.DefaultSortFieldName = Mehr.Web.Mvc.JqGrid.QueryableExtensions.DefaultSortFieldNameValue;
            this.DefaultSortDir = Mehr.Web.Mvc.JqGrid.QueryableExtensions.DefaultSortDirValue;
            this.gridModel = gridModel;

            ValuesExtracter = r => gridModel.Columns.Value.Select(c => FormatData(r, c)).ToArray();
        }

        public string DefaultSortFieldName { get; set; }
        public string DefaultSortDir { get; set; }

        public virtual IQueryable<TQueryRecord> AllRecords { get; set; }

        public virtual IQueryable<TQueryRecord> FilteredRecords 
        { get { return AllRecords.ApplyJqGridSearchFilter(); } }

        public virtual IQueryable<TQueryRecord> SortedFilteredRecords
        { get { return FilteredRecords.ApplyJqGridSorting(DefaultSortFieldName, DefaultSortDir); } }

        public virtual PaginatedList<TQueryRecord> PaggedSortedFilteredRecords 
        { get { return SortedFilteredRecords.ApplyJqGridPagging(); } }

        public virtual object JqGridFormattedData
        { get { return PaggedSortedFilteredRecords.FormatAsJqGridData(Formatter, ValuesExtracter); } }

        public virtual IEnumerable<TDisplayRecord> ExportRecords
        { get { return SortedFilteredRecords.AsEnumerable().Select(Formatter); } }

        public virtual ExportFileInfo ExportFileInfo
        {
            get
            {
                var exportInfo = HttpContext.Current.Request.GetExportInfo();
                //if (exportInfo != null)
                    //using (var excelExport = new Mehr.OpenOffice.Excel.ExcelExporter<TDisplayRecord>())
                    //{
                    //    var exportColumns = gridModel.Columns.Value.
                    //         Where(n => !gridModel.ExportExcludeColumnNames.Contains(n.Name));

                    //    excelExport.ColumnTitles = exportColumns.
                    //        Select(c => c.Title).
                    //        ToArray();
                    //    excelExport.ValuesExtracter = r =>
                    //            exportColumns.Select(c => FormatDataForExport(r, c)).ToArray();
                    //    return ExportRecords.GetExportFileInfo<TDisplayRecord>(gridModel.GetType().Name.Replace("Grid", ""), excelExport);
                    //}

                return null;
            }
        }

        protected virtual object FormatDataForExport(TDisplayRecord record, GridColumn column)
        {
            var clientCalculateValue = column.FirstOrDefault(p => p is IClientCalculateValue) as IClientCalculateValue;
            if (clientCalculateValue != null)
                return "";
            var value = properties[column.Name].GetValue(record);
            var exportFormatable = column.FirstOrDefault(p => p is IExportFormatable) as IExportFormatable;
            return exportFormatable != null ?
                    exportFormatable.Format(value) :
                    value;
        }

        protected virtual object FormatData(TDisplayRecord record, GridColumn column)
        {
            var clientCalculateValue = column.FirstOrDefault(p => p is IClientCalculateValue) as IClientCalculateValue;
            if (clientCalculateValue != null)
                return "";
            var value = properties[column.Name].GetValue(record);
            var serverFormatable = column.FirstOrDefault(p => p is IServerFormatable) as IServerFormatable;
            return serverFormatable != null ?
                    serverFormatable.FormatOnServer(value) :
                    value;
        }

        public Func<TQueryRecord, TDisplayRecord> Formatter { get; set; }
        public Func<TDisplayRecord, object[]> ValuesExtracter { get; set; }
    }
}
