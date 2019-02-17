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
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace IranSoftjo.Package.WebUi.Controllers
{
    public class CommentController : Controller
    {
        private readonly Entities _db = new Entities();

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var products = _db.Comments;
            return View(CommentVM.ToIEnumerable(products.ToList().OrderByDescending(d => d.CommentID)));
        }

        public ActionResult Active(int id = 0)
        {
            var comment = _db.Comments.Find(id);
            _db.Entry(comment).State = EntityState.Modified;
            comment.IsActive = true;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult List(int id = 0)
        {
            return View(id);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id = 0)
        {
            Comment products = _db.Comments.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View((CommentVM)products);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(CommentVM productVM)
        {
            if (ModelState.IsValid)
            {
                var product = (Comment)productVM;
                product.CommentText = Server.HtmlDecode(product.CommentText);
                _db.Entry(product).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productVM);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id = 0)
        {
            Comment products = _db.Comments.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View((CommentVM)products);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment products = _db.Comments.Find(id);
            _db.Comments.Remove(products);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}