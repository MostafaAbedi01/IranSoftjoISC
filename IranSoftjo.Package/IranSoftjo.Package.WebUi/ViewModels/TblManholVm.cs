using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class TblManholVm
    {
        [Key]
        [Display(Name = "سریال")]
        public int TblManholId { get; set; }

        [Display(Name = "کد تک")]
        public string CodeTag { get; set; }

        [Display(Name = "کد پی ام")]
        [UIHint("Integer")]
        public int? CodePm { get; set; }
        [Display(Name = "عرض جغرافیایی")]
        public string LatitudeStr { get; set; }

        [Display(Name = "طول جغرافیایی")]
        public string LongitudeStr { get; set; }
        public string CodePmStr { get; set; }

        [Display(Name = "کد مکانی")]
        [UIHint("Integer")]
        public int? CodeMakani { get; set; }

        [Display(Name = "عرض جغرافیایی")]
        public double? Latitude { get; set; }

        [Display(Name = "طول جغرافیایی")]
        public double? Longitude { get; set; }

        [Display(Name = "تاریخ ساخت منهول")]
        public DateTime? DateSakht { get; set; }

        [Display(Name = "تاریخ نصب")]
        public DateTime? DateNasb { get; set; }

        [Display(Name = "تاریخ بهره برداری")]
        public DateTime? DateBahrebardari { get; set; }

        [Display(Name = "تاریخ ثبت اطلاعات")]
        public DateTime? DateSabt { get; set; }

        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [Display(Name = "کدناحیه")]
        [UIHint("Integer")]
        public int? CodeNaheyeh { get; set; }

        [Display(Name = "مرئی بودن")]
        public Int16? VaziatZaheri1 { get; set; }
        [Display(Name = "سالم بودن")]
        public Int16? VaziatZaheri2 { get; set; }
        [Display(Name = "همسطح بودن")]
        public Int16? VaziatZaheri3 { get; set; }
        [Display(Name = "قطر دریچه")]
        public Int16? GhotrDariche { get; set; }
        [Display(Name = "نوع منهول")]
        public Int16? NoeManhol { get; set; }
        [Display(Name = "جنس منهول")]
        public Int16? JenseManhol { get; set; }


        [Display(Name = "جنس دریچه")]
        public short? JenseDaricheh { get; set; }

        [Display(Name = "کد جی ای اس")]
        [UIHint("Integer")]
        public int? CodeGis { get; set; }
        [UIHint("Integer")]

        [Display(Name = "عمق منهول")]
        [UIHint("Integer")]
        public int? OmgheManhol { get; set; }
        [UIHint("Integer")]
        [Display(Name = "قطر منهول")]
        public int? GhotreManhol { get; set; }

        [Display(Name = "قطر لوله ورودی")]
        [UIHint("Integer")]
        public int? GhotreLolehVorodi { get; set; }

        [Display(Name = "قطر لوله خروجی")]
        [UIHint("Integer")]
        public int? GhotreLolehKhoroji { get; set; }

        [Display(Name = "تصویر منهول")]
        public string ImageUrl { get; set; }

        [Display(Name = "تصویر منهول")]
        public string ThumbnailImageUrl { get; set; }

        [Display(Name = "توضیحات")]
        public string Comment { get; set; }

        [Display(Name = "تعداد لوله ورودی")]
        [UIHint("Integer")]
        public int? TedadLoleVorodi { get; set; }
        [Display(Name = "نیاز به بندکشی")]
        public short? BandKeshi { get; set; }

        [Display(Name = "کد نقشه جدید")]
        [UIHint("Integer")]
        public int? MapNew { get; set; }
        [UIHint("Integer")]

        [Display(Name = "کد نقشه قدیم")]
        [UIHint("Integer")]
        public int? MapOld { get; set; }

        [Display(Name = "کد تصفیه خانه")]
        [UIHint("Integer")]
        public int? CodeTasfieKhane { get; set; }

        [Display(Name = "کد اشتراک")]
        public string CodeEshterak { get; set; }

        [Display(Name = "کد سیستم تبلت")]
        [UIHint("Integer")]
        public int? CodeSystem { get; set; }

        [Display(Name = "کد شرکت")]
        [UIHint("Integer")]
        public int? CodeCompany { get; set; }
        [UIHint("Integer")]

        [Display(Name = "منطقه برق")]
        public int? MantagheBargh { get; set; }
        [UIHint("Integer")]

        [Display(Name = "منطقه شهرداری")]
        [UIHint("Integer")]
        public int? MantagheShahrdari { get; set; }
        [UIHint("Integer")]

        [Display(Name = "منطقه راهنمایی و رانندگی")]
        public int? MantagheRanandegi { get; set; }
        [UIHint("Integer")]

        [Display(Name = "منطقه مخابرات")]
        [UIHint("Integer")]
        public int? MantagheMohakhaberat { get; set; }

        [Display(Name = "منطقه گاز")]
        [UIHint("Integer")]
        public int? MantagheGaz { get; set; }

        [Display(Name = "کدکاربر")]
        [UIHint("Integer")]
        public int? TblUserId { get; set; }
        [Display(Name = "پاشنه منهول")]
        public short? Pashneh { get; set; }
        [Display(Name = "نیاز به لایروبی")]
        public short? LiRobi { get; set; }
        [Display(Name = "ماهیچه منهول")]
        public short? Mahicheh { get; set; }
        [Display(Name = "وضعیت پلکان")]
        public short? Pelekan1 { get; set; }
        [Display(Name = "نوع پلکان")]
        public short? Pelekan2 { get; set; }
             [Display(Name = "نیاز به سمپاشی")]
        public short? SamPashi { get; set; }
        [Display(Name = "نام محور")]
        public string MehvarName { get; set; }

        public static explicit operator TblManhol(TblManholVm model)
        {
            var obj = new TblManhol
            {
                TblManholId = model.TblManholId,
                CodeTag = model.CodeTag,
                CodePm = model.CodePm,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                DateSakht = model.DateSakht,
                DateBahrebardari = model.DateBahrebardari,
                DateSabt = model.DateSabt,
                DateNasb = model.DateNasb,
                ImageUrl = model.ImageUrl,
                ThumbnailImageUrl = model.ThumbnailImageUrl,
                CodeNaheyeh = model.CodeNaheyeh,
                Address = model.Address,
                VaziatZaheri1 = model.VaziatZaheri1,
                VaziatZaheri2 = model.VaziatZaheri2,
                VaziatZaheri3 = model.VaziatZaheri3,
                JenseDaricheh = model.JenseDaricheh,
                GhotrDariche = model.GhotrDariche,
                CodeMakani = model.CodeMakani,
                CodeGis = model.CodeGis,
                OmgheManhol = model.OmgheManhol,
                GhotreManhol = model.GhotreManhol,
                NoeManhol = model.NoeManhol,
                JenseManhol = model.JenseManhol,
                GhotreLolehVorodi = model.GhotreLolehVorodi,
                GhotreLolehKhoroji = model.GhotreLolehKhoroji,
                TedadLoleVorodi = model.TedadLoleVorodi,
                Comment = model.Comment,
                BandKeshi = model.BandKeshi,
                MapNew = model.MapNew,
                MapOld = model.MapOld,
                CodeTasfieKhane = model.CodeTasfieKhane,
                CodeEshterak = model.CodeEshterak,
                CodeSystem = model.CodeSystem,
                CodeCompany = model.CodeCompany,
                MantagheBargh = model.MantagheBargh,
                MantagheShahrdari = model.MantagheShahrdari,
                MantagheRanandegi = model.MantagheRanandegi,
                MantagheMohakhaberat = model.MantagheMohakhaberat,
                MantagheGaz = model.MantagheGaz,
                TblUserId = model.TblUserId,
                Pashneh = model.Pashneh,
                LiRobi = model.LiRobi,
                Mahicheh = model.Mahicheh,
                Pelekan1 = model.Pelekan1,
                Pelekan2 = model.Pelekan2,
                SamPashi = model.SamPashi,
                MehvarName = model.MehvarName
            };
            return obj;
        }

        public static explicit operator TblManholVm(TblManhol model)
        {
            var obj = new TblManholVm
            {
                TblManholId = model.TblManholId,
                CodeTag = model.CodeTag,
                CodePm = model.CodePm,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                DateSakht = model.DateSakht,
                DateBahrebardari = model.DateBahrebardari,
                DateSabt = model.DateSabt,
                DateNasb = model.DateNasb,
                ImageUrl = model.ImageUrl,
                ThumbnailImageUrl = model.ThumbnailImageUrl,
                CodeNaheyeh = model.CodeNaheyeh,
                Address = model.Address,
                VaziatZaheri1 = model.VaziatZaheri1,
                VaziatZaheri2 = model.VaziatZaheri2,
                VaziatZaheri3 = model.VaziatZaheri3,
                JenseDaricheh = model.JenseDaricheh,
                GhotrDariche = model.GhotrDariche,
                CodeMakani = model.CodeMakani,
                CodeGis = model.CodeGis,
                OmgheManhol = model.OmgheManhol,
                GhotreManhol = model.GhotreManhol,
                NoeManhol = model.NoeManhol,
                JenseManhol = model.JenseManhol,
                GhotreLolehVorodi = model.GhotreLolehVorodi,
                GhotreLolehKhoroji = model.GhotreLolehKhoroji,
                TedadLoleVorodi = model.TedadLoleVorodi,
                Comment = model.Comment,
                BandKeshi = model.BandKeshi,
                MapNew = model.MapNew,
                MapOld = model.MapOld,
                CodeTasfieKhane = model.CodeTasfieKhane,
                CodeEshterak = model.CodeEshterak,
                CodeSystem = model.CodeSystem,
                CodeCompany = model.CodeCompany,
                MantagheBargh = model.MantagheBargh,
                MantagheShahrdari = model.MantagheShahrdari,
                MantagheRanandegi = model.MantagheRanandegi,
                MantagheMohakhaberat = model.MantagheMohakhaberat,
                MantagheGaz = model.MantagheGaz,
                TblUserId = model.TblUserId,
                Pashneh = model.Pashneh,
                LiRobi = model.LiRobi,
                Mahicheh = model.Mahicheh,
                Pelekan1 = model.Pelekan1,
                Pelekan2 = model.Pelekan2,
                SamPashi = model.SamPashi,
                MehvarName = model.MehvarName
            };
            return obj;
        }

        public static IEnumerable<TblManholVm> ToIEnumerable(IEnumerable<TblManhol> models)
        {
            Entities _db = new Entities();
            var user = models.Select(model => new TblManholVm
            {
                TblManholId = model.TblManholId,
                CodeTag = model.CodeTag,
                CodePm = model.CodePm,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                DateSakht = model.DateSakht,
                DateBahrebardari = model.DateBahrebardari,
                DateSabt = model.DateSabt,
                DateNasb = model.DateNasb,
                ImageUrl = model.ImageUrl,
                //ThumbnailImageUrl = _db.TblPhotoLists.Where(d=>d.ItemId== model.TblManholId).OrderByDescending(d=>d.TblPhotoListId).FirstOrDefault()?.ThumbnailImageUrl,
                CodeNaheyeh = model.CodeNaheyeh,
                Address = model.Address,
                VaziatZaheri1 = model.VaziatZaheri1,
                VaziatZaheri2 = model.VaziatZaheri2,
                VaziatZaheri3 = model.VaziatZaheri3,
                JenseDaricheh = model.JenseDaricheh,
                GhotrDariche = model.GhotrDariche,
                CodeMakani = model.CodeMakani,
                CodeGis = model.CodeGis,
                OmgheManhol = model.OmgheManhol,
                GhotreManhol = model.GhotreManhol,
                NoeManhol = model.NoeManhol,
                JenseManhol = model.JenseManhol,
                GhotreLolehVorodi = model.GhotreLolehVorodi,
                GhotreLolehKhoroji = model.GhotreLolehKhoroji,
                CodePmStr = model.CodePm.ToString(),
                TedadLoleVorodi = model.TedadLoleVorodi,
                Comment = model.Comment,
                BandKeshi = model.BandKeshi,
                MapNew = model.MapNew,
                MapOld = model.MapOld,
                CodeTasfieKhane = model.CodeTasfieKhane,
                CodeEshterak = model.CodeEshterak,
                CodeSystem = model.CodeSystem,
                CodeCompany = model.CodeCompany,
                MantagheBargh = model.MantagheBargh,
                MantagheShahrdari = model.MantagheShahrdari,
                MantagheRanandegi = model.MantagheRanandegi,
                MantagheMohakhaberat = model.MantagheMohakhaberat,
                MantagheGaz = model.MantagheGaz,
                TblUserId = model.TblUserId,
                Pashneh = model.Pashneh,
                LiRobi = model.LiRobi,
                Mahicheh = model.Mahicheh,
                Pelekan1 = model.Pelekan1,
                Pelekan2 = model.Pelekan2,
                SamPashi = model.SamPashi,
                MehvarName = model.MehvarName
            });
            return user;
        }
    }
}