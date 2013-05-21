using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MP3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Add song to existing playlist",
                url: "Playlist/Add/{songId}/{playlistId}",
                defaults: new { controller = "Playlist", action = "Add" }
            );

            routes.MapRoute(
                name: "Add song to new playlist",
                url: "Playlist/Add/{songId}",
                defaults: new { controller = "Playlist", action = "Add" }
            );

            routes.MapRoute(
                name: "Video",
                url: "Video/{id}/{title}",
                defaults: new { controller = "Category", action = "ListVideo", title = UrlParameter.Optional, id = UrlParameter.Optional, page = 1 }
            );

            routes.MapRoute(
                name: "Video with page",
                url: "Video/{id}/{title}/{page}",
                defaults: new { controller = "Category", action = "ListVideo", title = UrlParameter.Optional, id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Play music",
                url: "Play/{id}/{title}",
                defaults: new { controller = "Song", action = "Details", name = UrlParameter.Optional, id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Music with page",
                url: "Music/{id}/{title}/{page}",
                defaults: new { controller = "Category", action = "ListMusic", title = UrlParameter.Optional, id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Music default page",
                url: "Music/{id}/{title}",
                defaults: new { controller = "Category", action = "ListMusic", id = UrlParameter.Optional, title = UrlParameter.Optional,  page = 1 }
            );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}