using System.Collections.Generic;

namespace GameStore.Infrastructure.BLInterfaces
{
    public interface IContentPaginator<T>
    {
        /// <summary>Returns part of list for custom page</summary>
        /// <param name="items">Source list of entities</param>
        /// <param name="pageNumber">Current page number</param>
        /// <param name="itemsPerPage">Items per page</param>
        /// <returns>List of entities</returns>
        IEnumerable<T> GetItems(IEnumerable<T> items, int pageNumber, int itemsPerPage);
    }
}
