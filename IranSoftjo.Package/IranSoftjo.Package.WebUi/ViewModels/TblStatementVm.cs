using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class TblStatementVm
    {
        [Key]
        [Display(Name = "سریال")]
        public int TblStatementID { get; set; }

        [Display(Name = "مبلغ پایه")]
        [UIHint("Currency")]
        public int? BasicPrice { get; set; }

        [Display(Name = "ضریب مینوس/پلوس")]
        [UIHint("Percent")]
        public int? FactorPelosi { get; set; }

        [Display(Name = "ضریب تعدیل سالیانه")]
        [UIHint("Integer")]
        public int? FactorYear { get; set; }

        [Display(Name = "عوارض")]
        [UIHint("Integer")]
        public int? Complication { get; set; }

        [Display(Name = "مالیات")]
        [UIHint("Integer")]
        public int? Tax { get; set; }

        [Display(Name = "نوع")]
        public int? Type { get; set; }

        public static explicit operator TblStatement(TblStatementVm model)
        {
            var obj = new TblStatement
            {
                BasicPrice = model.BasicPrice,
                FactorPelosi = model.FactorPelosi,
                TblStatementID = model.TblStatementID,
                Complication = model.Complication,
                FactorYear = model.FactorYear,
                Tax = model.Tax,
                Type = model.Type
            };
            return obj;
        }

        public static explicit operator TblStatementVm(TblStatement model)
        {
            var obj = new TblStatementVm
            {
                BasicPrice = model.BasicPrice,
                FactorPelosi = model.FactorPelosi,
                TblStatementID = model.TblStatementID,
                Complication = model.Complication,
                FactorYear = model.FactorYear,
                Tax = model.Tax,
                Type = model.Type
            };
            return obj;
        }

        public static IEnumerable<TblStatementVm> ToIEnumerable(IEnumerable<TblStatement> models)
        {
            var user = models.Select(model => new TblStatementVm
            {
                BasicPrice = model.BasicPrice,
                FactorPelosi = model.FactorPelosi,
                TblStatementID = model.TblStatementID,
                Complication = model.Complication,
                FactorYear = model.FactorYear,
                Tax = model.Tax,
                Type = model.Type
            });
            return user;
        }
    }
}