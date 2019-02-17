using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Mehr.Data
{
    public class ObjectParameterWrapper<T>
    {
        public ObjectParameter ObjectParameter { get; private set; }

        public ObjectParameterWrapper(string name)
        {
            ObjectParameter = new ObjectParameter(name, typeof(T));
        }

        public T Value { get { return ConvertValue(ObjectParameter.Value); } }

        public T ConvertValue(object value) { return Convert.IsDBNull(value) ? default(T) : (T)value; }
    }
}
