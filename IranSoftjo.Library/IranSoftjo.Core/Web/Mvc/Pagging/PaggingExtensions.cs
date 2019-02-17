using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Diagnostics.Contracts;
using Mehr.Linq;
using System.Web.Mvc.Html;
using System.Collections;
using Mehr.Web.Mvc.Pagging;

namespace Mehr.Web.Mvc.Html
{
    public static class PaggingExtensions
    {
        public static void RendarePaging(this HtmlHelper Html)
        {
            Contract.Requires(Html != null);

            IPaginated paginated = Html.ViewContext.ViewData.Model as IPaginated;
            Contract.Assume(paginated != null);
            
            RendarePaging(Html, paginated);
        }

        public static void RendarePaging(this HtmlHelper Html, IPaginated Paginated)
        {
            string currentActionName = Html.ViewContext.RequestContext.RouteData.Values["action"].ToString();
            RendarePagingForAction(Html, Paginated, currentActionName);
        }

        public static void RendarePagingForAction(this HtmlHelper Html, IPaginated Paginated, string actionName)
        {
            Contract.Requires(Html != null);
            Contract.Requires(Paginated != null);
            Contract.Requires(actionName != null);

            var urlHelper = new UrlHelper(Html.ViewContext.RequestContext, Html.RouteCollection);
            RendarePaging(Html, Paginated, urlHelper.Action(actionName, new { page = "{page}" }));
        }

        public static void RendarePaging(this HtmlHelper Html, IPaginated Paginated, string pageLink)
        {
            Contract.Requires(Html != null);
            Contract.Requires(Paginated != null);
            Contract.Assume(!string.IsNullOrWhiteSpace(pageLink));
            Contract.Assume(pageLink.Contains("%7Bpage%7D"), string.Format("Passed pageLink is '{0}'", pageLink));//'%7Bpage%7D' is html encoded '{page}'

            string sort = Html.ViewContext.HttpContext.Request["sort"];
            if (!string.IsNullOrWhiteSpace(sort))
                if (pageLink.Contains('?'))
                    pageLink += "&sort=" + sort;
                else
                    pageLink += "?sort=" + sort;

            if (Paginated.TotalPages > 1)
            {
                Html.RenderPartial("Pagination", new PaginatedView
                {
                    Paginated = Paginated,
                    PageActionLink = pageLink
                });
            }
        }

        public const string EmptyListTemplate = @"
<div class=""emptyList"">
	<hr />
	<b>موردی برای نمایش وجود ندارد.</b>
	<hr />
</div>";
        public static MvcHtmlString EmptyList(this HtmlHelper Html, ICollection Paginated)
        {
            if (Paginated.Count == 0)
                return MvcHtmlString.Create(EmptyListTemplate);
            return MvcHtmlString.Empty;
        }

        public static MvcHtmlString EmptyListForIEnumerable<T>(this HtmlHelper Html, IEnumerable<T> Paginated)
        {
            if (Paginated.Count() == 0)
                return MvcHtmlString.Create(EmptyListTemplate);
            return MvcHtmlString.Empty;
        }


        public static MvcHtmlString PagingOptions(this HtmlHelper Html, string id, IPaginated Paginated, string pageLink)
        {
            Contract.Requires(Html != null);
            Contract.Requires(Paginated != null);
            Contract.Assume(!string.IsNullOrWhiteSpace(pageLink));
            Contract.Assume(pageLink.Contains("%7Bpage%7D"), string.Format("Passed pageLink is '{0}'", pageLink));//'%7Bpage%7D' is html encoded '{page}'

            string sort = Html.ViewContext.HttpContext.Request["sort"];
            if (!string.IsNullOrWhiteSpace(sort))
                if (pageLink.Contains('?'))
                    pageLink += "&sort=" + sort;
                else
                    pageLink += "?sort=" + sort;

            if (Paginated.TotalPages > 1)
            {
                return Html.JsonScript(id, new PaginatedView
                {
                    Paginated = Paginated,
                    PageActionLink = pageLink
                });
            }

            return MvcHtmlString.Empty;
        }

    }
}
