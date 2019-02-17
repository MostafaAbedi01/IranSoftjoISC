using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class RasProductTypeVM
    {
        [Key]
        [Display(Name = "سریال")]
        public int ProductTypeID { get; set; }

        [Display(Name = "واحد کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ProductTypeTitle { get; set; } 
        
        public virtual ICollection<RasProduct> Products { get; set; }

        public static explicit operator RasProductType(RasProductTypeVM model)
        {
            var product = new RasProductType
            {
                ProductTypeID = model.ProductTypeID,
                ProductTypeTitle = model.ProductTypeTitle,

            };
            return product;
        }

        public static explicit operator RasProductTypeVM(RasProductType model)
        {
            var product = new RasProductTypeVM
            {
                ProductTypeID = model.ProductTypeID,
                ProductTypeTitle = model.ProductTypeTitle,
   
            };
            return product;
        }


        public static IEnumerable<RasProductTypeVM> ToIEnumerable(IEnumerable<RasProductType> models)
        {
            var product = models.Select(model => new RasProductTypeVM
            {
                ProductTypeID = model.ProductTypeID,
                ProductTypeTitle = model.ProductTypeTitle,
       
            });
            return product;
        }
    }
}
