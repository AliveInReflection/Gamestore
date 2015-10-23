using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.BLL.Interfaces;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.Controllers
{
    public class OrderController : Controller
    {
        
        private IGameService gameService;
        

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
