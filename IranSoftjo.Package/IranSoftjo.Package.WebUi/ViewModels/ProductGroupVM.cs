using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class ProductGroupVM
    {
        [Key]
        public int ProductGroupID { get; set; }
        public int? ProductGroupIDTree { get; set; }

        [Display(Name = "گروه کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ProductGroupTitle { get; set; } 
        
        [Display(Name = "ترتیب")]
        [UIHint("Integer")]
        public int? ProductGroupOrder { get; set; } 
        
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public static explicit operator ProductGroup(ProductGroupVM model)
        {
            var product = new ProductGroup
            {
                ProductGroupID = model.ProductGroupID,
                ProductGroupTitle = model.ProductGroupTitle,
                ProductGroupOrder = model.ProductGroupOrder,
                IsActive = model.IsActive,
                ProductGroupIDTree = model.ProductGroupIDTree,
            };
            return product;
        }

        public static explicit operator ProductGroupVM(ProductGroup model)
        {
            var product = new ProductGroupVM
            {
                ProductGroupID = model.ProductGroupID,
                ProductGroupTitle = model.ProductGroupTitle,
                ProductGroupOrder = model.ProductGroupOrder,
                IsActive = model.IsActive,
                ProductGroupIDTree = model.ProductGroupIDTree,
            };
            return product;
        }


        public static IEnumerable<ProductGroupVM> ToIEnumerable(IEnumerable<ProductGroup> models)
        {
            var product = models.Select(model => new ProductGroupVM
            {
                ProductGroupID = model.ProductGroupID,
                ProductGroupTitle = model.ProductGroupTitle,
                ProductGroupOrder = model.ProductGroupOrder,
                IsActive = model.IsActive,
                ProductGroupIDTree = model.ProductGroupIDTree,
            });
            return product;
        }
    }
}
