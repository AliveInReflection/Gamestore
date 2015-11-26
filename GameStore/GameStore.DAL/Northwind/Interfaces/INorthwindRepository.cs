using System.Collections.Generic;

namespace GameStore.DAL.Northwind.Interfaces
{
    public interface INorthwindRepository<TEntity> where TEntity : class
    {
        /// <summary>Count entities in database, except entities with received ids</summary>
        int Count(IEnumerable<int> idsToExclude);

        /// <summary>Returns all entities from database, except entities with received ids</summary>
        /// <returns>List of entities</returns>
        IEnumerable<TEntity> GetAll(IEnumerable<int> idsToExclude);

        /// <summary>Returns entity by specified id</summary>
        /// <param name="id">entity id</param>
        /// <returns>Entity</returns>
        /// <exception>InvalidOperationException</exception>
        TEntity Get(int id);
    }
}
