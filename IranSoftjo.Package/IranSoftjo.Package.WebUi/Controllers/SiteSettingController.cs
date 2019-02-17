using System;
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
    [Authorize(Roles = "Administrator")]
    public class SiteSettingController : Controller
    {
        private readonly Entities _db = new Entities();

        public ActionResult SaveFileIconImageUrl(HttpPostedFileBase fileIconImageUrl)
        {
            Session["HttpPostedFileBaseIconImageUrl"] = fileIconImageUrl;
            return Content("");
        }

        public ActionResult SaveFileLogoImageUrl(HttpPostedFileBase fileLogoImageUrl)
        {
            Session["HttpPostedFileBaseLogoImageUrl"] = fileLogoImageUrl;
            return Content("");
        }

        public ActionResult Edit()
        {
            SiteSetting siteSetting = _db.SiteSettings.FirstOrDefault();
            return View((SiteSettingVM)siteSetting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SiteSettingVM siteSettingVM)
        {
            if (ModelState.IsValid)
            {
                var siteSetting = (SiteSetting)siteSettingVM;
                const string imageUploadPath = "/Uploads/SiteSetting/";
                var productImageUrlUploadIconImageUrl = (HttpPostedFileBase)Session["HttpPostedFileBaseIconImageUrl"];
                var productImageUrlUploadLogoImageUrl = (HttpPostedFileBase)Session["HttpPostedFileBaseLogoImageUrl"];
                if (productImageUrlUploadIconImageUrl != null)
                {
                    string filename = Path.GetFileName(productImageUrlUploadIconImageUrl.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);
                    string newFilenameUrl = imageUploadPath + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    productImageUrlUploadIconImageUrl.SaveAs(physicalFilename);
                    if (System.IO.File.Exists(Server.MapPath("~/" + siteSetting.IconImageUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + siteSetting.IconImageUrl));
                    }
                    siteSetting.IconImageUrl = newFilenameUrl;
                    Session["HttpPostedFileBaseIconImageUrl"] = null;
                }

                if (productImageUrlUploadLogoImageUrl != null)
                {
                    string filename = Path.GetFileName(productImageUrlUploadLogoImageUrl.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);
                    string newFilenameUrl = imageUploadPath + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    productImageUrlUploadLogoImageUrl.SaveAs(physicalFilename);
                    if (System.IO.File.Exists(Server.MapPath("~/" + siteSetting.LogoImageUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + siteSetting.LogoImageUrl));
                    }
                    siteSetting.LogoImageUrl = newFilenameUrl;
                    Session["HttpPostedFileBaseLogoImageUrl"] = null;
                }
                siteSetting.HtmlTemplateMail = Server.HtmlDecode(siteSettingVM.HtmlTemplateMail);
                _db.Entry(siteSetting).State = EntityState.Modified;

                try
                {
                    _db.SaveChanges();
                    HttpContext.Cache["SiteSetting"] = _db.SiteSettings.FirstOrDefault(); ;
                    HttpContext.Cache["EditDateTime"] = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
                return RedirectToAction("Index", "Home");
            }
            return View(siteSettingVM);
        }

    }
}