using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class TblSubItemVm
    {
        [Key]
        [Display(Name = "سریال")]
        public int TblSubItemId { get; set; }
        public int TblItemId { get; set; }

        [Display(Name = "کد جبهه")]
        public int? ItemCode { get; set; }

        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "مساحت کل")]
        public double? MetrajKol { get; set; }

        [Display(Name = "مساحت کل")]
        public string MetrajKolStr { get; set; }

        [Display(Name = "کد آیتم")]
        public int SubItemCode { get; set; }

        public string Address { get; set; }
        public string ItemTitle { get; set; }
        public string ProjectName { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [Display(Name = "متراژ رنگ نهایی")]
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
        [Display(Name = "آماده سازی")]
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

        public static explicit operator TblSubItem(TblSubItemVm model)
        {
            var obj = new TblSubItem
            {
                TblSubItemId = model.TblSubItemId,
                Title = model.Title,
                MetrajKol = model.MetrajKol,
                SubItemCode = model.SubItemCode,
                TblItemId = model.TblItemId,
            };
            return obj;
        }

        public static explicit operator TblSubItemVm(TblSubItem model)
        {
            var obj = new TblSubItemVm
            {
                TblSubItemId = model.TblSubItemId,
                Title = model.Title,
                MetrajKol = model.MetrajKol,
                SubItemCode = model.SubItemCode,
                Address = model.TblItem.Address,
                ItemTitle = model.TblItem.Title,
                Latitude = model.TblItem.Latitude,
                Longitude = model.TblItem.Longitude,
                ProjectName = model.TblItem.TblProject.ProjectName,
                TblItemId = model.TblItemId,
            };
            return obj;
        }

        public static IEnumerable<TblSubItemVm> ToIEnumerable(IEnumerable<TblSubItem> models)
        {
            var user = models.Select(model => new TblSubItemVm
            {
                TblSubItemId = model.TblSubItemId,
                Title = model.Title,
                MetrajKol = model.MetrajKol,
                SubItemCode = model.SubItemCode,
                TblItemId = model.TblItemId,
            });
            return user;
        }
    }
}