using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.ViewModels;
using Mehr;
using Newtonsoft.Json;

namespace IranSoftjo.Package.WebUi.Controllers
{
    public class SendSmsApiController : ApiController
    {
        private readonly Entities _db = new Entities();
        public SendSm Post(string from, string to, string message, int timestamp, int ticket)
        {
            var json = Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            Configuration.Formatters.Remove(Configuration.Formatters.XmlFormatter);

            var sendSm = new SendSm
            {
                SmsText = message + "|" + from + "|" + to + "|" + timestamp + "|" + ticket,
                DriverID = 1,
                PassengerID = 1,
                SendDateTime = DateTime.Now,
                SendSmsID = 1,
                TrackID = 1,
            };
            _db.SendSms.Add(sendSm);
            _db.SaveChanges();
            return sendSm;
        }

        public SendSm Get()
        {
            var json = Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            Configuration.Formatters.Remove(Configuration.Formatters.XmlFormatter);

            return _db.SendSms.FirstOrDefault();
        }

        public void Get(string from, string text, string to, string udh)
        {
            _db.SendSms.Add(new SendSm
            {
                SmsText = text + "|" + from + "|" + to + "|" + udh,
                DriverID = 1,
                PassengerID = 1,
                SendDateTime = DateTime.Now,
                SendSmsID = 1,
                TrackID = 1,
            });
            _db.SaveChanges();
        }
        //public string GetMobile(string driverId)
        //{
        //    if (!string.IsNullOrEmpty(driverId))
        //    {

        //        if (_lstPassenger.Count == 0)
        //        {
        //            _lstDrivers = _db.Drivers.ToList();
        //        }
        //        var passenger = _lstDrivers.FirstOrDefault(d => d.Code == driverId);
        //        if (passenger != null)
        //        {
        //            return passenger.Mobile;
        //        }
        //        return "موبایل راننده با این مشخصات یافت نشد";
        //    }
        //    return "";
        //}

        //private List<Passenger> _lstPassenger = new List<Passenger>();
        //private List<Driver> _lstDrivers = new List<Driver>();
        //private List<Track> _lstTrack = new List<Track>();
        //public string Get(string passengerId, string driverId, string trackId)
        //{
        //    if (!string.IsNullOrEmpty(passengerId ))
        //    {

        //        if (_lstPassenger.Count == 0)
        //        {
        //            _lstPassenger = _db.Passengers.ToList();
        //        }
        //        Passenger passenger = _lstPassenger.FirstOrDefault(d => d.Code == passengerId);
        //        if (passenger != null)
        //        {
        //            string strPassenger = "نام مسافر :" + passenger.Name +
        //                                  " | آدرس مسافر:" + passenger.Address +
        //                                  " | موبایل مسافر: " + passenger.Mobile + "\n ";
        //            return strPassenger;
        //        }
        //        return "مسافری با این مشخصات یافت نشد";
        //    }
        //    if (!string.IsNullOrEmpty(driverId))
        //    {

        //        if (_lstPassenger.Count == 0)
        //        {
        //            _lstDrivers = _db.Drivers.ToList();
        //        }
        //        var passenger = _lstDrivers.FirstOrDefault(d => d.Code == driverId);
        //        if (passenger != null)
        //        {
        //            string strPassenger = "نام راننده:" + passenger.FirstName +
        //                                  " | نام خانوادگی راننده:" + passenger.LastName +
        //                                  " | موبایل راننده : " + passenger.Mobile +"\n ";
        //            return strPassenger;
        //        }
        //        return "راننده با این مشخصات یافت نشد"; 
        //    }
        //    if (!string.IsNullOrEmpty(trackId))
        //    {

        //        if (_lstTrack.Count == 0)
        //        {
        //            _lstTrack = _db.Tracks.ToList();
        //        }
        //        var passenger = _lstTrack.FirstOrDefault(d => d.Code == trackId);
        //        if (passenger != null)
        //        {
        //            string strPassenger = "نام مسیر :" + passenger.Name +
        //                                  " | مبلغ مسیر :" + passenger.Price +
        //                                  " | محدوده : " + passenger.TypeTrack + "\n ";
        //            return strPassenger;
        //        }
        //        return "مسیر با این مشخصات یافت نشد";
        //    }
        //    return "";
        //}


    }
}