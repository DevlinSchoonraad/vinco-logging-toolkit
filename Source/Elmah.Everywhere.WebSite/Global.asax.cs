﻿using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace Elmah.Everywhere
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            routes.IgnoreRoute("{*allaspx}", new {allaspx=@".*\.aspx(/.*)?"});
            routes.IgnoreRoute("{*robotstxt}", new { robotstxt = @"(.*/)?robots.txt(/.*)?" });
            
            routes.MapRoute(
                "Elmah", // Route name
                "{controller}/{action}/{type}", // URL with parameters
                new { controller = "Elmah", action = "Index", type = UrlParameter.Optional }, // Parameter defaults
                null, // Constraints
                new[] { "Elmah.Everywhere.Controllers" }); // Namespaces
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
        }
    }
}
