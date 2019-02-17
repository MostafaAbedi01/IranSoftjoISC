using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Web.Mvc.ClientModel;

namespace Mehr.Web.Mvc.JqGrid.ClientModel.ColumnPropertyModel
{
    public class SearchOperatorClientList : ClientList<SearchOperator>
    {
        public SearchOperatorClientList() : base() { }
        public SearchOperatorClientList(IEnumerable<SearchOperator> collection) : base(collection) { }

        protected override string SerializeItem(SearchOperator item)
        {
            return "'" + SearchOperatorTranslator.Translate(item) + "'";
        }
    }
}
