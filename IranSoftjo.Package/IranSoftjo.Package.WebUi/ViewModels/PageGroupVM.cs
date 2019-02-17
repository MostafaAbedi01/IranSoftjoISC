using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using IranSoftjo.Package.DataModel;
using Mehr;
using Mehr.Reflection;
using Mehr.Web.Mvc;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class PageGroupVM
    {
        [Key]
        [Display(Name = "سریال منو")]
        public int PageGroupID { get; set; }

        public int? PageGroupIDTree { get; set; }

        [Display(Name = "عنوان منو")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string PageGroupTitle { get; set; }

        [Display(Name = "وضعیت فعال بودن منو")]
        public bool PageGroupIsActive { get; set; }

        [Display(Name = "ترتیب منو")]
        [UIHint("Integer")]
        public int? PageGroupOrder { get; set; }

        [Display(Name = "نوع منو")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int PageGroupType { get; set; }

        [Display(Name = "نوع منو")]
        public SelectList SelectListPageGroupType { get; set; }

        public virtual ICollection<Page> Pages { get; set; }

        [Display(Name = "زیر منو دار باشد")]
        public bool IsSubMenu { get; set; }

        [Display(Name = "منوی اصلی لینک دار باشد")]
        public bool IsMenuLink { get; set; }

        public static explicit operator PageGroup(PageGroupVM model)
        {
            var obj = new PageGroup
            {
                PageGroupID = model.PageGroupID,
                PageGroupTitle = model.PageGroupTitle,
                PageGroupIsActive = model.PageGroupIsActive,
                PageGroupOrder = model.PageGroupOrder,
                PageGroupType = model.PageGroupType,
                IsSubMenu = model.IsSubMenu,
                IsMenuLink = model.IsMenuLink,
                PageGroupIDTree = model.PageGroupIDTree,
            };
            return obj;
        }

        public static explicit operator PageGroupVM(PageGroup model)
        {
            var enumMetadataFactory = ServiceLocator.ResolveOnCurrentInstance<IEnumMetadataFactory>();
            Dictionary<PageGroupTypeEnum, string> item = enumMetadataFactory.Get<PageGroupTypeEnum>().Items;
            Dictionary<int, string> lstselect = item.ToDictionary(variable => (int)variable.Key,
                variable => variable.Value);
            var obj = new PageGroupVM
            {
                PageGroupID = model.PageGroupID,
                PageGroupTitle = model.PageGroupTitle,
                PageGroupIsActive = model.PageGroupIsActive,
                PageGroupOrder = model.PageGroupOrder,
                PageGroupType = model.PageGroupType,
                IsSubMenu = model.IsSubMenu,
                IsMenuLink = model.IsMenuLink,
                PageGroupIDTree = model.PageGroupIDTree,
                SelectListPageGroupType = lstselect.ToSelectList(),
            };
            return obj;
        }


        public static IEnumerable<PageGroupVM> ToIEnumerable(IEnumerable<PageGroup> models)
        {
            var obj = models.Select(model => new PageGroupVM
            {
                PageGroupID = model.PageGroupID,
                PageGroupTitle = model.PageGroupTitle,
                PageGroupIsActive = model.PageGroupIsActive,
                PageGroupOrder = model.PageGroupOrder,
                PageGroupType = model.PageGroupType,
                IsSubMenu = model.IsSubMenu,
                IsMenuLink = model.IsMenuLink,
                PageGroupIDTree = model.PageGroupIDTree,
            });
            return obj;
        }
    }
}