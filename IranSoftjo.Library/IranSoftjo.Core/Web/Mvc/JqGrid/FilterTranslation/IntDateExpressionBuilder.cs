using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Web.Mvc.JqGrid.FilterTranslation
{
    public class IntDateExpressionBuilder : ISelfFactoryExpressionBuilder
    {
        public IntDateExpressionBuilder()
        {

        }

        FilterItemMetadata filterItemMetadata;
        private IntDateExpressionBuilder(FilterItemMetadata filterItemMetadata)
        {
            this.filterItemMetadata = filterItemMetadata;
        }

        string IExpressionBuilder.BuildExpression(out object value)
        {
            value = PersianDateTime.Parse(filterItemMetadata.FilterItem.data).ToDateInt();
            string expression = filterItemMetadata.FilterItem.GetExpression(filterItemMetadata.FilterIndex);
            return expression;
        }

        IExpressionBuilder IExpressionBuilderFactory.Build(FilterItemMetadata filterItemMetadata)
        {
            if (IsTargeted(filterItemMetadata))
                return new IntDateExpressionBuilder(filterItemMetadata);
            return null;
        }

        public static bool IsTargeted(FilterItemMetadata filterItemMetadata)
        {
            Type columnType = Nullable.GetUnderlyingType(filterItemMetadata.PropertyInfo.PropertyType) ?? filterItemMetadata.PropertyInfo.PropertyType;
            return columnType == typeof(int) &&
                filterItemMetadata.PropertyInfo.Name.EndsWith("Date");
        }
    }
}
