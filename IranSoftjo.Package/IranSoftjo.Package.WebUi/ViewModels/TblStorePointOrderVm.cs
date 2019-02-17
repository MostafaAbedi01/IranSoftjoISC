using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class TblStorePointOrderVm
    {
        [Display(Name = "مصرف شده")]
        public double? CountUse { get; set; }
        [Display(Name = "موجودی اولیه انبار")]
        public double? CountInStore { get; set; }
        [Display(Name = "موجودی روز انبار")]
        public double? CountInStoreToday { get; set; }
        [Display(Name = "درصد موجودی")]
        public double? CountProgress { get; set; }

        [Display(Name = "رال")]
        public string Ral { get; set; }

        [Display(Name = "محصول")]
        public string ProductTitle { get; set; }

        [Display(Name = "گروه محصول")]
        public string ProductGroupTitle { get; set; }

        [Display(Name = "پروژه")]
        public string ProjectTitle { get; set; }


    }
}