using System;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.GameStore.Interfaces
{
    public interface IGameStoreRepository<TEntity> where TEntity : class
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetMany(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetAll();
        void Delete(int id);
    }
}
