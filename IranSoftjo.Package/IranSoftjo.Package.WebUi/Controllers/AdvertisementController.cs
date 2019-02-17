using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Web;
using System.Web.Mvc;
using IranSoftjo.Common;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.ViewModels;

namespace IranSoftjo.Package.WebUi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdvertisementController : Controller
    {
        private readonly Entities _db = new Entities();

        public ActionResult SaveFile(HttpPostedFileBase file)
        {
            Session["HttpPostedFileBase"] = file;
            return Content("");
        }

        public ActionResult Index()
        {
            return View(AdvertisementVM.ToIEnumerable(_db.Advertisements.Include(p => p.AdvertisementGroup)));
        }

        public ActionResult Create()
        {
            ViewBag.AdvertisementGroupID = new SelectList(_db.AdvertisementGroups, "AdvertisementGroupID", "AdvertisementGroupTitle");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdvertisementVM advertisementVM)
        {
            if (ModelState.IsValid)
            {
                var pages = (Advertisement)advertisementVM;
                string imageUploadPath = "/Uploads/Advertisements/";
                var ProductImageUrlUpload = (HttpPostedFileBase)Session["HttpPostedFileBase"];
                string newFilenameUrl = string.Empty;
               // string thumbnailUrl = string.Empty;
                if (ProductImageUrlUpload != null)
                {
                    string filename = Path.GetFileName(ProductImageUrlUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);
                    newFilenameUrl = imageUploadPath + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    ProductImageUrlUpload.SaveAs(physicalFilename);
                    //thumbnailUrl = Utils.CreateThumbnail(physicalFilename, 100, 100, imageUploadPath);
                    pages.AdvertisementImage = newFilenameUrl;
                    //pages.PageThumbnailImageUrl = thumbnailUrl;
                    Session["HttpPostedFileBase"] = null;
                }
                pages.AdvertisementCreateDate = DateTime.Now;
                _db.Advertisements.Add(pages);
                _db.SaveChanges();
                HttpContext.Cache["EditDateTime"] = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                return RedirectToAction("Index");
            }
            ViewBag.AdvertisementGroupID = new SelectList(_db.AdvertisementGroups, "AdvertisementGroupID", "AdvertisementGroupTitle");
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            Advertisement pages = _db.Advertisements.Find(id);
            ViewBag.AdvertisementGroupID = new SelectList(_db.AdvertisementGroups, "AdvertisementGroupID", "AdvertisementGroupTitle", pages.AdvertisementGroupID);
            return View((AdvertisementVM)pages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdvertisementVM advertisementVM)
        {
            if (ModelState.IsValid)
            {
                var pages = (Advertisement)advertisementVM;
                string imageUploadPath = "/Uploads/Advertisements/";
                var ProductImageUrlUpload = (HttpPostedFileBase)Session["HttpPostedFileBase"];
                string newFilenameUrl = string.Empty;
                if (ProductImageUrlUpload != null)
                {
                    string filename = Path.GetFileName(ProductImageUrlUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);
                    newFilenameUrl = imageUploadPath + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    ProductImageUrlUpload.SaveAs(physicalFilename);
                    if (System.IO.File.Exists(Server.MapPath("~/" + pages.AdvertisementImage)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + pages.AdvertisementImage));
                    }
                    pages.AdvertisementImage = newFilenameUrl;
                    Session["HttpPostedFileBase"] = null;
                }
                _db.Entry(pages).State = EntityState.Modified;
                _db.SaveChanges();
                HttpContext.Cache["EditDateTime"] = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                return RedirectToAction("Index");
            }
            ViewBag.AdvertisementGroupID = new SelectList(_db.AdvertisementGroups, "AdvertisementGroupID", "AdvertisementGroupTitle", advertisementVM.AdvertisementGroupID);
            return View();
        }

        public ActionResult Delete(int id = 0)
        {
            Advertisement pages = _db.Advertisements.Find(id);
            if (pages == null)
            {
                return HttpNotFound();
            }
            return View((AdvertisementVM)pages);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Advertisement pages = _db.Advertisements.Find(id);
            if (System.IO.File.Exists(Server.MapPath("~/" + pages.AdvertisementImage)))
            {
                System.IO.File.Delete(Server.MapPath("~/" + pages.AdvertisementImage));
            }
            _db.Advertisements.Remove(pages);
            _db.SaveChanges();
            HttpContext.Cache["EditDateTime"] = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
            return RedirectToAction("Index");
        }
    }
}