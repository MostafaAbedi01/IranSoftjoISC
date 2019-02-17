using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Web.Mvc.ClientModel;

namespace Mehr.Web.Mvc.JqGrid.ClientModel
{
    [JsonEnum(ToLower = true)]
    public enum GridDataType
    {
        Json,
        Jsonp,
        XML,
    }
}
