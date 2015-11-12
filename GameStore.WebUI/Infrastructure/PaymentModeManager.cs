using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Infrastructure
{
    public static class PaymentModeManager
    {
        private static Dictionary<string, PaymentMethod> paymentMethods;
        private static Dictionary<string, PaymentMethod> paymentMethodsRu;


        static PaymentModeManager()
        {
            paymentMethods = new Dictionary<string, PaymentMethod>();
            paymentMethodsRu = new Dictionary<string, PaymentMethod>();
            Initialise();
        }

        public static IEnumerable<PaymentMethod> GetAll()
        {
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (culture == "ru")
            {
                return paymentMethodsRu.Select(m => m.Value);
            }
            return paymentMethods.Select(m => m.Value);
        }

        public static PaymentMethod Get(string paymentKey)
        {
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (culture == "ru")
            {
                return paymentMethodsRu[paymentKey];
            }
            return paymentMethods[paymentKey];
        }

        private static void Initialise()
        {
            paymentMethods.Add("Visa", new PaymentMethod("en.wikipedia.org/wiki/Visa_Inc.#/media/File:Visa.svg", "Visa", "American multinational financial services"));
            paymentMethods.Add("Ibox", new PaymentMethod("znet.lviv.ua/assets/img/ibox_logo.PNG", "Ibox", "The payment network of Ukraine"));
            paymentMethods.Add("Bank", new PaymentMethod("www.financemagnates.com/wp-content/uploads/fxmag-experts/2014/08/unicredit.jpg", "Bank", "Remote banking services"));

            paymentMethodsRu.Add("Visa", new PaymentMethod("en.wikipedia.org/wiki/Visa_Inc.#/media/File:Visa.svg", "Visa", "Американские интернациональные платежные услуги"));
            paymentMethodsRu.Add("Ibox", new PaymentMethod("znet.lviv.ua/assets/img/ibox_logo.PNG", "Ibox", "Платежная система Украины"));
            paymentMethodsRu.Add("Банковский платеж", new PaymentMethod("www.financemagnates.com/wp-content/uploads/fxmag-experts/2014/08/unicredit.jpg", "Банковский платеж", "Удаленные банковские платежи"));
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