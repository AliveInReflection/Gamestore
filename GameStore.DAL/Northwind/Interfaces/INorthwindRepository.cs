using System.Collections.Generic;

namespace GameStore.DAL.Northwind.Interfaces
{
    public interface INorthwindRepository<TEntity> where TEntity : class
    {
        int Count(IEnumerable<int> idsToExclude);
        IEnumerable<TEntity> GetAll(IEnumerable<int> idsToExclude);
        TEntity Get(int id);
    }
}
