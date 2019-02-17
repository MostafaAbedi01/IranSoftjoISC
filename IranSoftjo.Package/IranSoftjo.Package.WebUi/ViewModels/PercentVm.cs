using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class PercentVm
    {
        [Display(Name = "سریال")]
        public double Value { get; set; }
     
        [Display(Name = "عنوان")]
        public string Name { get; set; }
        public bool Explode { get; set; }
     
    }
}