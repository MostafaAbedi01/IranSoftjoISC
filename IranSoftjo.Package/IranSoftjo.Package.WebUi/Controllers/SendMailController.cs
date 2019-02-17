using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;
using System.Web.Mvc;
using IranSoftjo.Common;
using IranSoftjo.Core.Web.Mvc;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.ViewModels;

namespace IranSoftjo.Package.WebUi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SendMailController : Controller
    {
        private readonly Entities _db = new Entities();

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            IEnumerable<SendMailVM> pagesVM = SendMailVM.ToIEnumerable(_db.SendMails);
            return View(pagesVM);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var sendMailVM = new SendMailVM();
            return View(sendMailVM);
        }

        [HttpPost]
        public ActionResult Create(SendMailVM model)
        {
            return SendMail(model);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id = 0)
        {
            var sendMails = _db.SendMails.Find(id);
            return View((SendMailVM)sendMails);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(SendMailVM model)
        {
            return SendMail(model);
        }


        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id = 0)
        {
            var pages = _db.SendMails.Find(id);
            return View((SendMailVM)pages);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var pages = _db.SendMails.Find(id);
            _db.SendMails.Remove(pages);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        private ActionResult SendMail(SendMailVM model)
        {
            var siteSetting = _db.SiteSettings.FirstOrDefault();
            var mail = new MailMessage();
            string allEmailSended = "";
            if (!string.IsNullOrEmpty(model.ToEmail))
            {
                string[] splitTo = model.ToEmail.Split(';');
                foreach (var item in splitTo)
                {
                    mail.To.Add(item);
                    allEmailSended += item + ";";
                }
            }
            if (!string.IsNullOrEmpty(model.CcEmail))
            {
                string[] splitCc = model.CcEmail.Split(';');
                foreach (var item in splitCc)
                {
                    mail.CC.Add(item);
                    allEmailSended += item + ";";
                }
            }
            if (!string.IsNullOrEmpty(model.BccEmail))
            {
                string[] splitBCc = model.BccEmail.Split(';');
                foreach (var item in splitBCc)
                {
                    mail.Bcc.Add(item);
                    allEmailSended += item + ";";
                }
            }
            if (model.SendMailToAllPhoneBook)
            {
                foreach (var vari in _db.PhoneBooks.Where(d => d.Email != null))
                {
                    if (!string.IsNullOrEmpty(vari.Email))
                    {
                        mail.Bcc.Add(vari.Email);
                        allEmailSended += vari.Email + ";";
                    }
                }
            }
            mail.Subject = model.Subject;
            mail.Body = Server.HtmlDecode(model.Body);
            new CommonSendMail().Send(mail, siteSetting.SendMailHost, siteSetting.SendMailEnableSsl, siteSetting.SendMailUserName, siteSetting.SendMailPassword,2525);


            model.DateTimeSend = DateTime.Now;
            model.Body = Server.HtmlDecode(model.Body);
            _db.SendMails.Add((SendMail)model);
            _db.SaveChanges();
            TempData.SetMessage("ارسال ایمیل های " + allEmailSended + " با موفقیت انجام شد", MessageType.Success);
            return RedirectToAction("Index");
        }


    }
}