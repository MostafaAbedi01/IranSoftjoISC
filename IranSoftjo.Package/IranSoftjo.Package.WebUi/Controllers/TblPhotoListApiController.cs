using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.ViewModels;
using Newtonsoft.Json;

namespace IranSoftjo.Package.WebUi.Controllers
{
    public class TblPhotoListApiController : ApiController
    {
        private readonly Entities _db = new Entities();
        public IEnumerable<TblPhotoListVM> Get(int tblManholId)
        {
            var json = Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            Configuration.Formatters.Remove(Configuration.Formatters.XmlFormatter);

            return TblPhotoListVM.ToIEnumerable(_db.TblPhotoLists.Where(d => d.ItemId == tblManholId).AsEnumerable());
        }

        public IEnumerable<TblPhotoListVM> Get(int? ItemId, int? code)
        {
            var json = Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            Configuration.Formatters.Remove(Configuration.Formatters.XmlFormatter);
            var tblItem = _db.TblItems.FirstOrDefault(d=>d.ItemCode==code);
            if (tblItem != null)
                ItemId = tblItem.TblItemId;
            return TblPhotoListVM.ToIEnumerable(_db.TblPhotoLists.Where(d => d.ItemId == ItemId).AsEnumerable());
        }


       

    }
}