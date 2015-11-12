using System.Collections.Generic;
using System.Threading;

namespace GameStore.WebUI.Infrastructure
{
    public static class PagingManager
    {
        private static Dictionary<string, int> pages;
        private static Dictionary<string, int> pagesRu;


        static PagingManager()
        {
            pages = new Dictionary<string, int>();
            pagesRu = new Dictionary<string, int>();
            Initialize();
        }

        public static void Add(string key, int pageCount)
        {
            pages.Add(key, pageCount);
        }

        public static int Get(string key)
        {
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (culture == "ru")
            {
                return pagesRu[key];
            }
            return pages[key];
        }

        public static IEnumerable<string> GetKeys()
        {
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (culture == "ru")
            {
                return pagesRu.Keys;
            }
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
            pages.Add("All", short.MaxValue);

            pagesRu.Add("10", 10);
            pagesRu.Add("20", 20);
            pagesRu.Add("50", 50);
            pagesRu.Add("100", 100);
            pagesRu.Add("5", 5);
            pagesRu.Add("1", 1);
            pagesRu.Add("Все", short.MaxValue);
        }
    }
}