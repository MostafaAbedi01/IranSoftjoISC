using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Mehr.Web.Mvc.JqGrid
{
    [DataContract]
    public class ExportInfo
    {
        [DataMember(Name = "columns")]
        public string[] Columns { get; set; }
    }
}
