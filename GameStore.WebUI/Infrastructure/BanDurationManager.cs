using System;
using System.Collections.Generic;
using System.Threading;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Infrastructure
{
    public static class BanDurationManager
    {
        public static Dictionary<string, TimeSpan> Items
        {
            get
            {
                return new Dictionary<string, TimeSpan>()
                {
                    {InfrastructureRes.BanHour, TimeSpan.FromHours(1)},
                    {InfrastructureRes.BanDay, TimeSpan.FromDays(1)},
                    {InfrastructureRes.BanWeek, TimeSpan.FromDays(7)},
                    {InfrastructureRes.BanMonth, TimeSpan.FromDays(30)},
                    {InfrastructureRes.BanPermanent, TimeSpan.MaxValue}
                };
            }
        }
    }
}