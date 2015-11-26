using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameStore.Infrastructure.Enums;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Infrastructure
{
    public class CardTypeManager
    {
        public static Dictionary<string, CardType> Items
        {
            get
            {
                return new Dictionary<string, CardType>()
                {
                    {InfrastructureRes.CardTypeVisa, CardType.Visa},
                    {InfrastructureRes.CardTypeMasterCard, CardType.MasterCard}
                };
            }
        }
    }
}