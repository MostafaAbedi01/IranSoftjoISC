using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Web.Mvc.JqGrid.ClientModel.ColumnPropertyModel
{
    public interface IExportFormatable
    {
        string Format(object value);
    }
}
