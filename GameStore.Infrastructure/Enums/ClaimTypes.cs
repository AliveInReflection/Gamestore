using System;

namespace GameStore.Infrastructure.Enums
{
    [Flags]
    public enum ClaimTypes
    {
        Games = 1,
        Comments = 2,
        Genres = 4,
        PlatformTypes = 8,
        Publishers = 16,
        Orders = 32,
        Users = 64
    }
}
