using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Web.Mvc.ClientModel;

namespace Mehr.Web.Mvc.JqGrid.ClientModel
{
    public class CommandBarGridProperty : ClientProperty<ClientObject>
    {
        public const string PropertyName = "navGridPrms";

        public CommandBarGridProperty() :
            base(PropertyName, new ClientObject())
        {
            Add = false;
            Edit = false;
            Delete = false;
            Search = false;
        }

        public const string AddProperty = "add";
        public bool Add
        {
            get { return Value.GetProprty<bool>(AddProperty); }
            set { Value.SetProprty<bool>(AddProperty, value); }
        }

        public const string EditProperty = "edit";
        public bool Edit
        {
            get { return Value.GetProprty<bool>(EditProperty); }
            set { Value.SetProprty<bool>(EditProperty, value); }
        }

        public const string DeleteProperty = "del";
        public bool Delete
        {
            get { return Value.GetProprty<bool>(DeleteProperty); }
            set { Value.SetProprty<bool>(DeleteProperty, value); }
        }

        public const string SearchProperty = "search";
        public bool Search
        {
            get { return Value.GetProprty<bool>(SearchProperty); }
            set { Value.SetProprty<bool>(SearchProperty, value); }
        }
    }
}
