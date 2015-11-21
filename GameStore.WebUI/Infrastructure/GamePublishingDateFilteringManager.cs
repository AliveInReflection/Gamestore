using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using GameStore.BLL.Concrete.ContentFilters;
using GameStore.WebUI.App_LocalResources.Localization;
using Microsoft.SqlServer.Server;

namespace GameStore.WebUI.Infrastructure
{
    public static class GamePublishingDateFilteringManager
    {
        public static IDictionary<string, TimeSpan> Items
        {
            get
            {
                return new Dictionary<string, TimeSpan>
                {
                    {InfrastructureRes.PublicationDateWeek, TimeSpan.FromDays(7)},
                    {InfrastructureRes.PublicationDateMonth, TimeSpan.FromDays(30)},
                    {InfrastructureRes.PublicationDateYear, TimeSpan.FromDays(365)},
                    {InfrastructureRes.PublicationDate2Years, TimeSpan.FromDays(730)},
                    {InfrastructureRes.PublicationDate3Years, TimeSpan.FromDays(1095)},
                    {InfrastructureRes.PublicationDateAll, TimeSpan.FromDays(0)}
                };
            }
        }

    }
}