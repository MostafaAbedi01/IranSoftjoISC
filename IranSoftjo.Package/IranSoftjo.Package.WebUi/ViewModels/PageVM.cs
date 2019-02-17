using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using IranSoftjo.Package.DataModel;

namespace IranSoftjo.Package.WebUi.ViewModels
{
    public class PageVM
    {
        [Key]
        [Display(Name = "سریال")]
        public int PageID { get; set; }

        [Display(Name = "منو")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int PageGroupID { get; set; }

        [Display(Name = "عنوان صفحه")]
        public string PageTitle { get; set; }

        [Display(Name = "متن صفحه")]
        [AllowHtml]
        [UIHint("RichText")]
        public string PageText { get; set; }

        [Display(Name = "تاریخ ایجاد صفحه")]
        public DateTime PageDate { get; set; }

        [Display(Name = "عکس صفحه")]
        public string PageThumbnailImageUrl { get; set; }

        public string PageImageUrl { get; set; }

        [Display(Name = "عنوان گروه")]
        public string PageGroupTitle { get; set; }

        [Display(Name = "خلاصه")]
        public string Summary { get; set; }

        public long Visit { get; set; }
        public virtual PageGroup PageGroup { get; set; }

        [Display(Name = "ترتیب نمایش")]
        [UIHint("Integer")]
        public int? PageOrder { get; set; }

        [Display(Name = "امکان نظر دادن")]
        public bool HasComment { get; set; }

        [Display(Name = "تایید نظر توسط مدیر سایت")]
        public bool ActiveCommentAdmin { get; set; }

        [Display(Name = "فعال بودن صفحه")]
        public bool ActivePage { get; set; }

        [Display(Name = "نمایش تاریخ آخرین به روز رسانی")]
        public bool ActivePageDate { get; set; }

        [Display(Name = "نمایش امکان پرینت")]
        public bool ActivePrint { get; set; }

        [Display(Name = "نمایش تعداد بازدید کننده")]
        public bool ActiveVisit { get; set; }

        [Display(Name = "محتوای ویژه")]
        public bool IsSpecial { get; set; }

        public static explicit operator Page(PageVM model)
        {
            if (model.PageDate.Year == 1)
            {
                model.PageDate = DateTime.Now;
            }
            var pages = new Page
            {
                PageDate = model.PageDate,
                PageGroup = model.PageGroup,
                PageGroupID = model.PageGroupID,
                PageID = model.PageID,
                PageText = model.PageText,
                PageTitle = model.PageTitle,
                PageThumbnailImageUrl = model.PageThumbnailImageUrl,
                PageImageUrl = model.PageImageUrl,
                Summary = model.Summary,
                HasComment = model.HasComment,
                ActiveCommentAdmin = model.ActiveCommentAdmin,
                PageOrder = model.PageOrder,
                ActivePage = model.ActivePage,
                ActivePageDate = model.ActivePageDate,
                ActivePrint = model.ActivePrint,
                ActiveVisit = model.ActiveVisit,
                Visit = model.Visit,
                IsSpecial = model.IsSpecial
            };
            return pages;
        }

        public static explicit operator PageVM(Page model)
        {
            if (model.PageDate.Year == 1)
            {
                model.PageDate = DateTime.Now;
            }
            var pages = new PageVM
            {
                PageDate = model.PageDate,
                PageGroup = model.PageGroup,
                PageGroupID = model.PageGroupID,
                PageID = model.PageID,
                PageText = model.PageText,
                PageTitle = model.PageTitle,
                PageThumbnailImageUrl = model.PageThumbnailImageUrl,
                PageImageUrl = model.PageImageUrl,
                Summary = model.Summary,
                HasComment = model.HasComment,
                ActiveCommentAdmin = model.ActiveCommentAdmin,
                PageOrder = model.PageOrder,
                ActivePage = model.ActivePage,
                ActivePageDate = model.ActivePageDate,
                ActivePrint = model.ActivePrint,
                ActiveVisit = model.ActiveVisit,
                Visit = model.Visit,
                IsSpecial = model.IsSpecial
            };
            if (model.PageGroup != null)
            {
                pages.PageGroupTitle = model.PageGroup.PageGroupTitle;
                pages.PageGroupID = model.PageGroup.PageGroupID;
            }
            return pages;
        }

        public static IEnumerable<PageVM> ToIEnumerable(IEnumerable<PageVM> models)
        {
            var pages = models.Select(model => new PageVM
            {
                PageGroup = model.PageGroup,
                PageGroupID = model.PageGroupID,
                PageDate = model.PageDate,
                PageID = model.PageID,
                PageText = model.PageText,
                PageTitle = model.PageTitle,
                PageThumbnailImageUrl = model.PageThumbnailImageUrl,
                PageImageUrl = model.PageImageUrl,
                PageGroupTitle = model.PageGroup.PageGroupTitle,
                Summary = model.Summary,
                HasComment = model.HasComment,
                ActiveCommentAdmin = model.ActiveCommentAdmin,
                PageOrder = model.PageOrder,
                ActivePage = model.ActivePage,
                ActivePageDate = model.ActivePageDate,
                ActivePrint = model.ActivePrint,
                ActiveVisit = model.ActiveVisit,
                Visit = model.Visit,
                IsSpecial = model.IsSpecial
            });
            return pages;
        }

        public static IEnumerable<PageVM> ToIEnumerable(IQueryable<Page> models)
        {
            IEnumerable<PageVM> pages = models.Select(model => new PageVM
            {
                PageGroup = model.PageGroup,
                PageGroupID = model.PageGroupID,
                PageDate = model.PageDate,
                PageID = model.PageID,
                PageText = model.PageText,
                PageTitle = model.PageTitle,
                PageThumbnailImageUrl = model.PageThumbnailImageUrl,
                PageImageUrl = model.PageImageUrl,
                PageGroupTitle = model.PageGroup.PageGroupTitle,
                Summary = model.Summary,
                HasComment = model.HasComment != null && model.HasComment,
                ActiveCommentAdmin = model.ActiveCommentAdmin != null && model.ActiveCommentAdmin,
                PageOrder = model.PageOrder,
                ActivePage = model.ActivePage,
                ActivePageDate = model.ActivePageDate,
                ActivePrint = model.ActivePrint,
                ActiveVisit = model.ActiveVisit,
                Visit = model.Visit,
                IsSpecial = model.IsSpecial
            });
            return pages;
        }
    }
}