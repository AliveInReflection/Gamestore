using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Infrastructure
{
    public static class BanDurationManager
    {
        private static Dictionary<string, TimeSpan> durations;

        static BanDurationManager()
        {
            durations = new Dictionary<string, TimeSpan>();
            Initialize();
        }

        public static IEnumerable<string> GetKeys()
        {
            return durations.Keys;
        }

        public static TimeSpan Get(string key)
        {
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
        }
    }
}