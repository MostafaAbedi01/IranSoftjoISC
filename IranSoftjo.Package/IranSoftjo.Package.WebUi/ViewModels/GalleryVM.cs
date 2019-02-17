using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class GalleryVM
    {
        [Key]
        [Display(Name = "سریال")]
        public int GalleryId { get; set; }

        [Display(Name = "گروه عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int GalleryGroupId { get; set; }

        [Display(Name = "عنوان عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "عنوان گروه گالری")]
        public string GalleryGroupTitle { get; set; }

        [Display(Name = "توضیحات عکس")]
        public string Comment { get; set; }

        [Display(Name = "آدرس عکس")]
        public string GalleryImageUrl { get; set; }

        [Display(Name = "آدرس عکس")]
        public string GalleryImageUrlThumb { get; set; }

        [Display(Name = "ترتیب نمایش")]
        [UIHint("Integer")]
        public int? GalleryOrder { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        public virtual GalleryGroup GalleryGroup { get; set; }

        public static explicit operator Gallery(GalleryVM model)
        {
            var obj = new Gallery
                        {
                            GalleryGroupsId = model.GalleryGroupId,
                            GalleryId = model.GalleryId,
                            GalleryImageUrl = model.GalleryImageUrl,
                            Title = model.Title,
                            GalleryImageUrlThumb = model.GalleryImageUrlThumb,
                            Comment = model.Comment,
                            GalleryOrder = model.GalleryOrder,
                            IsActive = model.IsActive,
                            GalleryGroup = model.GalleryGroup,
                        };
            return obj;
        }

        public static explicit operator GalleryVM(Gallery model)
        {
            var pages = new GalleryVM
                        {
                            GalleryGroupId = model.GalleryGroupsId,
                            GalleryId = model.GalleryId,
                            GalleryImageUrl = model.GalleryImageUrl,
                            Title = model.Title,
                            GalleryImageUrlThumb = model.GalleryImageUrlThumb,
                            Comment = model.Comment,
                            GalleryOrder = model.GalleryOrder,
                            IsActive = model.IsActive,
                            GalleryGroup = model.GalleryGroup,
                        };
            if (model.GalleryGroup != null)
            {
                pages.GalleryGroupTitle = model.GalleryGroup.GalleryGroupTitle;
            }
            return pages;
        }

        public static IEnumerable<GalleryVM> ToIEnumerable(IEnumerable<Gallery> models)
        {
            IEnumerable<GalleryVM> pages = models.Select(model => new GalleryVM
                                                                {
                                                                    GalleryGroupId = model.GalleryGroupsId,
                                                                    GalleryId = model.GalleryId,
                                                                    GalleryImageUrl = model.GalleryImageUrl,
                                                                    Title = model.Title,
                                                                    GalleryImageUrlThumb = model.GalleryImageUrlThumb,
                                                                    Comment = model.Comment,
                                                                    GalleryOrder = model.GalleryOrder,
                                                                    IsActive = model.IsActive,
                                                                    GalleryGroup = model.GalleryGroup,
                                                                    GalleryGroupTitle = model.GalleryGroup.GalleryGroupTitle,
                                                                });
            return pages;
        }
    }
}