using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Services;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Infrastructure.Enums;
using GameStore.Logger.Interfaces;
using GameStore.WebUI.Infrastructure;
using GameStore.WebUI.Models;
using GameStore.WebUI.Models.Order;

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
                TempData["ErrorMessage"] = "Not enough units in stock";
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
            var methods = PaymentModeManager.GetAll();
            
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

                var payment = orderService.Make(basket.OrderId, paymentKey);
                var amount = orderService.CalculateAmount(basket.OrderId);
                var paymentMode = payment.Pay(basket.OrderId, amount);

                switch (paymentMode)
                {
                    case PaymentMode.Bank:
                        return File(InvoiceService.GenerateInvoice(basket.OrderId, amount),"application/pdf","invoice.pdf");
                    case PaymentMode.Ibox:
                        return View("IboxPayment", new IboxPaymentViewModel(){InvoiceId = new Random().Next(10000000,99999999), OrderId = basket.OrderId, Amount = amount});
                    case PaymentMode.Visa:
                        return View("VisaPayment");
                    default:
                        return HttpNotFound();

                }
            }
            catch (ValidationException e)
            {
                logger.Warn(e);
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Details");
            }
            
        }

        [ActionName("History")]
        public ActionResult GetHistory()
        {
            return View(new List<DisplayOrderViewModel>());
        }

        [ActionName("History")]
        [HttpPost]
        public ActionResult GetHistory(DateTime dateFrom, DateTime dateTo)
        {
            var orders = orderService.Get(dateFrom, dateTo);
            return View(Mapper.Map<IEnumerable<OrderDTO>, IEnumerable<DisplayOrderViewModel>>(orders));
        }

    }

}
