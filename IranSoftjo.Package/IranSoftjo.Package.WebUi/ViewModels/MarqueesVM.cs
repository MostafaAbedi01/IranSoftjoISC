using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class MarqueesVM
    {
        [Key]
        [Display(Name = "سریال")]
        public int MarqueeID { get; set; }

        [Display(Name = "پیام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }

        [Display(Name = "آدرس لینک")]
        public string MarqueeUrl { get; set; }

        [Display(Name = "وضعیت فعال")]
        public bool IsActive { get; set; }

        public static explicit operator Marquee(MarqueesVM model)
        {
            var pages = new Marquee
                        {
                            MarqueeID = model.MarqueeID,
                            Title = model.Title,
                            MarqueeUrl = model.MarqueeUrl,
                            IsActive = model.IsActive,
                        };
            return pages;
        }

        public static explicit operator MarqueesVM(Marquee model)
        {

            var pages = new MarqueesVM
                        {
                            MarqueeID = model.MarqueeID,
                            Title = model.Title,
                            MarqueeUrl = model.MarqueeUrl,
                            IsActive = model.IsActive,
                        };
            return pages;
        }

        public static IEnumerable<MarqueesVM> ToIEnumerable(IEnumerable<Marquee> models)
        {
            IEnumerable<MarqueesVM> pages = models.Select(model => new MarqueesVM
                                                                {
                                                                    MarqueeID = model.MarqueeID,
                                                                    Title = model.Title,
                                                                    MarqueeUrl = model.MarqueeUrl,
                                                                    IsActive = model.IsActive,
                                                                });
            return pages;
        }
    }
}