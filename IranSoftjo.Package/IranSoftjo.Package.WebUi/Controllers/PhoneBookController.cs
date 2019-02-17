using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using IranSoftjo.Core.Web.Mvc;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.ViewModels;

namespace IranSoftjo.Package.WebUi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PhoneBookController : Controller
    {
        private readonly Entities _db = new Entities();

        public ActionResult Index()
        {
            IEnumerable<PhoneBookVM> phoneBookVM = PhoneBookVM.ToIEnumerable(_db.PhoneBooks.Include(d => d.PhoneBookGroup).OrderByDescending(d=>d.PhoneBookID));
            return View(phoneBookVM);
        }

        public ActionResult Create()
        {
            ViewBag.PhoneBookGroupID = new SelectList(_db.PhoneBookGroups.OrderBy(d => d.PhoneBookGroupTitle), "PhoneBookGroupID", "PhoneBookGroupTitle");
            ModelState.Clear();
            var model = new PhoneBookVM
                        {
                            Title = string.Empty,
                            PhoneNumber = string.Empty,
                            Email = string.Empty,
                            Mobile = string.Empty,
                            Site = string.Empty,
                            Comment = string.Empty
                        };
            return View(model);
        }
        public ActionResult AllMobile()
        {
            var phoneBookVM = new PhoneBookVM();
            foreach (var item in _db.PhoneBooks)
            {
                if (!string.IsNullOrEmpty(item.Mobile))
                    phoneBookVM.Mobile += item.Mobile.Trim() + ",";
            }
            return View(phoneBookVM);
        }
        public ActionResult AllEmail()
        {
            var phoneBookVM = new PhoneBookVM();
            foreach (var item in _db.PhoneBooks)
            {
                if (!string.IsNullOrEmpty(item.Email))
                    phoneBookVM.Email += item.Email.Trim() + ";";
            }
            return View(phoneBookVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PhoneBookVM model)
        {
            var pages = (PhoneBook)model;
            _db.PhoneBooks.Add(pages);
            _db.SaveChanges();
            TempData.SetMessage("مشخصات " + model.Title + "|" + model.Mobile + "|" + model.PhoneNumber + "|" + model.Email + " با موفقیت ثبت شد", MessageType.Success);
            ViewBag.PhoneBookGroupID = new SelectList(_db.PhoneBookGroups, "PhoneBookGroupID", "PhoneBookGroupTitle");
            //model.Title = string.Empty;
            //model.PhoneNumber = string.Empty;
            //model.Email = string.Empty;
            //model.Mobile = string.Empty;
            //model.Site = string.Empty;
            //model.Comment = string.Empty;
            //return View(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var pages = _db.PhoneBooks.Include(d => d.PhoneBookGroup).FirstOrDefault(d=>d.PhoneBookID==id);
            ViewBag.PhoneBookGroupID = new SelectList(_db.PhoneBookGroups.OrderBy(d => d.PhoneBookGroupTitle), "PhoneBookGroupID", "PhoneBookGroupTitle", pages.PhoneBookGroupID);
            return View((PhoneBookVM)pages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PhoneBookVM marqueesVM)
        {
            if (ModelState.IsValid)
            {
                var model = (PhoneBook)marqueesVM;
                _db.Entry(model).State = EntityState.Modified;
                _db.SaveChanges();
                TempData.SetMessage("مشخصات " + model.Mobile + "|" + model.PhoneNumber + "|" + model.Email + " با موفقیت ویرایش شد", MessageType.Success);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id = 0)
        {
            var pages = _db.PhoneBooks.Find(id);
            if (pages == null)
            {
                return HttpNotFound();
            }
            return View((PhoneBookVM)pages);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var model = _db.PhoneBooks.Find(id);
            _db.PhoneBooks.Remove(model);
            _db.SaveChanges();
            TempData.SetMessage("مشخصات " + model.Mobile + "|" + model.PhoneNumber + "|" + model.Email + " با موفقیت حذف شد", MessageType.Warn);
            return RedirectToAction("Index");
        }
    }
}