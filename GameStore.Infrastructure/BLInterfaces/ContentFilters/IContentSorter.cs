using System.Collections.Generic;

namespace GameStore.Infrastructure.BLInterfaces
{
    public interface IContentSorter<T>
    {
        IEnumerable<T> Sort(IEnumerable<T> source);
    }
}
