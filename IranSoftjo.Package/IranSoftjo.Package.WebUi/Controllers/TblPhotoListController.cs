using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eShopMvc.Models;
using IranSoftjo.Common;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.ViewModels;

namespace IranSoftjo.Package.WebUi.Controllers
{
        [Authorize]
    public class TblPhotoListController : Controller
    {
        private readonly Entities _db = new Entities();

        [Authorize(Roles = "Administrator")]
        public ActionResult SaveFile(HttpPostedFileBase file)
        {
            Session["HttpPostedFileBase"] = file;
            return Content("");
        }

        public ActionResult Index(int id)
        {
            return View(TblPhotoListVM.ToIEnumerable(_db.TblPhotoLists.Where(d => d.ItemId == id).OrderByDescending(d => d.TblPhotoListId).ToList()));
        }

        [AllowAnonymous]
        public ActionResult List(int? id)
        {
            return View(_db.TblPhotoLists.Where(d => d.ItemId == id).ToList());
        }
                  [Authorize(Roles = "Administrator")]
        public ActionResult Create(int id)
        {
            var objVM = new TblPhotoListVM() { ItemId = id };
            return View(objVM);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TblPhotoListVM objVM)
        {
            if (ModelState.IsValid)
            {
                var productImage = (TblPhotoList)objVM;
                const string imageUploadPath = "/Uploads/TblPhotoList/";
                var productImageUrlUpload = (HttpPostedFileBase)Session["HttpPostedFileBase"];
                if (productImageUrlUpload != null)
                {
                    string filename = Path.GetFileName(productImageUrlUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = imageUploadPath + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    productImageUrlUpload.SaveAs(physicalFilename);
                    string thumbnailUrl = Utils.CreateThumbnail(physicalFilename, 150, 150, imageUploadPath);
                    productImage.ImageUrl = newFilenameUrl;
                    productImage.ThumbnailImageUrl = thumbnailUrl;
                    Session["HttpPostedFileBase"] = null;
                }
                _db.TblPhotoLists.Add(productImage);
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = objVM.ItemId });
            }
            return View(objVM);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id = 0)
        {
            TblPhotoList productgroups = _db.TblPhotoLists.Find(id);
            if (productgroups == null)
            {
                return HttpNotFound();
            }
            return View((TblPhotoListVM)productgroups);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TblPhotoListVM TblPhotoListVM)
        {
            if (ModelState.IsValid)
            {
                const string imageUploadPath = "/Uploads/TblPhotoList/";
                var productImage = (TblPhotoList)TblPhotoListVM;
                var productImageUrlUpload = (HttpPostedFileBase)Session["HttpPostedFileBase"];
                if (productImageUrlUpload != null)
                {
                    string filename = Path.GetFileName(productImageUrlUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);
                    string newFilenameUrl = imageUploadPath + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    productImageUrlUpload.SaveAs(physicalFilename);
                    string thumbnailUrl = Utils.CreateThumbnail(physicalFilename, 150, 150, imageUploadPath);
                    if (System.IO.File.Exists(Server.MapPath("~/" + productImage.ImageUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + productImage.ImageUrl));
                    }
                    if (System.IO.File.Exists(Server.MapPath("~/" + productImage.ThumbnailImageUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + productImage.ThumbnailImageUrl));
                    }
                    productImage.ImageUrl = newFilenameUrl;
                    productImage.ThumbnailImageUrl = thumbnailUrl;
                    Session["HttpPostedFileBase"] = null;
                }
                _db.Entry(productImage).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = TblPhotoListVM.ItemId });
            }
            return View(TblPhotoListVM);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id = 0)
        {
            TblPhotoList productImage = _db.TblPhotoLists.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View((TblPhotoListVM)productImage);
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblPhotoList productImage = _db.TblPhotoLists.Find(id);
            if (System.IO.File.Exists(Server.MapPath("~/" + productImage.ImageUrl)))
            {
                System.IO.File.Delete(Server.MapPath("~/" + productImage.ImageUrl));
            }
            if (System.IO.File.Exists(Server.MapPath("~/" + productImage.ThumbnailImageUrl)))
            {
                System.IO.File.Delete(Server.MapPath("~/" + productImage.ThumbnailImageUrl));
            }
            _db.TblPhotoLists.Remove(productImage);
            _db.SaveChanges();
            return RedirectToAction("Index", new { id = productImage.ItemId });
        }
    }
}