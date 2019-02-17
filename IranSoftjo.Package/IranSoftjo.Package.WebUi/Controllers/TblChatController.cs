using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;
using System.Web.Security;
using IranSoftjo.Common;
using IranSoftjo.Core.Web.Mvc;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.ViewModels;
using Mehr.Web.Mvc.Validation;

namespace IranSoftjo.Package.WebUi.Controllers
{
    [Authorize]
    public class TblChatController : Controller
    {
        private readonly Entities _db = new Entities();

        public ActionResult Index()
        {
            string username = User.Identity.Name;
            User userget = _db.Users.FirstOrDefault(u => u.Username == username);
            var tblChats = _db.TblChats.Where(
                    d =>
                        ( d.TblUserIdTo == userget.UserID) ||
                        (d.TblUserIdFrom == userget.UserID)
                    ).OrderByDescending(d => d.TblChatId);
            ViewBag.TblUserIdTo = _db.Users;
            ViewBag.TblUserIdFrom = _db.Users;
            return View(TblChatVm.ToIEnumerable(tblChats));
        }

        [HttpGet]
        public ActionResult Create()
        {
            string username = User.Identity.Name;
            User userget = _db.Users.FirstOrDefault(u => u.Username == username);
            ViewBag.TblUserIdTo = new SelectList(_db.Users.Where(d => d.UserID != userget.UserID), "UserID", "LastName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(TblChatVm modelVM)
        {
            string username = User.Identity.Name;
            User userget = _db.Users.FirstOrDefault(u => u.Username == username);
            if (ModelState.IsValid)
            {
                if (userget != null)
                {
                    modelVM.TblUserIdFrom = userget.UserID;
                    var user = (TblChat) modelVM;
                    user.DateSabt = DateTime.Now;
                    _db.TblChats.Add(user);
                    _db.SaveChanges();
                }
                else
                {
                    TempData.SetMessage("کاربر یافت نشد");
                }
                return RedirectToAction("Index", "TblChat");
            }
            ViewBag.TblUserIdTo = new SelectList(_db.Users.Where(d => d.UserID != userget.UserID), "UserID", "LastName");
            return View(modelVM);
        }


        public ActionResult Delete(int id = 0)
        {
            TblChat users = _db.TblChats.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View((TblChatVm)users);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblChat users = _db.TblChats.Find(id);
            _db.TblChats.Remove(users);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}