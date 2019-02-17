using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class TblAndroidLevelVm
    {
        [Key]
        [Display(Name = "سریال")]
        public int TblAndroidLevelId { get; set; }
        [Display(Name = "کد پروژه")]
        public int? TblProjectId { get; set; }
        [Display(Name = "متراژ")]
        public double? Metraj { get; set; }
        [Display(Name = "پایان")]
        public TimeSpan? TimeEnd { get; set; }
        [Display(Name = "شروع")]
        public TimeSpan? TimeStart { get; set; }
        [Display(Name = " تاریخ انجام")]
        public DateTime? DateDo { get; set; }
        [Display(Name = " تاریخ ارسال")]
        public DateTime? DateSend { get; set; }
        [Display(Name = "توضیحات")]
        public string Comment { get; set; }
        public short? LevelType { get; set; }
        [Display(Name = "نوع ماده")]
        public int? KeyID1 { get; set; }
        public int? KeyID2 { get; set; }
        [Display(Name = "هاردنر")]
        public double? Hardener { get; set; }
        [Display(Name = "تینر")]
        public double? Thinner { get; set; }
        [Display(Name = "رنگ")]
        public double? Color { get; set; }
        [Display(Name = "کاربر ثبت کننده")]
        public string FirrstAndLastName { get; set; }
        [Display(Name = "پروژه")]
        public string ProjectName { get; set; }
        [Display(Name = "جبهه")]
        public string ItemsTitle { get; set; }
        [Display(Name = "آیتم")]
        public string SubItemsTitle { get; set; }
        public int? UserID { get; set; }
        public int TblSubItemAccessId { get; set; }
        public int? TblSubItemId { get; set; }

        [Display(Name = "مرحله")]
        public string KeyID1Title { get; set; }
        [Display(Name = "نوع ماده")]
        public string KeyID2Title { get; set; }


        //[Display(Name = "رال")]
        //public int? Ral { get; set; }

        public static explicit operator TblAndroidLevel(TblAndroidLevelVm model)
        {
            var obj = new TblAndroidLevel
            {
                TblAndroidLevelId = model.TblAndroidLevelId,
                TblSubItemAccessId = model.TblSubItemAccessId,
                Metraj = model.Metraj,
                TimeEnd = model.TimeEnd,
                TimeStart = model.TimeStart,
                DateDo = model.DateDo,
                Comment = model.Comment,
                KeyID1 = model.KeyID1,
                KeyID2 = model.KeyID2,
                DateSend = model.DateSend,
                Hardener = model.Hardener,
                Thinner = model.Thinner,
                Color = model.Color,
                UserID = model.UserID,
                TblSubItemId = model.TblSubItemId,
                // Ral = model.Ral,


            };
            return obj;
        }

        public static explicit operator TblAndroidLevelVm(TblAndroidLevel model)
        {
            var obj = new TblAndroidLevelVm
            {
                TblAndroidLevelId = model.TblAndroidLevelId,
                TblSubItemAccessId = model.TblSubItemAccessId,
                Metraj = model.Metraj,
                TimeEnd = model.TimeEnd,
                TimeStart = model.TimeStart,
                DateDo = model.DateDo,
                Comment = model.Comment,
                KeyID1 = model.KeyID1,
                KeyID2 = model.KeyID2,
                DateSend = model.DateSend,
                Hardener = model.Hardener,
                Thinner = model.Thinner,
                Color = model.Color,
                UserID = model.UserID,
                TblSubItemId = model.TblSubItemId,
                //Ral = model.Ral,
            };
            return obj;
        }
        public static IEnumerable<TblAndroidLevelVm> ToIEnumerable(IEnumerable<TblAndroidLevel> models)
        {

            Entities _db = new Entities();
            var lstTblKeyValues = _db.TblKeyValues.ToList();
            var lstRasProducts = _db.RasProducts.ToList();
            var user = models.Select(model =>
            {
                string keyId2Title = "";
                if (model.KeyID1 < 3)
                {
                    var tblKeyValue = lstTblKeyValues.FirstOrDefault(d => d.Type == model.KeyID1 && d.KeyID == model.KeyID2);
                    if (tblKeyValue != null)
                        keyId2Title = tblKeyValue.Title;
                }
                else
                {
                    var rasProduct = lstRasProducts.FirstOrDefault(d => d.ProductGroupID == 1 && d.ProductID == model.KeyID2);
                    if (rasProduct !=
                        null)
                        keyId2Title = rasProduct.ProductTitle;
                }
                var firstOrDefault = lstTblKeyValues.FirstOrDefault(d => d.KeyID == model.KeyID1 && d.Type == 0);
                return firstOrDefault != null ? new TblAndroidLevelVm
                {
                    TblAndroidLevelId = model.TblAndroidLevelId,
                    TblSubItemAccessId = model.TblSubItemAccessId,
                    TblSubItemId = model.TblSubItemId,
                    Metraj = model.Metraj,
                    TimeEnd = model.TimeEnd,
                    TimeStart = model.TimeStart,
                    DateDo = model.DateDo,
                    Comment = model.Comment,
                    KeyID1 = model.KeyID1,
                    KeyID2 = model.KeyID2,
                    DateSend = model.DateSend,
                    Hardener = model.Hardener,
                    Thinner = model.Thinner,
                    Color = model.Color,
                    UserID = model.UserID,
                    FirrstAndLastName = model.User.FirstName + " " + model.User.LastName,
                    KeyID1Title = firstOrDefault.Title,
                    //  KeyID2Title = _db.TblKeyValues.FirstOrDefault(d => d.KeyID == model.KeyID2 && d.Type == (model.KeyID1 >= 3 ? 3 : model.KeyID1)).Title,
                    KeyID2Title = keyId2Title,
                } : null;
            });
            return user;
        }
        public static IEnumerable<TblAndroidLevelVm> ToIEnumerable2(IEnumerable<TblAndroidLevel> models)
        {

            Entities _db = new Entities();
            var lstTblKeyValues = _db.TblKeyValues.ToList();
            var lstRasProducts = _db.RasProducts.ToList();
            var lstTblProjects = _db.TblProjects.ToList();
            var lstTblItems = _db.TblItems.ToList();
            var lstTblSubItems = _db.TblSubItems.ToList();
            var user = models.Select(model =>
            {
                string keyId2Title = "";
                var tblSubItem = lstTblSubItems.FirstOrDefault(d => d.TblSubItemId == model.TblSubItemId);
                var tblProject = lstTblProjects.FirstOrDefault(d => d.TblProjectId == model.TblProjectId);
                var tblItem = lstTblItems.FirstOrDefault(d => d.TblItemId == model.TblItemId);
                if (tblProject != null && tblItem != null && tblSubItem != null)
                {
                    string ProjectName = tblProject.ProjectName;
                    string ItemsTitle = tblItem.Title;
                    string SubItemsTitle = tblSubItem.Title;
                    if (model.KeyID1 < 3)
                    {
                        var tblKeyValue = lstTblKeyValues.FirstOrDefault(d => d.Type == model.KeyID1 && d.KeyID == model.KeyID2);
                        if (tblKeyValue != null)
                            keyId2Title = tblKeyValue.Title;
                    }
                    else
                    {
                        var rasProduct = lstRasProducts.FirstOrDefault(d => d.ProductGroupID == 1 && d.ProductID == model.KeyID2);
                        if (rasProduct !=
                            null)
                            keyId2Title = rasProduct.ProductTitle;
                    }
                    var firstOrDefault = lstTblKeyValues.FirstOrDefault(d => d.KeyID == model.KeyID1 && d.Type == 0);
                    return firstOrDefault != null ? new TblAndroidLevelVm
                    {
                        TblAndroidLevelId = model.TblAndroidLevelId,
                        TblSubItemAccessId = model.TblSubItemAccessId,
                        TblSubItemId = model.TblSubItemId,
                        Metraj = model.Metraj,
                        TimeEnd = model.TimeEnd,
                        TimeStart = model.TimeStart,
                        DateDo = model.DateDo,
                        Comment = model.Comment,
                        KeyID1 = model.KeyID1,
                        KeyID2 = model.KeyID2,
                        DateSend = model.DateSend,
                        Hardener = model.Hardener,
                        Thinner = model.Thinner,
                        Color = model.Color,
                        UserID = model.UserID,
                        FirrstAndLastName = model.User.FirstName + " " + model.User.LastName,
                        ProjectName = ProjectName,
                        ItemsTitle = ItemsTitle,
                        SubItemsTitle = SubItemsTitle,
                        KeyID1Title = firstOrDefault.Title,
                        //  KeyID2Title = _db.TblKeyValues.FirstOrDefault(d => d.KeyID == model.KeyID2 && d.Type == (model.KeyID1 >= 3 ? 3 : model.KeyID1)).Title,
                        KeyID2Title = keyId2Title,
                    } : null;
                }
                return null;
            });
            return user;
        }
    }
}