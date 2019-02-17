using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Web.Mvc.JqGrid.ClientModel.ColumnPropertyModel
{
    public static class SearchOperatorTranslator
    {
        public static Dictionary<SearchOperator, string> mappings = new Dictionary<SearchOperator, string>()
      {
              { SearchOperator.Equal,
                   "eq"},
              { SearchOperator.NotEqual,
                   "ne"},
              { SearchOperator.LessThan,
                   "lt"},
              { SearchOperator.LessThanOrEqual,
                   "le"},
              { SearchOperator.GreaterThan,
                   "gt"},
              { SearchOperator.GreaterThanLessThanOrEqual,
                   "ge"},
              { SearchOperator.BeginWith,
                   "bw"},
              { SearchOperator.EndWith,
                   "ew"},
              { SearchOperator.Contains,
                   "cn"},
              { SearchOperator.NotBeginWith,
                   "bn"},
              { SearchOperator.NotEndWith,
                   "en"},
              { SearchOperator.NotContains,
                   "nc"},
              { SearchOperator.In,
                   "in"},
              { SearchOperator.NotIn,
                   "ni"},
        };

        public static string Translate(SearchOperator searchOperator)
        {
            string t;
            if (mappings.TryGetValue(searchOperator, out t))
                return t;

            throw new NotSupportedException(searchOperator.ToString());
        }

        public static SearchOperator Translate(string name)
        {
            if (mappings.Values.Contains(name))
                return mappings.First(m=>m.Value == name).Key;

            throw new NotSupportedException(name);
        }
    }
}
