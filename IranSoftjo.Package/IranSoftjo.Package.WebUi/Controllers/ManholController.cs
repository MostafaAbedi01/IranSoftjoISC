using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IranSoftjo.Common;
using IranSoftjo.Core.Web.Mvc;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.Configs;
using IranSoftjo.Package.WebUi.ViewModels;
using NPOI.HSSF.UserModel;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Collections;

namespace IranSoftjo.Package.WebUi.Android.Controllers
{
    [Authorize]
    public class ManholController : Controller
    {
        private readonly Entities _db = new Entities();


        public FileResult Export([DataSourceRequest]DataSourceRequest request)
        {
            //Get the data representing the current grid state - page, sort and filter
            IEnumerable products = _db.TblManhols.ToDataSourceResult(request).Data;

            //Create new Excel workbook
            var workbook = new HSSFWorkbook();

            //Create new Excel sheet
            var sheet = workbook.CreateSheet();

            //(Optional) set the width of the columns
            sheet.SetColumnWidth(0, 70 * 256);
            sheet.SetColumnWidth(1, 20 * 256);
            sheet.SetColumnWidth(2, 20 * 256);
            sheet.SetColumnWidth(3, 20 * 256);
            sheet.SetColumnWidth(4, 20 * 256);
            sheet.SetColumnWidth(5, 30 * 256);
            sheet.SetColumnWidth(6, 20 * 256);
            sheet.SetColumnWidth(7, 20 * 256);
            sheet.SetColumnWidth(8, 20 * 256);
            sheet.SetColumnWidth(13, 30 * 256);
            //Create a header row
            var headerRow = sheet.CreateRow(0);

            //Set the column names in the header row
            headerRow.CreateCell(0).SetCellValue("آدرس");
            headerRow.CreateCell(1).SetCellValue("کد پی ام");
            headerRow.CreateCell(2).SetCellValue("کد چی ای اس");
            headerRow.CreateCell(3).SetCellValue("کد مکانی");
            headerRow.CreateCell(4).SetCellValue("مرئی بودن");

            headerRow.CreateCell(5).SetCellValue("تاریخ ثبت");
            headerRow.CreateCell(6).SetCellValue("جنس منهول");
            headerRow.CreateCell(7).SetCellValue("قطر منهول");
            headerRow.CreateCell(8).SetCellValue("نوع منهول");
            headerRow.CreateCell(9).SetCellValue("وضعیت ظاهری");
            headerRow.CreateCell(10).SetCellValue("وضعیت ظاهری");
            headerRow.CreateCell(11).SetCellValue("جنس دریچه");
            headerRow.CreateCell(12).SetCellValue("قطر دریچه");
            headerRow.CreateCell(13).SetCellValue("توضیحات");
            headerRow.CreateCell(14).SetCellValue("کد تگ");
            headerRow.CreateCell(15).SetCellValue("سریال سیستمی");

            //(Optional) freeze the header row so it is not scrolled
            sheet.CreateFreezePane(0, 1, 0, 1);

            int rowNumber = 1;

            //Populate the sheet with values from the grid data
            foreach (TblManhol product in products)
            {
                //Create a new row
                var row = sheet.CreateRow(rowNumber++);

                lstTblKeyValues = _db.TblKeyValues.ToList();
                //Set values for the cells
                row.CreateCell(0).SetCellValue(product.Address);
                row.CreateCell(1).SetCellValue(product.CodePm?.ToString());
                row.CreateCell(2).SetCellValue(product.CodeGis?.ToString());
                row.CreateCell(3).SetCellValue(product.CodeMakani?.ToString());
                row.CreateCell(4).SetCellValue(lstTblKeyValues.Where(d => d.Type == 3).FirstOrDefault(d => d.KeyID == product.VaziatZaheri1)?.Title);
                row.CreateCell(5).SetCellValue(product.DateSabt?.ToString());
                row.CreateCell(6).SetCellValue(lstTblKeyValues.Where(d => d.Type == 2).FirstOrDefault(d => d.KeyID == product.JenseManhol)?.Title);
                row.CreateCell(7).SetCellValue(product.GhotreManhol?.ToString());
                row.CreateCell(8).SetCellValue(lstTblKeyValues.Where(d => d.Type == 1).FirstOrDefault(d => d.KeyID == product.NoeManhol)?.Title);
                row.CreateCell(9).SetCellValue(lstTblKeyValues.Where(d => d.Type == 6).FirstOrDefault(d => d.KeyID == product.VaziatZaheri2)?.Title);
                row.CreateCell(10).SetCellValue(lstTblKeyValues.Where(d => d.Type == 7).FirstOrDefault(d => d.KeyID == product.VaziatZaheri3)?.Title);
                row.CreateCell(11).SetCellValue(lstTblKeyValues.Where(d => d.Type == 4).FirstOrDefault(d => d.KeyID == product.JenseDaricheh)?.Title);
                row.CreateCell(12).SetCellValue(lstTblKeyValues.Where(d => d.Type == 5).FirstOrDefault(d => d.KeyID == product.GhotrDariche)?.Title);
                row.CreateCell(13).SetCellValue(product.Comment?.ToString());
                row.CreateCell(14).SetCellValue(product.CodeTag?.ToString());
                row.CreateCell(15).SetCellValue(product.TblManholId.ToString());

            }

            //Write the workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            //Return the result to the end user

            return File(output.ToArray(),   //The binary data of the XLS file
                "application/vnd.ms-excel", //MIME type of Excel files
                "GridExcelExport.xls");     //Suggested file name in the "Save as" dialog which will be displayed to the end user

        }

        [HttpPost]
        public void UploadImage(HttpPostedFileBase file)
        {

            //var json = Configuration.Formatters.JsonFormatter;
            //json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            //Configuration.Formatters.Remove(Configuration.Formatters.XmlFormatter);

            // if (file.ContentLength > 0)
            {
                const string imageUploadPath = "/Uploads/";

                //string filename = Path.GetFileName(file.FileName);
                //string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                //                     + Path.GetExtension(filename);
                //var newFilenameUrl = imageUploadPath + newFilename;
                //var fileName = Path.GetFileName(file.FileName);
                //var uploadFolderPath = Server.MapPath("~/Uploads/" + fileName);

                //var path = Path.Combine(uploadFolderPath);


                string filename = Path.GetFileName(file.FileName);
                string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                     + Path.GetExtension(filename);
                string newFilenameUrl = imageUploadPath + newFilename;
                string physicalFilename = Server.MapPath(newFilenameUrl);
                file.SaveAs(physicalFilename);
                Utils.CreateThumbnail(physicalFilename, PackageSettings.Active.PageThumbnailImageUrlWidth, PackageSettings.Active.PageThumbnailImageUrlHeight, imageUploadPath);



                //  file.SaveAs(fileName);
            }
        }

        public ActionResult SaveFile(HttpPostedFileBase file)
        {
            Session["HttpPostedFileBase"] = file;
            return Content("");
        }

        public ActionResult ManholCreate()
        {
            var pages = new TblManhol();
            lstTblKeyValues = _db.TblKeyValues.ToList();
            ViewBag.NoeManhol = new SelectList(lstTblKeyValues.Where(d => d.Type == 1), "KeyID", "Title");
            ViewBag.JenseManhol = new SelectList(lstTblKeyValues.Where(d => d.Type == 2), "KeyID", "Title");
            ViewBag.VaziatZaheri1 = new SelectList(lstTblKeyValues.Where(d => d.Type == 3), "KeyID", "Title");
            ViewBag.JenseDaricheh = new SelectList(lstTblKeyValues.Where(d => d.Type == 4), "KeyID", "Title");
            ViewBag.GhotrDariche = new SelectList(lstTblKeyValues.Where(d => d.Type == 5), "KeyID", "Title");
            ViewBag.VaziatZaheri2 = new SelectList(lstTblKeyValues.Where(d => d.Type == 6), "KeyID", "Title");
            ViewBag.VaziatZaheri3 = new SelectList(lstTblKeyValues.Where(d => d.Type == 7), "KeyID", "Title");

            ViewBag.Pelekan1 = new SelectList(lstTblKeyValues.Where(d => d.Type == 8), "KeyID", "Title");
            ViewBag.Pelekan2 = new SelectList(lstTblKeyValues.Where(d => d.Type == 9), "KeyID", "Title");
            ViewBag.SamPashi = new SelectList(lstTblKeyValues.Where(d => d.Type == 10), "KeyID", "Title");
            ViewBag.BandKeshi = new SelectList(lstTblKeyValues.Where(d => d.Type == 10), "KeyID", "Title");
            ViewBag.LiRobi = new SelectList(lstTblKeyValues.Where(d => d.Type == 10), "KeyID", "Title");
            ViewBag.Mahicheh = new SelectList(lstTblKeyValues.Where(d => d.Type == 8), "KeyID", "Title");
            ViewBag.Pashneh = new SelectList(lstTblKeyValues.Where(d => d.Type == 8), "KeyID", "Title");

            return View((TblManholVm)pages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManholCreate(TblManholVm tblManholVm)
        {
            if (ModelState.IsValid)
            {
                var tblManhol = (TblManhol)tblManholVm;
                tblManhol.Latitude = float.Parse(tblManholVm.LatitudeStr, CultureInfo.InvariantCulture.NumberFormat);
                tblManhol.Longitude = float.Parse(tblManholVm.LongitudeStr, CultureInfo.InvariantCulture.NumberFormat);
                const string imageUploadPath = "/Uploads/Pages/PageThumbnailImage/";
                var productImageUrlUpload = (HttpPostedFileBase)Session["HttpPostedFileBase"];
                if (productImageUrlUpload != null)
                {
                    string filename = Path.GetFileName(productImageUrlUpload.FileName);
                    string newFilename = Guid.NewGuid().ToString().Replace("-", string.Empty)
                                         + Path.GetExtension(filename);
                    var newFilenameUrl = imageUploadPath + newFilename;
                    string physicalFilename = Server.MapPath(newFilenameUrl);
                    productImageUrlUpload.SaveAs(physicalFilename);
                    var thumbnailUrl = Utils.CreateThumbnail(physicalFilename, PackageSettings.Active.PageThumbnailImageUrlWidth, PackageSettings.Active.PageThumbnailImageUrlHeight, imageUploadPath);
                    tblManhol.ImageUrl = newFilenameUrl;
                    tblManhol.ThumbnailImageUrl = thumbnailUrl;
                    Session["HttpPostedFileBase"] = null;
                }
                tblManhol.DateSabt = DateTime.Now;
                _db.TblManhols.Add(tblManhol);
                _db.SaveChanges();
                return RedirectToAction("ManholList", "Manhol", new { id = tblManhol.TblManholId });
            }
            lstTblKeyValues = _db.TblKeyValues.ToList();
            ViewBag.NoeManhol = new SelectList(lstTblKeyValues.Where(d => d.Type == 1), "KeyID", "Title");
            ViewBag.JenseManhol = new SelectList(lstTblKeyValues.Where(d => d.Type == 2), "KeyID", "Title");
            ViewBag.VaziatZaheri1 = new SelectList(lstTblKeyValues.Where(d => d.Type == 3), "KeyID", "Title");
            ViewBag.JenseDaricheh = new SelectList(lstTblKeyValues.Where(d => d.Type == 4), "KeyID", "Title");
            ViewBag.GhotrDariche = new SelectList(lstTblKeyValues.Where(d => d.Type == 5), "KeyID", "Title");
            ViewBag.VaziatZaheri2 = new SelectList(lstTblKeyValues.Where(d => d.Type == 6), "KeyID", "Title");
            ViewBag.VaziatZaheri3 = new SelectList(lstTblKeyValues.Where(d => d.Type == 7), "KeyID", "Title");

            ViewBag.Pelekan1 = new SelectList(lstTblKeyValues.Where(d => d.Type == 8), "KeyID", "Title");
            ViewBag.Pelekan2 = new SelectList(lstTblKeyValues.Where(d => d.Type == 9), "KeyID", "Title");
            ViewBag.SamPashi = new SelectList(lstTblKeyValues.Where(d => d.Type == 10), "KeyID", "Title");
            ViewBag.BandKeshi = new SelectList(lstTblKeyValues.Where(d => d.Type == 10), "KeyID", "Title");
            ViewBag.LiRobi = new SelectList(lstTblKeyValues.Where(d => d.Type == 10), "KeyID", "Title");
            ViewBag.Mahicheh = new SelectList(lstTblKeyValues.Where(d => d.Type == 8), "KeyID", "Title");
            ViewBag.Pashneh = new SelectList(lstTblKeyValues.Where(d => d.Type == 8), "KeyID", "Title");
            return View(tblManholVm);
        }

        private List<TblKeyValue> lstTblKeyValues = new List<TblKeyValue>();
        [HttpGet]
        public ActionResult ManholEdit(int id = 0)
        {
            var pages = _db.TblManhols.Find(id);
            if (pages == null)
            {
                return HttpNotFound();
            }
            lstTblKeyValues = _db.TblKeyValues.ToList();
            ViewBag.NoeManhol = new SelectList(lstTblKeyValues.Where(d => d.Type == 1), "KeyID", "Title", pages.NoeManhol);
            ViewBag.JenseManhol = new SelectList(lstTblKeyValues.Where(d => d.Type == 2), "KeyID", "Title", pages.JenseManhol);
            ViewBag.VaziatZaheri1 = new SelectList(lstTblKeyValues.Where(d => d.Type == 3), "KeyID", "Title", pages.VaziatZaheri1);
            ViewBag.JenseDaricheh = new SelectList(lstTblKeyValues.Where(d => d.Type == 4), "KeyID", "Title", pages.JenseDaricheh);
            ViewBag.GhotrDariche = new SelectList(lstTblKeyValues.Where(d => d.Type == 5), "KeyID", "Title", pages.GhotrDariche);
            ViewBag.VaziatZaheri2 = new SelectList(lstTblKeyValues.Where(d => d.Type == 6), "KeyID", "Title", pages.VaziatZaheri2);
            ViewBag.VaziatZaheri3 = new SelectList(lstTblKeyValues.Where(d => d.Type == 7), "KeyID", "Title", pages.VaziatZaheri3);

            ViewBag.Pelekan1 = new SelectList(lstTblKeyValues.Where(d => d.Type == 8), "KeyID", "Title", pages.Pelekan1);
            ViewBag.Pelekan2 = new SelectList(lstTblKeyValues.Where(d => d.Type == 9), "KeyID", "Title", pages.Pelekan2);
            ViewBag.SamPashi = new SelectList(lstTblKeyValues.Where(d => d.Type == 10), "KeyID", "Title", pages.SamPashi);
            ViewBag.BandKeshi = new SelectList(lstTblKeyValues.Where(d => d.Type == 10), "KeyID", "Title", pages.BandKeshi);
            ViewBag.LiRobi = new SelectList(lstTblKeyValues.Where(d => d.Type == 10), "KeyID", "Title", pages.LiRobi);
            ViewBag.Mahicheh = new SelectList(lstTblKeyValues.Where(d => d.Type == 8), "KeyID", "Title", pages.Mahicheh);
            ViewBag.Pashneh = new SelectList(lstTblKeyValues.Where(d => d.Type == 8), "KeyID", "Title", pages.Pashneh);
            return View((TblManholVm)pages);
        }


        [HttpPost]
        public ActionResult ManholEdit(TblManholVm tblManholVm)
        {
            if (ModelState.IsValid)
            {
                var tblManhol = (TblManhol)tblManholVm;
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
                    if (System.IO.File.Exists(Server.MapPath("~/" + tblManhol.ThumbnailImageUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + tblManhol.ThumbnailImageUrl));
                    }
                    if (System.IO.File.Exists(Server.MapPath("~/" + tblManhol.ImageUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + tblManhol.ImageUrl));
                    }
                    tblManhol.ImageUrl = newFilenameUrl;
                    tblManhol.ThumbnailImageUrl = thumbnailUrl;
                    Session["HttpPostedFileBase"] = null;
                }
                //tblManhol.DateSabt = DateTime.Now;
                _db.Entry(tblManhol).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("ManholList", "Manhol", new { id = tblManhol.TblManholId });
            }
            lstTblKeyValues = _db.TblKeyValues.ToList();
            ViewBag.NoeManhol = new SelectList(lstTblKeyValues.Where(d => d.Type == 1), "KeyID", "Title", tblManholVm.NoeManhol);
            ViewBag.JenseManhol = new SelectList(lstTblKeyValues.Where(d => d.Type == 2), "KeyID", "Title", tblManholVm.JenseManhol);
            ViewBag.VaziatZaheri1 = new SelectList(lstTblKeyValues.Where(d => d.Type == 3), "KeyID", "Title", tblManholVm.VaziatZaheri1);
            ViewBag.JenseDaricheh = new SelectList(lstTblKeyValues.Where(d => d.Type == 4), "KeyID", "Title", tblManholVm.JenseDaricheh);
            ViewBag.GhotrDariche = new SelectList(lstTblKeyValues.Where(d => d.Type == 5), "KeyID", "Title", tblManholVm.GhotrDariche);
            ViewBag.VaziatZaheri2 = new SelectList(lstTblKeyValues.Where(d => d.Type == 6), "KeyID", "Title", tblManholVm.VaziatZaheri2);
            ViewBag.VaziatZaheri3 = new SelectList(lstTblKeyValues.Where(d => d.Type == 7), "KeyID", "Title", tblManholVm.VaziatZaheri3);

            ViewBag.Pelekan1 = new SelectList(lstTblKeyValues.Where(d => d.Type == 8), "KeyID", "Title", tblManholVm.Pelekan1);
            ViewBag.Pelekan2 = new SelectList(lstTblKeyValues.Where(d => d.Type == 9), "KeyID", "Title", tblManholVm.Pelekan2);
            ViewBag.SamPashi = new SelectList(lstTblKeyValues.Where(d => d.Type == 10), "KeyID", "Title", tblManholVm.SamPashi);
            ViewBag.BandKeshi = new SelectList(lstTblKeyValues.Where(d => d.Type == 10), "KeyID", "Title", tblManholVm.BandKeshi);
            ViewBag.LiRobi = new SelectList(lstTblKeyValues.Where(d => d.Type == 10), "KeyID", "Title", tblManholVm.LiRobi);
            ViewBag.Mahicheh = new SelectList(lstTblKeyValues.Where(d => d.Type == 8), "KeyID", "Title", tblManholVm.Mahicheh);
            ViewBag.Pashneh = new SelectList(lstTblKeyValues.Where(d => d.Type == 8), "KeyID", "Title", tblManholVm.Pashneh);
            return View(tblManholVm);
        }

        public ActionResult ManholDelete(int id = 0)
        {
            TblManhol pages = _db.TblManhols.Find(id);
            if (pages == null)
            {
                return HttpNotFound();
            }
            return View((TblManholVm)pages);
        }

        [HttpPost, ActionName("ManholDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var tblManhol = _db.TblManhols.Find(id);
            if (System.IO.File.Exists(Server.MapPath("~/" + tblManhol.ImageUrl)))
            {
                System.IO.File.Delete(Server.MapPath("~/" + tblManhol.ImageUrl));
            }
            if (System.IO.File.Exists(Server.MapPath("~/" + tblManhol.ThumbnailImageUrl)))
            {
                System.IO.File.Delete(Server.MapPath("~/" + tblManhol.ThumbnailImageUrl));
            }
            _db.TblManhols.Remove(tblManhol);
            _db.SaveChanges();
            return RedirectToAction("ManholList");
        }

        public ActionResult ManholDetails(int id = 0)
        {
            var tblManhol = _db.TblManhols.FirstOrDefault(d => d.TblManholId == id);
            if (tblManhol == null)
            {
                return HttpNotFound();
            }
            return View((TblManholVm)tblManhol);
        }

        public ActionResult ManholList()
        {
            ViewBag.NoeManhol = _db.TblKeyValues.Where(d => d.Type == 1);
            ViewBag.JenseManhol = _db.TblKeyValues.Where(d => d.Type == 2);
            ViewBag.VaziatZaheri1 = _db.TblKeyValues.Where(d => d.Type == 3);
            ViewBag.JenseDaricheh = _db.TblKeyValues.Where(d => d.Type == 4);
            ViewBag.GhotrDariche = _db.TblKeyValues.Where(d => d.Type == 5);
            ViewBag.VaziatZaheri2 = _db.TblKeyValues.Where(d => d.Type == 6);
            ViewBag.VaziatZaheri3 = _db.TblKeyValues.Where(d => d.Type == 7);

            return View(TblManholVm.ToIEnumerable(_db.TblManhols.OrderByDescending(d => d.TblManholId).ToList()));
        }

    }
}
