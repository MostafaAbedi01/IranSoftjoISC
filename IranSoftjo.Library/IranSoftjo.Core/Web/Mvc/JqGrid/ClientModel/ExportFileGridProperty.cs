using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Web.Mvc.ClientModel;

namespace Mehr.Web.Mvc.JqGrid.ClientModel
{
   public class ExportFileGridProperty : ClientProperty<ClientObject>
    {
        public const string PropertyName = "exportFile";

        public ExportFileGridProperty() :
            base(PropertyName, new ClientObject())
        {
            this.Value["noColumnPass"] = new ClientProperty<bool>("noColumnPass", true);
        }
    }
}
