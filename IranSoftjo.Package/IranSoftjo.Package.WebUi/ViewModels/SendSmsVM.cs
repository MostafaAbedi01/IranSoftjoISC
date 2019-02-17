using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class SendSmsVm
    {
        [Key]
        [Display(Name = "سریال")]
        public int SendSmsID { get; set; }

        [Display(Name = "سریال راننده")]
        public int? DriverID { get; set; }

        [Display(Name = "سریال مسافر")]
        public int? PassengerID { get; set; }

        [Display(Name = "سریال مسیر")]
        public int? TrackID { get; set; }

        [Display(Name = "کد مسیر")]
        public int? TrackCode { get; set; }

        [Display(Name = "متن پیامک")]
        public string SmsText { get; set; }
        public string Mobile { get; set; }

        [Display(Name = "تاریخ ارسال")]
        public DateTime? SendDateTime { get; set; }

        public static explicit operator SendSm(SendSmsVm model)
        {
            var pages = new SendSm
                        {
                            SmsText = model.SmsText,
                            DriverID = model.DriverID,
                            PassengerID = model.PassengerID,
                            TrackID = model.TrackID,
                            SendDateTime = model.SendDateTime,
                            SendSmsID = model.SendSmsID,
                        };
            return pages;
        }

        public static explicit operator SendSmsVm(SendSm model)
        {
            var pages = new SendSmsVm
                        {
                            SmsText = model.SmsText,
                            DriverID = model.DriverID,
                            PassengerID = model.PassengerID,
                            TrackID = model.TrackID,
                            SendDateTime = model.SendDateTime,
                            SendSmsID = model.SendSmsID,
                        };
            return pages;
        }

        public static IEnumerable<SendSmsVm> ToIEnumerable(IEnumerable<SendSm> models)
        {
            IEnumerable<SendSmsVm> pages = models.Select(model => new SendSmsVm
                                                                {
                                                                    SmsText = model.SmsText,
                                                                    DriverID = model.DriverID,
                                                                    PassengerID = model.PassengerID,
                                                                    TrackID = model.TrackID,
                                                                    SendDateTime = model.SendDateTime,
                                                                    SendSmsID = model.SendSmsID,
                                                                });
            return pages;
        }
    }
}