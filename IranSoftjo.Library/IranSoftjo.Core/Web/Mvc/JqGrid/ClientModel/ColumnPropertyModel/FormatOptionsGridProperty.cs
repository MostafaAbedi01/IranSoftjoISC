using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Web.Mvc.ClientModel;

namespace Mehr.Web.Mvc.JqGrid.ClientModel.ColumnPropertyModel
{
    public static class FormatOptionsGridProperty
    {
        public const string PropertyName = "formatoptions";
    }

    public class FormatOptionsGridProperty<T> : ClientProperty<T>
        where T : ClientObject, IFormatOptions, new()
    {
        public FormatOptionsGridProperty()
        {
            base.Name = FormatOptionsGridProperty.PropertyName;
            base.Value = new T();
        }
    }

    public interface IFormatOptions
    {
    }
}
