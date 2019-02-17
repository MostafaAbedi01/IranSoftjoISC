using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Web.Mvc.JqGrid.ClientModel.ColumnPropertyModel
{
    public enum SearchOperator
    {
        Equal,
        NotEqual,
        LessThan,
        LessThanOrEqual,
        GreaterThan,
        GreaterThanLessThanOrEqual,
        BeginWith,
        EndWith,
        Contains,
        NotBeginWith,
        NotEndWith,
        NotContains,
        In,
        NotIn
    }
}
