using System.Collections.Generic;
using System.Linq;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.BLInterfaces;

namespace GameStore.BLL.ContentPaginators
{
    public class GamePaginator : IContentPaginator<Game>
    {
        public IEnumerable<Game> GetItems(IEnumerable<Game> items, int pageNumber, int itemsPerPage)
        {
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }

            if (itemsPerPage == 0)
            {
                itemsPerPage = int.MaxValue;
            }
            return items.Skip(itemsPerPage*(pageNumber - 1)).Take(itemsPerPage);
        }
    }
}
