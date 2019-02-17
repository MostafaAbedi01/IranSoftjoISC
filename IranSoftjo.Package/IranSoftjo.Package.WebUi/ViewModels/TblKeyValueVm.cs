using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class TblKeyValueVm
    {
        [Key]
        [Display(Name = "سریال")]
        public int TblKeyValueId { get; set; }
        public int? KeyId { get; set; }
        [Display(Name = "نوع")]
        public int? Type { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; }


        public static explicit operator TblKeyValue(TblKeyValueVm model)
        {
            var obj = new TblKeyValue
            {
                TblKeyValueID = model.TblKeyValueId,
                Title = model.Title,
                KeyID = model.KeyId,
                Type = model.Type,

            };
            return obj;
        }

        public static explicit operator TblKeyValueVm(TblKeyValue model)
        {
            var obj = new TblKeyValueVm
            {
                TblKeyValueId = model.TblKeyValueID,
                Title = model.Title,
                KeyId = model.KeyID,
                Type = model.Type,
            };
            return obj;
        }

        public static IEnumerable<TblKeyValueVm> ToIEnumerable(IEnumerable<TblKeyValue> models)
        {
            var user = models.Select(model => new TblKeyValueVm
            {
                TblKeyValueId = model.TblKeyValueID,
                Title = model.Title,
                KeyId = model.KeyID,
                Type = model.Type,
            });
            return user;
        }
    }
}