using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Web.Mvc.JqGrid.FilterTranslation
{
    public class DefaultExpressionBuilder : ISelfFactoryExpressionBuilder
    {
        public static Type[] NumericTypes = new Type[]{ 
                typeof(Int64),
                typeof(Int32),
                typeof(Int16),
                typeof(Byte),
            };

        public DefaultExpressionBuilder()
        {

        }

        FilterItemMetadata filterItemMetadata;
        private DefaultExpressionBuilder(FilterItemMetadata filterItemMetadata)
        {
            this.filterItemMetadata = filterItemMetadata;
        }

        string IExpressionBuilder.BuildExpression(out object value)
        {
            Type columnType = Nullable.GetUnderlyingType(filterItemMetadata.PropertyInfo.PropertyType) ?? filterItemMetadata.PropertyInfo.PropertyType;
            string data = filterItemMetadata.FilterItem.data;
            if (NumericTypes.Contains(columnType))
                data = data.UnLocalizeNumbers();
            try
            {
                value = Convert.ChangeType(data, columnType);
            }
            catch (Exception ex)
            {
                throw new JqGridFilterException(
                    "Data : " + filterItemMetadata.FilterItem.data +
                    "and FieldName :" + filterItemMetadata.FilterItem.FieldName,
                    ex,
                    ErrorCode.DataTypeConvert);
            }
            string expression = filterItemMetadata.FilterItem.GetExpression(filterItemMetadata.FilterIndex);
            return expression;
        }

        IExpressionBuilder IExpressionBuilderFactory.Build(FilterItemMetadata filterItemMetadata)
        {
            return new DefaultExpressionBuilder(filterItemMetadata);
        }
    }
}
