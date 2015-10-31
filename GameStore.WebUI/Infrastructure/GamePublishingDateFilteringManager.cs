using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameStore.BLL.Concrete.ContentFilters;
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
            filters.Add("Last week", TimeSpan.FromDays(7));
            filters.Add("Last month", TimeSpan.FromDays(30));
            filters.Add("Last year", TimeSpan.FromDays(365));
            filters.Add("Last 2 years", TimeSpan.FromDays(730));
            filters.Add("Last 3 years", TimeSpan.FromDays(1095));
            filters.Add("All", TimeSpan.FromDays(0));
        }

    }
}