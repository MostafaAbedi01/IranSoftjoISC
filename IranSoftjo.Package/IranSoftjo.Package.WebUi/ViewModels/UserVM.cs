using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.Android.ViewModels
{
    public class UserVm
    {
        [Key]
        [Display(Name = "سریال")]
        public int UserID { get; set; }

        [Display(Name = "گروه کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int RoleID { get; set; }

        [Display(Name = "کد ملی")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "{0} وارد شده صحیح نیست")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CodeMeli { get; set; }

        [Display(Name = "نام کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Username { get; set; }

        [Display(Name = " نام کاربر جدید")]
        public string UsernameNew { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string LastName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "کلمه عبور (مجدد)")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "{0} و {1} یکسان نیست.")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "آدرس ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "{0} وارد شده صحیح نیست")]
        public string Email { get; set; }

        [Display(Name = "شماره تماس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Phone { get; set; }

        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Mobile { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "نقش")]
        public string RoleTitle { get; set; }

        [Display(Name = " عکس 3*4")]
        public string ImageUrlUser3in4 { get; set; }

        [Display(Name = " عکس کارت ملی")]
        public string ImageUrlCardMeli { get; set; }

        public string LastLoginDateTime { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        public DateTime? CreateDate { get; set; }

        public string LastLoginIp { get; set; }
        public int AccountBalance { get; set; }
        public virtual Role Roles { get; set; }

        [Display(Name = "درآمد تبلیغات")]
        public double EarnMoneyPropaganda { get; set; }

        [Display(Name = "درآمد نمایندگی")]
        public double EarnMoneyAgency { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "کد پستی 10 رقمی")]
        public string AddressPostalCode { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "استان")]
        public short AddressProvince { get; set; }

        [Display(Name = "عکس اسکن قبض")]
        public string ImageUrlScanBill { get; set; }

        [Display(Name = "عکس پروفایل")]
        public string ImageUrlProfile { get; set; }
        public bool IsOnline { get; set; }
        public bool UserPassValid { get; set; }

        // public string Captcha { get; set; }

        public static explicit operator User(UserVm model)
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
                Mobile = model.Mobile,
                //CreateDate = model.CreateDate,
                //ImageUrlProfile = model.ImageUrlProfile,
                Email = model.Email
            };
            return obj;
        }

        public static explicit operator UserVm(User model)
        {
            var obj = new UserVm
            {
                UserID = model.UserID,
                RoleID = model.RoleID,
                Address = model.Address,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Username = model.Username,
                Phone = model.Phone,
                Mobile = model.Mobile,
                RoleTitle = model.Role.RoleTitle,
                //CreateDate = model.CreateDate,
                //ImageUrlProfile = model.ImageUrlProfile,
                //IsOnline = model.IsActive,

                Email = model.Email,
            };
            //var loginlog = model.LoginLogs.LastOrDefault();
            //if (loginlog != null)
            //{
            //    obj.LastLoginDateTime = loginlog.LogDatetime.ToString();
            //    obj.LastLoginIp = loginlog.LogIp;
            //}

            return obj;
        }

        public static IEnumerable<UserVm> ToIEnumerable(IEnumerable<User> models)
        {
            var user = models.Select(model => new UserVm
            {
                UserID = model.UserID,
                RoleID = model.RoleID,
                Address = model.Address,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = model.Password,
                Username = model.Username,
                Phone = model.Phone,
                Mobile = model.Mobile,
                RoleTitle = model.Role.RoleTitle,
                //ImageUrlProfile = model.ImageUrlProfile,
                //CreateDate = model.CreateDate,
                Email = model.Email
            });
            return user;
        }
    }
}