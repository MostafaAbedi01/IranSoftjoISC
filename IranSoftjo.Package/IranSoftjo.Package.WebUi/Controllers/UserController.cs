using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;
using IranSoftjo.Common;
using IranSoftjo.Core.Web.Mvc;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.Android.ViewModels;
using IranSoftjo.Package.WebUi.ViewModels;
using Mehr.Web.Mvc.Validation;

namespace IranSoftjo.Package.WebUi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {
        private readonly Entities _db = new Entities();

        public ActionResult Index()
        {
            IOrderedQueryable<User> users = _db.Users.Include(u => u.Role).OrderByDescending(d => d.UserID);
            return View(UserVm.ToIEnumerable(users));
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(_db.Roles, "RoleID", "RoleTitle");
            ViewBag.TblProjectId = new SelectList(_db.TblProjects, "TblProjectId", "ProjectName");
            return View();
        }

        [HttpPost]
        [CaptchaValidation]
        public ActionResult Create(UserVm modelVM)
        {
            if (ModelState.IsValid)
            {
                modelVM.RoleID = 2;
                var user = (User)modelVM;
                _db.Users.Add(user);
                _db.SaveChanges();

                //var siteSetting = _db.SiteSettings.FirstOrDefault();
                //var mail = new MailMessage();
                //string htmlTemplateMail = siteSetting.HtmlTemplateMail;
                //mail.Subject = " درخواست عضویت در سایت " + siteSetting.WebSiteTitle;
                //htmlTemplateMail = htmlTemplateMail.Replace("#TitleHeader#", mail.Subject);
                //htmlTemplateMail = htmlTemplateMail.Replace("#Name#", modelVM.FirstName + " " + modelVM.LastName);
                //string body = "";
                //body += "درخواست عضویت شما با مشخصات زیر در سایت ثبت گردیده شد: <br/>";
                //body += "نام کاربری : " + modelVM.Username + "<br/>";
                //body += "نام رمزعبور : " + modelVM.Password + "<br/>";
                //body += "تلفن همراه : " + modelVM.Mobile + "<br/>";
                //body += "تلفن تماس : " + modelVM.Phone + "<br/>";
                //body += "آدرس : " + modelVM.Address + "<br/>";
                //htmlTemplateMail = htmlTemplateMail.Replace("#Body#", body);
                //mail.To.Add(modelVM.Email);
                //mail.CC.Add(siteSetting.SendMailUserName);
                //mail.Body = htmlTemplateMail;
                //new CommonSendMail().Send(mail, siteSetting.SendMailHost, siteSetting.SendMailEnableSsl, siteSetting.SendMailUserName, siteSetting.SendMailPassword);

                //TempData.SetMessage("عضویت شما با نام کاربری " + modelVM.Username + " و ایمیل " + modelVM.Email + " با موفقیت انجام شد ", MessageType.Success);
                FormsAuthentication.RedirectFromLoginPage(modelVM.Username, true);
                return RedirectToAction("Index", "User");
            }
            ViewBag.TblProjectId = new SelectList(_db.TblProjects, "TblProjectId", "ProjectName");
            ViewBag.RoleID = new SelectList(_db.Roles, "RoleID", "RoleTitle");
            return View(modelVM);
        }

        public ActionResult Edit(int id = 0)
        {
            User users = _db.Users.Include(d => d.Role).FirstOrDefault(d=>d.UserID==id);
            var uservm= (UserVm) users;
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleID = new SelectList(_db.Roles, "RoleID", "RoleTitle", uservm.RoleID);
            return View(uservm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserVm userVM)
        {
            if (ModelState.IsValid)
            {
                var user = (User) userVM;
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleID = new SelectList(_db.Roles, "RoleID", "RoleTitle", userVM.RoleID);
            return View(userVM);
        }

        public ActionResult EditProfile()
        {
            User users = _db.Users.FirstOrDefault(d => d.Username == User.Identity.Name);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(User users)
        {
            users.RoleID = 1;
            if (ModelState.IsValid)
            {
                _db.Entry(users).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(users);
        }

      
        public ActionResult Delete(int id = 0)
        {
            User users = _db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View((UserVm) users);
        }

   
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User users = _db.Users.Find(id);
            foreach (var item in _db.LoginLogs.Where(d => d.UserId == id))
            {
                _db.LoginLogs.Remove(item);
            }
            _db.Users.Remove(users);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}