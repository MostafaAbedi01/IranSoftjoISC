using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class RasProductGroupVM
    {
        [Key]
        public int ProductGroupID { get; set; }
 

        [Display(Name = "گروه کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ProductGroupTitle { get; set; } 
        
        [Display(Name = "ترتیب")]
        [UIHint("Integer")]
        public int? ProductGroupOrder { get; set; } 
        
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public static explicit operator RasProductGroup(RasProductGroupVM model)
        {
            var product = new RasProductGroup
            {
                ProductGroupID = model.ProductGroupID,
                ProductGroupTitle = model.ProductGroupTitle,
                ProductGroupOrder = model.ProductGroupOrder,
                IsActive = model.IsActive,
               
            };
            return product;
        }

        public static explicit operator RasProductGroupVM(RasProductGroup model)
        {
            var product = new RasProductGroupVM
            {
                ProductGroupID = model.ProductGroupID,
                ProductGroupTitle = model.ProductGroupTitle,
                ProductGroupOrder = model.ProductGroupOrder,
                IsActive = model.IsActive,
  
            };
            return product;
        }


        public static IEnumerable<RasProductGroupVM> ToIEnumerable(IEnumerable<RasProductGroup> models)
        {
            var product = models.Select(model => new RasProductGroupVM
            {
                ProductGroupID = model.ProductGroupID,
                ProductGroupTitle = model.ProductGroupTitle,
                ProductGroupOrder = model.ProductGroupOrder,
                IsActive = model.IsActive,
              
            });
            return product;
        }
    }
}
