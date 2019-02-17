using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;
using eShopMVC;

namespace IranSoftjo.Common
{
    public class Utils
    {
        public static string CreateThumbnail(string originalFileFullPath, int? width = null, int? height = null, string imageUploadPath=null)
        {
            if (imageUploadPath==null)
            {
                imageUploadPath = "/Uploads/";
            }
            string filename = string.Empty;

            if (File.Exists(originalFileFullPath))
            {
                Image img = Image.FromFile(originalFileFullPath);
                var bmp = new Bitmap(img);
                if (width == null)
                {
                    width = 150;
                }
                if (height == null)
                {
                    height = 150;
                }
                bmp = BitmapManipulator.ThumbnailBitmap(bmp, (int) width, (int) height);

                string thumbfilename = Path.GetFileNameWithoutExtension(originalFileFullPath) + "_Thumb" +
                                       Path.GetExtension(originalFileFullPath);

                string thumbFileRelativePath = imageUploadPath + thumbfilename;

                bmp.Save(HttpContext.Current.Server.MapPath(thumbFileRelativePath), ImageFormat.Jpeg);
                img.Dispose();
                bmp.Dispose();
                filename = thumbFileRelativePath;
            }
            return filename;
        }
    }

    public class MyHttpControllerHandler : HttpControllerHandler, IRequiresSessionState
    {
        public MyHttpControllerHandler(RouteData routeData)
            : base(routeData)
        {
        }
    }

    public class MyHttpControllerRouteHandler : HttpControllerRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new MyHttpControllerHandler(requestContext.RouteData);
        }
    }
}