using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IranSoftjo.Common;
using IranSoftjo.Core.Web.Mvc;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.ViewModels;

namespace IranSoftjo.Package.WebUi.Controllers
{
    public class AgencyController : Controller
    {
        private readonly Entities _db = new Entities();

        public ActionResult SaveFile(HttpPostedFileBase file)
        {
            Session["HttpPostedFileBase"] = file;
            return Content("");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(AgencyVM.ToIEnumerable(_db.Agencies));
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View((AgencyVM)_db.Agencies.FirstOrDefault(d => d.Id == id));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Delete(AgencyVM model)
        {
            Page pages = _db.Pages.Find(model.Id);
            _db.Pages.Remove(pages);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View((AgencyVM)_db.Agencies.FirstOrDefault(d => d.Id == id));
        }
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Edit(AgencyVM model)
        {
            if (ModelState.IsValid)
            {
                const string imageUploadPath = "/Uploads/Agency/";
                var agency = (Agency)model;
                var imageUrlUpload = (HttpPostedFileBase)Session["HttpPostedFileBase"];
                if (imageUrlUpload != null)
                {
                    string filename = Path.GetFileName(imageUrlUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);
                    string newFilenameUrl = imageUploadPath + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    imageUrlUpload.SaveAs(physicalFilename);
                    string thumbnailUrl = Utils.CreateThumbnail(physicalFilename, 200, 200, imageUploadPath);
                    Session["HttpPostedFileBase"] = null;
                    agency.ImageUrl = thumbnailUrl;
                }
                _db.Entry(agency).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult List(int? searchpage)
        {
            if (searchpage==null)
                searchpage = 1;
            Session["searchpage"] = searchpage;
            return View(AgencyVM.ToIEnumerable(_db.Agencies.Where(d => d.Active == true).OrderBy(d => d.Code)));
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(AgencyVM model)
        {
            if (ModelState.IsValid)
            {
                string imageUploadPath = "/Uploads/Agency/";
                var agency = (Agency)model;
                string userName = User.Identity.Name;
                User user = _db.Users.FirstOrDefault(d => d.Username == userName);
                var imageUrlUpload = (HttpPostedFileBase)Session["HttpPostedFileBase"];
                if (imageUrlUpload != null)
                {
                    string filename = Path.GetFileName(imageUrlUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);

                    string newFilenameUrl = imageUploadPath + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    imageUrlUpload.SaveAs(physicalFilename);
                    string thumbnailUrl = Utils.CreateThumbnail(physicalFilename, 200, 200, imageUploadPath);
                    Session["HttpPostedFileBase"] = null;
                    agency.ImageUrl = thumbnailUrl;
                }
                if (user != null)
                {
                    agency.UserId = user.UserID;
                    agency.Code = user.UserID.ToString();
                }
                _db.Entry(agency).State = EntityState.Added;
                _db.SaveChanges();
                TempData.SetMessage(
                    " درخواست نمایندگی شما با موفقیت ثبت شد،بخش نمایندگی های شرکت در اولین فرصت با شما تماس خواهند گرفت.",
                    MessageType.Success);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

     
    }
}