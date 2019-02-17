using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using IranSoftjo.Package.DataModel;
using Mehr;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class RasProductVM
    {
        [Key]
        [Display(Name = "سریال کالا")]
        public int ProductID { get; set; }

        [Display(Name = "گروه کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ProductGroupID { get; set; }

        [Display(Name = "واحد کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? ProductTypeID { get; set; }

        [Display(Name = "واحد کالا")]
        public string ProductTypeTitle { get; set; }

        [Display(Name = "عنوان کالا")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ProductTitle { get; set; }

        [Display(Name = "شرح کالا")]
        public string ProductDescription { get; set; }


        public string ProductGroupTitle { get; set; }

        [Display(Name = "کد کالا")]
        public string ProductCode { get; set; }

        [Display(Name = "وضعیت فعال بودن کالا")]
        public bool IsActive { get; set; }



        [Display(Name = "ترتیب نمایش")]
        [UIHint("Integer")]
        public int? ProductOrder { get; set; }
        [Display(Name = "هاردنر")]
        public int? ProductIdHardener { get; set; }
        [Display(Name = "تینر")]
        public int? ProductIdThinner { get; set; }
        [Display(Name = "تینر")]
        public string TitleThinner { get; set; }
        [Display(Name = "هاردنر")]
        public string TitleHardener { get; set; }

        public ProductGroup ProductGroup { get; set; }

        public static explicit operator RasProduct(RasProductVM model)
        {
            var obj = new RasProduct
                      {
                          ProductID = model.ProductID,
                          ProductDescription = model.ProductDescription,
                          ProductTitle = model.ProductTitle,
                          ProductGroupID = model.ProductGroupID,
                          ProductTypeID = model.ProductTypeID,
                          ProductCode = model.ProductCode,
                          IsActive = model.IsActive,
                          ProductOrder = model.ProductOrder,
                          ProductIdHardener = model.ProductIdHardener,
                          ProductIdThinner = model.ProductIdThinner,


                      };
            return obj;
        }

        public static explicit operator RasProductVM(RasProduct model)
        {
            var obj = new RasProductVM
            {
                ProductID = model.ProductID,
                ProductDescription = model.ProductDescription,
                ProductTitle = model.ProductTitle,
                ProductGroupID = model.ProductGroupID,
                ProductTypeID = model.ProductTypeID,
                ProductCode = model.ProductCode.LocalizeNumbers(),
                IsActive = model.IsActive,
                ProductOrder = model.ProductOrder,
                ProductGroupTitle = model.RasProductGroup.ProductGroupTitle,
                ProductIdHardener = model.ProductIdHardener,
                ProductIdThinner = model.ProductIdThinner,
            };
            return obj;
        }


        public static IEnumerable<RasProductVM> ToIEnumerable(IEnumerable<RasProduct> models)
        {
           Entities _db = new Entities();
            IEnumerable<RasProductVM> obj = models.Select(model => new RasProductVM
                                                                {
                                                                    ProductID = model.ProductID,
                                                                    ProductDescription = model.ProductDescription,
                                                                    ProductTitle = model.ProductTitle,
                                                                    ProductGroupID = model.ProductGroupID,
                                                                    ProductTypeID = model.ProductTypeID,
                                                                    ProductCode = model.ProductCode,
                                                                    IsActive = model.IsActive,
                                                                    ProductOrder = model.ProductOrder,
                                                                    ProductTypeTitle = model.RasProductType.ProductTypeTitle,
                                                                    ProductGroupTitle = model.RasProductGroup.ProductGroupTitle,
                                                                    ProductIdHardener = model.ProductIdHardener,
                                                                    ProductIdThinner = model.ProductIdThinner,
                                                                    TitleThinner = model.ProductIdThinner == null ? null : _db.RasProducts.FirstOrDefault(d => d.ProductID == model.ProductIdThinner).ProductTitle,
                                                                    TitleHardener = model.ProductIdHardener == null ? null : _db.RasProducts.FirstOrDefault(d => d.ProductID == model.ProductIdHardener).ProductTitle
                                                                });
            return obj;
        }
    }
}