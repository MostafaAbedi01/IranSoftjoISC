using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class TblStoreVm
    {
        [Key]
        [Display(Name = "سریال")]
        public int TblStoreId { get; set; }
        public int? TblAndroidLevelId { get; set; }
        public int TblProjectId { get; set; }
        [Display(Name = "مقدار")]
        public double? Count { get; set; }
        [Display(Name = "مقدار")]
        public string CountStr { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "گروه محصول")]
        public int ProductGroupID { get; set; }
        [Display(Name = "کاربر")]
        public int? UserID { get; set; }
        [Display(Name = "تاریخ و ساعت ثبت")]
        public DateTime? DateSabt { get; set; }

        [Display(Name = "رال")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "طول باید 4 تا باشد")]
        public string Ral { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "محصول")]
        public int ProductID { get; set; }
        [Display(Name = "محصول")]
        public string ProductTitle { get; set; }

        [Display(Name = "گروه محصول")]
        public string ProductGroupTitle { get; set; }
        public bool IsOutStore { get; set; }


        public static explicit operator TblStore(TblStoreVm model)
        {
            var obj = new TblStore
            {
                TblStoreId = model.TblStoreId,
                TblProjectId = model.TblProjectId,
                Count = model.Count,
                DateSabt = model.DateSabt,
                ProductGroupID = model.ProductGroupID,
                UserID = model.UserID,
                Ral = model.Ral,
                ProductID = model.ProductID,
                IsOutStore = model.IsOutStore,
                TblAndroidLevelId = model.TblAndroidLevelId,
            };
            return obj;
        }

        public static explicit operator TblStoreVm(TblStore model)
        {
            var obj = new TblStoreVm
            {
                TblStoreId = model.TblStoreId,
                TblProjectId = model.TblProjectId,
                Count = model.Count,
                DateSabt = model.DateSabt,
                ProductGroupID = model.ProductGroupID,
                UserID = model.UserID,
                Ral = model.Ral,
                ProductID = model.ProductID,
                IsOutStore = model.IsOutStore,
                TblAndroidLevelId = model.TblAndroidLevelId,
            };
            return obj;
        }

        public static IEnumerable<TblStoreVm> ToIEnumerable(IEnumerable<TblStore> models)
        {
            var user = models.Select(model => new TblStoreVm
            {
                ProductTitle = model.RasProduct.ProductTitle,
                TblStoreId = model.TblStoreId,
                TblProjectId = model.TblProjectId,
                Count = model.Count,
                DateSabt = model.DateSabt,
                ProductGroupTitle = model.RasProductGroup.ProductGroupTitle,
                UserID = model.UserID,
                Ral = model.Ral,
                ProductGroupID = model.ProductGroupID,
                ProductID = model.ProductID,
                IsOutStore = model.IsOutStore,
                TblAndroidLevelId = model.TblAndroidLevelId,
            });
            return user;

        }
    }
}