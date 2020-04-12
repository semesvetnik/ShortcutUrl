using System.Web.Mvc;
using System.Web.Routing;

namespace ShortCutURL
{
    public class RouteConfig
    {
        /// <summary>
        /// Устанавливает маршрут.
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Отключает обработку запросов для некоторых файлов, например с расширением *.axd (WebResource.axd).
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Определение маршрута по умолчанию.
            // Сопоставляет маршрут запросу.
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{shortUrlValue}",
                defaults: new { controller = "Home", action = "Index", shortUrlValue = UrlParameter.Optional }
            );
        }
    }
}
