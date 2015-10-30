using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Interfaces.ContentFilters;
using GameStore.Domain.Entities;

namespace GameStore.BLL.Concrete.ContentPaginators
{
    public class GamePaginator : IContentPaginator<Game>
    {
        public IEnumerable<Game> GetItems(IEnumerable<Game> items, int pageNumber, int itemsPerPage)
        {
            return items.Skip(itemsPerPage*(pageNumber - 1)).Take(itemsPerPage);
        }
    }
}
