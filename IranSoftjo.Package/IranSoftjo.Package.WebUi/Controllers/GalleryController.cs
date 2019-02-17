using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IranSoftjo.Common;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.ViewModels;

namespace IranSoftjo.Package.WebUi.Controllers
{
    public class GalleryController : Controller
    {
        private readonly Entities _db = new Entities();

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            IEnumerable<GalleryVM> galleryVM = GalleryVM.ToIEnumerable(_db.Galleries.Include("GalleryGroup"));
            return View(galleryVM);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.GalleryGroupId = new SelectList(_db.GalleryGroups, "GalleryGroupId", "GalleryGroupTitle");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(GalleryVM galleriesVM)
        {
            if (ModelState.IsValid)
            {
                const string imageUploadPath = "/Uploads/Gallery/";
                var file = Session["HttpPostedFileBase"] as HttpPostedFileBase;
                if (file != null)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);
                    string newFilenameUrl = imageUploadPath + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    file.SaveAs(physicalFilename);
                    string thumbnailUrl = Utils.CreateThumbnail(physicalFilename, imageUploadPath: imageUploadPath);
                    galleriesVM.GalleryImageUrl = newFilenameUrl;
                    galleriesVM.GalleryImageUrlThumb = thumbnailUrl;
                    Session["HttpPostedFileBase"] = null;

                }
                _db.Galleries.Add((Gallery)galleriesVM);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GalleryGroupId = new SelectList(_db.GalleryGroups, "GalleryGroupId", "GalleryGroupTitle");
            return View(galleriesVM);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id = 0)
        {
            Gallery gallery = _db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            ViewBag.GalleryGroupId = new SelectList(_db.GalleryGroups, "GalleryGroupId", "GalleryGroupTitle");
            return View((GalleryVM)gallery);
        }


        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(GalleryVM galleriesVM)
        {
            if (ModelState.IsValid)
            {
                var file = Session["HttpPostedFileBase"] as HttpPostedFileBase;
                if (file != null)
                {
                    const string imageUploadPath = "/Uploads/Gallery/";
                    string filename = Path.GetFileName(file.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);
                    string newFilenameUrl = imageUploadPath + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    file.SaveAs(physicalFilename);
                    string thumbnailUrl = Utils.CreateThumbnail(physicalFilename, imageUploadPath: imageUploadPath);
                    if (System.IO.File.Exists(Server.MapPath("~/" + galleriesVM.GalleryImageUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + galleriesVM.GalleryImageUrl));
                    }
                    if (System.IO.File.Exists(Server.MapPath("~/" + galleriesVM.GalleryImageUrlThumb)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + galleriesVM.GalleryImageUrlThumb));
                    }
                    galleriesVM.GalleryImageUrl = newFilenameUrl;
                    galleriesVM.GalleryImageUrlThumb = thumbnailUrl;
                    Session["HttpPostedFileBase"] = null;
                }
                var pages = (Gallery)galleriesVM;
                _db.Entry(pages).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GalleryGroupId = new SelectList(_db.GalleryGroups, "GalleryGroupId", "GalleryGroupTitle");
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id = 0)
        {
            Gallery gallery = _db.Galleries.Find(id);
            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View((GalleryVM)gallery);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(GalleryVM galleriesVM)
        {
            Gallery gallery = _db.Galleries.Find(galleriesVM.GalleryId);
            if (System.IO.File.Exists(Server.MapPath("~/" + galleriesVM.GalleryImageUrl)))
            {
                System.IO.File.Delete(Server.MapPath("~/" + galleriesVM.GalleryImageUrl));
            }
            if (System.IO.File.Exists(Server.MapPath("~/" + galleriesVM.GalleryImageUrlThumb)))
            {
                System.IO.File.Delete(Server.MapPath("~/" + galleriesVM.GalleryImageUrlThumb));
            }
            _db.Galleries.Remove(gallery);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult SaveFile(HttpPostedFileBase file)
        {
            Session["HttpPostedFileBase"] = file;
            return Content("");
        }

        public ActionResult PageGalleryTn3(int id = 0)
        {
            Session["PageGroupTitle"] = _db.Galleries.FirstOrDefault(d => d.GalleryGroupsId == id).Title;
            var page = _db.Galleries.Where(d => d.GalleryGroupsId == id);
            return View(GalleryVM.ToIEnumerable(page).ToList());
        }

        public ActionResult PageGalleryLightbox(int id = 0)
        {
            Session["PageGroupTitle"] = _db.Galleries.FirstOrDefault(d => d.GalleryGroupsId == id).Title;
            var page = _db.Galleries.Where(d => d.GalleryGroupsId == id);
            return View(GalleryVM.ToIEnumerable(page).ToList());
        }
    }
}