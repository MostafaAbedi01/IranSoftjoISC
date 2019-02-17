using System.Data;
using System.Linq;
using System.Web.Mvc;
using eShopMvc.Models;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.ViewModels;

namespace IranSoftjo.Package.WebUi.Controllers
{
    public class GalleryGroupController : Controller
    {
        private readonly Entities _db = new Entities();

    [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(GalleryGroupVM.ToIEnumerable(_db.GalleryGroups.ToList().OrderByDescending(d => d.GalleryGroupOrder)));
        }

    [Authorize(Roles = "Administrator")]

    public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(GalleryGroupVM galleryGroupVM)
        {
            if (ModelState.IsValid)
            {
                var productGroup = (GalleryGroup)galleryGroupVM;
                _db.GalleryGroups.Add(productGroup);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(galleryGroupVM);
        }

    [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id = 0)
        {
            GalleryGroup productgroups = _db.GalleryGroups.Find(id);
            if (productgroups == null)
            {
                return HttpNotFound();
            }
            return View((GalleryGroupVM)productgroups);
        }

        [HttpPost]
    [Authorize(Roles = "Administrator")]
    [ValidateAntiForgeryToken]
        public ActionResult Edit(GalleryGroupVM productgroupVM)
        {
            if (ModelState.IsValid)
            {
                var productgroups = (GalleryGroup) productgroupVM;
                _db.Entry(productgroups).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productgroupVM);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id = 0)
        {
            GalleryGroup productgroups = _db.GalleryGroups.Find(id);
            if (productgroups == null)
            {
                return HttpNotFound();
            }
            return View((GalleryGroupVM)productgroups);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GalleryGroup productgroups = _db.GalleryGroups.Find(id);
            _db.GalleryGroups.Remove(productgroups);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult List()
        {
            return View(GalleryGroupVM.ToIEnumerable(_db.GalleryGroups.Include("Galleries")));
        }

        public ActionResult SliderGallery()
        {
            return View(GalleryGroupVM.ToIEnumerable(_db.GalleryGroups.Include("Galleries")));
        }
    }
}