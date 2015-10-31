using Gamestore.DAL.Context;
using GameStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace GameStore.DAL.Concrete
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DataContext context;

        public BaseRepository(DataContext context)
        {
            this.context = context;
        }

        public void Create(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Added;
        }

        public void Update(TEntity entity)
        {
            context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entity = context.Set<TEntity>().Find(id);
            context.Entry(entity).State = EntityState.Deleted;
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> where)
        {
            return context.Set<TEntity>().First(where);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return context.Set<TEntity>().Where(where).ToList();
        }

        public int Count()
        {
            return context.Set<TEntity>().Count() ;
        }
    }
}
