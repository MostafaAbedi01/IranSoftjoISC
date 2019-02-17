using System.Web.Mvc;

namespace IranSoftjo.Package.WebUi.Controllers
{
    public class HelpController : Controller
    {
        public ActionResult Payments()
        {
            return View();
        }

        public ActionResult ZarinPal()
        {
            return View();
        }

        public ActionResult AgencyConditions()
        {
            return View();
        }
        public ActionResult PropagandaConditions()
        {
            return View();
        }
    }
}