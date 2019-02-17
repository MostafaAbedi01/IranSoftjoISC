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

namespace IranSoftjo.Package.WebUi.Android.Controllers
{
    [Authorize]
    public class FinanceController : Controller
    {
        private readonly Entities _db = new Entities();

        [HttpGet]
        public ActionResult StatementBase()
        {
            var tblStatements = _db.TblStatements.FirstOrDefault(d => d.Type == 1);
            return View((TblStatementVm)tblStatements);
        }

        [HttpPost]
        public ActionResult StatementBase(TblStatementVm model)
        {
            var tblStatements = (TblStatement)model;
            _db.Entry(tblStatements).State = EntityState.Modified;
            _db.SaveChanges();
            return View();
        }

        public ActionResult StatementCalc()
        {
            Session["Price"] = _db.TblStatements.FirstOrDefault(d => d.Type == 1).BasicPrice;
            Session["Count"] = _db.TblManhols.Count(d => d.DateSabt < DateTime.Now);
            Session["PriceCount"] = (int)Session["Count"] * (int)Session["Price"];
            return View();
        }

    }
}
