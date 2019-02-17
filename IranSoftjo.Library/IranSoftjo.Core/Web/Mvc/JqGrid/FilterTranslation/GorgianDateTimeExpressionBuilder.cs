using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Web.Mvc.JqGrid.FilterTranslation
{
    public class GorgianDateTimeExpressionBuilder : ISelfFactoryExpressionBuilder
    {
        public GorgianDateTimeExpressionBuilder()
        {

        }

        FilterItemMetadata filterItemMetadata;
        private GorgianDateTimeExpressionBuilder(FilterItemMetadata filterItemMetadata)
        {
            this.filterItemMetadata = filterItemMetadata;
        }

        string IExpressionBuilder.BuildExpression(out object value)
        {
            string expression;
            if (filterItemMetadata.FilterItem.data.StartsWith("time:"))
            {
                value = (int)TimeSpan.Parse(filterItemMetadata.FilterItem.data.Substring(5)).TotalMinutes;
                expression = filterItemMetadata.FilterItem.GetExpression(
                    filterItemMetadata.FilterIndex,
                    @"EntityFunctions.DiffMinutes(EntityFunctions.TruncateTime({0}),{0})");
            }
            else
            {//Date
                value = PersianDateTime.Parse(filterItemMetadata.FilterItem.data).Date.ToGorgian();
                expression = filterItemMetadata.FilterItem.GetExpression(
                    filterItemMetadata.FilterIndex,
                    "EntityFunctions.TruncateTime({0})");
            }
            //Depricated: string expression = string.Format("(EntityFunctions.TruncateTime({0})=@{1})", filterItemMetadata.FilterItem.FieldName, filterItemMetadata.FilterIndex);
            return expression;
        }

        IExpressionBuilder IExpressionBuilderFactory.Build(FilterItemMetadata filterItemMetadata)
        {
            if (IsTargeted(filterItemMetadata))
                return new GorgianDateTimeExpressionBuilder(filterItemMetadata);
            return null;
        }

        public static bool IsTargeted(FilterItemMetadata filterItemMetadata)
        {
            Type columnType = Nullable.GetUnderlyingType(filterItemMetadata.PropertyInfo.PropertyType) ?? filterItemMetadata.PropertyInfo.PropertyType;
            return columnType == typeof(DateTime);
        }
    }
}
