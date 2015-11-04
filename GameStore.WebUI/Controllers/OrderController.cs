using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Logger.Interfaces;
using GameStore.WebUI.Infrastructure;
using GameStore.WebUI.Models;

namespace GameStore.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private IOrderService orderService;
        private IGameStoreLogger logger;

        public OrderController(IOrderService orderService, IGameStoreLogger logger)
        {
            this.orderService = orderService;
            this.logger = logger;
        }


        [HttpPost]
        public ActionResult Add(string gameKey, short quantity = 1)
        {
            string sessionId = HttpContext.Session.SessionID;
            try
            {
                orderService.AddItem(sessionId, gameKey, quantity);
            }
            catch (ValidationException e)
            {
                logger.Warn(e);
                TempData["ErrorMessage"] = "Validation error";
            }
            return RedirectToAction("Index", "Game");

        }

        [HttpGet]
        public ActionResult Details()
        {
            var basket = orderService.GetCurrent(HttpContext.Session.SessionID);
            return View(Mapper.Map<OrderDTO, OrderViewModel>(basket));
        }

        public ActionResult Make()
        {
            string sessionId = HttpContext.Session.SessionID;            
            var basket = orderService.GetCurrent(sessionId);
            var methods = PaymentManager.GetAll();
            
            var order = new MakeOrderViewModel();
            
            order.Order = Mapper.Map<OrderDTO,OrderViewModel>(basket);
            order.PaymentMethods = Mapper.Map<IEnumerable<PaymentMethod>, IEnumerable<DisplayPaymentMethodViewModel>>(methods);
            order.Amount = orderService.CalculateAmount(basket.OrderId);

            return View(order);
        }

        [HttpGet]
        public ActionResult Pay(string paymentKey)
        {
            try
            {
                string sessionId = HttpContext.Session.SessionID;
                var basket = orderService.GetCurrent(sessionId);

                var payment = PaymentManager.Get(paymentKey);

                var amount = orderService.CalculateAmount(basket.OrderId);

                orderService.Make(basket.OrderId);

                return payment.Payment.Pay(basket.OrderId, amount);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = "Error";
                return RedirectToAction("Details");
            }
            
        }

    }

}
