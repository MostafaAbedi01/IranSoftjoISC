using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mehr.Web.Mvc.ClientModel;

namespace Mehr.Web.Mvc.JqGrid.ClientModel.ColumnPropertyModel
{
    public class EnumColumnGridProperty : ClientProperty<ClientObject>, IExportFormatable
    {
        public const string PropertyName = "enumInfo";

        public EnumColumnGridProperty()
        {
            Name = PropertyName;
            Value = new ClientObject();
        }

        public EnumColumnGridProperty(IEnumerable<KeyValuePair<string, string>> dataList)
            : this()
        {
            Values = new ClientList<KeyValuePair<string, string>>(dataList);
        }

        public const string ValuesProperty = "values";
        public ClientList<KeyValuePair<string, string>> Values
        {
            get { return Value.GetProprty<ClientList<KeyValuePair<string, string>>>(ValuesProperty); }
            set { Value.SetProprty<ClientList<KeyValuePair<string, string>>>(ValuesProperty, value); }
        }

        protected override string GetValueAsJson()
        {
            return '{' + ValuesProperty + ":" + "'" + GetValueAsCommaSeperated(Values) + "'}";
        }

        public static string GetValueAsCommaSeperated(IEnumerable<KeyValuePair<string, string>> values)
        {
            return ":<<همه>>;" + string.Join(";", values.Select(item => item.Key + ":" + item.Value));
        }

        public string Format(object value)
        {
            if (value == null)
                return "";
            var resultIndex = Values.FindIndex(d => d.Key == value.ToString());
            if (resultIndex == -1)
                return "";
            return Values[resultIndex].Value;
        }
    }
}
