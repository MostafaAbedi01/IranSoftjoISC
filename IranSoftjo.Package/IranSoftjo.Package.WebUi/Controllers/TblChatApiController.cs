using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.ViewModels;
using Newtonsoft.Json;

namespace IranSoftjo.Package.WebUi.Controllers
{
    public class TblChatApiController : ApiController
    {
        private readonly Entities _db = new Entities();

        public IEnumerable<TblChatVm> Get(int tblUserIdFrom, int tblUserIdTo)
        {
            var json = Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            Configuration.Formatters.Remove(Configuration.Formatters.XmlFormatter);
            var tblchat =
                _db.TblChats.Where(
                    d =>
                        (d.TblUserIdFrom == tblUserIdFrom && d.TblUserIdTo == tblUserIdTo) ||
                        (d.TblUserIdFrom == tblUserIdTo && d.TblUserIdTo == tblUserIdFrom)
                    ).OrderByDescending(d => d.TblChatId).Take(40).AsEnumerable();
            return TblChatVm.ToIEnumerable(tblchat.OrderBy(d => d.TblChatId));
        }

        public TblChat Post(
            string textChat,
            int tblUserIdFrom,
            int tblUserIdTo
            )
        {
            try
            {
                var tblItem = new TblChat
                {
                    TblUserIdFrom = tblUserIdFrom,
                    TextChat = textChat,
                    DateSabt = DateTime.Now,
                    IsRead = false,
                    TblUserIdTo = tblUserIdTo
                };
                _db.TblChats.Add(tblItem);
                _db.SaveChanges();
                return tblItem;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Post(
            string textChat,
            int tblUserIdFrom,
            string tblUserIdTo,
            int isMulti
            )
        {
            foreach (var item in tblUserIdTo.Split(','))
            {
                var tblUserIdToInt = 0;

                if (int.TryParse(item, out tblUserIdToInt))
                {
                    var tblItem = new TblChat
                    {
                        TblUserIdFrom = tblUserIdFrom,
                        TextChat = textChat,
                        DateSabt = DateTime.Now,
                        IsRead = false,
                        TblUserIdTo = tblUserIdToInt
                    };
                    _db.TblChats.Add(tblItem);
                }

            }

            _db.SaveChanges();
        }
    }
}