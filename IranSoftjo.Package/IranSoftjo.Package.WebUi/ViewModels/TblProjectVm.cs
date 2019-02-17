using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class TblProjectVm
    {
        [Key]
        [Display(Name = "سریال")]
        public int TblProjectId { get; set; }

        [Display(Name = "نام پروژه")]
        public string ProjectName { get; set; }

        [Display(Name = "آدرس کارفرما")]
        public string AddressKarfarma { get; set; }

        [Display(Name = "آدرس پروژه")]
        public string AddressProject { get; set; }

        [Display(Name = "نام کارفرما")]
        public string KarfarmaName { get; set; }

        [Display(Name = "سرپرست کارگاه")]
        public string SarparastKargahName { get; set; }

        [Display(Name = "نام پیمانکار")]
        public string PeimanKarName { get; set; }

        [Display(Name = "نام مدیر پروژه")]
        public string ModirProjectName { get; set; }

        [Display(Name = "نام پیمانکار جزء")]
        public string PeimanKarJozName { get; set; }

        [Display(Name = "تاریخ شروع قرار داد")]
        public DateTime? DateStartGharardad { get; set; }

        [Display(Name = "تاریخ شروع پیمان")]
        public DateTime? DateStartPeiman { get; set; }

        [Display(Name = "کد پروژه")]
        public int ProjectCode { get; set; }

        [Display(Name = "متراژ موجود")]
        public double? AllMetraj { get; set; }

        [Display(Name = "متراژ رنگ نهایی")]
        public double? AllMetrajColor { get; set; }

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

        [Display(Name = "فعال بودن نقشه گوگل")]
        public bool IsGoogleMap { get; set; }


        public virtual ICollection<TblItem> TblItems { get; set; }

        public static explicit operator TblProject(TblProjectVm model)
        {
            var obj = new TblProject
            {
                TblProjectId = model.TblProjectId,
                ProjectCode = model.ProjectCode,
                ProjectName = model.ProjectName,
                AddressKarfarma = model.AddressKarfarma,
                AddressProject = model.AddressProject,
                KarfarmaName = model.KarfarmaName,
                SarparastKargahName = model.SarparastKargahName,
                PeimanKarName = model.PeimanKarName,
                ModirProjectName = model.ModirProjectName,
                PeimanKarJozName = model.PeimanKarJozName,
                DateStartGharardad = model.DateStartGharardad,
                DateStartPeiman = model.DateStartPeiman,
                TblItems = model.TblItems,
                IsGoogleMap = model.IsGoogleMap,
            };
            return obj;
        }

        public static explicit operator TblProjectVm(TblProject model)
        {
            var obj = new TblProjectVm
            {
                TblProjectId = model.TblProjectId,
                ProjectCode = model.ProjectCode,
                ProjectName = model.ProjectName,
                AddressKarfarma = model.AddressKarfarma,
                AddressProject = model.AddressProject,
                KarfarmaName = model.KarfarmaName,
                SarparastKargahName = model.SarparastKargahName,
                PeimanKarName = model.PeimanKarName,
                ModirProjectName = model.ModirProjectName,
                PeimanKarJozName = model.PeimanKarJozName,
                DateStartGharardad = model.DateStartGharardad,
                DateStartPeiman = model.DateStartPeiman,
                TblItems = model.TblItems,
                IsGoogleMap = model.IsGoogleMap,
            };
            return obj;
        }

        public static IEnumerable<TblProjectVm> ToIEnumerable(IEnumerable<TblProject> models)
        {
            var user = models.Select(model => new TblProjectVm
            {
                TblProjectId = model.TblProjectId,
                ProjectCode = model.ProjectCode,
                ProjectName = model.ProjectName,
                AddressKarfarma = model.AddressKarfarma,
                AddressProject = model.AddressProject,
                KarfarmaName = model.KarfarmaName,
                SarparastKargahName = model.SarparastKargahName,
                PeimanKarName = model.PeimanKarName,
                ModirProjectName = model.ModirProjectName,
                PeimanKarJozName = model.PeimanKarJozName,
                DateStartGharardad = model.DateStartGharardad,
                DateStartPeiman = model.DateStartPeiman,
                TblItems = model.TblItems,
                IsGoogleMap = model.IsGoogleMap,
            });
            return user;
        }
    }
}