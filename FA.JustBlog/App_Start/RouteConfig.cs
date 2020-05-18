using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FA.JustBlog
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Post",
                "Post/{year}/{month}/{title}",
                new { controller = "Post", action = "DetailPost" },
                new { year = @"\d{4}", month = @"\d{2}" }
                );

            routes.MapRoute(
                "Category",
                "Category/{category}",
                new { controller = "Post", action = "ListPostByCategory" }
                );

            routes.MapRoute(
                "Tag",
                "Tag/{tag}",
                new { controller = "Post", action = "GetPostsByTag" }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Post", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
