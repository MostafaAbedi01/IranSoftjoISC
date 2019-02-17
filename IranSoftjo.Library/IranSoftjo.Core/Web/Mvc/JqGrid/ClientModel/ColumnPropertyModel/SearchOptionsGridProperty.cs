using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Web.Mvc.ClientModel;

namespace Mehr.Web.Mvc.JqGrid.ClientModel.ColumnPropertyModel
{
    public class SearchOptionsGridProperty : ClientProperty<ClientObject>
    {
        public static SearchOptionsGridProperty JustEqual =
            new SearchOptionsGridProperty()
            {
                SearchOperators = new SearchOperatorClientList()
                {
                    SearchOperator.Equal,
                },
            };

        public static SearchOptionsGridProperty JustContains =
          new SearchOptionsGridProperty()
          {
              SearchOperators = new SearchOperatorClientList()
                {
                    SearchOperator.Contains,
                },
          };

        public static SearchOptionsGridProperty JustOperators(params SearchOperator[] searchOperators)
        {
            return new SearchOptionsGridProperty()
            {
                SearchOperators = new SearchOperatorClientList(searchOperators)
            };
        }

        public const string PropertyName = "searchoptions";
        public SearchOptionsGridProperty()
        {
            base.Name = PropertyName;
            base.Value = new ClientObject();
            SearchOperators = new SearchOperatorClientList();
        }

        public const string SearchOperatorsProperty = "sopt";
        public SearchOperatorClientList SearchOperators
        {
            get { return Value.GetProprty<SearchOperatorClientList>(SearchOperatorsProperty); }
            set { Value.SetProprty<SearchOperatorClientList>(SearchOperatorsProperty, value); }
        }
    }
}
