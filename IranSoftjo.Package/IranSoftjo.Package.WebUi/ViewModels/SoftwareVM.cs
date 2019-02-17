using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class SoftwareVM
    {
        [Key]
        [Display(Name = "سریال نرم افزار")]
        public int SoftwareID { get; set; }

        [Display(Name = "سریال کاربر")]
        public int? UserID { get; set; }

        [Display(Name = "نام فارسی نرم افزار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string SoftwareName { get; set; }

        [Display(Name = "نام انگلیسی نرم افزار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string SoftwareEnName { get; set; }

        [Display(Name = "قیمت نرم افزار")]
        [UIHint("Currency")]
        public int? SoftwarePrice { get; set; }

        [Display(Name = "نوع قیمت نرم افزار")]
        public short? SoftwarePriceType { get; set; }

        [Display(Name = "سایت نرم افزار")]
        public string SoftwareWebSite { get; set; }

        [Display(Name = "وضعیت نرم افزار")]
        public short SoftwareStatus { get; set; }
         [Display(Name = "وضعیت نرم افزار")]
        public SoftwareStatusEnum SoftwareStatusEnum { get; set; }

        [Display(Name = "تحت ویندوز")]
        public bool OperatingSystemWin { get; set; }

        [Display(Name = "تحت وب")]
        public bool OperatingSystemWeb { get; set; }

        [Display(Name = "تحت اندروید")]
        public bool OperatingSystemAndroid { get; set; }

        [Display(Name = "تحت IOS")]
        public bool OperatingSystemIOS { get; set; }

        [Display(Name = "تحت ویندوز موبایل")]
        public bool OperatingSystemWinMobile { get; set; }

        [Display(Name = "تحت لینوکس و یونیکس")]
        public bool OperatingSystemLinux { get; set; }

        [Display(Name = "نوع دریافت نرم افزار")]
        public SelectList SelectListSoftwareUserLicense { get; set; }     
        
        [Display(Name = "نوع دریافت نرم افزار")]
        public short SoftwareUserLicense { get; set; }

        public bool HowBuyPerson { get; set; }

        public bool HowBuyOnline { get; set; }

        public bool HowBuyShop { get; set; }

        [Display(Name = "لوگو")]
        public string SoftwareImageLogoUrl { get; set; }

        [Display(Name = "خلـاصـه توضیحات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string SoftwareSummary { get; set; }

        [Display(Name = "توضیحـات کامــل")]
        [AllowHtml]
        [UIHint("CommentRichText")]
        public string SoftwareFullDescription { get; set; }

        [Display(Name = "ذکر مزیت و قابلیت ها")]
        public string SoftwareAdvantage { get; set; }

        [Display(Name = "امتیاز کاربران")]
        public double? SoftwareUserRating { get; set; }

        [Display(Name = "تعداد بازدید")]
        public int SoftwareVisit { get; set; }

        [Display(Name = "نوع پلتفورم")]
        public SelectList SelectListProjectType { get; set; }

        [Display(Name = "قیمت")]
        public SelectList SelectListSoftwarePriceType { get; set; }

        [Display(Name = "نحــوه دریافت و خرید")]
        public SelectList SelectListPriceType { get; set; }

        [Display(Name = "توضیحات پلتفورم اندروید")]
        public string OperatingSystemAndroidText { get; set; }

        [Display(Name = "توضیحات پلتفورم ویندوز")]
        public string OperatingSystemWinText { get; set; }

        [Display(Name = "توضیحات پلتفورم وب")]
        public string OperatingSystemWebText { get; set; }

        [Display(Name = "توضیحات پلتفورم IOS")]
        public string OperatingSystemIOSText { get; set; }

        [Display(Name = "توضیحات پلتفورم ویندزو موبایل")]
        public string OperatingSystemWinMobileText { get; set; }

        public static explicit operator Software(SoftwareVM model)
        {
            var obj = new Software
                      {
                          SoftwareID = model.SoftwareID,
                          UserID = model.UserID,
                          SoftwarePrice = model.SoftwarePrice,
                          SoftwarePriceType = model.SoftwarePriceType,
                          SoftwareWebSite = model.SoftwareWebSite,
                          SoftwareName = model.SoftwareName,
                          SoftwareEnName = model.SoftwareEnName,
                          OperatingSystemWin = model.OperatingSystemWin,
                          OperatingSystemWeb = model.OperatingSystemWeb,
                          OperatingSystemAndroid = model.OperatingSystemAndroid,
                          OperatingSystemIOS = model.OperatingSystemIOS,
                          OperatingSystemWinMobile = model.OperatingSystemWinMobile,
                          OperatingSystemLinux = model.OperatingSystemLinux,
                          SoftwareUserLicense = model.SoftwareUserLicense,
                          HowBuyPerson = model.HowBuyPerson,
                          HowBuyOnline = model.HowBuyOnline,
                          HowBuyShop = model.HowBuyShop,
                          SoftwareImageLogoUrl = model.SoftwareImageLogoUrl,
                          SoftwareSummary = model.SoftwareSummary,
                          SoftwareUserRating = model.SoftwareUserRating,
                          SoftwareVisit = model.SoftwareVisit,
                          SoftwareStatus = model.SoftwareStatus,
                          SoftwareAdvantage = model.SoftwareAdvantage,
                          OperatingSystemAndroidText = model.OperatingSystemAndroidText,
                          SoftwareFullDescription = model.SoftwareFullDescription,
                          OperatingSystemIOSText = model.OperatingSystemIOSText,
                          OperatingSystemWebText = model.OperatingSystemWebText,
                          OperatingSystemWinMobileText = model.OperatingSystemWinMobileText,
                          OperatingSystemWinText = model.OperatingSystemWinText,
                          SoftwareStatusEnum = model.SoftwareStatusEnum,
                      };
            return obj;
        }

        public static explicit operator SoftwareVM(Software model)
        {
            var obj = new SoftwareVM
                      {
                          SoftwareID = model.SoftwareID,
                          UserID = model.UserID,
                          SoftwarePrice = model.SoftwarePrice,
                          SoftwarePriceType = model.SoftwarePriceType,
                          SoftwareWebSite = model.SoftwareWebSite,
                          SoftwareName = model.SoftwareName,
                          SoftwareEnName = model.SoftwareEnName,
                          OperatingSystemWin = model.OperatingSystemWin,
                          OperatingSystemWeb = model.OperatingSystemWeb,
                          OperatingSystemAndroid = model.OperatingSystemAndroid,
                          OperatingSystemIOS = model.OperatingSystemIOS,
                          OperatingSystemWinMobile = model.OperatingSystemWinMobile,
                          OperatingSystemLinux = model.OperatingSystemLinux,
                          SoftwareUserLicense = model.SoftwareUserLicense,
                          HowBuyPerson = model.HowBuyPerson,
                          HowBuyOnline = model.HowBuyOnline,
                          HowBuyShop = model.HowBuyShop,
                          SoftwareImageLogoUrl = model.SoftwareImageLogoUrl,
                          SoftwareSummary = model.SoftwareSummary,
                          SoftwareUserRating = model.SoftwareUserRating,
                          SoftwareVisit = model.SoftwareVisit,
                          SoftwareStatus = model.SoftwareStatus,
                          SoftwareAdvantage = model.SoftwareAdvantage,
                          OperatingSystemAndroidText = model.OperatingSystemAndroidText,
                          SoftwareFullDescription = model.SoftwareFullDescription,
                          OperatingSystemIOSText = model.OperatingSystemIOSText,
                          OperatingSystemWebText = model.OperatingSystemWebText,
                          OperatingSystemWinMobileText = model.OperatingSystemWinMobileText,
                          OperatingSystemWinText = model.OperatingSystemWinText,
                          SoftwareStatusEnum = model.SoftwareStatusEnum,
                      };
            return obj;
        }


        public static IEnumerable<SoftwareVM> ToIEnumerable(IEnumerable<Software> models)
        {
            IEnumerable<SoftwareVM> obj = models.Select(model => new SoftwareVM
                                                                 {
                                                                     SoftwareID = model.SoftwareID,
                                                                     UserID = model.UserID,
                                                                     SoftwarePrice = model.SoftwarePrice,
                                                                     SoftwarePriceType = model.SoftwarePriceType,
                                                                     SoftwareWebSite = model.SoftwareWebSite,
                                                                     SoftwareName = model.SoftwareName,
                                                                     SoftwareEnName = model.SoftwareEnName,
                                                                     OperatingSystemWin = model.OperatingSystemWin,
                                                                     OperatingSystemWeb = model.OperatingSystemWeb,
                                                                     OperatingSystemAndroid =
                                                                         model.OperatingSystemAndroid,
                                                                     OperatingSystemIOS = model.OperatingSystemIOS,
                                                                     OperatingSystemWinMobile =
                                                                         model.OperatingSystemWinMobile,
                                                                     OperatingSystemLinux = model.OperatingSystemLinux,
                                                                     SoftwareUserLicense = model.SoftwareUserLicense,
                                                                     HowBuyPerson = model.HowBuyPerson,
                                                                     HowBuyOnline = model.HowBuyOnline,
                                                                     HowBuyShop = model.HowBuyShop,
                                                                     SoftwareImageLogoUrl = model.SoftwareImageLogoUrl,
                                                                     SoftwareSummary = model.SoftwareSummary,
                                                                     SoftwareUserRating = model.SoftwareUserRating,
                                                                     SoftwareVisit = model.SoftwareVisit,
                                                                     SoftwareStatus = model.SoftwareStatus,
                                                                     SoftwareAdvantage = model.SoftwareAdvantage,
                                                                     OperatingSystemAndroidText =
                                                                         model.OperatingSystemAndroidText,
                                                                     SoftwareFullDescription =
                                                                         model.SoftwareFullDescription,
                                                                     OperatingSystemIOSText =
                                                                         model.OperatingSystemIOSText,
                                                                     OperatingSystemWebText =
                                                                         model.OperatingSystemWebText,
                                                                     OperatingSystemWinMobileText =
                                                                         model.OperatingSystemWinMobileText,
                                                                     OperatingSystemWinText =
                                                                         model.OperatingSystemWinText,
                                                                     SoftwareStatusEnum = model.SoftwareStatusEnum,
                                                                 });
            return obj;
        }
    }
}