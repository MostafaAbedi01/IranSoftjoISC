using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.ViewModels;
using Newtonsoft.Json;

namespace IranSoftjo.Package.WebUi.Android.Controllers
{
    public class AwtTblShirkhatApiController : ApiController
    {
        private readonly Entities _db = new Entities();
        public TblCodeTagPmVm Get(string CodeTag)
        {
            var json = Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            Configuration.Formatters.Remove(Configuration.Formatters.XmlFormatter);
            var dd = _db.TblCodeTagPms.FirstOrDefault(d => d.CodeTag == CodeTag);
            if (dd != null)
            {
                return (TblCodeTagPmVm)dd;
            }
            return null;
        }

        public IEnumerable<TblShirkhatVm> Get(int count, bool? isshow)
        {
            var json = Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            Configuration.Formatters.Remove(Configuration.Formatters.XmlFormatter);

            if (count == 0)
            {
                return
                    TblShirkhatVm.ToIEnumerable(
                        _db.TblShirkhats.OrderByDescending(d => d.TblShirkhatId).AsEnumerable());
            }
            return
                TblShirkhatVm.ToIEnumerable(
                    _db.TblShirkhats.OrderByDescending(d => d.TblShirkhatId).Take(count).AsEnumerable());
        }


        public TblShirkhatVm Post(
            string codetag,
            string codepm,
            string Comment,
            int? TblUserId,
            string SizeDariceh,
            string SizeOtaghak,
            string Status,
            Int16? VaziatZaheri,
            Int16? OtaghakType,
            Int16? StatusMode,
            Int16? VaziatDaricheh,
            Int16? VaziatShir,
            Int16? VaziatPelak
            )
        {

            try
            {
                if (VaziatZaheri == 1)
                {
                    OtaghakType = null;
                    StatusMode = null;
                    VaziatDaricheh = null;
                    VaziatShir = null;
                    VaziatPelak = null;
                }
                int? tblCodeTagPmId = _db.TblCodeTagPms.FirstOrDefault(d => (d.CodeTag == codetag) || (d.CodePm == codepm))?.TblCodeTagPmId;
                if (tblCodeTagPmId == null)
                {
                    TblCodeTagPm tblCodeTagPm = new TblCodeTagPm
                    {
                        CodePm = codepm,
                        CodeTag = codetag,
                    };
                    _db.TblCodeTagPms.Add(tblCodeTagPm);
                    _db.SaveChanges();
                    tblCodeTagPmId = tblCodeTagPm.TblCodeTagPmId;
                }
                var tblShirkhat = _db.TblShirkhats.FirstOrDefault(d => d.TblCodeTagPmId == tblCodeTagPmId);
                if (tblShirkhat != null)
                {
                    tblShirkhat.Comment = Comment;
                    tblShirkhat.DateTimeSabt = DateTime.Now;
                    tblShirkhat.TblUserId = TblUserId;
                    tblShirkhat.TblCodeTagPmId = tblCodeTagPmId;
                    tblShirkhat.SizeDariceh = SizeDariceh;
                    tblShirkhat.SizeOtaghak = SizeOtaghak;
                    tblShirkhat.Status = Status;
                    tblShirkhat.VaziatZaheri = VaziatZaheri;
                    tblShirkhat.OtaghakType = OtaghakType;
                    tblShirkhat.StatusMode = StatusMode;
                    tblShirkhat.VaziatDaricheh = VaziatDaricheh;
                    tblShirkhat.VaziatShir = VaziatShir;
                    tblShirkhat.VaziatPelak = VaziatPelak;
                }
                else
                {
                    tblShirkhat = new TblShirkhat
                    {
                        Comment = Comment,
                        DateTimeSabt = DateTime.Now,
                        TblUserId = TblUserId,
                        TblCodeTagPmId = tblCodeTagPmId,
                        SizeDariceh = SizeDariceh,
                        SizeOtaghak = SizeOtaghak,
                        Status = Status,
                        VaziatZaheri = VaziatZaheri,
                        OtaghakType = OtaghakType,
                        StatusMode = StatusMode,
                        VaziatDaricheh = VaziatDaricheh,
                        VaziatShir = VaziatShir,
                        VaziatPelak = VaziatPelak
                    };
                    _db.TblShirkhats.Add(tblShirkhat);
                }
                _db.SaveChanges();
                return (TblShirkhatVm)tblShirkhat;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}