using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Web.Mvc.ClientModel;

namespace Mehr.Web.Mvc.JqGrid.ClientModel
{
    public class FilterToolbarGridProperty : ClientProperty<ClientObject>
    {
        public const string PropertyName = "filterToolbar";

        public FilterToolbarGridProperty() :
            base(PropertyName, new ClientObject())
        {
            DisplayOnStart = false;
        }

        public const string DisplayOnStartProperty = "displayOnStart";
        public bool DisplayOnStart
        {
            get { return Value.GetProprty<bool>(DisplayOnStartProperty); }
            set { Value.SetProprty<bool>(DisplayOnStartProperty, value); }
        }
    }
}
