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
         [Authorize(Roles = "Administrator")]
    public class ProductImageController : Controller
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
            return View(ProductImageVM.ToIEnumerable(_db.ProductImages.Where(d => d.ProductID == id).OrderByDescending(d => d.ProductImageID).ToList()));
        }

        [AllowAnonymous]
        public ActionResult List(int? id)
        {
            return View(_db.ProductImages.Where(d => d.ProductID == id).ToList());
        }

        public ActionResult Create(int id)
        {
            var objVM = new ProductImageVM() { ProductID = id };
            return View(objVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductImageVM objVM)
        {
            if (ModelState.IsValid)
            {
                var productImage = (ProductImage)objVM;
                const string imageUploadPath = "/Uploads/Products/ProductImage/";
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
                    productImage.ProductImageUrl = newFilenameUrl;
                    productImage.ProductThumbnailImageUrl = thumbnailUrl;
                    Session["HttpPostedFileBase"] = null;
                }
                _db.ProductImages.Add(productImage);
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = objVM.ProductID });
            }
            return View(objVM);
        }

        public ActionResult Edit(int id = 0)
        {
            ProductImage productgroups = _db.ProductImages.Find(id);
            if (productgroups == null)
            {
                return HttpNotFound();
            }
            return View((ProductImageVM)productgroups);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductImageVM productImageVM)
        {
            if (ModelState.IsValid)
            {
                const string imageUploadPath = "/Uploads/Products/ProductImage/";
                var productImage = (ProductImage)productImageVM;
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
                    if (System.IO.File.Exists(Server.MapPath("~/" + productImage.ProductImageUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + productImage.ProductImageUrl));
                    }
                    if (System.IO.File.Exists(Server.MapPath("~/" + productImage.ProductThumbnailImageUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + productImage.ProductThumbnailImageUrl));
                    }
                    productImage.ProductImageUrl = newFilenameUrl;
                    productImage.ProductThumbnailImageUrl = thumbnailUrl;
                    Session["HttpPostedFileBase"] = null;
                }
                _db.Entry(productImage).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = productImageVM.ProductID });
            }
            return View(productImageVM);
        }

        public ActionResult Delete(int id = 0)
        {
            ProductImage productImage = _db.ProductImages.Find(id);
            if (productImage == null)
            {
                return HttpNotFound();
            }
            return View((ProductImageVM)productImage);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductImage productImage = _db.ProductImages.Find(id);
            if (System.IO.File.Exists(Server.MapPath("~/" + productImage.ProductImageUrl)))
            {
                System.IO.File.Delete(Server.MapPath("~/" + productImage.ProductImageUrl));
            }
            if (System.IO.File.Exists(Server.MapPath("~/" + productImage.ProductThumbnailImageUrl)))
            {
                System.IO.File.Delete(Server.MapPath("~/" + productImage.ProductThumbnailImageUrl));
            }
            _db.ProductImages.Remove(productImage);
            _db.SaveChanges();
            return RedirectToAction("Index", new { id = productImage.ProductID });
        }
    }
}