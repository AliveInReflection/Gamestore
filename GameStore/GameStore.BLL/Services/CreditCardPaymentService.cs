using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.CreditCardService;
using GameStore.BLL.Infrastructure;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.DTO;
using GameStore.Infrastructure.Enums;

namespace GameStore.BLL.Services
{
    public class CreditCardPaymentService : GameStore.Infrastructure.BLInterfaces.Services.ICreditCardPaymentService
    {
        private ICreditCardService paymentService;
        private IOrderService orderService;

        public CreditCardPaymentService(IOrderService orderService)
        {
            this.paymentService = new CreditCardServiceClient();
            this.orderService = orderService;
        }

        public CardPaymentStatus Pay(CardPaymentInfoDTO info)
        {
            Mapper.CreateMap<CardPaymentInfoDTO, PaymentInfo>();
            var infoToSend = Mapper.Map<PaymentInfo>(info);

            infoToSend.Purpose = BLLConstants.CardPaymentPurpose;
            infoToSend.Receiver = BLLConstants.CardPaymentReceiver;
            infoToSend.Amount = orderService.CalculateAmount(info.OrderId);

            switch (info.CardType)
            {
                case CardType.Visa:
                    return (CardPaymentStatus)paymentService.PayVisa(infoToSend);
                case CardType.MasterCard:
                    return (CardPaymentStatus)paymentService.PayMasterCard(infoToSend);
                default: 
                    throw new ValidationException(string.Format("Wrong card type({0})", info.CardType));
            }

        }

        public CardConfirmationStatus Confirm(string cardNumber, string confirmationCode)
        {
            return (CardConfirmationStatus)paymentService.Confirm(cardNumber, confirmationCode);
        }
    }
}
