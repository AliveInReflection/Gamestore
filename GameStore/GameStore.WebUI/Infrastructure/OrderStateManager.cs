using GameStore.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Infrastructure
{
    public static class OrderStateManager
    {
        public static IDictionary<OrderState, string> Items
        {
            get
            {
                return new Dictionary<OrderState, string>()
                {
                    {OrderState.NotIssued, InfrastructureRes.OrderStateNotIssued},
                    {OrderState.NotPayed, InfrastructureRes.OrderStateNotPayed},
                    {OrderState.Payed, InfrastructureRes.OrderStatePayed},
                    {OrderState.Shipped, InfrastructureRes.OrderStateShipped},
                };
            }
        }
    }
}