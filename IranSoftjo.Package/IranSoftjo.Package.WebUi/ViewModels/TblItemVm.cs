using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class TblItemVm
    {
        [Key]
        [Display(Name = "سریال")]
        public int TblItemId { get; set; }

        [Display(Name = "کد جبهه")]
        public int ItemCode { get; set; }
        public string ItemCodeStr { get; set; }

        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [Display(Name = "عرض جغرافیایی")]
        public double? Latitude { get; set; }

        [Display(Name = "طول جغرافیایی")]
        public double? Longitude { get; set; }

        [Display(Name = "عرض جغرافیایی")]
        public string LatitudeStr { get; set; }

        [Display(Name = "طول جغرافیایی")]
        public string LongitudeStr { get; set; }

        [Display(Name = "تصویر پل")]
        public string ImageUrl { get; set; }

        [Display(Name = "تصویر پل")]
        public string ThumbnailImageUrl { get; set; }
        [Display(Name = "توضیحات")]
        public string Comment { get; set; }

        [Display(Name = "عنوان")]
        public string Title { get; set; }
        public int TblProjectId { get; set; }

        [Display(Name = "فعال بودن نقشه گوگل")]
        public bool IsGoogleMap { get; set; }


        [Display(Name = "متراژ موجود")]
        public double? AllMetraj { get; set; }

        [Display(Name = "متراژ رنگ شده")]
        public double? AllMetrajColor { get; set; }

        [Display(Name = "متراژ خط کشی شده")]
        public double? AllMetrajLine { get; set; }

        [Display(Name = "تینر مصرفی")]
        public double? AllThinner { get; set; }
        [Display(Name = "هاردنر مصرفی")]
        public double? AllHardener { get; set; }

        [Display(Name = "رنگ مصرفی")]
        public double? AllColorUse { get; set; }
        [Display(Name = "رنگ خط کشی")]
        public double? AllColorLine { get; set; }
        [Display(Name = "درصدپیشرفت")]
        public double? AllProgress { get; set; }

       

        [Display(Name = "ریت")]
        public double? AllRate { get; set; }

        public double? ProgressLevel1 { get; set; }
        [Display(Name = "تمیزکاری")]
        public double? ProgressLevel2 { get; set; }
        [Display(Name = "پرایمر")]
        public double? ProgressLevel3 { get; set; }
        [Display(Name = "لایه میانی")]
        public double? ProgressLevel4 { get; set; }
        [Display(Name = "لایه نهایی")]
        public double? ProgressLevel5 { get; set; }
        [Display(Name = "خط کشی")]
        public double? ProgressLevel6 { get; set; }
        public double Space { get; set; }

        public static explicit operator TblItem(TblItemVm model)
        {
            var obj = new TblItem
            {
                TblItemId = model.TblItemId,
                Address = model.Address,
                ItemCode = model.ItemCode,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Comment = model.Comment,
                ImageUrl = model.ImageUrl,
                ThumbnailImageUrl = model.ThumbnailImageUrl,
                Title = model.Title,
                TblProjectId = model.TblProjectId,
            };
            return obj;
        }

        public static explicit operator TblItemVm(TblItem model)
        {
            var obj = new TblItemVm
            {
                TblItemId = model.TblItemId,
                Address = model.Address,
                ItemCode = model.ItemCode,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Comment = model.Comment,
                ImageUrl = model.ImageUrl,
                ThumbnailImageUrl = model.ThumbnailImageUrl,
                Title = model.Title,
                TblProjectId = model.TblProjectId,

            };
            return obj;
        }
        public static IEnumerable<TblItemVm> ToIEnumerable(IEnumerable<TblItem> models)
        {
            Entities _db = new Entities();
            var orDefault = models.FirstOrDefault();
            if (orDefault != null)
            {
                int tblProjectId = (int)orDefault.TblProjectId;
                var firstOrDefault = _db.TblProjects.FirstOrDefault(d => d.TblProjectId == tblProjectId);
                var isGoogleMap = firstOrDefault != null && firstOrDefault.IsGoogleMap;
                var user = models.Select(model => new TblItemVm
                {
                    TblItemId = model.TblItemId,
                    Address = model.Address,
                    ItemCode = model.ItemCode,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    Comment = model.Comment,
                    ImageUrl = model.ImageUrl,
                    ThumbnailImageUrl = model.ThumbnailImageUrl,
                    Title = model.Title,
                    TblProjectId = model.TblProjectId,
                    ItemCodeStr = model.ItemCode.ToString(),
                    IsGoogleMap = isGoogleMap,
                });
                return user;
            }
            return null;
        }
               
    }
}