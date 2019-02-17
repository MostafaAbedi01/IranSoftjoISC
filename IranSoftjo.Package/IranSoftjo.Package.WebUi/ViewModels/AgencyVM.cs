using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class AgencyVM
    {
        [Display(Name = "سریال نمایندگی")]
        public int Id { get; set; }
        public int PageAgency { get; set; }

        [Display(Name = "سریال کاربر")]
        public int UserId { get; set; }

        [Display(Name = "کد نمایندگی")]
        public string Code { get; set; }

        [Display(Name = "نام نمایندگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "کشور")]
        public string Country { get; set; }

        [Display(Name = "استان")]
        public string City { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Address { get; set; }

        [Display(Name = "وضعیت")]
        public bool Active { get; set; }

        [Display(Name = "وضعیت")]
        public string ActiveType { get; set; }

        [Display(Name = "لوگو یا عکس")]
        public string ImageUrl { get; set; }

        [Display(Name = "تلفن تماس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Phone { get; set; }   
        
        [Display(Name = "شماره همراه")]
        public string Mobile { get; set; }

        [Display(Name = "توضیحات")]
        [AllowHtml]
        public string Description { get; set; }

        [Display(Name = "نام مدیریت")]
        public string Manager { get; set; }

        [Display(Name = "شرایط و ضوابط")]
        public bool AgencyConditions { get; set; }

        [Display(Name = "آدرس ایمیل")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "{0} وارد شده صحیح نیست")]
        public string Email { get; set; }

        public static explicit operator Agency(AgencyVM model)
        {
            var agency = new Agency
                         {
                             Id = model.Id,
                             Active = model.Active,
                             City = model.City,
                             Address = model.Address,
                             Country = model.Country,
                             Code = model.Code,
                             Description = model.Description,
                             ImageUrl = model.ImageUrl,
                             Manager = model.Manager,
                             Name = model.Name,
                             Phone = model.Phone,
                             Email = model.Email,
                             UserId = model.UserId,
                             Mobile = model.Mobile,
                         };
            return agency;
        }

        public static explicit operator AgencyVM(Agency model)
        {
            var agency = new AgencyVM
                         {
                             Id = model.Id,
                             Active = model.Active ?? false,
                             City = model.City,
                             Address = model.Address,
                             Country = model.Country,
                             Code = model.Code,
                             Description = model.Description,
                             ImageUrl = model.ImageUrl,
                             Manager = model.Manager,
                             Name = model.Name,
                             Phone = model.Phone,
                             Email = model.Email,
                             ActiveType = model.Active == false ? "غیرفعال" : "فعال",
                             UserId = model.UserId,
                             Mobile = model.Mobile,
                         };
            return agency;
        }

        public static IEnumerable<AgencyVM> ToIEnumerable(IEnumerable<Agency> models)
        {
            var agency = models.Select(model => new AgencyVM
                                               {
                                                   Id = model.Id,
                                                   Active = model.Active ?? false,
                                                   City = model.City,
                                                   Address = model.Address,
                                                   Country = model.Country,
                                                   Code = model.Code,
                                                   Description = model.Description,
                                                   ImageUrl = model.ImageUrl,
                                                   Manager = model.Manager,
                                                   Name = model.Name,
                                                   Phone = model.Phone,
                                                   Email = model.Email,
                                                   ActiveType = model.Active == false ? "غیرفعال" : "فعال",
                                                   UserId = model.UserId,
                                                   Mobile = model.Mobile,
                                               });
            return agency;
        }
    }
}