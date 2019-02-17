using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Linq.Dynamic;

namespace Mehr.Web.Mvc.JqGrid.FilterTranslation
{
    public interface IFilterTranslator
    {
        FilterInfo Translate<T>(SearchFilter searchFilter);
    }
}
