using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameStore.BLL.Concrete.ContentFilters;
using GameStore.WebUI.App_LocalResources.Localization;
using Microsoft.SqlServer.Server;

namespace GameStore.WebUI.Infrastructure
{
    public static class GamePublishingDateFilteringManager
    {
        private static Dictionary<string, TimeSpan> filters;

        static GamePublishingDateFilteringManager()
        {
            filters = new Dictionary<string, TimeSpan>();
            Initialize();
        }

        public static void Add(string key, TimeSpan filter)
        {
            filters.Add(key,filter);
        }

        public static TimeSpan Get(string key)
        {
            return filters[key];
        }

        public static IEnumerable<string> GetKeys()
        {
            return filters.Keys;
        }

        public static void Initialize()
        {
            filters.Add(InfrastructureRes.PublicationDateWeek, TimeSpan.FromDays(7));
            filters.Add(InfrastructureRes.PublicationDateMonth, TimeSpan.FromDays(30));
            filters.Add(InfrastructureRes.PublicationDateYear, TimeSpan.FromDays(365));
            filters.Add(InfrastructureRes.PublicationDate2Years, TimeSpan.FromDays(730));
            filters.Add(InfrastructureRes.PublicationDate3Years, TimeSpan.FromDays(1095));
            filters.Add(InfrastructureRes.PublicationDateAll, TimeSpan.FromDays(0));
        }

    }
}