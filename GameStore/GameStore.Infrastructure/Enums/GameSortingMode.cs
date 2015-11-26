using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Enums
{
    public enum GamesSortingMode
    {
        None = 0,
        MostPopular = 1,
        MostCommented = 2,
        PriceAscending = 3,
        PriceDescending = 4,
        AdditionDate = 5
    }
}
