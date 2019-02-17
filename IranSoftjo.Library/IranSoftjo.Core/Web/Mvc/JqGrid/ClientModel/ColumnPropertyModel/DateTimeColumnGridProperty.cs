using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Web.Mvc.ClientModel;

namespace Mehr.Web.Mvc.JqGrid.ClientModel.ColumnPropertyModel
{
    public class DateTimeColumnGridProperty : ClientProperty, IExportFormatable
    {
        public const string PropertyName = "date";

        public DateTimeColumnGridProperty()
        {
            base.Name = PropertyName;
            base.Value = "dateTime";
        }

        public string Format(object value)
        {
            if (value == null)
                return "";

            var type = value.GetType();
            type = Nullable.GetUnderlyingType(type) ?? type;
            if (type == typeof(long))
                return PersianDateTime.FromLong((long)value).ToString();
            return value.ToString();
        }
    }
}
