using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Web.Mvc.ClientModel;
using Mehr.Reflection;

namespace Mehr.Web.Mvc.JqGrid.ClientModel.ColumnPropertyModel
{
    public class EnumColumnGridProperty<EnumT> : EnumColumnGridProperty
    {
        public EnumColumnGridProperty(params EnumT[] excludeValues)
        {
            var enumMetadataFactory = ServiceLocator.Current.Resolve<IEnumMetadataFactory>();
            var items = enumMetadataFactory.Get<EnumT>().Items.AsEnumerable();

            if (excludeValues != null && excludeValues.Length > 0)
                items = items.Where(item => !excludeValues.Contains(item.Key));
            
            var dataList = items.
                Select((item) =>
                new KeyValuePair<string, string>(
                Convert.ToInt32(item.Key).ToString(), item.Value));
            
            Values = new ClientList<KeyValuePair<string, string>>(dataList);
        }
    }
}
