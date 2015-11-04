using System.Collections.Generic;
using System.Linq;

namespace GameStore.WebUI.Infrastructure
{
    public static class PaymentModeManager
    {
        private static Dictionary<string, PaymentMethod> paymentMethods;

        static PaymentModeManager()
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