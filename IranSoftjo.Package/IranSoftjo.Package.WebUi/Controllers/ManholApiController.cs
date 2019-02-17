using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.ViewModels;
using Newtonsoft.Json;

namespace IranSoftjo.Package.WebUi.Controllers
{
    public class ManholApiController : ApiController
    {
        private readonly Entities _db = new Entities();
        // GET api/mapgpsapi
        public IEnumerable<TblManholVm> Get(int count, bool? isshow)
        {
            var json = Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            Configuration.Formatters.Remove(Configuration.Formatters.XmlFormatter);
            if (count == 0)
            {
                return TblManholVm.ToIEnumerable(_db.TblManhols.OrderByDescending(d => d.TblManholId).AsEnumerable());
            }
            else
            {
                return TblManholVm.ToIEnumerable(_db.TblManhols.OrderByDescending(d => d.TblManholId).Take(count).AsEnumerable());
            }
        }

        public TblManholVm Get(int id, int isDelete)
        {
            var tblManhol = _db.TblManhols.FirstOrDefault(x => x.TblManholId == id);
            if (tblManhol != null)
            {
                var tblManholVm = (TblManholVm)tblManhol;
                _db.TblManhols.Remove(tblManhol);
                _db.SaveChanges();
                return tblManholVm;
            }
            return null;
        }

        // GET api/mapgpsapi/5
        public TblManhol Get(int id)
        {
            return _db.TblManhols.FirstOrDefault(x => x.TblManholId == id);
        }

        // POST api/mapgpsapi
        public TblManhol Post(
            string address,
            int? codenaheyeh,
            int? omgheManhol,
            Int16? vaziatZaheri1,
            Int16? vaziatZaheri2,
            Int16? vaziatZaheri3,
            float Longitude,
            float Latitude,
            Int16? jenseDaricheh,
            Int16? ghotrDariche,
            Int32 codeMakani,
            Int32 codeGis,
            string codeEshterak,
            Int16? BandKeshi,
            Int16? SamPashi,
            Int16? LiRobi,
            Int16? Mahicheh,
            Int16? Pashneh,
            Int16? Pelekan1,
            Int16? Pelekan2,
            Int32? TedadLoleVorodi,
            Int16? NoeManhol,
            string MehvarName,
            string Comment,
            Int32? GhotreLolehKhoroji,
            Int32? GhotreLolehVorodi,
            Int32? TblUserId,
            Int32? MapOld,
            Int32? MapNew,
            Int32? CodeTasfieKhane,
            Int32? MantagheShahrdari,
            Int32? MantagheGaz,
            Int32? MantagheBargh,
            Int32? MantagheMohakhaberat,
            Int32? MantagheRanandegi,
            Int32? CodeCompany,
            Int32? CodeSystem,
            Int32? GhotreManhol,
            Int16? JenseManhol
            )
        {
            try
            {
                if (vaziatZaheri1 == 1)
                {
                    vaziatZaheri2 = -1;
                    vaziatZaheri3 = -1;
                }
                var codePm = codeMakani.ToString() + codeGis;
                var intcodePm = Convert.ToInt32(codePm);
                var tblManhol = new TblManhol
                {
                    Address = address,
                    CodeNaheyeh = codenaheyeh,
                    OmgheManhol = omgheManhol,
                    VaziatZaheri1 = vaziatZaheri1,
                    VaziatZaheri2 = vaziatZaheri2,
                    VaziatZaheri3 = vaziatZaheri3,
                    Longitude = Longitude,
                    Latitude = Latitude,
                    DateNasb = DateTime.Now,
                    DateBahrebardari = DateTime.Now,
                    DateSakht = DateTime.Now,
                    DateSabt = DateTime.Now,
                    JenseDaricheh = jenseDaricheh,
                    GhotrDariche = ghotrDariche,
                    CodeMakani = codeMakani,
                    CodeGis = codeGis,
                    CodePm = intcodePm,
                    CodeEshterak = codeEshterak,
                    BandKeshi = BandKeshi,
                    SamPashi = SamPashi,
                    LiRobi = LiRobi,
                    Mahicheh = Mahicheh,
                    Pashneh = Pashneh,
                    Pelekan1 = Pelekan1,
                    Pelekan2 = Pelekan2,
                    TedadLoleVorodi = TedadLoleVorodi,
                    NoeManhol = NoeManhol,
                    MehvarName = MehvarName,
                    Comment = Comment,
                    GhotreLolehKhoroji = GhotreLolehKhoroji,
                    GhotreLolehVorodi = GhotreLolehVorodi,
                    TblUserId = TblUserId,
                    MapOld = MapOld,
                    MapNew = MapNew,
                    CodeTasfieKhane = CodeTasfieKhane,
                    MantagheShahrdari = MantagheShahrdari,
                    MantagheGaz = MantagheGaz,
                    MantagheBargh = MantagheBargh,
                    MantagheMohakhaberat = MantagheMohakhaberat,
                    MantagheRanandegi = MantagheRanandegi,
                    CodeCompany = CodeCompany,
                    CodeSystem = CodeSystem,
                    GhotreManhol = GhotreManhol,
                    JenseManhol = JenseManhol
                };
                _db.TblManhols.Add(tblManhol);
                _db.SaveChanges();
                return tblManhol;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public TblManhol Post(TblManhol model)
        {
            try
            {
                var tblManhol = _db.TblManhols.FirstOrDefault(x => x.TblManholId == model.TblManholId);
                if (tblManhol != null)
                {
                    tblManhol.TblManholId = model.TblManholId;
                    tblManhol.CodeTag = model.CodeTag;
                    tblManhol.CodePm = model.CodePm;
                    //tblManhol.Latitude = model.Latitude;
                    //tblManhol.Longitude = model.Longitude;
                    tblManhol.DateSakht = model.DateSakht;
                    tblManhol.DateBahrebardari = model.DateBahrebardari;
                    tblManhol.DateSabt = model.DateSabt;
                    tblManhol.DateNasb = model.DateNasb;
                    tblManhol.ImageUrl = model.ImageUrl;
                    tblManhol.ThumbnailImageUrl = model.ThumbnailImageUrl;
                    tblManhol.CodeNaheyeh = model.CodeNaheyeh;
                    tblManhol.Address = model.Address;
                    tblManhol.VaziatZaheri1 = model.VaziatZaheri1;
                    tblManhol.VaziatZaheri2 = model.VaziatZaheri2;
                    tblManhol.VaziatZaheri3 = model.VaziatZaheri3;
                    tblManhol.JenseDaricheh = model.JenseDaricheh;
                    tblManhol.GhotrDariche = model.GhotrDariche;
                    tblManhol.CodeMakani = model.CodeMakani;
                    tblManhol.CodeGis = model.CodeGis;
                    tblManhol.OmgheManhol = model.OmgheManhol;
                    tblManhol.GhotreManhol = model.GhotreManhol;
                    tblManhol.NoeManhol = model.NoeManhol;
                    tblManhol.JenseManhol = model.JenseManhol;
                    tblManhol.GhotreLolehVorodi = model.GhotreLolehVorodi;
                    tblManhol.GhotreLolehKhoroji = model.GhotreLolehKhoroji;
                }
                _db.SaveChanges();
                return tblManhol;
            }
            catch (Exception)
            {
                return null;
            }
        }

        //[HttpGet]
        //public void Delete(int id)
        //{
        //    var tblManhol = _db.TblManhols.FirstOrDefault(x => x.TblManholId == id);
        //    if (tblManhol != null)
        //    {
        //        _db.TblManhols.Remove(tblManhol);
        //    }
        //    _db.SaveChanges();
        //}


    }
}