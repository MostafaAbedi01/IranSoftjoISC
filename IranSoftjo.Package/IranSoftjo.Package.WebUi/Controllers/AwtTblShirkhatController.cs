using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using IranSoftjo.Common;
using IranSoftjo.Core.Web.Mvc;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.Configs;
using IranSoftjo.Package.WebUi.ViewModels;

namespace IranSoftjo.Package.WebUi.Controllers
{
    [Authorize]
    public class AwtTblShirkhatController : Controller
    {
        private Entities _db = new Entities();

        public ActionResult Create()
        {
            var pages = new TblShirkhat();
            //lstTblKeyValues = _db.TblKeyValues.ToList();
            //ViewBag.NoeManhol = new SelectList(lstTblKeyValues.Where(d => d.Type == 1), "KeyID", "Title");
            //ViewBag.JenseManhol = new SelectList(lstTblKeyValues.Where(d => d.Type == 2), "KeyID", "Title");
            //ViewBag.VaziatZaheri1 = new SelectList(lstTblKeyValues.Where(d => d.Type == 3), "KeyID", "Title");
            //ViewBag.JenseDaricheh = new SelectList(lstTblKeyValues.Where(d => d.Type == 4), "KeyID", "Title");
            //ViewBag.GhotrDariche = new SelectList(lstTblKeyValues.Where(d => d.Type == 5), "KeyID", "Title");
            //ViewBag.VaziatZaheri2 = new SelectList(lstTblKeyValues.Where(d => d.Type == 6), "KeyID", "Title");
            //ViewBag.VaziatZaheri3 = new SelectList(lstTblKeyValues.Where(d => d.Type == 7), "KeyID", "Title");

            //ViewBag.Pelekan1 = new SelectList(lstTblKeyValues.Where(d => d.Type == 8), "KeyID", "Title");
            //ViewBag.Pelekan2 = new SelectList(lstTblKeyValues.Where(d => d.Type == 9), "KeyID", "Title");
            //ViewBag.SamPashi = new SelectList(lstTblKeyValues.Where(d => d.Type == 10), "KeyID", "Title");
            //ViewBag.BandKeshi = new SelectList(lstTblKeyValues.Where(d => d.Type == 10), "KeyID", "Title");
            //ViewBag.LiRobi = new SelectList(lstTblKeyValues.Where(d => d.Type == 10), "KeyID", "Title");
            //ViewBag.Mahicheh = new SelectList(lstTblKeyValues.Where(d => d.Type == 8), "KeyID", "Title");
            //ViewBag.Pashneh = new SelectList(lstTblKeyValues.Where(d => d.Type == 8), "KeyID", "Title");

            return View((TblShirkhatVm)pages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TblShirkhatVm TblShirkhatVm)
        {
            if (ModelState.IsValid)
            {
                var tblManhol = (TblShirkhat)TblShirkhatVm;
                tblManhol.DateTimeSabt = DateTime.Now;
                _db.TblShirkhats.Add(tblManhol);
                _db.SaveChanges();
                return RedirectToAction("ManholList", "Manhol", new { id = tblManhol.TblShirkhatId });
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
            return View(TblShirkhatVm);
        }

        private List<TblKeyValue> lstTblKeyValues = new List<TblKeyValue>();
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            var pages = _db.TblShirkhats.Find(id);
            if (pages == null)
            {
                return HttpNotFound();
            }
            lstTblKeyValues = _db.TblKeyValues.ToList();
       
            ViewBag.VaziatZaheri = new SelectList(lstTblKeyValues.Where(d => d.Type == 1), "KeyID", "Title", pages.VaziatZaheri);
            ViewBag.OtaghakType = new SelectList(lstTblKeyValues.Where(d => d.Type == 2), "KeyID", "Title", pages.OtaghakType);
            ViewBag.VaziatShir = new SelectList(lstTblKeyValues.Where(d => d.Type == 4), "KeyID", "Title", pages.VaziatShir);
            ViewBag.VaziatPelak = new SelectList(lstTblKeyValues.Where(d => d.Type == 4), "KeyID", "Title", pages.VaziatPelak);
            ViewBag.VaziatDaricheh = new SelectList(lstTblKeyValues.Where(d => d.Type == 4), "KeyID", "Title", pages.VaziatDaricheh);
            ViewBag.StatusMode = new SelectList(lstTblKeyValues.Where(d => d.Type == 3), "KeyID", "Title", pages.StatusMode);
            return View((TblShirkhatVm)pages);
        }

        [HttpPost]
        public ActionResult Edit(TblShirkhatVm tblShirkhatVm)
        {
           
            if (ModelState.IsValid)
            {
                var tblManhol = (TblShirkhat)tblShirkhatVm;
                tblManhol.Latitude = float.Parse(tblShirkhatVm.LatitudeStr, CultureInfo.InvariantCulture.NumberFormat);
                tblManhol.Longitude = float.Parse(tblShirkhatVm.LongitudeStr, CultureInfo.InvariantCulture.NumberFormat);
                _db.Entry(tblManhol).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("List", "AwtTblShirkhat", new { id = tblManhol.TblShirkhatId });
            }
            lstTblKeyValues = _db.TblKeyValues.ToList();

            ViewBag.VaziatZaheri = new SelectList(lstTblKeyValues.Where(d => d.Type == 1), "KeyID", "Title", tblShirkhatVm.VaziatZaheri);
            ViewBag.OtaghakType = new SelectList(lstTblKeyValues.Where(d => d.Type == 2), "KeyID", "Title", tblShirkhatVm.OtaghakType);
            ViewBag.VaziatShir = new SelectList(lstTblKeyValues.Where(d => d.Type == 4), "KeyID", "Title", tblShirkhatVm.VaziatShir);
            ViewBag.VaziatPelak = new SelectList(lstTblKeyValues.Where(d => d.Type == 4), "KeyID", "Title", tblShirkhatVm.VaziatPelak);
            ViewBag.VaziatDaricheh = new SelectList(lstTblKeyValues.Where(d => d.Type == 4), "KeyID", "Title", tblShirkhatVm.VaziatDaricheh);
            ViewBag.StatusMode = new SelectList(lstTblKeyValues.Where(d => d.Type == 3), "KeyID", "Title", tblShirkhatVm.StatusMode);
            return View(tblShirkhatVm);
        }

        public ActionResult Delete(int id = 0)
        {
            TblShirkhat pages = _db.TblShirkhats.Find(id);
            if (pages == null)
            {
                return HttpNotFound();
            }
            return View((TblShirkhatVm)pages);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var tblManhol = _db.TblShirkhats.Find(id);
          
            _db.TblShirkhats.Remove(tblManhol);
            _db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            ViewBag.VaziatZaheri = _db.TblKeyValues.Where(d => d.Type == 1);
            ViewBag.OtaghakType = _db.TblKeyValues.Where(d => d.Type == 2);
            ViewBag.StatusMode = _db.TblKeyValues.Where(d => d.Type == 3);
            ViewBag.Vaziat = _db.TblKeyValues.Where(d => d.Type == 4);
            ViewBag.TblUserId = _db.Users;
            return View(TblShirkhatVm.ToIEnumerable(_db.TblShirkhats.ToList().OrderByDescending(d => d.DateTimeSabt)));
        }


        public ActionResult Details(int id = 0)
        {
            var tblManhol = _db.TblShirkhats.FirstOrDefault(d => d.TblShirkhatId == id);
            if (tblManhol == null)
            {
                return HttpNotFound();
            }
            return View((TblShirkhatVm)tblManhol);
        }

        public ActionResult SearchMap(int? id, int? gisid)
        {
            List<TblShirkhat> lst = new List<TblShirkhat>();
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
                lst = _db.TblShirkhats.Take(15).ToList();
            }
            return View(lst);
        }

        public ActionResult SearchByIdOnMap(int id)
        {
            return View(id);
        }

        [HttpPost]
        public ActionResult Search(string location)
        {
            object result = null;
            if (Session["gisid"] != null)
            {
                string gis = (string)Session["gisid"];
                result = (_db.TblShirkhats.Where(d => d.TblCodeTagPm.CodePm == gis).Select(d => new TblShirkhatVm
                {
                    Longitude = d.Longitude,
                    Latitude = d.Latitude,
                    TblShirkhatId = d.TblShirkhatId,
                    Address = d.Address,
                })).ToList();
            }
            else
            {
                if ((string)Session["SM"] != "Search")
                {
                    if ((string)Session["SM"] == "100")
                    {
                        //result = TblShirkhatVm.ToIEnumerable(_db.TblManhols.Take(100)).ToList();
                        result = (_db.TblShirkhats.Take(1000).Select(d => new TblShirkhatVm
                        {
                            Longitude = d.Longitude,
                            Latitude = d.Latitude,
                            TblShirkhatId = d.TblShirkhatId,
                            Address = d.Address,
                        })).ToList();
                    }
                    else if ((string)Session["SM"] == "Today")
                    {
                        var ddd = DateTime.Now.Date;
                        result = (_db.TblShirkhats.Where(d => d.DateTimeSabt > ddd).Select(d => new TblShirkhatVm
                        {
                            Longitude = d.Longitude,
                            Latitude = d.Latitude,
                            TblShirkhatId = d.TblShirkhatId,
                            Address = d.Address,
                        })).ToList();
                    }
                    else if ((string)Session["SM"] == "All")
                    {
                        result = (_db.TblShirkhats.Take(2000).Select(d => new TblShirkhatVm
                        {
                            Longitude = d.Longitude,
                            Latitude = d.Latitude,
                            TblShirkhatId = d.TblShirkhatId,
                            Address = d.Address,
                        })).ToList();
                    }
                }
                else
                {
                    List<TblShirkhatVm> lst =
                              TblShirkhatVm.ToIEnumerable(_db.TblShirkhats.Where(x => x.TblCodeTagPm.CodePm.Contains(location)).ToList()).ToList();
                    result = lst.ToList().Take(1000);
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SearchById(int manhoId)
        {
            List<TblShirkhatVm> lst = TblShirkhatVm.ToIEnumerable(_db.TblShirkhats.Where(x => x.TblShirkhatId == manhoId).ToList()).ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
    }
}
