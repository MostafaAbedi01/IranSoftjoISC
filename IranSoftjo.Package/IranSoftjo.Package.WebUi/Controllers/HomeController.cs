using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml;
using IranSoftjo.Core.Web.Mvc;
using IranSoftjo.MvcPaging;
using IranSoftjo.Package.DataModel;
using IranSoftjo.Package.WebUi.ViewModels;
using Mehr;
using Mehr.Reflection;
using Mehr.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Page = IranSoftjo.Package.DataModel.Page;

namespace IranSoftjo.Package.WebUi.Controllers
{
    //[Authorize(Roles = "Administrator")]
   // [AllowAnonymous]
    public class HomeController : Controller
    {

        private readonly Entities _db = new Entities();


         [Authorize]
        // [OutputCache(Duration = 214748364, Location = OutputCacheLocation.Server, VaryByCustom = "EditDateTime")]
        public ActionResult Index(int? id)
        {
            //Session["CountProduct"] = 4;
            //if (id != null)
            //{
            //    string userHostAddressIp = Request.UserHostAddress;
            //    var userid123 = (int)(id / 123);
            //    var dd = DateTime.Now.Date;
            //    if (!_db.Propagandas.Any(d => d.IpVisitor == userHostAddressIp && d.DateTimeVisit.Value >= dd))
            //    {
            //        _db.Propagandas.Add(new Propaganda()
            //                            {
            //                                DateTimeVisit = DateTime.Now,
            //                                IpVisitor = userHostAddressIp,
            //                                UserId = userid123
            //                            });

            //        _db.SaveChanges();
            //    }
            //}

            return View();
        }

        // [OutputCache(Duration = 214748364, Location = OutputCacheLocation.Server, VaryByCustom = "EditDateTime")]
                [Authorize]
        public ActionResult Index1()
        { 
            return View();
        }

                [Authorize]
                public ActionResult Index2()
                {
                    return View();
                }

        public ActionResult Propaganda()
        {
            return View(_db.SiteSettings.FirstOrDefault());
        }

        [HttpGet]
        public ActionResult RequestEvaluation()
        {
            var requestEvaluationVM = new RequestEvaluationVM();
            var enumMetadataFactory = ServiceLocator.ResolveOnCurrentInstance<IEnumMetadataFactory>();
            Dictionary<AmountEnum, string> item = enumMetadataFactory.Get<AmountEnum>().Items;
            Dictionary<int, string> lstselect = item.ToDictionary(variable => (int)variable.Key,
                variable => variable.Value);
            requestEvaluationVM.SelectListAmount = lstselect.ToSelectList();

            Dictionary<ProjectTypeEnum, string> itemProjectType = enumMetadataFactory.Get<ProjectTypeEnum>().Items;
            Dictionary<int, string> lstselectProjectType = itemProjectType.ToDictionary(variable => (int)variable.Key,
                variable => variable.Value);
            requestEvaluationVM.SelectListProjectType = lstselectProjectType.ToSelectList();

            return View(requestEvaluationVM);
        }

        [HttpPost]
        public ActionResult RequestEvaluation(RequestEvaluationVM modelVM)
        {
            if (ModelState.IsValid)
            {
                var model = (RequestEvaluation)modelVM;
                _db.RequestEvaluations.Add(model);
                _db.SaveChanges();
                TempData.SetMessage("درخواست ارزیابی شما با موفقیت ثبت شد", MessageType.Success);
                return RedirectToAction("Index");
            }
            var enumMetadataFactory = ServiceLocator.ResolveOnCurrentInstance<IEnumMetadataFactory>();
            Dictionary<AmountEnum, string> item = enumMetadataFactory.Get<AmountEnum>().Items;
            Dictionary<int, string> lstselect = item.ToDictionary(variable => (int)variable.Key,
                variable => variable.Value);
            modelVM.SelectListAmount = lstselect.ToSelectList();

            Dictionary<ProjectTypeEnum, string> itemProjectType = enumMetadataFactory.Get<ProjectTypeEnum>().Items;
            Dictionary<int, string> lstselectProjectType = itemProjectType.ToDictionary(variable => (int)variable.Key,
                variable => variable.Value);
            modelVM.SelectListProjectType = lstselectProjectType.ToSelectList();
            return View(modelVM);
        }


        public ActionResult SaveDomainName(string domainName)
        {
            if (string.IsNullOrEmpty(domainName))
                TempData.SetMessage("لطفا نام  سایت خود را وارد نمایید.", MessageType.Error);
            else
            {
                Session["DomainName"] = domainName;

            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.ToString());
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteDomainName()
        {
            Session["DomainName"] = null;

            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.ToString());
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SaveDiscountCode(string discountCode)
        {
            if (string.IsNullOrEmpty(discountCode))
            {
                TempData.SetMessage("لطفا کد تخفیف را وارد نمایید.", MessageType.Error);
            }
            else
            {
                if (_db.Agencies.Any(d => d.Code == discountCode))
                {
                    Session["DiscountCode"] = discountCode;
                }
                else
                {
                    TempData.SetMessage("کد تخفیف وارد شده صحیح نمی باشد.", MessageType.Error);
                }
            }
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.ToString());
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteDiscountCode()
        {
            Session["DiscountCode"] = null;
            if (Request.UrlReferrer != null)
                return Redirect(Request.UrlReferrer.ToString());
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AjaxContentTheme(int? id)
        {
            if (id != null)
            {
                Product product = _db.Products.FirstOrDefault(d => d.ProductID == id);
                if (product != null)
                    Session["Address"] = product.ProductImageUrl;
            }
            return View();
        }


        public ActionResult PageTab(int id = 0)
        {
            Session["PageGroupTitle"] = _db.PageGroups.FirstOrDefault(d => d.PageGroupID == id).PageGroupTitle;
            var page = _db.Pages.Where(d => d.PageGroupID == id);
            return View(PageVM.ToIEnumerable(page).ToList());
        }

        public ActionResult PageList(int? id, int? searchpage, string tag)
        {
            if (searchpage == null)
                searchpage = 1;
            Session["searchpagePageList"] = searchpage;
            IQueryable<PageVM> page;
            if (!string.IsNullOrEmpty(tag))
            {
                page =
                   _db.Pages
                   .Where(d => d.PageTitle.Contains(tag))
                    //.Where(d => d.ActivePage)
                   .OrderByDescending(d => d.PageID)
                   .Select(model => new PageVM
                   {
                       PageGroup = model.PageGroup,
                       PageGroupID = model.PageGroupID,
                       PageDate = model.PageDate,
                       PageID = model.PageID,
                       PageTitle = model.PageTitle,
                       PageThumbnailImageUrl = model.PageThumbnailImageUrl,
                       PageImageUrl = model.PageImageUrl,
                       PageGroupTitle = model.PageGroup.PageGroupTitle,
                       Summary = model.Summary,
                       HasComment = model.HasComment != null && (bool)model.HasComment,
                       ActiveCommentAdmin = model.ActiveCommentAdmin != null && (bool)model.ActiveCommentAdmin,
                       PageOrder = model.PageOrder,
                       ActivePage = model.ActivePage,
                       ActivePageDate = model.ActivePageDate,
                       ActivePrint = model.ActivePrint,
                       ActiveVisit = model.ActiveVisit,
                       Visit = model.Visit,
                   });
            }
            else
            {

                if (id != null)
                {
                    var pageGroup = _db.PageGroups.FirstOrDefault(d => d.PageGroupID == id);
                    if (pageGroup != null)
                        Session["PageGroupTitle"] = pageGroup.PageGroupTitle;
                    page = _db.Pages
                        .Where(d => d.PageGroupID == id)
                        //.Where(d => d.ActivePage)
                        .OrderByDescending(d => d.PageID)
                        .Select(model => new PageVM
                        {
                            PageGroup = model.PageGroup,
                            PageGroupID = model.PageGroupID,
                            PageDate = model.PageDate,
                            PageID = model.PageID,
                            PageTitle = model.PageTitle,
                            PageThumbnailImageUrl = model.PageThumbnailImageUrl,
                            PageImageUrl = model.PageImageUrl,
                            PageGroupTitle = model.PageGroup.PageGroupTitle,
                            Summary = model.Summary,
                            HasComment = model.HasComment != null && (bool)model.HasComment,
                            ActiveCommentAdmin = model.ActiveCommentAdmin != null && (bool)model.ActiveCommentAdmin,
                            PageOrder = model.PageOrder,
                            ActivePage = model.ActivePage,
                            ActivePageDate = model.ActivePageDate,
                            ActivePrint = model.ActivePrint,
                            ActiveVisit = model.ActiveVisit,
                            Visit = model.Visit,
                        });
                }
                else
                {
                    Session["PageGroupTitle"] = "آرشیو همه پستها";
                    page =
                        _db.Pages
                        //.Where(d => d.ActivePage)
                        .OrderByDescending(d => d.PageID)
                        .Select(model => new PageVM
                        {
                            PageGroup = model.PageGroup,
                            PageGroupID = model.PageGroupID,
                            PageDate = model.PageDate,
                            PageID = model.PageID,
                            PageTitle = model.PageTitle,
                            PageThumbnailImageUrl = model.PageThumbnailImageUrl,
                            PageImageUrl = model.PageImageUrl,
                            PageGroupTitle = model.PageGroup.PageGroupTitle,
                            Summary = model.Summary,
                            HasComment = model.HasComment != null && (bool)model.HasComment,
                            ActiveCommentAdmin = model.ActiveCommentAdmin != null && (bool)model.ActiveCommentAdmin,
                            PageOrder = model.PageOrder,
                            ActivePage = model.ActivePage,
                            ActivePageDate = model.ActivePageDate,
                            ActivePrint = model.ActivePrint,
                            ActiveVisit = model.ActiveVisit,
                            Visit = model.Visit,
                        });
                }
            }
            var pageToPagedList = page.ToPagedList((int)(searchpage - 1), 16);
            return View(pageToPagedList);
        }

        public ActionResult PagePanelBar(int id = 0)
        {
            Session["PageGroupTitle"] = _db.PageGroups.FirstOrDefault(d => d.PageGroupID == id).PageGroupTitle;
            var page = _db.Pages.Where(d => d.PageGroupID == id);
            return View(PageVM.ToIEnumerable(page).ToList());
        }

        public ActionResult PageBook(int id = 0)
        {
            Session["PageGroupTitle"] = _db.PageGroups.FirstOrDefault(d => d.PageGroupID == id).PageGroupTitle;
            var page = _db.Pages.Where(d => d.PageGroupID == id);
            return View(PageVM.ToIEnumerable(page).ToList());
        }

        public ActionResult PageText(int? id, int? pageGroupId, int? pageID, string title)
        {
            Page page;
            if (id != null)
            {
                pageID = id;
            }

            if (pageGroupId != null)
            {
                Session["PageGroupTitle"] = _db.PageGroups.FirstOrDefault(d => d.PageGroupID == pageGroupId).PageGroupTitle;
                page = _db.Pages.FirstOrDefault(d => d.PageGroupID == pageGroupId);
            }
            else
            {
                Session["PageGroupTitle"] = _db.Pages.FirstOrDefault(d => d.PageID == pageID).PageTitle;
                page = _db.Pages.FirstOrDefault(d => d.PageID == pageID);
            }
            page.Visit = page.Visit + 1;
            _db.SaveChanges();
            return View((PageVM)page);
        }

        public ActionResult PageWindow(int id = 0)
        {
            return GetPage(id);
        }
        public ActionResult PageNull(int id = 0)
        {
            return GetPage(id);
        }

        private ActionResult GetPage(int id, int? Searchpage = null)
        {
            Page pages = _db.Pages.FirstOrDefault(m => m.PageID == id);
            if (pages != null)
            {
                pages.Visit++;
                _db.SaveChanges();
                var vm = (PageVM)pages;
                //vm.PageComment = Searchpage;
                return View(vm);
            }
            return null;
        }

        public ActionResult PageNotFound()
        {
            //301
            TempData.SetMessage("صفحه مورد نظر یافت نشد", MessageType.Error);
            return RedirectToActionPermanent("Index", "Home");
        }


        [HttpGet]
        public ActionResult SendComment(int id)
        {
            var model = new CommentVM() { SubID = id };
            return View(model);
        }

        [HttpPost]
        public ActionResult SendComment(CommentVM model)
        {
            var _db = new Entities();
            var comment = (Comment)model;
            comment.CommentDate = DateTime.Now;
            _db.Comments.Add(comment);
            _db.SaveChanges();
            return RedirectToAction("SendCommentResult", new { id = comment.CommentID });
        }

        public ActionResult SendCommentResult(int id)
        {
            var comment = _db.Comments.Find(id);
            return View((CommentVM)comment);
        }

        public ActionResult ListRss()
        {
            return View();
        }

        public ActionResult Rss(int[] pageGroup)
        {

           var siteSetting = (SiteSetting)HttpContext.Cache["SiteSetting"];
           if (siteSetting==null)
            {
             siteSetting=   _db.SiteSettings.FirstOrDefault();
            }
            // Clear any previous output from the buffer
            Response.Clear();
            Response.ContentType = "text/xml";

            // XML Declaration Tag
            var xml = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
            xml.WriteStartDocument();

            // RSS Tag
            xml.WriteStartElement("rss");
            xml.WriteAttributeString("version", "2.0");

            // The Channel Tag - RSS Feed Details
            xml.WriteStartElement("channel");
            xml.WriteElementString("title", siteSetting.WebSiteTitle + " - News RSS Feed");
            xml.WriteElementString("link", siteSetting.WebSiteName);
            xml.WriteElementString("description", "همه روزه با خبر های روز با ما همراه باشید");
            xml.WriteElementString("copyright", "Copyright 2015. All rights reserved.");

            xml.WriteStartElement("image");
            xml.WriteElementString("title", siteSetting.WebSiteTitle);
            xml.WriteElementString("url", siteSetting.LogoImageUrl);
            xml.WriteElementString("link", siteSetting.WebSiteName);
            xml.WriteEndElement();
            try
            {
                var lstPages=new List<Page>();
                if (pageGroup==null)
                {
                    lstPages = _db.Pages.OrderByDescending(d => d.PageID).Take(15).ToList();
                }
                else
                {
                    foreach (int variable in pageGroup)
                    {
                        lstPages.AddRange(_db.Pages.Where(d => d.PageGroupID == variable).OrderByDescending(d => d.PageID).Take(15).ToList()); 
                    }
                }
                foreach (var item in lstPages.OrderByDescending(d => d.PageID))
                {
                    xml.WriteStartElement("item");
                    xml.WriteElementString("title", item.PageTitle);
                    xml.WriteElementString("description", item.Summary);
                    xml.WriteElementString("link", siteSetting.WebSiteName+"/Home/PageText/" + item.PageID);
                    xml.WriteElementString("pubDate", item.PageDate.ToString());
                    xml.WriteElementString("image", siteSetting.WebSiteName + item.PageThumbnailImageUrl);
                    xml.WriteEndElement();
                }

            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            xml.WriteEndElement();
            xml.WriteEndElement();
            xml.WriteEndDocument();
            xml.Flush();
            xml.Close();
            Response.End();
            return null;
        }
    }
}