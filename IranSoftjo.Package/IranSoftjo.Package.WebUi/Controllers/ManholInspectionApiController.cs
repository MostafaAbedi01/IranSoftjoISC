using System;
using System.Linq;
using System.Web.Http;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.ViewModels;

namespace IranSoftjo.Package.WebUi.Android.Controllers
{
    public class ManholInspectionApiController : ApiController
    {
        private readonly Entities _db = new Entities();
        public TblInspectionVm Get(string codeTag)
        {
            var tblManhol = _db.TblInspections.OrderByDescending(d => d.TblInspectionId).FirstOrDefault(x => x.TblManhol.CodeTag == codeTag);
            if (tblManhol != null)
            {
                var tblManholVm = (TblInspectionVm)tblManhol;
                return tblManholVm;
            }
            return null;
        }


        //public System.Collections.Generic.IEnumerable<TblInspectionVm> GetAll()
        //{
        //    var tblManhol = _db.TblInspections;
        //    return TblInspectionVm.ToIEnumerable(tblManhol);
        //}

        public TblInspectionVm Post(
            int? TblUserId,
            int? codepm,
            string codetag,
            Int16? NeedHamsatSazi,
            Int16? NeedBazsazi,
            Int16? NeedShostosho,
            Int16? NeedSampashi,
            Int16? StatusPashne,
            Int16? StatusMahiche,
            int? JenseManhole,
            int? TypeManhole,
            int? VaziatManhole
            )
        {
            //            var tblInspection = _db.TblInspections.FirstOrDefault(d => (d.TblManhol.CodePm == codepm || d.TblManhol.CodeTag == codetag) && d.DateSabt == null);
            //            if (tblInspection != null)
            //            {
            //                if (codepm!=null && codepm>0)
            //                {
            //                    tblInspection.TblManhol.CodePm = codepm;
            //                }
            //                if (!string.IsNullOrEmpty(codetag))
            //                {
            //                    tblInspection.TblManhol.CodeTag = codetag;
            //                }

            ////                tblInspection.TblManhol.JenseManhol = (short?) JenseManhole;
            ////                tblInspection.TblManhol.NoeManhol = (short?)TypeManhole;
            ////                tblInspection.TblManhol.VaziatZaheri1 = (short?)VaziatManhole;

            //                tblInspection.DateSabt = DateTime.Now;
            //                tblInspection.TblUserId = TblUserId;
            //                tblInspection.NeedHamsatSazi =  (Int16)NeedHamsatSazi;
            //                tblInspection.NeedBazsazi =  (Int16)NeedBazsazi;
            //                tblInspection.NeedShostosho = (Int16) NeedShostosho;
            //                tblInspection.NeedSampashi = (Int16)NeedSampashi;
            //                tblInspection.StatusPashne =  (Int16)StatusPashne;
            //                tblInspection.StatusMahiche = (Int16)StatusMahiche;
            //                tblInspection.JenseManhole = JenseManhole;
            //                tblInspection.TypeManhole = TypeManhole;
            //                tblInspection.VaziatManhole = VaziatManhole;
            //                _db.SaveChanges();
            //                return (TblInspectionVm) tblInspection;
            //            }
            //            else
            //  {
            var tblManhol = _db.TblManhols.FirstOrDefault(d => (d.CodePm == codepm || d.CodeTag == codetag));
            if (tblManhol != null)
            {
               // if (!string.IsNullOrEmpty(codetag))
               // {
                    tblManhol.CodeTag = codetag;
               // }
                var tblInspectionAdd = new TblInspection
                {
                    TblManholId = tblManhol.TblManholId,
                    DateSabt = DateTime.Now,
                    TblUserId = TblUserId,
                    NeedHamsatSazi = (Int16)NeedHamsatSazi,
                    NeedBazsazi = (Int16)NeedBazsazi,
                    NeedShostosho = (Int16)NeedShostosho,
                    NeedSampashi = (Int16)NeedSampashi,
                    StatusPashne = (Int16)StatusPashne,
                    StatusMahiche = (Int16)StatusMahiche,
                    JenseManhole = JenseManhole,
                    TypeManhole = TypeManhole,
                    VaziatManhole = VaziatManhole
                };
                _db.TblInspections.Add(tblInspectionAdd);
                _db.SaveChanges();
                return (TblInspectionVm) tblInspectionAdd;
            }

            return null;
            // }
            return null;
        }
    }
}