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
                "Explorer1Route",
                "explore",
                new { controller = "App", action = "Explore2" }
            );

            routes.MapRoute(
                "Explorer2Route",
                "explore/1",
                new { controller = "App", action = "Explore1" }
            );

            routes.MapRoute(
                "ExplorerRoute",
                "explore/2",
                new { controller = "App", action = "Explore2" }
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
                "api/coords/{id}",
                new { controller = "API", action = "GetCoords", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "CoordinateTest2Route",
                "api/coordsbydate/{id}",
                new { controller = "API", action = "GetCoordsByDate", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "CoordinateTest3Route",
                "api/planets",
                new { controller = "API", action = "GetPlanets", id = UrlParameter.Optional }
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