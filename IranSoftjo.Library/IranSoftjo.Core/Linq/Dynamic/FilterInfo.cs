using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Linq.Dynamic
{
    public class FilterInfo
    {
        public string Predicate { get; set; }
        public object[] ParameterValues { get; set; }
    }
}
