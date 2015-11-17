using System.Collections.Generic;
using System.Threading;
using GameStore.WebUI.App_LocalResources.Localization;

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
                    {"10", 10},
                    {"20", 20},
                    {"50", 50},
                    {"100", 100},
                    {InfrastructureRes.PaginationAll, short.MaxValue}
                };
            }
        }
    }
}