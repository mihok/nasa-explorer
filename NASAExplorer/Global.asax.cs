using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NASAExplorer
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "ExplorerRoute",
                "explore",
                new { controller = "App", action = "Explore" }
            );

            routes.MapRoute(
                "AboutRoute",
                "about",
                new { controller = "Web", action = "About" }
            );

            routes.MapRoute(
                "ContactRoute",
                "contact",
                new { controller = "Web", action = "Contact" }
            );

            routes.MapRoute(
                "CoordinateTest1Route",
                "coords/{id}",
                new { controller = "API", action = "GetCoords", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "CoordinateTest2Route",
                "coordsbydate/{id}",
                new { controller = "API", action = "GetCoordsByDate", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Web", action = "Default", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}