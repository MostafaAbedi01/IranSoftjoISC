//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IranSoftjo.Package.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class TblSubItem
    {
        public TblSubItem()
        {
            this.TblSubItemAccesses = new HashSet<TblSubItemAccess>();
        }
    
        public int TblSubItemId { get; set; }
        public int TblItemId { get; set; }
        public int SubItemCode { get; set; }
        public string Title { get; set; }
        public Nullable<double> MetrajKol { get; set; }
        public string Comment { get; set; }
    
        public virtual TblItem TblItem { get; set; }
        public virtual ICollection<TblSubItemAccess> TblSubItemAccesses { get; set; }
    }
}
