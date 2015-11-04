using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.BLInterfaces;
using GameStore.Infrastructure.Enums;

namespace GameStore.BLL.Concrete.ContentSorters
{
    public class GameSorterByPrice : IContentSorter<Game>
    {
        private SortingDirection direction;
        public GameSorterByPrice(SortingDirection direction)
        {
            this.direction = direction;
        }

        public IEnumerable<Game> Sort(IEnumerable<Game> source)
        {
            if (direction == SortingDirection.Ascending)
            {
                return source.OrderBy(m => m.Price);
            }
            return source.OrderByDescending(m => m.Price);
        }
    }
}
