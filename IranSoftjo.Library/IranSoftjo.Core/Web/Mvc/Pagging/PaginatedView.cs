using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Mehr.Linq;

namespace Mehr.Web.Mvc
{
    public class PaginatedView : Mehr.Web.Mvc.Pagging.PaginatedView
    {
    }
}

namespace Mehr.Web.Mvc.Pagging
{
    public class PaginatedView
    {
        public IPaginated Paginated { get; set; }
        public string PageActionLink { get; set; }

        public const string PageNumberPlaceHolder = "%7Bpage%7D";

        public string BuildPageActionLink(int page)
        {
            return this.PageActionLink.ToString().Replace(PageNumberPlaceHolder, page.ToString());
        }
    }
}