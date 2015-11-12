using System.Collections.Generic;
using System.Linq;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Infrastructure
{
    public static class PaymentModeManager
    {
        private static Dictionary<string, PaymentMethod> paymentMethods;

        static PaymentModeManager()
        {
            paymentMethods = new Dictionary<string, PaymentMethod>();
            Initialise();
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

        private static void Initialise()
        {
            Add(new PaymentMethod("en.wikipedia.org/wiki/Visa_Inc.#/media/File:Visa.svg", "Visa", InfrastructureRes.PaymentVisaDescription));
            Add(new PaymentMethod("znet.lviv.ua/assets/img/ibox_logo.PNG", "Ibox", InfrastructureRes.PaymentIboxDescription));
            Add(new PaymentMethod("www.financemagnates.com/wp-content/uploads/fxmag-experts/2014/08/unicredit.jpg", InfrastructureRes.PaymentBank, InfrastructureRes.PaymentBankDescription));
        }
    }

    public class PaymentMethod
    {
        public PaymentMethod(string pictureURL, string paymentKey, string description)
        {
            PictureUrl = pictureURL;
            PaymentKey = paymentKey;
            Description = description;
        }
        public string PictureUrl { get; private set; }
        public string PaymentKey { get; private set; }
        public string Description { get; private set; }
    }
}