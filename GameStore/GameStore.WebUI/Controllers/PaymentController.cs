using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using GameStore.WebUI.Infrastructure;
using GameStore.WebUI.Models;
using GameStore.WebUI.Models.Payment;
using GameStore.Infrastructure.BLInterfaces.Services;
using GameStore.Infrastructure.DTO;
using GameStore.Infrastructure.Enums;
using GameStore.Logger.Interfaces;
using GameStore.WebUI.App_LocalResources.Localization;
using GameStore.BLL.Infrastructure;
using GameStore.Infrastructure.BLInterfaces;

namespace GameStore.WebUI.Controllers
{
    public class PaymentController : BaseController
    {
        private ICreditCardPaymentService creditCardService;
        private IOrderService orderService;

        public PaymentController(IGameStoreLogger logger, ICreditCardPaymentService creditCardService, IOrderService orderService)
            : base(logger)
        {
            this.creditCardService = creditCardService;
            this.orderService = orderService;
        }

        public ActionResult PayCard(int orderId)
        {
            var model = new CardPaymentInfoViewModel()
            {
                CardTypeList = Mapper.Map<IEnumerable<SelectListItem>>(CardTypeManager.Items.Keys),
                OrderId = orderId
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult PayCard(CardPaymentInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CardTypeList = Mapper.Map<IEnumerable<SelectListItem>>(CardTypeManager.Items.Keys);
                return View(model);
            }

            try
            {
                var info = Mapper.Map<CardPaymentInfoDTO>(model);
                var result = creditCardService.Pay(info);

                switch (result)
                {
                    case CardPaymentStatus.ConfirmationRequired:
                        return RedirectToAction("ConfirmCard", new { cardNumber = model.CardNumber, orderId = model.OrderId });
                    case CardPaymentStatus.NotEnoughMoney:
                        ModelState.AddModelError("CardNumber", MessageRes.NotEnoughMoney);
                        break;
                    case CardPaymentStatus.NotExistedCard:
                        ModelState.AddModelError("CardNumber", MessageRes.NotExistedCard);
                        break;
                    default:
                        ModelState.AddModelError("CardNumber", MessageRes.PaymentFailed);
                        break;
                }
                model.CardTypeList = Mapper.Map<IEnumerable<SelectListItem>>(CardTypeManager.Items.Keys);
                return View(model);
            }
            catch (ValidationException e)
            {
                Logger.Warn(e);
                SetErrorMessage(ValidationRes.ValidationError);
                return RedirectToAction("Index", "Game");
            }
        }

        public ActionResult ConfirmCard(string cardNumber, int orderId)
        {
            var model = new CardPaymentConfirmationViewModel()
            {
                CardNumber = cardNumber,
                OrderId = orderId
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ConfirmCard(CardPaymentConfirmationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = creditCardService.Confirm(model.CardNumber, model.ConfirmationCode);

            switch (result)
            {
                case CardConfirmationStatus.Failed:
                    ModelState.AddModelError("ConfirmationCode", MessageRes.WrongConfirmationCode);
                    return View(model);
                case CardConfirmationStatus.Aborted:
                    SetInfoMessage(MessageRes.AttemptsExceeded);
                    break;
                case CardConfirmationStatus.Successfull:
                    SetInfoMessage(MessageRes.ConfirmationSuccessfull);
                    orderService.ChangeState(model.OrderId, OrderState.Payed);
                    break;
                default:
                    SetErrorMessage(MessageRes.ConfirmationError);
                    break;;
            }
            return RedirectToAction("Index", "Game");
        }

    }
}
