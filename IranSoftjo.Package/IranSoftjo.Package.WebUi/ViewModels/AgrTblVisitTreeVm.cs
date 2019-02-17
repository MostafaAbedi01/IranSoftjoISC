using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class AgrTblVisitTreeVm
    {
        [Key]
        [Display(Name = "سریال")]
        public int TblVisitTreeId { get; set; }

        [Display(Name = "تاریخ ثبت اطلاعات")]
        public DateTime? DateSabt { get; set; }

        [Display(Name = "کاربر")]
        public int? TblUserId { get; set; }

        [Display(Name = "ارتفاع")]
        public double? Height { get; set; }
        [Display(Name = "انحراف")]
        public double? Diversion { get; set; }
        [Display(Name = "آفت")]
        public Int16? Pest { get; set; }
        [Display(Name = "بیماری")]
        public Int16? Sickness { get; set; }



        public static explicit operator TblVisitTree(AgrTblVisitTreeVm model)
        {
            var obj = new TblVisitTree
            {
                TblVisitTreeId = model.TblVisitTreeId,
                TblUserId = model.TblUserId,
                DateSabt = model.DateSabt,
                Height = model.Height,
                Diversion = model.Diversion,
                Pest = model.Pest,
                Sickness = model.Sickness
            };
            return obj;
        }

        public static explicit operator AgrTblVisitTreeVm(TblVisitTree model)
        {
            var obj = new AgrTblVisitTreeVm
            {
                TblVisitTreeId = model.TblVisitTreeId,
                TblUserId = model.TblUserId,
                DateSabt = model.DateSabt,
                Height = model.Height,
                Diversion = model.Diversion,
                Pest = model.Pest,
                Sickness = model.Sickness
            };
            return obj;
        }

        public static IEnumerable<AgrTblVisitTreeVm> ToIEnumerable(IEnumerable<TblVisitTree> models)
        {
            var user = models.Select(model => new AgrTblVisitTreeVm
            {
                TblVisitTreeId = model.TblVisitTreeId,
                TblUserId = model.TblUserId,
                DateSabt = model.DateSabt,
                Height = model.Height,
                Diversion = model.Diversion,
                Pest = model.Pest,
                Sickness = model.Sickness
            });
            return user;
        }
    }
}