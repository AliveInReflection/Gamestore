using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.DTO;
using GameStore.BLL.Interfaces;
using GameStore.WebUI.Abstract;
using GameStore.WebUI.Infrastructure;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private IOrderService orderService;
        

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public ActionResult Add(string gameKey, short quantity = 1)
        {
            string sessionId = HttpContext.Session.SessionID;
            try
            {
                orderService.AddItem(sessionId, gameKey, quantity);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Error";
            }
            return RedirectToAction("List", "Game");

        }

        [HttpGet]
        public ActionResult Details()
        {
            var busket = orderService.GetCurrent(HttpContext.Session.SessionID);
            return View(Mapper.Map<OrderDTO, OrderViewModel>(busket));
        }

        public ActionResult Make()
        {
            string sessionId = HttpContext.Session.SessionID;            
            var busket = orderService.GetCurrent(sessionId);
            var methods = PaymentManager.GetAll();
            
            var order = new MakeOrderViewModel();
            
            order.Order = Mapper.Map<OrderDTO,OrderViewModel>(busket);
            order.PaymentMethods = Mapper.Map<IEnumerable<PaymentMethod>, IEnumerable<DisplayPaymentMethodViewModel>>(methods);
            order.Amount = orderService.CalculateAmount(busket.OrderId);

            return View(order);
        }

        [HttpGet]
        public ActionResult Pay(string paymentKey)
        {
            try
            {
                string sessionId = HttpContext.Session.SessionID;
                var busket = orderService.GetCurrent(sessionId);

                var payment = PaymentManager.Get(paymentKey);

                var amount = orderService.CalculateAmount(busket.OrderId);

                orderService.Make(busket.OrderId);

                return payment.Payment.Pay(busket.OrderId, amount);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Error";
                return RedirectToAction("Details");
            }
            
        }

    }

}
