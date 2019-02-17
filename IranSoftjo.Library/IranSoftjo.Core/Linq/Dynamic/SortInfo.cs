using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mehr.Linq.Dynamic
{
    [Serializable]
    public class SortInfo
    {
        string columnName;
        public string ColumnName { get { return columnName.Replace('_', '.'); } set { columnName = value; } }
        public string Direction { get; set; }
    }
}
