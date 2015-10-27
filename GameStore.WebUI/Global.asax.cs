using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.WebUI.Concrete;
using GameStore.WebUI.Infrastructure;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Models;

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
            
            ModelBinders.Binders.Add(typeof(OrderViewModel),new BusketBinder());

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AutomapperBLLProfile());
                cfg.AddProfile(new AutomapperWebProfile());
            });

            PaymentManager.Add(new PaymentMethod("https://en.wikipedia.org/wiki/Visa_Inc.#/media/File:Visa.svg","Visa", "American multinational financial services", new VisaPayment()));
            PaymentManager.Add(new PaymentMethod("http://znet.lviv.ua/assets/img/ibox_logo.PNG", "IBox", "The payment network of Ukraine", new IBoxPayment()));
            PaymentManager.Add(new PaymentMethod("http://www.financemagnates.com/wp-content/uploads/fxmag-experts/2014/08/unicredit.jpg", "Bank", "Remote banking services", new BankPayment()));
        }
    }
}