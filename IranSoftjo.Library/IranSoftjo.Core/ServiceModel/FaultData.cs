using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Mehr.ServiceModel
{
    [DataContract]
    public class FaultData 
    {
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Detail { get; set; }

        public override string ToString()
        {
            return "ErrorData : " + Code + " " + Detail;
        }
    }
}
