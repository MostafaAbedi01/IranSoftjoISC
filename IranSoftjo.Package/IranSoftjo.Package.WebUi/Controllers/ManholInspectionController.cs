using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IranSoftjo.Common;
using IranSoftjo.Core.Web.Mvc;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.Configs;
using IranSoftjo.Package.WebUi.ViewModels;

namespace IranSoftjo.Package.WebUi.Android.Controllers
{
    [Authorize]
    public class ManholInspectionController : Controller
    {
        private readonly Entities _db = new Entities();

        public ActionResult List(int? id)
        {
            ViewBag.TypeManhole = _db.TblKeyValues.Where(d => d.Type == 1);
            ViewBag.JenseManhole = _db.TblKeyValues.Where(d => d.Type == 2);
            ViewBag.VaziatManhole = _db.TblKeyValues.Where(d => d.Type == 3);
            ViewBag.ViewBag10 = _db.TblKeyValues.Where(d => d.Type == 10);
            ViewBag.ViewBag8 = _db.TblKeyValues.Where(d => d.Type == 8);
            if (id == null)
            {
                return View(TblInspectionVm.ToIEnumerable(_db.TblInspections.ToList().OrderByDescending(d => d.TblInspectionId)));
            }
            return View(TblInspectionVm.ToIEnumerable(_db.TblInspections.Where(d => d.TblManholId == id).ToList().OrderByDescending(d => d.TblInspectionId)));

        }

        public ActionResult Details(int id = 0)
        {
            var tblManhol = _db.TblInspections.FirstOrDefault(d => d.TblInspectionId == id);
            if (tblManhol == null)
            {
                return HttpNotFound();
            }
            return View((TblInspectionVm)tblManhol);
        }

        public ActionResult Delete(int id = 0)
        {
            TblInspection pages = _db.TblInspections.Find(id);
            if (pages == null)
            {
                return HttpNotFound();
            }
            return View((TblInspectionVm)pages);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var tblManhol = _db.TblInspections.Find(id);
            _db.TblInspections.Remove(tblManhol);
            _db.SaveChanges();
            return RedirectToAction("List");
        }


        [HttpGet]
        public ActionResult Add()
        {
            var pages = new TblInspectionVm
            {
                DateOrder = DateTime.Now
            };
            return View(pages);
        }

        [HttpPost]
        public ActionResult Add(TblInspectionVm modelVm)
        {
            var model = (TblInspection)modelVm;
            var tblManholId = 1;
            var tblManhol = _db.TblManhols.FirstOrDefault(d => d.CodePm == modelVm.CodePm);
            if (tblManhol != null)
            {
                model.TblManholId = tblManhol.TblManholId;
            }
            else
            {
                TempData.SetMessage("کد پی ام اشتباه است", MessageType.Error);
                return View(modelVm);
            }
            _db.TblInspections.Add(model);
            _db.SaveChanges();
            TempData.SetMessage("کد پی ام با موفقیت ثبت شد", MessageType.Success);
            modelVm.CodePm = 0;
            return View(modelVm);
        }


    }
}
