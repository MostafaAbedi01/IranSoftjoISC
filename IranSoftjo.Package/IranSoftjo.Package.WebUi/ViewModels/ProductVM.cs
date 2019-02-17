using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using IranSoftjo.Package.DataModel;
using Mehr;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class ProductVM
    {
        [Key]
        [Display(Name = "سریال کالا")]
        public int ProductID { get; set; }

        [Display(Name = "گروه کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ProductGroupID { get; set; }

        [Display(Name = "نوع کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? ProductTypeID { get; set; }

        [Display(Name = "نوع کالا")]
        public string ProductTypeTitle { get; set; }

        [Display(Name = "عنوان کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ProductTitle { get; set; }

        [Display(Name = "شرح کامل کالا")]
        [AllowHtml]
        [UIHint("RichText")]
        public string ProductDescription { get; set; }

        [Display(Name = "خلاصه شرح کالا")]
        public string ProductSummery { get; set; }

        [Display(Name = "قیمت کالا")]
        [UIHint("Currency")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ProductPrice { get; set; }

        [Display(Name = "تصویر کالا")]
        public string ProductImageUrl { get; set; }

        [Display(Name = "تصویر کوچک کالا")]
        public string ProductThumbnailImageUrl { get; set; }

        public string ProductGroupTitle { get; set; }

        [Display(Name = "کد کالا")]
        public string ProductCode { get; set; }

        [Display(Name = "وضعیت فعال بودن کالا")]
        public bool IsActive { get; set; }

        [Display(Name = "نمایش 'تماس بگیرید' به جای قیمت ")]
        public bool IsCall { get; set; }

        [Display(Name = "ترتیب نمایش")]
        [UIHint("Integer")]
        public int? ProductOrder { get; set; }

        [Display(Name = "توضیحات جستجوگری کالا")]
        public string MetaDescription { get; set; }

        [Display(Name = "کلمات کلیدی جستجوگری کالا")]
        public string Metakeywords { get; set; }

        [Display(Name = "محصول ویژه")]
        public bool IsSpecial { get; set; }

        [Display(Name = "تعداد بازدید")]
        public long Visit { get; set; }

        [Display(Name = "تعداد بازدید")]
        public string VisitLoc { get; set; }

        [Display(Name = "تعداد موجودی کالا")]
        [UIHint("Number")]
        public int? ProductAvailable { get; set; }
        public string ProductAvailableLoc { get; set; }

        public ProductGroup ProductGroup { get; set; }

        public static explicit operator Product(ProductVM model)
        {
            var obj = new Product
                      {
                          ProductSummery = model.ProductSummery,
                          ProductID = model.ProductID,
                          ProductPrice = model.ProductPrice,
                          ProductDescription = model.ProductDescription,
                          ProductTitle = model.ProductTitle,
                          ProductGroupID = model.ProductGroupID,
                          ProductTypeID = model.ProductTypeID,
                          ProductImageUrl = model.ProductImageUrl,
                          ProductThumbnailImageUrl = model.ProductThumbnailImageUrl,
                          ProductCode = model.ProductCode,
                          IsActive = model.IsActive,
                          ProductOrder = model.ProductOrder,
                          MetaDescription = model.MetaDescription,
                          Metakeywords = model.Metakeywords,
                          IsSpecial = model.IsSpecial,
                          Visit = model.Visit,
                          ProductGroup = model.ProductGroup,
                          IsCall = model.IsCall,
                          ProductAvailable = model.ProductAvailable,
                      };
            return obj;
        }

        public static explicit operator ProductVM(Product model)
        {
            var obj = new ProductVM
            {
                ProductSummery = model.ProductSummery,
                          ProductID = model.ProductID,
                          ProductPrice = model.ProductPrice,
                          ProductDescription = model.ProductDescription,
                          ProductTitle = model.ProductTitle,
                          ProductGroupID = model.ProductGroupID,
                          ProductTypeID = model.ProductTypeID,
                          ProductTypeTitle = model.ProductType.ProductTypeTitle,
                          ProductImageUrl = model.ProductImageUrl,
                          ProductThumbnailImageUrl = model.ProductThumbnailImageUrl,
                          ProductGroupTitle = model.ProductGroup.ProductGroupTitle,
                          ProductCode = model.ProductCode.LocalizeNumbers(),
                          IsActive = model.IsActive,
                          ProductOrder = model.ProductOrder,
                          MetaDescription = model.MetaDescription,
                          Metakeywords = model.Metakeywords,
                          IsSpecial = model.IsSpecial,
                          Visit = model.Visit,
                          VisitLoc = model.Visit.LocalizeNumbers(),
                          ProductGroup = model.ProductGroup,
                          IsCall = model.IsCall,
                          ProductAvailable = model.ProductAvailable,
                          ProductAvailableLoc = model.ProductAvailable.LocalizeNumbers(),
                      };
            return obj;
        }


        public static IEnumerable<ProductVM> ToIEnumerable(IEnumerable<Product> models)
        {
            IEnumerable<ProductVM> obj = models.Select(model => new ProductVM
                                                                {
                                                                    ProductSummery = model.ProductSummery,
                                                                    ProductID = model.ProductID,
                                                                    ProductPrice = model.ProductPrice,
                                                                    ProductDescription = model.ProductDescription,
                                                                    ProductTitle = model.ProductTitle,
                                                                    ProductGroupID = model.ProductGroupID,
                                                                    ProductTypeID = model.ProductTypeID,
                                                                    ProductImageUrl = model.ProductImageUrl,
                                                                    ProductThumbnailImageUrl =
                                                                        model.ProductThumbnailImageUrl,
                                                                    ProductGroupTitle =
                                                                        model.ProductGroup.ProductGroupTitle,
                                                                    ProductCode = model.ProductCode,
                                                                    IsActive = model.IsActive,
                                                                    ProductOrder = model.ProductOrder,
                                                                    MetaDescription = model.MetaDescription,
                                                                    Metakeywords = model.Metakeywords,
                                                                    IsSpecial = model.IsSpecial,
                                                                    Visit = model.Visit,
                                                                    ProductGroup = model.ProductGroup,
                                                                    IsCall = model.IsCall,
                                                                    ProductAvailable = model.ProductAvailable,
                                                                    ProductTypeTitle = model.ProductType.ProductTypeTitle,
                                                                });
            return obj;
        }
    }
}