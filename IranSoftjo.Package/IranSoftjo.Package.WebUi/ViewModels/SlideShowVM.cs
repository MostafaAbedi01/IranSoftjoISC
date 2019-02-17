using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class SlideShowVM
    {
        [Key]
        [Display(Name = "سریال")]
        public int SlideShowID { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string SlideShowTitle { get; set; }

        [Display(Name = "زیرعنوان")]
        public string SlideShowSubTitle { get; set; }

        [Display(Name = "عکس")]
        public string SlideShowImageUrl { get; set; }

        [Display(Name = "آدرس صفحه")]
        public string SlideShowHref { get; set; }


        public static explicit operator SlideShow(SlideShowVM model)
        {
            var obj = new SlideShow
            {
                SlideShowID = model.SlideShowID,
                SlideShowTitle = model.SlideShowTitle,
                SlideShowSubTitle = model.SlideShowSubTitle,
                SlideShowImageUrl = model.SlideShowImageUrl,
                SlideShowHref = model.SlideShowHref,
            };
            return obj;
        }

        public static explicit operator SlideShowVM(SlideShow model)
        {
            var obj = new SlideShowVM
            {
                SlideShowID = model.SlideShowID,
                SlideShowTitle = model.SlideShowTitle,
                SlideShowSubTitle = model.SlideShowSubTitle,
                SlideShowImageUrl = model.SlideShowImageUrl,
                SlideShowHref = model.SlideShowHref,
            };
            return obj;
        }


        public static IEnumerable<SlideShowVM> ToIEnumerable(DbSet<SlideShow> models)
        {
            var obj = models.Select(model => new SlideShowVM
            {
                SlideShowID = model.SlideShowID,
                SlideShowTitle = model.SlideShowTitle,
                SlideShowSubTitle = model.SlideShowSubTitle,
                SlideShowImageUrl = model.SlideShowImageUrl,
                SlideShowHref = model.SlideShowHref,
            });
            return obj;
        }
    }
}
