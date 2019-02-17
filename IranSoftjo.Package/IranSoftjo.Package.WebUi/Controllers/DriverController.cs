using System;
using System.Data;
using System.Web.Mvc;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.ViewModels;

namespace IranSoftjo.Package.WebUi.Controllers
{
        [Authorize(Roles = "Administrator")]
    public class DriverController : Controller
    {
        private readonly Entities _db = new Entities();

        public ActionResult Index()
        {
            return View(DriverVm.ToIEnumerable(_db.Drivers));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DriverVm DriverVm)
        {
            if (ModelState.IsValid)
            {
                var pages = (Driver)DriverVm;
                _db.Drivers.Add(pages);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            Driver pages = _db.Drivers.Find(id);
            return View((DriverVm)pages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DriverVm DriverVm)
        {
            if (ModelState.IsValid)
            {
                var pages = (Driver)DriverVm;
                _db.Entry(pages).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id = 0)
        {
            Driver pages = _db.Drivers.Find(id);
            return View((DriverVm)pages);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Driver pages = _db.Drivers.Find(id);
            _db.Drivers.Remove(pages);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}