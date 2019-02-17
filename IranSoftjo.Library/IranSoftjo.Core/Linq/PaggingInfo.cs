using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Linq
{
    [Serializable]
    public class PaggingInfo
    {
        public const int DefaultPage = 1;
        public const int DefaultRowCount = 10;

        public PaggingInfo()
        {
            Page = DefaultPage;
            RowCount = DefaultRowCount;
        }

        public int Page { get; set; }
        public int RowCount { get; set; }
    }
}
