using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Mvc;
using AutoMapper;
using GameStore.BLL.Infrastructure;
using GameStore.BLL.Services;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Infrastructure.Enums;
using GameStore.Logger.Interfaces;
using GameStore.WebUI.App_LocalResources.Localization;
using GameStore.WebUI.Filters;
using GameStore.WebUI.Infrastructure;
using GameStore.WebUI.Models;
using GameStore.WebUI.Models.Order;

namespace GameStore.WebUI.Controllers
{
    public class OrderController : BaseController
    {
        private IOrderService orderService;

        public OrderController(IOrderService orderService, IGameStoreLogger logger)
            :base(logger)
        {
            this.orderService = orderService;
        }


        [HttpPost]
        [Claims(GameStoreClaim.Orders, Permissions.Create)]
        [Claims(ClaimTypes.Country, Countries.Ukraine)]
        public ActionResult Add(string gameKey, short quantity = 1)
        {
            int userId = int.Parse((User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.SerialNumber).Value);
            try
            {
                orderService.AddItem(userId, gameKey, quantity);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.NotEnoughUnitsInStock);
            }
            return RedirectToAction("Index", "Game");

        }

        [HttpGet]
        [Claims(GameStoreClaim.Orders, Permissions.Create)]
        [Claims(ClaimTypes.Country, Countries.Ukraine)]
        public ActionResult Details()
        {
            int userId = int.Parse((User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.SerialNumber).Value);
            var basket = orderService.GetCurrent(userId);
            return View(Mapper.Map<OrderDTO, OrderViewModel>(basket));
        }

        [Claims(GameStoreClaim.Orders, Permissions.Create)]
        public ActionResult Make()
        {
            int userId = int.Parse((User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.SerialNumber).Value);            
            var basket = orderService.GetCurrent(userId);
            var methods = PaymentModeManager.Items.Values;
            
            var order = new MakeOrderViewModel();
            
            order.Order = Mapper.Map<OrderDTO,OrderViewModel>(basket);
            order.PaymentMethods = Mapper.Map<IEnumerable<PaymentMethod>, IEnumerable<DisplayPaymentMethodViewModel>>(methods);
            order.Amount = orderService.CalculateAmount(basket.OrderId);

            return View(order);
        }

        [HttpGet]
        [Claims(GameStoreClaim.Orders, Permissions.Create)]
        [Claims(ClaimTypes.Country, Countries.Ukraine)]
        public ActionResult Pay(string paymentKey)
        {
            int userId = int.Parse((User.Identity as ClaimsIdentity).FindFirst(ClaimTypes.SerialNumber).Value);
            var basket = orderService.GetCurrent(userId);
            IPayment payment;
            try
            {                
                payment = orderService.Make(basket.OrderId, paymentKey);                
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                SetErrorMessage(e.Message);
                return RedirectToAction("Details");
            }

            var amount = orderService.CalculateAmount(basket.OrderId);
            var paymentMode = payment.Pay(basket.OrderId, amount);

            switch (paymentMode)
            {
                case PaymentMode.Bank:
                    return File(InvoiceService.GenerateInvoice(basket.OrderId, amount), "application/pdf", "invoice.pdf");
                case PaymentMode.Ibox:
                    return View("IboxPayment", new IboxPaymentViewModel() { InvoiceId = new Random().Next(10000000, 99999999), OrderId = basket.OrderId, Amount = amount });
                case PaymentMode.Visa:
                    return RedirectToAction("PayCard", "Payment", new{orderId = basket.OrderId});
                default:
                    return HttpNotFound();

            }            
        }

        public ActionResult GetShortHistory()
        {
            try
            {
                var orders = orderService.Get(DateTime.UtcNow - TimeSpan.FromDays(30), DateTime.UtcNow);
                return View(Mapper.Map<IEnumerable<OrderDTO>, IEnumerable<DisplayOrderViewModel>>(orders));
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.ValidationError);
            }
            return RedirectToAction("Index", "Game");

        }

        public ActionResult ChangeState(int orderId)
        {
            try
            {
                orderService.ChangeState(orderId, OrderState.Shipped);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.ValidationError);
            }
            return RedirectToAction("GetShortHistory");
        }

        [ActionName("History")]
        [Claims(GameStoreClaim.Orders, Permissions.Retreive)]
        public ActionResult GetHistory()
        {
            return View(new List<DisplayOrderViewModel>());
        }

        [ActionName("History")]
        [HttpPost]
        [Claims(GameStoreClaim.Orders, Permissions.Retreive)]
        public ActionResult GetHistory(DateTime dateFrom, DateTime dateTo)
        {
            var orders = orderService.Get(dateFrom, dateTo);
            return View(Mapper.Map<IEnumerable<OrderDTO>, IEnumerable<DisplayOrderViewModel>>(orders));
        }

    }

}
