using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Web.Mvc.ClientModel;

namespace Mehr.Web.Mvc.JqGrid.ClientModel
{
    public interface ISerializeMinifyManager
    {
        bool Igonrable(ClientProperty property);
    }
}
