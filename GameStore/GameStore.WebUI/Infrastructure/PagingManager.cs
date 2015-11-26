using GameStore.WebUI.App_LocalResources.Localization;
using System.Collections.Generic;
using System.Threading;

namespace GameStore.WebUI.Infrastructure
{

    public static class PagingManager
    {
        public static Dictionary<string, int> Items
        {
            get
            {
                return new Dictionary<string, int>()
                {
                    {InfrastructureRes.Pagination_10, 10},
                    {InfrastructureRes.Pagination_20, 20},
                    {InfrastructureRes.Pagination_50, 50},
                    {InfrastructureRes.Pagination_100, 100},
                    {InfrastructureRes.PaginationAll, short.MaxValue}
                };
            }
        }
    }
}
