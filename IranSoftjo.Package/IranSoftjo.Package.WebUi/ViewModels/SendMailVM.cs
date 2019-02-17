using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class SendMailVM
    {
        [Key]
        [Display(Name = "سریال")]
        public int SendMailID { get; set; }

        [Display(Name = "موضوع ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Subject { get; set; }

        [Display(Name = "متن ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [AllowHtml]
        [UIHint("RichText")]
        public string Body { get; set; }

        [Display(Name = "به - Cc")]
        public string CcEmail { get; set; }

        [Display(Name = "به - To")]
        public string ToEmail { get; set; }

        [Display(Name = "به - Bcc")]
        public string BccEmail { get; set; }


        [Display(Name = "ارسال به کل ایمیل های موجود در بانک ایمیل")]
        public bool SendMailToAllPhoneBook { get; set; }

        [Display(Name = "تاریخ ارسال")]
        public DateTime? DateTimeSend { get; set; }

        public static explicit operator SendMail(SendMailVM model)
        {
            var pages = new SendMail
                        {
                            Body = model.Body,
                            CcEmail = model.CcEmail,
                            BccEmail = model.BccEmail,
                            ToEmail = model.ToEmail,
                            Subject = model.Subject,
                            SendMailID = model.SendMailID,
                            DateTimeSend = model.DateTimeSend,
                        };
            return pages;
        }

        public static explicit operator SendMailVM(SendMail model)
        {
            var pages = new SendMailVM
                        {
                            Body = model.Body,
                            CcEmail = model.CcEmail,
                            BccEmail = model.BccEmail,
                            ToEmail = model.ToEmail,
                            Subject = model.Subject,
                            SendMailID = model.SendMailID,
                            DateTimeSend = model.DateTimeSend,

                        };
            return pages;
        }

        public static IEnumerable<SendMailVM> ToIEnumerable(IEnumerable<SendMail> models)
        {
            IEnumerable<SendMailVM> pages = models.Select(model => new SendMailVM
                                                                {
                                                                    Body = model.Body,
                                                                    CcEmail = model.CcEmail,
                                                                    ToEmail = model.ToEmail,
                                                                    Subject = model.Subject,
                                                                    SendMailID = model.SendMailID,
                                                                    DateTimeSend = model.DateTimeSend,
                                                                });
            return pages;
        }
    }
}