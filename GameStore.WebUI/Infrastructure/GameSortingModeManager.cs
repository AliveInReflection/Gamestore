using System.Collections.Generic;
using System.Threading;
using GameStore.Infrastructure.Enums;
using GameStore.WebUI.App_LocalResources.Localization;


namespace GameStore.WebUI.Infrastructure
{
    public static class GameSortingModeManager
    {
        public static Dictionary<string, GamesSortingMode> Items
        {
            get
            {
                return new Dictionary<string, GamesSortingMode>()
                {
                    {InfrastructureRes.SortingMostPopular, GamesSortingMode.MostPopular},
                    {InfrastructureRes.SortingMostCommented, GamesSortingMode.MostCommented},
                    {InfrastructureRes.SortingPriceAscending, GamesSortingMode.PriceAscending},
                    {InfrastructureRes.SortingPriceDescending, GamesSortingMode.PriceDescending},
                    {InfrastructureRes.SortingAdditionDate, GamesSortingMode.AdditionDate},
                };
            }
        }
    }
}