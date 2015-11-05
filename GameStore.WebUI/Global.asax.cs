using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using GameStore.CL.AutomapperProfiles;
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


            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AutomapperDALProfile());
                cfg.AddProfile(new AutomapperBLLProfile());
                cfg.AddProfile(new AutomapperWebProfile());
            });

            PaymentModeManager.Add(new PaymentMethod("en.wikipedia.org/wiki/Visa_Inc.#/media/File:Visa.svg","Visa", "American multinational financial services"));
            PaymentModeManager.Add(new PaymentMethod("znet.lviv.ua/assets/img/ibox_logo.PNG", "Ibox", "The payment network of Ukraine"));
            PaymentModeManager.Add(new PaymentMethod("www.financemagnates.com/wp-content/uploads/fxmag-experts/2014/08/unicredit.jpg", "Bank", "Remote banking services"));
        }
    }
}