using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using eShopMvc.Models;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.ViewModels;

namespace IranSoftjo.Package.WebUi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductGroupController : Controller
    {
        private readonly Entities _db = new Entities();

        public ActionResult Index()
        {
            return View(ProductGroupVM.ToIEnumerable(_db.ProductGroups.ToList().OrderByDescending(d => d.ProductGroupOrder)));
        }


        public JsonResult ProductGroupTree(int? id)
        {
            var employees = from e in _db.ProductGroups
                            where (id.HasValue ? e.ProductGroupIDTree == id : e.ProductGroupIDTree == null)
                            select new
                            {
                                id = e.ProductGroupID,
                                Name = e.ProductGroupTitle,
                                hasChildren = e.ProductGroups1.Any()
                            };
            return Json(employees, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductGroupVM productGroupVM)
        {
            if (ModelState.IsValid)
            {
                var productGroup = (ProductGroup)productGroupVM;
                _db.ProductGroups.Add(productGroup);
                _db.SaveChanges();
                HttpContext.Cache["EditDateTime"] = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                return RedirectToAction("Index");
            }
            return View(productGroupVM);
        }

        public ActionResult Edit(int id = 0)
        {
            ProductGroup productgroups = _db.ProductGroups.Find(id);
            if (productgroups == null)
            {
                return HttpNotFound();
            }
            return View((ProductGroupVM)productgroups);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductGroup productgroups)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(productgroups).State = EntityState.Modified;
                _db.SaveChanges();
                HttpContext.Cache["EditDateTime"] = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                return RedirectToAction("Index");
            }
            return View((ProductGroupVM)productgroups);
        }

        public ActionResult Delete(int id = 0)
        {
            ProductGroup productgroups = _db.ProductGroups.Find(id);
            if (productgroups == null)
            {
                return HttpNotFound();
            }
            return View((ProductGroupVM)productgroups);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductGroup productgroups = _db.ProductGroups.Find(id);

            _db.ProductGroups.Remove(productgroups);

            _db.SaveChanges();
            HttpContext.Cache["EditDateTime"] = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
            return RedirectToAction("Index");
        }
    }
}