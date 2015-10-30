using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Infrastructure
{
    public static class PagingManager
    {
        private static Dictionary<string, int> pages;

        static PagingManager()
        {
            pages = new Dictionary<string, int>();
            Initialize();
        }

        public static void Add(string key, int pageCount)
        {
            pages.Add(key, pageCount);
        }

        public static int Get(string key)
        {
            return pages[key];
        }

        public static IEnumerable<string> GetKeys()
        {
            return pages.Keys;
        }

        public static void Initialize()
        {
            pages.Add("10", 10);
            pages.Add("20", 20);
            pages.Add("50", 50);
            pages.Add("100", 100);
            pages.Add("5", 5);
            pages.Add("1", 1);
            pages.Add("All", 0);
        }
    }
}