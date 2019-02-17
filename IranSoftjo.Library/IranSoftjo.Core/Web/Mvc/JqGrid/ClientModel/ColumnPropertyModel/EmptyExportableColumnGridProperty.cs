using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Web.Mvc.ClientModel;

namespace Mehr.Web.Mvc.JqGrid.ClientModel.ColumnPropertyModel
{
    public class EmptyExportableColumnGridProperty : ClientProperty, IExportFormatable, ISelfSerializeMinifyManager
    {
        public EmptyExportableColumnGridProperty()
        {
        }

        public string Format(object value)
        {
            return "";
        }

        public override string GetClientModelAsJson()
        {
            return "";
        }

        public bool Igonrable()
        {
            return true;
        }
    }
}
