using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class TblPhotoListVM
    {
        [Key]
        [Display(Name = "سریال عکس ")]
        public int TblPhotoListId { get; set; }

        [Display(Name = "سریال ")]
        public int ItemId { get; set; }

        [Display(Name = "عنوان تصویر ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "تصویر ")]
        public string ImageUrl { get; set; }

        [Display(Name = "تصویر کوچک ")]
        public string ThumbnailImageUrl { get; set; }

        public static explicit operator TblPhotoList(TblPhotoListVM model)
        {
            var obj = new TblPhotoList
            {
                ItemId = model.ItemId,
                Title = model.Title,
                ThumbnailImageUrl = model.ThumbnailImageUrl,
                ImageUrl = model.ImageUrl,
                TblPhotoListId = model.TblPhotoListId,
            };
            return obj;
        }

        public static explicit operator TblPhotoListVM(TblPhotoList model)
        {
            var obj = new TblPhotoListVM
            {
                ItemId = model.ItemId,
                Title = model.Title,
                ThumbnailImageUrl = model.ThumbnailImageUrl,
                ImageUrl = model.ImageUrl,
                TblPhotoListId = model.TblPhotoListId,
            };
            return obj;
        }


        public static IEnumerable<TblPhotoListVM> ToIEnumerable(IEnumerable<TblPhotoList> models)
        {
            var obj = models.Select(model => new TblPhotoListVM
            {
                ItemId = model.ItemId,
                Title = model.Title,
                ThumbnailImageUrl = model.ThumbnailImageUrl,
                ImageUrl = model.ImageUrl,
                TblPhotoListId = model.TblPhotoListId,
            });
            return obj;
        }
    }
}