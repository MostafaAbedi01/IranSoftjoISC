using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Web.Mvc.JqGrid.FilterTranslation
{
    public class DefaultFilterExpressionProvider : IFilterExpressionProvider
    {
        public static readonly IList<IExpressionBuilderFactory> ExpressionBuilderFactories = new List<IExpressionBuilderFactory>();
        public static IExpressionBuilderFactory DefaultExpressionBuilderFactory = new DefaultExpressionBuilder();

        public DefaultFilterExpressionProvider()
        {
            ExpressionBuilderFactories.Add(new IntDateExpressionBuilder());
            ExpressionBuilderFactories.Add(new LongDateTimeExpressionBuilder());
            ExpressionBuilderFactories.Add(new GorgianDateTimeExpressionBuilder());
            ExpressionBuilderFactories.Add(new GuidExpressionBuilder());
        }

        public IExpressionBuilder GetExpressionBuilder(FilterItemMetadata filterItemMetadata)
        {
            foreach (var expressionBuilderFactory in ExpressionBuilderFactories)
            {
                var expressionBuilder = expressionBuilderFactory.Build(filterItemMetadata);
                if (expressionBuilder != null)
                    return expressionBuilder;
            }

            return DefaultExpressionBuilderFactory.Build(filterItemMetadata);
        }
    }
}
