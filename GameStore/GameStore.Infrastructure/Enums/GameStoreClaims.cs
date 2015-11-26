using System;
using System.Collections.Generic;

namespace GameStore.Infrastructure.Enums
{
    public static class GameStoreClaim
    {
        public const string Games = "Games";
        public const string Comments = "Comments";
        public const string Genres = "Genres";
        public const string PlatformTypes = "PlatformTypes";
        public const string Publishers = "Publishers";
        public const string Orders = "Orders";
        public const string Users = "Users";
        public const string Roles = "Roles";

        public static IEnumerable<string> Items
        {
            get
            {
                return new[]
                {
                    Games, Comments, Genres, PlatformTypes,
                    Publishers, Orders, Users, Roles
                };
            }
        }
            
    }
}
