using System.Collections.Generic;

namespace GameStore.Infrastructure.BLInterfaces
{
    public interface IContentPaginator<T>
    {
        IEnumerable<T> GetItems(IEnumerable<T> items, int pageNumber, int itemsPerPage);
    }
}
