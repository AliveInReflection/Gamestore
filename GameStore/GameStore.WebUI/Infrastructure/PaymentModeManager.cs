using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Infrastructure
{
    public static class PaymentModeManager
    {
        public static Dictionary<string, PaymentMethod> Items
        {
            get
            {
                return new Dictionary<string, PaymentMethod>()
                {
                    {
                        InfrastructureRes.PaymentVisa,
                        new PaymentMethod(@"https://upload.wikimedia.org/wikipedia/commons/thumb/0/0c/MasterCard_logo.png/320px-MasterCard_logo.png", InfrastructureRes.PaymentVisa,
                            InfrastructureRes.PaymentVisaDescription)
                    },
                    {
                        InfrastructureRes.PaymentIbox,
                        new PaymentMethod(@"http://www.uic.in.ua/wp-content/uploads/2014/06/ibox.png", InfrastructureRes.PaymentIbox,
                            InfrastructureRes.PaymentIboxDescription)
                    },
                    {
                        InfrastructureRes.PaymentBank,
                        new PaymentMethod(
                            @"https://lh5.ggpht.com/N7HIRYjD9L-aPe-RCIknkN3_kXgBhqALaelv83sFD70hsZHbL99Zr4T20AQcmkRw4Q=w300-rw", InfrastructureRes.PaymentBank,
                            InfrastructureRes.PaymentBankDescription)
                    }
                };
            }
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