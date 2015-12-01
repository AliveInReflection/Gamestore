using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Validation.Providers;

namespace GameStore.WebUI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "CommentsApi",
                routeTemplate: "api/{lang}/games/{gameId}/comments/{id}",
                defaults: new { controller = "Comments", id = RouteParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            config.Routes.MapHttpRoute(
                name: "GameGenresApi",
                routeTemplate: "api/{lang}/games/{id}/genres",
                defaults: new { controller = "GameGenres", id = RouteParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" }
            );
            config.Routes.MapHttpRoute(
                name: "GenreGamesApi",
                routeTemplate: "api/{lang}/genres/{id}/games",
                defaults: new { controller = "GenreGames", id = RouteParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" }
            );


            config.Routes.MapHttpRoute(
                name: "PublisherGamesApi",
                routeTemplate: "api/{lang}/publisher/{id}/games",
                defaults: new { controller = "PublisherGames", id = RouteParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{lang}/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional, lang = "en" },
                constraints: new { lang = @"ru|en" }
            );


            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.MediaTypeMappings.Add(
            new QueryStringMapping("type", "xml", new MediaTypeHeaderValue("application/xml")));

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(
            new QueryStringMapping("type", "json", new MediaTypeHeaderValue("application/json")));

            GlobalConfiguration.Configuration.Services.RemoveAll(
            typeof(System.Web.Http.Validation.ModelValidatorProvider),
            v => v is InvalidModelValidatorProvider);
        }
    }
}
