using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.Interfaces.ContentFilters;
using GameStore.Domain.Entities;

namespace GameStore.BLL.Concrete.ContentSorters
{
    public class GameSorterByDate : IContentSorter<Game>
    {
        public IEnumerable<Game> Sort(IEnumerable<Game> source)
        {
            return source.OrderByDescending(m => m.ReceiptDate);
        }
    }
}
