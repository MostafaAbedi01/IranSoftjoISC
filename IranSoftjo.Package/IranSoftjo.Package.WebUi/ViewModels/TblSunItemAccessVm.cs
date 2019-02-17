using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class TblSubItemAccessVm
    {
        [Key]
        [Display(Name = "سریال")]
        public int TblSubItemAccessId { get; set; }
        public int TblSubItemId { get; set; }

        [Display(Name = "مرحله")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int KeyId1 { get; set; }
        public int KeyId1_1 { get; set; }

        [Display(Name = "نوع ماده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int KeyId2 { get; set; }

        [Display(Name = "توضیحات")]
        public string Title { get; set; }
            
        [Display(Name = "مرحله")]
        public string KeyID1Title { get; set; }
             
        [Display(Name = "نوع ماده")]
        public string KeyID2Title { get; set; }


        [Display(Name = "رال")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "طول باید 4 تا باشد")]
        public string Ral { get; set; }

        [Display(Name = "نوع تینر")]
        public int? ThinnerType { get; set; }
        [Display(Name = "نوع هاردنر")]
        public int? HardenerType { get; set; }
        [Display(Name = "نوع هاردنر")]
        public string HardenerTypeTitle { get; set; }
        [Display(Name = "نوع تینر")]
        public string ThinnerTypeTitle { get; set; }

        public static explicit operator TblSubItemAccess(TblSubItemAccessVm model)
        {
            var obj = new TblSubItemAccess
            {
                TblSubItemAccessId = model.TblSubItemAccessId,
                Title = model.Title,
                KeyID1 = model.KeyId1,
                KeyID2 = model.KeyId2,
                TblSubItemId = model.TblSubItemId,
                Ral = model.Ral,

            };
            return obj;
        }

        public static explicit operator TblSubItemAccessVm(TblSubItemAccess model)
        {
            Entities _db = new Entities();
            var tblKeyValue1 = _db.TblKeyValues.FirstOrDefault(d => d.KeyID == model.KeyID1);
            var tblKeyValue2 = _db.TblKeyValues.FirstOrDefault(d => d.KeyID == model.KeyID2 && d.Type == model.KeyID1);
            string keyId1Title = null;
            string keyId2Title = null;
            if (tblKeyValue1 != null && tblKeyValue2 != null)
            {
                keyId1Title = tblKeyValue1.Title;
                keyId2Title = tblKeyValue2.Title;
            }
            var obj = new TblSubItemAccessVm
            {
                TblSubItemAccessId = model.TblSubItemAccessId,
                Title = model.Title,
                KeyId1 = model.KeyID1,
                KeyId2 = model.KeyID2,
                KeyID1Title = keyId1Title,
                KeyID2Title = keyId2Title,
                TblSubItemId = model.TblSubItemId,
                Ral = model.Ral,

            };
            return obj;
        }

        public static IEnumerable<TblSubItemAccessVm> ToIEnumerable(IEnumerable<TblSubItemAccess> models)
        {
            Entities _db = new Entities();
            var user = models.Select(model =>
            {
                var firstOrDefault = _db.TblKeyValues.FirstOrDefault(d => d.KeyID == model.KeyID1 && d.Type == 0);
                string title;
                string thinnerTypeTitle = null;
                string hardenerTypeTitle = null;
                if (model.KeyID1 < 3)
                {
                    title = _db.TblKeyValues.FirstOrDefault(d => d.KeyID == model.KeyID2 && d.Type == model.KeyID1).Title;
                }
                else
                {
                    title = _db.RasProducts.FirstOrDefault(d => d.ProductGroupID == 1 && d.ProductID == model.KeyID2).ProductTitle;
                }

                if (model.KeyID1 >= 3)
                {
                    int? productIdHardener = _db.RasProducts.FirstOrDefault(d => d.ProductID == model.KeyID2).ProductIdHardener;
                    if (productIdHardener!=null)
                    {
                        hardenerTypeTitle = _db.RasProducts.FirstOrDefault(d => d.ProductID == productIdHardener).ProductTitle;
                    }
                    int? productIdThinner = _db.RasProducts.FirstOrDefault(d => d.ProductID == model.KeyID2).ProductIdThinner;
                    if (productIdHardener != null)
                    {
                        thinnerTypeTitle = _db.RasProducts.FirstOrDefault(d => d.ProductID == productIdThinner).ProductTitle;
                    }
                }
                return firstOrDefault != null ? new TblSubItemAccessVm
                                                  {
                                                      TblSubItemAccessId = model.TblSubItemAccessId,
                                                      Title = model.Title,
                                                      KeyId1 = model.KeyID1,
                                                      KeyId1_1 = model.KeyID1,
                                                      KeyId2 = model.KeyID2,
                                                      KeyID1Title = firstOrDefault.Title,
                                                      KeyID2Title = title,
                                                      TblSubItemId = model.TblSubItemId,
                                                      Ral = model.Ral,
                                                      HardenerTypeTitle = hardenerTypeTitle,
                                                      ThinnerTypeTitle = thinnerTypeTitle,
                                                  } : null;
            });
            return user;
        }
    }
}