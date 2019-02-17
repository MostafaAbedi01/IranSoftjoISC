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
    [Authorize()]
    public class DashboardController : Controller
    {
        private readonly Entities _db = new Entities();

        public ActionResult Index1()
        {
            return View();
        }

      

     
    }
}