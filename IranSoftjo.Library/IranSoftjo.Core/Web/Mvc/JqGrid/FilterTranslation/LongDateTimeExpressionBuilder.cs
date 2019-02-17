using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Web.Mvc.JqGrid.FilterTranslation
{
    public class LongDateTimeExpressionBuilder : ISelfFactoryExpressionBuilder
    {
        public LongDateTimeExpressionBuilder()
        {

        }

        FilterItemMetadata filterItemMetadata;
        private LongDateTimeExpressionBuilder(FilterItemMetadata filterItemMetadata)
        {
            this.filterItemMetadata = filterItemMetadata;
        }

        string IExpressionBuilder.BuildExpression(out object value)
        {
            var persianDateTimeValue = PersianDateTime.Parse(filterItemMetadata.FilterItem.data);
            value = persianDateTimeValue.ToLong();
            string expressionTemplate = "";
            switch (filterItemMetadata.FilterItem.Operator)
            {
                case SearchFilter.SearchFilterItemOperator.Greater:
                case SearchFilter.SearchFilterItemOperator.LessOrEqual:
                    value = persianDateTimeValue.AddDays(1).AddSeconds(-1).ToLong();
                    expressionTemplate = filterItemMetadata.FilterItem.ExpressionTemplate;
                    break;
                case SearchFilter.SearchFilterItemOperator.GreaterOrEqual:
                case SearchFilter.SearchFilterItemOperator.Less:
                    expressionTemplate = filterItemMetadata.FilterItem.ExpressionTemplate;
                    break;
                case SearchFilter.SearchFilterItemOperator.Equal:
                    expressionTemplate = "({0}>=@{1} && {0}<(@{1}+1000000))";
                    break;
                case SearchFilter.SearchFilterItemOperator.NotEqual:
                    expressionTemplate = "!({0}>=@{1} && {0}<(@{1}+1000000))";
                    break;
                default:
                    throw new NotSupportedException("filterItemMetadata.FilterItem.Operator");
            }

            string expression = string.Format(expressionTemplate, filterItemMetadata.FilterItem.FieldName, filterItemMetadata.FilterIndex);
            return expression;
        }

        IExpressionBuilder IExpressionBuilderFactory.Build(FilterItemMetadata filterItemMetadata)
        {
            if (IsTargeted(filterItemMetadata))
                return new LongDateTimeExpressionBuilder(filterItemMetadata);
            return null;
        }

        public static bool IsTargeted(FilterItemMetadata filterItemMetadata)
        {
            Type columnType = Nullable.GetUnderlyingType(filterItemMetadata.PropertyInfo.PropertyType) ?? filterItemMetadata.PropertyInfo.PropertyType;
            return columnType == typeof(long) &&
                filterItemMetadata.PropertyInfo.Name.EndsWith("DateTime");
        }
    }
}
