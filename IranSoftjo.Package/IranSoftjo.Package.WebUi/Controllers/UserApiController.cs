using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.Android.ViewModels;
using IranSoftjo.Package.WebUi.ViewModels;
using Newtonsoft.Json;

namespace IranSoftjo.Package.WebUi.Controllers
{
    public class UserApiController : ApiController
    {
        private readonly Entities _db = new Entities();

        public void Post(int userID)
        {
          //  , bool isOnline
            var json = Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            Configuration.Formatters.Remove(Configuration.Formatters.XmlFormatter);
            var userFinded = _db.Users.FirstOrDefault(x => x.UserID == userID);
            if (userFinded != null)
            {
                userFinded.IsOnline = false;
                userFinded.LastDateOnline = DateTime.Now;
                _db.SaveChanges();
            }
        }

        public IEnumerable<UserVm> Get(int userID)
        {
            var json = Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            Configuration.Formatters.Remove(Configuration.Formatters.XmlFormatter);

            return UserVm.ToIEnumerable(_db.Users.Where(d => d.UserID != userID).OrderByDescending(d => d.UserID).AsEnumerable());
        }

        public UserVm Get(string username, string password)
        {
            var json = Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            Configuration.Formatters.Remove(Configuration.Formatters.XmlFormatter);
            var userFinded = _db.Users.FirstOrDefault(x => x.Username == username && x.Password == password);
            if (userFinded != null)
            {
                userFinded.IsOnline = true;
                userFinded.LastDateOnline = DateTime.Now;
                _db.SaveChanges();
                return (UserVm)userFinded;
            }

            return null;
        }

        public User Get(string username, string password, int? type, string Imei)
        {
            var json = Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            Configuration.Formatters.Remove(Configuration.Formatters.XmlFormatter);
            if (_db.TblImeis.Any(d => d.Imei == Imei))
            {
                var userFinded = _db.Users.FirstOrDefault(x => x.Username == username && x.Password == password);
                if (userFinded != null)
                {
                    type = type + 2;
                    if (userFinded.RoleID == 1)
                    {
                        userFinded.IsOnline = true;
                        userFinded.LastDateOnline = DateTime.Now;
                        _db.SaveChanges();
                        return userFinded;
                    }
                    if (type == userFinded.RoleID)
                    {
                        userFinded.IsOnline = true;
                        userFinded.LastDateOnline = DateTime.Now;
                        _db.SaveChanges();
                        return userFinded;
                    }
                    return null;
                }
            }
            return null;
        }

    }
}