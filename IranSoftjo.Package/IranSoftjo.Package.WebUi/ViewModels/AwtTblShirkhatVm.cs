using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class TblShirkhatVm
    {
        [Key]
        [Display(Name = "سریال")]
        public int? TblCodeTagPmId { get; set; }
        public int TblShirkhatId { get; set; }
        [Display(Name = "تاریخ ثبت اطلاعات")]
        public DateTime? DateTimeSabt { get; set; }
        [Display(Name = "کاربر")]
        public int? TblUserId { get; set; }

        [Display(Name = "تصویر ")]
        public string ImageUrl { get; set; }

        [Display(Name = "تصویر کوچک ")]
        public string ThumbnailImageUrl { get; set; }
        [Display(Name = "توضیحات ")]
        public string Comment { get; set; }

        [Display(Name = "کد تک")]
        public string CodeTag { get; set; }

        [Display(Name = "کد پی ام")]
        public string CodePm { get; set; }

        [Display(Name = "سایز دریچه ")]
        public string SizeDariceh { get; set; }
        [Display(Name = "سایز اتاقک ")]
        public string SizeOtaghak { get; set; }
        [Display(Name = "حالت ")]
        public string Status { get; set; }
        [Display(Name = "وضعیت ظاهری ")]
        public Int16? VaziatZaheri { get; set; }
        [Display(Name = "نوع اتاقک ")]
        public Int16? OtaghakType { get; set; }
        [Display(Name = "حالت ")]
        public Int16? StatusMode { get; set; }
        [Display(Name = "وضعیت دریچه")]
        public Int16? VaziatDaricheh { get; set; }
        [Display(Name = "وضعیت شیر")]
        public Int16? VaziatShir { get; set; }
        [Display(Name = "وضعیت پلاک")]
        public Int16? VaziatPelak { get; set; }

 
        [Display(Name = "عرض جغرافیایی")]
        public string LatitudeStr { get; set; }

        [Display(Name = "طول جغرافیایی")]
        public string LongitudeStr { get; set; }
        [Display(Name = "عرض جغرافیایی")]
        public double? Latitude { get; set; }

        [Display(Name = "طول جغرافیایی")]
        public double? Longitude { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; }

        public static explicit operator TblShirkhat(TblShirkhatVm model)
        {
            var obj = new TblShirkhat
            {
                TblUserId = model.TblUserId,
                TblShirkhatId = model.TblShirkhatId,
                TblCodeTagPmId = model.TblCodeTagPmId,
                Comment = model.Comment,
                SizeDariceh = model.SizeDariceh,
                SizeOtaghak = model.SizeOtaghak,
                Status = model.Status,
                VaziatZaheri = model.VaziatZaheri,
                OtaghakType = model.OtaghakType,
                VaziatDaricheh = model.VaziatDaricheh,
                VaziatShir = model.VaziatShir,
                VaziatPelak = model.VaziatPelak,
                DateTimeSabt = model.DateTimeSabt,
                StatusMode = model.StatusMode,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Address = model.Address,
            };
            return obj;
        }

        public static explicit operator TblShirkhatVm(TblShirkhat model)
        {
            Entities _db = new Entities();
            var lst = _db.TblPhotoLists.ToList();
            var obj = new TblShirkhatVm
            {
                TblUserId = model.TblUserId,
                TblShirkhatId = model.TblShirkhatId,
                TblCodeTagPmId = model.TblCodeTagPmId,
                Comment = model.Comment,
                SizeDariceh = model.SizeDariceh,
                SizeOtaghak = model.SizeOtaghak,
                Status = model.Status,
                VaziatZaheri = model.VaziatZaheri,
                OtaghakType = model.OtaghakType,
                VaziatDaricheh = model.VaziatDaricheh,
                VaziatShir = model.VaziatShir,
                VaziatPelak = model.VaziatPelak,
                DateTimeSabt = model.DateTimeSabt,
                StatusMode = model.StatusMode,
                CodePm = model.TblCodeTagPm.CodePm,
                CodeTag = model.TblCodeTagPm.CodeTag,
                ThumbnailImageUrl = lst.FirstOrDefault(d => d.ItemId == model.TblShirkhatId)?.ThumbnailImageUrl,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Address = model.Address,
            };
            return obj;
        }

        public static IEnumerable<TblShirkhatVm> ToIEnumerable(IEnumerable<TblShirkhat> models)
        {
            Entities _db = new Entities();
            var lst = _db.TblPhotoLists.ToList();
            var lstTblCodeTagPms = _db.TblCodeTagPms.ToList();
            var user = models.Select(model => new TblShirkhatVm
            {
                TblUserId = model.TblUserId,
                TblShirkhatId = model.TblShirkhatId,
                TblCodeTagPmId = model.TblCodeTagPmId,
                Comment = model.Comment,
                SizeDariceh = model.SizeDariceh,
                SizeOtaghak = model.SizeOtaghak + "*" + model.SizeDariceh,
                Status = model.Status,
                VaziatZaheri = model.VaziatZaheri,
                OtaghakType = model.OtaghakType,
                VaziatDaricheh = model.VaziatDaricheh,
                VaziatShir = model.VaziatShir,
                VaziatPelak = model.VaziatPelak,
                DateTimeSabt = model.DateTimeSabt,
                StatusMode = model.StatusMode,
                ThumbnailImageUrl = lst.FirstOrDefault(d => d.ItemId == model.TblShirkhatId)?.ThumbnailImageUrl,
                CodePm = lstTblCodeTagPms.FirstOrDefault(d => d.TblCodeTagPmId == model.TblCodeTagPmId)?.CodePm,
                CodeTag = lstTblCodeTagPms.FirstOrDefault(d => d.TblCodeTagPmId == model.TblCodeTagPmId)?.CodeTag,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Address = model.Address,
            });
            return user;
        }
    }
}