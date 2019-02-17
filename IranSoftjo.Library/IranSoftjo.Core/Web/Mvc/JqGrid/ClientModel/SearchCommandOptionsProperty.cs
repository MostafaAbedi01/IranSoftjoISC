using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Web.Mvc.ClientModel;

namespace Mehr.Web.Mvc.JqGrid.ClientModel
{
    public class SearchCommandOptionsPropert : ClientProperty<ClientObject>
    {
        public const string PropertyName = "navGridSrchPrms";

        public SearchCommandOptionsPropert() :
            base(PropertyName, new ClientObject())
        {
        }

        public const string MultipleSearchProperty = "multipleSearch";
        public bool MultipleSearch
        {
            get { return Value.GetProprty<bool>(MultipleSearchProperty); }
            set { Value.SetProprty<bool>(MultipleSearchProperty, value); }
        }
    }
}
