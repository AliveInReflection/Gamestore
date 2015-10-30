using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.BLL.Interfaces.ContentFilters
{
    public interface IContentPaginator<T>
    {
        IEnumerable<T> GetItems(IEnumerable<T> items, int pageNumber, int itemsPerPage);
    }
}
