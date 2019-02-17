using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class DriverVm
    {
        [Key]
        [Display(Name = "سریال")]
        public int DriverID { get; set; }

        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Display(Name = " نام خانوادگی")]
        public string LastName { get; set; }

        [Display(Name = "کد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Code { get; set; }

        [Display(Name = "توضیحات")]
        public string Commnet { get; set; }

        [Display(Name = "تلفن")]
        public string Phone { get; set; }

        [Display(Name = "موبایل")]
        public string Mobile { get; set; }

        [Display(Name = "پلاک ماشین")]
        public string Car { get; set; }

        public static explicit operator Driver(DriverVm model)
        {
            var pages = new Driver
            {
                DriverID = model.DriverID,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Code = model.Code,
                Phone = model.Phone,
                Car = model.Car,
                Mobile = model.Mobile,
                Commnet = model.Commnet,
            };
            return pages;
        }

        public static explicit operator DriverVm(Driver model)
        {
            var pages = new DriverVm
            {
                DriverID = model.DriverID,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Code = model.Code,
                Phone = model.Phone,
                Car = model.Car,
                Mobile = model.Mobile,
                Commnet = model.Commnet,
            };
            return pages;
        }

        public static IEnumerable<DriverVm> ToIEnumerable(IEnumerable<Driver> models)
        {
            IEnumerable<DriverVm> pages = models.Select(model => new DriverVm
            {
                DriverID = model.DriverID,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Code = model.Code,
                Phone = model.Phone,
                Car = model.Car,
                Mobile = model.Mobile,
                Commnet = model.Commnet,
            });
            return pages;
        }
    }
}