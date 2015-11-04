
using System.Collections.Generic;
using GameStore.Infrastructure.DTO;


namespace GameStore.WebUI.Infrastructure
{
    public static class GameSortingModeManager
    {
        private static Dictionary<string, GamesSortingMode> sorters;

        static GameSortingModeManager()
        {
            sorters = new Dictionary<string, GamesSortingMode>();
            Initialize();
        }

        public static void Add(string key, GamesSortingMode mode)
        {
            sorters.Add(key, mode);
        }

        public static GamesSortingMode Get(string key)
        {
            return sorters[key];
        }

        public static IEnumerable<string> GetKeys()
        {
            return sorters.Keys;
        }

        public static void Initialize()
        {
            sorters.Add("None", GamesSortingMode.None);
            sorters.Add("Most popular", GamesSortingMode.MostPopular);
            sorters.Add("Most commented", GamesSortingMode.MostCommented);
            sorters.Add("Price ascending", GamesSortingMode.PriceAscending);
            sorters.Add("Price descending", GamesSortingMode.PriceDescending);
            sorters.Add("Addition date", GamesSortingMode.AdditionDate);            
        }
    }
}