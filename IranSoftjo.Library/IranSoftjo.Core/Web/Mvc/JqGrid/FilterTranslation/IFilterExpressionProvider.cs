using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Web.Mvc.JqGrid.FilterTranslation
{
    public interface IFilterExpressionProvider
    {
        IExpressionBuilder GetExpressionBuilder(FilterItemMetadata filterItemMetadata);
    }
}
