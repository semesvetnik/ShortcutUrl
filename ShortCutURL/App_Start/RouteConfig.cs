using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShortCutURL
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            /*
            routes.MapRoute(
                "Index",
                "Home/{url}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index ",
                    url = UrlParameter.Optional
                }
            );*/
        }
    }
}
