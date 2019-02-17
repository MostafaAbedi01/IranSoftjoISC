using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Mehr.Web.Mvc.JqGrid.FilterTranslation
{
    public interface IExpressionBuilder
    {
        string BuildExpression(out object value);
    }

    public interface IExpressionBuilderFactory
    {
        IExpressionBuilder Build(FilterItemMetadata filterItemMetadata);
    }

    public interface ISelfFactoryExpressionBuilder : IExpressionBuilder, IExpressionBuilderFactory
    {
    }
}
