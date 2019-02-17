using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Web.Mvc.JqGrid
{
    [Serializable]
    public class JqGridException : Exception
    {
        public JqGridException() { }
        public JqGridException(string message) : base(message) { }
        public JqGridException(string message, Exception inner) : base(message, inner) { }
        protected JqGridException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
