using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class PassengerVm
    {
        [Key]
        [Display(Name = "سریال")]
        public int PassengerID { get; set; }

        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "کد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Code { get; set; }

        [Display(Name = "آدرس")]
        public string Address { get; set; }
        [Display(Name = "تلفن")]
        public string Phone { get; set; }
        [Display(Name = "موبایل")]
        public string Mobile { get; set; }
        [Display(Name = "توضیحات")]
        public string Commnet { get; set; }

        public static explicit operator Passenger(PassengerVm model)
        {
            var pages = new Passenger
                        {
                            PassengerID = model.PassengerID,
                            Name = model.Name,
                            Code = model.Code,
                            Address = model.Address,
                            Phone = model.Phone,
                            Mobile = model.Mobile,
                            Commnet = model.Commnet,
                        };
            return pages;
        }

        public static explicit operator PassengerVm(Passenger model)
        {

            var pages = new PassengerVm
                        {
                            PassengerID = model.PassengerID,
                            Name = model.Name,
                            Code = model.Code,
                            Address = model.Address,
                            Phone = model.Phone,
                            Mobile = model.Mobile,
                            Commnet = model.Commnet,
                        };
            return pages;
        }

        public static IEnumerable<PassengerVm> ToIEnumerable(IEnumerable<Passenger> models)
        {
            IEnumerable<PassengerVm> pages = models.Select(model => new PassengerVm
                                                                {
                                                                    PassengerID = model.PassengerID,
                                                                    Name = model.Name,
                                                                    Code = model.Code,
                                                                    Address = model.Address,
                                                                    Phone = model.Phone,
                                                                    Mobile = model.Mobile,
                                                                    Commnet = model.Commnet,
                                                                });
            return pages;
        }
    }
}