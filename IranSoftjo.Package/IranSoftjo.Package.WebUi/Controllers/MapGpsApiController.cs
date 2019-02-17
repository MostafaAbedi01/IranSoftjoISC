using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.Controllers
{
    public class MapGpsApiController : ApiController
    {
        private readonly Entities _db = new Entities();
        // GET api/mapgpsapi
        public IEnumerable<TblManhol> Get()
        {
            return _db.TblManhols.AsEnumerable();
        }

        // GET api/mapgpsapi/5
        public TblManhol Get(int id)
        {
            return _db.TblManhols.FirstOrDefault(x => x.TblManholId == id);
        }

        // POST api/mapgpsapi
        public TblManhol Post(string codepm, string codetag, string address)
        {
            try
            {
                codetag = codetag.ToLower();
                var tblManholId = 1;
                var codepmint = Convert.ToInt32(codepm);
                TblManhol tblManhol = _db.TblManhols.FirstOrDefault(d => d.CodePm == codepmint);
                if (tblManhol != null)
                {
                    tblManholId = tblManhol.TblManholId;
                    var tblInspection = _db.TblInspections.FirstOrDefault(d => d.TblManholId == tblManholId && d.DateSabt == null);
                    if (tblInspection != null)
                    {
                        tblInspection.DateSabt = DateTime.Now;
                        _db.SaveChanges();
                        return null;
                    }
                    return null;
                }
                else
                {
                    tblManhol = _db.TblManhols.FirstOrDefault(d => d.CodeTag == codetag);
                    if (tblManhol != null)
                        tblManholId = tblManhol.TblManholId;
                    var tblInspection = _db.TblInspections.FirstOrDefault(d => d.TblManholId == tblManholId && d.DateSabt == null);
                    if (tblInspection != null)
                    {
                        tblInspection.DateSabt = DateTime.Now;
                        _db.SaveChanges();
                        return null;
                    }
                    return null;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }

        //public void Post(HttpPostedFileBase file)
        //{
        //    if (file.ContentLength > 0)
        //    {
        //        var pp = "c://";
        //        var fileName = Path.GetFileName(file.FileName);
        //        if (HttpContext.Current != null)
        //            pp = HttpContext.Current.Server.MapPath("~/uploads");
        //        var path = Path.Combine(pp, fileName);
        //        file.SaveAs(path);
        //    }
        //}

        // PUT api/mapgpsapi/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/mapgpsapi/5
        public void Delete(int id)
        {
        }
    }
}