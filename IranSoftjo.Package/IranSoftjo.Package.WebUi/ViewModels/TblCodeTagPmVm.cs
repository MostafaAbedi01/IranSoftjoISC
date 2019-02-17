using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class TblCodeTagPmVm
    {
        [Key]
        [Display(Name = "سریال")]
        public int TblCodeTagPmId { get; set; }

        [Display(Name = "کد تک")]
        public string CodeTag { get; set; }

        [Display(Name = "کد پی ام")]
        public string CodePm { get; set; }
        public int? TblShirkhatId { get; set; }
     

        public static explicit operator TblCodeTagPm(TblCodeTagPmVm model)
        {
            var obj = new TblCodeTagPm
            {
                TblCodeTagPmId = model.TblCodeTagPmId,
                CodeTag = model.CodeTag,
                CodePm = model.CodePm,
            };
            return obj;
        }

        public static explicit operator TblCodeTagPmVm(TblCodeTagPm model)
        {
            var obj = new TblCodeTagPmVm
            {
                TblCodeTagPmId = model.TblCodeTagPmId,
                CodeTag = model.CodeTag,
                CodePm = model.CodePm,
                TblShirkhatId=model.TblShirkhats.FirstOrDefault()?.TblShirkhatId
            };
            return obj;
        }

        public static IEnumerable<TblCodeTagPmVm> ToIEnumerable(IEnumerable<TblCodeTagPm> models)
        {
            var user = models.Select(model => new TblCodeTagPmVm
            {
                TblCodeTagPmId = model.TblCodeTagPmId,
                CodeTag = model.CodeTag,
                CodePm = model.CodePm,
            });
            return user;
        }
    }
}