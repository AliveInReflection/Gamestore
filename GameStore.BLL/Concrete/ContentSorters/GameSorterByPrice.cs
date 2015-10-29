using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Interfaces.ContentFilters;
using GameStore.Domain.Entities;

namespace GameStore.BLL.Concrete.ContentSorters
{
    public class GameSorterByPrice : IContentSorter<Game>
    {
        private SortingDirection direction;
        public GameSorterByPrice(SortingDirection direction)
        {
            this.direction = direction;
        }

        public void Sort(IEnumerable<Game> source)
        {
            throw new NotImplementedException();
        }
    }
}
