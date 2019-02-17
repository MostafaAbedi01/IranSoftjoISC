using System;
using System.Data;
using System.Web.Mvc;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.ViewModels;

namespace IranSoftjo.Package.WebUi.Controllers
{
        [Authorize(Roles = "Administrator")]
    public class PassengerController : Controller
    {
        private readonly Entities _db = new Entities();

        public ActionResult Index()
        {
            return View(PassengerVm.ToIEnumerable(_db.Passengers));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PassengerVm PassengerVm)
        {
            if (ModelState.IsValid)
            {
                var pages = (Passenger)PassengerVm;
                _db.Passengers.Add(pages);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            Passenger pages = _db.Passengers.Find(id);
            return View((PassengerVm)pages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PassengerVm PassengerVm)
        {
            if (ModelState.IsValid)
            {
                var pages = (Passenger)PassengerVm;
                _db.Entry(pages).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id = 0)
        {
            Passenger pages = _db.Passengers.Find(id);
            return View((PassengerVm)pages);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Passenger pages = _db.Passengers.Find(id);
            _db.Passengers.Remove(pages);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}