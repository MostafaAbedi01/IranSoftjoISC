using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class AdvertisementVM
    {
        [Key]
        [Display(Name = "سریال")]
        public int AdvertisementID { get; set; }

        [Display(Name = "گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? AdvertisementGroupID { get; set; }

        [Display(Name = "ترتیب")]
        [UIHint("Integer")]
        public int? AdvertisementOrder { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string AdvertisementTitle { get; set; }

        [Display(Name = "آدرس لینک")]
        public string AdvertisementUrl { get; set; }

        [Display(Name = "گروه")]
        public string AdvertisementGroupTitle { get; set; }

        [Display(Name = "وضعیت فعال")]
        public bool AdvertisementIsActive { get; set; }

        [Display(Name = "تصویر")]
        public string AdvertisementImage { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime? AdvertisementCreateDate { get; set; }

        public AdvertisementGroup AdvertisementGroup { get; set; }

        public static explicit operator Advertisement(AdvertisementVM model)
        {
            var pages = new Advertisement
                        {
                            AdvertisementID = model.AdvertisementID,
                            AdvertisementTitle = model.AdvertisementTitle,
                            AdvertisementUrl = model.AdvertisementUrl,
                            AdvertisementIsActive = model.AdvertisementIsActive,
                            AdvertisementOrder = model.AdvertisementOrder,
                            AdvertisementCreateDate = model.AdvertisementCreateDate,
                            AdvertisementGroupID = model.AdvertisementGroupID,
                            AdvertisementImage = model.AdvertisementImage,
                        };
            return pages;
        }

        public static explicit operator AdvertisementVM(Advertisement model)
        {

            var pages = new AdvertisementVM
                        {
                            AdvertisementID = model.AdvertisementID,
                            AdvertisementTitle = model.AdvertisementTitle,
                            AdvertisementUrl = model.AdvertisementUrl,
                            AdvertisementIsActive = model.AdvertisementIsActive,
                            AdvertisementOrder = model.AdvertisementOrder,
                            AdvertisementCreateDate = model.AdvertisementCreateDate,
                            AdvertisementGroupID = model.AdvertisementGroupID,
                            AdvertisementGroup = model.AdvertisementGroup,
                            AdvertisementImage = model.AdvertisementImage,

                        };
            if (model.AdvertisementGroup != null)
            {
                pages.AdvertisementGroupTitle = model.AdvertisementGroup.AdvertisementGroupTitle;
                pages.AdvertisementGroupID = model.AdvertisementGroup.AdvertisementGroupID;
            }
            return pages;
        }

        public static IEnumerable<AdvertisementVM> ToIEnumerable(IEnumerable<Advertisement> models)
        {
            IEnumerable<AdvertisementVM> pages = models.Select(model => new AdvertisementVM
                                                                {
                                                                    AdvertisementID = model.AdvertisementID,
                                                                    AdvertisementTitle = model.AdvertisementTitle,
                                                                    AdvertisementUrl = model.AdvertisementUrl,
                                                                    AdvertisementIsActive = model.AdvertisementIsActive,
                                                                    AdvertisementOrder = model.AdvertisementOrder,
                                                                    AdvertisementCreateDate = model.AdvertisementCreateDate,
                                                                    AdvertisementGroupID = model.AdvertisementGroupID,
                                                                    AdvertisementGroup = model.AdvertisementGroup,
                                                                    AdvertisementGroupTitle = model.AdvertisementGroup.AdvertisementGroupTitle,
                                                                    AdvertisementImage = model.AdvertisementImage,
                                                                });
            return pages;
        }
    }
}