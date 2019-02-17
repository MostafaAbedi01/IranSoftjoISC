using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class TrackVm
    {
        [Key]
        [Display(Name = "سریال")]
        public int TrackID { get; set; }

        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "کد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Code { get; set; }

        [Display(Name = "مبلغ")]
        public int? Price { get; set; }

        [Display(Name = "زمان مسیر")]
        public string TimeTrack { get; set; }

        [Display(Name = "موبایل")]
        public string Mobile { get; set; }

        [Display(Name = "محدوده")]
        public Int16? TypeTrack { get; set; }

        public static explicit operator Track(TrackVm model)
        {
            var pages = new Track
            {
                TrackID = model.TrackID,
                Name = model.Name,
                Code = model.Code,
                Price = model.Price,
                TimeTrack = model.TimeTrack,
                TypeTrack = model.TypeTrack,
            };
            return pages;
        }

        public static explicit operator TrackVm(Track model)
        {
            var pages = new TrackVm
            {
                TrackID = model.TrackID,
                Name = model.Name,
                Code = model.Code,
                Price = model.Price,
                TimeTrack = model.TimeTrack,
                TypeTrack = model.TypeTrack,
            };
            return pages;
        }

        public static IEnumerable<TrackVm> ToIEnumerable(IEnumerable<Track> models)
        {
            IEnumerable<TrackVm> pages = models.Select(model => new TrackVm
            {
                TrackID = model.TrackID,
                Name = model.Name,
                Code = model.Code,
                Price = model.Price,
                TimeTrack = model.TimeTrack,
                TypeTrack = model.TypeTrack,
            });
            return pages;
        }
    }
}