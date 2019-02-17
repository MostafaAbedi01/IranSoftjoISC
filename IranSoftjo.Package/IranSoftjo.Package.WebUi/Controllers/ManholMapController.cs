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
    public class ManholMapController : Controller
    {
        private readonly Entities _db = new Entities();


        //----------------------------------------
        public ActionResult Index()
        {
            return View(_db.TblManhols.ToList());
        }


        public ActionResult SearchMap(int? id, int? gisid)
        {
            List<TblManhol> lst = new List<TblManhol>();
            Session["gisid"] = gisid;
            if (id == 100)
            {
                // lst = _db.TblManhols.Take(100).ToList();
                Session["SM"] = "100";
            }
            else if (id == 0)
            {
                //var ddd = DateTime.Now.Date;
                // lst = _db.TblManhols.Where(d => d.DateSabt > ddd).ToList();
                Session["SM"] = "Today";
            }
            else if (id == -1)
            {
                //lst = _db.TblManhols.ToList();
                Session["SM"] = "All";
            }
            else
            {
                Session["SM"] = "Search";
                lst = _db.TblManhols.Take(15).ToList();
            }
            return View(lst);
        }


        public ActionResult SearchByManhoId(int id)
        {
            return View(id);
        }

        [HttpPost]
        public ActionResult Search(string location)
        {
            object result = null;
            if (Session["gisid"] != null)
            {
                int gis = (int)Session["gisid"];
                result = (_db.TblManhols.Where(d => d.CodeGis == gis).Select(d => new TblManholVm
                {
                    Longitude = d.Longitude,
                    Latitude = d.Latitude,
                    TblManholId = d.TblManholId,
                    CodePm = d.CodePm,
                    Address = d.Address,
                    CodeGis = d.CodeGis,
                    CodeMakani = d.CodeMakani,
                })).ToList();
            }
            else
            {
                if ((string)Session["SM"] != "Search")
                {
                    if ((string)Session["SM"] == "100")
                    {
                        //result = TblManholVm.ToIEnumerable(_db.TblManhols.Take(100)).ToList();
                        result = (_db.TblManhols.Take(1000).Select(d => new TblManholVm
                        {
                            Longitude = d.Longitude,
                            Latitude = d.Latitude,
                            TblManholId = d.TblManholId,
                            CodePm = d.CodePm,
                            Address = d.Address,
                            CodeGis = d.CodeGis,
                            CodeMakani = d.CodeMakani,
                        })).ToList();
                    }
                    else if ((string)Session["SM"] == "Today")
                    {
                        var ddd = DateTime.Now.Date;
                        result = (_db.TblManhols.Where(d => d.DateSabt > ddd).Select(d => new TblManholVm
                        {
                            Longitude = d.Longitude,
                            Latitude = d.Latitude,
                            TblManholId = d.TblManholId,
                            CodePm = d.CodePm,
                            Address = d.Address,
                            CodeGis = d.CodeGis,
                            CodeMakani = d.CodeMakani,
                        })).ToList();
                    }
                    else if ((string)Session["SM"] == "All")
                    {
                        result = (_db.TblManhols.Take(2000).Select(d => new TblManholVm
                        {
                            Longitude = d.Longitude,
                            Latitude = d.Latitude,
                            TblManholId = d.TblManholId,
                            CodePm = d.CodePm,
                            Address = d.Address,
                            CodeGis = d.CodeGis,
                            CodeMakani = d.CodeMakani,
                        })).ToList();
                    }
                }
                else
                {
                    List<TblManholVm> lst =
                              TblManholVm.ToIEnumerable(_db.TblManhols.ToList()).ToList();
                    if(location!=null)
                        result = lst.Where(x => x.CodePmStr.Contains(location)).ToList().Take(1000);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SearchById(int manhoId)
        {
            List<TblManholVm> lst = TblManholVm.ToIEnumerable(_db.TblManhols.Where(x => x.TblManholId == manhoId).ToList()).ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDataFromServerAction(TblManholVm manholVm)
        {
            manholVm.CodePm = 123456;
            manholVm.Latitude = 51.30;
            manholVm.Longitude = 35.70;
            /* do something with your usermodel object */
            return Json(manholVm, JsonRequestBehavior.AllowGet);
        }
    }
}
