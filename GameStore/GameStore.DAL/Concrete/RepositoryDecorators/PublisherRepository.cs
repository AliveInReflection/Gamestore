using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using GameStore.DAL.Concrete.RepositoryDecorators;
using GameStore.DAL.GameStore.Interfaces;
using GameStore.DAL.Infrastructure;
using GameStore.DAL.Northwind.Interfaces;
using GameStore.Domain.Entities;

namespace GameStore.DAL.Concrete.Repositories
{
    public class PublisherRepository : BaseRepository<Publisher>
    {
        public PublisherRepository(IGameStoreUnitOfWork gameStore, INorthwindUnitOfWork northwind)
            : base(gameStore, northwind)
        {
            
        }

        public override void Create(Publisher entity)
        {
            gameStore.Publishers.Create(entity);
        }

        public override void Update(Publisher entity)
        {
            var database = KeyManager.GetDatabase(entity.PublisherId);

            if (database == DatabaseType.Northwind &&
                gameStore.Publishers.Get(m => m.PublisherId.Equals(entity.PublisherId)) == null)
            {
                Create(entity);
                return;
            }
            gameStore.Publishers.Update(entity);
        }

        public override void Delete(int id)
        {
            var database = KeyManager.GetDatabase(id);

            if (database == DatabaseType.Northwind &&
                gameStore.Publishers.Get(m => m.PublisherId.Equals(id)) == null)
            {
                var publiser = northwind.Publishers.Get(KeyManager.Decode(id));
                publiser.IsDeleted = true;
                Create(publiser);
                return;
            }

            gameStore.Publishers.Delete(id);
        }

        public override Publisher Get(Expression<Func<Publisher, bool>> predicate)
        {
            var publisher = gameStore.Publishers.GetMany(predicate).FirstOrDefault(m => !m.IsDeleted);
            if (publisher == null)
            {
                publisher = northwind.Publishers.GetAll(GetPubliserIdsToExclude()).First(predicate.Compile());
            }

            return publisher;
        }

        public override IEnumerable<Publisher> GetAll()
        {
            var publishers = gameStore.Publishers.GetMany(m => !m.IsDeleted).ToList();

            publishers.AddRange(northwind.Publishers.GetAll(GetPubliserIdsToExclude()));

            return publishers;
        }

        public override IEnumerable<Publisher> GetMany(Expression<Func<Publisher, bool>> predicate)
        {
            var publishers = gameStore.Publishers.GetMany(predicate).Where(m => !m.IsDeleted).ToList();

            publishers.AddRange(northwind.Publishers.GetAll(GetPubliserIdsToExclude()).Where(predicate.Compile()));

            return publishers;
        }

        public override int Count()
        {
            var gameStoreCount = gameStore.Publishers.GetMany(m => !m.IsDeleted).Count();
            var northwindCount = northwind.Publishers.Count(GetPubliserIdsToExclude());

            return gameStoreCount + northwindCount;
        }

        public override bool IsExists(Expression<Func<Publisher, bool>> predicate)
        {
            var gameStoreIsExists = gameStore.Publishers.GetMany(m => !m.IsDeleted).Any(predicate);
            return gameStoreIsExists ? gameStoreIsExists : northwind.Publishers.GetAll(GetPubliserIdsToExclude()).Any(predicate.Compile());
        }

        private IEnumerable<int> GetPubliserIdsToExclude()
        {
            return gameStore.Publishers.GetAll().Select(m => m.PublisherId).ToList()
                .Where(m => KeyManager.GetDatabase(m) == DatabaseType.Northwind)
                .Select(m => KeyManager.Decode(m));
        }
    }
}
