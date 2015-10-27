using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameStore.WebUI.Abstract;

namespace GameStore.WebUI.Infrastructure
{
    public static class PaymentManager
    {
        private static Dictionary<string, PaymentMethod> paymentMethods;

        static PaymentManager()
        {
            paymentMethods = new Dictionary<string, PaymentMethod>();
        }

        public static void Add(PaymentMethod paymentMethod)
        {
            paymentMethods.Add(paymentMethod.PaymentKey,paymentMethod);
        }

        public static IEnumerable<PaymentMethod> GetAll()
        {
            return paymentMethods.Select(m => m.Value);
        }

        public static PaymentMethod Get(string paymentKey)
        {
            return paymentMethods[paymentKey];
        }
    }

    public class PaymentMethod
    {
        public PaymentMethod(string pictureURL, string paymentKey, string description, IPayment payment)
        {
            PictureUrl = pictureURL;
            PaymentKey = paymentKey;
            Description = description;
            Payment = payment;
        }
        public string PictureUrl { get; private set; }
        public string PaymentKey { get; private set; }
        public string Description { get; private set; }
        public IPayment Payment { get; set; }
    }
}