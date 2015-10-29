using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Interfaces.ContentFilters;
using GameStore.Domain.Entities;

namespace GameStore.BLL.Concrete.ContentSorters
{
    public class GameSorterByViews : IContentSorter<Game>
    {
        public void Sort(IEnumerable<Game> source)
        {
            throw new NotImplementedException();
        }
    }
}
