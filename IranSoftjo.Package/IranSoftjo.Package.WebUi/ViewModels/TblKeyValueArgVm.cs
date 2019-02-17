using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class TblKeyValueAgrVm
    {
        [Key]
        [Display(Name = "سریال")]
        public int TblKeyValueId { get; set; }
        public int? KeyId { get; set; }
        [Display(Name = "نوع")]
        public int? Type { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; }


        public static explicit operator TblKeyValueAgr(TblKeyValueAgrVm model)
        {
            var obj = new TblKeyValueAgr
            {
                TblKeyValueID = model.TblKeyValueId,
                Title = model.Title,
                KeyID = model.KeyId,
                Type = model.Type,

            };
            return obj;
        }

        public static explicit operator TblKeyValueAgrVm(TblKeyValueAgr model)
        {
            var obj = new TblKeyValueAgrVm
            {
                TblKeyValueId = model.TblKeyValueID,
                Title = model.Title,
                KeyId = model.KeyID,
                Type = model.Type,
            };
            return obj;
        }

        public static IEnumerable<TblKeyValueAgrVm> ToIEnumerable(IEnumerable<TblKeyValueAgr> models)
        {
            var user = models.Select(model => new TblKeyValueAgrVm
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