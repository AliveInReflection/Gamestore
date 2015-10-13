using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using GameStore.Domain.Entities.DAL.Context;
using GameStore.Domain.Entities.DAL.Context;
using GameStore.Domain.Entities.DAL.Interfaces;

namespace GameStore.Domain.Entities.Domain.Entities.DAL.Concrete
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DataContext context;

        public BaseRepository(DataContext context)
        {
            this.context = context;
        }

        public void Add(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Added;
        }

        public void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return context.Set<TEntity>().FirstOrDefault(where);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return context.Set<TEntity>().Where(where).ToList();
        }
    }
}
