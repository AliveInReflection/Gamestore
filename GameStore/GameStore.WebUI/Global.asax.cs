using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using GameStore.Auth;
using GameStore.CL.AutomapperProfiles;
using GameStore.Infrastructure.AuthInterfaces;
using GameStore.WebUI.App_Start;
using GameStore.WebUI.Infrastructure;

namespace GameStore.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AntiForgeryConfig.UniqueClaimTypeIdentifier = System.Security.Claims.ClaimTypes.Name;
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AutomapperDALProfile());
                cfg.AddProfile(new AutomapperBLLProfile());
                cfg.AddProfile(new AutomapperWebProfile());
                cfg.AddProfile(new AutomapperAuthProfile());
            });
        }

        public override void Init()
        {
            base.Init();
            var authModule = new ClaimBasedAuthenticationModule();
            authModule.Init(this);
        }
    }
}