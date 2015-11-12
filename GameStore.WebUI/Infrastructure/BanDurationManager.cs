using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Infrastructure
{
    public static class BanDurationManager
    {
        private static Dictionary<string, TimeSpan> durations;
        private static Dictionary<string, TimeSpan> durationsRu;

        static BanDurationManager()
        {
            durations = new Dictionary<string, TimeSpan>();
            durationsRu = new Dictionary<string, TimeSpan>();
            Initialize();
        }

        public static IEnumerable<string> GetKeys()
        {
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (culture == "ru")
            {
                return durationsRu.Keys;
            }
            return durations.Keys;
        }

        public static TimeSpan Get(string key)
        {
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (culture == "ru")
            {
                return durationsRu[key];
            }
            return durations[key];
        }

        public static void Add(string key, TimeSpan duration)
        {
            durations.Add(key, duration);
        }

        public static void Initialize()
        {
            durations.Add("1 hour", TimeSpan.FromHours(1));
            durations.Add("1 day", TimeSpan.FromDays(1));
            durations.Add("1 week", TimeSpan.FromDays(7));
            durations.Add("1 month", TimeSpan.FromDays(30));
            durations.Add("Permanent", TimeSpan.MaxValue);

            durationsRu.Add("1 час", TimeSpan.FromHours(1));
            durationsRu.Add("1 день", TimeSpan.FromDays(1));
            durationsRu.Add("1 неделя", TimeSpan.FromDays(7));
            durationsRu.Add("1 месяц", TimeSpan.FromDays(30));
            durationsRu.Add("Навсегда", TimeSpan.MaxValue);
        }
    }
}