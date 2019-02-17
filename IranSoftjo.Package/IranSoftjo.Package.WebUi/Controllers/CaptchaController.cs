using System;
using System.Drawing.Imaging;
using System.Web.Mvc;
using IranSoftjo.Core.Web.Mvc.Html;
using Mehr.Web.Mvc;

namespace IranSoftjo.WebUi.Controllers
{
    [OutputCache(CacheProfile = "disabled")]
    public partial class CaptchaController : Controller
    {
        public const string RefreshImageRelativeUrl = "http://www.iransoftjo.com/Images/Common/refresh.png";

        public virtual ActionResult Image(string id)
        {
            CaptchaImage ci = CaptchaImage.GetCachedCaptcha(id);

            if (String.IsNullOrEmpty(id) || ci == null)
            {
                return null;
            }

            // write the image to the HTTP output stream as an array of bytes
            return new ImageResult()
            {
                Image = ci.RenderImage(),
                ImageFormat = ImageFormat.Gif
            };
        }

        public virtual ActionResult Renew()
        {
            return Content(InputExtensions.CaptchaImageHtml(50, 180, RefreshImageRelativeUrl));
        }

    }
}
