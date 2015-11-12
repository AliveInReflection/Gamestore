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
        private static Dictionary<string, TimeSpan> filters;
        private static Dictionary<string, TimeSpan> filtersRu;

        static GamePublishingDateFilteringManager()
        {
            filters = new Dictionary<string, TimeSpan>();
            filtersRu = new Dictionary<string, TimeSpan>();
            Initialize();
        }

        
        public static TimeSpan Get(string key)
        {
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (culture == "ru")
            {
                return filtersRu[key];
            }
            return filters[key];
        }

        public static IEnumerable<string> GetKeys()
        {
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (culture == "ru")
            {
                return filtersRu.Keys;
            }
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

            filtersRu.Add("Последняя неделя", TimeSpan.FromDays(7));
            filtersRu.Add("Последний месяц", TimeSpan.FromDays(30));
            filtersRu.Add("Последний год", TimeSpan.FromDays(365));
            filtersRu.Add("Последние 2 года", TimeSpan.FromDays(730));
            filtersRu.Add("Последние 3 года", TimeSpan.FromDays(1095));
            filtersRu.Add("Все", TimeSpan.FromDays(0));
        }

    }
}