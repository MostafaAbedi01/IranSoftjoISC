using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.IO;
using System.Web;
using System.Net;
using Mehr.Web.Mvc.JqGrid;
using Mehr.Web.Mvc.JqGrid.ClientModel;
using Mehr.Web.Mvc.Html;
using System.Text.RegularExpressions;

namespace Mehr.Web.Mvc
{
    public static class ControllerExtensions
    {
        public static FileStreamResult CopyStream(this Controller controller, string url, string authCockieName)
        {

            WebClient client = new WebClient();
            if (HttpContext.Current.Request.Cookies.AllKeys.Contains(authCockieName))
            {
                string cockieValue = HttpContext.Current.Request.Cookies[authCockieName].Value;
                if (!string.IsNullOrWhiteSpace(cockieValue))
                    client.Headers.Add("Cookie: " + authCockieName + "=" + cockieValue);
            }

            Stream stream = null;
            try { stream = client.OpenRead(url); }
            catch
            {
                try { stream = client.OpenRead(url); }
                catch { stream = client.OpenRead(url); }
            }

            string contentType = client.ResponseHeaders["Content-Type"];
            if (string.IsNullOrWhiteSpace(contentType))
                contentType = "text/html";
            return new FileStreamResult(stream, contentType)
            {

            };
        }

        public static HtmlHelper GetHtmlHelper(this Controller controller)
        {
            var viewContext = new ViewContext(controller.ControllerContext, new FakeView(), controller.ViewData, controller.TempData, TextWriter.Null);
            return new HtmlHelper(viewContext, new ViewPage());
        }

        public static ActionResult DialogJqGrid(this Controller controller, Grid grid, string id = null , object options = null)
        {
            var html = controller.GetHtmlHelper();

            var isJsonp = !string.IsNullOrEmpty(controller.Request["jsoncallback"]);
            if (isJsonp)
            {
                grid.DataType = GridDataType.Jsonp;
                grid.DataUrl += "?jsoncallback=?";
                grid.DefinitionVersion += "XJ0";

                if (!grid.DataUrl.ToLower().StartsWith("http"))
                    grid.DataUrl = controller.Request.Url.GetLeftPart(UriPartial.Authority) + grid.DataUrl;
            }

            if (id == null)
                id = char.ToLower(grid.GetType().Name[0]) + "grid";
          
            return new ContentJsonpResult()
            {
                Data = html.DialogJqGrid(grid, id,options).ToHtmlString(),
            };
        }

        public static string ViewAsString(this Controller controller, string viewName, object model = null)
        {
            if (string.IsNullOrEmpty(viewName)) return null;

            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);

                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);

                viewResult.View.Render(viewContext, sw);

                var reg = new Regex(@"(?<=[^])\t{2,}|(?<=[>])\s{2,}(?=[<])|(?<=[>])\s{2,11}(?=[<])|(?=[\n])\s{2,}",
                                    RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

                var html = sw.GetStringBuilder().ToString().Replace('\n', ' ').Replace('\r', ' ');
                html = reg.Replace(html, string.Empty);
                return html;
            }
        }

        class FakeView : IView
        {
            public void Render(ViewContext viewContext, TextWriter writer)
            { throw new InvalidOperationException(); }
        }
    }

}
