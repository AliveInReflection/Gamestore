using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gamestore.DAL.Context;
using GameStore.DAL.Interfaces;

namespace GameStore.DAL.Concrete.Repositories
{
    public class PublisherRepository : IRepository<Publisher>
    {
        private GameStoreContext context;

        public PublisherRepository(GameStoreContext context)
        {
            this.context = context;
        }

        public void Create(Publisher entity)
        {
            context.Entry(entity).State = EntityState.Added;
        }

        public void Update(Publisher entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entry = context.Publishers.First(m => m.PublisherId.Equals(id));
            entry.IsDeleted = true;
        }

        public Publisher Get(System.Linq.Expressions.Expression<Func<Publisher, bool>> predicate)
        {
            return context.Publishers.Where(predicate).First(m => !m.IsDeleted);
        }

        public IEnumerable<Publisher> GetAll()
        {
            return context.Publishers.Where(m => !m.IsDeleted).ToList();
        }

        public IEnumerable<Publisher> GetMany(System.Linq.Expressions.Expression<Func<Publisher, bool>> predicate)
        {
            return context.Publishers.Where(predicate).Where(m => !m.IsDeleted).ToList();
        }

        public int Count()
        {
            return context.Publishers.Count(m => !m.IsDeleted);
        }

        public bool IsExists(System.Linq.Expressions.Expression<Func<Publisher, bool>> predicate)
        {
            return context.Publishers.Where(predicate).Any(m => !m.IsDeleted);
        }
    }
}
