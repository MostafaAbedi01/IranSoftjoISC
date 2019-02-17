using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class TblInspectionVm
    {
        [Key]
        [Display(Name = "سریال")]
        public int TblManholId { get; set; }
        public int TblInspectionId { get; set; }

        [Display(Name = "کد تک")]
        [UIHint("Integer")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CodeTag { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [Display(Name = "کد پی ام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [UIHint("Integer")]
        public int? CodePm { get; set; }

        [Display(Name = "انجام کار")]
        public DateTime? DateSabt { get; set; }

        [Display(Name = "دستور کار")]
        public DateTime? DateOrder { get; set; }

        [Display(Name = "تصویر منهول")]
        public string ImageUrl { get; set; }

        [Display(Name = "تصویر منهول")]
        public string ThumbnailImageUrl { get; set; }
        [Display(Name = "جنس منهول")]
        public int? JenseManhole { get; set; }
        [Display(Name = "نوع منهول")]
        public int? TypeManhole { get; set; }
        [Display(Name = "وضعیت منهول")]
        public int? VaziatManhole { get; set; }
        [Display(Name = "نیازبازرسی")]
        public Int16? NeedBazsazi { get; set; }
        [Display(Name = "نیازهمسطح سازی")]
        public Int16? NeedHamsatSazi { get; set; }
        [Display(Name = "نیازسمپاشی")]
        public Int16? NeedSampashi { get; set; }

        [Display(Name = "نیازشستشو")]
        public Int16? NeedShostosho { get; set; }
        [Display(Name = "وضعیت ماهیچه")]
        public Int16? StatusMahiche { get; set; }
        [Display(Name = "وضعیت پاشنه")]
        public Int16? StatusPashne { get; set; }

        public static explicit operator TblInspection(TblInspectionVm model)
        {
            var obj = new TblInspection
            {
                TblManholId = model.TblManholId,
                DateSabt = model.DateSabt,
                DateOrder = model.DateOrder,
                JenseManhole = model.JenseManhole,
                TypeManhole = model.TypeManhole,
                VaziatManhole = model.VaziatManhole,
                NeedBazsazi = model.NeedBazsazi,
                NeedHamsatSazi = model.NeedHamsatSazi,
                NeedSampashi = model.NeedSampashi,
                NeedShostosho = model.NeedShostosho,
                StatusMahiche = model.StatusMahiche,
                StatusPashne = model.StatusPashne,
                TblInspectionId = model.TblInspectionId

            };
            return obj;
        }

        public static explicit operator TblInspectionVm(TblInspection model)
        {
            var obj = new TblInspectionVm
            {
                TblManholId = model.TblManholId,
                DateSabt = model.DateSabt,
                DateOrder = model.DateOrder,
                Address = model.TblManhol.Address,
                CodePm = model.TblManhol.CodePm,
                CodeTag = model.TblManhol.CodeTag,
                JenseManhole = model.JenseManhole,
                TypeManhole = model.TypeManhole,
                VaziatManhole = model.VaziatManhole,
                NeedBazsazi = model.NeedBazsazi,
                NeedHamsatSazi = model.NeedHamsatSazi,
                NeedSampashi = model.NeedSampashi,
                NeedShostosho = model.NeedShostosho,
                StatusMahiche = model.StatusMahiche,
                StatusPashne = model.StatusPashne,
                TblInspectionId = model.TblInspectionId
            };
            return obj;
        }

        public static IEnumerable<TblInspectionVm> ToIEnumerable(IEnumerable<TblInspection> models)
        {
            var user = models.Select(model => new TblInspectionVm
            {
                TblManholId = model.TblManholId,
                DateSabt = model.DateSabt,
                DateOrder = model.DateOrder,
                Address = model.TblManhol.Address,
                CodePm = model.TblManhol.CodePm,
                CodeTag = model.TblManhol.CodeTag,
                JenseManhole = model.JenseManhole,
                TypeManhole = model.TypeManhole,
                VaziatManhole = model.VaziatManhole,
                NeedBazsazi = model.NeedBazsazi,
                NeedHamsatSazi = model.NeedHamsatSazi,
                NeedSampashi = model.NeedSampashi,
                NeedShostosho = model.NeedShostosho,
                StatusMahiche = model.StatusMahiche,
                StatusPashne = model.StatusPashne,
                TblInspectionId = model.TblInspectionId

            });
            return user;
        }
    }
}