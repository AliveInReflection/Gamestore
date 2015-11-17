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
                        new PaymentMethod("en.wikipedia.org/wiki/Visa_Inc.#/media/File:Visa.svg", InfrastructureRes.PaymentVisa,
                            InfrastructureRes.PaymentVisaDescription)
                    },
                    {
                        InfrastructureRes.PaymentIbox,
                        new PaymentMethod("znet.lviv.ua/assets/img/ibox_logo.PNG", InfrastructureRes.PaymentIbox,
                            InfrastructureRes.PaymentIboxDescription)
                    },
                    {
                        InfrastructureRes.PaymentBank,
                        new PaymentMethod(
                            "www.financemagnates.com/wp-content/uploads/fxmag-experts/2014/08/unicredit.jpg", InfrastructureRes.PaymentBank,
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