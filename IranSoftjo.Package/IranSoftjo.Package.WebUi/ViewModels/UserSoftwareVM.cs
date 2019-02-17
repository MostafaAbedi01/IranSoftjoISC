using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class UserSoftwareVM
    {
        [Key]
        [Display(Name = "سریال")]
        public int UserID { get; set; }

        [Display(Name = "گروه کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DefaultValue(2)]
        public int RoleID { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Username { get; set; }

        [Display(Name = "نام کامل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FirstName { get; set; }

        [Display(Name = "سال تاسیس")]
        public string LastName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "کلمه عبور (مجدد)")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "{0} و {1} یکسان نیست.")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "آدرس ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "{0} وارد شده صحیح نیست")]
        public string Email { get; set; }

        [Display(Name = "شماره تماس")]
        public string Phone { get; set; }

        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Mobile { get; set; }

        [Display(Name = "آدرس")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        
        [Display(Name = "نقش")]
        public string RoleTitle { get; set; }

        [Display(Name = "آدرس لوگو")]
        public string UserImageUrl { get; set; }

        public string LastLoginDateTime { get; set; }
        public string LastLoginIp { get; set; }
        public int AccountBalance { get; set; }
        public virtual Role Roles { get; set; }

         [Display(Name = "درآمد تبلیغات")]
         public double EarnMoneyPropaganda { get; set; }

         [Display(Name = "درآمد نمایندگی")]
         public double EarnMoneyAgency { get; set; }

        public string Captcha { get; set; }

        public static explicit operator User(UserSoftwareVM model)
        {
            var obj = new User
                      {
                          UserID = model.UserID,
                          RoleID = model.RoleID,
                          Address = model.Address,
                          FirstName = model.FirstName,
                          LastName = model.LastName,
                          Password = model.Password,
                          Username = model.Username,
                          Phone = model.Phone,
                          AccountBalance = model.AccountBalance,
                          Email = model.Email,
                          Mobile = model.Mobile,
                      };
            return obj;
        }

        public static explicit operator UserSoftwareVM(User model)
        {
            var obj = new UserSoftwareVM
                      {
                          UserID = model.UserID,
                          RoleID = model.RoleID,
                          Address = model.Address,
                          FirstName = model.FirstName,
                          LastName = model.LastName,
                          Password = model.Password,
                          Username = model.Username,
                          Phone = model.Phone,
                          AccountBalance = model.AccountBalance,
                          UserImageUrl = model.UserImageUrl,
                          Email = model.Email,
                          Mobile = model.Mobile,
                          RoleTitle = model.Role.RoleTitle,
                      };
            LoginLog loginlog = model.LoginLogs.LastOrDefault();
            if (loginlog != null)
            {
                obj.LastLoginDateTime = loginlog.LogDatetime.ToString();
                obj.LastLoginIp = loginlog.LogIp;
            }
            return obj;
        }

        public static IEnumerable<UserSoftwareVM> ToIEnumerable(IEnumerable<User> models)
        {
            IEnumerable<UserSoftwareVM> user = models.Select(model => new UserSoftwareVM
                                                              {
                                                                  UserID = model.UserID,
                                                                  RoleID = model.RoleID,
                                                                  Address = model.Address,
                                                                  FirstName = model.FirstName,
                                                                  LastName = model.LastName,
                                                                  Password = model.Password,
                                                                  Username = model.Username,
                                                                  Phone = model.Phone,
                                                                  AccountBalance = model.AccountBalance,
                                                                  Email = model.Email,
                                                                  Mobile = model.Mobile,
                                                                  RoleTitle = model.Role.RoleTitle,
                                                              });
            return user;
        }
    }
}