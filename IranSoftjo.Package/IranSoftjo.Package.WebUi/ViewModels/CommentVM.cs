using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class CommentVM
    {
        [Key]
        [Display(Name = "سریال")]
        public int CommentID { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        public string UserEmail { get; set; }

        [Display(Name = "وب سایت")]
        public string UserSite { get; set; }

        [Display(Name = "نظر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CommentText { get; set; }

        [Display(Name = "تاریخ ثبت نظر")]
        public DateTime CommentDate { get; set; }

        [Display(Name = "عنوان گروه")]
        public int SubID { get; set; }

        [Display(Name = "نمایش برای عموم")]
        public bool IsActive { get; set; }

        public static explicit operator Comment(CommentVM model)
        {
            var obj = new Comment
                        {
                            CommentDate = model.CommentDate,
                            CommentID = model.CommentID,
                            CommentText = model.CommentText,
                            UserEmail = model.UserEmail,
                            UserName = model.UserName,
                            UserSite = model.UserSite,
                            SubID = model.SubID,
                            IsActive = model.IsActive,
                        };
            return obj;
        }

        public static explicit operator CommentVM(Comment model)
        {

            var pages = new CommentVM
                        {
                            CommentDate = model.CommentDate,
                            CommentID = model.CommentID,
                            CommentText = model.CommentText,
                            UserEmail = model.UserEmail,
                            UserName = model.UserName,
                            UserSite = model.UserSite,
                            SubID = model.SubID,
                            IsActive = model.IsActive != null && (bool) model.IsActive,
                        };
            return pages;
        }

        public static IEnumerable<CommentVM> ToIEnumerable(IEnumerable<Comment> models)
        {
            IEnumerable<CommentVM> pages = models.Select(model => new CommentVM
                                                                {
                                                                    CommentDate = model.CommentDate,
                                                                    CommentID = model.CommentID,
                                                                    CommentText = model.CommentText,
                                                                    UserEmail = model.UserEmail,
                                                                    UserName = model.UserName,
                                                                    UserSite = model.UserSite,
                                                                    SubID = model.SubID,
                                                                    IsActive = model.IsActive != null && (bool)model.IsActive,
                                                                });
            return pages;
        }
    }
}