using System;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.GameStore.Interfaces
{
    public interface IGameStoreRepository<TEntity> where TEntity : class
    {
        /// <summary>Add new entity to database</summary>
        /// <param name="entity">Entity</param>
        /// <exception>InvalidOperationException</exception>
        void Create(TEntity entity);

        /// <summary>Update entity in database</summary>
        /// <param name="entity">Entity</param>
        /// <exception>InvalidOperationException</exception>
        void Update(TEntity entity);

        /// <summary>Returns entity by specified expression</summary>
        /// <param name="predicate">Expression</param>
        /// <returns>Entity</returns>
        /// <exception>InvalidOperationException</exception>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>Returns list of entities by specified expression</summary>
        /// <param name="predicate">Expression</param>
        /// <returns>List of entities</returns>
        /// <exception>InvalidOperationException</exception>
        IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate);

        /// <summary>Returns all entities from database</summary>
        /// <returns>List of entities</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>Remove entity from database</summary>
        /// <param name="id">Entity</param>
        /// <exception>InvalidOperationException</exception>
        void Delete(int id);
    }
}
