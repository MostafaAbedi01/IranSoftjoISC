using System.ComponentModel.DataAnnotations;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.Android.ViewModels;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class ProfileVm : UserVm
    {
        [Display(Name = "کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "تکرار کلمه عبور جدید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [System.Web.Mvc.Compare("Password", ErrorMessage = "{0} و {1} یکسان نیست.")]
        public string NewPasswordConfirm { get; set; }

        public static explicit operator ProfileVm(User model)
        {
            var obj = new ProfileVm
            {
                UserID = model.UserID,
                RoleID = model.RoleID,
                Address = model.Address,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Username = model.Username,
                Phone = model.Phone,
            };
            return obj;
        }

    }
}