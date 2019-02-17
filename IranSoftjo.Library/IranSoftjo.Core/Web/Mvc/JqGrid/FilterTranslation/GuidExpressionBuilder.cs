using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Web.Mvc.JqGrid.FilterTranslation
{
    public class GuidExpressionBuilder : ISelfFactoryExpressionBuilder
    {
        public GuidExpressionBuilder()
        {

        }

        FilterItemMetadata filterItemMetadata;
        private GuidExpressionBuilder(FilterItemMetadata filterItemMetadata)
        {
            this.filterItemMetadata = filterItemMetadata;
        }

        string IExpressionBuilder.BuildExpression(out object value)
        {
            value = Guid.Parse(filterItemMetadata.FilterItem.data);
            string expression = string.Format("{0} == @{1}", filterItemMetadata.FilterItem.FieldName, filterItemMetadata.FilterIndex);
            return expression;
        }

        IExpressionBuilder IExpressionBuilderFactory.Build(FilterItemMetadata filterItemMetadata)
        {
            Type columnType = Nullable.GetUnderlyingType(filterItemMetadata.PropertyInfo.PropertyType) ?? filterItemMetadata.PropertyInfo.PropertyType;
            if (columnType == typeof(Guid))
                return new GuidExpressionBuilder(filterItemMetadata);
            return null;
        }
    }
}
