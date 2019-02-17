using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Mehr.Web.Mvc.JqGrid.FilterTranslation
{
   public class FilterItemMetadata
    {
        public PropertyInfo PropertyInfo { get; set; }
        public SearchFilter.SearchFilterItem FilterItem { get; set; }
        public int FilterIndex { get; set; }
    }
}
