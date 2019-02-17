using System;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using eShopMvc;
using GSD.Globalization;
using Mehr;
using Mehr.Reflection;
using Mehr.Web;

namespace IranSoftjo.Package.WebUi.Android
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}/{title}", // URL with parameters
                new { action = "Index", id = UrlParameter.Optional, title = "" } // Parameter defaults
            );

            routes.MapRoute(
              "Default", // Route name
              "{controller}/{action}/{tag}", // URL with parameters
              new { action = "Index", tag = UrlParameter.Optional } // Parameter defaults
          );

        }
        protected void Application_Start()
        {
            ServiceLocator.Current = Mehr.ServiceLocator.WebContext;
            ServiceLocator.Current.Set<IEnumMetadataFactory>(new HttpContextEnumMetadataFactory());

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();



        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var persianCulture = new PersianCulture
            {
                DateTimeFormat =
                {
                    ShortDatePattern = "yyyy/MM/dd",
                    LongDatePattern = "dddd d MMMM yyyy",
                    AMDesignator = "صبح",
                    PMDesignator = "عصر"
                }
            };
            Thread.CurrentThread.CurrentCulture = persianCulture;
            Thread.CurrentThread.CurrentUICulture = persianCulture;
        }

        public class HttpContextEnumMetadataFactory : EnumMetadataFactory
        {
            public HttpContextEnumMetadataFactory() : base(new HttpContextCacheProvider()) { }

            public static EnumMetadataFactory Instance = new HttpContextEnumMetadataFactory();
        }

        public override string GetVaryByCustomString(HttpContext ctx, string custom)
        {
            if (custom == "EditDateTime")
            {
                if (HttpContext.Current.Cache["EditDateTime"] == null)
                {
                    HttpContext.Current.Cache["EditDateTime"] = DateTime.Now.ToString("yyyy-MM-dd_HH:mm:ss");
                }
                return HttpContext.Current.Cache["EditDateTime"].ToString();
            }
            return base.GetVaryByCustomString(ctx, custom);
        }
    }
}