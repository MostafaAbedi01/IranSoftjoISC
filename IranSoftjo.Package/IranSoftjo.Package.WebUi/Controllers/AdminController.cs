using System.Web.Mvc;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly Entities _db = new Entities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListPosPaymentConfirm()
        {
            return View();
        }

        public ActionResult RequestEvaluation()
        {
            return View(_db.RequestEvaluations);
        }
    }
}