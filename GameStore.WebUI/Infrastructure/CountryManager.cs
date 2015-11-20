using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameStore.Infrastructure.Enums;
using GameStore.WebUI.App_LocalResources.Localization;

namespace GameStore.WebUI.Infrastructure
{
    public static class CountryManager
    {
        public static IDictionary<string, string> Items
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {InfrastructureRes.CountryUkraine, Countries.Ukraine},
                    {InfrastructureRes.CountryRussia, Countries.Russia},
                    {InfrastructureRes.CountryUsa, Countries.Usa},
                    {InfrastructureRes.CountryGreatBritain, Countries.GB}
                };
            }
        }
    }
}