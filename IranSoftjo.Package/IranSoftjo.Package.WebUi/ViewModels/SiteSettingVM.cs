using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class SiteSettingVM
    {
        [Key]
        [Display(Name = "سریال")]
        public int SiteSettingID { get; set; }

        [Display(Name = "توضیحات جستجوگری")]
        public String MetaDescription { get; set; }

        [Display(Name = "کلمات کلیدی جستجوگری")]
        public String Metakeywords { get; set; }

        [Display(Name = "عنوان وب سایت فارسی")]
        public string WebSiteTitle { get; set; }

        [Display(Name = "آدرس وب سایت")]
        public string WebSiteName { get; set; }

        [Display(Name = "آیکون بالای مرورگر")]
        public string IconImageUrl { get; set; }

        [Display(Name = "لوگو سایت")]
        public string LogoImageUrl { get; set; }

        [Display(Name = "آدرس یاهو")]
        public string YahooId { get; set; }

        [Display(Name = "نوع اسلایدر")]
        public Int16? SliderType { get; set; }

        [Display(Name = "تلفن سایت")]
        public string PhoneNumber { get; set; }

        [Display(Name = "محصولات جدید")]
        public bool NewProducts { get; set; }

        [Display(Name = "ضمانت پرداخت")]
        public bool PaymentGuarantee { get; set; }

        [Display(Name = "محصولات پربازدید")]
        public bool PopularProducts { get; set; }

        [Display(Name = "دسته بندی محصولات")]
        public bool ProductCategories { get; set; }

        [Display(Name = "تبلیغات")]
        public bool Propaganda { get; set; }

        [Display(Name = "محصولات پیشنهاد ويژه")]
        public bool SpecialProducts { get; set; }

        [Display(Name = "نوع مبالغ")]
        public short? PriceType { get; set; }

        [Display(Name = "مالیات")]
        public int? Tax { get; set; }

        [Display(Name = "شماره حساب اینترنتی")]
        public string AccountNumberOnline { get; set; }

        [Display(Name = "تاریخ و مناسبت روز")]
        public bool Date { get; set; }

        [Display(Name = "زمان اذان")]
        public bool Prayer { get; set; }

        [Display(Name = "ساعت فلش")]
        public bool FlashClocks { get; set; }

        [Display(Name = "دیکشنری")]
        public bool Translate { get; set; }

        [Display(Name = "آب و هوا")]
        public bool Weather { get; set; }

        [Display(Name = "هاست ایمیل")]
        public string SendMailHost { get; set; }

        [Display(Name = "نام کاربری ایمیل")]
        public string SendMailUserName { get; set; }

        [Display(Name = "رمز عبور ایمیل")]
        [DataType(DataType.Password)]
        public string SendMailPassword { get; set; }


        [Display(Name = "SSL - امنیت ایمیل")]
        public bool SendMailEnableSsl { get; set; }

        [Display(Name = "قالب بندی ایمیلها")]
        [AllowHtml]
        [UIHint("RichText")]
        public string HtmlTemplateMail { get; set; }

        [Display(Name = "انتخاب قالب سایت")]
        public bool ThemeChange { get; set; }

        [Display(Name = "ثابت بودن هدر سایت")]
        public bool FixHeaderTop { get; set; }

        public static explicit operator SiteSetting(SiteSettingVM model)
        {
            var obj = new SiteSetting
            {
                ThemeChange = model.ThemeChange,
                FixHeaderTop = model.FixHeaderTop,
                SendMailPassword = model.SendMailPassword,
                SendMailUserName = model.SendMailUserName,
                SendMailHost = model.SendMailHost,
                SendMailEnableSsl = model.SendMailEnableSsl,
                Weather = model.Weather,
                Translate = model.Translate,
                FlashClocks = model.FlashClocks,
                Prayer = model.Prayer,
                Date = model.Date,
                SiteSettingID = model.SiteSettingID,
                MetaDescription = model.MetaDescription,
                Metakeywords = model.Metakeywords,
                WebSiteName = model.WebSiteName,
                WebSiteTitle = model.WebSiteTitle,
                IconImageUrl = model.IconImageUrl,
                LogoImageUrl = model.LogoImageUrl,
                YahooId = model.YahooId,
                SliderType = model.SliderType,
                PhoneNumber = model.PhoneNumber,
                NewProducts = model.NewProducts,
                PaymentGuarantee = model.PaymentGuarantee,
                PopularProducts = model.PopularProducts,
                PriceType = model.PriceType,
                ProductCategories = model.ProductCategories,
                Propaganda = model.Propaganda,
                SpecialProducts = model.SpecialProducts,
                Tax = model.Tax,
                AccountNumberOnline = model.AccountNumberOnline,
                HtmlTemplateMail = model.HtmlTemplateMail,
            };
            return obj;
        }

        public static explicit operator SiteSettingVM(SiteSetting model)
        {
            var obj = new SiteSettingVM
            {
                ThemeChange = model.ThemeChange,
                FixHeaderTop = model.FixHeaderTop,
                SendMailPassword = model.SendMailPassword,
                SendMailUserName = model.SendMailUserName,
                SendMailHost = model.SendMailHost,
                SendMailEnableSsl = model.SendMailEnableSsl,
                Weather = model.Weather,
                Translate = model.Translate,
                FlashClocks = model.FlashClocks,
                Prayer = model.Prayer,
                Date = model.Date,
                SiteSettingID = model.SiteSettingID,
                MetaDescription = model.MetaDescription,
                Metakeywords = model.Metakeywords,
                WebSiteName = model.WebSiteName,
                WebSiteTitle = model.WebSiteTitle,
                IconImageUrl = model.IconImageUrl,
                LogoImageUrl = model.LogoImageUrl,
                YahooId = model.YahooId,
                SliderType = model.SliderType,
                PhoneNumber = model.PhoneNumber,
                NewProducts = model.NewProducts,
                PaymentGuarantee = model.PaymentGuarantee,
                PopularProducts = model.PopularProducts,
                PriceType = model.PriceType,
                ProductCategories = model.ProductCategories,
                Propaganda = model.Propaganda,
                SpecialProducts = model.SpecialProducts,
                Tax = model.Tax,
                AccountNumberOnline = model.AccountNumberOnline,
                HtmlTemplateMail = model.HtmlTemplateMail,
            };
            return obj;
        }

    }
}