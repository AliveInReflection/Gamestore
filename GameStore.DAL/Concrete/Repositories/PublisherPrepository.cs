using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Gamestore.DAL.Context;
using GameStore.DAL.Infrastructure;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Northwind.Interfaces;

namespace GameStore.DAL.Concrete.Repositories
{
    public class PublisherRepository : IRepository<Publisher>
    {
        private GameStoreContext context;
        private INorthwindUnitOfWork northwind;

        public PublisherRepository(GameStoreContext context, INorthwindUnitOfWork northwind)
        {
            this.context = context;
            this.northwind = northwind;
        }

        public void Create(Publisher entity)
        {
            if (entity.PublisherId == 0)
            {
                entity.PublisherId = GetId();
            }
            context.Publishers.Add(entity);
        }

        public void Update(Publisher entity)
        {
            var database = KeyManager.GetDatabase(entity.PublisherId);

            if (database == DatabaseType.Northwind)
            {
                Create(entity);
                return;
            }

            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var database = KeyManager.GetDatabase(id);

            if (database == DatabaseType.Northwind)
            {
                var publiser = northwind.Publishers.Get(KeyManager.Decode(id));
                publiser.IsDeleted = true;
                Create(publiser);
                return;
            }

            var entry = context.Publishers.First(m => m.PublisherId.Equals(id));
            entry.IsDeleted = true;
        }

        public Publisher Get(System.Linq.Expressions.Expression<Func<Publisher, bool>> predicate)
        {
            var publisherIdsToExclude = GetPubliserIdsToExclude();

            var publisher = context.Publishers.Where(predicate).FirstOrDefault(m => !m.IsDeleted);
            if (publisher == null)
            {
                publisher = northwind.Publishers.GetAll(publisherIdsToExclude).First(predicate.Compile());
            }

            return publisher;
        }

        public IEnumerable<Publisher> GetAll()
        {
            var publisherIdsToExclude = GetPubliserIdsToExclude();

            var publishers = context.Publishers.Where(m => !m.IsDeleted).ToList();

            publishers.AddRange(northwind.Publishers.GetAll(publisherIdsToExclude));

            return publishers;
        }

        public IEnumerable<Publisher> GetMany(System.Linq.Expressions.Expression<Func<Publisher, bool>> predicate)
        {
            var publisherIdsToExclude = GetPubliserIdsToExclude();

            var publishers = context.Publishers.Where(predicate).Where(m => !m.IsDeleted).ToList();

            publishers.AddRange(northwind.Publishers.GetAll(publisherIdsToExclude).Where(predicate.Compile()));

            return publishers;
        }

        public int Count()
        {
            var publisherIdsToExclude = GetPubliserIdsToExclude();

            var gameStoreCount = context.Publishers.Count(m => !m.IsDeleted);
            var northwindCount = northwind.Publishers.GetAll(publisherIdsToExclude).Count();

            return gameStoreCount + northwindCount;
        }

        public bool IsExists(System.Linq.Expressions.Expression<Func<Publisher, bool>> predicate)
        {
            var publisherIdsToExclude = GetPubliserIdsToExclude();

            var gameStoreIsExists = context.Publishers.Where(m => !m.IsDeleted).Any(predicate);
            var northwindIsExists = northwind.Publishers.GetAll(publisherIdsToExclude).Any(predicate.Compile());

            return gameStoreIsExists || northwindIsExists;
        }

        private IEnumerable<int> GetPubliserIdsToExclude()
        {
            return context.Publishers.ToList().Where(m => KeyManager.GetDatabase(m.PublisherId) == DatabaseType.Northwind).Select(m => KeyManager.Decode(m.PublisherId));
        }

        private int GetId()
        {
            return context.Publishers.Select(m => m.PublisherId).OrderBy(m => m).ToList().Last() + KeyManager.Coefficient;
        }
    }
}
