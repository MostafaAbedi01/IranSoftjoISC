using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using IranSoftjo.Common;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.Configs;
using IranSoftjo.Package.WebUi.ViewModels;

namespace IranSoftjo.Package.WebUi.Controllers
{
    public class PageController : Controller
    {
        private readonly Entities _db = new Entities();

        [Authorize(Roles = "Administrator")]
        public ActionResult SaveFile(HttpPostedFileBase file)
        {
            Session["HttpPostedFileBase"] = file;
            return Content("");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var pages = _db.Pages.Include(p => p.PageGroup).OrderBy(d => d.PageOrder).Select(d => new PageVM
            {
                PageID = d.PageID,
                PageGroupID = d.PageGroupID,
                PageTitle = d.PageTitle,
                PageDate = d.PageDate,
                PageThumbnailImageUrl = d.PageThumbnailImageUrl,
                PageOrder = d.PageOrder,
                PageGroup = d.PageGroup,
            }).AsEnumerable();
            IEnumerable<PageVM> pagesVM = PageVM.ToIEnumerable(pages);
            return View(pagesVM);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            var pages = new Page();
            ViewBag.PageGroupID = new SelectList(_db.PageGroups, "PageGroupID", "PageGroupTitle");
            pages.HasComment = true;
            pages.ActiveCommentAdmin = true;
            pages.ActivePage = true;
            pages.ActivePageDate = true;
            pages.ActivePrint = true;
            pages.ActiveVisit = true;
            pages.PageOrder = 0;
            return View((PageVM)pages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(PageVM pagesVM)
        {
            if (ModelState.IsValid)
            {
                var pages = (Page)pagesVM;
                string imageUploadPath = "/Uploads/Pages/PageThumbnailImage/";
                var ProductImageUrlUpload = (HttpPostedFileBase)Session["HttpPostedFileBase"];
                string newFilenameUrl = string.Empty;
                string thumbnailUrl = string.Empty;
                if (ProductImageUrlUpload != null)
                {
                    string filename = Path.GetFileName(ProductImageUrlUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);
                    newFilenameUrl = imageUploadPath + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    ProductImageUrlUpload.SaveAs(physicalFilename);
                    thumbnailUrl = Utils.CreateThumbnail(physicalFilename, PackageSettings.Active.PageThumbnailImageUrlWidth, PackageSettings.Active.PageThumbnailImageUrlHeight, imageUploadPath);
                    pages.PageImageUrl = newFilenameUrl;
                    pages.PageThumbnailImageUrl = thumbnailUrl;
                    Session["HttpPostedFileBase"] = null;
                }
                pages.PageText = Server.HtmlDecode(pages.PageText);
                _db.Pages.Add(pages);
                _db.SaveChanges();
                HttpContext.Cache["EditDateTime"] = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                HttpContext.Cache.Remove("KendoWindow");
                return RedirectToAction("PageText", "Home", new { id = pages.PageID });
            }
            pagesVM.PageText = Server.HtmlDecode(pagesVM.PageText);
            ViewBag.PageGroupID = new SelectList(_db.PageGroups, "PageGroupID", "PageGroupTitle");
            return View(pagesVM);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id = 0)
        {
            Page pages = _db.Pages.Find(id);
            if (pages == null)
            {
                return HttpNotFound();
            }
            ViewBag.PageGroupID = new SelectList(_db.PageGroups, "PageGroupID", "PageGroupTitle", pages.PageGroupID);
            return View((PageVM)pages);
        }


        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(PageVM pagesVM)
        {
            //ModelState.Remove("PageDate");
            if (ModelState.IsValid)
            {
                var pages = (Page)pagesVM;
                string imageUploadPath = "/Uploads/Pages/PageThumbnailImage/";
                var ProductImageUrlUpload = (HttpPostedFileBase)Session["HttpPostedFileBase"];
                string newFilenameUrl = string.Empty;
                string thumbnailUrl = string.Empty;
                if (ProductImageUrlUpload != null)
                {
                    string filename = Path.GetFileName(ProductImageUrlUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);
                    newFilenameUrl = imageUploadPath + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    ProductImageUrlUpload.SaveAs(physicalFilename);
                    thumbnailUrl = Utils.CreateThumbnail(physicalFilename, PackageSettings.Active.PageThumbnailImageUrlWidth, PackageSettings.Active.PageThumbnailImageUrlHeight, imageUploadPath);
                    if (System.IO.File.Exists(Server.MapPath("~/" + pages.PageThumbnailImageUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + pages.PageThumbnailImageUrl));
                    }
                    if (System.IO.File.Exists(Server.MapPath("~/" + pages.PageImageUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + pages.PageImageUrl));
                    }
                    pages.PageImageUrl = newFilenameUrl;
                    pages.PageThumbnailImageUrl = thumbnailUrl;
                    Session["HttpPostedFileBase"] = null;
                }
                pages.PageText = Server.HtmlDecode(pages.PageText);

                _db.Entry(pages).State = EntityState.Modified;
                HttpContext.Cache.Remove("KendoWindow");
                HttpContext.Cache["EditDateTime"] = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                _db.SaveChanges();
                return RedirectToAction("PageText", "Home", new { id = pages.PageID });
            }
            pagesVM.PageText = Server.HtmlDecode(pagesVM.PageText);
            ViewBag.PageGroupID = new SelectList(_db.PageGroups, "PageGroupID", "PageGroupTitle");
            return View(pagesVM);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id = 0)
        {
            Page pages = _db.Pages.Find(id);
            if (pages == null)
            {
                return HttpNotFound();
            }
            return View((PageVM)pages);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Page pages = _db.Pages.Find(id);
            if (System.IO.File.Exists(Server.MapPath("~/" + pages.PageImageUrl)))
            {
                System.IO.File.Delete(Server.MapPath("~/" + pages.PageImageUrl));
            }
            if (System.IO.File.Exists(Server.MapPath("~/" + pages.PageThumbnailImageUrl)))
            {
                System.IO.File.Delete(Server.MapPath("~/" + pages.PageThumbnailImageUrl));
            }
            _db.Pages.Remove(pages);
            HttpContext.Cache["EditDateTime"] = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

        //public ActionResult Show(int id = 0)
        //{
        //    Page pages = _db.Pages.FirstOrDefault(m => m.PageID == id);
        //    pages.Visit++;
        //    _db.SaveChanges();
        //    return View((PageVM)pages);
        //}
    }
}