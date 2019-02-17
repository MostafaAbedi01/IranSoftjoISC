using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class GalleryGroupVM
    {
        [Key]
        public int GalleryGroupId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string GalleryGroupTitle { get; set; }

         [Display(Name = "ترتیب")]
        [UIHint("Integer")]
        public int? GalleryGroupOrder { get; set; } 
        
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        public int CountShow { get; set; } 

        public virtual List<Gallery> Galleries { get; set; }

        public static explicit operator GalleryGroup(GalleryGroupVM model)
        {
            var product = new GalleryGroup
            {
               GalleryGroupId = model.GalleryGroupId,
                GalleryGroupTitle = model.GalleryGroupTitle,
                GalleryGroupOrder = model.GalleryGroupOrder,
                IsActive = model.IsActive,
            };
            return product;
        }

        public static explicit operator GalleryGroupVM(GalleryGroup model)
        {
            var product = new GalleryGroupVM
            {
            GalleryGroupId = model.GalleryGroupId,
                GalleryGroupTitle = model.GalleryGroupTitle,
                GalleryGroupOrder = model.GalleryGroupOrder,
                IsActive = model.IsActive,
            };
            return product;
        }


        public static IEnumerable<GalleryGroupVM> ToIEnumerable(IEnumerable<GalleryGroup> models)
        {
            var product = models.Select(model => new GalleryGroupVM
            {
             GalleryGroupId = model.GalleryGroupId,
                GalleryGroupTitle = model.GalleryGroupTitle,
                GalleryGroupOrder = model.GalleryGroupOrder,
                IsActive = model.IsActive,
             Galleries = model.Galleries.ToList(),
            });
            return product;
        }
    }
}