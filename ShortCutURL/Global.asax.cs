﻿using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;

namespace ShortCutURL
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
