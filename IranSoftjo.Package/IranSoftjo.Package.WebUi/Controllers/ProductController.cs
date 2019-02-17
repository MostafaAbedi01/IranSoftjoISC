using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using IranSoftjo.Common;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.Configs;
using IranSoftjo.Package.WebUi.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace IranSoftjo.Package.WebUi.Controllers
{
    public class ProductController : Controller
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
            var products = _db.Products.Include("ProductGroup");
            return View(ProductVM.ToIEnumerable(products.ToList().OrderByDescending(d => d.ProductOrder)));
        }

        public ActionResult Products_Read([DataSourceRequest] DataSourceRequest request)
        {
            DataSourceResult result = GetProducts(0).ToDataSourceResult(request);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private List<Product> lst;
        private List<ProductVM> GetProducts(int? id)
        {
            if (id == null)
            {
                return
                    ProductVM.ToIEnumerable(_db.Products.Where(d => d.IsActive).OrderByDescending(d => d.ProductOrder))
                        .ToList();
            }
            else
            {
                lst = new List<Product>();

                GetValue(id);

                var lstProductVm = ProductVM.ToIEnumerable(lst).ToList();
                return lstProductVm;

            }
        }

        private void GetValue(int? id)
        {
            var p = _db.Products.Where(d => d.IsActive)
                .Where(d => d.ProductGroupID == id)
                .OrderByDescending(d => d.ProductOrder).ToList();
            lst.AddRange(p);
          
                foreach (var item in _db.ProductGroups.Where(d => d.ProductGroupIDTree == id).OrderByDescending(d => d.ProductGroupOrder))
                {
                    GetValue(item.ProductGroupID);
                }
        }


        [AllowAnonymous]
        //[OutputCache(Duration = 3000000, VaryByCustom = "EditDateTime", VaryByHeader = "user-agent", VaryByParam = "id",
        //    Location = OutputCacheLocation.Server)]
        public ActionResult List(int? id)
        {
            Session["CountProduct"] = 16;
            Session["GetJustChildNode"] = GetJustChildNode();
            return View(GetProducts(id).ToList().Where(d => d.IsActive).ToList());
        }

        [AllowAnonymous]
        public ActionResult Similar(int? id)
        {
            Session["CountProduct"] = 16;
            return View(GetProducts(id));
        }

        [AllowAnonymous]
        //[OutputCache(Duration = 180000, VaryByCustom = "EditDateTime", VaryByHeader = "user-agent", VaryByParam = "id",
        //    Location = OutputCacheLocation.Server)]
        public ActionResult Details(int id = 0)
        {
            Product products = _db.Products.Include("ProductGroup").FirstOrDefault(d => d.ProductID == id);
            if (products == null)
            {
                return HttpNotFound();
            }
            products.Visit++;
            _db.SaveChanges();
            return View((ProductVM)products);
        }

        private readonly List<ProductGroup> _lstJustChildNode = new List<ProductGroup>();
        private IEnumerable<ProductGroup> GetJustChildNode()
        {
            foreach (var item in _db.ProductGroups.OrderBy(d => d.ProductGroupIDTree))
            {
                if (item.ProductGroupIDTree != null)
                {
                    var itemDelete = _db.ProductGroups.FirstOrDefault(d => d.ProductGroupID == item.ProductGroupIDTree);
                    _lstJustChildNode.Remove(itemDelete);
                    item.ProductGroupTitle = itemDelete.ProductGroupTitle + " >> " + item.ProductGroupTitle;
                    _lstJustChildNode.Add(item);
                }
                else
                {
                    _lstJustChildNode.Add(item);
                }
            }
            return _lstJustChildNode;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.ProductTypeID = new SelectList(_db.ProductTypes, "ProductTypeID", "ProductTypeTitle");
            ViewBag.ProductGroupID = new SelectList(GetJustChildNode(), "ProductGroupID", "ProductGroupTitle");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var product = (Product)productVM;
                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductTypeID = new SelectList(_db.ProductTypes, "ProductTypeID", "ProductTypeTitle");
            ViewBag.ProductGroupID = new SelectList(GetJustChildNode(), "ProductGroupID", "ProductGroupTitle", productVM.ProductGroupID);
            return View(productVM);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id = 0)
        {
            Product products = _db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductGroupID = new SelectList(GetJustChildNode(), "ProductGroupID", "ProductGroupTitle", products.ProductGroupID);
            ViewBag.ProductTypeID = new SelectList(_db.ProductTypes, "ProductTypeID", "ProductTypeTitle", products.ProductTypeID);
            return View((ProductVM)products);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                string imageUploadPath = "/Uploads/Products/";
                var product = (Product)productVM;
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

                    thumbnailUrl = Utils.CreateThumbnail(physicalFilename, PackageSettings.Active.ProductThumbnailImageUrlWidth, PackageSettings.Active.ProductThumbnailImageUrlHeight, imageUploadPath);

                    if (System.IO.File.Exists(Server.MapPath("~/" + product.ProductImageUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + product.ProductImageUrl));
                    }
                    if (System.IO.File.Exists(Server.MapPath("~/" + product.ProductThumbnailImageUrl)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + product.ProductThumbnailImageUrl));
                    }
                    product.ProductImageUrl = newFilenameUrl;
                    product.ProductThumbnailImageUrl = thumbnailUrl;
                    Session["HttpPostedFileBase"] = null;
                }
                product.ProductDescription = Server.HtmlDecode(product.ProductDescription);
                _db.Entry(product).State = EntityState.Modified;
                _db.SaveChanges();
                HttpContext.Cache["EditDateTime"] = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                return RedirectToAction("Index");
            }
            ViewBag.ProductGroupID = new SelectList(GetJustChildNode(), "ProductGroupID", "ProductGroupTitle", productVM.ProductGroupID);
            ViewBag.ProductTypeID = new SelectList(_db.ProductTypes, "ProductTypeID", "ProductTypeTitle", productVM.ProductTypeID);
            return View(productVM);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id = 0)
        {
            Product products = _db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View((ProductVM)products);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product products = _db.Products.Find(id);
            _db.Products.Remove(products);
            if (System.IO.File.Exists(Server.MapPath("~/" + products.ProductImageUrl)))
            {
                System.IO.File.Delete(Server.MapPath("~/" + products.ProductImageUrl));
            }
            if (System.IO.File.Exists(Server.MapPath("~/" + products.ProductThumbnailImageUrl)))
            {
                System.IO.File.Delete(Server.MapPath("~/" + products.ProductThumbnailImageUrl));
            }
            try
            {
                _db.SaveChanges();
                HttpContext.Cache["EditDateTime"] = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View((ProductVM)products);
            }
        }
    }
}