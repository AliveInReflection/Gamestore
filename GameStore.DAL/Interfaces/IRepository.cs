using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        /// <summary>Add new entity to database</summary>
        /// <param name="entity">Entity</param>
        /// <exception>InvalidOperationException</exception>
        void Create(T entity);

        /// <summary>Update entity in database</summary>
        /// <param name="entity">Entity</param>
        /// <exception>InvalidOperationException</exception>
        void Update(T entity);

        /// <summary>Remove entity from database</summary>
        /// <param name="id">Entity</param>
        /// <exception>InvalidOperationException</exception>
        void Delete(int id);

        /// <summary>Returns entity by specified expression</summary>
        /// <param name="predicate">Expression</param>
        /// <returns>Entity</returns>
        /// <exception>InvalidOperationException</exception>
        T Get(Expression<Func<T, bool>> predicate);

        /// <summary>Returns all entities from database</summary>
        /// <returns>List of entities</returns>
        IEnumerable<T> GetAll();

        /// <summary>Returns list of entities by specified expression</summary>
        /// <param name="predicate">Expression</param>
        /// <returns>List of entities</returns>
        /// <exception>InvalidOperationException</exception>
        IEnumerable<T> GetMany(Expression<Func<T, bool>> predicate);

        /// <summary>Count entities in database</summary>
        int Count();

        /// <summary>Check existance of entities by specified expression</summary>
        /// <param name="predicate">Expression</param>
        bool IsExists(Expression<Func<T, bool>> predicate);
    }
}
