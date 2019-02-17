using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class ProductImageVM
    {
        [Key]
        [Display(Name = "سریال عکس کالا")]
        public int ProductImageID { get; set; }

        [Display(Name = "سریال کالا")]
        public int ProductID { get; set; }

        [Display(Name = "عنوان عکس کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ProductImageTitle { get; set; }

        [Display(Name = "تصویر کالا")]
        public string ProductImageUrl { get; set; }

        [Display(Name = "تصویر کوچک کالا")]
        public string ProductThumbnailImageUrl { get; set; }

        public static explicit operator ProductImage(ProductImageVM model)
        {
            var obj = new ProductImage
            {
                ProductImageID = model.ProductImageID,
                ProductID = model.ProductID,
                ProductImageTitle = model.ProductImageTitle,
                ProductImageUrl = model.ProductImageUrl,
                ProductThumbnailImageUrl = model.ProductThumbnailImageUrl,
            };
            return obj;
        }

        public static explicit operator ProductImageVM(ProductImage model)
        {
            var obj = new ProductImageVM
            {
                ProductImageID = model.ProductImageID,
                ProductID = model.ProductID,
                ProductImageTitle = model.ProductImageTitle,
                ProductImageUrl = model.ProductImageUrl,
                ProductThumbnailImageUrl = model.ProductThumbnailImageUrl,
            };
            return obj;
        }


        public static IEnumerable<ProductImageVM> ToIEnumerable(IEnumerable<ProductImage> models)
        {
            var obj = models.Select(model => new ProductImageVM
            {
                ProductImageID = model.ProductImageID,
                ProductID = model.ProductID,
                ProductImageTitle = model.ProductImageTitle,
                ProductImageUrl = model.ProductImageUrl,
                ProductThumbnailImageUrl = model.ProductThumbnailImageUrl,
            });
            return obj;
        }
    }
}