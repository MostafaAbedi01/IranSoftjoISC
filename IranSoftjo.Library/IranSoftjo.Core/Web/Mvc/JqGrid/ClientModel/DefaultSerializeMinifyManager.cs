using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Web.Mvc.ClientModel;
using Mehr.Web.Mvc.JqGrid.ClientModel.ColumnPropertyModel;

namespace Mehr.Web.Mvc.JqGrid.ClientModel
{
    public class DefaultSerializeMinifyManager : ISerializeMinifyManager
    {
        public virtual bool Igonrable(ClientProperty property)
        {
            if (property is ISelfSerializeMinifyManager)
                return (property as ISelfSerializeMinifyManager).Igonrable();

            if (property.Name == GridColumn.IsHiddenProperty)
                return (property as ClientProperty<bool>).Value == false;

            if (property.Name == GridColumn.IsSortableProperty)
                return (property as ClientProperty<bool>).Value == true;

            if (property.Name == GridColumn.WidthProperty)
                return (property as ClientProperty<int>).Value == 100;

            if (property.Name == Grid.DisplayRowNumberProperty)
                return (property as ClientProperty<bool>).Value == false;

            if (property.Name == Grid.RowCountProperty)
                return (property as ClientProperty<int>).Value == 10;

            if (property.Name == SearchOptionsGridProperty.PropertyName)
                return (property as SearchOptionsGridProperty).SearchOperators.Count == 0;

            if (property.Name == Grid.DataUrlProperty)
                return string.IsNullOrEmpty((property as ClientProperty<string>).Value);

            return false;
        }
    }
}
