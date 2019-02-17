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
    
    public partial class Product
    {
        public Product()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            this.ProductImages = new HashSet<ProductImage>();
        }
    
        public int ProductID { get; set; }
        public int ProductGroupID { get; set; }
        public string ProductTitle { get; set; }
        public string ProductDescription { get; set; }
        public int ProductPrice { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductThumbnailImageUrl { get; set; }
        public string ProductCode { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> ProductOrder { get; set; }
        public string MetaDescription { get; set; }
        public string Metakeywords { get; set; }
        public bool IsSpecial { get; set; }
        public long Visit { get; set; }
        public Nullable<int> ProductAvailable { get; set; }
        public bool IsCall { get; set; }
        public Nullable<int> ProductTypeID { get; set; }
        public string ProductSummery { get; set; }
    
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ProductType ProductType { get; set; }
    }
}
