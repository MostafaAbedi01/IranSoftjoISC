using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class TblDocumentTreeVm
    {
        [Key]
        [Display(Name = "سریال")]
        public int TblDocumentTreeId { get; set; }

        [Display(Name = "تاریخ ثبت اطلاعات")]
        public DateTime? DateSabt { get; set; }

        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [Display(Name = "کاربر")]
        public int? TblUserId { get; set; }

        [Display(Name = "منطقه")]
        public Int16? MantagheShahrdari { get; set; }
        [Display(Name = "پلاک")]
        public double? Pelak { get; set; }
        [Display(Name = "نوع درخت")]
        public Int16? TreeType { get; set; }
        [Display(Name = "سال کاشت")]
        public Int32? YearPlanting { get; set; }



        [Display(Name = "عرض جغرافیایی")]
        public double? Latitude { get; set; }

        [Display(Name = "طول جغرافیایی")]
        public double? Longitude { get; set; }
        public double Space { get; set; }
  
        public double SpaceLongitude { get; set; }
        public double SpaceLatitude { get; set; }
        public string TreeTypeTitle { get; set; }

        [Display(Name = "تصویر ")]
        public string ImageUrl { get; set; }

        [Display(Name = "تصویر کوچک ")]
        public string ThumbnailImageUrl { get; set; }

        public static explicit operator TblDocumentTree(TblDocumentTreeVm model)
        {
            var obj = new TblDocumentTree
            {
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                DateSabt = model.DateSabt,
                Address = model.Address,
                MantagheShahrdari = model.MantagheShahrdari,
                TblUserId = model.TblUserId,
                Pelak = model.Pelak,
                TreeType = model.TreeType,
                YearPlanting = model.YearPlanting,
                TblDocumentTreeId = model.TblDocumentTreeId
            };
            return obj;
        }

        public static explicit operator TblDocumentTreeVm(TblDocumentTree model)
        {
            var obj = new TblDocumentTreeVm
            {
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                DateSabt = model.DateSabt,
                Address = model.Address,
                MantagheShahrdari = model.MantagheShahrdari,
                TblUserId = model.TblUserId,
                Pelak = model.Pelak,
                TreeType = model.TreeType,
                YearPlanting = model.YearPlanting,
                TblDocumentTreeId = model.TblDocumentTreeId
            };
            return obj;
        }

        public static IEnumerable<TblDocumentTreeVm> ToIEnumerable(IEnumerable<TblDocumentTree> models)
        {
            Entities _db = new Entities();
            var lst = _db.TblPhotoLists.ToList();
            var user = models.Select(model => new TblDocumentTreeVm
            {
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                DateSabt = model.DateSabt,
                Address = model.Address,
                MantagheShahrdari = model.MantagheShahrdari,
                TblUserId = model.TblUserId,
                Pelak = model.Pelak,
                TreeType = model.TreeType,
                YearPlanting = model.YearPlanting,
                TblDocumentTreeId = model.TblDocumentTreeId,
                ThumbnailImageUrl = lst.FirstOrDefault(d => d.ItemId == model.TblDocumentTreeId)?.ThumbnailImageUrl,
        });
            return user;
        }
    }
}