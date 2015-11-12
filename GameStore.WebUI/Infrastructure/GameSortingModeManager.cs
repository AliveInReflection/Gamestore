using System.Collections.Generic;
using System.Threading;
using GameStore.Infrastructure.Enums;


namespace GameStore.WebUI.Infrastructure
{
    public static class GameSortingModeManager
    {
        private static Dictionary<string, GamesSortingMode> sorters;
        private static Dictionary<string, GamesSortingMode> sortersRu;

        static GameSortingModeManager()
        {
            sorters = new Dictionary<string, GamesSortingMode>();
            sortersRu = new Dictionary<string, GamesSortingMode>();
            Initialize();
        }

        public static GamesSortingMode Get(string key)
        {
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (culture == "ru")
            {
                return sortersRu[key];
            }
            return sorters[key];
        }

        public static IEnumerable<string> GetKeys()
        {
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            if (culture == "ru")
            {
                return sortersRu.Keys;
            }
            return sorters.Keys;
        }

        public static void Initialize()
        {
            sorters.Add("Most popular", GamesSortingMode.MostPopular);
            sorters.Add("Most commented", GamesSortingMode.MostCommented);
            sorters.Add("Price ascending", GamesSortingMode.PriceAscending);
            sorters.Add("Price descending", GamesSortingMode.PriceDescending);
            sorters.Add("Addition date", GamesSortingMode.AdditionDate);

            sortersRu.Add("Самые популярные", GamesSortingMode.MostPopular);
            sortersRu.Add("Самые комментируемые", GamesSortingMode.MostCommented);
            sortersRu.Add("Цена по возрастанию", GamesSortingMode.PriceAscending);
            sortersRu.Add("Цена по убыванию", GamesSortingMode.PriceDescending);
            sortersRu.Add("По дате выхода", GamesSortingMode.AdditionDate); 
        }
    }
}