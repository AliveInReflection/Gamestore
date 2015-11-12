using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameStore.WebUI.App_LocalResources.Localization;

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
            durations.Add(InfrastructureRes.BanHour, TimeSpan.FromHours(1));
            durations.Add(InfrastructureRes.BanDay, TimeSpan.FromDays(1));
            durations.Add(InfrastructureRes.BanWeek, TimeSpan.FromDays(7));
            durations.Add(InfrastructureRes.BanMonth, TimeSpan.FromDays(30));
            durations.Add(InfrastructureRes.BanPermanent, TimeSpan.MaxValue);
        }
    }
}