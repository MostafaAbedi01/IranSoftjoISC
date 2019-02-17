using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.ViewModels;

namespace IranSoftjo.Package.WebUi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PageGroupController : Controller
    {
        private readonly Entities _db = new Entities();

        public ActionResult Index()
        {
            return View(PageGroupVM.ToIEnumerable(_db.PageGroups));
        }

        public JsonResult PageGroupTree(int? id)
        {
            var employees = from e in _db.PageGroups.OrderBy(d=>d.PageGroupOrder)
                            where (id.HasValue ? e.PageGroupIDTree == id : e.PageGroupIDTree == null)
                            select new
                            {
                                id = e.PageGroupID,
                                Name = e.PageGroupTitle,
                                hasChildren = e.PageGroups1.Any()
                            };
            return Json(employees, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            var pageGroupVM = new PageGroup();
            return View((PageGroupVM)pageGroupVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PageGroupVM pageGroupVM)
        {
            if (ModelState.IsValid)
            {
                var pageGroup = (PageGroup)pageGroupVM;
                _db.PageGroups.Add(pageGroup);
                _db.SaveChanges();
                HttpContext.Cache["EditDateTime"] = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                return RedirectToAction("Index");
            }
            return View(pageGroupVM);
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            PageGroup pagegroups = _db.PageGroups.Find(id);
            return View((PageGroupVM)pagegroups);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PageGroupVM pageGroupVM)
        {
            if (ModelState.IsValid)
            {
                var pagegroups = (PageGroup)pageGroupVM;
                _db.Entry(pagegroups).State = EntityState.Modified;
                _db.SaveChanges();
                HttpContext.Cache["EditDateTime"] = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                return RedirectToAction("Index");
            }
            return View(pageGroupVM);
        }

        public ActionResult Delete(int id = 0)
        {
            PageGroup pagegroups = _db.PageGroups.Find(id);
            if (pagegroups == null)
            {
                return HttpNotFound();
            }
            return View((PageGroupVM)pagegroups);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PageGroup pagegroups = _db.PageGroups.Find(id);
            _db.PageGroups.Remove(pagegroups);
            _db.SaveChanges();
            HttpContext.Cache["EditDateTime"] = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
            return RedirectToAction("Index");
        }
    }
}