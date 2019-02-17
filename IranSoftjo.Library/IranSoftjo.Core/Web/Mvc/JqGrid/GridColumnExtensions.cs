using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Mehr.Web.Mvc.JqGrid.ClientModel.ColumnPropertyModel;
using Mehr.Web.Mvc.ClientModel;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Mehr.Web.Mvc.JqGrid.ClientModel
{
    public static class GridColumnExtensions
    {
        public static GridColumn AddTo(this GridColumn column, Grid grid)
        {
            grid.Columns.Add(column);
            return column;
        }

        public static GridColumn For<TRecord, TProperty>(this GridColumn column, Expression<Func<TRecord, TProperty>> expression)
        {
            column.Name = ExpressionHelper.GetExpressionText(expression);
            return column;
        }

        public static GridColumn AddEnumProperty(this GridColumn column, IEnumerable<KeyValuePair<string, string>> dataList)
        {
            column.Add(new EnumColumnGridProperty(dataList));
            return column;
        }

        public static GridColumn AddEnumProperty<EnumT>(this GridColumn column)
            where EnumT : struct
        {
            return AddEnhancedEnumProperty<EnumT>(column);
        }

        public static GridColumn AddEnhancedEnumProperty<EnumT>(this GridColumn column, params EnumT[] excludeValues)
            where EnumT : struct
        {
            column.Add(new EnumColumnGridProperty<EnumT>(excludeValues));
            return column;
        }

        public static GridColumn AddDateTimeProperty(this GridColumn column)
        {
            column.Add(new DateTimeColumnGridProperty());
            return column;
        }

        public static GridColumn ExportEmpty(this GridColumn column)
        {
            column.Add(new EmptyExportableColumnGridProperty());
            return column;
        }

        public static GridColumn FormatOptions<T>(this GridColumn column, T formatOptions)
            where T : ClientObject, IFormatOptions, new()
        {
            column.Add(new ClientProperty(GridColumn.FormatterProperty, CommandLinkFormatOptions.FormatterFunctionName));
            column.Add(new FormatOptionsGridProperty<T>()
            {
                Value = formatOptions,
            });
            return column;
        }

        public static GridColumn CommandLink(this GridColumn column, string linkeText = null, string lineTitle = null)
        {
            var commandLinkFormatOptions = new CommandLinkFormatOptions()
            {
                Text = linkeText,
                Title = lineTitle,
            };
            return CommandLink(column, commandLinkFormatOptions);
        }

        public static GridColumn CommandLink(this GridColumn column, Icons icon, string lineTitle = null)
        {
            column.Width = 25;
            var commandLinkFormatOptions = new CommandLinkFormatOptions()
            {
                Title = lineTitle,
                Icon = icon,
            };
            return CommandLink(column, commandLinkFormatOptions);
        }

        private static GridColumn CommandLink(this GridColumn column, CommandLinkFormatOptions commandLinkFormatOptions)
        {
            column.IsSearchable = false;
            column.IsSortable = false;
            column.Add(new ClientCalculateValueColumnGridProperty());
            column.FormatOptions(commandLinkFormatOptions);
            return column;
        }

        public static GridColumn ContentLink(this GridColumn column, string linkTitle = null)
        {
            var commandLinkFormatOptions = new CommandLinkFormatOptions() { Title = linkTitle };
            return column.FormatOptions(commandLinkFormatOptions);
        }

        public static GridColumn CommandLinkUrlTemplate(this GridColumn column, string urlTemplate)
        {
            var aTagBuilder = CommandLinkFormatOptions.BuildLinkTemplate(urlTemplate);
            var commandLinkFormatOptions =
               column[FormatOptionsGridProperty.PropertyName].Value as CommandLinkFormatOptions;
            commandLinkFormatOptions.LinkTemplate = aTagBuilder.ToString();

            return column;
        }

        public static GridColumn ConfimRequired(this GridColumn column, string title = null, string message = null)
        {
            var commandLinkFormatOptions =
               column[FormatOptionsGridProperty.PropertyName].Value as CommandLinkFormatOptions;
            commandLinkFormatOptions.Confirm = new CommandLinkFormatOptions.CommandLinkFormatOptionsConfirmProperty()
            {
                Title = title,
                Message = message,
            };
            return column;
        }


        public static GridColumn AddTimeProperty(this GridColumn column)
        {
            column.Width = 50;
            column.IsSearchable = false;
            column.Add(new TimeColumnGridProperty());
            return column;
        }
    }
}
