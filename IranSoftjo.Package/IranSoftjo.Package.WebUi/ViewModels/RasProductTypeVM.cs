using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class ProductTypeVM
    {
        [Key]
        [Display(Name = "سریال")]
        public int ProductTypeID { get; set; }

        [Display(Name = "نوع کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ProductTypeTitle { get; set; } 
        
        public virtual ICollection<Product> Products { get; set; }

        public static explicit operator ProductType(ProductTypeVM model)
        {
            var product = new ProductType
            {
                ProductTypeID = model.ProductTypeID,
                ProductTypeTitle = model.ProductTypeTitle,

            };
            return product;
        }

        public static explicit operator ProductTypeVM(ProductType model)
        {
            var product = new ProductTypeVM
            {
                ProductTypeID = model.ProductTypeID,
                ProductTypeTitle = model.ProductTypeTitle,
   
            };
            return product;
        }


        public static IEnumerable<ProductTypeVM> ToIEnumerable(IEnumerable<ProductType> models)
        {
            var product = models.Select(model => new ProductTypeVM
            {
                ProductTypeID = model.ProductTypeID,
                ProductTypeTitle = model.ProductTypeTitle,
       
            });
            return product;
        }
    }
}
