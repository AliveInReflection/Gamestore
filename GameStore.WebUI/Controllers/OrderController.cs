using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Interfaces;
using GameStore.WebUI.Abstract;
using GameStore.WebUI.Infrastructure;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.Controllers
{
    public class OrderController : Controller
    {
        
        private IGameService gameService;
        private IPayment paymentContext;
        

        public OrderController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        public ActionResult Add(string gameKey,  OrderViewModel busket, short quantity = 1)
        {
            try
            {
                var game = gameService.Get(gameKey);                
                var currentOrderDetails = busket.OrderDetailses.FirstOrDefault(m => m.GameKey.Equals(gameKey));
                if (currentOrderDetails == null)
                {
                    busket.OrderDetailses.Add(new OrderDetailsViewModel()
                    {
                        GameKey = gameKey,
                        Quantity = quantity
                    });
                }
                else
                {
                    currentOrderDetails.Quantity += quantity;
                }
                HttpContext.Session[BusketBinder.BusketKey] = busket;
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Error";
            }
            return RedirectToAction("List", "Game");

        }

        [HttpGet]
        public ActionResult Details(OrderViewModel busket)
        {
            return View(busket);
        }

        public ActionResult Make()
        {
            OrderViewModel busket = Session["Busket"] as OrderViewModel;
            var methods = PaymentManager.GetAll();
            var order = new MakeOrderViewModel();
            order.Order = busket;
            order.PaymentMethods =
                Mapper.Map<IEnumerable<PaymentMethod>, IEnumerable<DisplayPaymentMethodViewModel>>(methods);
            return View(order);
        }

        [HttpGet]
        public ActionResult Pay(string paymentKey)
        {
            try
            {

                var payment = PaymentManager.Get(paymentKey);
                return payment.Payment.Pay(1, 200m);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Error";
                return RedirectToAction("Details");
            }
            
        }

    }

    public class BusketBinder : IModelBinder
    {
        public static string BusketKey {get { return "Busket"; }} 

        public object BindModel(ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            var order = controllerContext.HttpContext.Session[BusketKey] as OrderViewModel;
            if (order == null)
            {
                order = new OrderViewModel();
                order.OrderDetailses = new List<OrderDetailsViewModel>();
                controllerContext.HttpContext.Session[BusketKey] = order;
            }
            return order;
        }
    }
}
