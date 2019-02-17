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
using Mehr;

namespace IranSoftjo.Package.WebUi.Controllers
{
    [Authorize]
    public class TblKeyValueController : Controller
    {
        private Entities _db = new Entities();

        public ActionResult Create()
        {
            var pages = new TblKeyValue();
            ViewBag.Type = new SelectList(_db.TblKeyValues.Where(d => d.Type == 200), "KeyID", "Title");
            return View((TblKeyValueVm)pages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TblKeyValueVm tblKeyValueVm)
        {
            if (ModelState.IsValid)
            {
                var tblKeyValue = (TblKeyValue)tblKeyValueVm;
                var max = _db.TblKeyValues.Where(d => d.Type == tblKeyValue.Type).Max(d => d.KeyID);
                if (max != null)
                {
                    int dd = (int) max;
                    tblKeyValue.KeyID = dd + 1;
                }
                else
                {
                    tblKeyValue.KeyID = 1;
                }
                _db.TblKeyValues.Add(tblKeyValue);
                _db.SaveChanges();
                return RedirectToAction("List", "TblKeyValue");
            }
             ViewBag.Type = new SelectList(_db.TblKeyValues.Where(d => d.Type == 200), "KeyID", "Title");
            return View(tblKeyValueVm);
        }


        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var pages = _db.TblKeyValues.Find(id);
            ViewBag.Type = new SelectList(_db.TblKeyValues.Where(d => d.Type == 200), "KeyID", "Title", pages.Type);
            if (pages == null)
            {
                return HttpNotFound();
            }
            return View((TblKeyValueVm)pages);
        }


        [HttpPost]
        public ActionResult Edit(TblKeyValueVm TblKeyValueVm)
        {
            if (ModelState.IsValid)
            {
                var tblKeyValue = (TblKeyValue)TblKeyValueVm;
                _db.Entry(tblKeyValue).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("List", "TblKeyValue");
            }

            return View(TblKeyValueVm);
        }

        public ActionResult Delete(int id = 0)
        {
            TblKeyValue pages = _db.TblKeyValues.Find(id);
            if (pages == null)
            {
                return HttpNotFound();
            }
            return View((TblKeyValueVm)pages);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedItem(int id)
        {
            var TblKeyValue = _db.TblKeyValues.Find(id);
            _db.TblKeyValues.Remove(TblKeyValue);
            _db.SaveChanges();
            return RedirectToAction("List", "TblKeyValue");
        }

        public ActionResult Details(int id = 0)
        {
            var TblKeyValue = _db.TblKeyValues.FirstOrDefault(d => d.TblKeyValueID == id);
            if (TblKeyValue == null)
            {
                return HttpNotFound();
            }
            return View((TblKeyValueVm)TblKeyValue);
        }

        public ActionResult List()
        {
            ViewBag.Type = _db.TblKeyValues.Where(d => d.Type == 200);
            return View(TblKeyValueVm.ToIEnumerable(_db.TblKeyValues.Where(d => d.Type == 3 || d.Type == 201 || d.Type == 202).ToList().OrderByDescending(d => d.TblKeyValueID)));
        }
    }
}
