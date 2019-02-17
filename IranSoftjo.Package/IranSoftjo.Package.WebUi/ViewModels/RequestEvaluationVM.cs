using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class RequestEvaluationVM
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "آدرس ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "{0} وارد شده صحیح نیست")]
        public string Email { get; set; }

        [Display(Name = "شماره تماس")]
        public string PhoneNumber { get; set; }

        [Display(Name = "آدرس وب سایت")]
        public string Website { get; set; }

        [Display(Name = "تلفن همراه")]
        public string Mobile { get; set; }

        [Display(Name = "توضیحات پروژه")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "نوع پروژه")]
        public short? ProjectType { get; set; }

        [Display(Name = "بودجه تقریبی")]
        public short? Amount { get; set; }

        [Display(Name = "نوع پروژه")]
        public SelectList SelectListProjectType { get; set; }

        [Display(Name = "بودجه تقریبی")]
        public SelectList SelectListAmount { get; set; }

        public static explicit operator RequestEvaluation(RequestEvaluationVM model)
        {
            var obj = new RequestEvaluation
                      {
                          Id = model.Id,
                          Description = model.Description,
                          Name = model.Name,
                          Email = model.Email,
                          PhoneNumber = model.PhoneNumber,
                          Mobile = model.Mobile,
                          ProjectType = model.ProjectType,
                          Amount = model.Amount,
                          Website = model.Website,
                      };
            return obj;
        }

        public static explicit operator RequestEvaluationVM(RequestEvaluation model)
        {
            var obj = new RequestEvaluationVM
                      {
                          Id = model.Id,
                          Description = model.Description,
                          Name = model.Name,
                          Email = model.Email,
                          PhoneNumber = model.PhoneNumber,
                          Mobile = model.Mobile,
                          ProjectType = model.ProjectType,
                          Amount = model.Amount,
                          Website = model.Website,
                      };
            return obj;
        }

        public static IEnumerable<RequestEvaluationVM> ToIEnumerable(IEnumerable<RequestEvaluation> models)
        {
            IEnumerable<RequestEvaluationVM> user = models.Select(model => new RequestEvaluationVM
                                                                           {
                                                                               Id = model.Id,
                                                                               Description = model.Description,
                                                                               Name = model.Name,
                                                                               Email = model.Email,
                                                                               PhoneNumber = model.PhoneNumber,
                                                                               Mobile = model.Mobile,
                                                                               ProjectType = model.ProjectType,
                                                                               Amount = model.Amount,
                                                                               Website = model.Website,
                                                                           });
            return user;
        }
    }
}