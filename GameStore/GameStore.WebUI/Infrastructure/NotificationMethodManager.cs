using GameStore.Infrastructure.Enums;
using GameStore.WebUI.App_LocalResources.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Infrastructure
{
    public static class NotificationMethodManager
    {
        public static Dictionary<string, NotificationMethod> Items
        {
            get
            {
                return new Dictionary<string, NotificationMethod>()
                {
                    {InfrastructureRes.NotificationMethodEmail, NotificationMethod.Email},
                    {InfrastructureRes.NotificationMethodSms, NotificationMethod.Sms},
                    {InfrastructureRes.NotificationMethodMobileApp, NotificationMethod.MobileApp}
                };
            }
        }
    }
}