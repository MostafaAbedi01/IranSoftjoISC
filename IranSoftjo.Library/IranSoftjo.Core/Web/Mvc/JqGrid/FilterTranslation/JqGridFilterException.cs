using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Web.Mvc.JqGrid.FilterTranslation
{
    public enum ErrorCode
    {
        Other,
        DataTypeConvert,
    }

    [Serializable]
    public class JqGridFilterException : JqGridException
    {
        public ErrorCode ErrorCode { get; set; }

        public JqGridFilterException() { }
        public JqGridFilterException(string message) : this(message, null) { }
        public JqGridFilterException(string message, Exception inner) : this(message, inner, ErrorCode.Other) { }
        public JqGridFilterException(string message, Exception inner, ErrorCode errorCode)
            : base(message, inner)
        {
            this.ErrorCode = errorCode;
        }
        protected JqGridFilterException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
