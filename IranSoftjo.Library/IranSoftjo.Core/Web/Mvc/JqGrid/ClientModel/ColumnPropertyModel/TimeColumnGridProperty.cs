using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Web.Mvc.ClientModel;

namespace Mehr.Web.Mvc.JqGrid.ClientModel.ColumnPropertyModel
{
    public class TimeColumnGridProperty : ClientProperty, IServerFormatable, ISelfSerializeMinifyManager
    {
        public TimeColumnGridProperty() { }

        public string FormatOnServer(object value)
        {
            if(value == null) 
                return "";
            if (value.GetType() != typeof(TimeSpan))
                return value.ToString();
            return ((TimeSpan)value).ToString("hh\\:mm\\:ss").LocalizeNumbers();
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
