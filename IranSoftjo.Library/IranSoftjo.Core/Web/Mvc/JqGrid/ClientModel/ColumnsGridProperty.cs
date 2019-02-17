using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Web.Mvc.JqGrid.ClientModel.ColumnPropertyModel;
using Mehr.Web.Mvc.ClientModel;

namespace Mehr.Web.Mvc.JqGrid.ClientModel
{
    public class ColumnsGridProperty : ClientProperty<ClientList<GridColumn>>
    {
        public const string PropertyName = "colModel";

        public ColumnsGridProperty() :
            base(PropertyName, new ClientList<GridColumn>()) { }

        //protected override string GetValueAsJson()
        //{
        //    StringBuilder builder = new StringBuilder(this.Value.Count * 100);

        //    builder.Append('[');
        //    var count = this.Value.Count;
        //    for (int i = 0; i < count; i++)
        //    {
        //        var gridColumn = this.Value[i];
        //        builder.Append(gridColumn.GetClientModelAsJson());
        //        if (i != count - 1)
        //            builder.Append(',');
        //    }
        //    builder.Append(']');

        //    return builder.ToString();
        //}

        public ColumnsGridProperty Add(GridColumn newColumn)
        {
            this.Value.Add(newColumn);
            return this;
        }

        public GridColumn this[string columnName]
        {
            get { return this.Value.FirstOrDefault(c => c.Name == columnName); }
            set
            {
                var t = this.Value.FirstOrDefault(c => c.Name == columnName);
                if (t != null)
                    this.Value.Remove(t);
                else
                    this.Value.Add(t);
            }
        }
    }
}
