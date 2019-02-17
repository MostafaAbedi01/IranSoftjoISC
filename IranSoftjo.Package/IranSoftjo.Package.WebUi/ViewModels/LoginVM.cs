using System.ComponentModel.DataAnnotations;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "نام کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Username { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "مشخصات من را به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}