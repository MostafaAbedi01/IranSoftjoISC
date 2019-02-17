using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using System.Web.Security;
using IranSoftjo.Common;
using IranSoftjo.Core.Web.Mvc;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.Android.ViewModels;
using IranSoftjo.Package.WebUi.Payment;
using IranSoftjo.Package.WebUi.ViewModels;
using Mehr;
using Mehr.Web.Mvc.Validation;

namespace IranSoftjo.Package.WebUi.Controllers
{
    public class AccountController : Controller
    {
        private readonly Entities _db = new Entities();

       [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            string userName = User.Identity.Name;
            User user = _db.Users.FirstOrDefault(d => d.Username == userName);
            if (user == null)
            {
                RedirectToAction("Login");
            }
            var userVM = (UserVm)user;
            //userVM.EarnMoneyPropaganda = _db.Propagandas.Count(d => d.UserId == userfind.UserID) * 150;
            return View(userVM);
        }

        [HttpGet]
        public ActionResult Login(string ReturnUrl = "", bool resetPassword = false)
        {
            if (Request.IsAuthenticated)
            {
                if (ReturnUrl != "")
                {
                    Redirect(ReturnUrl);
                }
                RedirectToAction("Edit", "SiteSetting");
            }
            if (resetPassword)
                Session["resetPassword"] = "resetPassword";
            else
                Session["resetPassword"] = null;
            Session["ReturnUrl"] = ReturnUrl;
            HttpContext.Cache.Remove("EditDateTime");
            HttpContext.Cache["EditDateTime"] = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM login, string ReturnUrl = null)
        {
            if (ModelState.IsValid)
            {
                if (_db.Users.Any(u => u.Username == login.Username && u.Password == login.Password))
                {
                    FormsAuthentication.SetAuthCookie(login.Username, login.RememberMe);
                    User user = _db.Users.FirstOrDefault(d => d.Username == login.Username);
                    if (user != null)
                    {
                        string userHostAddressIp = Request.UserHostAddress;
                        _db.LoginLogs.Add(new LoginLog
                                          {
                                              LogDatetime = DateTime.Now,
                                              LogIp = userHostAddressIp,
                                              UserId = user.UserID,
                                              TypeLog = (short)TypeLogEnum.Login,
                                          });
                        _db.SaveChanges();
                    }
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                TempData.SetMessage("نام کاربری یا رمز عبور اشتباه است", MessageType.Error);
            }

            return View();
        }

        public ActionResult LoginCountDay(string ReturnUrl = "", bool resetPassword = false)
        {
            if (resetPassword)
                Session["resetPassword"] = "resetPassword";
            else
                Session["resetPassword"] = null;
            Session["ReturnUrl"] = ReturnUrl;

            return View();
        }

        [HttpPost]
        public ActionResult LoginCountDay(LoginVM login, string ReturnUrl = null)
        {
            if (ModelState.IsValid)
            {
                if (_db.Users.Any(u => u.Username == login.Username && u.Password == login.Password))
                {
                    FormsAuthentication.SetAuthCookie(login.Username, login.RememberMe);
                    User user = _db.Users.FirstOrDefault(d => d.Username == login.Username);
                    if (user != null)
                    {
                        string userHostAddressIp = Request.UserHostAddress;
                        _db.LoginLogs.Add(new LoginLog
                        {
                            LogDatetime = DateTime.Now,
                            LogIp = userHostAddressIp,
                            UserId = user.UserID,
                            TypeLog = (short)TypeLogEnum.Login,
                        });
                        _db.SaveChanges();
                    }
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    return RedirectToAction("Index", "Account");
                }
                TempData.SetMessage("نام کاربری یا رمز عبور اشتباه است", MessageType.Error);
            }

            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            string userHostAddressIp = Request.UserHostAddress;
            string username = User.Identity.Name;
            User user = _db.Users.FirstOrDefault(d => d.Username == username);
            if (user != null)
                _db.LoginLogs.Add(new LoginLog
                                  {
                                      LogDatetime = DateTime.Now,
                                      LogIp = userHostAddressIp,
                                      UserId = user.UserID,
                                      TypeLog = (short)TypeLogEnum.Logout,
                                  });
            _db.SaveChanges();
            HttpContext.Cache.Remove("EditDateTime");
            HttpContext.Cache["EditDateTime"] = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ResetPassword(string resetPassword)
        {
            if (string.IsNullOrEmpty(resetPassword))
            {
                TempData.SetMessage("لطفا نام  کاربری خود را وارد نمایید.", MessageType.Error);
                Session["resetPassword"] = "resetPassword";
            }
            else if (!_db.Users.Any(u => u.Username == resetPassword))
            {
                TempData.SetMessage("نام کاربری وارد شده صحیح نمیباشد.", MessageType.Error);
                Session["resetPassword"] = "resetPassword";
            }
            else
            {
                Session["resetPassword"] = null;
                TempData.SetMessage("کلمه عبور برای ایمیل شما ارسال گردیده شد.", MessageType.Success);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation]
        public ActionResult Register(UserVm modelVM)
        {
            if (ModelState.IsValid)
            {
                modelVM.RoleID = 2;
                var user = (User)modelVM;
                _db.Users.Add(user);
                _db.SaveChanges();
                HttpContext.Cache["EditDateTime"] = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");

                var siteSetting = _db.SiteSettings.FirstOrDefault();
                var mail = new MailMessage();
                string htmlTemplateMail = siteSetting.HtmlTemplateMail;
                mail.Subject = " درخواست عضویت در سایت " + siteSetting.WebSiteTitle;
                htmlTemplateMail = htmlTemplateMail.Replace("#TitleHeader#", mail.Subject);
                htmlTemplateMail = htmlTemplateMail.Replace("#Name#", modelVM.FirstName + " " + modelVM.LastName);
                string body = "";
                body += "درخواست عضویت شما با مشخصات زیر در سایت ثبت گردیده شد: <br/>";
                body += "نام کاربری : " + modelVM.Username + "<br/>";
                body += "نام رمزعبور : " + modelVM.Password + "<br/>";
                body += "تلفن همراه : " + modelVM.Mobile + "<br/>";
                body += "تلفن تماس : " + modelVM.Phone + "<br/>";
                body += "آدرس : " + modelVM.Address + "<br/>";
                htmlTemplateMail = htmlTemplateMail.Replace("#Body#", body);
                mail.To.Add(modelVM.Email);
                mail.CC.Add(siteSetting.SendMailUserName);
                mail.Body = htmlTemplateMail;
                new CommonSendMail().Send(mail, siteSetting.SendMailHost, siteSetting.SendMailEnableSsl, siteSetting.SendMailUserName, siteSetting.SendMailPassword);

                TempData.SetMessage("عضویت شما با نام کاربری " + modelVM.Username + " و ایمیل " + modelVM.Email + " با موفقیت انجام شد ", MessageType.Success);
                FormsAuthentication.RedirectFromLoginPage(modelVM.Username, true);
                return RedirectToAction("Index", "Account");
            }
            return View(modelVM);
        }


        [HttpGet]
        [Authorize]
        public ActionResult ProfileUser()
        {
            string userName = User.Identity.Name;
            User user = _db.Users.FirstOrDefault(d => d.Username == userName);
            Session["User"] = user;
            return View((ProfileVm)user);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ProfileUser(ProfileVm model)
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult PaymentAgency()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult PaymentEarnMoney()
        {
            string userName = User.Identity.Name;
            User user = _db.Users.FirstOrDefault(d => d.Username == userName);
            Session["UserId123"] = user.UserID * 123;
            return View(_db.Propagandas.Where(d => d.UserId == user.UserID));
        }

        [HttpGet]
        [Authorize]
        public ActionResult IncreaseBalance()
        {
            string userName = User.Identity.Name;
            User user = _db.Users.FirstOrDefault(d => d.Username == userName);
            var increaseBalanceVM = new IncreaseBalanceVM();
            if (user != null)
                increaseBalanceVM.AccountBalance = user.AccountBalance.NumericalMoney();
            return View(increaseBalanceVM);
        }

        [HttpPost]
        [Authorize]
        public ActionResult IncreaseBalance(IncreaseBalanceVM model)
        {
            var siteSettings = _db.SiteSettings.FirstOrDefault();
            if (siteSettings != null)
            {
                string authority;
                bool zarinpalPayment = new ZarinpalPayment().ProcessPayment(model.Amount, "افزایش موجودی حساب",
                    siteSettings.WebSiteName + "/Account/PaymentVerification?amount=" + model.Amount, siteSettings.AccountNumberOnline, out authority);
                if (zarinpalPayment)
                {
                    Response.Redirect("https://www.zarinpal.com/pg/StartPay/" + authority);
                    //Response.Redirect(siteSettings.WebSiteName + "/Account/PaymentVerification?amount=" + model.Amount);
                }
                else
                {
                    TempData.SetMessage(string.Format("شماره خطا {0}  : خطا در اتصال به دروازه پرداخت ", authority),
                        MessageType.Error);
                }
            }
            return View();
        }

        [Authorize]
        public ActionResult PaymentVerification(int amount)
        {
            PaymentResponse zarinpalPayment = new ZarinpalPayment().ProcessResponse(Request.QueryString["Authority"],
                Request.QueryString["Status"], amount);
            string userName = User.Identity.Name;
            User user = _db.Users.FirstOrDefault(d => d.Username == userName);
            if (user != null)
            {
                if (zarinpalPayment.Successful)
                {
                    user.AccountBalance += amount;
                    TempData.SetMessage(zarinpalPayment.ResponseMessage, MessageType.Success);
                }
                else
                {
                    TempData.SetMessage(zarinpalPayment.ResponseMessage, MessageType.Error);
                }
                var paymentLogs = new PaymentLog
                                  {
                                      UserID = user.UserID,
                                      Amount = amount,
                                      BankTypeEnum = BankTypeEnum.Zarinpal,
                                      IsSuccessful = zarinpalPayment.Successful,
                                      PaymentDate = DateTime.Now,
                                      PaymentTypeEnum = PaymentTypeEnum.PaymentOnline,
                                      PaymentResponseMessage = "افزایش موجودی با پرداخت آنلاین",
                                      PaymentResponseCode = zarinpalPayment.ResponseMessage,
                                      TrackingCode = zarinpalPayment.RefID.ToString(),
                                  };
                _db.PaymentLogs.Add(paymentLogs);
                _db.SaveChanges();
            }
            else
            {
                TempData.SetMessage(string.Format("لطفا ابتدا وارد کاربری خود شوید"),
              MessageType.Error);
            }
            return View(zarinpalPayment);
        }

        [HttpGet]
        [Authorize]
        public ActionResult LoginLogs()
        {
            string userName = User.Identity.Name;
            var log = _db.LoginLogs.Where(d => d.User.Username == userName).ToList().OrderByDescending(d => d.Id);
            return View(log);
        }
    }
}