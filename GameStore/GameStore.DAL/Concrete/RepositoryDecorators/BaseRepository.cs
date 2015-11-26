using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.GameStore.Interfaces;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Northwind.Interfaces;

namespace GameStore.DAL.Concrete.RepositoryDecorators
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected IGameStoreUnitOfWork gameStore;
        protected INorthwindUnitOfWork northwind;

        protected BaseRepository(IGameStoreUnitOfWork gameStore, INorthwindUnitOfWork northwind)
        {
            this.gameStore = gameStore;
            this.northwind = northwind;
        }

        public abstract void Create(TEntity entity);

        public abstract void Update(TEntity entity);

        public abstract void Delete(int id);

        public abstract TEntity Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);

        public abstract IEnumerable<TEntity> GetAll();

        public abstract IEnumerable<TEntity> GetMany(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);

        public abstract int Count();

        public abstract bool IsExists(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
    }
}
