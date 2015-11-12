using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamestore.DAL.Context;
using GameStore.DAL.GameStore.Interfaces;

namespace GameStore.DAL.GameStore.Concrete
{
    public abstract class BaseGameStoreRepository<TEntity> : IGameStoreRepository<TEntity> where TEntity : class
    {
        protected GameStoreContext context;

        protected BaseGameStoreRepository(GameStoreContext context)
        {
            this.context = context;
        }

        public virtual void Create(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public virtual TEntity Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public virtual IQueryable<TEntity> GetMany(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Where(predicate);
        }

        public abstract void Delete(int id);

        public abstract void Update(TEntity entity);


        public IQueryable<TEntity> GetAll()
        {
            return context.Set<TEntity>();
        }
    }
}
