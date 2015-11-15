using System.Collections.Generic;

namespace GameStore.Infrastructure.BLInterfaces
{
    public interface IContentSorter<T>
    {
        /// <summary>Returns sorted list of received entities</summary>
        /// <param name="source">Source list of entities</param>
        /// <returns>List of entities</returns>
        IEnumerable<T> Sort(IEnumerable<T> source);
    }
}
