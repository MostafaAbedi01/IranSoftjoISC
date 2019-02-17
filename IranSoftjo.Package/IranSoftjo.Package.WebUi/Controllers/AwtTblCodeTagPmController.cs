using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using IranSoftjo.Common;
using IranSoftjo.Core.Web.Mvc;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.Configs;
using IranSoftjo.Package.WebUi.ViewModels;

namespace IranSoftjo.Package.WebUi.Controllers
{
    [Authorize]
    public class AwtTblCodeTagPmController : Controller
    {
        private Entities _db = new Entities();

        public ActionResult Create()
        {
            var pages = new TblCodeTagPm();
            return View((TblCodeTagPmVm)pages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TblCodeTagPmVm tblManholVm)
        {
            if (ModelState.IsValid)
            {
                var tblManhol = (TblCodeTagPm)tblManholVm;
                _db.TblCodeTagPms.Add(tblManhol);
                _db.SaveChanges();
                return RedirectToAction("List", "AwtTblCodeTagPm", new { id = tblManhol.TblCodeTagPmId });
            }
            return View(tblManholVm);
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var pages = _db.TblCodeTagPms.Find(id);
            if (pages == null)
            {
                return HttpNotFound();
            }
            return View((TblCodeTagPmVm)pages);
        }


        [HttpPost]
        public ActionResult Edit(TblCodeTagPmVm tblManholVm)
        {
            if (ModelState.IsValid)
            {
                var tblManhol = (TblCodeTagPm)tblManholVm;
                _db.Entry(tblManhol).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("List", "AwtTblCodeTagPm", new { id = tblManhol.TblCodeTagPmId });
            }
            return View(tblManholVm);
        }

        public ActionResult Delete(int id = 0)
        {
            TblCodeTagPm pages = _db.TblCodeTagPms.Find(id);
            if (pages == null)
            {
                return HttpNotFound();
            }
            return View((TblCodeTagPmVm)pages);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var tblManhol = _db.TblCodeTagPms.Find(id);
            _db.TblCodeTagPms.Remove(tblManhol);
            _db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            return View(TblCodeTagPmVm.ToIEnumerable(_db.TblCodeTagPms.ToList().OrderByDescending(d => d.TblCodeTagPmId)));
        }

    }
}
