using System.Data;
using System.Web.Mvc;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.ViewModels;

namespace IranSoftjo.Package.WebUi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductTypeController : Controller
    {
        private readonly Entities _db = new Entities();

        public ActionResult Index()
        {
            return View(ProductTypeVM.ToIEnumerable(_db.ProductTypes));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductTypeVM productGroupVM)
        {
            if (ModelState.IsValid)
            {
                var productGroup = (ProductType)productGroupVM;
                _db.ProductTypes.Add(productGroup);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productGroupVM);
        }

        public ActionResult Edit(int id = 0)
        {
            ProductType productgroups = _db.ProductTypes.Find(id);
            if (productgroups == null)
            {
                return HttpNotFound();
            }
            return View((ProductTypeVM)productgroups);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductType productgroups)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(productgroups).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View((ProductTypeVM)productgroups);
        }

        public ActionResult Delete(int id = 0)
        {
            ProductType productgroups = _db.ProductTypes.Find(id);
            if (productgroups == null)
            {
                return HttpNotFound();
            }
            return View((ProductTypeVM)productgroups);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductType productgroups = _db.ProductTypes.Find(id);

            _db.ProductTypes.Remove(productgroups);

            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}