using System.Data;
using System.Linq;
using AutoMapper;
using Gamestore.DAL.Context;
using GameStore.DAL.Infrastructure;
using GameStore.Domain.Entities;

namespace GameStore.DAL.GameStore.Concrete
{
    class GameStorePublisherRepository : BaseGameStoreRepository<Publisher>
    {
        public GameStorePublisherRepository(GameStoreContext context)
            :base(context)
        {
            
        }

        public override void Create(Publisher entity)
        {
            if (entity.PublisherId == 0)
            {
                entity.PublisherId = GetId();
            }
            base.Create(entity);
        }

        public override void Delete(int id)
        {
            var entry = context.Publishers.First(m => m.PublisherId.Equals(id));
            entry.IsDeleted = true;
        }

        public override void Update(Publisher entity)
        {
            var entry = context.Publishers.First(m => m.PublisherId.Equals(entity.PublisherId));
            entry.CompanyName = entity.CompanyName;
            entry.Description = entity.Description;
            entry.HomePage = entity.HomePage;
            
        }

        private int GetId()
        {
            return context.Publishers.Select(m => m.PublisherId).OrderBy(m => m).ToList().Last() + KeyManager.Coefficient;
        }
    }
}
