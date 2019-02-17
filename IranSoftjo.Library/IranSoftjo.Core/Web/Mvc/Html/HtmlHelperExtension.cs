using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Mehr.Web.Mvc.Html
{
    public static class HtmlHelperExtension
    {
        public static bool ViewExists(this HtmlHelper html, string viewName)
        {
            var controllerContext = html.ViewContext.Controller.ControllerContext;
            ViewEngineResult result = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
            return result.View != null;
        }
    }
}
